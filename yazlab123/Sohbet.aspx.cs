using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Web.Configuration;
using System.Web.UI.WebControls;

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

            if (Session["EtkinlikID"] != null)
            {
                int etkinlikID = (int)Session["EtkinlikID"];
                LoadSelectedEventInfo(etkinlikID);
                LoadMessages(etkinlikID);
            }
        }

        private void LoadEtkinlikler()
        {
            try
            {
                if (Session["KullaniciID"] == null)
                {
                    Response.Write("<script>alert('Kullanıcı girişi yapılmamış.');</script>");
                    return;
                }

                int kullaniciID = (int)Session["KullaniciID"];

                using (SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["YazlabConnection"].ConnectionString))
                {
                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter(@"
                        SELECT E.EtkinlikID, E.EtkinlikAdi 
                        FROM Etkinlikler E
                        JOIN Katilimcilar U ON E.EtkinlikID = U.EtkinlikID
                        WHERE U.KullaniciID = @KullaniciID", con);
                    da.SelectCommand.Parameters.AddWithValue("@KullaniciID", kullaniciID);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        Response.Write("<script>alert('Kullanıcıya ait etkinlik bulunamadı.');</script>");
                    }

                    rpEtkinlikler.DataSource = dt;
                    rpEtkinlikler.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Etkinlikler yüklenirken hata oluştu: " + ex.Message + "');</script>");
            }
        }

        protected void rpEtkinlikler_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                int etkinlikID = int.Parse(e.CommandArgument.ToString());
                selectEvent(etkinlikID);
            }
        }

        private void selectEvent(int etkinlikID)
        {
            Session["EtkinlikID"] = etkinlikID;
            LoadSelectedEventInfo(etkinlikID);
            LoadMessages(etkinlikID);
        }

        private void LoadSelectedEventInfo(int etkinlikID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["YazlabConnection"].ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(@"
                        SELECT EtkinlikAdi, EtkinlikKonumu, EtkinlikTarihi
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
            catch (Exception ex)
            {
                Response.Write("<script>alert('Etkinlik bilgileri yüklenirken hata oluştu: " + ex.Message + "');</script>");
            }
        }

        private void LoadMessages(int etkinlikID)
        {
            try
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
            catch (Exception ex)
            {
                Response.Write("<script>alert('Mesajlar yüklenirken hata oluştu: " + ex.Message + "');</script>");
            }
        }
        protected void btnAnasayfa_Click(object sender, EventArgs e)
        {
            // Kullanıcıyı Anasayfa.aspx sayfasına yönlendir
            Response.Redirect("Anasayfa.aspx");
        }

        protected void btnGonder_Click(object sender, EventArgs e)
        {
            if (Session["KullaniciID"] == null)
            {
                Response.Write("<script>alert('Mesaj göndermek için oturum açmalısınız.');</script>");
                return;
            }
            if (Session["EtkinlikID"] == null)
            {
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
                        SqlCommand cmd = new SqlCommand(@"
    INSERT INTO Mesajlar (GondericiID, EtkinlikID, MesajMetni, GonderimZamani) 
    VALUES (@GondericiID, @EtkinlikID, @MesajMetni, @GonderimZamani)", con);

                        cmd.Parameters.AddWithValue("@GondericiID", Session["KullaniciID"]);
                        cmd.Parameters.AddWithValue("@EtkinlikID", Session["EtkinlikID"]);
                        cmd.Parameters.AddWithValue("@MesajMetni", txtMesaj.Text);
                        cmd.Parameters.AddWithValue("@GonderimZamani", DateTime.Now);

                        cmd.ExecuteNonQuery();

                    }

                    txtMesaj.Text = "";
                    LoadMessages((int)Session["EtkinlikID"]);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("<script>alert('Mesaj gönderilemedi: " + ex.Message + "');</script>");
                }
            }
        }
    }
}
