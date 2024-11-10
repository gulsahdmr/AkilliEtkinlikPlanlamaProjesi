using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace yazlab123
{
    public partial class Anasayfa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                LoadUserEvents();
            }
        }

        private void LoadUserEvents()
        {
            string username = Session["Username"].ToString();
            string connectionString = ConfigurationManager.ConnectionStrings["YazlabConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT EtkinlikID, EtkinlikAdi, EtkinlikKonumu, EtkinlikSaati " +
                                   "FROM Etkinlikler " +
                                   "WHERE KullaniciID = (SELECT KullaniciID FROM Kullanicilar WHERE KullaniciAdi = @Username)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        SqlDataReader reader = cmd.ExecuteReader();

                        rpKullaniciEtkinlikler.DataSource = reader;
                        rpKullaniciEtkinlikler.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("Hata: " + ex.Message);
                }
            }
        }

        protected void btnSil_Command(object sender, CommandEventArgs e)
        {
            int etkinlikID = Convert.ToInt32(e.CommandArgument);
            string connectionString = ConfigurationManager.ConnectionStrings["YazlabConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // 1. Katılımcıları sil
                    string deleteParticipantsQuery = "DELETE FROM Katilimcilar WHERE EtkinlikID = @EtkinlikID";
                    using (SqlCommand deleteParticipantsCmd = new SqlCommand(deleteParticipantsQuery, conn))
                    {
                        deleteParticipantsCmd.Parameters.AddWithValue("@EtkinlikID", etkinlikID);
                        deleteParticipantsCmd.ExecuteNonQuery();
                    }

                    // 2. Etkinliği sil
                    string deleteEventQuery = "DELETE FROM Etkinlikler WHERE EtkinlikID = @EtkinlikID";
                    using (SqlCommand deleteEventCmd = new SqlCommand(deleteEventQuery, conn))
                    {
                        deleteEventCmd.Parameters.AddWithValue("@EtkinlikID", etkinlikID);
                        deleteEventCmd.ExecuteNonQuery();
                    }

                    // 3. Etkinlik listesini güncellemek için tekrar yükleyin
                    LoadUserEvents();
                }
                catch (Exception ex)
                {
                    Response.Write("Hata: " + ex.Message);
                }
            }
        }
    }
}
