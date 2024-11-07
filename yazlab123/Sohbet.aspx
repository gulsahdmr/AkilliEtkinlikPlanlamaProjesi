<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sohbet.aspx.cs" Inherits="yazlab123.Sohbet" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Sohbet</title>
    <style>
        /* Sohbet sayfası tasarımını burada yapıyoruz */
    </style>
</head>
<body>
    <div class="container">
        <h1>Sohbet</h1>
        <div class="chat-box">
            <!-- Burada sohbet mesajları gösterilecek -->
        </div>
        <textarea id="message" placeholder="Mesajınızı yazın..."></textarea>
        <button id="sendBtn">Gönder</button>
    </div>
</body>
</html>
