<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Harita.aspx.cs" Inherits="yazlab123.Harita" %>



<!DOCTYPE html>
<html>
<head>
    <title>Harita ve Etkinlik Konumları</title>
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDqml8D46ngghevR-f9nLFNoKCwZ-EZyEo" async defer></script>
    <style>
        #map {
            height: 500px;
            width: 100%;
        }
    </style>
</head>
<body>
    <h2>Etkinlik Haritası</h2>
    <div id="map"></div>

    <script>
        // Harita başlatma fonksiyonu
        function initMap() {
            // Haritanın merkezi (örneğin İstanbul)
            const initialLocation = { lat: 41.0082, lng: 28.9784 };

            // Haritayı oluştur
            const map = new google.maps.Map(document.getElementById("map"), {
                zoom: 12,
                center: initialLocation,
            });

            // Örnek bir işaretçi ekleyelim
            new google.maps.Marker({
                position: initialLocation,
                map: map,
                title: "Etkinlik Noktası",
            });
        }
    </script>
</body>
</html>
