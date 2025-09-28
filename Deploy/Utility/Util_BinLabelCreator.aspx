<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Util_BinLabelCreator.aspx.cs" Inherits="BW_WebApp.Utility.Util_BinLabelCreator" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cMC" runat="server">
    <h1>Bin Label Creator</h1>
    <div class="row">
        <div class="col-md-6">
            <label>Project:</label>
            <asp:DropDownList ID="drpClientList_New" runat="server" ToolTip="Project" AutoPostBack="True" />
        </div>
        <div class="col-md-6">
            <label>Number of Labels:</label>
            <asp:RadioButtonList ID="rdlNumberToPrint" CssClass="radiolist-inline" runat="server" RepeatLayout="UnorderedList" />
        </div>
    </div>
        <asp:Button ID="btnPrintLabels" runat="server" Text="Generate Label(s)" />

</asp:Content>

<asp:Content ContentPlaceHolderID="js" runat="server">
    <script type="text/javascript">
        function OpenBinTag(Qty, Seed, Client) {
            var xDataList = {};
            xDataList["SEED"] = Seed;
            xDataList["QTY"] = Qty;
            xDataList["CLIENT"] = Client;

            var pstring = GetParameterStream(xDataList);
            var WindowToOpen = "../BinLabel.aspx";
            if (pstring.length > 0) {
                WindowToOpen = WindowToOpen + "?" + pstring
            }
            var win = window.open(WindowToOpen, "_blank", "menubar", true);
            // win.focus();
        }
    </script>
</asp:Content>

