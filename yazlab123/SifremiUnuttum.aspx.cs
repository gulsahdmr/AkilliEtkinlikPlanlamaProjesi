using System;
using System.Data.SqlClient;
using System.Configuration;

namespace yazlab123
{
    public partial class SifremiUnuttum : System.Web.UI.Page
    {
        protected void btnSifreYenile_Click(object sender, EventArgs e)
        {
            string eposta = txtEposta.Text;
            string yeniSifre = txtYeniSifre.Text;

            string connString = ConfigurationManager.ConnectionStrings["YazlabConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                // Kullanıcıyı e-posta ile kontrol et
                string kontrolQuery = "SELECT COUNT(*) FROM Kullanicilar WHERE Eposta = @Eposta";
                SqlCommand kontrolCmd = new SqlCommand(kontrolQuery, conn);
                kontrolCmd.Parameters.AddWithValue("@Eposta", eposta);

                conn.Open();
                int kullaniciVar = (int)kontrolCmd.ExecuteScalar();
                conn.Close();

                if (kullaniciVar == 0)
                {
                    // E-posta bulunamazsa hata mesajı göster
                    lblMesaj.Text = "Bu e-posta adresi sistemde bulunamadı.";
                    return;
                }

                // Yeni şifreyi güncelle
                string updateQuery = "UPDATE Kullanicilar SET Sifre = @YeniSifre WHERE Eposta = @Eposta";
                SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
                updateCmd.Parameters.AddWithValue("@YeniSifre", yeniSifre);
                updateCmd.Parameters.AddWithValue("@Eposta", eposta);

                try
                {
                    conn.Open();
                    updateCmd.ExecuteNonQuery();
                 
                    lblMesaj.Text = "Şifreniz başarıyla güncellendi! Girişe  yönlendiriliyorsunuz...";
                    lblMesaj.ForeColor = System.Drawing.Color.Green;

                    // JavaScript kodu ile 3 saniye sonra anasayfaya yönlendirme
                    ClientScript.RegisterStartupScript(this.GetType(), "Redirect", "setTimeout(function(){ window.location.href='Login.aspx'; }, 3000);", true);

                }
                catch (Exception ex)
                {
                    lblMesaj.Text = "Hata: " + ex.Message;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
