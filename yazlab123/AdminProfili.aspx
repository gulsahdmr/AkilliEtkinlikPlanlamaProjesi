<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminProfili.aspx.cs" Inherits="yazlab123.AdminProfili" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Profili</title>
    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f8f9fa;
        }
        .container {
            margin-top: 50px;
        }
        h1 {
            text-align: center;
            margin-bottom: 30px;
            color: #343a40;
        }
        .grid-view {
            background-color: #ffffff;
            border: 1px solid #dee2e6;
            border-radius: 5px;
            padding: 20px;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
        }
        .grid-view table {
            width: 100%;
            border-collapse: collapse;
        }
        .grid-view th {
            background-color: #007bff;
            color: white;
            text-align: center;
            padding: 10px;
        }
        .grid-view td {
            text-align: center;
            padding: 10px;
            border: 1px solid #dee2e6;
        }
        .grid-view tr:nth-child(even) {
            background-color: #f2f2f2;
        }
        .btn-custom {
            margin: 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>Admin Profili</h1>
            <div class="grid-view">
                <asp:GridView ID="gvKullanicilar" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered" OnRowCommand="gvKullanicilar_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="KullaniciID" HeaderText="Kullanıcı ID" />
                        <asp:BoundField DataField="Ad" HeaderText="Ad" />
                        <asp:BoundField DataField="Soyad" HeaderText="Soyad" />
                        <asp:BoundField DataField="KullaniciAdi" HeaderText="Kullanıcı Adı" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button CommandName="Sil" CommandArgument="<%# Container.DataItemIndex %>" CssClass="btn btn-danger btn-sm btn-custom" Text="Sil" runat="server" />
                                <asp:Button CommandName="Detay" CommandArgument="<%# Container.DataItemIndex %>" CssClass="btn btn-info btn-sm btn-custom" Text="Detay" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
    <!-- Bootstrap JS -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
