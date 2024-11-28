<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sohbet.aspx.cs" Inherits="yazlab123.Sohbet" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Sohbet Sayfası</title>
    <style>
        /* Sayfa genel stili */
        body {
            font-family: Arial, sans-serif;
            background-color: #f7f9fc;
            margin: 0;
            padding: 0;
            display: flex;
            justify-content: center;
            align-items: center;
            min-height: 100vh;
        }

        .container {
            display: flex;
            width: 90%;
            max-width: 1200px;
            height: 90vh;
            background-color: white;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
            border-radius: 10px;
            overflow: hidden;
        }

        /* Sol panel: Etkinlik listesi */
        .event-list-panel {
            width: 30%;
            background-color: #2c3e50;
            color: white;
            display: flex;
            flex-direction: column;
            padding: 20px;
            overflow-y: auto;
        }

        .event-list-panel h2 {
            text-align: center;
            color: #ecf0f1;
            margin-bottom: 20px;
        }

        .event-list {
            display: flex;
            flex-direction: column;
            gap: 10px;
        }

        .event-item {
            padding: 12px;
            background-color: #34495e;
            color: white;
            border-radius: 6px;
            cursor: pointer;
            text-align: center;
            transition: background-color 0.3s;
        }

        .event-item:hover {
            background-color: #1abc9c;
        }

        /* Sağ panel: Sohbet alanı */
        .chat-panel {
            width: 70%;
            background-color: #f9f9f9;
            padding: 20px;
            display: flex;
            flex-direction: column;
        }

        .chat-panel h2 {
            text-align: center;
            color: #333;
        }

        /* Etkinlik bilgisi alanı */
        .event-info {
            background-color: #ecf0f1;
            padding: 15px;
            border-radius: 10px;
            margin-bottom: 20px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }

        .event-info h3 {
            margin: 0;
            color: #3498db;
        }

        .event-info p {
            margin: 5px 0;
            color: #555;
        }

        /* Sohbet alanı */
        .chat-container {
            background-color: #ffffff;
            border-radius: 10px;
            padding: 20px;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
            flex-grow: 1;
            overflow-y: auto;
            margin-bottom: 20px;
        }

        /* Mesaj stilleri */
        .message {
            margin-bottom: 15px;
            display: flex;
            flex-direction: column;
        }

        .message .sender {
            font-weight: bold;
            color: #3498db;
        }

        .message .text {
            background-color: #f0f2f5;
            padding: 10px;
            border-radius: 10px;
            margin-top: 5px;
            max-width: 80%;
            color: #333;
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
            padding: 10px;
            border: 1px solid #ddd;
            border-radius: 10px 0 0 10px;
            font-size: 1em;
        }

        .send-message button {
            padding: 10px 20px;
            border: none;
            background-color: #3498db;
            color: white;
            border-radius: 0 10px 10px 0;
            font-size: 1em;
            cursor: pointer;
            transition: background-color 0.3s;
        }

        .send-message button:hover {
            background-color: #2980b9;
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
            CssClass="event-button" />
    </ItemTemplate>

</asp:Repeater>

                </div>
            </div>

        <!-- Sağ panel: Sohbet alanı -->
<div class="chat-panel">
    <h2>Sohbet</h2>

    <!-- Anasayfa'ya dön butonu -->
    <div style="text-align: right; margin-bottom: 10px;">
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
        </div>
    </form>

    <script>
        // JavaScript fonksiyonu, Detay Görüntüle butonuna basıldığında çağrılır
        function updateChat(eventDetails) {
            const chatMessages = document.querySelector('.chat-messages');
            chatMessages.innerHTML = `
                <div class="chat-message">
                    <span class="username">Etkinlik Detayı</span>
                    <p>${eventDetails}</p>
                </div>
            `;
        }
    </script>
</body>
</html>
