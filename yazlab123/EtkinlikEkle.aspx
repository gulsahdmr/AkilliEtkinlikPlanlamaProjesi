<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EtkinlikEkle.aspx.cs" Inherits="yazlab123.EtkinlikEkle" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Etkinlik Ekle</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 100;
            padding: 0;
            background-color: #f0f0f0;
        }
        .container {
            margin: 1000px;
        }
        .form-group {
            margin-bottom: 15px;
        }
        .form-group label {
            font-weight: bold;
        }
        .form-group input,
        .form-group textarea {
            width: 100%;
            padding: 10px;
            margin-top: 5px;
            border: 1px solid #ccc;
            border-radius: 4px;
        }
        .form-group button {
            background-color: #007bff;
            color: white;
            padding: 10px 20px;
            border: none;
            cursor: pointer;
            border-radius: 5px;
        }
        .form-group button:hover {
            background-color: #0056b3;
        }
    </style>
</head>
<body>
    <div class="container">
        <h1>Yeni Etkinlik Ekle</h1>
        <form method="post" runat="server">
            <div class="form-group">
                <label for="EtkinlikAdi">Etkinlik Adı</label>
                <input type="text" id="EtkinlikAdi" name="EtkinlikAdi" runat="server" />
            </div>
            <div class="form-group">
                <label for="EtkinlikAciklamasi">Etkinlik Açıklaması</label>
                <textarea id="EtkinlikAciklamasi" name="EtkinlikAciklamasi" runat="server"></textarea>
            </div>
            <div class="form-group">
                <label for="EtkinlikTarihi">Etkinlik Tarihi</label>
                <input type="date" id="EtkinlikTarihi" name="EtkinlikTarihi" runat="server" />
            </div>
            <div class="form-group">
                <label for="EtkinlikSaati">Etkinlik Saati</label>
                <input type="time" id="EtkinlikSaati" name="EtkinlikSaati" runat="server" />
            </div>
            <div class="form-group">
                <label for="EtkinlikSuresi">Etkinlik Süresi (Dakika)</label>
                <input type="number" id="EtkinlikSuresi" name="EtkinlikSuresi" runat="server" />
            </div>
            <div class="form-group">
                <label for="EtkinlikKonumu">Etkinlik Konumu</label>
                <input type="text" id="EtkinlikKonumu" name="EtkinlikKonumu" runat="server" />
            </div>
            <div class="form-group">
                <label for="EtkinlikKategorisi">Etkinlik Kategorisi</label>
                <input type="text" id="EtkinlikKategorisi" name="EtkinlikKategorisi" runat="server" />
            </div>
            <div class="form-group">
                <button type="submit" runat="server" onserverclick="SubmitEtkinlik">Etkinliği Ekle</button>
            </div>
            <asp:Label ID="lblMessage" runat="server" ForeColor="Green" Visible="false"></asp:Label>
            <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red" Visible="false"></asp:Label>

        </form>
    </div>
</body>
</html>
