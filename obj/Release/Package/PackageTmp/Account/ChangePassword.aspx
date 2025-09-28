<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="BM_WebApp.Account.ChangePassword" %>
<asp:Content runat="server" ContentPlaceHolderID="HeadContent"></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="cMC">

    <h1>Change Password</h1>
    <p>Use the form below to change your password. New passwords are required to be a minimum of <%= Membership.MinRequiredPasswordLength %> characters in length.</p>

    <asp:ChangePassword ID="ChangeUserPassword" runat="server" CancelDestinationPageUrl="~/" EnableViewState="false" RenderOuterTable="false" 
         SuccessPageUrl="ChangePasswordSuccess.aspx">
        <ChangePasswordTemplate>
            <span class="failureNotification">
                <asp:Literal ID="FailureText" runat="server" />
            </span>
            <asp:ValidationSummary ID="ChangeUserPasswordValidationSummary" runat="server" CssClass="failureNotification" 
                 ValidationGroup="ChangeUserPasswordValidationGroup"/>
                
            <div class="row">
                <div class="col-md-6">
                    <asp:Label ID="CurrentPasswordLabel" runat="server" AssociatedControlID="CurrentPassword">Old Password:</asp:Label>
                    <asp:TextBox ID="CurrentPassword" runat="server" TextMode="Password" />
                    <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" ControlToValidate="CurrentPassword" 
                            ErrorMessage="Password is required." ToolTip="Old Password is required." 
                            ValidationGroup="ChangeUserPasswordValidationGroup" />

                    <asp:Label ID="NewPasswordLabel" runat="server" AssociatedControlID="NewPassword">New Password:</asp:Label>
                    <asp:TextBox ID="NewPassword" runat="server" TextMode="Password" />
                    <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword" 
                            ErrorMessage="New Password is required." ToolTip="New Password is required." 
                            ValidationGroup="ChangeUserPasswordValidationGroup" />

                    <asp:Label ID="ConfirmNewPasswordLabel" runat="server" AssociatedControlID="ConfirmNewPassword">Confirm New Password:</asp:Label>
                    <asp:TextBox ID="ConfirmNewPassword" runat="server" TextMode="Password" />
                    <asp:RequiredFieldValidator hidden ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="ConfirmNewPassword" 
                            Display="Dynamic" ErrorMessage="Confirm New Password is required."
                            ToolTip="Confirm New Password is required." ValidationGroup="ChangeUserPasswordValidationGroup" />
                    <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword" 
                            CssClass="failureNotification" Display="Dynamic" ErrorMessage="The Confirm New Password must match the New Password entry."
                            ValidationGroup="ChangeUserPasswordValidationGroup" />

                    <asp:Button ID="CancelPushButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"/>
                    <asp:Button ID="ChangePasswordPushButton" runat="server" CommandName="ChangePassword" Text="Change Password" 
                            ValidationGroup="ChangeUserPasswordValidationGroup"/>
                </div>
            </div>
        </ChangePasswordTemplate>
    </asp:ChangePassword>
</asp:Content>

