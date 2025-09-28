<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Maint_MasterSKU.aspx.cs" Inherits="BW_WebApp.Maintenance.Maint_MasterSKU" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="cMC" runat="server"> 
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <%--<asp:HiddenField ID="hdnSelectedClientID" runat="server" Value="xxxxxxx" />--%>

            

            <asp:Panel ID="pnlMainView" runat="server">
                <h1><asp:Label ID="lblRecordTitle" runat="server" Text="Maintenance Master SKU" /></h1>
                <h5 class="text-muted word-break"><asp:Label ID="lblRecordSubtitle" runat="server"/></h5>

                <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
                <asp:Button ID="btnEdit" runat="server" Text="Edit" Visible="False" OnClick="btnEdit_Click" />
                <asp:Button ID="btnDelete" runat="server" Text="Delete" Visible="False" OnClick="btnDelete_Click" />
                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Continue with Delete?"
                    Enabled="True" TargetControlID="btnDelete" />
                
                <asp:Panel ID="pnlMainGrid" runat="server" ScrollBars="Auto">

                    <asp:GridView ID="MainGrid" CssClass="table" runat="server" AutoGenerateSelectButton="True" DataKeyNames="MasterSKUID" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="MasterSKUID" HeaderText="ID" ReadOnly="True" Visible="false" />
                            <asp:BoundField DataField="Status" HeaderText="Status" ReadOnly="True" />
                            <asp:BoundField DataField="SKU" HeaderText="SKU" ReadOnly="True" />
                            <asp:BoundField DataField="Carrier" HeaderText="Carrier" ReadOnly="True" />
                            <asp:BoundField DataField="Manufacturer" HeaderText="Manufacturer" ReadOnly="True" />
                            <asp:BoundField DataField="Model" HeaderText="Model" ReadOnly="True" />
                            <asp:BoundField DataField="Colour" HeaderText="Colour" ReadOnly="True" />

                            <%--<asp:BoundField DataField="ManufacturerID" HeaderText="ManufacturerID" ReadOnly="True" />
                            <asp:BoundField DataField="ModelID" HeaderText="ModelID" ReadOnly="True" />
                            <asp:BoundField DataField="CarrierID" HeaderText="CarrierID" ReadOnly="True" />
                            <asp:BoundField DataField="ColourID" HeaderText="ColourID" ReadOnly="True" />--%>

                            <asp:BoundField DataField="CreateDate" HeaderText="CreateDate" ReadOnly="True" />
                            <asp:BoundField DataField="CreateUser" HeaderText="CreateUser" ReadOnly="True" />
                            <asp:BoundField DataField="LastUpdateDate" HeaderText="LastUpdateDate" ReadOnly="True" />
                            <asp:BoundField DataField="LastUpdateUser" HeaderText="LastUpdateUser" ReadOnly="True" />
                        </Columns>
                    </asp:GridView>

                </asp:Panel>
            </asp:Panel>

            <asp:Panel ID="pnlAdd" CssClass="w-md-50" runat="server">
                <h1>Add SKU</h1>

                <label>Status:</label>
                <asp:DropDownList ID="drpAddStatus" runat="server" />

                <label>SKU:</label>
                <asp:Label ID="AddSKU" CssClass="form-control" runat="server" />

                <label>Carrier:</label>
                <asp:DropDownList ID="drpAddCarrier" runat="server" AutoPostBack="True" />

                <label>Manufacturer:</label>
                <asp:DropDownList ID="drpAddManufacturer" runat="server" AutoPostBack="True" />

                <label>Model:</label>
                <asp:DropDownList ID="drpAddModel" runat="server" AutoPostBack="True" />

                <label>Colour:</label>
                <asp:DropDownList ID="drpAddColour" runat="server" AutoPostBack="True" />

                <label>Message:</label>
                <asp:Label ID="AddMessage" CssClass="form-control" runat="server" />

                <asp:Button ID="AddVerify" runat="server" Text="Verify" OnClick="AddVerify_Click" />
                <asp:Button ID="AddOK" runat="server" Text="OK" OnClick="AddOK_Click" Enabled="False" />
                <asp:Button ID="AddCancel" runat="server" Text="Cancel" OnClick="AddCancel_Click1" />
            </asp:Panel>

            <asp:Panel ID="pnlEdit" runat="server">
                <h1>Edit SKU</h1>

                <h5 class="text-muted word-break"><asp:Label ID="EditOriginal" runat="server" /></h5>

                <div class="w-md-50">
                    <label>Status:</label>
                    <asp:DropDownList ID="drpEditStatus" runat="server" />

                    <label>Original SKU:</label>
                    <asp:Label ID="EditSKU" CssClass="form-control" runat="server" Text="Label" />

                    <label>New SKU:</label>
                    <asp:Label ID="EditNewSKU" CssClass="form-control" runat="server" Text="Label" />

                    <label>Carrier:</label>
                    <asp:DropDownList ID="drpEditCarrier" runat="server" AutoPostBack="True" ClientIDMode="Static" />

                    <label>Manufacturer:</label>
                    <asp:DropDownList ID="drpEditManufacturer" runat="server" AutoPostBack="True" ClientIDMode="Static" />

                    <label>Model:</label>
                    <asp:DropDownList ID="drpEditModel" runat="server" AutoPostBack="True" ClientIDMode="Static" />

                    <label>Colour:</label>
                    <asp:DropDownList ID="drpEditColour" runat="server" AutoPostBack="True" ClientIDMode="Static" />

                    <label>Message:</label>
                    <asp:Label ID="EditMessage" CssClass="form-control" runat="server" />
                </div>

                <asp:Button ID="EditVerify" runat="server" Text="Verify" OnClick="EditVerify_Click" />
                <asp:Button ID="EditOK" runat="server" Text="OK" OnClick="EditOK_Click" />
                <asp:Button ID="EditCancel" runat="server" Text="Cancel" OnClick="EditCancel_Click" />
                
            </asp:Panel>

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
    </script>
</asp:Content>

