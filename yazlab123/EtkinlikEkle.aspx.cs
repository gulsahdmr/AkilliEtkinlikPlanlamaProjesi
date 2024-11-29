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
            // Kullanıcıdan gelen verileri al
            string etkinlikAdi = EtkinlikAdi.Value;
            string etkinlikAciklamasi = EtkinlikAciklamasi.Value;
            DateTime etkinlikTarihi = DateTime.Parse(EtkinlikTarihi.Value);
            TimeSpan etkinlikSaati = TimeSpan.Parse(EtkinlikSaati.Value);
            int etkinlikSuresi = int.Parse(EtkinlikSuresi.Value);
            string etkinlikKonumu = EtkinlikKonumu.Value;
            // ComboBox'tan seçilen ilgi alanını alıyoruz
            string etkinlikKategorisi = ddlIlgiAlanlari.SelectedValue;
            int kullaniciID = (int)Session["KullaniciID"]; // Kullanıcı ID'sini alıyoruz

            // Etkinlik nesnesini oluştur
            Etkinlik yeniEtkinlik = new Etkinlik
            {
                EtkinlikAdi = etkinlikAdi,
                EtkinlikAciklamasi = etkinlikAciklamasi,
                EtkinlikTarihi = etkinlikTarihi,
                EtkinlikSaati = etkinlikSaati,
                EtkinlikSuresi = etkinlikSuresi,
                EtkinlikKonumu = etkinlikKonumu,
                EtkinlikKategorisi = etkinlikKategorisi
            };

            // Web.config dosyasındaki bağlantı dizesini al
            string connectionString = ConfigurationManager.ConnectionStrings["YazlabConnection"].ConnectionString;

            // Veritabanına ekleme işlemi
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Etkinlikler (EtkinlikAdi, EtkinlikAciklamasi, EtkinlikTarihi, EtkinlikSaati, EtkinlikSuresi, EtkinlikKonumu, EtkinlikKategorisi, KullaniciID) " +
                                   "VALUES (@EtkinlikAdi, @EtkinlikAciklamasi, @EtkinlikTarihi, @EtkinlikSaati, @EtkinlikSuresi, @EtkinlikKonumu, @EtkinlikKategorisi, @KullaniciID)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@EtkinlikAdi", yeniEtkinlik.EtkinlikAdi);
                        cmd.Parameters.AddWithValue("@EtkinlikAciklamasi", yeniEtkinlik.EtkinlikAciklamasi);
                        cmd.Parameters.AddWithValue("@EtkinlikTarihi", yeniEtkinlik.EtkinlikTarihi);
                        cmd.Parameters.AddWithValue("@EtkinlikSaati", yeniEtkinlik.EtkinlikSaati);
                        cmd.Parameters.AddWithValue("@EtkinlikSuresi", yeniEtkinlik.EtkinlikSuresi);
                        cmd.Parameters.AddWithValue("@EtkinlikKonumu", yeniEtkinlik.EtkinlikKonumu);
                        cmd.Parameters.AddWithValue("@EtkinlikKategorisi", yeniEtkinlik.EtkinlikKategorisi);
                        cmd.Parameters.AddWithValue("@KullaniciID", kullaniciID);
                        cmd.ExecuteNonQuery();
                    }

                    // Etkinlik başarıyla eklendiğinde başarılı mesajı göster
                    lblMessage.Text = "Etkinlik başarıyla eklendi! Anasayfaya yönlendiriliyorsunuz...";
                    lblMessage.Visible = true; // Etiket görünür yapıldı
                    lblErrorMessage.Visible = false; // Hata mesajı varsa gizlensin
                    lblMessage.ForeColor = System.Drawing.Color.Green;

                    // JavaScript kodu ile 3 saniye sonra anasayfaya yönlendirme
                    ClientScript.RegisterStartupScript(this.GetType(), "Redirect", "setTimeout(function(){ window.location.href='Anasayfa.aspx'; }, 3000);", true);
                }
                catch (Exception ex)
                {
                    // Hata mesajını kullanıcıya göster
                    lblErrorMessage.Text = "Bir hata oluştu: " + ex.Message;
                    lblErrorMessage.Visible = true; // Hata etiketini görünür yap
                    lblMessage.Visible = false; // Başarı mesajı varsa gizlensin
                }
            }
        }
    }
}
