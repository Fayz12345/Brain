<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RPT_UnitView.aspx.cs" Inherits="BW_WebApp.Reports.RPT_UnitView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
   <div>
       <asp:Table ID="Table1" runat="server">
           <asp:TableRow>
               <asp:TableCell HorizontalAlign="Center" Font-Size="X-Large" Width="100%">
                   <br />
                   Unit View Report:
                   <asp:Label ID="lblESN" runat="server" Text="ESN"></asp:Label>
                   <br />
                   <br />
               </asp:TableCell>
           </asp:TableRow>
           <asp:TableRow>
               <asp:TableCell Width="100%">
                   <asp:GridView runat="server" ID="UnitView">
                   </asp:GridView>
               </asp:TableCell>
           </asp:TableRow>
       </asp:Table>








    </div>
    </form>
</body>
</html>
