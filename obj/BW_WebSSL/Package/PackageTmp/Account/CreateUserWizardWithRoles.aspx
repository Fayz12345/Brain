<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateUserWizardWithRoles.aspx.cs" Inherits="BW_WebApp.Account.CreateUserWizardWithRoles" %>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="cMC" runat="server">
    <h1>Add New User</h1>

    <asp:CreateUserWizard ID="RegisterUserWithRoles" runat="server" ContinueDestinationPageUrl="~/Account/CreateUserWizardWithRoles.aspx"
        LoginCreatedUser="False" ActiveStepIndex="0">
        <LayoutTemplate>
            <asp:PlaceHolder ID="headerPlaceHolder" runat="server" />
            <asp:PlaceHolder ID="sideBarPlaceHolder" runat="server" />
            <asp:PlaceHolder ID="WizardStepPlaceHolder" runat="server" />
            <asp:PlaceHolder ID="navigationPlaceHolder" runat="server" />
        </LayoutTemplate>
        <StartNavigationTemplate>
            <asp:Button runat="server" CommandName="MoveNext" Text="Next" />
        </StartNavigationTemplate>
        <StepNavigationTemplate>
            <asp:Button runat="server" CommandName="MoveNext" Text="Next" />
        </StepNavigationTemplate>
        <FinishNavigationTemplate>
            <asp:Button runat="server" CommandName="MoveFinish" Text="Next" />
        </FinishNavigationTemplate>
        <WizardSteps>
            <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server">
                <ContentTemplate>
                    <asp:Label runat="server" AssociatedControlID="UserName">User Name:</asp:Label>
                    <asp:TextBox ID="UserName" CssClass="w-md-50" runat="server" />
                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                        ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>

                    <asp:Label runat="server" AssociatedControlID="Password">Password:</asp:Label>
                    <asp:TextBox ID="Password" CssClass="w-md-50" runat="server" TextMode="Password" />
                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                        ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>

                    <asp:Label runat="server" AssociatedControlID="ConfirmPassword">Confirm Password:</asp:Label>
                    <asp:TextBox ID="ConfirmPassword" CssClass="w-md-50" runat="server" TextMode="Password" />
                    <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword"
                        ErrorMessage="Confirm Password is required." ToolTip="Confirm Password is required."
                        ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>

                    <asp:Label runat="server" AssociatedControlID="Email">Email:</asp:Label>
                    <asp:TextBox ID="Email" CssClass="w-md-50" runat="server" />
                    <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email"
                        ErrorMessage="E-mail is required." ToolTip="E-mail is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>

                    <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                        Display="Dynamic" ErrorMessage="The Password and Confirmation Password must match." ValidationGroup="CreateUserWizard1" />

                    <div><asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False" /></div>
                </ContentTemplate>

                <CustomNavigationTemplate>
                    <asp:Button ID="RegistrationButton" ValidationGroup="CreateUserWizardStep1" CommandName="MoveNext" runat="server" Text="Create User" />
                </CustomNavigationTemplate>
            </asp:CreateUserWizardStep>

            <asp:WizardStep ID="WizardStep2" runat="server" StepType="Step" Title="Client Location Restrictions" AllowReturn="false">
                <asp:Label runat="server" Text="Department:" AssociatedControlID="txtDepartment" />
                <asp:TextBox ID="txtDepartment" CssClass="w-md-50" runat="server" MaxLength="50" />

                <asp:Label runat="server" Text="Friendly Name:" AssociatedControlID="txtFriendlyName" />
                <asp:TextBox ID="txtFriendlyName" CssClass="w-md-50" runat="server" MaxLength="100"/>

                <asp:Label runat="server" Text="Location Scancodes (e.g. A0001, A0002, A0003):"
                    AssociatedControlID="txtLocationList" />
                <label class="text-muted">Note: Leave Blank to allow access to all client locations</label>
                <asp:TextBox ID="txtLocationList" CssClass="w-md-50" runat="server" />
            </asp:WizardStep>

            <asp:WizardStep ID="SpecifyRolesStep" runat="server" StepType="Step" Title="Specify Roles" AllowReturn="false">
                <asp:CheckBoxList ID="RoleList" CssClass="checklist-inline" runat="server" RepeatLayout="UnorderedList" />
            </asp:WizardStep>

            <asp:CompleteWizardStep ID="CompleteWizardStep1" runat="server">
                <ContentTemplate>
                    <p>Your account has been successfully created.</p>
                    <asp:Button ID="ContinueButton" runat="server" CausesValidation="False" CommandName="Continue" Text="Continue" 
                        ValidationGroup="RegisterUserWithRoles" />
                </ContentTemplate>
            </asp:CompleteWizardStep>
        </WizardSteps>
    </asp:CreateUserWizard>
</asp:Content>

