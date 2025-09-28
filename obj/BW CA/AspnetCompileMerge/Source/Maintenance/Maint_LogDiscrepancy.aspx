<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Maint_LogDiscrepancy.aspx.cs" Inherits="BW_WebApp.Maintenance.Maint_LogDiscrepancy" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>
<%@ Register Assembly="Syncfusion.Grid.Grouping.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Grid.Grouping" TagPrefix="syncfusion" %>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="cMC" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            
            <asp:HiddenField ID="hdnUserName" runat="server" />

            <asp:Panel ID="pnlMainView" runat="server">
                <h1><asp:Label ID="lblRecordTitle" runat="server" Text="Discrepancy Log" /></h1>

                <div class="row">
                	<div class="col-md-6">
                        <label>ScanKey:</label>
                        <div class="input-group">
                            <asp:TextBox ID="txtScanKey" runat="server" ToolTip="Client Location Scan Code." />
                            <div class="input-group-append">
                                <asp:Button ID="btnScanKeyGo" runat="server" Text="Search" />
                            </div>
                        </div>
                
                        <asp:Label ID="ClientCell01" runat="server" Text="Client:" />
                        <asp:Panel ID="ClientCell02" runat="server">
                            <asp:DropDownList ID="drpClientList" runat="server" ToolTip="Client" AutoPostBack="True" />
                        </asp:Panel>
                    
                        <label>Location:</label>
                        <asp:DropDownList ID="drpLocationList" runat="server" ToolTip="Location" AutoPostBack="True" />
                    
                        <asp:CheckBox ID="chkResolved" CssClass="d-block" runat="server" Text="Show Resolved" ToolTip="Check to include resolved issued on list." />

                        <asp:Button ID="btnAdd" runat="server" Text="Add" />
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" Visible="False" />
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" Visible="False" />
                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Continue with Delete?"
                            Enabled="True" TargetControlID="btnDelete" />
                    </div>
                
                    <div class="col-md-6">
                        <div class="form-row">
                	        <div class="col-6">
                                <label>Scan Key:</label>
                                <asp:TextBox ID="SearchScanKey" runat="server" ToolTip="Search by Client Location Scan key" />
                            </div>
                            <div class="col-6">
                                <label>ID:</label>
                                <asp:TextBox ID="SearchID" runat="server" ToolTip="Search by ID" />
                            </div>
                            <div class="col-6">
                                <label>IMEI:</label>
                                <asp:TextBox ID="SearchIMEI" runat="server" ToolTip="Search by IMEI" />
                            </div>
                            <div class="col-6">
                                <label>Create Begin Date:</label>
                                <asp:TextBox ID="txtBeginDate" runat="server" ToolTip="Find all records created Beginning" />
                                <asp:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="txtBeginDate"
                                    Format="MM/dd/yyyy" />
                            </div>
                            <div class="col-6">
                                <label>End Date:</label>
                                <asp:TextBox ID="txtEndDate" runat="server" ToolTip="Find all records created Ending" />
                                <asp:CalendarExtender ID="CalendarExtender8" runat="server" TargetControlID="txtEndDate"
                                    Format="MM/dd/yyyy" />
                            </div>
                        </div>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" />

                </div>
                
                <asp:GridView ID="MainGrid" CssClass="table" runat="server" AutoGenerateSelectButton="True" DataKeyNames="DiscrepancyID"
                    AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="DiscrepancyID" HeaderText="ID" ReadOnly="True" Visible="true" />
                        <asp:BoundField DataField="CreateDate" HeaderText="CreateDate" />
                        <asp:BoundField DataField="Type" HeaderText="Type" />
                        <asp:BoundField DataField="DiscrepancyText" HeaderText="Discrepancy" />
                        <asp:BoundField DataField="Resolved" HeaderText="Resolved" />
                        <asp:BoundField DataField="OutCome" HeaderText="Outcome" />
                        <asp:BoundField DataField="CompanyName" HeaderText="CompanyName" />
                        <asp:BoundField DataField="ScanKey" HeaderText="Scankey" />
                        <asp:BoundField DataField="IMEI" HeaderText="IMEI" />

                        <%--<asp:BoundField DataField="ClientID" HeaderText="ClientID" />
                        <asp:BoundField DataField="ClientLocationID" HeaderText="ClientLocationID" />
                        <asp:BoundField DataField="Division" HeaderText="Division" />
                        <asp:BoundField DataField="Transfer_WO" HeaderText="Transfer_WO" />
                        <asp:BoundField DataField="ReturnTransfer" HeaderText="ReturnTransfer" />
                        <asp:BoundField DataField="IMEI" HeaderText="IMEI" />
                        <asp:BoundField DataField="AttemptDate" HeaderText="AttemptDate" />
                        <asp:BoundField DataField="AttemptUser" HeaderText="AttemptUser" />
                        <asp:BoundField DataField="AttemptDate2" HeaderText="AttemptDate2" />
                        <asp:BoundField DataField="AttemptUser2" HeaderText="AttemptUser2" />
                        <asp:BoundField DataField="AttemptDate3" HeaderText="AttemptDate3" />
                        <asp:BoundField DataField="AttemptUser3" HeaderText="AttemptUser3" />
                        <asp:BoundField DataField="CreateUser" HeaderText="CreateUser" />
                        <asp:BoundField DataField="LastUpdateDate" HeaderText="LastUpdateDate" />
                        <asp:BoundField DataField="LastUpdateUser" HeaderText="LastUpdateUser" />--%>
                    </Columns>
                </asp:GridView>
            </asp:Panel>

            <asp:Panel ID="pnlAdd" runat="server">
                <h1>Add Discrepancy</h1>

                <div class="row">
                	<div class="col-md-6">
                        <label>ID:</label>
                        <asp:TextBox ID="AddDiscrepancyID" runat="server" ReadOnly="True" />
                
                        <label>Date:</label>
                        <asp:TextBox ID="AddCreateDate" runat="server" ReadOnly="True" />
                
                        <label>Store:</label>
                        <asp:TextBox ID="AddStore" runat="server" ReadOnly="True" />
                
                        <label>Store Name:</label>
                        <asp:TextBox ID="AddStoreName" runat="server" ReadOnly="True" />
                
                        <label>IMEI:</label>
                        <asp:TextBox ID="AddIMEI" runat="server" MaxLength="50" />
                
                        <label>Discrepancy:</label>
                        <asp:DropDownList ID="drpAddDiscrepancy" runat="server" ToolTip="Process Status" AutoPostBack="True" />
                
                        <label>Type:</label>
                        <asp:DropDownList ID="AddType" runat="server" ToolTip="Process Status" AutoPostBack="True" />        
                    </div>

                    <div class="col-md-6">
                        <label>Transfer WO#:</label>
                        <asp:TextBox ID="AddTransferWO" runat="server" MaxLength="50" />
                
                        <label>First Attempt:</label>
                        <asp:TextBox ID="AddFirstAttemp" runat="server" />
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="AddFirstAttemp" Format="MM/dd/yyyy" />
                
                        <label>Return Transfer:</label>
                        <asp:TextBox ID="AddReturnTransfer" runat="server" MaxLength="50" />
                
                        <label>Second Attempt:</label>
                        <asp:TextBox ID="AddSecondAttemp" runat="server" />
                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="AddSecondAttemp" Format="MM/dd/yyyy" />
                
                        <label>Third Attempt:</label>
                        <asp:TextBox ID="AddThirdAttemp" runat="server" />
                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="AddThirdAttemp" Format="MM/dd/yyyy" />
                
                        <label>Outcome:</label>
                        <asp:DropDownList ID="drpAddOutcome" runat="server" ToolTip="Process Status" />

                        <asp:CheckBox ID="chkAddResolved" Text="Resolved" runat="server" />
                    </div>
                </div>
                <asp:Button ID="btnAddOK" runat="server" Text="OK" />
                <asp:Button ID="btnAddCancel" runat="server" Text="Cancel" />
            </asp:Panel>

            <asp:Panel ID="pnlEditx" runat="server">
                <asp:HiddenField ID="hdnDiscrepancyID" runat="server" />

                <div class="row">
                	<div class="col-md-6">
                        <h1>Edit Discrepancy</h1>

                        <label>ID:</label>
                        <asp:TextBox ID="EditDiscrepancyID" runat="server" ReadOnly="True" />
                
                        <label>Date:</label>
                        <asp:TextBox ID="EditCreateDate" runat="server" ReadOnly="True" />
                
                        <label>Store:</label>
                        <asp:TextBox ID="EditStore" runat="server" ReadOnly="True" />
                
                        <label>Store Name:</label>
                        <asp:TextBox ID="EditStoreName" runat="server" ReadOnly="True" />
                
                        <label>IMEI:</label>
                        <asp:TextBox ID="EditIMEI" runat="server" MaxLength="50" />
                
                        <label>Discrepancy:</label>
                        <asp:DropDownList ID="drpEditDiscrepancy" runat="server" ToolTip="Discrepancy" AutoPostBack="True" />
                
                        <label>Type:</label>
                        <asp:DropDownList ID="drpEditType" runat="server" ToolTip="Type" AutoPostBack="True" />
                    </div>

                    <div class="col-md-6">                
                        <label>Transfer WO#:</label>
                        <asp:TextBox ID="EditTransferWO" runat="server" MaxLength="50" />
                
                        <label>First Attempt:</label>
                        <asp:TextBox ID="EditFirstAttemp" runat="server" />
                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="EditFirstAttemp" Format="MM/dd/yyyy" />
                
                        <label>Return Transfer:</label>
                        <asp:TextBox ID="EditReturnTransfer" runat="server" MaxLength="50" />
                
                        <label>Second Attempt:</label>
                        <asp:TextBox ID="EditSecondAttemp" runat="server" />
                        <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="EditSecondAttemp" Format="MM/dd/yyyy" />
                
                        <label>Third Attempt:</label>
                        <asp:TextBox ID="EditThirdAttemp" runat="server" />
                        <asp:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="EditThirdAttemp" Format="MM/dd/yyyy" />
                
                        <label>Outcome:</label>
                        <asp:DropDownList ID="drpEditOutcome" runat="server" ToolTip="Outcome" />

                        <asp:CheckBox ID="chkEditResolved" CssClass="d-block" Text="Resolved" runat="server" />
                    </div>
                </div>
                
                <asp:Button ID="btnEditOK" runat="server" Text="OK" />
                <asp:Button ID="btnEditCancel" runat="server" Text="Cancel" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ContentPlaceHolderID="js" runat="server">
    <script type="text/javascript">

        document.onkeypress = stopRKey;

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

        function stopRKey(evt) {
            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if ((evt.keyCode == 13) && (node.type == "text")) { return false; }
        }

    </script>
</asp:Content>



