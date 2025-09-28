<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Maint_Process_BKUP.aspx.cs" Inherits="BW_WebApp.Maintenance.Maint_Process_BKUP" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>
<%@ Register Assembly="Syncfusion.Grid.Grouping.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Grid.Grouping" TagPrefix="syncfusion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cMC" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdnUserName" runat="server" />
            <asp:Panel ID="pnlMainView" runat="server">
                <asp:Panel ID="pnldrpProject" runat="server">
                    <h1><asp:Label ID="lblRecordTitle" runat="server" Text="Maintenance Process Definition" /></h1>
                    <label>Project:</label>
                    <asp:DropDownList ID="drpProjectList" CssClass="w-md-50 d-block" runat="server" ToolTip="Project" AutoPostBack="True" />
                    <%--<asp:Label ID="lblMisc" runat="server"/>--%>
                </asp:Panel>

                <asp:Panel ID="Panel3" runat="server">
                    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
                    <asp:Button ID="btnEdit" runat="server" Text="Edit" Visible="False" OnClick="btnEdit_Click" />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" Visible="False" OnClick="btnDelete_Click" />
                    <asp:Button ID="btnQuestion" runat="server" Text="Question" Visible="False" ToolTip="Attach Questions to this Process" OnClick="btnProcesss_Click" />
                    <asp:Button ID="btnNextMove" runat="server" Text="Next Move" Visible="False" ToolTip="Attach Next Move to this Process" OnClick="btnNextMove_Click" />
                    <asp:Button ID="btnPrintScanCodes" runat="server" Text="Print Scan Codes" Visible="False" />
                    <%--<asp:Button ID="btnBinLocation" runat="server" Text="Bin(s)" Visible="False" ToolTip="Assign suggested bin location" OnClick="btnBinLocation_Click" />--%>
                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Continue with Delete?" Enabled="True" TargetControlID="btnDelete" />


                    <asp:TextBox ID="txtSelectChoice" runat="server"></asp:TextBox>

                </asp:Panel>

                <asp:Panel ID="pnlMainGrid" runat="server" Style="overflow: auto; max-height: 500px;">
                    <asp:GridView ID="MainGrid" CssClass="table" runat="server" AutoGenerateSelectButton="True" DataKeyNames="ProcessID" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="ProcessID" HeaderText="ID" ReadOnly="True" Visible="false" />
                            <asp:BoundField DataField="ScanKey" HeaderText="ScanKey">
                                <ItemStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MacroKey" HeaderText="MacroKey">
                                <ItemStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ButtonText" HeaderText="Button">
                                <ItemStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="RMASuffix" HeaderText="Suffix">
                                <ItemStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Name" HeaderText="Name">
                                <ItemStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Description" HeaderText="Process" />
                            <asp:BoundField DataField="Description_Client" HeaderText="Description_Client" />
                            <asp:BoundField DataField="Status" HeaderText="Status" />
                            <asp:BoundField DataField="ShowTAT" HeaderText="Show TAT" />
                            <asp:BoundField DataField="CanJumpProject" HeaderText="JP" />
                            <asp:BoundField DataField="TurnStickyOn" HeaderText="ST" />
                            <asp:BoundField DataField="Sequence" HeaderText="Seq" />
                            <asp:BoundField DataField="ShowCompletedStatus" HeaderText="S" />
                            <asp:BoundField DataField="IFSDirectiveType" HeaderText="IFS Directive" />
                            <%--<asp:BoundField DataField="MinutesToYellow" HeaderText="Yellow" />
                            <asp:BoundField DataField="MinutesToRed" HeaderText="Red" />--%>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>

                <hr>
                <asp:Panel ID="pnlMainBucket" runat="server">
                    <asp:Panel ID="Panel1" runat="server">
                        <h3><asp:Label ID="lblBucket" Text="Bucket Counters" runat="server" /></h3>
                        <asp:Button ID="btnAddBucket" runat="server" Text="Add" OnClick="btnAddBucket_Click" />
                        <asp:Button ID="btnEditBucket" runat="server" Text="Edit" Visible="False" OnClick="btnEditBucket_Click" />
                        <asp:Button ID="btnDeleteBucket" runat="server" Text="Delete" Visible="False" OnClick="btnDeleteBucket_Click" />
                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server" ConfirmText="Continue with Delete?" Enabled="True" TargetControlID="btnDeleteBucket" />
                    </asp:Panel>
                    <asp:Panel ID="Panel4" runat="server" Style="overflow: auto; max-height: 400px;">
                        <asp:GridView ID="grdMasterBucketTransaction" CssClass="table" runat="server" AutoGenerateSelectButton="True" DataKeyNames="MasterBucketTransactionsID" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="MasterBucketTransactionsID" HeaderText="ID" ReadOnly="True" Visible="false" />
                                <asp:BoundField DataField="Action" HeaderText="Action">
                                    <ItemStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Count" HeaderText="Count">
                                    <ItemStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OneOnly" HeaderText="Single">
                                    <ItemStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Active" HeaderText="Active">
                                    <ItemStyle Wrap="False" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </asp:Panel>
            </asp:Panel>

            <asp:Panel ID="pnlAdd" runat="server">
                <h1>Add Process Definition</h1>
                <div class="row">
                  <div class="col-md">
                        <label>Scan Key:</label>
                        <asp:TextBox ID="AddScanKey" runat="server" MaxLength="50" />

                        <label>Macro Key:</label>
                        <asp:TextBox ID="AddMacroKey" runat="server" MaxLength="2" />

                        <label>RMA Suffix:</label>
                        <asp:TextBox ID="AddRMASuffix" runat="server" MaxLength="2" />

                        <label>Name:</label>
                        <asp:TextBox ID="AddName" runat="server" MaxLength="20" />

                        <label>Button Text:</label>
                        <asp:TextBox ID="AddButton" runat="server" MaxLength="25" />

                        <label>Description:</label>
                        <asp:TextBox ID="AddDescription" runat="server" MaxLength="50" />

                        <label>Description_Client:</label>
                        <asp:TextBox ID="AddDescription_Client" runat="server" MaxLength="50" />
                    </div>
                  <div class="col-md">
                        <label>Status:</label>
                        <asp:DropDownList ID="drpAddStatus" runat="server" ToolTip="Process Status" />

                        <label>Sequence:</label>
                        <asp:TextBox ID="AddSequence" runat="server" />

                        <label>Minutes To Yellow:</label>
                        <asp:TextBox ID="AddMinutesToYellow" runat="server" MaxLength="50" />

                        <label>Minutes To Red:</label>
                        <asp:TextBox ID="AddMinutesToRed" runat="server" MaxLength="50" />

                        <label>IFS Directive:</label>
                        <asp:TextBox ID="AddIFSDirectiveType" runat="server" />

                        <div class="form-row">
                          <div class="col-lg">
                                <asp:CheckBox ID="AddShowCompletedStatus" CssClass="d-block" Text="Show completed status" runat="server" />
                                <asp:CheckBox ID="AddisReadOnly" CssClass="d-block" Text="Read only" runat="server" />
                                <asp:CheckBox ID="AddShowTat" CssClass="d-block" Text="Show TAT" runat="server" />
                                <asp:CheckBox ID="AddCanJumpProject" CssClass="d-block" Text="Can Jump Projects" runat="server" />
                            </div>
                          <div class="col-lg">
                                <asp:CheckBox ID="AddTurnStickyOn" CssClass="d-block" Text="Turn Static On" runat="server" />
                                <asp:CheckBox ID="chkAddDisablePrint" CssClass="d-block" Text="Disable Print Button" runat="server" />
                                <asp:CheckBox ID="chkAddForcePrintOnSave" CssClass="d-block" Text="Force Print on Save" runat="server" />
                                <asp:CheckBox ID="AddAllowXBINX" CssClass="d-block" Text="Allow XBINX" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
                <asp:Button ID="AddProcessOK" runat="server" Text="OK" onclick="AddProcessOK_Click" />
                <asp:Button ID="AddProcessCancel" runat="server" Text="Cancel" onclick="AddProcessCancel_Click" />
            </asp:Panel>

            <asp:Panel ID="pnlEdit" runat="server">
                <h1>Edit Process Definition</h1>
                <div class="row">
                  <div class="col-md">
                        <label>Scan Key:</label>
                        <asp:TextBox ID="EditScanKey" runat="server" MaxLength="50" />

                        <label>Macro Key:</label>
                        <asp:TextBox ID="EditMacroKey" runat="server" MaxLength="2" />

                        <label>RMA Suffix:</label>
                        <asp:TextBox ID="EditRMASuffix" runat="server" MaxLength="2" />

                        <label>Name:</label>
                        <asp:TextBox ID="EditName" runat="server" MaxLength="20" />
                        <asp:TextBox ID="EditKeyID" runat="server" ReadOnly="True" Visible="False" />

                        <label>Button Text:</label>
                        <asp:TextBox ID="EditButton" runat="server" MaxLength="25" />

                        <label>Description:</label>
                        <asp:TextBox ID="EditDescription" runat="server" MaxLength="50" />

                        <label>Description_Client:</label>
                        <asp:TextBox ID="EditDescription_Client" runat="server" MaxLength="50" />
                    </div>
                  <div class="col-md">
                        <label>Status:</label>
                        <asp:DropDownList ID="drpEditStatus" runat="server" ToolTip="Process Status" />

                        <label>Sequence:</label>
                        <asp:TextBox ID="EditSequence" runat="server" />

                        <label>Minutes To Yellow:</label>
                        <asp:TextBox ID="EditMinutesToYellow" runat="server" MaxLength="50" />

                        <label>Minutes To Red:</label>
                        <asp:TextBox ID="EditMinutesToRed" runat="server" MaxLength="50" />

                        <label>IFS Directive:</label>
                        <asp:TextBox ID="EditIFSDirectiveType" runat="server" />

                        <div class="form-row">
                          <div class="col-lg">
                                <asp:CheckBox ID="EditShowCompletedStatus" CssClass="d-block" Text="Show completed status" runat="server" />
                                <asp:CheckBox ID="EditisReadOnly" CssClass="d-block" Text="Read only" runat="server" />
                                <asp:CheckBox ID="EditShowTat" CssClass="d-block" Text="Show TAT" runat="server" />
                                <asp:CheckBox ID="EditCanJumpProject" CssClass="d-block" Text="Can Jump Projects" runat="server" />
                            </div>
                          <div class="col-lg">
                                <asp:CheckBox ID="EditTurnStickyOn" CssClass="d-block" Text="Turn Static On" runat="server" />
                                <asp:CheckBox ID="chkEditDisablePrint" CssClass="d-block" Text="Disable Print Button" runat="server" />
                                <asp:CheckBox ID="chkEditForcePrintOnSave" CssClass="d-block" Text="Force Print on Save" runat="server" />
                                <asp:CheckBox ID="EditAllowXBINX" CssClass="d-block" Text="Allow XBINX" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
                <asp:Button ID="EditProcessOK" runat="server" Text="OK" onclick="EditProcessOK_Click" />
                <asp:Button ID="EditProcessCancel" runat="server" Text="Cancel" onclick="EditProcessCancel_Click" />
            </asp:Panel>

            <asp:Panel ID="pnlAddBucket" runat="server" Visible="False">
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

            <asp:Panel ID="pnlEditBucket" runat="server" Visible="False">
                <h1>Edit Process Bucket</h1>
                <div class="row">
                  <div class="col-md-6">
                        <label>Action:</label>
                        <asp:TextBox ID="txtEditAction" runat="server" MaxLength="20" />

                        <label>Count:</label>
                        <asp:TextBox ID="txtEditCount" runat="server" />

                        <asp:CheckBox ID="chkEditSingle" CssClass="d-block" Text="Single" runat="server" />
                        <asp:CheckBox ID="chkEditBucket" CssClass="d-block" Text="Active" runat="server" />
                    </div>
                </div>
                <asp:Button ID="EditBucketOK" runat="server" Text="OK" onclick="EditBucketOK_Click" />
                <asp:Button ID="EditBucketCancel" runat="server" Text="Cancel" onclick="EditBucketCancel_Click" />
            </asp:Panel>

            <asp:Panel ID="pnlProcessAnswer" runat="server">
                <h1>Answer List</h1>
                <h5><asp:Label ID="lblAnswerList" runat="server" Text="NextMove" /></h5>

                <div class="row">
                  <div class="col">
                        <label>Source:</label>
                        <asp:ListBox ID="lstQuestionSource" runat="server" SelectionMode="Multiple" ViewStateMode="Inherit" ClientIDMode="Static" />
                    </div>
                  <div class="col-auto align-self-end">
                        <asp:Button ID="btnRight" CssClass="w-100" runat="server" Text=">" OnClientClick="MoveItem('lstQuestionSource','lstQuestionTarget');return false;" />
                        <asp:Button ID="btnLeft" CssClass="w-100" runat="server" Text="<" OnClientClick="MoveItem('lstQuestionTarget','lstQuestionSource');return false;" />
                    </div>
                  <div class="col">
                        <label>Target:</label>
                        <asp:ListBox ID="lstQuestionTarget" runat="server" ClientIDMode="Static" SelectionMode="Multiple" />
                    </div>
                </div>

                <asp:Button ID="btnSave" runat="server" Text="OK" OnClick="btnSave_Click" OnClientClick="GatherKeys('lstQuestionTarget','HiddenQuestionIDs');" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />

                <hr>

                <asp:DropDownList ID="drpProcessList" CssClass="w-md-50 d-block" runat="server" ToolTip="Process to Copy" />
                <asp:Button ID="btnCopyFrom" runat="server" Text="Copy from Process" />

                <asp:HiddenField ID="HiddenQuestionIDs" runat="server" ClientIDMode="Static" />
            </asp:Panel>

            <asp:Panel ID="pnlProcessNextMove" runat="server">
                <h1>Process Next Move List:</h1>
                <h5><asp:Label ID="lblNextMove" runat="server" Text="NextMove" /></h5>

                <div class="row">
                  <div class="col">
                        <label>Source:</label>
                        <asp:ListBox ID="lstNextMoveFrom" runat="server" SelectionMode="Multiple" ViewStateMode="Inherit" ClientIDMode="Static" />
                    </div>
                  <div class="col-auto align-self-end">
                        <asp:Button ID="Button1" CssClass="w-100" runat="server" Text=">" OnClientClick="MoveItem('lstNextMoveFrom','lstNextMoveTo'); return false;" />
                        <asp:Button ID="Button2" CssClass="w-100" runat="server" Text="<" OnClientClick="MoveItem('lstNextMoveTo','lstNextMoveFrom'); return false;" />
                    </div>
                  <div class="col">
                        <label>Target:</label>
                        <asp:ListBox ID="lstNextMoveTo" runat="server" ClientIDMode="Static" SelectionMode="Multiple" />
                    </div>
                </div>

                <asp:Button ID="btnNextMoveOK" runat="server" Text="OK" onclick="btnNextMoveOK_Click" OnClientClick="GatherKeys('lstNextMoveTo','HiddenProcessNextStepIDs');" />
                <asp:Button ID="btnNextMoveCancel" runat="server" Text="Cancel" onclick="btnNextMoveCancel_Click" />
                <asp:HiddenField ID="HiddenProcessNextStepIDs" runat="server" ClientIDMode="Static" />
            </asp:Panel>

            <asp:Panel ID="pnlProcessBinLocation" runat="server">
                <h1>Process Bin Location List:</h1>
                <h5><asp:Label ID="lblBinLocation" runat="server" /></h5>

                <div class="row">
                  <div class="col">
                        <label>Source:</label>
                        <asp:ListBox ID="lstBinMoveFrom" runat="server" SelectionMode="Multiple" ViewStateMode="Inherit" ClientIDMode="Static" />
                    </div>
                  <div class="col-auto align-self-end">
                        <asp:Button ID="Button3" CssClass="w-100" runat="server" Text=">" OnClientClick="MoveItem('lstBinMoveFrom','lstBinMoveTo');return false;" />
                        <asp:Button ID="Button4" CssClass="w-100" runat="server" Text="<" OnClientClick="MoveItem('lstBinMoveTo','lstBinMoveFrom');return false;" />
                    </div>
                  <div class="col">
                        <label>Target:</label>
                        <asp:ListBox ID="lstBinMoveTo" runat="server" ClientIDMode="Static" SelectionMode="Multiple" />
                    </div>
                </div>

                <%--<asp:Button ID="btnBinLocationOK" runat="server" Text="OK" onclick="btnBinLocationOK_Click" OnClientClick="GatherKeys('lstBinMoveTo','HiddenProcessBinLocationIDs');" />
                <asp:Button ID="btnBinLocationCancel" runat="server" Text="Cancel" onclick="btnBinLocationCancel_Click" />
                <asp:HiddenField ID="HiddenProcessBinLocationIDs" runat="server" ClientIDMode="Static" />--%>
            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="js" runat="server">
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


