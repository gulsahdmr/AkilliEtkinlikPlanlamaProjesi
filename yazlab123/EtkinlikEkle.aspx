<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EtkinlikEkle.aspx.cs" Inherits="yazlab123.EtkinlikEkle" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Etkinlik Ekle</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f9f9fb;
            color: #333;
            display: flex;
            justify-content: center;
            align-items: center;
            min-height: 100vh;
            margin: 0;
        }
        
        .container {
            max-width: 600px;
            width: 100%;
            padding: 20px;
            margin: 50px auto;
            background-color: #ffffff;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
            border-radius: 8px;
            text-align: center;
        }
        
        h1 {
            color: #4a90e2;
            font-size: 24px;
            margin-bottom: 20px;
        }
        
        .form-group {
            margin-bottom: 15px;
            text-align: left;
        }
        
        .form-group label {
            font-weight: bold;
            color: #555;
            display: block;
            margin-bottom: 5px;
        }
        
        .form-group input,
        .form-group textarea {
            width: 100%;
            padding: 10px;
            margin-top: 5px;
            border: 1px solid #ddd;
            border-radius: 4px;
            font-size: 16px;
            background-color: #f5f5f5;
        }
        
        .form-group textarea {
            resize: vertical;
            min-height: 100px;
        }
        
        .form-group button {
            width: 100%;
            padding: 12px;
            background-color: #4a90e2;
            color: white;
            font-size: 16px;
            font-weight: bold;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }
        
        .form-group button:hover {
            background-color: #357abd;
        }
        
        .message-label {
            display: block;
            font-size: 16px;
            font-weight: bold;
            margin-top: 20px;
        }
        
        #lblMessage {
            color: #4caf50;
        }
        
        #lblErrorMessage {
            color: #f44336;
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
            <asp:Label ID="lblMessage" runat="server" CssClass="message-label" Visible="false"></asp:Label>
            <asp:Label ID="lblErrorMessage" runat="server" CssClass="message-label" Visible="false"></asp:Label>
        </form>
    </div>
</body>
</html>
