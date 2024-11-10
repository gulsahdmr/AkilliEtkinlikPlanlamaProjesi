using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace yazlab123
{
    public partial class KullaniciProfili : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KullaniciID"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else if (!IsPostBack)
            {
                int kullaniciID = (int)Session["KullaniciID"];
                LoadProfile(kullaniciID);
                LoadEtkinlikler(kullaniciID);
            }
        }

        private void LoadProfile(int kullaniciID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["YazlabConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Kullanicilar WHERE KullaniciID = @KullaniciID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@KullaniciID", kullaniciID);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // Adjusted field names to match your database
                    txtAd.Text = reader["KullaniciAdi"].ToString();
                    txtSoyad.Text = reader["Soyad"].ToString();
                    txtEposta.Text = reader["Eposta"].ToString();
                    txtTelefonNo.Text = reader["TelefonNo"].ToString();
                    txtIlgiAlanlari.Text = reader["ilgiAlanlari"].ToString();
                    txtKonum.Text = reader["Konum"].ToString();
                  

                    if (reader["ProfilFoto"] != DBNull.Value)
                    {
                        imgProfilFoto.ImageUrl = reader["ProfilFoto"].ToString();
                    }

                }

                reader.Close();
            }
        }


        protected void btnKaydet_Click(object sender, EventArgs e)
        {
            int kullaniciID = (int)Session["KullaniciID"];
            string connectionString = ConfigurationManager.ConnectionStrings["YazlabConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Kullanicilar SET Ad = @Ad, Soyad = @Soyad, Eposta = @Eposta, " +
                               "TelefonNo = @TelefonNo, ilgiAlanlari = @ilgiAlanlari, Konum = @Konum " +
                               "WHERE KullaniciID = @KullaniciID";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Ad", txtAd.Text);
                cmd.Parameters.AddWithValue("@Soyad", txtSoyad.Text);
                cmd.Parameters.AddWithValue("@Eposta", txtEposta.Text);
                cmd.Parameters.AddWithValue("@TelefonNo", txtTelefonNo.Text);
                cmd.Parameters.AddWithValue("@ilgiAlanlari", txtIlgiAlanlari.Text);
                cmd.Parameters.AddWithValue("@Konum", txtKonum.Text);
                cmd.Parameters.AddWithValue("@KullaniciID", kullaniciID);

                conn.Open();
                cmd.ExecuteNonQuery();
               
                lblMesaj.Text = "Bilgiler başarıyla güncellendi!! Anasayfaya yönlendiriliyorsunuz...";
                lblMesaj.ForeColor = System.Drawing.Color.Green;

                // JavaScript kodu ile 3 saniye sonra anasayfaya yönlendirme
                ClientScript.RegisterStartupScript(this.GetType(), "Redirect", "setTimeout(function(){ window.location.href='Anasayfa.aspx'; }, 3000);", true);
            }
        }

        protected void btnFotoGuncelle_Click(object sender, EventArgs e)
        {
            int kullaniciID = (int)Session["KullaniciID"];

            if (fileUploadProfilFoto.HasFile)
            {
                try
                {
                    string fileName = fileUploadProfilFoto.FileName;
                    string filePath = "~/Uploads/" + fileName;
                    string serverPath = Server.MapPath(filePath);

                    if (!System.IO.Directory.Exists(Server.MapPath("~/Uploads")))
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/Uploads"));
                    }

                    fileUploadProfilFoto.SaveAs(serverPath);

                    string connectionString = ConfigurationManager.ConnectionStrings["YazlabConnection"].ConnectionString;

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string query = "UPDATE Kullanicilar SET ProfilFoto = @ProfilFoto WHERE KullaniciID = @KullaniciID";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@ProfilFoto", filePath); // Tam dosya yolunu kaydediyoruz
                        cmd.Parameters.AddWithValue("@KullaniciID", kullaniciID);

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        lblMesaj.Text = "Profil fotoğrafı başarıyla güncellendi!";
                    }

                    imgProfilFoto.ImageUrl = filePath + "?v=" + DateTime.Now.Ticks; // Önbellek yenileme için sorgu dizesi ekleyin
                }
                catch (Exception ex)
                {
                    lblMesaj.Text = "Fotoğraf yüklenirken bir hata oluştu.";
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                lblMesaj.Text = "Lütfen bir profil fotoğrafı seçin.";
            }
        }



        private void LoadEtkinlikler(int kullaniciID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["YazlabConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT e.EtkinlikID, e.EtkinlikAdi, e.EtkinlikTarihi, e.EtkinlikSaati " +
                               "FROM Etkinlikler e " +
                               "INNER JOIN Katilimcilar k ON e.EtkinlikID = k.EtkinlikID " +
                               "WHERE k.KullaniciID = @KullaniciID";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@KullaniciID", kullaniciID);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                gvEtkinlikler.DataSource = reader;
                gvEtkinlikler.DataBind();
            }
        }
    }
}
