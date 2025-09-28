<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageRoles.aspx.cs" Inherits="BW_WebApp.Account.ManageRoles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajct" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="cMC" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdn_Role" Value="" runat="server" />
            <h1>
                Role Management</h1>
            <div class="row">
                <div class="col-lg-5 col-xl-4">
                    <label>
                        Create a New Role:</label>
                    <div class="input-group w-sm-50 w-lg-100">
                        <asp:TextBox ID="RoleName" runat="server" />
                        <div class="input-group-append">
                            <asp:Button ID="CreateRoleButton" runat="server" Text="Create Role" />
                        </div>
                    </div>
                    <div class="overflow">
                        <asp:GridView ID="RoleList" CssClass="table table-nonfluid" runat="server" AutoGenerateColumns="False">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" CommandArgument="<%# Container.DataItem.ToString()  %>">Delete</asp:LinkButton>
                                        <ajct:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" TargetControlID="btnDelete"
                                            ConfirmText="Are you sure you want to Delete this file?" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Roles">
                                    <ItemTemplate>
                                        <asp:Label ID="RoleNameLabel" runat="server" Text="<%# Container.DataItem %>" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DataAccess">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbtnIsolate" runat="server" CommandName="Isolate" CommandArgument="<%# Container.DataItem.ToString()  %>">Isolate</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <hr class="d-lg-none">
                </div>
                <div class="col-lg-7 col-xl-8">
                    <ajct:TabContainer ID="UserAccessTabs" CssClass="tab-container" runat="server" Visible="False">
                        <ajct:TabPanel ID="TabProject" CssClass="tab-panel" runat="server" HeaderText="Project">
                            <ContentTemplate>
                                <h3>
                                    <asp:Label ID="lblUserName_Project" runat="server" /></h3>
                                <div class="card mb-2">
                                    <asp:GridView ID="gvProject" CssClass="table" runat="server" DataKeyField="ID" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" Visible="false" />
                                            <asp:BoundField DataField="Name" HeaderText="Name" />
                                            <asp:TemplateField HeaderText="Allow Add">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnIDFieldp" runat="server" Value='<%# Eval("ID") %>' />
                                                    <asp:CheckBox ID="cbAddp" runat="server" Checked='<%# Eval("AllowAdd") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Select">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cbSelectp" runat="server" Checked='<%# Eval("AllowSelect") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Update">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cbUpdatep" runat="server" Checked='<%# Eval("AllowUpdate") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cbDeletep" runat="server" Checked='<%# Eval("AllowDelete") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <asp:Button ID="btnProject" runat="server" Text="Save Role Project Access" />
                            </ContentTemplate>
                        </ajct:TabPanel>
                        <ajct:TabPanel ID="TabProcess" CssClass="tab-panel" runat="server" HeaderText="Process">
                            <ContentTemplate>
                                <h3>
                                    <asp:Label ID="lblUserName_Process" runat="server" /></h3>
                                <asp:DropDownList ID="drpProjectList" CssClass="w-md-50" runat="server" ToolTip="Project"
                                    AutoPostBack="True" />
                                <div class="card mb-2">
                                    <asp:GridView ID="gvProcess" CssClass="table" runat="server" DataKeyField="ID" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" Visible="false" />
                                            <asp:BoundField DataField="Name" HeaderText="Name" />
                                            <asp:TemplateField HeaderText="Allow Add">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnIDFieldp" runat="server" Value='<%# Eval("ID") %>' />
                                                    <asp:CheckBox ID="cbAddp" runat="server" Checked='<%# Eval("AllowAdd") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Select">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cbSelectp" runat="server" Checked='<%# Eval("AllowSelect") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Update">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cbUpdatep" runat="server" Checked='<%# Eval("AllowUpdate") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cbDeletep" runat="server" Checked='<%# Eval("AllowDelete") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <asp:Button ID="btnProcess" runat="server" Text="Save Role Process Access" />
                            </ContentTemplate>
                        </ajct:TabPanel>
                        <ajct:TabPanel ID="tabClient" CssClass="tab-panel" runat="server" HeaderText="Client">
                            <ContentTemplate>
                                <%--<asp:HiddenField ID="hdnUserName_Client" runat="server" />--%>
                                <h3>
                                    <asp:Label ID="lblUserName_Client" runat="server" /></h3>
                                <asp:Button ID="btnClient" runat="server" Text="Save Role Client Access" />
                                <div class="card mb-2">
                                    <asp:GridView ID="gvClient" CssClass="table" runat="server" DataKeyField="ID" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" Visible="false" />
                                            <asp:BoundField DataField="Name" HeaderText="Name" />
                                            <asp:TemplateField HeaderText="Allow Add">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnIDFieldc" runat="server" Value='<%# Eval("ID") %>' />
                                                    <asp:CheckBox ID="cbAddc" runat="server" Checked='<%# Eval("AllowAdd") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Select">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cbSelectc" runat="server" Checked='<%# Eval("AllowSelect") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Update">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cbUpdatec" runat="server" Checked='<%# Eval("AllowUpdate") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cbDeletec" runat="server" Checked='<%# Eval("AllowDelete") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </ContentTemplate>
                        </ajct:TabPanel>
                        <ajct:TabPanel ID="TabQuestion" CssClass="tab-panel" runat="server" HeaderText="Question">
                            <ContentTemplate>
                                <%--<asp:HiddenField ID="hdnUserName_Question" runat="server" />--%>
                                <h3>
                                    <asp:Label ID="lblUserName_Question" runat="server" /></h3>
                                <asp:Button ID="btnQuestion" runat="server" Text="Save Role Question Access" />
                                <asp:GridView ID="gvQuestion" CssClass="table" runat="server" DataKeyField="ID" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" Visible="false" />
                                        <asp:BoundField DataField="Name" HeaderText="Name" />
                                        <asp:TemplateField HeaderText="Allow Add">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdnIDFieldc" runat="server" Value='<%# Eval("ID") %>' />
                                                <asp:CheckBox ID="cbAddc" runat="server" Checked='<%# Eval("AllowAdd") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbSelectc" runat="server" Checked='<%# Eval("AllowSelect") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Update">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbUpdatec" runat="server" Checked='<%# Eval("AllowUpdate") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbDeletec" runat="server" Checked='<%# Eval("AllowDelete") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </ajct:TabPanel>
                        <ajct:TabPanel ID="TabAnswer" CssClass="tab-panel" runat="server" HeaderText="Answer"
                            Visible="False">
                            <ContentTemplate>
                                <%--<asp:HiddenField ID="hdnUserName_Question" runat="server" />--%>
                                <h3>
                                    <asp:Label ID="lblUserName_Answer" runat="server" /></h3>
                                <asp:Button ID="btnAnswer" runat="server" Text="Save Role Answer Access" />
                                <asp:GridView ID="gvAnswer" runat="server" DataKeyField="ID" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" Visible="false" />
                                        <asp:BoundField DataField="Name" HeaderText="Name" />
                                        <asp:TemplateField HeaderText="Allow Add">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdnIDFieldc" runat="server" Value='<%# Eval("ID") %>' />
                                                <asp:CheckBox ID="cbAddc" runat="server" Checked='<%# Eval("AllowAdd") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbSelectc" runat="server" Checked='<%# Eval("AllowSelect") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Update">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbUpdatec" runat="server" Checked='<%# Eval("AllowUpdate") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbDeletec" runat="server" Checked='<%# Eval("AllowDelete") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </ajct:TabPanel>
                        <ajct:TabPanel ID="TabFunction" CssClass="tab-panel" runat="server" HeaderText="Function"
                            Visible="False">
                            <ContentTemplate>
                                <%--<asp:HiddenField ID="hdnUserName_Question" runat="server" />--%>
                                <h3>
                                    <asp:Label ID="lblUserName_Function" runat="server" /></h3>
                                <asp:Button ID="btnFunction" runat="server" Text="Save Role Function Access" />
                                <asp:GridView ID="gvFunction" CssClass="table" runat="server" DataKeyField="ID" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" Visible="false" />
                                        <asp:BoundField DataField="Name" HeaderText="Name" />
                                        <asp:TemplateField HeaderText="Allow Add">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdnIDFieldc" runat="server" Value='<%# Eval("ID") %>' />
                                                <asp:CheckBox ID="cbAddc" runat="server" Checked='<%# Eval("AllowAdd") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbSelectc" runat="server" Checked='<%# Eval("AllowSelect") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Update">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbUpdatec" runat="server" Checked='<%# Eval("AllowUpdate") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbDeletec" runat="server" Checked='<%# Eval("AllowDelete") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </ajct:TabPanel>
                        <ajct:TabPanel ID="TabMenu" CssClass="tab-panel" runat="server" HeaderText="Menu">
                            <ContentTemplate>
                                <%--<asp:HiddenField ID="hdnUserName_Question" runat="server" />--%>
                                <h3>
                                    <asp:Label ID="lblMenuName" runat="server" /></h3>
                                <asp:Button ID="btnSaveMenuRoles" runat="server" Text="Save Role Menu Access" />
                                <asp:TreeView ID="tvMenu" CssClass="tree-view" runat="Server" ExpandDepth="0" ShowLines="true"
                                    ShowExpandCollapse="true" ShowCheckBoxes="All" />
                                <%--<syncfusion:TreeView ID="TreeData" CssClass="tree-view" runat="server" OnNodeExpanded="TreeView1_NodeExpanded"
                                    ClientSideOnContextMenu="NodeOnContextMenu(this)" EditNode="False" ClientSideOnNodeSelect="NodeOnSelect(this)" />--%>
                            </ContentTemplate>
                        </ajct:TabPanel>
                    </ajct:TabContainer>
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

    </script>
</asp:Content>