<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PMI_Integration.aspx.cs" Inherits="BW_WebApp.Maintenance.PMI_Integration" %>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="cMC" runat="server">
    <asp:Button ID="btnFile01" runat="server" Text="GenerateFile01"/>
    <asp:Button ID="btnReadFile01" runat="server" Text="ReadFile01" />
</asp:Content>
<asp:Content ContentPlaceHolderID="js" runat="server">
    <script type="text/javascript">
        function ShowLogReport(fName) {
            var xDataList = {};
            xDataList['RPT'] = 'PMIFILE01';
            xDataList['FileName'] = fName;
            var pstring = GetParameterStream(xDataList);
            var WindowToOpen = '/Reports/RPT_EXCEL_Out.aspx';
            if (pstring.length > 0) { WindowToOpen = WindowToOpen + '?' + pstring }
            var win = window.open(WindowToOpen, '_blank', 'menubar', true);
            return;
        }
    </script>
</asp:Content>
