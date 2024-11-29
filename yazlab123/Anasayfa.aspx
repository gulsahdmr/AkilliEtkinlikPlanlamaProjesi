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

        .event-section {
            margin-top: 40px;
        }

        .event-list {
            display: grid;
            grid-template-columns: repeat(auto-fill, minmax(250px, 1fr));
            gap: 20px;
            margin-top: 20px;
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

        /* Katıl Butonu Rengi ve Tasarımı */
        .event-card .join-button {
            background-color: #28a745;
            color: white;
            padding: 12px 20px; /* Butonun iç paddingini artırarak daha geniş yapıyoruz */
            border: none;
            border-radius: 30px; /* Yuvarlatılmış köşeler */
            text-align: center;
            font-size: 16px; /* Font boyutunu biraz artırıyoruz */
            transition: background-color 0.3s ease, transform 0.3s ease; /* Hover efektine geçiş ekliyoruz */
            display: inline-block; /* Butonu satır içi eleman olarak yapıyoruz */
            text-decoration: none; /* Alt çizgiyi kaldırıyoruz */
            width: auto; /* Butonun genişliği içeriğine bağlı olarak ayarlanacak */
            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.2); /* Hafif gölge efekti */
        }

        .event-card .join-button:hover {
            background-color: #218838; /* Hover efektiyle renk değişimi */
            transform: scale(1.05); /* Hoverda butonu biraz büyütüyoruz */
        }

        /* Tıklandığında buton için bir efekt eklemek de faydalı olabilir */
        .event-card .join-button:active {
            transform: scale(1); /* Tıklanıldığında büyüme efekti iptal */
            background-color: #1e7e34; /* Tıklanıldığında daha koyu renk */
        }

        /* Kullanıcı Profili */
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

       /* Yeni Etkinlik Ekle Butonu */
.add-event-button {
    background-color: #28a745;
    color: white;
    padding: 12px 20px; /* Butonun iç paddingini artırarak daha geniş yapıyoruz */
    border: none;
    border-radius: 30px; /* Yuvarlatılmış köşeler */
    text-decoration: none;
    text-align: center;
    display: inline-block;
    margin-top: 20px;
    font-size: 16px; /* Font boyutunu biraz artırıyoruz */
    transition: background-color 0.3s ease, transform 0.3s ease; /* Hover efektine geçiş ekliyoruz */
    box-shadow: 0 4px 10px rgba(0, 0, 0, 0.2); /* Hafif gölge efekti */
}

.add-event-button:hover {
    background-color: #218838;
    transform: scale(1.05); /* Hoverda butonu biraz büyütüyoruz */
}

.add-event-button:active {
    transform: scale(1); /* Tıklanıldığında büyüme efekti iptal */
    background-color: #1e7e34; /* Tıklanıldığında daha koyu renk */
}

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <!-- Menü Barı -->
        <div class="menu">
            <a href="Anasayfa.aspx">Ana Sayfa</a>
            <a href="EtkinlikSayfasi.aspx">Etkinlik Sayfası</a>
            <a href="Sohbet.aspx">Sohbet</a>
            <a href="KullaniciProfili.aspx">Kullanıcı Profili</a>
            <asp:PlaceHolder ID="phAdmin" runat="server">
                <a href="AdminProfili.aspx">Admin Profili</a>
            </asp:PlaceHolder>
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

            <!-- Önerilen Etkinlikler Bölümü -->
            <div class="event-section">
                <h3>Önerilen Etkinlikler</h3>
                <div class="event-list">
                    <!-- Veritabanından gelen etkinlikleri listelemek için Repeater kullanıyoruz -->
                    <asp:Repeater ID="rptEtkinlikler" runat="server">
                        <ItemTemplate>
                            <div class="event-card">
                                <h4><%# Eval("EtkinlikAdi") %></h4>
                              
                               <div>
                                    <p><%# Eval("EtkinlikKonumu") %> - <%# Eval("EtkinlikTarihi", "{0:dd MMM yyyy}") %></p>
                                    <a href='EtkinlikDetay.aspx?EtkinlikID=<%# Eval("EtkinlikID") %>' class="join-button">Etkinlik Detay</a>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>

            <!-- Kullanıcının Oluşturduğu Etkinlikler Bölümü -->
            <div class="event-section">
                <h3>Kendi Oluşturduğunuz Etkinlikler</h3>
                <div class="event-list">
                    <asp:Repeater ID="rpKullaniciEtkinlikler" runat="server">
                        <ItemTemplate>
                            <div class="event-card">
                                <h4><%# Eval("EtkinlikAdi") %></h4>
                                <p><%# Eval("EtkinlikKonumu") %> - <%# Eval("EtkinlikSaati") %></p>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>

            <!-- Yeni Etkinlik Ekleme Butonu -->
            <a href="EtkinlikEkle.aspx" class="add-event-button">Yeni Etkinlik Ekle</a>
        </div>
    </form>
</body>
</html>
