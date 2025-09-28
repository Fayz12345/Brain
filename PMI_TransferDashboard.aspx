<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PMI_TransferDashboard.aspx.cs" Inherits="BW_WebApp.PMI_TransferDashboard" %>
<%@ Register Assembly="Syncfusion.Grid.Grouping.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89"
    Namespace="Syncfusion.Web.UI.WebControls.Grid.Grouping" TagPrefix="syncfusion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="cMC" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            

            <asp:Panel ID="pnlMainView" runat="server">

                <%--<asp:HiddenField ID="hdnESNtoAdd" runat="server" />--%>
                <h1><asp:Label ID="lblRecordTitle" runat="server" Text="PMI Transfer Dashboard" /></h1>

                <div class="row">
                	<div class="col-md-6">
                        <asp:DropDownList ID="drpFileType" runat="server" AutoPostBack="True">
                            <asp:ListItem>File 01</asp:ListItem>
                            <asp:ListItem>File 02</asp:ListItem>
                            <asp:ListItem>File 03</asp:ListItem>
                            <asp:ListItem>All</asp:ListItem>
                        </asp:DropDownList>

                        <asp:Button ID="btnNotReceived" runat="server" Text="Not Received Report" Visible="False" />
                        <asp:Button ID="btnStartTransfer" runat="server" Text="Start Transfer" Visible="True" OnClick="btnStartPMITransfer_Click" />
                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Continue to Start PMI Transfer process?"
                            Enabled="True" TargetControlID="btnStartTransfer" />

                        <%--<asp:CheckBox ID="chkIncludeClosedMessages" runat="server" Text="Include Archived" ToolTip="Click here to inlcude Archived Dates" AutoPostBack="True" />--%>
                    </div>
                </div>

                <asp:Panel ID="pnlMainGrid" runat="server">
                    <div class="grid-grouping-control">
                        <syncfusion:GridGroupingControl ID="MainGrid_B" TabIndex="2" runat="server"
                            EnableCallbacks="True" DataSourceCachingMode="ViewState"
                            StatusBarText="List Updating.." BorderCollapse="Separate" ShowFocusedBorder="True"
                            ShowGroupDropArea="False" NestedTableGroupOptions-ShowFilterBarCondition="False"
                            TopLevelGroupOptions-ShowFilterStatusMessage="False" EnableAjaxPaging="False"
                            PageSize="0" ReadOnly="True" PostBackOnRowDblClick="False" ShowLoadingIndicatorOnCallback="True"
                            ShowSearchBox="True" EnsureCurrentRowVisibility="True">
                            <TableDescriptor AllowEdit="false" AllowNew="false">
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
                                    <syncfusion:GridColumnDescriptor MappingName="MasterFileTransferLogID" HeaderText="ID" />
                                    <syncfusion:GridColumnDescriptor MappingName="MasterFileTransferDetailLogID" HeaderText="ID" />
                                    <syncfusion:GridColumnDescriptor MappingName="TransferType" HeaderText="TransferType" />
                                    <syncfusion:GridColumnDescriptor MappingName="TransferDate" HeaderText="TransferDate" />
                                    <syncfusion:GridColumnDescriptor MappingName="FileName" HeaderText="FileName" />
                                    <%--<syncfusion:GridColumnDescriptor MappingName="Name" HeaderText="Name" />--%>
                                    <syncfusion:GridColumnDescriptor MappingName="ESN" HeaderText="ESN" />
                                    <syncfusion:GridColumnDescriptor MappingName="StatusDetail" HeaderText="StatusDetail" />
                                    <syncfusion:GridColumnDescriptor MappingName="StatusMessage" HeaderText="StatusMessage" />
                                    <syncfusion:GridColumnDescriptor MappingName="RecordText" HeaderText="RecordText" />
                                    <syncfusion:GridColumnDescriptor MappingName="CreateDate" HeaderText="CreateDate" />
                                    <syncfusion:GridColumnDescriptor MappingName="CreateUser" HeaderText="CreateUser" />
                                    <syncfusion:GridColumnDescriptor MappingName="LastUpdateDate" HeaderText="LastUpdateDate" />
                                    <syncfusion:GridColumnDescriptor MappingName="LastUpdateUser" HeaderText="LastUpdateUser" />
                                    <syncfusion:GridColumnDescriptor MappingName="CreateDateDetail" HeaderText="CreateDateDetail" />
                                    <syncfusion:GridColumnDescriptor MappingName="CreateUserDetail" HeaderText="CreateUserDetail" />
                                    <syncfusion:GridColumnDescriptor MappingName="LastUpdateDateDetail" HeaderText="LastUpdateDateDetail" />
                                    <syncfusion:GridColumnDescriptor MappingName="LastUpdateUserDetail" HeaderText="LastUpdateUserDetail" />
                                </Columns>
                            </TableDescriptor>
                        </syncfusion:GridGroupingControl>
                    </div>
                </asp:Panel>
            </asp:Panel>

            <%--<asp:Panel ID="pnlAdd" runat="server">
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
            </asp:Panel>--%>

            <%--<asp:Panel ID="pnlEdit" runat="server">
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
            </asp:Panel>--%>

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



