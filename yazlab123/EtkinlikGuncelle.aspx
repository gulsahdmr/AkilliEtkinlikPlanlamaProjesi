<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EtkinlikGuncelle.aspx.cs" Inherits="yazlab123.EtkinlikGuncelle" %>

<!DOCTYPE html>
<html lang="tr">
<head runat="server">
    <meta charset="UTF-8" />
    <title>Etkinlik Güncelle</title>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/css/bootstrap.min.css">
    <style>
        body {
            background-color: #f8f9fa;
            font-family: Arial, sans-serif;
        }
        .container {
            max-width: 600px;
            margin-top: 50px;
            background-color: white;
            padding: 30px;
            border-radius: 8px;
            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
        }
        .form-group label {
            font-weight: bold;
        }
        .form-group input {
            border-radius: 4px;
        }
        .form-group input:focus {
            box-shadow: 0 0 0 0.2rem rgba(0, 123, 255, 0.25);
            border-color: #0056b3;
        }
        .btn-primary {
            width: 100%;
            padding: 10px;
            font-size: 16px;
        }
        .menu {
            background-color: #343a40;
            padding: 10px 0;
            text-align: center;
        }
        .menu a {
            color: white;
            margin: 0 15px;
            font-size: 18px;
            text-decoration: none;
        }
        .menu a:hover {
            text-decoration: underline;
        }
    </style>
</head>
<body>
 
    <!-- Etkinlik Güncelleme Formu -->
    <div class="container">
        <h2 class="text-center mb-4">Etkinlik Güncelle</h2>
        
        <form id="form1" runat="server">
            <div class="form-group mb-3">
                <label for="txtEtkinlikAdi">Etkinlik Adı:</label>
                <asp:TextBox ID="txtEtkinlikAdi" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group mb-3">
                <label for="txtEtkinlikAciklamasi">Etkinlik Açıklaması:</label>
                <asp:TextBox ID="txtEtkinlikAciklamasi" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4"/>
            </div>
            <div class="form-group mb-3">
                <label for="txtEtkinlikKonumu">Etkinlik Konumu:</label>
                <asp:TextBox ID="txtEtkinlikKonumu" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group mb-3">
                <label for="txtEtkinlikTarihi">Etkinlik Tarihi:</label>
                <asp:TextBox ID="txtEtkinlikTarihi" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group mb-3">
                <label for="txtEtkinlikSaati">Etkinlik Saati:</label>
                <asp:TextBox ID="txtEtkinlikSaati" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group mb-3">
                <label for="txtEtkinlikSuresi">Etkinlik Süresi:</label>
                <asp:TextBox ID="txtEtkinlikSuresi" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group mb-4">
                <label for="txtEtkinlikKategorisi">Etkinlik Kategorisi:</label>
                <asp:TextBox ID="txtEtkinlikKategorisi" runat="server" CssClass="form-control" />
            </div>
            <asp:Button ID="btnGuncelle" runat="server" Text="Güncelle" CssClass="btn btn-primary" OnClick="btnGuncelle_Click" />
              <asp:Label ID="lblMesaj" runat="server" ForeColor="Red"></asp:Label>
        </form>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
