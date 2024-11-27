<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminKullaniciProfiliDetay.aspx.cs" Inherits="yazlab123.AdminKullaniciProfiliDetay" %>

<!DOCTYPE html>
<html lang="tr">
<head runat="server">
    <meta charset="utf-8" />
    <title>Kullanıcı Profili</title>
    <style>
        /* Genel Stil */
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
            background-color: #f4f4f4;
            display: flex;
            justify-content: center;
            align-items: center;
            min-height: 100vh;
        }

        form {
            width: 100%;
            max-width: 800px;
            background-color: #fff;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
        }

        h2 {
            color: #333;
            text-align: center;
        }

        .puan-container {
            text-align: center;
            margin-bottom: 20px;
            font-size: 18px;
        }

        .puan-container span {
            font-weight: bold;
            color: #4CAF50;
        }

        /* Profil Fotoğrafı ve Fotoğraf Güncelleme */
        .profil-foto-container {
            display: flex;
            flex-direction: column;
            align-items: center;
            margin-bottom: 20px;
        }

        .profil-foto-container img {
            width: 150px;
            height: 150px;
            border-radius: 50%;
            object-fit: cover;
            border: 3px solid #4CAF50;
            margin-bottom: 10px;
        }

        .profil-foto-container input[type="file"] {
            display: none;
        }

        .profil-foto-container label {
            background-color: #4CAF50;
            color: white;
            padding: 10px 15px;
            border-radius: 4px;
            cursor: pointer;
        }

        .btn-guncelle {
            background-color: #4CAF50;
            color: white;
            padding: 10px 20px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            margin-top: 10px;
            font-size: 16px;
        }

        .btn-guncelle:hover {
            background-color: #45a049;
        }

        /* Profil Bilgileri */
        .profil-info {
            display: grid;
            grid-template-columns: 1fr 1fr;
            gap: 15px;
            margin-bottom: 20px;
        }

        .profil-info label {
            font-weight: bold;
            color: #555;
            display: block;
            margin-bottom: 5px;
        }

        .profil-info input {
            width: 100%;
            padding: 8px;
            font-size: 16px;
            border-radius: 4px;
            border: 1px solid #ddd;
        }

        /* Katıldıkları Etkinlikler */
        .etkinlikler-container {
            margin-top: 30px;
        }

        .etkinlikler-container h3 {
            color: #333;
            text-align: center;
        }

        .table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 10px;
        }

        .table th, .table td {
            padding: 12px;
            text-align: left;
            border: 1px solid #ddd;
        }

        .table th {
            background-color: #4CAF50;
            color: white;
        }

        .table td {
            background-color: #f9f9f9;
        }

        .table tr:hover {
            background-color: #f1f1f1;
        }

        /* Hata mesajı */
        .error-message {
            color: red;
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Kullanıcı Profili</h2>

       <!-- Kullanıcı Puanları -->
<div class="puan-container">
    <h3>Puanınız</h3>
    <asp:Label ID="lblPuanMesaji" runat="server" Text="Puanınız hesaplanıyor..." 
               style="display: block; font-size: 20px; color: #4CAF50; font-weight: bold; margin-top: 10px;" />
</div>


        <!-- Profil Fotoğrafı ve Fotoğraf Güncelleme -->
        <div class="profil-foto-container">
            <asp:Image ID="imgProfilFoto" runat="server" Width="150" Height="150" />
            <label for="fileUploadProfilFoto">Fotoğraf Seç</label>
            <asp:FileUpload ID="fileUploadProfilFoto" runat="server" />
            <asp:Button ID="btnFotoGuncelle" runat="server" Text="Fotoğrafı Güncelle" OnClick="btnFotoGuncelle_Click" CssClass="btn-guncelle" />
        </div>

        <!-- Profil Bilgileri -->
        <div class="profil-info">
            <div>
                <label for="txtAd">Ad:</label>
                <asp:TextBox ID="txtAd" runat="server" CssClass="profil-input" />
            </div>
            <div>
                <label for="txtSoyad">Soyad:</label>
                <asp:TextBox ID="txtSoyad" runat="server" CssClass="profil-input" />
            </div>

            <div>
                <label for="txtEposta">E-posta:</label>
                <asp:TextBox ID="txtEposta" runat="server" CssClass="profil-input" />
            </div>

            <div>
                <label for="txtTelefonNo">Telefon No:</label>
                <asp:TextBox ID="txtTelefonNo" runat="server" CssClass="profil-input" />
            </div>

            <div>
                <label for="txtIlgiAlanlari">İlgi Alanları:</label>
                <asp:TextBox ID="txtIlgiAlanlari" runat="server" CssClass="profil-input" />
            </div>

            <div>
                <label for="txtKonum">Konum:</label>
                <asp:TextBox ID="txtKonum" runat="server" CssClass="profil-input" />
            </div>
        </div>

        <div style="text-align:center;">
            <asp:Button ID="btnKaydet" runat="server" Text="Bilgileri Güncelle" OnClick="btnKaydet_Click" CssClass="btn-guncelle" />
        </div>

        <!-- Hata Mesajı -->
        <asp:Label ID="lblMesaj" runat="server" CssClass="error-message" />
      


        <!-- Katıldıkları Etkinlikler -->
        <div class="etkinlikler-container">
            <h3>Katıldığınız Etkinlikler:</h3>
            <asp:GridView ID="gvEtkinlikler" runat="server" AutoGenerateColumns="False" CssClass="table">
                <Columns>
                    <asp:BoundField DataField="EtkinlikAdi" HeaderText="Etkinlik Adı" SortExpression="EtkinlikAdi" />
                    <asp:BoundField DataField="EtkinlikTarihi" HeaderText="Etkinlik Tarihi" SortExpression="EtkinlikTarihi" />
                    <asp:BoundField DataField="EtkinlikSaati" HeaderText="Etkinlik Saati" SortExpression="EtkinlikSaati" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
