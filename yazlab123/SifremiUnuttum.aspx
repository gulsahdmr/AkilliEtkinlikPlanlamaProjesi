<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SifremiUnuttum.aspx.cs" Inherits="yazlab123.SifremiUnuttum" %>

<!DOCTYPE html>
<html>
<head>
    <title>Şifremi Unuttum</title>
    <style>
        /* Sayfa düzeni için */
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
        }
        .form-container {
            background-color: rgba(168, 168, 168, 0.85);
            padding: 30px;
            border-radius: 8px;
            box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1);
            width: 400px;
        }
        .form-group {
            margin-bottom: 15px;
        }
        label {
            display: block;
            font-weight: bold;
        }
        input {
            width: 100%;
            padding: 10px;
            font-size: 14px;
            border: 1px solid #ccc;
            border-radius: 4px;
        }
        .btn {
            background-color: #4CAF50;
            color: white;
            padding: 12px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            width: 100%;
            font-size: 16px;
        }
        .btn:hover {
            background-color: #45a049;
        }
        .error-message {
            color: red;
            text-align: center;
        }
    </style>
</head>
<body>
    <div class="form-container">
        <h2>Şifremi Unuttum</h2>
        <form id="form1" runat="server">
            <div class="form-group">
                <label for="txtEposta">E-posta Adresi:</label>
                <asp:TextBox ID="txtEposta" runat="server" />
            </div>
            <div class="form-group">
                <label for="txtYeniSifre">Yeni Şifre:</label>
                <asp:TextBox ID="txtYeniSifre" runat="server" TextMode="Password" />
            </div>
            <div class="form-group">
                <asp:Button ID="btnSifreYenile" runat="server" Text="Şifreyi Yenile" OnClick="btnSifreYenile_Click" CssClass="btn" />
            </div>
            <div class="error-message">
                <asp:Label ID="lblMesaj" runat="server" ForeColor="Red" />
            </div>
        </form>
    </div>
</body>
</html>
