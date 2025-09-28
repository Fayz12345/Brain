<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReceiveDetail_XBinLocation.aspx.cs" Inherits="BW_WebApp.ReceiveDetail_XBinLocation" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="cMC" runat="server">
    <asp:HiddenField ID="hdnUserName" runat="server" ClientIDMode="Static" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            
            <h1>Bulk Location Update</h1>
            <div class="row">
            	<div class="col-md-6">
                    <label>Bin Number:</label>
                    <asp:TextBox ID="lblBinNumber" runat="server" ToolTip="Bin Number for which the Location number is updated." />
                    <asp:Label ID="lblMessage" runat="server" />

                    <label>ESN/IMEI:</label>
                    <asp:TextBox ID="lblESN" runat="server" ToolTip="ESN/IMEI Number for which the Location number is updated." />

                    <label>Set Location To:</label>
                    <asp:DropDownList ID="drpLocationList" runat="server" ToolTip="Location Number to assign units in bin." />
            
                    <asp:Button ID="btnUpdateLocation" runat="server" Text="Update Locations" />
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
                //                ConfigureWaitingPopup(Popup);
                $('#loading').show();
            }
        }

        function EndRequestHandler(sender, args) {

            $('#loading').hide();
        }

        // Variable Functions ----------------------------------
        function UserName() {
            return $get("<%= hdnUserName.ClientID %>").value;
        }

    </script>
</asp:Content>


