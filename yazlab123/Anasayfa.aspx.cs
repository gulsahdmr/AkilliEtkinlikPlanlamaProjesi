using System;
using System.Configuration;
using System.Data.SqlClient;

namespace yazlab123
{
    public partial class Anasayfa: System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
            {
                Response.Redirect("Login.aspx"); // Oturum açmamış kullanıcıyı giriş sayfasına yönlendir
            }

            // Etkinlikleri veritabanından yükle
            LoadEvents();
        }

        private void LoadEvents()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["YazlabConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT * FROM Etkinlikler"; // Etkinlikleri veritabanından alıyoruz
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string eventId = reader["EtkinlikID"].ToString();
                    string eventName = reader["EtkinlikAdi"].ToString();
                    string eventDescription = reader["EtkinlikAciklamasi"].ToString();

                    // Etkinlikleri sayfada dinamik olarak ekleyelim
                    Response.Write($@"
                <div class='event-card'>
                    <h4>{eventName}</h4>
                    <p>{eventDescription}</p>
                    <a href='EtkinlikSayfasi.aspx?id={eventId}'>
                        <button>Etkinliğe Katıl</button>
                    </a>
                </div>
            ");
                }

                reader.Close();
            }
        }
    }
}
