﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EtkinlikDetay.aspx.cs" Inherits="yazlab123.EtkinlikDetay" %>


<!DOCTYPE html>
<html>
<head runat="server">
    <title>Etkinlik Detayı</title>
    <style>
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f7f7f7;
            margin: 0;
            padding: 0;
        }
        .container {
            width: 50%;
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
        .event-info {
            font-size: 18px;
            color: #333;
            margin-bottom: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>Etkinlik Detayı</h1>
         <div class="event-detail">
                <asp:HiddenField ID="hdnEtkinlikID" runat="server" />
    <p class="event-info"><strong>Etkinlik Adı:</strong> <asp:Label ID="eventNameLabel" runat="server" /></p>
    <p class="event-info"><strong>Açıklama:</strong> <asp:Label ID="eventDescriptionLabel" runat="server" /></p>
    <p class="event-info"><strong>Tarih:</strong> <asp:Label ID="eventDateLabel" runat="server" /></p>
    <p class="event-info"><strong>Saat:</strong> <asp:Label ID="eventTimeLabel" runat="server" /></p>
    <p class="event-info"><strong>Süre:</strong> <asp:Label ID="eventDurationLabel" runat="server" /></p>
    <p class="event-info"><strong>Konum:</strong> <asp:Label ID="eventLocationLabel" runat="server" /></p>
    <p class="event-info"><strong>Kategori:</strong> <asp:Label ID="eventCategoryLabel" runat="server" /></p>



    <asp:Button ID="KatilButton" runat="server" Text="Etkinliğe Katıl" OnClick="KatilButton_Click" />
   <asp:Button ID="SilButton" runat="server" Text="Etkinliği Sil" OnClick="SilButton_Click" Visible="false" />
    <asp:Button ID="GuncelleButton" runat="server" Text="Etkinliği Güncelle" OnClick="GuncelleButton_Click" Visible="false" />
              <asp:Button ID="Onayla" runat="server" Text="Etkinliği Onayla" OnClick="OnaylaButton_Click" Visible="false" />
             <asp:Label ID="OnayliMesajLabel" runat="server" ForeColor="Green" Visible="False" />
<asp:Label ID="ErrorMessageLabel" runat="server" ForeColor="Red" Visible="False" />

    <asp:Label ID="lblMesaj" runat="server" ForeColor="Red"></asp:Label>
</div>

        </div>
    </form>
</body>
</html>