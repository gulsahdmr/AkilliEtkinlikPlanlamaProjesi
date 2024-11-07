<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Anasayfa.aspx.cs" Inherits="yazlab123.Anasayfa" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Ana Sayfa</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
            background-color: #f0f0f0;
        }

        .menu {
            background-color: #007bff;
            padding: 15px;
            color: white;
            text-align: center;
        }

        .menu a {
            color: white;
            text-decoration: none;
            margin: 0 15px;
        }

        .container {
            margin: 20px;
        }

        .event-list {
            display: grid;
            grid-template-columns: repeat(auto-fill, minmax(250px, 1fr));
            gap: 20px;
            margin-top: 40px;
        }

        .event-card {
            background-color: white;
            border-radius: 5px;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
            padding: 20px;
            text-align: center;
            transition: transform 0.3s ease;
        }

        .event-card:hover {
            transform: scale(1.05);
        }

        .event-card h4 {
            margin-top: 0;
            font-size: 20px;
            color: #333;
        }

        .event-card p {
            color: #666;
            font-size: 14px;
            margin-bottom: 15px;
        }

        .event-card button {
            background-color: #007bff;
            color: white;
            padding: 10px;
            border: none;
            width: 100%;
            cursor: pointer;
            border-radius: 5px;
        }

        .event-card button:hover {
            background-color: #0056b3;
        }

        .profile {
            background-color: #007bff;
            color: white;
            padding: 10px;
            border-radius: 5px;
            text-align: center;
        }

        .profile a {
            color: white;
            text-decoration: none;
        }

        .menu a:hover {
            text-decoration: underline;
        }

        h1 {
            color: #333;
        }
    </style>
</head>
<body>
    <!-- Menü Barı -->
    <div class="menu">
        <a href="Anasayfa.aspx">Ana Sayfa</a>
        <a href="EtkinlikSayfasi.aspx">Etkinlik Sayfası</a>
        <a href="Sohbet.aspx">Sohbet</a>
        <a href="KullaniciProfili.aspx">Kullanıcı Profili</a>
        <a href="AdminProfili.aspx">Admin Profili</a>
        <a href="Login.aspx">Çıkış</a>
    </div>

    <!-- Ana Sayfa İçeriği -->
    <div class="container">
        <h1>Hoşgeldiniz, <%= Session["Username"] %>!</h1>

        <!-- Kullanıcı Profili -->
        <div class="profile">
            <h3>Profiliniz</h3>
            <p>Kullanıcı adınız: <%= Session["Username"] %></p>
            <a href="KullaniciProfili.aspx">Profilinizi Görüntüleyin</a>
        </div>

        <!-- Etkinlik Listesi -->
        <h3>Önerilen Etkinlikler</h3>
        <div class="event-list">
            <%-- Veritabanından etkinlikler çekilip burada listelenecek --%>
            <div class="event-card">
                <h4>Etkinlik 1</h4>
                <p>Etkinlik açıklaması burada olacak. Bu etkinlik hakkında daha fazla bilgi edinin.</p>
                <button>Etkinliğe Katıl</button>
            </div>
            <div class="event-card">
                <h4>Etkinlik 2</h4>
                <p>Etkinlik açıklaması burada olacak. Bu etkinlik hakkında daha fazla bilgi edinin.</p>
                <button>Etkinliğe Katıl</button>
            </div>
            <div class="event-card">
                <h4>Etkinlik 3</h4>
                <p>Etkinlik açıklaması burada olacak. Bu etkinlik hakkında daha fazla bilgi edinin.</p>
                <button>Etkinliğe Katıl</button>
            </div>
        </div>
    </div>
</body>
</html>
