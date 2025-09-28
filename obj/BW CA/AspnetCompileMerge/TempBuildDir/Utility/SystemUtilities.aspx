<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SystemUtilities.aspx.cs" Inherits="BW_WebApp.Utility.SystemUtilities" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajct" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>
<%@ Register Assembly="Syncfusion.Tools.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89"  Namespace="Syncfusion.Web.UI.WebControls.Tools" TagPrefix="syncfusion" %>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="cMC" runat="server">
    <h1>System Utilities</h1>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            
            <ajct:TabContainer ID="TabContainer1" CssClass="tab-container" runat="server">
                <ajct:TabPanel ID="TabPanel1" CssClass="tab-panel" HeaderText="Menu Maintenance" runat="server">
                    <ContentTemplate>
                        <h3>Menu Maintenance - Clean Unused Role Menu Access Table</h3>
                        <p>This Utility looks at current valid roles and current valid menu options.
                        If there are any entries in the control table "RoleMenuAccess" table, 
                        for invalid roles, or invalid menu options, it will flag the directive to 0... 
                        IT will then have to remove those records from the RoleMenuAccess table.</p>
                        <asp:Button ID="btnCleanMenu" runat="server" Text="Run Clean" />
                        <asp:Button ID="btnRemove" runat="server" Text="Remove Invalid Records" />
                        <asp:Label ID="lblCleanMenuMessage" runat="server" />
                    </ContentTemplate>
                </ajct:TabPanel>

                <ajct:TabPanel ID="aStock" CssClass="tab-panel" HeaderText="Available Stock" runat="server">
                    <ContentTemplate>
                        <h3>Available Stock - Missing Units</h3>
                        <p>Some units are not showing up under Available Stock: Run this to correct a possible
                        cause.</p>
                        <p>This Utility corrects the higher level Carrier, Manufacturer, Model, Colour and
                        Grade Keys. Missing data here can be verified by running the Detail inventory report
                        and looking under the header level columns for the above data items.</p>
                        <asp:Button ID="btnUpdateHeaderLevelKeys" runat="server" Text="Run Update" />
                        <asp:Label ID="lblmessage" runat="server" />
                    </ContentTemplate>
                </ajct:TabPanel>
            </ajct:TabContainer>
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

    </script>
</asp:Content>
