<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChangePin.aspx.cs" Inherits="BM_WebApp.Account.ChangePin" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content runat="server" ContentPlaceHolderID="HeadContent"></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="cMC">
    <h1>Change PIN for: <span class="text-muted"><asp:Label ID="lblUserName" runat="server" /></span></h1>
    <p>Use the form below to change your PIN.</p>
    <div class="row">
        <div class="col-md-6">
            <asp:Label ID="CurrentPasswordLabel" runat="server" AssociatedControlID="CurrentPassword">Old PIN:</asp:Label>
            <asp:TextBox ID="CurrentPassword" runat="server" TextMode="Password" MaxLength="8" />

            <asp:Label ID="NewPasswordLabel" runat="server" AssociatedControlID="NewPassword">New PIN:</asp:Label>
            <asp:TextBox ID="NewPassword" runat="server" TextMode="Password" MaxLength="8" />

            <asp:Label ID="ConfirmNewPasswordLabel" runat="server" AssociatedControlID="ConfirmNewPassword">Confirm New PIN:</asp:Label>
            <asp:TextBox ID="ConfirmNewPassword" runat="server" TextMode="Password" MaxLength="8" />

            <%--<asp:Button ID="CancelPushButton" runat="server" CausesValidation="False" CommandName="CancelPin" Text="Cancel"/>--%>
            <asp:Button ID="ChangePasswordPushButton" runat="server" CommandName="ChangePin" Text="Change PIN" />

            <asp:Label ID="lblStatus" CssClass="d-block" runat="server" />
        </div>
    </div>
</asp:Content>

