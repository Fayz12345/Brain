<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IDC_LocationUpdate.aspx.cs" Inherits="BW_WebApp.IDC_LocationUpdate" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="cMC" runat="server">
    <asp:HiddenField ID="hdnUserName" runat="server" ClientIDMode="Static" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            
            <h1>Inventory Distribution</h1>
            <div class="row">
            	<div class="col-md-6">
                    <label>ESN/IMEI:</label>
                    <asp:TextBox ID="lblESN" runat="server" ToolTip="ESN/IMEI Number for which the Location number is updated." />

                    <label>Bin Number:</label>
                    <asp:TextBox ID="lblBinNumber" runat="server" ToolTip="Bin Number for which the Location number is updated." />

                    <label>Location:</label>
                    <asp:TextBox ID="txtLocation" runat="server" ToolTip="Location." />
            
                    <asp:Button ID="btnUpdateLocation" runat="server" Text="Update"  UseSubmitBehavior="False"/>
                </div>
            </div>
            <asp:Label ID="lblMessage" runat="server" />
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


