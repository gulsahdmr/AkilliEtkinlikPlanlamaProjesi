<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="yazlab123.Register" %>

<!DOCTYPE html>
<html>
<head>
    <title>Kayıt Ol</title>
    <style>
        /* Sayfa düzenini ortalamak için */
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh; /* Sayfanın tamamını kapsayacak şekilde */
            margin: 0;
        }

        /* Kayıt formu */
        .form-container {
            background-color: rgba(168, 168, 168, 0.85); /* Hafif şeffaf gri arka plan */
            padding: 30px;
            border-radius: 8px;
            box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1);
            width: 500px; /* Form genişliği */
        }

        h2 {
            text-align: center;
            margin-bottom: 20px;
        }

        /* Flexbox kullanarak form elemanlarını 2 sütuna yerleştirmek */
        .form-row {
            display: flex;
            justify-content: space-between;
            gap: 20px;
            margin-bottom: 15px;
        }

        /* Form elemanlarının genişliği */
        .form-group {
            flex: 1;
        }

        /* Form input ve select elemanlarının stilleri */
        input, select {
            width: 100%;
            padding: 10px;
            font-size: 14px;
            border: 1px solid #ccc;
            border-radius: 4px;
        }

        label {
            display: block;
            font-weight: bold;
            margin-bottom: 5px;
        }

        .form-group-full {
            width: 100%;
        }

        .btn {
            background-color: #4CAF50;
            color: white;
            padding: 12px 20px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            width: 100%;
            font-size: 16px;
        }

        .btn:hover {
            background-color: #45a049;
        }

        .error-message {
            color: red;
            text-align: center;
        }
    </style>
</head>
<body>
    <div class="form-container">
        <h2>Kayıt Ol</h2>
    <link rel="stylesheet" type="text/css" href="styles.css">

        <form id="form1" runat="server">
            <!-- Kullanıcı adı ve şifre gibi alanlar için 2'li düzen -->
            <div class="form-row">
                <div class="form-group">
                    <label for="txtKullaniciAdi">Kullanıcı Adı:</label>
                    <asp:TextBox ID="txtKullaniciAdi" runat="server" />
                </div>
                <div class="form-group">
                    <label for="txtSifre">Şifre:</label>
                    <asp:TextBox ID="txtSifre" runat="server" TextMode="Password" />
                </div>
            </div>
            <!-- Diğer form alanları için 2'li düzen -->
            <div class="form-row">
                <div class="form-group">
                    <label for="txtEposta">E-posta:</label>
                    <asp:TextBox ID="txtEposta" runat="server" />
                </div>
                <div class="form-group">
                    <label for="txtAd">Ad:</label>
                    <asp:TextBox ID="txtAd" runat="server" />
                </div>
            </div>
            <div class="form-row">
                <div class="form-group">
                    <label for="txtSoyad">Soyad:</label>
                    <asp:TextBox ID="txtSoyad" runat="server" />
                </div>
                <div class="form-group">
                    <label for="txtDogumTarihi">Doğum Tarihi:</label>
                    <asp:TextBox ID="txtDogumTarihi" runat="server" />
                </div>
            </div>
            <div class="form-row">
                <div class="form-group">
                    <label for="ddlCinsiyet">Cinsiyet:</label>
                    <asp:DropDownList ID="ddlCinsiyet" runat="server">
                        <asp:ListItem Text="Erkek" Value="Erkek" />
                        <asp:ListItem Text="Kadın" Value="Kadın" />
                    </asp:DropDownList>
                </div>
                <div class="form-group">
                    <label for="txtTelefon">Telefon:</label>
                    <asp:TextBox ID="txtTelefon" runat="server" />
                </div>
            </div>
            <div class="form-row">
                <div class="form-group-full">
                    <label for="txtIlgiAlanlari">İlgi Alanları:</label>
                    <asp:TextBox ID="txtIlgiAlanlari" runat="server" />
                </div>
            </div>
            <div class="form-row">
                <div class="form-group-full">
                    <label for="fuProfilFoto">Profil Fotoğrafı:</label>
                    <asp:FileUpload ID="fuProfilFoto" runat="server" />
                </div>
            </div>
            <div class="form-row">
                <div class="form-group-full">
                    <asp:Button ID="btnKayitOl" runat="server" Text="Kayıt Ol" OnClick="btnKayitOl_Click" CssClass="btn" />
                </div>
            </div>
            <div class="error-message">
                <asp:Label ID="lblMesaj" runat="server" ForeColor="Red" />
            </div>
        </form>
    </div>
</body>
</html>
