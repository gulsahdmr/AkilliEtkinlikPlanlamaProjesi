using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace yazlab123
{
    public partial class EtkinlikDetay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int etkinlikID;
                if (int.TryParse(Request.QueryString["EtkinlikID"], out etkinlikID))
                {
                    ViewState["EtkinlikID"] = etkinlikID; // Etkinlik ID'yi ViewState'e kaydediyoruz
                    LoadEventDetails(etkinlikID);
                    CheckUserEvent(etkinlikID); // Kullanıcının kendi etkinliği olup olmadığını kontrol et


                }
            }
        }

        private void LoadEventDetails(int etkinlikID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["YazlabConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
        SELECT EtkinlikAdi, EtkinlikAciklamasi, EtkinlikTarihi, EtkinlikSaati, 
               EtkinlikSuresi, EtkinlikKonumu, EtkinlikKategorisi, KullaniciID
        FROM Etkinlikler 
        WHERE EtkinlikID = @EtkinlikID";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@EtkinlikID", etkinlikID);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    eventNameLabel.Text = reader["EtkinlikAdi"].ToString();
                    eventDescriptionLabel.Text = reader["EtkinlikAciklamasi"].ToString();
                    eventDateLabel.Text = Convert.ToDateTime(reader["EtkinlikTarihi"]).ToString("dd MMMM yyyy");
                    eventTimeLabel.Text = reader["EtkinlikSaati"].ToString();
                    eventDurationLabel.Text = reader["EtkinlikSuresi"].ToString();
                    eventLocationLabel.Text = reader["EtkinlikKonumu"].ToString();
                    eventCategoryLabel.Text = reader["EtkinlikKategorisi"].ToString();

                    // Kullanıcı ID'sini kontrol et
                    if (reader["KullaniciID"] != DBNull.Value)
                    {
                        int etkinlikSahibiID = Convert.ToInt32(reader["KullaniciID"]);
                        int kullaniciID = Convert.ToInt32(Session["KullaniciID"]);

                        // Eğer etkinlik sahibiyse, katıl butonunu gizle
                        if (etkinlikSahibiID == kullaniciID)
                        {
                            KatilButton.Visible = false; // Katıl butonunu gizle
                            AutoJoinEvent(etkinlikID, kullaniciID); // Otomatik katılım işlemini yap
                        }
                    }
                }
                reader.Close();
            }
        }

        private void CheckUserEvent(int etkinlikID)
        {
            if (Session["KullaniciID"] == null)
            {
                lblMesaj.Text = "Lütfen önce giriş yapın."; // Oturum yoksa, kullanıcıya giriş yapması gerektiğini belirt
                return;
            }

            int kullaniciID = Convert.ToInt32(Session["KullaniciID"]); // Kullanıcı ID'sini oturumdan al

            string connectionString = ConfigurationManager.ConnectionStrings["YazlabConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Etkinlik sahibinin ID'sini kontrol ediyoruz
                string query = "SELECT KullaniciID FROM Etkinlikler WHERE EtkinlikID = @EtkinlikID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@EtkinlikID", etkinlikID);

                conn.Open();
                object result = cmd.ExecuteScalar();
                conn.Close();

                if (result != DBNull.Value)
                {
                    int etkinlikSahibiID = Convert.ToInt32(result);

                    // Eğer kullanıcı admin ise (KullaniciID = 1), "Onayla" butonu görünür
                    if (kullaniciID == 1)
                    {
                        Onayla.Visible = true;
                        SilButton.Visible = true; // Diğer butonları gizle
                        GuncelleButton.Visible = true;
                    }
                    // Eğer kullanıcı etkinlik sahibiyse, "Sil" ve "Güncelle" butonları görünür
                    else if (etkinlikSahibiID == kullaniciID)
                    {
                        SilButton.Visible = true;
                        GuncelleButton.Visible = true;
                        Onayla.Visible = false; // Admin butonunu gizle
                    }
                    else
                    {
                        // Diğer kullanıcılar için tüm butonları gizle
                        SilButton.Visible = false;
                        GuncelleButton.Visible = false;
                        Onayla.Visible = false;
                    }
                }
            }
        }



        private void AutoJoinEvent(int etkinlikID, int kullaniciID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["YazlabConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Katılımın zaten var olup olmadığını kontrol et
                string checkQuery = "SELECT COUNT(*) FROM Katilimcilar WHERE KullaniciID = @KullaniciID AND EtkinlikID = @EtkinlikID";
                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@KullaniciID", kullaniciID);
                checkCmd.Parameters.AddWithValue("@EtkinlikID", etkinlikID);

                conn.Open();
                int existingParticipation = (int)checkCmd.ExecuteScalar();
                conn.Close();

                // Eğer kullanıcı daha önce katılmışsa, tekrar eklemeyi engelle
                if (existingParticipation == 0)
                {
                    // Eğer katılım yoksa, yeni katılım ekle
                    string query = "INSERT INTO Katilimcilar (KullaniciID, EtkinlikID) VALUES (@KullaniciID, @EtkinlikID)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@KullaniciID", kullaniciID);
                    cmd.Parameters.AddWithValue("@EtkinlikID", etkinlikID);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        lblMesaj.Text = "Bir hata oluştu: " + ex.Message;
                        lblMesaj.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }


        protected void SilButton_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["EtkinlikID"] != null)
            {
                int etkinlikID;
                if (int.TryParse(Request.QueryString["EtkinlikID"], out etkinlikID))
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["YazlabConnection"].ConnectionString;

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        try
                        {
                            conn.Open();

                            // 1. Katılımcıları sil
                            string deleteParticipantsQuery = "DELETE FROM Katilimcilar WHERE EtkinlikID = @EtkinlikID";
                            using (SqlCommand deleteParticipantsCmd = new SqlCommand(deleteParticipantsQuery, conn))
                            {
                                deleteParticipantsCmd.Parameters.AddWithValue("@EtkinlikID", etkinlikID);
                                deleteParticipantsCmd.ExecuteNonQuery();
                            }

                            // 2. Etkinliği sil
                            string deleteEventQuery = "DELETE FROM Etkinlikler WHERE EtkinlikID = @EtkinlikID";
                            using (SqlCommand deleteEventCmd = new SqlCommand(deleteEventQuery, conn))
                            {
                                deleteEventCmd.Parameters.AddWithValue("@EtkinlikID", etkinlikID);
                                deleteEventCmd.ExecuteNonQuery();
                            }

                            lblMesaj.Text = "Etkinlik Başarıyla Silindi!! Anasayfaya  yönlendiriliyorsunuz...";
                            lblMesaj.ForeColor = System.Drawing.Color.Green;

                            // JavaScript kodu ile 3 saniye sonra anasayfaya yönlendirme
                            ClientScript.RegisterStartupScript(this.GetType(), "Redirect", "setTimeout(function(){ window.location.href='Anasayfa.aspx'; }, 3000);", true);

                        }
                        catch (Exception ex)
                        {
                            lblMesaj.Text = "Hata: " + ex.Message;
                            lblMesaj.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
               
            }
          
        }

        protected void GuncelleButton_Click(object sender, EventArgs e)
        {
            // URL'den EtkinlikID'yi al
            string etkinlikID = Request.QueryString["EtkinlikID"];

            // EtkinlikID boş değilse yönlendirme yap
            if (!string.IsNullOrEmpty(etkinlikID))
            {
                // EtkinlikGuncelle sayfasına yönlendir
                Response.Redirect("EtkinlikGuncelle.aspx?EtkinlikID=" + etkinlikID);
            }
            else
            {
                // EtkinlikID parametresi bulunamadıysa hata mesajı
                lblMesaj.Text = "Geçersiz Etkinlik ID!";
                lblMesaj.ForeColor = System.Drawing.Color.Red;
            }
        }



        protected void KatilButton_Click(object sender, EventArgs e)
        {
            // Kullanıcı giriş yapmamışsa uyarı ver
            if (Session["KullaniciID"] == null)
            {
                lblMesaj.Text = "Lütfen önce giriş yapın.";
                lblMesaj.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // Kullanıcı ve etkinlik bilgilerini al
            int etkinlikID = Convert.ToInt32(Request.QueryString["EtkinlikID"]);
            int kullaniciID = (int)Session["KullaniciID"];

            string connectionString = ConfigurationManager.ConnectionStrings["YazlabConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Kullanıcı daha önce etkinliğe katılmış mı kontrol et
                    string checkQuery = "SELECT COUNT(*) FROM Katilimcilar WHERE KullaniciID = @KullaniciID AND EtkinlikID = @EtkinlikID";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@KullaniciID", kullaniciID);
                    checkCmd.Parameters.AddWithValue("@EtkinlikID", etkinlikID);

                    int existingParticipation = (int)checkCmd.ExecuteScalar();
                    if (existingParticipation > 0)
                    {
                        lblMesaj.Text = "Bu etkinliğe zaten katıldınız! Etkinlik Sayfasına yönlendiriliyorsunuz";
                        lblMesaj.ForeColor = System.Drawing.Color.Green;
                        ClientScript.RegisterStartupScript(this.GetType(), "Redirect", "setTimeout(function(){ window.location.href='EtkinlikSayfasi.aspx'; }, 3000);", true);
                        return;
                    }

                    // Etkinlik bilgilerini al
                    string etkinlikQuery = "SELECT EtkinlikTarihi, EtkinlikSaati, EtkinlikSuresi FROM Etkinlikler WHERE EtkinlikID = @EtkinlikID";
                    SqlCommand etkinlikCmd = new SqlCommand(etkinlikQuery, conn);
                    etkinlikCmd.Parameters.AddWithValue("@EtkinlikID", etkinlikID);

                    SqlDataReader reader = etkinlikCmd.ExecuteReader();
                    DateTime etkinlikBaslangic = DateTime.MinValue;
                    TimeSpan etkinlikSuresi = TimeSpan.Zero;

                    if (reader.Read())
                    {
                        DateTime etkinlikTarihi = (DateTime)reader["EtkinlikTarihi"];
                        TimeSpan etkinlikSaati = (TimeSpan)reader["EtkinlikSaati"];
                        int etkinlikSuresiDakika = (int)reader["EtkinlikSuresi"];

                        etkinlikBaslangic = etkinlikTarihi.Add(etkinlikSaati);
                        etkinlikSuresi = TimeSpan.FromMinutes(etkinlikSuresiDakika);
                    }
                    reader.Close();

                    // Etkinlik bitiş saatini hesapla
                    DateTime etkinlikBitis = etkinlikBaslangic.Add(etkinlikSuresi);

                    // Kullanıcının katıldığı etkinliklerin zaman çakışmasını kontrol et
                    // Kullanıcının mevcut etkinlik zamanlarıyla çakışıp çakışmadığını kontrol et
                    string overlappingCheckQuery = @"
SELECT COUNT(*) 
FROM Etkinlikler e
JOIN Katilimcilar k ON e.EtkinlikID = k.EtkinlikID
WHERE k.KullaniciID = @KullaniciID
AND (
    -- İlk koşul: Etkinlik başlangıcı ve bitiş saati
    (DATEADD(SECOND, DATEDIFF(SECOND, '00:00:00', e.EtkinlikSaati), CAST(e.EtkinlikTarihi AS DATETIME)) >= @EtkinlikBaslangicSaati
        AND DATEADD(SECOND, DATEDIFF(SECOND, '00:00:00', e.EtkinlikSaati), CAST(e.EtkinlikTarihi AS DATETIME)) < @EtkinlikBitisSaati)
    OR
    -- İkinci koşul: Etkinlik süresi
    (DATEADD(MINUTE, e.EtkinlikSuresi, DATEADD(SECOND, DATEDIFF(SECOND, '00:00:00', e.EtkinlikSaati), CAST(e.EtkinlikTarihi AS DATETIME))) > @EtkinlikBaslangicSaati 
        AND DATEADD(MINUTE, e.EtkinlikSuresi, DATEADD(SECOND, DATEDIFF(SECOND, '00:00:00', e.EtkinlikSaati), CAST(e.EtkinlikTarihi AS DATETIME))) <= @EtkinlikBitisSaati)
)
";

                    SqlCommand overlapCheckCmd = new SqlCommand(overlappingCheckQuery, conn);
                    overlapCheckCmd.Parameters.AddWithValue("@KullaniciID", kullaniciID);
                    overlapCheckCmd.Parameters.AddWithValue("@EtkinlikBaslangicSaati", etkinlikBaslangic); // Başlangıç zamanı
                    overlapCheckCmd.Parameters.AddWithValue("@EtkinlikBitisSaati", etkinlikBitis);       // Bitiş zamanı

                    // Sorguyu çalıştır ve çakışma sayısını kontrol et
                    int overlapCount = (int)overlapCheckCmd.ExecuteScalar();

                    // Eğer zaman çakışması varsa, katılım yapılmasın
                    if (overlapCount > 0)
                    {
                        lblMesaj.Text = "Bu tarihte ve saatte başka bir etkinliğe katıldığınız için bu etkinliğe katılamazsınız! Etkinlik Sayfasına yönlendiriliyorsunuz";
                        lblMesaj.ForeColor = System.Drawing.Color.Red;
                        ClientScript.RegisterStartupScript(this.GetType(), "Redirect", "setTimeout(function(){ window.location.href='EtkinlikSayfasi.aspx'; }, 3000);", true);
                        return;
                    }


                    // Katılım ekle
                    string insertQuery = "INSERT INTO Katilimcilar (KullaniciID, EtkinlikID) VALUES (@KullaniciID, @EtkinlikID)";
                    SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                    insertCmd.Parameters.AddWithValue("@KullaniciID", kullaniciID);
                    insertCmd.Parameters.AddWithValue("@EtkinlikID", etkinlikID);

                    insertCmd.ExecuteNonQuery();

                    lblMesaj.Text = "Etkinliğe Katıldınız ! Etkinlik Sayfasına yönlendiriliyorsunuz..";
                    lblMesaj.ForeColor = System.Drawing.Color.Green;

                    // JavaScript ile 3 saniye sonra yönlendirme yapılacak
                    ClientScript.RegisterStartupScript(this.GetType(), "Redirect", "setTimeout(function(){ window.location.href='EtkinlikSayfasi.aspx'; }, 3000);", true);
                }
                catch (Exception ex)
                {
                    lblMesaj.Text = "Bir hata oluştu: " + ex.Message;
                    lblMesaj.ForeColor = System.Drawing.Color.Red;
                }
                finally
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                        conn.Close();
                }
            }
        }



        protected void OnaylaButton_Click(object sender, EventArgs e)
        {
            // URL'den EtkinlikID'yi al
            string etkinlikID = Request.QueryString["EtkinlikID"];

            if (!string.IsNullOrEmpty(etkinlikID))
            {
                // Veritabanı bağlantı dizesi
                string connStr = ConfigurationManager.ConnectionStrings["YazlabConnection"].ConnectionString;

                // SQL sorgusunu hazırlıyoruz: Onay'ı 1 yaparak etkinlikyi onaylıyoruz
                string query = "UPDATE Etkinlikler SET Onay = @Onay WHERE EtkinlikID = @EtkinlikID";

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);

                    // Parametreleri ekliyoruz
                    cmd.Parameters.AddWithValue("@Onay", 1); // Onay değeri 1 (true) olarak set edilir
                    cmd.Parameters.AddWithValue("@EtkinlikID", etkinlikID); // EtkinlikID'yi parametre olarak ekliyoruz

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery(); // Sorguyu çalıştırıyoruz

                        // İşlem başarılıysa kullanıcıya "Etkinlik Onaylandı" mesajını gösterebiliriz
                        OnayliMesajLabel.Text = "Etkinlik Onaylandı. Etkinlik Sayfasına yönlendiriliyorsunuz";
                        OnayliMesajLabel.ForeColor = System.Drawing.Color.Red;
                        OnayliMesajLabel.Visible = true;




                        // JavaScript kodu ile 3 saniye sonra anasayfaya yönlendirme
                        ClientScript.RegisterStartupScript(this.GetType(), "Redirect", "setTimeout(function(){ window.location.href='EtkinlikSayfasi.aspx'; }, 3000);", true);
                    }
                    catch (Exception ex)
                    {
                        // Hata meydana gelirse hata mesajını göster
                        ErrorMessageLabel.Text = "Hata: " + ex.Message;
                        ErrorMessageLabel.Visible = true;
                    }
                }
            }
            
        }


    }
}