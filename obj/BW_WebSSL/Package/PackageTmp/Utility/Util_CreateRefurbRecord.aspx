<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Util_CreateRefurbRecord.aspx.cs" Inherits="BW_WebApp.Utility.Util_CreateRefurbRecord" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>
<%@ Register Assembly="Syncfusion.Tools.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89"  Namespace="Syncfusion.Web.UI.WebControls.Tools" TagPrefix="syncfusion" %>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="cMC" runat="server">
    <asp:HiddenField ID="hdnUserName" runat="server" ClientIDMode="Static" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            
            <h1><asp:Label ID="lblRecordTitle" runat="server" Text="Create Refurb Record" /></h1>
            <asp:Label ID="lblWarningMessage" runat="server" />

            <div class="row">
            	<div class="col-md-6">
                    <%--<label>Project:</label>
                        <asp:DropDownList ID="drpProjectList" runat="server" ToolTip="Project" />--%>
            
                    <label>IMEI/ESN:</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtESN" runat="server" />
                        <div class="input-group-append">
                            <asp:Button ID="btnProcess" runat="server" Text="Create" />
                        </div>
                    </div>
                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnProcess"
                        ConfirmText="This utility will roll the existing 000 record in 'Harvest', and create a new record in 'Build For Sale'. Continue?" />
            
                    <asp:ListBox ID="lstHistory" runat="server" SelectionMode="Single" ViewStateMode="Inherit" ClientIDMode="Static" />
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ContentPlaceHolderID="js" runat="server">
    <script type="text/javascript">

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

        function BeginRequestHandler(sender, args) {
            if (args._postBackElement.id != "SkinTable1") {
                // ConfigureWaitingPopup(Popup);
                $('#loading').show();
            }
        }

        function EndRequestHandler(sender, args) {
            $('#loading').hide();
        }

        function OpenBagTag(RDID) {
            var xDataList = {};
            xDataList["RPT"] = "Bagtag";
            xDataList["RDID"] = RDID;
            xDataList["ISTHREAD"] = "N";
            var pstring = GetParameterStream(xDataList);
            var WindowToOpen = "BagTag.aspx";
            if (pstring.length > 0) {
                WindowToOpen = WindowToOpen + "?" + pstring
            }
            var win = window.open(WindowToOpen, "_blank", "menubar", true);
            // win.focus();
        }

        function GetParameterStream(ParmameterList) {
            var count = 0;
            var sb = new Sys.StringBuilder();
            for (var property in ParmameterList) {
                if (count > 0) { sb.append("&"); }
                sb.append(property + "=" + ParmameterList[property]);
                count += 1;
            }
            return sb.toString();
        }
    </script>
</asp:Content>

