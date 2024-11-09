using System;
using System.Configuration;
using System.Data.SqlClient;

namespace yazlab123
{
    public partial class Anasayfa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
            {
                Response.Redirect("Login.aspx"); // Oturum açmamış kullanıcıyı giriş sayfasına yönlendir
            }


        }


    }
}