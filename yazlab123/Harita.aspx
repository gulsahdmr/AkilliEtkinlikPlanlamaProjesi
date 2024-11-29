<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Harita.aspx.cs" Inherits="yazlab123.Harita" %>



<!DOCTYPE html>
<html>
<head>
    <title>Etkinlik Konumu</title>
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyD8cviC_WIeo3SkAa8HA-keHz5U2T4SxDI"></script>
    <style>
        #map {
            height: 500px;
            width: 100%;
        }
    </style>
</head>
<body>
    <h3>Etkinlik Konumu</h3>
    <div id="map"></div>
    <script>
        function initMap() {
            // Etkinlik konumu (örneğin, İstanbul)
            const etkinlikKonumu = { lat: 41.0082, lng: 28.9784 };

            // Harita oluştur
            const map = new google.maps.Map(document.getElementById("map"), {
                zoom: 15,
                center: etkinlikKonumu,
            });

            // İşaretçi ekle
            const marker = new google.maps.Marker({
                position: etkinlikKonumu,
                map: map,
                title: "Etkinlik Yeri",
            });
        }

        // Haritayı yükle
        window.onload = initMap;
    </script>
</body>
</html>
