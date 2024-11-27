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

            // Kullanıcı ID'sini ve admin olup olmadığını kontrol et
            int userID = Convert.ToInt32(Session["KullaniciID"]);  // Kullanıcı ID'sini alın (örneğin session'dan)
            bool isAdmin = userID == 1;  // Admin ise UserID 1 olsun, bunu kendi admin kontrol mekanizmanızla değiştirin

            string query = isAdmin
                ? "SELECT EtkinlikID, EtkinlikAdi, EtkinlikTarihi, EtkinlikKonumu, Onay FROM Etkinlikler ORDER BY EtkinlikTarihi DESC"
                : "SELECT EtkinlikID, EtkinlikAdi, EtkinlikTarihi, EtkinlikKonumu, Onay FROM Etkinlikler WHERE Onay = 1 ORDER BY EtkinlikTarihi DESC";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Etkinlikleri Repeater'a bağla
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
