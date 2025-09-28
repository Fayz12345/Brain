<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RPT_SpotCountReport.aspx.cs" Inherits="BW_WebApp.Reports.RPT_SpotCountReport" %>


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
               <asp:TableCell ColumnSpan="4" HorizontalAlign="Center" Font-Size="X-Large">
                    <br />
                    Spot Count Report
                    <br />
               </asp:TableCell>
           </asp:TableRow>
           <asp:TableRow>
               <asp:TableCell HorizontalAlign="Left" Font-Size="Large">
                     <asp:Label ID="lblBinNumber" runat="server" Text="Bin Number:XXXXX"></asp:Label>
                    <br />
               </asp:TableCell>
               <asp:TableCell HorizontalAlign="Left" Font-Size="Large">
                     <asp:Label ID="lblRunDate" runat="server" Text="Run Date:XXXXX"></asp:Label>
                    <br />
               </asp:TableCell>
               <asp:TableCell HorizontalAlign="Left" Font-Size="Large">
                     <asp:Label ID="lblProject" runat="server" Text="Project:XXXXX"></asp:Label>
                    <br />
               </asp:TableCell>
               <asp:TableCell HorizontalAlign="Left" Font-Size="Large">
                     <asp:Label ID="lblTotal" runat="server" Text="Total:XXXXX"></asp:Label>
                    <br />
               </asp:TableCell>
           </asp:TableRow>
           

           <asp:TableRow>
               <asp:TableCell ColumnSpan="4" Width="100%">


<asp:GridView runat="server" ID="BinData">
           </asp:GridView>


               </asp:TableCell>
           </asp:TableRow>


       </asp:Table>








    </div>
    </form>
</body>
</html>

