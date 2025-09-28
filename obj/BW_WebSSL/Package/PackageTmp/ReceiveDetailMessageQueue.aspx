<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReceiveDetailMessageQueue.aspx.cs" Inherits="BW_WebApp.ReceiveDetailMessageQueue" %>

<%@ Register Assembly="Syncfusion.Grid.Grouping.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89"
    Namespace="Syncfusion.Web.UI.WebControls.Grid.Grouping" TagPrefix="syncfusion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="cMC" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

        

            <asp:Panel ID="pnlMainView" runat="server">

                <h1><asp:Label ID="lblRecordTitle" runat="server" Text="IMEI Message Queue" /></h1>

                <asp:HiddenField ID="hdnESNtoAdd" runat="server" />
                <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" OnClientClick="return GetESN();" />
                <asp:Button ID="btnEdit" runat="server" Text="Edit" Visible="False" OnClick="btnEdit_Click" />
                <asp:Button ID="btnDelete" runat="server" Text="Delete" Visible="False" OnClick="btnDelete_Click" />
                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Continue with Delete?"
                    Enabled="True" TargetControlID="btnDelete" />
                
                <asp:CheckBox ID="chkIncludeClosedMessages" runat="server" Text="Include Archived"
                    ToolTip="Click here to inlcude Archived Messages." AutoPostBack="True" />
               
                <asp:Panel ID="pnlMainGrid" runat="server">
                    <div class="grid-grouping-control">
                        <syncfusion:GridGroupingControl ID="MainGrid_B" TabIndex="2" runat="server"
                        EnableCallbacks="True" DataSourceCachingMode="ViewState" StatusBarText="List Updating.." BorderCollapse="Separate"
                        ShowFocusedBorder="True" ShowGroupDropArea="False" NestedTableGroupOptions-ShowFilterBarCondition="False" 
                        TopLevelGroupOptions-ShowFilterStatusMessage="False" EnableAjaxPaging="False" PageSize="0" ReadOnly="True" 
                        PostBackOnRowDblClick="False" ShowLoadingIndicatorOnCallback="True" ShowSearchBox="True">
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
                                    <syncfusion:GridColumnDescriptor MappingName="ReceiveDetailESNMessageID" HeaderText="ID" />
                                    <syncfusion:GridColumnDescriptor MappingName="ESN" HeaderText="IMEI" />
                                    <syncfusion:GridColumnDescriptor MappingName="StatusOpen" HeaderText="Show" />
                                    <syncfusion:GridColumnDescriptor MappingName="StatusStop" HeaderText="Show" />
                                    <syncfusion:GridColumnDescriptor MappingName="CreateDate" HeaderText="Created" />
                                    <syncfusion:GridColumnDescriptor MappingName="CreateUser" HeaderText="Created by" />
                                    <syncfusion:GridColumnDescriptor MappingName="Message" HeaderText="Message" />
                                </Columns>
                            </TableDescriptor>
                        </syncfusion:GridGroupingControl>
                    </div>
                </asp:Panel>

            </asp:Panel>

            <asp:Panel ID="pnlAdd" CssClass="w-md-50" runat="server">

                <h1>Add IMEI Message</h1>

                <label>ESN:</label>
                <asp:TextBox ID="AddESN" runat="server" />

                <div>
                    <div class="form-check form-check-inline">
                        <asp:CheckBox ID="AddStatusOpen" runat="server" />
                        <label class="form-check-label">
                            Show
                        </label>
                    </div>

                    <div class="form-check form-check-inline">
                        <asp:CheckBox ID="AddStatusStop" runat="server" />
                        <label class="form-check-label">
                            Locked
                        </label>
                    </div>
                </div>

                <label>Message:</label>
                <asp:TextBox ID="AddMessage" runat="server" MaxLength="250" TextMode="MultiLine" Rows="5" />

                <asp:Button ID="AddOK" runat="server" Text="OK" OnClick="AddOK_Click" />
                <asp:Button ID="AddCancel" runat="server" Text="Cancel" OnClick="AddCancel_Click1" />

            </asp:Panel>

            <asp:Panel ID="pnlEdit" CssClass="w-md-50" runat="server">
                <h1>Edit IMEI Message</h1>
                      
                <label>ESN:</label>
                <asp:TextBox ID="EditESN" runat="server" />
                <asp:TextBox ID="EditKeyID" runat="server" ReadOnly="True" Visible="False" />

                <div>
                    <div class="form-check form-check-inline">
                        <asp:CheckBox ID="EditStatusOpen" runat="server" />
                        <label class="form-check-label">
                            Show
                        </label>
                    </div>

                    <div class="form-check form-check-inline">
                        <asp:CheckBox ID="EditStatusStop" runat="server" />
                        <label class="form-check-label">
                            Locked
                        </label>
                    </div>
                </div>

                <label>Message:</label>
                <asp:TextBox ID="EditMessage" runat="server" MaxLength="250" TextMode="MultiLine" Rows="5" />

                <asp:Button ID="EditOK" runat="server" Text="OK" OnClick="EditOK_Click" />
                <asp:Button ID="EditCancel" runat="server" Text="Cancel" OnClick="EditCancel_Click" />
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





        //    function PrintReportView() {
        ////             $find('= rvBagTag.ClientID ').invokePrintDialog();
        //    }

        function GetESN() {
            $get("<%= hdnESNtoAdd.ClientID %>").value = prompt("ESN to add message to:", "");
            return true;
        }

    </script>
</asp:Content>

