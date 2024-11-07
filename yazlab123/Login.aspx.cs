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
                string query = "SELECT COUNT(*) FROM Kullanicilar WHERE KullaniciAdi = @KullaniciAdi AND Sifre = @Sifre";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);
                cmd.Parameters.AddWithValue("@Sifre", sifre);

                con.Open();
                int count = (int)cmd.ExecuteScalar();
                con.Close();

                if (count == 1)
                {
                    // Giriş başarılı, oturumu başlat ve Etkinlikler sayfasına yönlendir
                    Session["Username"] = kullaniciAdi;  // Oturumda kullanıcı adını saklıyoruz
                    Response.Redirect("Anasayfa.aspx");
                }
                else
                {
                    // Hatalı giriş mesajı göster
                    lblMesaj.Text = "Kullanıcı adı veya şifre hatalı.";
                }
            }
        }
    }
}
