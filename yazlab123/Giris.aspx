<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Giris.aspx.cs" Inherits="yazlab123.Giris" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Anasayfa</title>
    <link href="Styles.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="center-container">
            <h1>Akıllı Etkinlik Planlama Platformu</h1>
            <p class="description">Etkinlik oluşturun, katılın ve sosyal etkileşim kurarak harika anlar yaşayın!</p>
            <asp:Button ID="btnGiris" CssClass="button button-login" runat="server" Text="Giriş Yap" PostBackUrl="~/Login.aspx" />
            <asp:Button ID="btnKayit" CssClass="button button-register" runat="server" Text="Kayıt Ol" PostBackUrl="~/Register.aspx" />
        </div>
    </form>
</body>
</html>
