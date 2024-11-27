<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EtkinlikSayfasi.aspx.cs" Inherits="yazlab123.EtkinlikSayfasi" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Etkinlik Listesi</title>
    <style>
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f7f7f7;
            margin: 0;
            padding: 0;
        }
        .container {
            width: 80%;
            margin: 20px auto;
            display: flex;
            flex-wrap: wrap;
            justify-content: space-between;
        }
        .event-item {
            background-color: #ffffff;
            padding: 20px;
            margin-bottom: 20px;
            border-radius: 10px;
            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
            width: 48%;
            box-sizing: border-box;
        }
        .event-info {
            font-size: 18px;
            color: #333;
            margin-bottom: 10px;
        }
        .btn {
            background-color: #007bff;
            color: white;
            padding: 10px 20px;
            border: none;
            cursor: pointer;
            font-size: 16px;
            border-radius: 5px;
            transition: background-color 0.3s;
            text-align: center;
        }
        .btn:hover {
            background-color: #0056b3;
        }
        .btn-delete {
            background-color: #dc3545;
        }
        .btn-edit {
            background-color: #28a745;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>Etkinlik Listesi</h1>
            <asp:Repeater ID="eventRepeater" runat="server">
                <ItemTemplate>
                    <div class="event-item">
                        <p class="event-info"><strong>Etkinlik Adı:</strong> <%# Eval("EtkinlikAdi") %></p>
                        <p class="event-info"><strong>Tarih:</strong> <%# Eval("EtkinlikTarihi", "{0:dd MMMM yyyy}") %></p>
                        <p class="event-info"><strong>Konum:</strong> <%# Eval("EtkinlikKonumu") %></p>
                         <!-- Etkinlik Onay Durumu -->
                       <p class="event-info">
    <strong>Durum:</strong> 
    <%# Convert.ToInt32( Eval("Onay")) == 1 ? "Onaylı" : "Onaylı Değil" %>
</p>

                        
                        
                        <!-- Düzenle  Butonları -->
                        <asp:Button ID="ShowDetailsButton" runat="server" Text="Etkinlik Detayını Göster" CssClass="btn" CommandArgument='<%# Eval("EtkinlikID") %>' OnCommand="ShowDetailsButton_Command" />
                        
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
