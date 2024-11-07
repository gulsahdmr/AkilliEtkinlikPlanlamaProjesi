using System;
using System.Configuration;
using System.Data.SqlClient;

namespace yazlab123
{
    public partial class EtkinlikSayfasi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Etkinlik ID'sini URL'den alıyoruz
                string etkinlikId = Request.QueryString["id"];
                if (etkinlikId != null)
                {
                    LoadEventDetails(etkinlikId);
                }
                else
                {
                    Response.Redirect("Anasayfa.aspx"); // Etkinlik ID'si yoksa anasayfaya yönlendir
                }
            }
        }

        private void LoadEventDetails(string etkinlikId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["YazlabConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT * FROM Etkinlikler WHERE EtkinlikID = @EtkinlikId"; // Etkinlik detaylarını alıyoruz
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@EtkinlikId", etkinlikId);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string eventName = reader["EtkinlikAdi"].ToString();
                    string eventDescription = reader["EtkinlikAciklamasi"].ToString();
                    string eventDate = reader["EtkinlikTarihi"].ToString();
                    string eventTime = reader["EtkinlikSaati"].ToString();
                    string eventduration = reader["EtkinlikSuresi"].ToString();
                    string eventlocation = reader["EtkinlikKonumu"].ToString() ;
                    string eventcategory = reader["EtkinlikKategorisi"].ToString();


                    // Etkinlik detaylarını sayfada gösteriyoruz
                    eventNameLabel.Text = eventName;
                    eventDescriptionLabel.Text = eventDescription;
                    eventDateLabel.Text = eventDate;
                    eventTimeLabel.Text = eventTime;
                    eventdurationlabel.Text = eventduration;
                    eventlocationlabel.Text = eventlocation;
                    eventcategorylabel.Text = eventcategory;


                }
                else
                {
                    // Etkinlik bulunamazsa kullanıcıyı anasayfaya yönlendirebiliriz
                    Response.Redirect("Anasayfa.aspx");
                }

                reader.Close();
            }
        }
    }
}
