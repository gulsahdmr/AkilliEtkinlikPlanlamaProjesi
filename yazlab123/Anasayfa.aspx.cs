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
                    string query = @"
                SELECT EtkinlikID, EtkinlikAdi, EtkinlikKonumu, EtkinlikSaati 
                FROM Etkinlikler 
                WHERE KullaniciID = (
                    SELECT TOP 1 KullaniciID 
                    FROM Kullanicilar 
                    WHERE KullaniciAdi = @Username
                )";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);

                        SqlDataReader reader = cmd.ExecuteReader();

                        // Eğer kullanıcıya ait etkinlik yoksa bir mesaj göster
                        if (!reader.HasRows)
                        {
                            Response.Write("Bu kullanıcıya ait etkinlik bulunmamaktadır.");
                        }

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



    }
}