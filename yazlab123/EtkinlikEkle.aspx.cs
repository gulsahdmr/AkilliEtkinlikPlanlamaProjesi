using System;
using System.Web.UI;
using System.Data.SqlClient;
using System.Configuration;

namespace yazlab123
{
    public partial class EtkinlikEkle : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
            {
                Response.Redirect("Login.aspx"); // Oturum açmamış kullanıcıyı giriş sayfasına yönlendir
            }
        }

        // Etkinlik Ekleme işlevi
        protected void SubmitEtkinlik(object sender, EventArgs e)
        {
            string etkinlikAdi = EtkinlikAdi.Value;
            string etkinlikAciklamasi = EtkinlikAciklamasi.Value;
            DateTime etkinlikTarihi = DateTime.Parse(EtkinlikTarihi.Value);
            TimeSpan etkinlikSaati = TimeSpan.Parse(EtkinlikSaati.Value);
            int etkinlikSuresi = int.Parse(EtkinlikSuresi.Value);
            string etkinlikKonumu = EtkinlikKonumu.Value;
            string etkinlikKategorisi = ddlIlgiAlanlari.SelectedValue;
            int kullaniciID = (int)Session["KullaniciID"];

            string connectionString = ConfigurationManager.ConnectionStrings["YazlabConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Çakışma kontrolü için sorgu
                    string checkQuery = @"
                SELECT COUNT(*) 
                FROM Etkinlikler 
                WHERE 
                    KullaniciID = @KullaniciID AND
                    EtkinlikTarihi = @EtkinlikTarihi AND
                    (
                        (EtkinlikSaati <= @EtkinlikSaati AND DATEADD(MINUTE, EtkinlikSuresi, EtkinlikSaati) > @EtkinlikSaati) OR
                        (@EtkinlikSaati <= EtkinlikSaati AND DATEADD(MINUTE, @EtkinlikSuresi, @EtkinlikSaati) > EtkinlikSaati)
                    )";

                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@KullaniciID", kullaniciID);
                        checkCmd.Parameters.AddWithValue("@EtkinlikTarihi", etkinlikTarihi);
                        checkCmd.Parameters.AddWithValue("@EtkinlikSaati", etkinlikSaati);
                        checkCmd.Parameters.AddWithValue("@EtkinlikSuresi", etkinlikSuresi);

                        int conflictCount = (int)checkCmd.ExecuteScalar();
                        if (conflictCount > 0)
                        {
                            // Çakışma durumu varsa kullanıcıya mesaj göster
                            lblErrorMessage.Text = "Bu tarih ve saatte başka bir etkinlik mevcut. Lütfen farklı bir saat seçin.";
                            lblErrorMessage.Visible = true;
                            lblMessage.Visible = false;
                            lblMessage.ForeColor = System.Drawing.Color.Green;
                            ClientScript.RegisterStartupScript(this.GetType(), "Redirect", "setTimeout(function(){ window.location.href='Anasayfa.aspx'; }, 3000);", true);
                            return;
                        }
                    }

                    // Çakışma yoksa etkinlik ekleme işlemi
                    string insertQuery = @"
                INSERT INTO Etkinlikler 
                (EtkinlikAdi, EtkinlikAciklamasi, EtkinlikTarihi, EtkinlikSaati, EtkinlikSuresi, EtkinlikKonumu, EtkinlikKategorisi, KullaniciID) 
                VALUES 
                (@EtkinlikAdi, @EtkinlikAciklamasi, @EtkinlikTarihi, @EtkinlikSaati, @EtkinlikSuresi, @EtkinlikKonumu, @EtkinlikKategorisi, @KullaniciID)";

                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@EtkinlikAdi", etkinlikAdi);
                        insertCmd.Parameters.AddWithValue("@EtkinlikAciklamasi", etkinlikAciklamasi);
                        insertCmd.Parameters.AddWithValue("@EtkinlikTarihi", etkinlikTarihi);
                        insertCmd.Parameters.AddWithValue("@EtkinlikSaati", etkinlikSaati);
                        insertCmd.Parameters.AddWithValue("@EtkinlikSuresi", etkinlikSuresi);
                        insertCmd.Parameters.AddWithValue("@EtkinlikKonumu", etkinlikKonumu);
                        insertCmd.Parameters.AddWithValue("@EtkinlikKategorisi", etkinlikKategorisi);
                        insertCmd.Parameters.AddWithValue("@KullaniciID", kullaniciID);

                        insertCmd.ExecuteNonQuery();
                    }

                    // Başarılı mesaj
                    lblMessage.Text = "Etkinlik başarıyla eklendi! Anasayfaya yönlendiriliyorsunuz...";
                    lblMessage.Visible = true;
                    lblErrorMessage.Visible = false;
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    ClientScript.RegisterStartupScript(this.GetType(), "Redirect", "setTimeout(function(){ window.location.href='Anasayfa.aspx'; }, 3000);", true);
                }
                catch (Exception ex)
                {
                    lblErrorMessage.Text = "Bir hata oluştu: " + ex.Message;
                    lblErrorMessage.Visible = true;
                    lblMessage.Visible = false;
                }
            }
        }

    }
}
