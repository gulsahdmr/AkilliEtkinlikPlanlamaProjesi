﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EtkinlikDetay.aspx.cs" Inherits="yazlab123.EtkinlikDetay" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Etkinlik Detayı</title>
    <style>
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f3f4f6;
            margin: 0;
            padding: 0;
        }
        .container {
            width: 70%;
            margin: 50px auto;
            padding: 20px;
            box-sizing: border-box;
        }
        .event-detail {
            background-color: #ffffff;
            padding: 30px;
            border-radius: 12px;
            box-shadow: 0 6px 20px rgba(0, 0, 0, 0.1);
            text-align: left;
        }
        h1 {
            color: #333;
            font-size: 2rem;
            text-align: center;
            margin-bottom: 20px;
        }
        .event-info {
            font-size: 1.1rem;
            color: #555;
            margin-bottom: 15px;
        }
        .event-info strong {
            color: #222;
        }
        .btn {
            display: inline-block;
            margin: 15px 10px;
            padding: 12px 30px;
            font-size: 1.1rem;
            color: white;
            border: none;
            border-radius: 8px;
            cursor: pointer;
            transition: all 0.3s ease;
            text-align: center;
        }
        .btn:hover {
            opacity: 0.8;
        }
        .btn-join {
            background-color: #007bff;
        }
        .btn-join:hover {
            background-color: #0056b3;
        }
        .btn-delete {
            background-color: #dc3545;
        }
        .btn-delete:hover {
            background-color: #c82333;
        }
        .btn-update {
            background-color: #ffc107;
            color: #333;
        }
        .btn-update:hover {
            background-color: #e0a800;
        }
        .btn-approve {
            background-color: #28a745;
        }
        .btn-approve:hover {
            background-color: #218838;
        }
        .message {
            font-size: 1rem;
            margin-top: 20px;
            text-align: center;
        }
        .message-success {
            color: #28a745;
        }
        .message-error {
            color: #dc3545;
        }
        /* Responsive design */
        @media (max-width: 768px) {
            .container {
                width: 90%;
            }
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

                <div class="btn-container">
                    <asp:Button ID="KatilButton" runat="server" Text="Etkinliğe Katıl" OnClick="KatilButton_Click" CssClass="btn btn-join" />
                    <asp:Button ID="SilButton" runat="server" Text="Etkinliği Sil" OnClick="SilButton_Click" Visible="false" CssClass="btn btn-delete" />
                    <asp:Button ID="GuncelleButton" runat="server" Text="Etkinliği Güncelle" OnClick="GuncelleButton_Click" Visible="false" CssClass="btn btn-update" />
                    <asp:Button ID="Onayla" runat="server" Text="Etkinliği Onayla" OnClick="OnaylaButton_Click" Visible="false" CssClass="btn btn-approve" />
                </div>

                <asp:Label ID="OnayliMesajLabel" runat="server" ForeColor="Green" Visible="False" class="message message-success" />
                <asp:Label ID="ErrorMessageLabel" runat="server" ForeColor="Red" Visible="False" class="message message-error" />
                <asp:Label ID="lblMesaj" runat="server" ForeColor="Red" class="message message-error"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
