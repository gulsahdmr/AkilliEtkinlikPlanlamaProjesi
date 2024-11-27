using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace yazlab123
{
    public partial class AdminProfili : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                KullaniciListesiYukle();
            }
        }

        private void KullaniciListesiYukle()
        {
            string connStr = WebConfigurationManager.ConnectionStrings["YazlabConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT KullaniciID, Ad, Soyad, KullaniciAdi FROM Kullanicilar";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();

                conn.Open();
                da.Fill(dt);
                conn.Close();

                gvKullanicilar.DataSource = dt;
                gvKullanicilar.DataBind();
            }
        }

        protected void gvKullanicilar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvKullanicilar.Rows[rowIndex];
            string kullaniciID = row.Cells[0].Text;

            if (e.CommandName == "Sil")
            {
                KullaniciSil(kullaniciID);
            }
            else if (e.CommandName == "Detay")
            {
                Response.Redirect("AdminKullaniciProfiliDetay.aspx?KullaniciID=" + kullaniciID);
            }
        }

        private void KullaniciSil(string kullaniciID)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["YazlabConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();

                    // İlk olarak Puanlar tablosundan ilişkili verileri sil
                    string deletePuanlarQuery = "DELETE FROM Puanlar WHERE KullaniciID = @KullaniciID";
                    SqlCommand deletePuanlarCmd = new SqlCommand(deletePuanlarQuery, conn);
                    deletePuanlarCmd.Parameters.AddWithValue("@KullaniciID", kullaniciID);
                    deletePuanlarCmd.ExecuteNonQuery();

                    // Ardından Kullanicilar tablosundan kullanıcıyı sil
                    string deleteKullaniciQuery = "DELETE FROM Kullanicilar WHERE KullaniciID = @KullaniciID";
                    SqlCommand deleteKullaniciCmd = new SqlCommand(deleteKullaniciQuery, conn);
                    deleteKullaniciCmd.Parameters.AddWithValue("@KullaniciID", kullaniciID);
                    deleteKullaniciCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Response.Write("Hata: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }

            // GridView'i güncelle
            KullaniciListesiYukle();
        }

    }
}
