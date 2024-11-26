using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace yazlab123
{
    public partial class Sohbet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadEtkinlikler();
            }

            if (Session["SelectedEtkinlikID"] != null)
            {
                LoadSelectedEventInfo((int)Session["SelectedEtkinlikID"]);
                LoadMessages((int)Session["SelectedEtkinlikID"]);
            }
            
        }

        private void LoadEtkinlikler()
        {
            using (SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["YazlabConnection"].ConnectionString))
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT EtkinlikID, EtkinlikAdi FROM Etkinlikler", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                rpEtkinlikler.DataSource = dt;
                rpEtkinlikler.DataBind();
            }
        }

        protected void selectEvent(int etkinlikID)
        {
            // Etkinlik ID'sini Session'da sakla
            Session["SelectedEtkinlikID"] = etkinlikID;
            LoadSelectedEventInfo(etkinlikID);
            LoadMessages(etkinlikID);
        }

        private void LoadSelectedEventInfo(int etkinlikID)
        {
            using (SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["YazlabConnection"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(@"
                    SELECT EtkinlikAdi, EtkinlikKonumu,EtkinlikTarihi
                    FROM Etkinlikler 
                    WHERE EtkinlikID = @EtkinlikID", con);
                cmd.Parameters.AddWithValue("@EtkinlikID", etkinlikID);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    lblEtkinlikAdi.Text = reader["EtkinlikAdi"].ToString();
                    lblEtkinlikKonumu.Text = reader["EtkinlikKonumu"].ToString();
                    lblEtkinlikSaati.Text = reader["EtkinlikTarihi"].ToString();
                }
                reader.Close();
            }
        }

        private void LoadMessages(int etkinlikID)
        {
            using (SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["YazlabConnection"].ConnectionString))
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(@"
                    SELECT M.MesajMetni, M.GonderimZamani, K.KullaniciAdi AS GondericiAdi 
                    FROM Mesajlar M
                    JOIN Kullanicilar K ON M.GondericiID = K.KullaniciID
                    WHERE M.EtkinlikID = @EtkinlikID
                    ORDER BY M.GonderimZamani", con);
                da.SelectCommand.Parameters.AddWithValue("@EtkinlikID", etkinlikID);
                DataTable dt = new DataTable();
                da.Fill(dt);
                rpMesajlar.DataSource = dt;
                rpMesajlar.DataBind();
            }
        }

        protected void btnGonder_Click(object sender, EventArgs e)
        {
            // Gerekli kontrolleri yap
            if (Session["KullaniciID"] == null)
            {
                // Kullanıcı oturumu yoksa hata ver
                Response.Write("<script>alert('Mesaj göndermek için oturum açmalısınız.');</script>");
                return;
            }
            if (Session["SelectedEtkinlikID"] == null)
            {
                // Etkinlik seçilmemişse hata ver
                Response.Write("<script>alert('Bir etkinlik seçmelisiniz.');</script>");
                return;
            }

            if (!string.IsNullOrEmpty(txtMesaj.Text))
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["YazlabConnection"].ConnectionString))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("INSERT INTO Mesajlar (GondericiID, EtkinlikID, MesajMetni, GonderimZamani) VALUES (@GondericiID, @EtkinlikID, @MesajMetni, @GonderimZamani)", con);
                        cmd.Parameters.AddWithValue("@GondericiID", Session["KullaniciID"]);
                        cmd.Parameters.AddWithValue("@EtkinlikID", Session["EtkinlikID"]);
                        cmd.Parameters.AddWithValue("@MesajMetni", txtMesaj.Text);
                        cmd.Parameters.AddWithValue("@GonderimZamani", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }

                    // Mesaj gönderildikten sonra metin kutusunu temizle ve mesajları yeniden yükle
                    txtMesaj.Text = "";
                    LoadMessages((int)Session["SelectedEtkinlikID"]);
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Mesaj gönderilemedi: " + ex.Message + "');</script>");
                }
            }
        }
    }
}
