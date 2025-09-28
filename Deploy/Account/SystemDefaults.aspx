<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SystemDefaults.aspx.cs" Inherits="BW_WebApp.Account.SystemDefaults" %>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="cMC" runat="server">
    <asp:Panel ID="pnlEdit" runat="server">
        <h1>Company Setup</h1>

        <div class="row">
        	<div class="col-md-6">
                <label>Name:</label>
                <asp:TextBox ID="EditName" runat="server" MaxLength="50" />
                <label>Address Line 1:</label>
                <asp:TextBox ID="EditAddressLine1" runat="server" MaxLength="50" />
                <label>Address Line 2:</label>
                <asp:TextBox ID="EditAddressLine2" runat="server" MaxLength="50" />
                <label>City:</label>
                <asp:TextBox ID="EditCity" runat="server" MaxLength="50" />
                <label>Province:</label>
                <asp:TextBox ID="EditProvince" runat="server" MaxLength="50" />
                <label>Postal Code:</label>
                <asp:TextBox ID="EditPostal" runat="server" MaxLength="50" />
                <label>Phone:</label>
                <asp:TextBox ID="EditPhone" runat="server" MaxLength="50" />
                <label>Fax:</label>
                <asp:TextBox ID="EditFax" runat="server" MaxLength="50" />
                <label>Website:</label>
                <asp:TextBox ID="EditWebsite" runat="server" MaxLength="50" />
            </div>
        	<div class="col-md-6">
                <label>Welcome Message:</label>
                <asp:TextBox ID="EditWelcome" runat="server" MaxLength="50" />
                <label>Support Email:</label>
                <asp:TextBox ID="EditSupportEmail" runat="server" MaxLength="50" />
                <label>Part Request email:</label>
                <asp:TextBox ID="EditPartRequestEmail" runat="server" MaxLength="50" />
                <label>Part Return email:</label>
                <asp:TextBox ID="EditPartReturnEmail" runat="server" MaxLength="50" />
                <asp:CheckBox ID="EditCheckDeleteMasterOK" runat="server" Text="Allow System to Delete from Master Tables" />
            </div>
        </div>
        
        <asp:Button ID="EditOK" runat="server" Text="Save" />
        <%--<asp:Button ID="EditCancel" runat="server" Text="Cancel" />--%>
    </asp:Panel>
</asp:Content>


