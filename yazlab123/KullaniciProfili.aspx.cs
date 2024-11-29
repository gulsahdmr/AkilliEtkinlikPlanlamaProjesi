using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

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
                HesaplaVeKaydetPuan(kullaniciID);
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

                    // Eğer veritabanındaki ilgi alanları virgülle ayrılmışsa
                    string ilgiAlanlari = reader["ilgiAlanlari"].ToString();
                    string[] selectedItems = ilgiAlanlari.Split(',');

                    foreach (var item in selectedItems)
                    {
                        ListItem listItem = chkIlgiAlanlari.Items.FindByValue(item);
                        if (listItem != null)
                        {
                            listItem.Selected = true;
                        }
                    }

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

                // CheckBoxList'teki seçilen öğeleri al
                string selectedIlgiAlanlari = string.Join(",", chkIlgiAlanlari.Items.Cast<ListItem>()
                                                                .Where(item => item.Selected)
                                                                .Select(item => item.Value));

                cmd.Parameters.AddWithValue("@ilgiAlanlari", selectedIlgiAlanlari);
                cmd.Parameters.AddWithValue("@Konum", txtKonum.Text);
                cmd.Parameters.AddWithValue("@KullaniciID", kullaniciID);

                conn.Open();
                cmd.ExecuteNonQuery();

                lblMesaj.Text = "Bilgiler başarıyla güncellendi!! Anasayfaya yönlendiriliyorsunuz...";
                lblMesaj.ForeColor = System.Drawing.Color.Green;

                // Kullanıcı bilgileri güncellenince etkinlikleri de yeniden yükleyelim
                LoadEtkinlikler(kullaniciID);

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

        private void HesaplaVeKaydetPuan(int kullaniciID)
        {
            int katilimPuan = 0, olusturmaPuan = 0, bonusPuan = 0, toplamPuan = 0;
            bool ilkKatilimVar = false;
            string connectionString = ConfigurationManager.ConnectionStrings["YazlabConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Katılım Puanı Hesapla
                string katilimQuery = "SELECT COUNT(*) FROM Katilimcilar WHERE KullaniciID = @KullaniciID";
                using (SqlCommand cmd = new SqlCommand(katilimQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@KullaniciID", kullaniciID);
                    katilimPuan = (int)cmd.ExecuteScalar() * 10;
                }

                // Etkinlik Oluşturma Puanı Hesapla
                string olusturmaQuery = "SELECT COUNT(*) FROM Etkinlikler WHERE KullaniciID = @KullaniciID";
                using (SqlCommand cmd = new SqlCommand(olusturmaQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@KullaniciID", kullaniciID);
                    olusturmaPuan = (int)cmd.ExecuteScalar() * 15;
                }

                // İlk Katılım Bonusunu Kontrol Et
                string ilkKatilimQuery = "SELECT TOP 1 1 FROM Katilimcilar WHERE KullaniciID = @KullaniciID";
                using (SqlCommand cmd = new SqlCommand(ilkKatilimQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@KullaniciID", kullaniciID);
                    ilkKatilimVar = cmd.ExecuteScalar() != null;
                }
                bonusPuan = ilkKatilimVar ? 20 : 0;

                // Toplam Puan Hesapla
                toplamPuan = katilimPuan + olusturmaPuan + bonusPuan;

                // Puanı Puanlar Tablosuna Kaydet
                string insertPuanQuery = "INSERT INTO Puanlar (KullaniciID, Puanlar, KazanilanTarih) VALUES (@KullaniciID, @Puanlar, @KazanilanTarih)";
                using (SqlCommand cmd = new SqlCommand(insertPuanQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@KullaniciID", kullaniciID);
                    cmd.Parameters.AddWithValue("@Puanlar", toplamPuan);
                    cmd.Parameters.AddWithValue("@KazanilanTarih", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
            }

            // Kullanıcıya sonucu göster
            lblPuanMesaji.Text = $"Toplam puanınız: {toplamPuan} (Katılım: {katilimPuan}, Oluşturma: {olusturmaPuan}, Bonus: {bonusPuan})";
            lblPuanMesaji.ForeColor = System.Drawing.Color.Green;
        }


    }
}
