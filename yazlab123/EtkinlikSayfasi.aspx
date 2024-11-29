<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EtkinlikSayfasi.aspx.cs" Inherits="yazlab123.EtkinlikSayfasi" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Etkinlik Listesi</title>
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyD8cviC_WIeo3SkAa8HA-keHz5U2T4SxDI"></script>
    <script>
    document.addEventListener("DOMContentLoaded", function () {
        // Harita başlangıç ayarları
        var map = new google.maps.Map(document.getElementById("map"), {
            center: { lat: 41.0082, lng: 28.9784 }, // Başlangıç konumu (İstanbul)
            zoom: 6 // Yakınlaştırma seviyesi
        });

        // Etkinlik işaretleyicilerini ekleme
        etkinlikler.forEach(function (etkinlik) {
            var marker = new google.maps.Marker({
                position: { lat: etkinlik.Lat, lng: etkinlik.Lng },
                map: map,
                title: etkinlik.EtkinlikAdi
            });

            // İşaretleyiciye açılır bilgi ekleme
            var infoWindow = new google.maps.InfoWindow({
                content: `<h3>${etkinlik.EtkinlikAdi}</h3>`
            });

            marker.addListener("click", function () {
                infoWindow.open(map, marker);
            });
        });
    });
    </script>

    <style>
        /* Genel stil */
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f4f4f9;
            margin: 0;
            padding: 0;
        }
        h1 {
            text-align: center;
            margin: 20px 0;
            color: #4a4a4a;
        }
        .container {
            width: 90%;
            margin: 0 auto;
            display: flex;
            flex-wrap: wrap;
            gap: 20px;
        }
        .event-item {
            background-color: #ffffff;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.15);
            flex: 1 1 calc(45% - 20px); /* İki sütun düzeni */
            min-width: 300px;
            transition: transform 0.3s ease, box-shadow 0.3s ease;
        }
        .event-item:hover {
            transform: translateY(-5px);
            box-shadow: 0 8px 20px rgba(0, 0, 0, 0.2);
        }
        .event-info {
            font-size: 16px;
            color: #555;
            margin-bottom: 10px;
        }
        .btn {
            display: inline-block;
            background-color: #007bff;
            color: white;
            padding: 10px 20px;
            border: none;
            cursor: pointer;
            font-size: 14px;
            border-radius: 5px;
            transition: background-color 0.3s ease, transform 0.3s ease;
            text-align: center;
            text-decoration: none;
        }
        .btn:hover {
            background-color: #0056b3;
            transform: scale(1.05);
        }

        #map {
        width: 100%;
        height: 500px;
        margin: 20px 0;
    }


        .btn-delete {
            background-color: #dc3545;
        }
        .btn-edit {
            background-color: #28a745;
        }
        .btn-delete:hover {
            background-color: #c82333;
        }
        .btn-edit:hover {
            background-color: #218838;
        }
        .back-button {
            background-color: #6c757d;
            color: white;
            margin-bottom: 20px;
            padding: 8px 15px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            transition: background-color 0.3s ease, transform 0.3s ease;
        }
        .back-button:hover {
            background-color: #5a6268;
        }
        .event-status {
            font-weight: bold;
            color: #007bff;
        }
        .event-status.not-approved {
            color: #dc3545;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center; margin-top: 20px;">
            <asp:Button ID="btnAnasayfa" runat="server" Text="Anasayfa" OnClick="btnAnasayfa_Click" CssClass="back-button" />
        </div>
        <div id="map"></div>

        <h1>Etkinlik Listesi</h1>
        <div class="container">
            <asp:Repeater ID="eventRepeater" runat="server">
                <ItemTemplate>
                    <div class="event-item">
                        <p class="event-info"><strong>Etkinlik Adı:</strong> <%# Eval("EtkinlikAdi") %></p>
                        <p class="event-info"><strong>Tarih:</strong> <%# Eval("EtkinlikTarihi", "{0:dd MMMM yyyy}") %></p>
                        <p class="event-info"><strong>Konum:</strong> <%# Eval("EtkinlikKonumu") %></p>
                        <p class="event-info">
                            <strong>Durum:</strong> 
                            <span class="event-status <%# Convert.ToInt32(Eval("Onay")) == 1 ? "" : "not-approved" %>">
                                <%# Convert.ToInt32(Eval("Onay")) == 1 ? "Onaylı" : "Onaylı Değil" %>
                            </span>
                        </p>
                        <asp:Button ID="ShowDetailsButton" runat="server" Text="Etkinlik Detayını Göster" CssClass="btn" CommandArgument='<%# Eval("EtkinlikID") %>' OnCommand="ShowDetailsButton_Command" />
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
