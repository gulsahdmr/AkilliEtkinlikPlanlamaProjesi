using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace yazlab123
{
    public partial class EtkinlikSayfasi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadEventDetails();
            }
        }

        private void LoadEventDetails()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["YazlabConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT EtkinlikID, EtkinlikAdi, EtkinlikTarihi, EtkinlikKonumu FROM Etkinlikler ORDER BY EtkinlikTarihi DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                eventRepeater.DataSource = dt;
                eventRepeater.DataBind();
            }
        }

        // Detayları Göster Butonu
        protected void ShowDetailsButton_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            string etkinlikID = e.CommandArgument.ToString();
            Response.Redirect("EtkinlikDetay.aspx?EtkinlikID=" + etkinlikID);
        }

        // Düzenleme Butonu
        protected void EditButton_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            int etkinlikID = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("EtkinlikEkle.aspx?EtkinlikID=" + etkinlikID);
        }

       
    }
}
