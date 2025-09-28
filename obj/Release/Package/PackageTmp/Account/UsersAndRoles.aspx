<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UsersAndRoles.aspx.cs" Inherits="BW_WebApp.Account.UsersAndRoles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="cMC" runat="server"> 

    <h2>Add Roles to User</h2>
        
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

          

        <label class="d-block">Select a User:</label>
        <asp:DropDownList ID="UserList" CssClass="w-md-50" runat="server" AutoPostBack="True" DataTextField="UserName" DataValueField="UserName" />
        <asp:Button ID="btnlock" runat="server" Text="Lock User" />
        <asp:Button ID="btnUnlock" runat="server" Text="Unlock User" />
        <asp:Label ID="lblLockedDate" CssClass="text-warning" runat="server" />

        <!-- TODO: hide if empty -->
        <%--<div id="locked-date" class="alert alert-warning">
             <asp:Label ID="lblLockedDate" runat="server" />
        </div>--%>

        <%--<div id="action-status" class="alert alert-info">
            <asp:Label ID="ActionStatus" runat="server" />
        </div>--%>

        <asp:Label ID="ActionStatus" CssClass="d-block text-info mb-2" runat="server" />

        <div class="card p-2 mb-3">
            <div class="checklist-inline">
                <asp:Repeater ID="UsersRoleList" runat="server">
                    <ItemTemplate>
                        <asp:CheckBox ID="RoleCheckBox" CssClass="checkbox" runat="server" AutoPostBack="true" Text="<%# Container.DataItem %>" OnCheckedChanged="RoleCheckBox_CheckChanged" />
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>

        <h2>Manage Users By Role</h2>
        <div class="row">
        	<div class="col-sm">
                <label>Select a Role:</label>
                <asp:DropDownList ID="RoleList" runat="server" AutoPostBack="True" />

                <label>Add User to Role:</label>
                <div class="input-group">
                    <asp:TextBox ID="UserNameToAddToRole" runat="server" placeholder="Username" />
                    <div class="input-group-append">
                        <asp:Button ID="AddUserToRoleButton" runat="server" Text="Add" />
                    </div>
                </div>

                
                
            </div>

        	<div class="col-sm">
                <asp:GridView ID="RolesUserList" CssClass="table w-auto" runat="server" AutoGenerateColumns="false" EmptyDataText="No users belong to this role.">
                    <Columns>
                        <asp:CommandField DeleteText="Remove" ShowDeleteButton="true" />
                        <asp:TemplateField HeaderText="Roles">
                            <ItemTemplate>
                                <asp:Label ID="UserNameLabel" runat="server" Text="<%# Container.DataItem %>" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
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
                //ConfigureWaitingPopup(Popup);
                $('#loading').show();
            }
        }

        function EndRequestHandler(sender, args) {

            $('#loading').hide();
        }

        $('#action-status span:empty').hide();

    </script>
</asp:Content>
