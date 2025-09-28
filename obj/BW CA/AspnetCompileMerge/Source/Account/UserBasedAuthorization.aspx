<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserBasedAuthorization.aspx.cs" Inherits="BW_WebApp.Account.UserBasedAuthorization" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="cMC" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <h1>
                User Management</h1>
            <div id="UserList" class="row">
                <div class="col-lg-100 col-xl-100">
                    <div class="input-group w-sm-50 w-lg-100">
                        <asp:DropDownList ID="drpUserFilter" CssClass="w-md-50 w-xl-25" runat="server" />
                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" />
                        <asp:Button ID="btnCleanUserList" runat="server" Text="Kill All Users" ToolTip="jmccomb and Admin will remain."
                            Visible="False" Enabled="False" />
                    </div>
                    <div class="card overflow">
                        <asp:GridView ID="UserGrid" runat="server" DataKeyNames="UserName" AutoGenerateColumns="false">
                            <Columns>
                                <asp:CommandField ButtonType="Link" ShowEditButton="true" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="xDelete" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">
                                    Delete
                                        </asp:LinkButton>
                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" TargetControlID="btnDelete"
                                            ConfirmText='Are you sure you want to Delete this user:<%# Bind("UserName") %> ?'>
                                        </asp:ConfirmButtonExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Password">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbtnReset" runat="server" CommandName="Reset" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">
                                    Reset</asp:LinkButton>
                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtenderReset" runat="server" TargetControlID="lnkbtnReset"
                                            ConfirmText='Are you sure you want to Reset this user:<%# Bind("UserName") %> Password?' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DataAccess">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbtnIsolate" runat="server" CommandName="Isolate" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                            Visible="True">Isolate</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="UserName" HeaderText="UserName" ReadOnly="true" />
                                <asp:TemplateField HeaderText="Department">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="DeptName" Text='<%# Bind("Department") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" ID="DepartmentName" Text='<%# Bind("Department") %>'
                                            MaxLength="50" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3x" runat="server" ControlToValidate="DepartmentName"
                                            Display="Dynamic" ErrorMessage="You must provide a Department name." SetFocusOnError="true">*</asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="LastLoginDate" HeaderText="Last Login" ReadOnly="true" />
                                <asp:BoundField DataField="ClientLocationRestrictions" HeaderText="Client Location Restrictions"
                                    ReadOnly="true" />
                                <asp:BoundField DataField="IsApproved" HeaderText="Approved" ReadOnly="true" HtmlEncode="false"
                                    DataFormatString="{0:d}" />
                                <asp:TemplateField HeaderText="Friendly Name">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="FriendlyName" Text='<%# Bind("FriendlyName") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" ID="FriendlyName" Text='<%# Bind("FriendlyName") %>' />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="FriendlyName"
                                            Display="Dynamic" ErrorMessage="You must provide friendly name." SetFocusOnError="true">*</asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Email">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="Email" Text='<%# Bind("Email") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" ID="Email" Text='<%# Bind("Email") %>' />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Email"
                                            Display="Dynamic" ErrorMessage="You must provide an email address." SetFocusOnError="true">*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="Email"
                                            Display="Dynamic" ErrorMessage="The email address you have entered is not valid. Please fix this and try again."
                                            SetFocusOnError="true" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Comment">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="Label2" Text='<%# Eval("Comment") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" ID="Comment" TextMode="MultiLine" Columns="40" Rows="4"
                                            Text='<%# Eval("Comment") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <%-- <hr class="d-lg-none">--%>
                </div>
            </div>




            <div id="IsolateList" class="row">
                <div class="col-lg-100 col-xl-100">

                    <asp:Button ID="btnBack" runat="server" Text="<<--   " tooltip="Return to User List"/>
                    <asp:Label ID="lbluserPicked" runat="server" Text="" Visible="false"></asp:Label>

                    <asp:TabContainer ID="UserAccessTabs" CssClass="tab-container" runat="server" Visible="false">
                        <asp:TabPanel ID="TabClient" CssClass="tab-panel" runat="server" HeaderText="Client">
                            <ContentTemplate>
                                <%--<asp:HiddenField ID="HiddenField1" runat="server" />--%>
                                <asp:Label ID="lblUserName_Client" runat="server" />
                                <%--<asp:Label ID="lblUserRoles_Client" runat="server" />--%>
                                <asp:Button ID="btnClient" runat="server" Text="Save User Client Access" />
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
                            </ContentTemplate>
                        </asp:TabPanel>
                        <asp:TabPanel ID="tabClientLocation" CssClass="tab-panel" runat="server" HeaderText="Client Location" Visible="false">
                            <ContentTemplate>
                                <asp:HiddenField ID="UserNameToUpdate" runat="server" />
                                <asp:Label ID="lblUserName_ClientLocation" runat="server" />
                                <asp:Label ID="Label1" runat="server" Text="Location Scancodes (e.g. A0001,A0002,A0003) Leave Blank to allow access to all client locations"
                                    AssociatedControlID="txtLocationList" />
                                <asp:TextBox ID="txtLocationList" runat="server" />
                                <asp:Button ID="btnClientLocation" runat="server" Text="Save User Client Access" />
                                <%--<asp:GridView ID="gvClientLocation" CssClass="table" runat="server" DataKeyField="ID" AutoGenerateColumns="False">
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
                        </asp:GridView>--%>
                            </ContentTemplate>
                        </asp:TabPanel>
                        <asp:TabPanel ID="TabProject" CssClass="tab-panel" runat="server" HeaderText="Project">
                            <ContentTemplate>
                                <asp:Label ID="lblUserName_Project" runat="server" />
                                <asp:Button ID="btnProject" runat="server" Text="Save User project Access" />
                                <asp:GridView ID="gvProject" CssClass="table" runat="server" DataKeyField="ID" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" Visible="false" />
                                        <asp:BoundField DataField="Name" HeaderText="Name" />
                                        <asp:TemplateField HeaderText="Allow Add">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdnIDFieldp" runat="server" Value='<%# Eval("ID") %>' />
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
                        </asp:TabPanel>
                        <asp:TabPanel ID="TabProcess" CssClass="tab-panel" runat="server" HeaderText="Process">
                            <ContentTemplate>
                                <asp:Label ID="lblUserName_Process" runat="server" />
                                <asp:DropDownList ID="drpProjectList" runat="server" ToolTip="Project" AutoPostBack="True" />
                                <asp:Button ID="btnProcess" runat="server" Text="Save User process Access" />
                                <asp:GridView ID="gvProcess" CssClass="table" runat="server" DataKeyField="ID" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" Visible="false" />
                                        <asp:BoundField DataField="Name" HeaderText="Name" />
                                        <asp:TemplateField HeaderText="Allow Add">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdnIDFieldp" runat="server" Value='<%# Eval("ID") %>' />
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
                        </asp:TabPanel>
                        <asp:TabPanel ID="Question" CssClass="tab-panel" runat="server" HeaderText="Question">
                            <ContentTemplate>
                                <asp:Label ID="lblUserName_Question" runat="server" />
                                <asp:Button ID="btnQuestion" runat="server" Text="Save User Question Access" />
                                <asp:GridView ID="gvQuestion" CssClass="table" runat="server" DataKeyField="ID" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" Visible="false" />
                                        <asp:BoundField DataField="Name" HeaderText="Name" />
                                        <asp:TemplateField HeaderText="Allow Add">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdnIDFieldq" runat="server" Value='<%# Eval("ID") %>' />
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
                        </asp:TabPanel>
                        <asp:TabPanel ID="Function" CssClass="tab-panel" runat="server" HeaderText="Function" Visible="false">
                            <ContentTemplate>
                                <asp:Label ID="lblUserName_Function" runat="server" />
                                <asp:Button ID="btnFunction" runat="server" Text="Save User Function Access" />
                                <asp:GridView ID="gvFunction" CssClass="table" runat="server" DataKeyField="ID" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" Visible="false" />
                                        <asp:BoundField DataField="Name" HeaderText="Name" />
                                        <asp:TemplateField HeaderText="Allow Add">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdnIDFieldq" runat="server" Value='<%# Eval("ID") %>' />
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
                        </asp:TabPanel>
                    </asp:TabContainer>
                </div>
            </div>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                ShowSummary="false" />
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

