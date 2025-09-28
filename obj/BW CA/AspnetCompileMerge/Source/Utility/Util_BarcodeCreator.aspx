<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Util_BarcodeCreator.aspx.cs" Inherits="BW_WebApp.Utility.Util_BarcodeCreator" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="cMC" runat="server">
    <h1>Bin Label Creator</h1>
    
    <div class="row">
    	<div class="col-md-6">
            <label>Description:</label>
            <asp:TextBox ID="txtDesctiption" runat="server" Width="100%"></asp:TextBox>
    
            <label>Text to Encode:</label>
            <asp:TextBox ID="txtToEncode" runat="server" Width="100%"></asp:TextBox>
    
            <label>Number of Labels:</label>
            <asp:RadioButtonList ID="rdlNumberToPrint" CssClass="radiolist-inline" runat="server" RepeatLayout="UnorderedList" />
    
            <asp:Button ID="btnPrintLabels" runat="server" Text="Generate Label(s)" />
        </div>
    </div>

</asp:Content>
<asp:Content ContentPlaceHolderID="js" runat="server">
    <script type="text/javascript">

        function OpenBarcodeTag(QTY, Text, CodeText) {
            var xDataList = {};
            xDataList["QTY"] = QTY;
            xDataList["TEXT"] = Text;
            xDataList["CodeText"] = CodeText;
            var pstring = GetParameterStream(xDataList);
            var WindowToOpen = "../BarcodeLabel.aspx";
            if (pstring.length > 0) {
                WindowToOpen = WindowToOpen + "?" + pstring
            }
            var win = window.open(WindowToOpen, "_blank", "menubar", true);
            // win.focus();
        }
</script>
</asp:Content>
