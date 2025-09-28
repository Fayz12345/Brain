<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="BW_WebApp.Account.Login" %>
<asp:Content runat="server" ContentPlaceHolderID="HeadContent"></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="cMC">
    <%--<asp:Label ID="lblCompanyName" runat="server" Text="Label"></asp:Label>--%>
    <div class="card">
  	    <div class="card-body">
            <h1>Log In</h1>
   
            <asp:Label ID="Label1" CssClass="d-block" runat="server" Text="Please enter your username and password."/>
            <%--<asp:HyperLink ID="RegisterHyperLink" runat="server" EnableViewState="false" Visible="False">Register</asp:HyperLink>--%> 
            <%--<asp:Label ID="lblSupportEmail" CssClass="small text-muted d-block" runat="server" Text="Label"/>--%>

            <asp:Login ID="LoginUser" runat="server" EnableViewState="false" RenderOuterTable="false">
                <LayoutTemplate>
                    <span class="failureNotification">
                        <asp:Literal ID="FailureText" runat="server" />
                    </span>
                    <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification" ValidationGroup="LoginUserValidationGroup"/>

                    <div class="row">
                        <div class="col-md">
                            <label><asp:Label ID="UserNameLabel" runat="server">Username:</asp:Label></label>
                            <asp:TextBox ID="UserName" runat="server" />
                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="LoginUserValidationGroup" />

                            <label><asp:Label ID="PasswordLabel" runat="server">Password:</asp:Label></label>
                            <asp:TextBox ID="Password" runat="server" TextMode="Password" />
                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="LoginUserValidationGroup" />

                            <asp:CheckBox ID="RememberMe" CssClass="d-block mb-1" runat="server" Text="Keep me logged in" ViewStateMode="Inherit" />

                            <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" ValidationGroup="LoginUserValidationGroup"/>

                            <hr class="d-md-none">
                        </div>

                        <%--JIM: Set this up. The default PasswordRecovery control was un-workable on my part--%>
                        <div class="col-md">
                            <label>Forgot your password? Enter your username below to recover it.</label>
                            <label><asp:Label ID="RecoverPasswordLabel" runat="server">Username:</asp:Label></label>
                            <asp:TextBox ID="RecoverPassword" runat="server" />

                            <asp:Button ID="RecoverPasswordButton" runat="server" Text="Recover Password"/>
                        </div>
                    </div>
                </LayoutTemplate>
            </asp:Login>
        </div>
    </div>
</asp:Content>
