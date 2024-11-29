<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EtkinlikEkle.aspx.cs" Inherits="yazlab123.EtkinlikEkle" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Etkinlik Ekle</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f7fa;
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
            padding: 30px;
            background-color: #ffffff;
            box-shadow: 0 8px 16px rgba(0, 0, 0, 0.1);
            border-radius: 12px;
            text-align: center;
            border: 1px solid #ddd;
        }
        
        h1 {
            color: #4CAF50;
            font-size: 28px;
            margin-bottom: 20px;
            font-weight: 600;
        }
        
        .form-group {
            margin-bottom: 20px;
            text-align: left;
            display: flex;
            flex-direction: column;
            align-items: flex-start;
        }
        
        .form-group label {
            font-weight: bold;
            color: #555;
            margin-bottom: 8px;
        }
        
        .form-group input,
        .form-group textarea,
        .form-group select {
            width: 100%;
            padding: 12px;
            margin-top: 5px;
            border: 1px solid #ddd;
            border-radius: 8px;
            font-size: 16px;
            background-color: #f5f5f5;
            box-sizing: border-box;
        }
        
        .form-group textarea {
            resize: vertical;
            min-height: 120px;
        }
        
        .form-group button {
            width: 100%;
            padding: 14px;
            background-color: #28a745; /* Yeşil buton rengi */
            color: white;
            font-size: 18px;
            font-weight: bold;
            border: none;
            border-radius: 8px;
            cursor: pointer;
            transition: background-color 0.3s ease, transform 0.2s ease;
        }
        
        .form-group button:hover {
            background-color: #218838;
            transform: scale(1.05);
        }
        
        .form-group button:active {
            background-color: #1e7e34;
            transform: scale(1);
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
                <asp:DropDownList ID="ddlIlgiAlanlari" runat="server">
                    <asp:ListItem Text="Teknoloji" Value="Teknoloji" />
                    <asp:ListItem Text="Sağlık" Value="Saglik" />
                    <asp:ListItem Text="Spor" Value="Spor" />
                    <asp:ListItem Text="Sanat" Value="Sanat" />
                    <asp:ListItem Text="Edebiyat" Value="Edebiyat" />
                </asp:DropDownList>
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
