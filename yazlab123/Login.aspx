<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="yazlab123.Login" %>


<!DOCTYPE html>
<html lang="tr">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Giriş Yap</title>
    <link rel="stylesheet" type="text/css" href="styles.css">
</head>
<body>
    <div class="center-container">
        <h1>Giriş Yap</h1>
        <p class="description">Hesabınıza giriş yaparak hizmetlerimizden faydalanın.</p>

      <form id="loginForm" runat="server">
    <div class="input-group">
        <label for="txtKullaniciAdi">Kullanıcı Adı</label>
        <asp:TextBox ID="txtKullaniciAdi" runat="server" placeholder="Kullanıcı adınızı girin" CssClass="input"></asp:TextBox>
    </div>
    <div class="input-group">
        <label for="txtSifre">Şifre</label>
        <asp:TextBox ID="txtSifre" TextMode="Password" runat="server" placeholder="Şifrenizi girin" CssClass="input"></asp:TextBox>
    </div>
    <asp:Button ID="btnGiris" runat="server" CssClass="button button-login" Text="Giriş Yap" OnClick="btnGiris_Click" />
    <button type="button" class="button button-register" onclick="window.location.href='register.aspx'">Kayıt Ol</button>

    <!-- Mesaj etiketi -->
    <asp:Label ID="lblMesaj" runat="server" CssClass="message-label"></asp:Label>
</form>

    </div>
</body>
</html>
