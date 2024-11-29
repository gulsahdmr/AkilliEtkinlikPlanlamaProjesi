<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="yazlab123.Register" %>

<!DOCTYPE html>
<html>
<head>
    <title>Kayıt Ol</title>
    <style>
        /* Genel Sayfa Ayarları */
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
        }

        /* Form İçeriği */
        .form-container {
            background-color: rgba(168, 168, 168, 0.9);
            padding: 30px;
            border-radius: 8px;
            box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1);
            width: 500px;
        }

        h2 {
            text-align: center;
            margin-bottom: 20px;
            font-size: 24px;
            color: #333;
        }

        /* Form Satırı Düzeni */
        .form-row {
            display: flex;
            flex-wrap: wrap;
            justify-content: space-between;
            gap: 20px;
            margin-bottom: 15px;
        }

        /* Form Elemanları */
        .form-group {
            flex: 1;
            min-width: 200px; /* Genişlik sınırlaması */
        }

        label {
            display: block;
            font-weight: bold;
            margin-bottom: 5px;
            color: #333;
        }

        input, select, .check-list {
            width: 100%;
            padding: 12px;
            font-size: 14px;
            border: 1px solid #ccc;
            border-radius: 4px;
            box-sizing: border-box;
        }

        /* Button Stil */
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

        /* İlgi Alanları Kutuları */
        .ilgi-alanlari-container {
            display: flex;
            flex-wrap: wrap;
            gap: 20px;
        }

        .ilgi-alanlari-container .ilgi-alanlari-item {
            width: 48%;
        }

        .check-list {
            display: flex;
            flex-direction: column;
            gap: 10px;
        }

        .check-list input {
            transform: scale(1.2);
            margin-right: 10px;
        }

        .check-list label {
            font-weight: normal;
            margin-bottom: 5px;
            font-size: 16px;
            color: #555;
            cursor: pointer;
            display: flex;
            align-items: center;
        }

        .check-list label:hover {
            color: #4CAF50;
            transition: color 0.3s ease;
        }

        /* Profil Fotoğrafı */
        .form-group-full {
            width: 100%;
        }

        /* Hata Mesajları */
        .error-message {
            color: red;
            text-align: center;
            margin-top: 15px;
        }

        /* Responsive Ayarlar */
        @media (max-width: 768px) {
            .form-container {
                width: 90%;
            }

            .form-row {
                flex-direction: column;
            }

            .ilgi-alanlari-container .ilgi-alanlari-item {
                width: 100%;
            }
        }

    </style>
</head>
<body>
    <div class="form-container">
        <h2>Kayıt Ol</h2>

        <form id="form1" runat="server">
            <!-- Kullanıcı Bilgileri -->
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

            <!-- İlgi Alanları -->
            <div class="form-row">
                <div class="form-group-full">
                    <label for="chkIlgiAlanlari">İlgi Alanları:</label>
                    <div class="ilgi-alanlari-container">
                        <div class="ilgi-alanlari-item">
                            <asp:CheckBoxList ID="chkIlgiAlanlari" runat="server" CssClass="check-list">
                                <asp:ListItem Text="Teknoloji" Value="Teknoloji" />
                                <asp:ListItem Text="Sağlık" Value="Saglik" />
                            </asp:CheckBoxList>
                        </div>
                        <div class="ilgi-alanlari-item">
                            <asp:CheckBoxList ID="chkIlgiAlanlari2" runat="server" CssClass="check-list">
                                <asp:ListItem Text="Spor" Value="Spor" />
                                <asp:ListItem Text="Sanat" Value="Sanat" />
                                <asp:ListItem Text="Edebiyat" Value="Edebiyat" />
                            </asp:CheckBoxList>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Profil Fotoğrafı -->
            <div class="form-row">
                <div class="form-group-full">
                    <label for="fuProfilFoto">Profil Fotoğrafı:</label>
                    <asp:FileUpload ID="fuProfilFoto" runat="server" />
                </div>
            </div>

            <!-- Kayıt Ol Butonu -->
            <div class="form-row">
                <div class="form-group-full">
                    <asp:Button ID="btnKayitOl" runat="server" Text="Kayıt Ol" OnClick="btnKayitOl_Click" CssClass="btn" />
                </div>
            </div>

            <!-- Hata Mesajı -->
            <div class="error-message">
                <asp:Label ID="lblMesaj" runat="server" ForeColor="Red" />
            </div>
        </form>
    </div>
</body>
</html>
