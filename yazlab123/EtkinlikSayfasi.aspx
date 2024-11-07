<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EtkinlikSayfasi.aspx.cs" Inherits="yazlab123.EtkinlikSayfasi" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Etkinlik Detayları</title>
    <style>
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f7f7f7;
            margin: 0;
            padding: 0;
        }

        .container {
            width: 80%;
            margin: 0 auto;
            padding-top: 20px;
        }

        .event-detail {
            background-color: #ffffff;
            padding: 30px;
            border-radius: 10px;
            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
            max-width: 900px;
            margin: 0 auto;
        }

        .event-detail h3 {
            font-size: 24px;
            margin-bottom: 15px;
            color: #333;
        }

        .event-detail p {
            font-size: 16px;
            line-height: 1.6;
            color: #555;
            margin-bottom: 15px;
        }

        .event-detail strong {
            color: #333;
        }

        .event-buttons {
            display: flex;
            justify-content: space-between;
            margin-top: 20px;
        }

        .event-buttons button {
            background-color: #007bff;
            color: white;
            padding: 12px 20px;
            border: none;
            cursor: pointer;
            font-size: 16px;
            border-radius: 5px;
            transition: background-color 0.3s;
        }

        .event-buttons button:hover {
            background-color: #0056b3;
        }

        .event-info {
            font-size: 18px;
            color: #007bff;
            margin-bottom: 10px;
        }

        /* Responsive Design */
        @media screen and (max-width: 768px) {
            .container {
                width: 95%;
            }

            .event-detail {
                padding: 20px;
            }

            .event-detail h3 {
                font-size: 22px;
            }

            .event-buttons button {
                width: 48%;
            }
        }
    </style>
</head>
<body>
    <div class="container">
        <h1>Etkinlik Detayları</h1>
        <div class="event-detail">
            <p class="event-info">
                   <strong>Etkinlik Adı:</strong> 
                  <asp:Label ID="eventNameLabel" runat="server" Text="Etkinlik Adı" />
            </p>
               <p class="event-info">
                  <strong>Etkinlik Açıklaması:</strong> 
                   <asp:Label ID="eventDescriptionLabel" runat="server" Text="Etkinlik Açıklaması" />
              </p>
           
            <p class="event-info">
                <strong>Tarih:</strong> 
                <asp:Label ID="eventDateLabel" runat="server" Text="Tarih" />
            </p>
            <p class="event-info">
                <strong>Saat:</strong> 
                <asp:Label ID="eventTimeLabel" runat="server" Text="Saat" />
            </p>
             <p class="event-info">
                 <strong>Süre:</strong> 
                 <asp:Label ID="eventdurationlabel" runat="server" Text="Süre" />
             </p>
            <p class="event-info">
                <strong>Konum:</strong> 
                <asp:Label ID="eventlocationlabel" runat="server" Text="Konum" />
            </p>
            <p class="event-info">
                 <strong>Kategori:</strong> 
                 <asp:Label ID="eventcategorylabel" runat="server" Text="Kategori" />
            </p>
            <div class="event-buttons">
                <button>Etkinliğe Katıl</button>
                <button>Etkinliği Paylaş</button>
            </div>
        </div>
    </div>
</body>
</html>
