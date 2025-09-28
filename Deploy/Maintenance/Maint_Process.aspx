<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Maint_Process.aspx.cs" Inherits="BW_WebApp.Maintenance.Maint_Process" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>
<%@ Register Assembly="Syncfusion.Grid.Grouping.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Grid.Grouping" TagPrefix="syncfusion" %>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="cMC" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional"> 
        <ContentTemplate>

            

            <asp:HiddenField ID="hdnUserName" runat="server" />
            <asp:Panel ID="pnlMainView" runat="server">
                
                <asp:Label ID="lblRecordTitle" runat="server" Text="Maintenance Process Definition" />
                    
                <label>Project:</label>
                <asp:DropDownList ID="drpProjectList" CssClass="d-block w-md-50" runat="server" ToolTip="Project" AutoPostBack="True" />
                    
                <%--<label><asp:Label ID="lblMisc" runat="server" /></label>--%>
                
                <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
                <asp:Button ID="btnEdit" runat="server" Text="Edit" Visible="False" OnClick="btnEdit_Click" />
                <asp:Button ID="btnDelete" runat="server" Text="Delete" Visible="False" OnClick="btnDelete_Click" />
                <asp:Button ID="btnQuestion" runat="server" Text="Question" Visible="False" ToolTip="Attach Questions to this Process" OnClick="btnProcesss_Click" />
                <asp:Button ID="btnNextMove" runat="server" Text="Next Move" Visible="False" ToolTip="Attach Next Move to this Process" OnClick="btnNextMove_Click" />
                <asp:Button ID="btnPrintScanCodes" runat="server" Text="Print Scan Codes" Visible="False" />
                <%--<asp:Button ID="btnBinLocation" runat="server" Text="Bin(s)" Visible="False" ToolTip="Assign suggested bin location" OnClick="btnBinLocation_Click" />--%>
                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Continue with Delete?" Enabled="True" TargetControlID="btnDelete" />

                <asp:Panel ID="pnlMainGrid" ScrollBars="auto" runat="server">
                    <div class="grid-grouping-control">
                        <syncfusion:GridGroupingControl ID="MainGrid_B" TabIndex="2" runat="server" 
                            EnableCallbacks="True" DataSourceCachingMode="ViewState"
                            StatusBarText="List Updating.." BorderCollapse="Separate" ShowFocusedBorder="True"
                            ShowGroupDropArea="False" NestedTableGroupOptions-ShowFilterBarCondition="False"
                            TopLevelGroupOptions-ShowFilterStatusMessage="False" EnableAjaxPaging="False"
                            PageSize="0" ReadOnly="True" PostBackOnRowDblClick="False" ShowLoadingIndicatorOnCallback="True"
                            ShowSearchBox="True" EnsureCurrentRowVisibility="True" ClientSideOnSelectionChanged="GridSelectionChangedx('oData')">
                            <TableDescriptor AllowEdit="false" AllowNew="false">
                                <Columns>
                                    <syncfusion:GridColumnDescriptor MappingName="ProcessID" HeaderText="ID" />
                                    <syncfusion:GridColumnDescriptor MappingName="ScanKey" HeaderText="ScanKey" />
                                    <syncfusion:GridColumnDescriptor MappingName="MacroKey" HeaderText="MacroKey" />
                                    <syncfusion:GridColumnDescriptor MappingName="ButtonText" HeaderText="ButtonText" />
                                    <syncfusion:GridColumnDescriptor MappingName="RMASuffix" HeaderText="RMASuffix" />
                                    <syncfusion:GridColumnDescriptor MappingName="Name" HeaderText="Name by" />
                                    <syncfusion:GridColumnDescriptor MappingName="Description" HeaderText="Process" />
                                    <syncfusion:GridColumnDescriptor MappingName="Description_Client" HeaderText="Description_Client" />
                                    <syncfusion:GridColumnDescriptor MappingName="Status" HeaderText="Status" />
                                    <syncfusion:GridColumnDescriptor MappingName="ShowTAT" HeaderText="Show TAT" />
                                    <syncfusion:GridColumnDescriptor MappingName="CanJumpProject" HeaderText="JP" />
                                    <syncfusion:GridColumnDescriptor MappingName="TurnStickyOn" HeaderText="ST" />
                                    <syncfusion:GridColumnDescriptor MappingName="Sequence" HeaderText="Seq" />
                                    <syncfusion:GridColumnDescriptor MappingName="ShowCompletedStatus" HeaderText="S" />
                                </Columns>
                            </TableDescriptor>
                        </syncfusion:GridGroupingControl>
                    </div>

                    <%--<asp:GridView ID="MainGrid" CssClass="table" runat="server" AutoGenerateSelectButton="True" DataKeyNames="ProcessID" AutoGenerateColumns="False"> 
                        <Columns>
                            <asp:BoundField DataField="ProcessID" HeaderText="ID" ReadOnly="True" Visible="false" />
                            <asp:BoundField DataField="ScanKey" HeaderText="ScanKey" />
                            <asp:BoundField DataField="MacroKey" HeaderText="MacroKey" />
                            <asp:BoundField DataField="ButtonText" HeaderText="Button" />
                            <asp:BoundField DataField="RMASuffix" HeaderText="Suffix" />
                            <asp:BoundField DataField="Name" HeaderText="Name" />
                            <asp:BoundField DataField="Description" HeaderText="Process" />
                            <asp:BoundField DataField="Description_Client" HeaderText="Description_Client" />
                            <asp:BoundField DataField="Status" HeaderText="Status" />
                            <asp:BoundField DataField="ShowTAT" HeaderText="Show TAT" />
                            <asp:BoundField DataField="CanJumpProject" HeaderText="JP" />
                            <asp:BoundField DataField="TurnStickyOn" HeaderText="ST" />
                            <asp:BoundField DataField="Sequence" HeaderText="Seq" />
                            <asp:BoundField DataField="ShowCompletedStatus" HeaderText="S" />
                        </Columns>
                    </asp:GridView>--%>
                </asp:Panel>

                <asp:Panel ID="pnlMainBucket" runat="server">
                    <asp:Button ID="btnAddBucket" runat="server" Text="Add" OnClick="btnAddBucket_Click" />
                    <asp:Button ID="btnEditBucket" runat="server" Text="Edit" Visible="False" OnClick="btnEditBucket_Click" />
                    <asp:Button ID="btnDeleteBucket" runat="server" Text="Delete" Visible="False" OnClick="btnDeleteBucket_Click" />
                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server" ConfirmText="Continue with Delete?" Enabled="True" TargetControlID="btnDeleteBucket" />
                    
                    <asp:Label ID="lblBucket" Text="Bucket Counters" runat="server" />

                    <asp:GridView ID="grdMasterBucketTransaction" CssClass="table" runat="server" AutoGenerateSelectButton="True"
                        DataKeyNames="MasterBucketTransactionsID" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="MasterBucketTransactionsID" HeaderText="ID" ReadOnly="True" Visible="false" />
                            <asp:BoundField DataField="Action" HeaderText="Action" />
                            <asp:BoundField DataField="Count" HeaderText="Count" />
                            <asp:BoundField DataField="OneOnly" HeaderText="Single" />
                            <asp:BoundField DataField="Active" HeaderText="Active" />
                        </Columns>
                    </asp:GridView>
                </asp:Panel>

            </asp:Panel>

            <asp:Panel ID="pnlAdd" runat="server">
                <h1>Add Process Definition</h1>
                
                <div class="row">
                	<div class="col-md">
                        <label>ScanKey:</label>
                        <asp:TextBox ID="AddScanKey" runat="server" MaxLength="50" />
                
                        <label>MacroKey:</label>
                        <asp:TextBox ID="AddMacroKey" runat="server" MaxLength="2" />
                
                        <label>RMA Suffix:</label>
                        <asp:TextBox ID="AddRMASuffix" runat="server" MaxLength="2" />
                
                        <label>Name:</label>
                        <asp:TextBox ID="AddName" runat="server" MaxLength="20" />
                
                        <label>Button Text:</label>
                        <asp:TextBox ID="AddButton" runat="server" MaxLength="25" />
                
                        <label>Description:</label>
                        <asp:TextBox ID="AddDescription" runat="server" MaxLength="50" />
                    </div>
                	<div class="col-md">
                        <label>Description Client:</label>
                        <asp:TextBox ID="AddDescription_Client" runat="server" MaxLength="50" />
                
                        <label>Status:</label>
                        <asp:DropDownList ID="drpAddStatus" runat="server" ToolTip="Process Status" />
                
                        <label>Sequence:</label>
                        <asp:TextBox ID="AddSequence" runat="server" />

                        <asp:CheckBox ID="AddShowCompletedStatus" CssClass="d-block" Text="Show Completed Status" runat="server" />
                        <asp:CheckBox ID="AddisReadOnly" CssClass="d-block" Text="Read Only" runat="server" />
                        <asp:CheckBox ID="AddShowTat" CssClass="d-block" Text="Show TAT" runat="server" />
                        <asp:CheckBox ID="AddCanJumpProject" CssClass="d-block" Text="Can Jump Projects" runat="server" />
                        <asp:CheckBox ID="AddTurnStickyOn" CssClass="d-block" Text="Static" runat="server" />
                        <asp:CheckBox ID="AddAllowXBINX" CssClass="d-block" Text="Allow XBINX" runat="server" />

                        <%--<asp:TextBox ID="AddBucketCount" Text="Bucket Count" runat="server" />
                        <asp:TextBox ID="AddBucketCountOffset" Text="Bucket Count Offset" runat="server" />--%>
                    </div>
                </div>
                                
                <asp:Button ID="AddProcessOK" runat="server" Text="OK" onclick="AddProcessOK_Click" />
                <asp:Button ID="AddProcessCancel" runat="server" Text="Cancel" onclick="AddProcessCancel_Click" />

            </asp:Panel>

            <asp:Panel ID="pnlEdit" runat="server">
                <h1>Edit Process Definition</h1>

                <div class="row">
                	<div class="col-md">
                        <label>ScanKey:</label>
                        <asp:TextBox ID="EditScanKey" runat="server" MaxLength="50" />
                
                        <label>MacroKey:</label>
                        <asp:TextBox ID="EditMacroKey" runat="server" MaxLength="2" />
                
                        <label>RMA Suffix:</label>
                        <asp:TextBox ID="EditRMASuffix" runat="server" MaxLength="2" />
                
                        <label>Name:</label>
                        <asp:TextBox ID="EditName" runat="server" />
                        <asp:TextBox ID="EditKeyID" runat="server" ReadOnly="True" Visible="False" />
                
                        <label>Button Text:</label>
                        <asp:TextBox ID="EditButton" runat="server" MaxLength="25" />
                
                        <label>Description:</label>
                        <asp:TextBox ID="EditDescription" runat="server" MaxLength="50" />
                    </div>
                	<div class="col-md">
                        <label>Description Client:</label>
                        <asp:TextBox ID="EditDescription_Client" runat="server" MaxLength="50" />

                        <label>Status:</label>
                        <asp:DropDownList ID="drpEditStatus" runat="server" ToolTip="Process Status" />
                
                        <label>Sequence:</label>
                        <asp:TextBox ID="EditSequence" runat="server" />

                        <asp:CheckBox ID="EditShowCompletedStatus" CssClass="d-block" Text="Show Completed Status" runat="server" />
                        <asp:CheckBox ID="EditisReadOnly" CssClass="d-block" Text="Read Only" runat="server" />
                        <asp:CheckBox ID="EditShowTat" CssClass="d-block" Text="Show TAT" runat="server" />
                        <asp:CheckBox ID="EditCanJumpProject" CssClass="d-block" Text="Can Jump Projects" runat="server" />
                        <asp:CheckBox ID="EditTurnStickyOn" CssClass="d-block" Text="Static" runat="server" />
                        <asp:CheckBox ID="EditAllowXBINX" CssClass="d-block" Text="Allow XBINX" runat="server" />
                        <asp:CheckBox ID="chkDisablePrint" CssClass="d-block" Text="Disable Print Button" runat="server" />
                        <asp:CheckBox ID="chkForcePrintOnSave" CssClass="d-block" Text="Force Print on Save" runat="server" />

                        <%--<asp:TextBox ID="EditBucketCount" Text="Bucket Count" runat="server" />
                        <asp:TextBox ID="EditBucketCountOffset" Text="Bucket Count Offset" runat="server" />--%>
                    </div>
                </div>
                <asp:Button ID="EditProcessOK" runat="server" Text="OK" onclick="EditProcessOK_Click" />
                <asp:Button ID="EditProcessCancel" runat="server" Text="Cancel" onclick="EditProcessCancel_Click" />
            </asp:Panel>

            <asp:Panel ID="pnlAddBucket" runat="server" Visible="false">
                <h1>Add Process Bucket</h1>

                <div class="row">
                	<div class="col-md-6">
                        <label>Action:</label>
                        <asp:TextBox ID="txtAddAction" runat="server" MaxLength="20" />
                
                        <label>Count:</label>
                        <asp:TextBox ID="txtAddCount" runat="server" />
                
                        <asp:CheckBox ID="chkAddSingle" CssClass="d-block" Text="Single" runat="server" />
                        <asp:CheckBox ID="chkAddBucket" CssClass="d-block" Text="Active" runat="server" />
                    </div>
                </div>

                <asp:Button ID="AddBucketOK" runat="server" Text="OK" onclick="AddBucketOK_Click" />
                <asp:Button ID="AddBucketCancel" runat="server" Text="Cancel" onclick="AddBucketCancel_Click" />
                
            </asp:Panel>

            <asp:Panel ID="pnlEditBucket" runat="server" Visible="false">
                <h1>Edit Process Bucket</h1>
                
                <div class="row">
                	<div class="col-md-6">
                        <label>Action:</label>
                        <asp:TextBox ID="txtEditAction" runat="server" MaxLength="20" />
                
                        <label>Count:</label>
                        <asp:TextBox ID="txtEditCount" runat="server"  />
                
                        <asp:CheckBox ID="chkEditSingle" CssClass="d-block" Text="Single" runat="server" />
                        <asp:CheckBox ID="chkEditBucket" CssClass="d-block" Text="Active" runat="server" />
                    </div>
                </div>
                
                <asp:Button ID="EditBucketOK" runat="server" Text="OK" onclick="EditBucketOK_Click" />
                <asp:Button ID="EditBucketCancel" runat="server" Text="Cancel" onclick="EditBucketCancel_Click" />
            </asp:Panel>

            <asp:Panel ID="pnlProcessAnswer" runat="server">
                <h1>Answer List</h1>
                <h3><asp:Label ID="lblAnswerList" runat="server" Text="NextMove" /></h3>

                <div class="row">
                	<div class="col">
                        <label>Source:</label>
                        <asp:ListBox ID="lstQuestionSource" runat="server" SelectionMode="Multiple" ViewStateMode="Inherit" ClientIDMode="Static">
                            <asp:ListItem Value="1" Text="One" />
                            <asp:ListItem Value="2" Text="Two" />
                            <asp:ListItem Value="3" Text="Three" />
                            <asp:ListItem Value="4" Text="Four" />
                            <asp:ListItem Value="5" Text="Five" />
                            <asp:ListItem Value="6" Text="Six" />
                            <asp:ListItem Value="7" Text="Seven" />
                        </asp:ListBox>
                    </div>
                	<div class="col-auto align-self-end">
                        <asp:Button ID="btnRight" CssClass="w-100" runat="server" Text=">" OnClientClick="MoveItem('lstQuestionSource','lstQuestionTarget');return false;" />
                        <asp:Button ID="btnLeft" CssClass="w-100" runat="server" Text="<" OnClientClick="MoveItem('lstQuestionTarget','lstQuestionSource');return false;" />
                    </div>
                	<div class="col">
                        <label>Target:</label>
                        <asp:ListBox ID="lstQuestionTarget" runat="server" ClientIDMode="Static" SelectionMode="Multiple">
                            <asp:ListItem Value="8" Text="Eight" />
                            <asp:ListItem Value="9" Text="Nine" />
                        </asp:ListBox>
                    </div>
                </div>

                <div class="row">
                	<div class="col-md-6">
                        <asp:Button ID="btnSave" runat="server" Text="OK" OnClick="btnSave_Click" OnClientClick="GatherKeys('lstQuestionTarget','HiddenQuestionIDs');" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />

                        <asp:DropDownList ID="drpProcessList" runat="server" ToolTip="Process to Copy" />
                        <asp:Button ID="btnCopyFrom" runat="server" Text="Copy from Process" />

                        <asp:HiddenField ID="HiddenQuestionIDs" runat="server" ClientIDMode="Static" />
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="pnlProcessNextMove" runat="server">
                <h1>Process Next Move List:</h1>
                <h3><asp:Label ID="lblNextMove" runat="server" Text="NextMove" /></h3>

                <div class="row">
                	<div class="col">
                        <label>Source:</label>
                        <asp:ListBox ID="lstNextMoveFrom" runat="server" SelectionMode="Multiple" ViewStateMode="Inherit" ClientIDMode="Static">
                            <asp:ListItem Value="1" Text="One" />
                            <asp:ListItem Value="2" Text="Two" />
                            <asp:ListItem Value="3" Text="Three" />
                            <asp:ListItem Value="4" Text="Four" />
                            <asp:ListItem Value="5" Text="Five" />
                            <asp:ListItem Value="6" Text="Six" />
                            <asp:ListItem Value="7" Text="Seven" />
                        </asp:ListBox>
                    </div>
                    <div class="col-auto align-self-end">
                        <asp:Button CssClass="w-100" runat="server" Text=">" OnClientClick="MoveItem('lstNextMoveFrom','lstNextMoveTo');return false;" />
                        <asp:Button CssClass="w-100" runat="server" Text="<" OnClientClick="MoveItem('lstNextMoveTo','lstNextMoveFrom');return false;" />
                    </div>
                    <div class="col">
                        <label>Target:</label>
                        <asp:ListBox ID="lstNextMoveTo" runat="server" ClientIDMode="Static" SelectionMode="Multiple">
                            <asp:ListItem Value="8" Text="Eight" />
                            <asp:ListItem Value="9" Text="Nine" />
                        </asp:ListBox>
                    </div>
                </div>
                
                <asp:Button ID="btnNextMoveOK" runat="server" Text="OK" onclick="btnNextMoveOK_Click" OnClientClick="GatherKeys('lstNextMoveTo','HiddenProcessNextStepIDs');" />
                <asp:Button ID="btnNextMoveCancel" runat="server" Text="Cancel" onclick="btnNextMoveCancel_Click" />
                <asp:HiddenField ID="HiddenProcessNextStepIDs" runat="server" ClientIDMode="Static" />
            </asp:Panel>

            <asp:Panel ID="pnlProcessBinLocation" runat="server">
                <h1>Process Bin Location List:</h1>
                <h3><asp:Label ID="lblBinLocation" runat="server" /></h3>

                <div class="row">
                	<div class="col">
                        <label>Source:</label>
                        <asp:ListBox ID="lstBinMoveFrom" runat="server" SelectionMode="Multiple" ViewStateMode="Inherit" ClientIDMode="Static">
                            <asp:ListItem Value="1" Text="One" />
                            <asp:ListItem Value="2" Text="Two" />
                            <asp:ListItem Value="3" Text="Three" />
                            <asp:ListItem Value="4" Text="Four" />
                            <asp:ListItem Value="5" Text="Five" />
                            <asp:ListItem Value="6" Text="Six" />
                            <asp:ListItem Value="7" Text="Seven" />
                        </asp:ListBox>
                    </div>
                	<div class="col-auto align-self-end">
                        <asp:Button CssClass="w-100" runat="server" Text=">" OnClientClick="MoveItem('lstBinMoveFrom','lstBinMoveTo');return false;" />
                        <asp:Button CssClass="w-100" runat="server" Text="<" OnClientClick="MoveItem('lstBinMoveTo','lstBinMoveFrom');return false;" />
                    </div>
                	<div class="col">
                        <label>Target:</label>
                        <asp:ListBox ID="lstBinMoveTo" runat="server" ClientIDMode="Static" SelectionMode="Multiple">
                            <asp:ListItem Value="8" Text="Eight" />
                            <asp:ListItem Value="9" Text="Nine" />
                        </asp:ListBox>
                    </div>
                </div>
                
                <%--<asp:Button ID="btnBinLocationOK" runat="server" Text="OK" onclick="btnBinLocationOK_Click" OnClientClick="GatherKeys('lstBinMoveTo','HiddenProcessBinLocationIDs');" />
                <asp:Button ID="btnBinLocationCancel" runat="server" Text="Cancel" onclick="btnBinLocationCancel_Click" />
                <asp:HiddenField ID="HiddenProcessBinLocationIDs" runat="server" ClientIDMode="Static" />--%>
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

        function GridSelectionChangedx() {
            //	        alert("selected");
            //alert(oData.Row.GetValue("oData.ProcessID"));
        }

        function GridSelectionChanged(oData) {
            //	        alert("selected");
            //alert(oData.Row.GetValue("oData.ProcessID"));
        }

        function MoveProcessIn() {

            var CurrentUser = $get('<%=hdnUserName.ClientID %>').value;
            var IndexValue = $get('<%=drpProcessList.ClientID %>').selectedIndex;
            var ProcessText = $get('<%=drpProcessList.ClientID %>').options[IndexValue].text;
            var ProcessValue = $get('<%=drpProcessList.ClientID %>').options[IndexValue].value;
            var service = new WebServer_01();
            service.GetProcessQuestionIDValue(CurrentUser, ProcessValue, onSuccess, null, null);
        }

        function onSuccess(result) {
            var B = result.split(",")
            for (var i = 0; i < B.length; i++) {
                setValues('lstQuestionSource', B[i])
                MoveItem('lstQuestionSource', 'lstQuestionTarget')
                // go down through the source list and select any that are of this ID.
            }
            return;
        }

        function setValues(ctrlSource, SetID) {
            var Source = document.getElementById(ctrlSource);
            if (Source != null) {
                for (i = 0; i < Source.options.length; i++) {
                    if (Source.options[i].value == SetID) {
                        Source.selectedIndex = i;
                    }
                }
            }
        }

        function PrintScanCodes(ID, Project) {
            var xDataList = {};
            if (ID == null || ID == 0) {
                return;
            }
            xDataList["Table"] = "Process";
            xDataList["ID"] = ID;
            xDataList["PROJECT"] = Project;
            xDataList["ShowComma"] = "0";
            var pstring = GetParameterStream(xDataList);
            var WindowToOpen = "/Reports/RPT_ProjectScanCodeList.aspx";
            if (pstring.length > 0) {
                WindowToOpen = WindowToOpen + "?" + pstring
            }
            var win = window.open(WindowToOpen, "_blank", "", true);
        }

        function GetParameterStream(ParmameterList) {
            var count = 0;
            var sb = new Sys.StringBuilder();
            for (var property in ParmameterList) {
                if (count > 0) { sb.append("&"); }
                sb.append(property + "=" + ParmameterList[property]);
                count += 1;
            }
            return sb.toString();
        }

    </script>
</asp:Content>
