using System;
using System.Configuration;
using System.Data.SqlClient;

namespace yazlab123
{
    public partial class EtkinlikGuncelle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                // Etkinlik ID parametresi alınarak etkinlik bilgileri yüklenecek
                if (Request.QueryString["EtkinlikID"] != null)
                {
                    int etkinlikID = Convert.ToInt32(Request.QueryString["EtkinlikID"]);
                    LoadEventDetails(etkinlikID);
                }
                else
                {
                    Response.Redirect("Anasayfa.aspx");
                }
            }
        }

        private void LoadEventDetails(int etkinlikID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["YazlabConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT EtkinlikAdi, EtkinlikAciklamasi, EtkinlikKonumu, EtkinlikTarihi, EtkinlikSaati, EtkinlikSuresi, EtkinlikKategorisi FROM Etkinlikler WHERE EtkinlikID = @EtkinlikID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@EtkinlikID", etkinlikID);
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            txtEtkinlikAdi.Text = reader["EtkinlikAdi"].ToString();
                            txtEtkinlikAciklamasi.Text = reader["EtkinlikAciklamasi"].ToString();
                            txtEtkinlikKonumu.Text = reader["EtkinlikKonumu"].ToString();
                            txtEtkinlikTarihi.Text = reader["EtkinlikTarihi"].ToString();
                            txtEtkinlikSaati.Text = reader["EtkinlikSaati"].ToString();
                            txtEtkinlikSuresi.Text = reader["EtkinlikSuresi"].ToString();
                            txtEtkinlikKategorisi.Text = reader["EtkinlikKategorisi"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("Hata: " + ex.Message);
                }
            }
        }

        protected void btnGuncelle_Click(object sender, EventArgs e)
        {
            // Güncellenmiş bilgileri al
            string etkinlikAdi = txtEtkinlikAdi.Text;
            string etkinlikAciklamasi = txtEtkinlikAciklamasi.Text;
            string etkinlikKonumu = txtEtkinlikKonumu.Text;
            string etkinlikTarihi = txtEtkinlikTarihi.Text;
            string etkinlikSaati = txtEtkinlikSaati.Text;
            string etkinlikSuresi = txtEtkinlikSuresi.Text;
            string etkinlikKategorisi = txtEtkinlikKategorisi.Text;

            // Tarih ve saat bilgilerini doğru formatta alalım
            DateTime etkinlikTarihiDate;
            DateTime etkinlikSaatiDate;

            // Tarih ve saat formatlarını kontrol et
            if (DateTime.TryParse(etkinlikTarihi, out etkinlikTarihiDate) && DateTime.TryParse(etkinlikSaati, out etkinlikSaatiDate))
            {
                // Tarih ve saati doğru formatta alınca güncelleme işlemi yapılacak
                int etkinlikID = Convert.ToInt32(Request.QueryString["EtkinlikID"]);
                string connectionString = ConfigurationManager.ConnectionStrings["YazlabConnection"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = "UPDATE Etkinlikler SET EtkinlikAdi = @EtkinlikAdi, EtkinlikAciklamasi = @EtkinlikAciklamasi, EtkinlikKonumu = @EtkinlikKonumu, EtkinlikTarihi = @EtkinlikTarihi, EtkinlikSaati = @EtkinlikSaati, EtkinlikSuresi = @EtkinlikSuresi, EtkinlikKategorisi = @EtkinlikKategorisi WHERE EtkinlikID = @EtkinlikID";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@EtkinlikAdi", etkinlikAdi);
                            cmd.Parameters.AddWithValue("@EtkinlikAciklamasi", etkinlikAciklamasi);
                            cmd.Parameters.AddWithValue("@EtkinlikKonumu", etkinlikKonumu);
                            cmd.Parameters.AddWithValue("@EtkinlikTarihi", etkinlikTarihiDate);
                            cmd.Parameters.AddWithValue("@EtkinlikSaati", etkinlikSaatiDate);
                            cmd.Parameters.AddWithValue("@EtkinlikSuresi", etkinlikSuresi);
                            cmd.Parameters.AddWithValue("@EtkinlikKategorisi", etkinlikKategorisi);
                            cmd.Parameters.AddWithValue("@EtkinlikID", etkinlikID);
                            cmd.ExecuteNonQuery();
                        }

                        // Güncelleme başarılı mesajı
                        lblMesaj.Text = "Güncelleme Başarılı! Anasayfaya yönlendiriliyorsunuz..";
                        lblMesaj.ForeColor = System.Drawing.Color.Green;

                        // JavaScript ile 3 saniye sonra yönlendirme yapılacak
                        ClientScript.RegisterStartupScript(this.GetType(), "Redirect", "setTimeout(function(){ window.location.href='Anasayfa.aspx'; }, 3000);", true);
                    }
                    catch (Exception ex)
                    {
                        Response.Write("Hata: " + ex.Message);
                    }
                }
            }
            else
            {
                lblMesaj.Text = "Geçersiz tarih veya saat formatı!";
                lblMesaj.ForeColor = System.Drawing.Color.Red;
            }
        }

    }
}
