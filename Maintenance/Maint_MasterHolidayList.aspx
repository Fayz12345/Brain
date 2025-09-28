<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Maint_MasterHolidayList.aspx.cs" Inherits="BW_WebApp.Maintenance.Maint_MasterHolidayList" %>
<%@ Register Assembly="Syncfusion.Grid.Grouping.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Grid.Grouping" TagPrefix="syncfusion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>

<asp:Content ContentPlaceHolderID="cMC" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

        

            <asp:Panel ID="pnlMainView" runat="server">

                    <%--<asp:HiddenField ID="hdnESNtoAdd" runat="server" />--%>

                    <h1><asp:Label ID="lblRecordTitle" runat="server" Text="Master Holiday List" /></h1>

                    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click"/>
                    <asp:Button ID="btnEdit" runat="server" Text="Edit" Visible="False" OnClick="btnEdit_Click" />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" Visible="False" OnClick="btnDelete_Click" />
                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Continue with Delete?"
                        Enabled="True" TargetControlID="btnDelete" />
                    <asp:CheckBox ID="chkIncludeClosedMessages" runat="server" Text="Include Archived"
                        ToolTip="Click here to inlcude Archived Dates." AutoPostBack="True" />

                <asp:Panel ID="pnlMainGrid" runat="server" ScrollBars="Auto">
                    <div class="grid-grouping-control">
                        <syncfusion:GridGroupingControl ID="MainGrid_B" TabIndex="2" runat="server"
                            EnableCallbacks="True" DataSourceCachingMode="ViewState"
                            StatusBarText="List Updating.." BorderCollapse="Separate" ShowFocusedBorder="True"
                            ShowGroupDropArea="False" NestedTableGroupOptions-ShowFilterBarCondition="False"
                            TopLevelGroupOptions-ShowFilterStatusMessage="False" EnableAjaxPaging="False"
                            PageSize="0" ReadOnly="True" PostBackOnRowDblClick="False" ShowLoadingIndicatorOnCallback="True"
                            ShowSearchBox="True" EnsureCurrentRowVisibility="True">
                            <TableDescriptor AllowEdit="false" AllowNew="false">
                                <Appearance></Appearance>
                                <%--<VisibleColumns>
                                    <syncfusion:GridVisibleColumnDescriptor Name="ID" />
                                    <syncfusion:GridVisibleColumnDescriptor Name="IMEI" />
                                    <syncfusion:GridVisibleColumnDescriptor Name="Show" />
                                    <syncfusion:GridVisibleColumnDescriptor Name="Locked" />
                                    <syncfusion:GridVisibleColumnDescriptor Name="Created" />
                                    <syncfusion:GridVisibleColumnDescriptor Name="Created by" />
                                    <syncfusion:GridVisibleColumnDescriptor Name="Message" />
                                </VisibleColumns>--%>
                                <Columns>
                                    <syncfusion:GridColumnDescriptor MappingName="MasterHolidayID" HeaderText="ID" />
                                    <syncfusion:GridColumnDescriptor MappingName="Name" HeaderText="Holiday" />
                                    <syncfusion:GridColumnDescriptor MappingName="HolidayDate" HeaderText="Date" />
                                </Columns>
                            </TableDescriptor>
                        </syncfusion:GridGroupingControl>
                    </div>
                </asp:Panel>
            </asp:Panel>

            <asp:Panel ID="pnlAdd" runat="server">
                <h1>Add Holiday</h1>
                <div class="row">
                	<div class="col-md-6">
                        <label>Name:</label>
                        <asp:TextBox ID="AddName" runat="server" />
                        <label>Date:</label>
                        <asp:TextBox ID="AddDate" runat="server" />
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="AddDate" Format="MM/dd/yyyy" />
                        <asp:Button ID="AddOK" runat="server" Text="OK" OnClick="AddOK_Click" />
                        <asp:Button ID="AddCancel" runat="server" Text="Cancel" OnClick="AddCancel_Click1" />
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="pnlEdit" runat="server">
                <h1>Edit Holiday</h1>
                <div class="row">
                	<div class="col-md-6">
                        <label>Name:</label>
                        <asp:TextBox ID="EditName" runat="server" />
                        <asp:TextBox ID="EditKeyID" runat="server" ReadOnly="True" Visible="False" />
                        <label>Date:</label>
                        <asp:TextBox ID="EditDate" runat="server" />
                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="EditDate" Format="MM/dd/yyyy" />
                        <asp:Button ID="EditOK" runat="server" Text="OK" OnClick="EditOK_Click" />
                        <asp:Button ID="EditCancel" runat="server" Text="Cancel" OnClick="EditCancel_Click" />
                    </div>
                </div>
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
                //                ConfigureWaitingPopup(Popup);
                $('#loading').show();
            }
        }
        function EndRequestHandler(sender, args) {

            $('#loading').hide();
        }
    </script>
</asp:Content> 


