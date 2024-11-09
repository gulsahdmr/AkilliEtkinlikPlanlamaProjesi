using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace yazlab123
{
    public partial class Login : System.Web.UI.Page
    {
        protected void btnGiris_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = txtKullaniciAdi.Text;
            string sifre = txtSifre.Text;

            string connectionString = ConfigurationManager.ConnectionStrings["YazlabConnection"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT KullaniciID FROM Kullanicilar WHERE KullaniciAdi = @KullaniciAdi AND Sifre = @Sifre";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);
                cmd.Parameters.AddWithValue("@Sifre", sifre);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();  // İlk satırı al
                    int kullaniciID = (int)reader["KullaniciID"];  // Kullanıcı ID'sini al
                    Session["KullaniciID"] = kullaniciID;  // Oturuma kullanıcı ID'sini kaydet

                    // Giriş başarılı, oturumu başlat ve Anasayfa'ya yönlendir
                    Session["Username"] = kullaniciAdi;  // Oturumda kullanıcı adını saklıyoruz
                    Response.Redirect("Anasayfa.aspx");  // Veya giriş sonrası yönlendirmek istediğiniz sayfa
                }
                else
                {
                    // Hatalı giriş mesajı göster
                    lblMesaj.Text = "Kullanıcı adı veya şifre hatalı.";
                }

                reader.Close();  // Okuyucuyu kapat
            }
        }
        protected void btnSifremiUnuttum_Click(object sender, EventArgs e)
        {
            // Şifremi Unuttum butonuna tıklanıldığında, kullanıcıyı şifre sıfırlama sayfasına yönlendir.
            Response.Redirect("SifremiUnuttum.aspx");
        }

    }
}
