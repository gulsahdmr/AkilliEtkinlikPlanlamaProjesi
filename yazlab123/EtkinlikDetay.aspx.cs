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
                lblMesaj.Text = "Lütfen önce giriş yapın.";  // Oturum yoksa, kullanıcıya giriş yapması gerektiğini belirtiyoruz
                return;
            }

            int kullaniciID = Convert.ToInt32(Session["KullaniciID"]);  // Kullanıcı ID'sini oturumdan alın

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
                    // Eğer etkinlik sahibiyse, katıl butonunu gizle
                    if (etkinlikSahibiID == kullaniciID)
                    {
                        KatilButton.Visible = false; // Katıl butonunu gizle
                        AutoJoinEvent(etkinlikID, kullaniciID); // Otomatik katılım işlemini yap
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

        protected void KatilButton_Click(object sender, EventArgs e)
        {
            // Etkinliğe katılma işlemi burada yapılacak
            if (Session["KullaniciID"] == null)
            {
                lblMesaj.Text = "Lütfen önce giriş yapın.";  // Oturum yoksa, kullanıcıya giriş yapması gerektiğini belirtiyoruz
                return;
            }

            // Kullanıcı ID'sini oturumdan al
            int etkinlikID = Convert.ToInt32(Request.QueryString["EtkinlikID"]);  // URL'den EtkinlikID'yi al
            int kullaniciID = (int)Session["KullaniciID"];  // Kullanıcı ID'sini oturumdan alın

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
                if (existingParticipation > 0)
                {
                    lblMesaj.Text = "Etkinliğe  zaten Katıldınız!! Etkinlik sayfasına  yönlendiriliyorsunuz...";
                    lblMesaj.ForeColor = System.Drawing.Color.Green;

                    // JavaScript kodu ile 3 saniye sonra anasayfaya yönlendirme
                    ClientScript.RegisterStartupScript(this.GetType(), "Redirect", "setTimeout(function(){ window.location.href='EtkinlikSayfasi.aspx'; }, 3000);", true);
                    return;
                }

                // Eğer katılım yoksa, yeni katılım ekle
                string query = "INSERT INTO Katilimcilar (KullaniciID, EtkinlikID) VALUES (@KullaniciID, @EtkinlikID)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@KullaniciID", kullaniciID);
                cmd.Parameters.AddWithValue("@EtkinlikID", etkinlikID);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    lblMesaj.Text = "Etkinliğe katıldınız!  Etkinlik sayfasına  yönlendiriliyorsunuz..";
                    lblMesaj.ForeColor = System.Drawing.Color.Green;
                    // JavaScript kodu ile 3 saniye sonra anasayfaya yönlendirme
                    ClientScript.RegisterStartupScript(this.GetType(), "Redirect", "setTimeout(function(){ window.location.href='EtkinlikSayfasi.aspx'; }, 3000);", true);
                }
                catch (Exception ex)
                {
                    lblMesaj.Text = "Bir hata oluştu: " + ex.Message;
                    lblMesaj.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
}
