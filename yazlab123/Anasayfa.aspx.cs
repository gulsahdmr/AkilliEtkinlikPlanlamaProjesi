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
                LoadUserEvents(); // Kullanıcının oluşturduğu etkinlikleri yükle
                LoadRecommendedEvents(); // Önerilen etkinlikleri yükle
            }

            if (Session["KullaniciID"] != null && Session["KullaniciID"].ToString() == "1")
            {
                phAdmin.Visible = true; // Admin kullanıcıyı göster
            }
            else
            {
                phAdmin.Visible = false; // Admin değilse, Admin kısmını gizle
            }
        }

        // Kullanıcının oluşturduğu etkinlikleri veritabanından alıp Repeater'a bind et
        private void LoadUserEvents()
        {
            // Kullanıcı ID'sini Session'dan al
            if (Session["KullaniciID"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            int kullaniciID = Convert.ToInt32(Session["KullaniciID"]);
            string connectionString = ConfigurationManager.ConnectionStrings["YazlabConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                SELECT EtkinlikID, EtkinlikAdi, EtkinlikKonumu, EtkinlikSaati 
                FROM Etkinlikler 
                WHERE KullaniciID = @KullaniciID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Kullanıcı ID'sini sorguya parametre olarak ekle
                        cmd.Parameters.AddWithValue("@KullaniciID", kullaniciID);

                        // Veritabanından gelen sonuçları oku ve Repeater'a bağla
                        SqlDataReader reader = cmd.ExecuteReader();

                        rpKullaniciEtkinlikler.DataSource = reader;
                        rpKullaniciEtkinlikler.DataBind();
                        rptEtkinlikler.DataBind();

                    }
                }
                catch (Exception ex)
                {
                    // Hata durumunda kullanıcıya hata mesajını göster
                    Response.Write("Hata: " + ex.Message);
                }
            }
        }


        private void LoadRecommendedEvents()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["YazlabConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
 SELECT DISTINCT 
    e.EtkinlikID, 
    e.EtkinlikAdi, 
    e.EtkinlikAciklamasi, 
    e.EtkinlikTarihi, 
    e.EtkinlikSaati, 
    e.EtkinlikKonumu, 
    e.EtkinlikKategorisi, 
    CASE 
        WHEN e.EtkinlikKategorisi IN (
            SELECT value 
            FROM STRING_SPLIT((SELECT ilgiAlanlari FROM Kullanicilar WHERE KullaniciID = @KullaniciID), ',')
        ) THEN 1  -- İlgi alanlarına uygun etkinlikler
        WHEN e.EtkinlikID IN (
            SELECT DISTINCT EtkinlikID
            FROM Katilimcilar 
            WHERE KullaniciID = @KullaniciID
        ) THEN 2  -- Katılınan etkinliklerden türetilen öneriler
        WHEN e.EtkinlikKonumu = (
            SELECT Konum 
            FROM Kullanicilar 
            WHERE KullaniciID = @KullaniciID
        ) THEN 3  -- Kullanıcının konumuna yakın etkinlikler
        ELSE 4      -- Diğer etkinlikler
    END AS Priority
FROM Etkinlikler e
WHERE (
    e.EtkinlikKategorisi IN (
        SELECT value 
        FROM STRING_SPLIT((SELECT ilgiAlanlari FROM Kullanicilar WHERE KullaniciID = @KullaniciID), ',')
    ) 
    OR e.EtkinlikID IN (
        SELECT DISTINCT EtkinlikID 
        FROM Katilimcilar kc 
        WHERE kc.KullaniciID = @KullaniciID
    ) 
    OR e.EtkinlikKonumu = (
        SELECT Konum 
        FROM Kullanicilar 
        WHERE KullaniciID = @KullaniciID
    )
) 
AND e.EtkinlikID NOT IN (
    SELECT EtkinlikID 
    FROM Katilimcilar 
    WHERE KullaniciID = @KullaniciID
) 
AND e.Onay = 1
ORDER BY Priority ASC, e.EtkinlikTarihi ASC;  -- Öncelik sırasına göre etkinlikleri sıralıyoruz



";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@KullaniciID", Session["KullaniciID"]);

                        SqlDataReader reader = cmd.ExecuteReader();
                        rptEtkinlikler.DataSource = reader; // Repeater kontrolünün adı
                        rptEtkinlikler.DataBind();
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
