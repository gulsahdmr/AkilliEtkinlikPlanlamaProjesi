<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sohbet.aspx.cs" Inherits="yazlab123.Sohbet" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Sohbet Sayfası</title>
    <style>
        /* Sayfa genel stili */
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f4f7fc;
            margin: 0;
            padding: 0;
            display: flex;
            justify-content: center;
            align-items: center;
            min-height: 100vh;
            color: #333;
        }

        .container {
            display: flex;
            width: 120%;
            max-width: 1400px;
            height: 95vh;
            background-color: #fff;
            border-radius: 20px;
            overflow: hidden;
            box-shadow: 0 8px 16px rgba(0, 0, 0, 0.1);
        }

        /* Sol panel: Katıldığım etkinlikler listesi */
        .event-list-panel {
            width: 30%;
            background: linear-gradient(180deg, #3498db, #2980b9);
            color: #fff;
            display: flex;
            flex-direction: column;
            padding: 20px;
            overflow-y: auto;
            border-radius: 12px 0 0 12px;
        }

        .event-list-panel h2 {
            text-align: center;
            margin-bottom: 20px;
            font-size: 1.5em;
            font-weight: 600;
        }

        .event-list {
            display: flex;
            flex-direction: column;
            gap: 15px;
        }

        .event-item {
            padding: 12px;
            background-color: #2c3e50;
            color: #fff;
            border-radius: 8px;
            cursor: pointer;
            text-align: center;
            transition: background-color 0.3s;
            font-weight: 500;
        }

        .event-item:hover {
            background-color: #1abc9c;
            transform: scale(1.05);
        }

        /* Sağ panel: Sohbet alanı */
        .chat-panel {
            width: 70%;
            background-color: #fff;
            display: flex;
            flex-direction: column;
            padding: 20px;
            box-shadow: inset 0 0 15px rgba(0, 0, 0, 0.05);
        }

        .chat-panel h2 {
            text-align: center;
            font-size: 1.8em;
            color: #333;
            margin-bottom: 20px;
        }

        /* Etkinlik bilgisi alanı */
        .event-info {
            background-color: #f1f1f1;
            padding: 20px;
            border-radius: 12px;
            margin-bottom: 20px;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
        }

        .event-info h3 {
            margin: 0;
            color: #3498db;
            font-weight: 600;
        }

        .event-info p {
            margin: 5px 0;
            color: #555;
        }

        /* Sohbet alanı */
        .chat-container {
            background-color: #fff;
            border-radius: 12px;
            padding: 20px;
            box-shadow: 0 4px 15px rgba(0, 0, 0, 0.1);
            flex-grow: 1;
            overflow-y: auto;
            margin-bottom: 20px;
        }

        /* Mesaj stilleri */
        .message {
            margin-bottom: 20px;
            display: flex;
            flex-direction: column;
        }

        .message .sender {
            font-weight: bold;
            color: #3498db;
        }

        .message .text {
            background-color: #ecf0f1;
            padding: 12px;
            border-radius: 10px;
            margin-top: 8px;
            color: #333;
            max-width: 75%;
            font-size: 1em;
        }

        .message .time {
            font-size: 0.8em;
            color: #888;
            margin-top: 5px;
        }

        /* Mesaj gönderme alanı */
        .send-message {
            display: flex;
            align-items: center;
            margin-top: 10px;
        }

        .send-message input[type="text"] {
            flex: 1;
            padding: 12px;
            border: 1px solid #ddd;
            border-radius: 15px;
            font-size: 1em;
            transition: border-color 0.3s;
        }

        .send-message input[type="text"]:focus {
            outline: none;
            border-color: #3498db;
        }

        .send-message button {
            padding: 12px 24px;
            border: none;
            background-color: #3498db;
            color: white;
            border-radius: 0 15px 15px 0;
            font-size: 1.2em;
            cursor: pointer;
            transition: background-color 0.3s;
            margin-left: 10px;
        }

        .send-message button:hover {
            background-color: #2980b9;
        }

        /* Anasayfa butonu */
        .back-button {
            background-color: #e74c3c;
            color: white;
            padding: 8px 15px;
            border-radius: 8px;
            font-size: 1.1em;
            border: none;
            cursor: pointer;
            transition: background-color 0.3s;
        }

        .back-button:hover {
            background-color: #c0392b;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <!-- Sol panel: Katıldığım etkinlikler listesi -->
            <div class="event-list-panel">
                <h2>Katıldığım Etkinlikler</h2>
                <div class="event-list">
                  <asp:Repeater ID="rpEtkinlikler" runat="server" OnItemCommand="rpEtkinlikler_ItemCommand">
    <ItemTemplate>
        <asp:Button 
            ID="btnEtkinlikSec" 
            runat="server" 
            Text='<%# Eval("EtkinlikAdi") %>' 
            CommandName="Select" 
            CommandArgument='<%# Eval("EtkinlikID") %>' 
            CssClass="event-item" />
    </ItemTemplate>
</asp:Repeater>
                </div>
            </div>

        <!-- Sağ panel: Sohbet alanı -->
        <div class="chat-panel">
            <h2>Sohbet</h2>

            <!-- Anasayfa'ya dön butonu -->
            <div style="text-align: right; margin-bottom: 15px;">
                <asp:Button ID="btnAnasayfa" runat="server" Text="Anasayfa" OnClick="btnAnasayfa_Click" CssClass="back-button" />
            </div>

            <!-- Etkinlik bilgisi alanı -->
            <div class="event-info">
                <h3><asp:Label ID="lblEtkinlikAdi" runat="server" /></h3>
                <p><strong>Konum:</strong> <asp:Label ID="lblEtkinlikKonumu" runat="server" /></p>
                <p><strong>Tarih ve Saat:</strong> <asp:Label ID="lblEtkinlikSaati" runat="server" /></p>
            </div>

            <!-- Sohbet alanı -->
            <div class="chat-container" id="chatContainer">
                <asp:Repeater ID="rpMesajlar" runat="server">
                    <ItemTemplate>
                        <div class="message">
                            <span class="sender"><%# Eval("GondericiAdi") %>:</span>
                            <div class="text"><%# Eval("MesajMetni") %></div>
                            <span class="time"><%# Eval("GonderimZamani", "{0:dd.MM.yyyy HH:mm}") %></span>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>

            <!-- Mesaj gönderme alanı -->
            <div class="send-message">
                <asp:TextBox ID="txtMesaj" runat="server" placeholder="Mesajınızı yazın..." Width="100%" />
                <asp:Button ID="btnGonder" runat="server" Text="Gönder" OnClick="btnGonder_Click" />
            </div>
        </div>
    </form>
</body>
</html>
