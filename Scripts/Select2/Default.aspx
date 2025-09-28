<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <link href="Select/select2.css" rel="stylesheet" />
    <script src="Select/select2.js"></script>

    <style>
        body
        {
            font: 11px verdana;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:DropDownList ID="myDropDownlistID" Width="300px" runat="server">
                <asp:ListItem Text="Select Color"></asp:ListItem>
                <asp:ListItem Text="Red"></asp:ListItem>
                <asp:ListItem Text="Green" />
                <asp:ListItem Text="Blue" />
                <asp:ListItem Text="Pink" />
                <asp:ListItem Text="Yellow" />
                <asp:ListItem Text="Lime" />
                <asp:ListItem Text="Black" />
                <asp:ListItem Text="Purple" />
                <asp:ListItem Text="Deep Pink" />
                <asp:ListItem Text="Orange" />
                <asp:ListItem Text="Light Pink" />
            </asp:DropDownList>
        </div>

        <script>
            $(document).ready(function () { $("#myDropDownlistID").select2(); });
        </script>
    </form>
</body>
</html>
