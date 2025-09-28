<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Maint_Question.aspx.cs" Inherits="BW_WebApp.Maintenance.Maint_Question" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="cMC" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlMainView" runat="server">
                <h1><asp:Label ID="lblRecordTitle" runat="server" Text="Maintenance Question Definition" /></h1>
                
                <h2>Test page - code study - Aman</h2>
                <asp:TabContainer ID="TabContainer1" CssClass="tab-container" runat="server">
                    <asp:TabPanel ID="TabQuestion" CssClass="tab-panel" runat="server" HeaderText="Question">
                        <ContentTemplate>
                            <asp:Panel ID="Panel3" runat="server">
                                <label>Project:</label>
                                <asp:DropDownList ID="drpProjectList" CssClass="d-block w-md-50" runat="server" ToolTip="Project" AutoPostBack="True" />

                                <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
                                <asp:Button ID="btnEdit" runat="server" Text="Edit" Visible="False" OnClick="btnEdit_Click" />
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" Visible="False" OnClick="btnDelete_Click" />
                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Continue with Delete?" Enabled="True" TargetControlID="btnDelete" />
                                <asp:Button ID="btnPrintQuestion" runat="server" Text="Print Question" />
                                <asp:Button ID="btnSetScanKey" runat="server" Text="Set ALL blank Scan Keys" Visible="False" />
                                <asp:Button ID="btnPrint" runat="server" Text="Print Selected Scan Keys" />

                                <asp:CheckBox ID="chkIncludeComma" runat="server" Text="Include trailing comma" ToolTip="Check box to include a trailing Comma" />
                            </asp:Panel>

                            <asp:Panel ID="pnlMainGrid" runat="server" Style="overflow: auto; max-height: 400px;">
                                <asp:GridView ID="MainGrid" CssClass="table" runat="server" AutoGenerateSelectButton="True" DataKeyNames="QuestionID" AutoGenerateColumns="False">
                                    <SelectedRowStyle CssClass="srowstyle" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="QuestionID" HeaderText="ID" ReadOnly="True" Visible="false" />
                                        <asp:BoundField DataField="Name" HeaderText="Name">
                                            <ItemStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Description" HeaderText="Question" />
                                        <asp:BoundField DataField="Status" HeaderText="Status" />
                                        <asp:BoundField DataField="Type" HeaderText="Type" />
                                        <asp:BoundField DataField="Sequence" HeaderText="Seq" />
                                        <%--<asp:BoundField DataField="IFS_Condition" HeaderText="Condtion" />
                                        <asp:BoundField DataField="IFS_Condition_Sequence" HeaderText="IFS C Seq" />--%>
                                        <asp:BoundField DataField="IsHeaderQuestion" HeaderText="H" />
                                        <asp:BoundField DataField="IsManditory" HeaderText="M" />
                                        <asp:BoundField DataField="IsReadOnly" HeaderText="R" />
                                        <%--<asp:TemplateField HeaderText="P">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="imgPrint" runat="server" ToolTip="Print Question Scancode Log">
                                                    <span class="oi oi-print"></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>

                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabAnswer" CssClass="tab-panel" runat="server" HeaderText="Answers">
                        <ContentTemplate>
                            <asp:Panel ID="pnlChild" runat="server" Visible="false">
                                <asp:Panel ID="Panel2" runat="server">
                                    <div class="row">
                                        <div class="col-md-8">
                                            <h5><asp:Label ID="lblQuestionText" runat="server" Text="Question Answers" /></h5>
                                        </div>
                                        <div class="col-md text-md-right">
                                            <asp:Button ID="btnAddOption" runat="server" Text="Add" OnClick="btnAddOption_Click" />
                                            <asp:Button ID="btnEditOption" runat="server" Text="Edit" Visible="False" OnClick="btnEditOption_Click" />
                                            <asp:Button ID="btnDeleteOption" runat="server" Text="Delete" Visible="False" OnClick="btnDeleteOption_Click" />
                                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Continue with Delete?"
                                                Enabled="True" TargetControlID="btnDeleteOption">
                                            </asp:ConfirmButtonExtender>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="xx" runat="server" Style="overflow: auto; max-height: 350px;">
                                    <asp:GridView ID="ChildGrid" CssClass="table" runat="server" AutoGenerateSelectButton="True" DataKeyNames="OptionID" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundField DataField="ScanKey" HeaderText="ScanKey">
                                                <ItemStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="MicroKey" HeaderText="MicroKey">
                                                <ItemStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="MacroKey" HeaderText="MacroKey">
                                                <ItemStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Name" HeaderText="Abbr">
                                                <ItemStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="OptionText" HeaderText="Answer" />
                                            <asp:BoundField DataField="Status" HeaderText="Status" />
                                            <asp:BoundField DataField="Type" HeaderText="Type" />
                                            <asp:BoundField DataField="Sequence" HeaderText="Seq" />
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </asp:Panel>

                            <hr>

                            <asp:TabContainer ID="TabContainer2" CssClass="tab-container" runat="server">
                                <asp:TabPanel ID="TabPanel1" CssClass="tab-panel" runat="server" HeaderText="Bucket Counter">
                                    <ContentTemplate>
                                        <asp:Panel ID="Panel5" runat="server">
                                            <asp:Panel ID="pnlMainBucket" runat="server">
                                                <asp:Panel ID="Panel1" runat="server">
                                                    <div class="row">
                                                        <div class="col-md-8">
                                                            <h5><asp:Label ID="lblBucket" Text="Bucket Counters" runat="server" /></h5>
                                                        </div>
                                                        <div class="col-md text-md-right">
                                                            <asp:Button ID="btnAddBucket" runat="server" Text="Add" OnClick="btnAddBucket_Click" />
                                                            <asp:Button ID="btnEditBucket" runat="server" Text="Edit" Visible="False" OnClick="btnEditBucket_Click" />
                                                            <asp:Button ID="btnDeleteBucket" runat="server" Text="Delete" Visible="False" OnClick="btnDeleteBucket_Click" />
                                                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server" ConfirmText="Continue with Delete?" Enabled="True" TargetControlID="btnDeleteBucket" />
                                                        </div>
                                                    </div>
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
                                    </ContentTemplate>
                                </asp:TabPanel>
                                <asp:TabPanel ID="TabMerge" CssClass="tab-panel" runat="server" HeaderText="Merge Utility">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlMergeUtility" runat="server" Visible="false">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:Panel ID="Panel7" runat="server">
                                                        <div class="row">
                                                            <div class="col-md-9">
                                                                <h5><asp:Label ID="lblMergeTitle" runat="server" Text="Merge selected attribute option below into the selected option above:" /></h5>
                                                            </div>
                                                            <div class="col-md text-md-right">
                                                                <%--<asp:Button ID="Button1" runat="server" Text="Add" OnClick="btnAddOption_Click" />--%>
                                                                <%--<asp:Button ID="Button1" runat="server" Text="Edit" Visible="true" OnClick="btnEditOption_Click" />--%>
                                                                <%--<asp:Button ID="Button4" runat="server" Text="Delete" Visible="true" OnClick="btnDeleteOption_Click" />--%>
                                                                <asp:Button ID="btnMergeClear" runat="server" Text="Clear" Visible="true" />
                                                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender4" runat="server" ConfirmText="Continue with Merge?" Enabled="True" TargetControlID="btnMergeGo" />
                                                                <asp:Button ID="btnMergeGo" runat="server" Text="Go" Visible="true" />
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel8" runat="server" Style="overflow: auto; max-height: 400px;">
                                                        <asp:GridView ID="ChildGrid_Merge" CssClass="table" runat="server" AutoGenerateSelectButton="False" DataKeyNames="OptionID" AutoGenerateColumns="False">
                                                            <SelectedRowStyle CssClass="srowstyle" />
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                                                        <asp:Label ID="lblMergeInto" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="ScanKey" HeaderText="ScanKey">
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="MicroKey" HeaderText="MicroKey">
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="MacroKey" HeaderText="MacroKey">
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Name" HeaderText="Name">
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="OptionText" HeaderText="Answer" />
                                                                <asp:BoundField DataField="Status" HeaderText="Status" />
                                                                <asp:BoundField DataField="Type" HeaderText="Type" />
                                                                <asp:BoundField DataField="Sequence" HeaderText="Seq" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:TabPanel>
                            </asp:TabContainer>
                        </ContentTemplate>
                    </asp:TabPanel>
                </asp:TabContainer>
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
                        <asp:TextBox ID="txtEditCount" runat="server"  />

                        <asp:CheckBox ID="chkEditSingle" CssClass="d-block" Text="Single" runat="server" />
                        <asp:CheckBox ID="chkEditBucket" CssClass="d-block" Text="Active" runat="server" />
                    </div>
                </div>
                <asp:Button ID="EditBucketOK" runat="server" Text="OK" onclick="EditBucketOK_Click" />
                <asp:Button ID="EditBucketCancel" runat="server" Text="Cancel" onclick="EditBucketCancel_Click" />
            </asp:Panel>

            <asp:Panel ID="pnlAdd" runat="server">
                <h1>Add Question Definition</h1>
                <div class="row">
                    <div class="col-md">
                        <label>Name:</label>
                        <asp:TextBox ID="AddName" runat="server" MaxLength="20" />

                        <label>Description:</label>
                        <asp:TextBox ID="AddDescription" runat="server" />

                        <label>Status:</label>
                        <asp:DropDownList ID="drpAddStatus" runat="server" ToolTip="Question Status" />

                        <label>Type:</label>
                        <asp:DropDownList ID="drpAddType" runat="server" ToolTip="Question Type" />

                        <label>Tool Tip:</label>
                        <asp:TextBox ID="AddToolTip" runat="server" MaxLength="100" TextMode="MultiLine" />
                    </div>
                    <div class="col-md">
                        <label>Table Name:</label>
                        <asp:TextBox ID="AddTableName" runat="server" />

                        <label>Sequence:</label>
                        <asp:TextBox ID="AddSequence" runat="server" />

                        <asp:CheckBox ID="AddIsHeaderQuestion" CssClass="d-block" Text="Show in Header" runat="server" />
                        <asp:CheckBox ID="AddIsManditory" CssClass="d-block" Text="Manditory" runat="server" />
                        <%--<asp:CheckBox ID="AddIsKeyQuestion" CssClass="d-block" Text="Key Question" runat="server" />--%>
                        <%--<asp:CheckBox ID="AddIFSCondition" CssClass="d-block" Text="IFS Condition" runat="server" />
                        <label>IFS Condition Sequence:</label>
                        <asp:TextBox ID="AddIFSConditionSequence" runat="server" />--%>
                        <asp:CheckBox ID="AddIsSearchable" CssClass="d-block" Text="Search-able" runat="server" />
                        <asp:CheckBox ID="AddShowVertical" CssClass="d-block" Text="Show Vertical" runat="server" />
                        <asp:CheckBox ID="AddisTimeSpread" CssClass="d-block" Text="Time Spread" runat="server" />
                        <asp:CheckBox ID="AddReadOnly" CssClass="d-block" Text="Read Only" runat="server" />

                        <label>Display Column:</label>
                        <asp:TextBox ID="AddDisplayColumn" runat="server" />

                        <%--<label>Bucket Count:</label>
                        <asp:TextBox ID="AddBucketCount" runat="server" />

                        <label>Bucket Count Offset:</label>
                        <asp:TextBox ID="AddBucketCountOffset" runat="server" />--%>
                    </div>
                </div>

                <asp:Button ID="AddQuestionOK" runat="server" Text="OK" onclick="AddQuestionOK_Click" />
                <asp:Button ID="AddQuestionCancel" runat="server" Text="Cancel" onclick="AddQuestionCancel_Click" />
            </asp:Panel>
            <asp:Panel ID="pnlEdit" runat="server">
                <h1>Edit Question Definition</h1>

                <div class="row">
                    <div class="col-md">
                        <label>Name:</label>
                        <asp:TextBox ID="EditName" runat="server" MaxLength="20" />
                        <asp:TextBox ID="EditKeyID" runat="server" ReadOnly="True" Visible="False" />

                        <label>Description:</label>
                        <asp:TextBox ID="EditDescription" runat="server" />

                        <label>Status:</label>
                        <asp:DropDownList ID="drpEditStatus" runat="server" ToolTip="Question Status" />

                        <label>Type:</label>
                        <asp:DropDownList ID="drpEditType" runat="server" ToolTip="Question Type" />

                        <label>Tool Tip:</label>
                        <asp:TextBox ID="EditToolTip" runat="server" MaxLength="100" TextMode="MultiLine" />
                    </div>
                    <div class="col-md">
                        <label>Table Name:</label>
                        <asp:TextBox ID="EditTableName" runat="server" />

                        <label>Sequence:</label>
                        <asp:TextBox ID="EditSequence" runat="server" />

                        <asp:CheckBox ID="EditIsHeaderQuestion" CssClass="d-block" Text="Show in Header" runat="server" />
                        <asp:CheckBox ID="EditIsManditory" CssClass="d-block" Text="Manditory" runat="server" />
                        <%--<asp:CheckBox ID="EditIsKeyQuestion" CssClass="d-block" Text="Key Question" runat="server" />--%>
                        <%--<asp:CheckBox ID="EditIFSCondition" CssClass="d-block" Text="IFS Condition" runat="server" />
                        <label>IFS Condition Sequence:</label>
                        <asp:TextBox ID="EditIFSConditionSequence" runat="server" />--%>
                        <asp:CheckBox ID="EditIsSearchable" CssClass="d-block" Text="Search-able" runat="server" />
                        <asp:CheckBox ID="EditShowVertical" CssClass="d-block" Text="Show Vertical" runat="server" />
                        <asp:CheckBox ID="EditisTimeSpread" CssClass="d-block" Text="Time Spread" runat="server" />
                        <asp:CheckBox ID="EditReadOnly" CssClass="d-block" Text="Read Only" runat="server" />

                        <label>Display Column:</label>
                        <asp:TextBox ID="EditDisplayColumn" runat="server" />

                        <%--<label>Bucket Count:</label>
                        <asp:TextBox ID="EditBucketCount" runat="server" />

                        <label>Bucket Count Offset:</label>
                        <asp:TextBox ID="EditBucketCountOffset" runat="server" />--%>
                    </div>
                </div>

                <asp:Button ID="EditQuestionOK" runat="server" Text="OK" onclick="EditQuestionOK_Click" />
                <asp:Button ID="EditQuestionCancel" runat="server" Text="Cancel" onclick="AddQuestionCancel_Click" />
            </asp:Panel>

            <asp:Panel ID="pnlAddOption" runat="server">
                <h1>Add Answer</h1>

                <div class="row">
                    <div class="col-md">
                        <label>ScanKey:</label>
                        <asp:TextBox ID="AddScanKeyOption" runat="server" Enabled="False" />

                        <label>MicroKey:</label>
                        <asp:TextBox ID="AddMicroKey" runat="server" MaxLength="10" />

                        <label>MacroKey:</label>
                        <asp:TextBox ID="AddMacroKey" runat="server" MaxLength="2" />

                        <label>Abbreviation:</label>
                        <asp:TextBox ID="AddNameOption" runat="server" MaxLength="20" />

                        <label>Answer:</label>
                        <asp:TextBox ID="AddDescriptionOption" runat="server" MaxLength="50" />
                    </div>
                    <div class="col-md">
                        <label>Status:</label>
                        <asp:DropDownList ID="drpAddStatusOption" runat="server" ToolTip="Question Status" />

                        <label>Type:</label>
                        <asp:DropDownList ID="drpAddTypeOption" runat="server" ToolTip="Question Type" />

                        <label>Sequence:</label>
                        <asp:TextBox ID="AddSequenceOption" runat="server" />

                        <label>Master Client (IDC use):</label>
                        <asp:DropDownList ID="drpAddIDCMasterClient" runat="server">
                            <asp:ListItem Text="All" Value="0" />
                            <asp:ListItem Text="EMP" Value="1" />
                            <asp:ListItem Text="A001" Value="2" />
                            <asp:ListItem Text="A002" Value="3" />
                        </asp:DropDownList>

                        <label>Friendly Description (IDC use):</label>
                        <asp:TextBox ID="AddFriendlyName" runat="server" />

                    </div>
                </div>

                <asp:Button ID="AddOptionOK" runat="server" Text="OK" onclick="AddOptionOK_Click" />
                <asp:Button ID="AddOptionCancel" runat="server" Text="Cancel" onclick="AddOptionCancel_Click" />
            </asp:Panel>
            <asp:Panel ID="pnlEditOption" runat="server">
                <h1>Edit Answer</h1>

                <div class="row">
                    <div class="col-md">
                        <label>ScanKey:</label>
                        <asp:TextBox ID="EditScanKey" runat="server" Enabled="False" />

                        <label>MicroKey:</label>
                        <asp:TextBox ID="EditMicroKey" runat="server" MaxLength="10" />

                        <label>MacroKey:</label>
                        <asp:TextBox ID="EditMacroKey" runat="server" MaxLength="2" />

                        <label>Abbreviation:</label>
                        <asp:TextBox ID="EditNameOption" runat="server" MaxLength="20" />
                        <asp:TextBox ID="EditKeyIDOption" runat="server" ReadOnly="True" Visible="False" />

                        <label>Answer:</label>
                        <asp:TextBox ID="EditDescriptionOption" runat="server" MaxLength="50" />
                    </div>
                    <div class="col-md">
                        <label>Status:</label>
                        <asp:DropDownList ID="drpEditStatusOption" runat="server" ToolTip="Question Status" />

                        <label>Type:</label>
                        <asp:DropDownList ID="drpEditTypeOption" runat="server" ToolTip="Question Type" />

                        <label>Sequence:</label>
                        <asp:TextBox ID="EditSequenceOption" runat="server" />

                        <label>Master Client (IDC use):</label>
                        <asp:DropDownList ID="drpEditIDCMasterClient" runat="server">
                            <asp:ListItem Text="All" Value="0" />
                            <asp:ListItem Text="EMP" Value="1" />
                            <asp:ListItem Text="A001" Value="2" />
                            <asp:ListItem Text="A002" Value="3" />
                        </asp:DropDownList>

                        <label>Friendly Description (IDC use):</label>
                        <asp:TextBox ID="EditFriendlyName" runat="server" />
                    </div>
                </div>

                <asp:Button ID="EditOptionOK" runat="server" Text="OK" onclick="EditOptionOK_Click" />
                <asp:Button ID="EditOptionCancel" runat="server" Text="Cancel" onclick="EditOptionCancel_Click" />
            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="js" runat="server">
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




        //           function pageLoad() {
        //               var manager = Sys.WebForms.PageRequestManager.getInstance();
        //               manager.add_endRequest(endRequest);
        //           }

        //           function endRequest(sender, args) {
        //               window.scrollTo(0, 0);
        //           }


        function goTopofScreen() {
            window.scrollTo(0, 0);
        }



        //           function PrintScanCodes(IDName, QuestionText) {
        //               var xDataList = {};
        //               xDataList["Table"] = "Question";
        //               xDataList["ID"] = IDName;
        //               xDataList["PROJECT"] = QuestionText;

        //               var pstring = GetParameterStream(xDataList);
        //               var WindowToOpen = "RPT_ProjectScanCodeList.aspx";
        //               if (pstring.length > 0) {
        //                   WindowToOpen = WindowToOpen + "?" + pstring
        //               }
        //               var win = window.open(WindowToOpen, "_blank", "", true);
        //           }




        function PrintQuestionCodes() {
            var xDataList = {};
            if (SelectedList.length == 0) {
                return;
            }
            //               xDataList["Table"] = "Questions";
            xDataList["ID"] = SelectedList.replace(/,,/g, ',');
            //               xDataList["PROJECT"] = "";
            xDataList["RPT"] = "Question";

            xDataList["CMD"] = "exec GetReport_Question '" + xDataList["ID"] + "'";
            //               xDataList["fName"] = FileName;
            //               xDataList["fList"] = FieldList;
            //               //           var pstring = GetParameterStream(GetReportParameterList("Bagtag"));
            var pstring = GetParameterStream(xDataList);
            // var WindowToOpen = "RPT_SpotCountReport.aspx";
            var WindowToOpen = "/Reports/RPT_EXCEL_Out.aspx";
            if (pstring.length > 0) {
                WindowToOpen = WindowToOpen + "?" + pstring
            }
            var win = window.open(WindowToOpen, "_blank", "menubar", true);
            return;



        }




        function PrintScanCodes() {
            var xDataList = {};
            if (SelectedList.length == 0) {
                return;
            }
            xDataList["Table"] = "Questions";
            xDataList["ID"] = SelectedList.replace(/,,/g, ',');
            xDataList["PROJECT"] = "";
            if ($get("<%= chkIncludeComma.ClientID %>").checked == true) {
                xDataList["ShowComma"] = "1";
            }
            else {
                xDataList["ShowComma"] = "0";
            }
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



        /////////////////////////////////////


        var SelectedList = '';

        function CheckboxChecked(ID) {
            AddtoSelectList(ID);
            //    alert(SelectedList.replace(/,,/g,','));
        }

        function AddtoSelectList(ID) {
            var oString = ',' + ID + ',';
            var FoundAt = SelectedList.indexOf(oString);
            if (FoundAt >= 0) {
                SelectedList = SelectedList.replace(oString, '');
                //        SelectedList = SelectedList.replace(ID, '');
                return;
            }
            if (FoundAt < 0) {
                SelectedList += oString;
                return;
            }
        }



        var gridViewCtlId = '<%=MainGrid.ClientID%>';
        var gridViewCtl = null;
        var curSelRow = null;
        var curRowIdx = -1;

        function getGridViewControl() {
            if (null == gridViewCtl) {
                gridViewCtl = document.getElementById(gridViewCtlId);
            }
        }

        function onGridViewRowSelected(rowIdx) {
            var selRow = getSelectedRow(rowIdx);
            if (null != selRow) {
                curSelRow = selRow;
                var cellValue = getCellValue(rowIdx, 0);
                alert(cellValue);
            }
        }

        function getSelectedRow(rowIdx) {
            return getGridRow(rowIdx);
        }

        function getGridRow(rowIdx) {
            getGridViewControl();
            if (null != gridViewCtl) {
                return gridViewCtl.rows[rowIdx];
            }
            return null;
        }

        function getGridColumn(rowIdx, colIdx) {
            var gridRow = getGridRow(rowIdx);
            if (null != gridRow) {
                return gridRow.cells[colIdx];
            }
            return null;
        }

        function getCellValue(rowIdx, colIdx) {
            var gridCell = getGridColumn(rowIdx, colIdx);
            if (null != gridCell) {
                return gridCell.innerText;
            }
            return null;
        }



    </script>
</asp:Content>

