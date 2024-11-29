using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Linq;

namespace yazlab123
{
    public partial class Register : System.Web.UI.Page
    {
        protected void btnKayitOl_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = txtKullaniciAdi.Text;
            string sifre = txtSifre.Text;
            string eposta = txtEposta.Text;
            string ad = txtAd.Text;
            string soyad = txtSoyad.Text;
            DateTime dogumTarihi = DateTime.Parse(txtDogumTarihi.Text);
            string cinsiyet = ddlCinsiyet.SelectedValue;
            string telefon = txtTelefon.Text;

            // CheckBoxList'ten seçilen ilgi alanlarını alıyoruz
            string ilgiAlanlari = string.Join(",", chkIlgiAlanlari.Items.Cast<ListItem>()
                                                 .Where(i => i.Selected)
                                                 .Select(i => i.Value)
                                                 .ToArray());

            string profilFoto = "";

            if (fuProfilFoto.HasFile)
            {
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + fuProfilFoto.FileName;
                profilFoto = "/uploads/" + uniqueFileName;
                fuProfilFoto.SaveAs(Server.MapPath(profilFoto));
            }

            string connString = ConfigurationManager.ConnectionStrings["YazlabConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                // Kullanıcı adı ve e-posta kontrolü
                SqlCommand kontrolCommand = new SqlCommand("SELECT COUNT(*) FROM Kullanicilar WHERE KullaniciAdi = @KullaniciAdi OR Eposta = @Eposta", conn);
                kontrolCommand.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);
                kontrolCommand.Parameters.AddWithValue("@Eposta", eposta);
                conn.Open();
                int mevcutKayitSayisi = (int)kontrolCommand.ExecuteScalar();
                conn.Close();

                if (mevcutKayitSayisi > 0)
                {
                    lblMesaj.Text = "Bu kullanıcı adı veya e-posta zaten kayıtlı. Lütfen farklı bir kullanıcı adı veya e-posta deneyin.";
                    return;
                }

                // Kullanıcı bilgilerini veritabanına ekleme
                string query = "INSERT INTO Kullanicilar (KullaniciAdi, Sifre, Eposta, Ad, Soyad, DogumTarihi, Cinsiyet, TelefonNo, ilgiAlanlari, ProfilFoto) " +
                               "VALUES (@KullaniciAdi, @Sifre, @Eposta, @Ad, @Soyad, @DogumTarihi, @Cinsiyet, @TelefonNo, @ilgiAlanlari, @ProfilFoto)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);
                cmd.Parameters.AddWithValue("@Sifre", sifre);
                cmd.Parameters.AddWithValue("@Eposta", eposta);
                cmd.Parameters.AddWithValue("@Ad", ad);
                cmd.Parameters.AddWithValue("@Soyad", soyad);
                cmd.Parameters.AddWithValue("@DogumTarihi", dogumTarihi);
                cmd.Parameters.AddWithValue("@Cinsiyet", cinsiyet);
                cmd.Parameters.AddWithValue("@TelefonNo", telefon);
                cmd.Parameters.AddWithValue("@ilgiAlanlari", ilgiAlanlari); // Seçilen ilgi alanlarını kaydediyoruz
                cmd.Parameters.AddWithValue("@ProfilFoto", profilFoto);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();

                    lblMesaj.Text = "Kayıt başarıyla tamamlandı. Girişe yönlendiriliyorsunuz...";
                    lblMesaj.ForeColor = System.Drawing.Color.Green;

                    // JavaScript kodu ile 3 saniye sonra anasayfaya yönlendirme
                    ClientScript.RegisterStartupScript(this.GetType(), "Redirect", "setTimeout(function(){ window.location.href='Giris.aspx'; }, 2000);", true);
                    return;
                }
                catch (Exception ex)
                {
                    lblMesaj.Text = "Hata: " + ex.Message;
                }
            }
        }


    }
}
