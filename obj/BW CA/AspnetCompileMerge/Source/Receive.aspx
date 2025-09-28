<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Receive.aspx.cs" Inherits="BW_WebApp.Receive" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>
<%@ Register Assembly="Syncfusion.Tools.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Tools" TagPrefix="syncfusion" %>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="cMC" runat="server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
        <Scripts>
            <asp:ScriptReference Path="~/Scripts/ReceiveSpecific.js" NotifyScriptLoaded="true" />
        </Scripts>
    </asp:ScriptManagerProxy>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <asp:Panel ID="pnlHeader_01" runat="server">
                <asp:HiddenField ID="hdnForcePrintOnSave" runat="server" ClientIDMode="Static" Value="" />
                <asp:HiddenField ID="hdnRoleList" runat="server" ClientIDMode="Static" Value="" />
                <asp:HiddenField ID="hdnForceLoadID" runat="server" ClientIDMode="Static" Value="" />
                <asp:HiddenField ID="hdnOpenTime" runat="server" ClientIDMode="Static" Value="" />
                <asp:HiddenField ID="hdnForceLoadPID" runat="server" ClientIDMode="Static" Value="" />
                <asp:HiddenField ID="hdnForceLoadProcessName" runat="server" ClientIDMode="Static" Value="" />
                <asp:HiddenField ID="hdnIsThreadedSave" runat="server" ClientIDMode="Static" Value="N" />
                <asp:HiddenField ID="HdnAuthorizeRequired" runat="server" ClientIDMode="Static" Value="N" />
                <asp:HiddenField ID="HdnClientLocationEmail" runat="server" ClientIDMode="Static" Value="" />
                <asp:HiddenField ID="HdnClientLocationEmail2" runat="server" ClientIDMode="Static" Value="" />
                <asp:HiddenField ID="hdnisSecondaryProjectOverride" runat="server" ClientIDMode="Static" Value="N" />
                <asp:HiddenField ID="hdnDoAuthorize" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnStickyData" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnAllowXBINX" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnCalledFrom" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnIsIMEIBulk" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnManditoryFields" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnQuestionIDList" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnQuestionClientIDList" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnAllowProjectPassThrough" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnDealerPortal" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnMiscDialogueAnswer" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnAllowDupAdd" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="HdnKeepUnitActive" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="HdnHeaderData" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="HdnCurrentProcess" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnSaveProcessID" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="HdnCurrentProcessID" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="HdnNextProcess" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="HdnNextProcessID" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="HdnNextStep" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="HdnNextStepID" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnReceiveHeaderID" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnReceiveDetailBulkID" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnReceiveDetailID" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnLastESN" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnLastESNVersion" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="txtESNVersion" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnSearchReturnMode" runat="server" ClientIDMode="Static" Value="" />
                <asp:HiddenField ID="hdnUserName" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnStepUp" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnClientLocationID" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnProjectDependencies" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnProcessDependencies" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnSourceOrTarget" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnIsProcessReadOnly" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnAllowVersionFind" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnisMasterLinked" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnCarrierIDx" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnManufacturerIDx" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnModelIDx" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnMemoryIDx" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnColourIDx" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnClientIDx" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnPartNumberIDs" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnPONumberIDs" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnPOVendorIDs" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnPOLineNumberIDs" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnPOUnnitCostIDs" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnBinID" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnLabDestinationID" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnCarrierID" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnManufacturerID" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnModelID" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnColourID" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnMemoryID" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnOptionKeyReplaceIMEI" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="isClientScreen" runat="server" ClientIDMode="Static" />
                <%--<asp:HiddenField ID="hdnCheckBinNumber" runat="server" ClientIDMode="Static" />--%>

                <h1><asp:Label runat="server" ID="lblProcessHeader" /></h1>

                <div class="row">
                	<div class="col-md-6">
                        <asp:DropDownList ID="drpProjectList" runat="server" ToolTip="Project" AutoPostBack="True"/>
                        <asp:HiddenField ID="HdnProjSetup" runat="server" />
                   </div>
                </div>
                	
                <asp:Button ID="btnBagTag" runat="server" Text="Print Form ++" />
                <asp:Button ID="btnClearData" runat="server" Text="Clear --" />
                <asp:Button ID="btnSave" runat="server" Text="Save **" />
                <asp:Button ID="btnTSave" runat="server" Text="TSave" Enabled="False" />
                <asp:Button ID="btnCheckBin" runat="server" Text="Check Bin" OnClientClick="ShowBinReport(); return false;" />
                <asp:Button ID="btnUnitView" runat="server" Text="Unit View" OnClientClick="ShowUnitViewReport(); return false;" />
                <asp:Button ID="btnSwitch" runat="server" Text="Switch" ToolTip="Switch this unit with another" OnClientClick="SwitchIMEI(); return false;" />
                <asp:Button ID="btnNextProcess" runat="server" Text="P" style="display:none" />
                <asp:Button ID="btnJumpProject" runat="server" Text="P" style="display:none" />
                <asp:Button ID="btnShowTimeLog" runat="server" Text="Log" OnClientClick="ShowLogReport(); return false;" />
                <asp:Button ID="btnClearLog" runat="server" Text="Clear Log" OnClientClick="ClearLogReport(); return false;" />
                <asp:Label ID="lblSessionTime" runat="server" ToolTip="Number of seconds inactivity before a new login is required." />
                <asp:Label ID="lblCarrierLock" runat="server" ToolTip="Is the Carrier manufacturer etc Linked?" />
            </asp:Panel>

            <hr>

            <div class="row">
            	<div class="col-md-3 col-lg-2">

                    <!-- JIM: Please swap these buttons for a dropdown. I will need to do some slight modifications afterwards -->
                    <asp:Repeater ID="ViewProcess" runat="server">
                        <ItemTemplate>
                            <asp:Button ID="btnViewProcess" CssClass="w-md-100 ws-normal" runat="server" UseSubmitBehavior="False" />
                        </ItemTemplate>
                    </asp:Repeater>

                    <hr>
                    
                    <style>
                        .ajax__calendar_container, .ajax__calendar_body, .ajax__calendar_days, .ajax__calendar_months, .ajax__calendar_years {
                                width: 250px !important;
                                background: White;
                            }
                             .ajax__calendar_dayname
                            {
                                width: auto !important;
                                }
                    </style>
                    <div class="form-row">
                    	<div class="col col-md-12">
                            <asp:TextBox ID="txtHistoryCount" runat="server" ReadOnly="True" Text="0" />
                        </div>
                    	<div class="col col-md-12">
                             <asp:Button ID="btnDelete" CssClass="w-100" runat="server" Text="Delete" UseSubmitBehavior="False" ClientIDMode="Static"
                                OnClientClick='DeleteHistory(); return false;' ToolTip="Delete Selected History Item"/>
                        </div>
                    	<div class="col col-md-12">
                            <asp:Button ID="btnRemoveAll" CssClass="w-100" runat="server" Text="Reset" UseSubmitBehavior="False" ClientIDMode="Static"
                                OnClientClick='ResetHistory(); return false;' ToolTip="Reset History List" />
                        </div>
                    </div>
                    
                    <asp:ListBox ID="lstHistory" runat="server" SelectionMode="Single" ViewStateMode="Inherit" ClientIDMode="Static" />
                    <asp:Label ID="txtToolTip" runat="server" />

                    <hr class="d-md-none">

                </div>

            	<div class="col-md">
                    <asp:Panel ID="pnlmainentry" runat="server">

                        <asp:Panel ID="pnlHeader" runat="server">

                            <div class="form-row">
                                <div class="col-lg-5">
                                    <asp:TextBox ID="txtClientName" runat="server" TabIndex="1" ToolTip="Store Name" Enabled="False" />
                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtClientName"
                                        WatermarkText="Client Name" />
                                </div>

                                <div class="col-5 col-lg-3">
                                    <asp:TextBox ID="txtStoreNumber" runat="server" ToolTip="Store Number" Enabled="False" />
                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtStoreNumber"
                                        WatermarkText="Store Number" />
                                </div>

                                <div class="col-5 col-lg-3">
                                    <asp:TextBox ID="txtStoreSuffix" runat="server" ClientIDMode="Static" ToolTip="Store Suffix" Enabled="False" />
                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtStoreSuffix"
                                        WatermarkText="Store Suffix" />
                                </div>

                                <div class="col-2 col-lg-1">
                                    <asp:LinkButton ID="btnSearchClient" runat="server" CssClass="btn btn-default w-100" OnClientClick="OpenClientSearch(); return false;"
                                        ToolTip="Search Client">
                                        <span class="oi oi-magnifying-glass"></span>
                                    </asp:LinkButton>
                                </div>
                            </div>

                            <div class="form-row">
                                <div class="col-7">
                                    <asp:TextBox ID="txtClientAddress" runat="server" TextMode="MultiLine" Enabled="False" ToolTip="Location Address"
                                        placeholder="Client Address"/>
                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" TargetControlID="txtClientAddress"
                                        WatermarkText="Client Address" />
                                </div>
                                <div class="col-5">
                                    <asp:TextBox ID="txtESN" runat="server" ClientIDMode="Static" TabIndex="3" ToolTip="ESN/IMEI Number" />
                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtESN"
                                        WatermarkText="ESN/IMEI Number" />

                                    <asp:TextBox ID="txtRMA" runat="server" ClientIDMode="Static" TabIndex="2" ToolTip="RMA Number" />
                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender9" runat="server" TargetControlID="txtRMA"
                                        WatermarkText="RMA Number" />
   
                                    <asp:TextBox ID="txtProjectTag" runat="server" ClientIDMode="Static" TabIndex="2" ToolTip="Project Tag" />
                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtProjectTag"
                                        WatermarkText="Project Tag" />
                                </div>
                            </div>

                            <div class="form-row">
    	                        <div class="col">
                                    <asp:TextBox ID="Display_ESN" runat="server" ClientIDMode="Static" Enabled="True" ReadOnly="True" />
                                </div>
                                <div class="col">
                                    <asp:TextBox ID="Display_MSG" runat="server" ClientIDMode="Static" Enabled="True" ReadOnly="True" />
                                </div>
                                <div class="col">
                                    <asp:TextBox ID="txtStatusPanel" runat="server" ClientIDMode="Static" Enabled="True" ReadOnly="True" />
                                </div> 
                            </div>

                            <div class="card p-3 mt-2 mb-3">

                                <div class="form-row">
    	                            <div class="col">
                                        <div class="form-row">
                                            <div class="col">
                                                <asp:Label ID="lblScanFieldText" runat="server" Text="Scan Field:" AssociatedControlID="ScanKey" />
                                                <asp:TextBox ID="ScanKey" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-row">
                                            <div class="col">
                                                <asp:TextBox ID="txtQTY" runat="server" ClientIDMode="Static" ToolTip="Quantity" placeholder="Quanitiy" />
                                            </div>
                                            <div class="col">
                                                <asp:TextBox ID="ScanKeyHistory" runat="server" ToolTip="Scan Key History" Enabled="False" />
                                            </div>
                                            <div class="col">
                                                <asp:TextBox ID="txtDateReceived" runat="server" ToolTip="Date when Item arrived at GMP" />
                                            </div>
                                        </div>
                                        <div class="form-row">
                                            <div class="col text-right">
                                                <div class="form-check-inline">
                                                    <asp:CheckBox ID="chkAutoSaveOnScan" runat="server" Text="Auto Save"
                                                    ToolTip="Click here to have the system automatically Save following an ESN scan." />                 
                                                </div>
                                                <div class="form-check-inline">
                                                    <asp:CheckBox ID="chkAutoPrintBagTag" runat="server" Text="Print"
                                                        ToolTip="Click here to Print Form auto printed once saved." />                                
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-auto my-auto">
                                        <div id="tr_WaitingForIMEI" runat="server">
                                            <span ID="imgWaitingForIMEIX" class="oi oi-x text-danger" runat="server"></span>
                                            <span ID="imgWaitingForIMEICheck" class="oi oi-check text-success" runat="server"></span>
                                            <asp:Label ID="lblWaitingForIMEIText" runat="server" Text="ESN/IMEI" />
                                        </div>
                                        <div id="tr_WaitingForClient" runat="server">
                                            <span ID="imgWaitingForClientX" class="oi oi-x text-danger" runat="server"></span>
                                            <span ID="imgWaitingForClientCheck" class="oi oi-check text-success" runat="server"></span>
                                            <asp:Label ID="lblWaitingForClientText" runat="server" Text="Client" />
                                        </div>
                                        <div id="tr_WaitingForRMA" runat="server">
                                            <span ID="imgWaitingForRMAX" class="oi oi-x text-danger" runat="server"></span>
                                            <span ID="imgWaitingForRMACheck" class="oi oi-check text-success" runat="server"></span>
                                            <asp:Label ID="lblWaitingForRMAText" runat="server" Text="RMA Number" />
                                        </div>
                                        <div id="tr_WaitingForPTag" runat="server">
                                            <span ID="imgWaitingForPTagX" class="oi oi-x text-danger" runat="server"></span>
                                            <span ID="imgWaitingForPTagCheck" class="oi oi-check text-success" runat="server"></span>
                                            <asp:Label ID="lblWaitingForPTagText" runat="server" Text="Project Tag" />
                                        </div>
                                    </div>
                                </div>

                            </div>

                        </asp:Panel>

                        <asp:TabContainer runat="server" ID="t1x" ActiveTabIndex="0" CssClass="tab-container">

                            <asp:TabPanel runat="server" ID="tb1x" Enabled="true" HeaderText="Data" CssClass="tab-panel">
                                <HeaderTemplate>
                                    <asp:Label ID="lblDataTab" runat="server" Text="Data" />
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <asp:Repeater ID="NextStep" runat="server">
                                        <ItemTemplate>
                                            <asp:Button ID="btnNextStep" runat="server" UseSubmitBehavior="False" />
                                        </ItemTemplate>
                                    </asp:Repeater>

                                    <asp:Panel ID="Panel1" runat="server">
                                        <asp:Label ID="lblMakeModelTitle" runat="server" />
                                        <asp:Label ID="lblProjectClientLocationBinTitle" runat="server" />
                                        <asp:CheckBoxList ID="chkProcessCheckList" CssClass="checklist-inline" runat="server" RepeatLayout="UnorderedList" />
                                    </asp:Panel>

                                    <asp:CheckBox ID="chkSticky" runat="server" Text='Keep Static' Checked="False" ToolTip='Select to turn "Static" on/off'
                                        OnClick="javascript:SaveSticky()" />
                                    
                                    <asp:Panel ID="pnlxInPutAreax" runat="server">
                                        <h5>You are here Aman.....</h5>
                                        <div class="card p-2 mt-2 mb-3">
                                            <asp:Panel ID="pnlHeaderArea" runat="server">
                                                <asp:Label runat="server" ID="Location" />
                                                <asp:GridView ID="GridView2" CssClass="table" runat="server" DataKeyField="QuestionID" AutoGenerateColumns="False" ShowHeader="False">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Wrap="False" ShowHeader="False">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="Descriptionh" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ShowHeader="False">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="CurrencyAnswerH" runat="server" ClientIDMode="Predictable" />
                                                                <asp:TextBox ID="NumericAnswerh" runat="server" ClientIDMode="Predictable" />
                                                                <asp:TextBox ID="TextAnswerh" runat="server" Enabled="False" />
                                                                <asp:TextBox ID="Num3Digith" runat="server" Enabled="False" />
                                                                <asp:TextBox ID="Text20Digith" runat="server" Enabled="False" />
                                                                <asp:TextBox ID="Text3Digith" runat="server" Enabled="False" />
                                                                <asp:TextBox ID="Text10Digith" runat="server" Enabled="False" />
                                                                <asp:TextBox ID="Text18Digith" runat="server" Enabled="False" />
                                                                <asp:TextBox ID="Text50Digith" runat="server" Enabled="False" />
                                                                <asp:RadioButtonList ID="RadioAnswerh" CssClass="radiolist-inline" runat="server" RepeatLayout="UnorderedList"
                                                                    Enabled="False" />
                                                                <asp:CheckBoxList ID="checkAnswerh" CssClass="checklist-inline" runat="server" RepeatLayout="UnorderedList"
                                                                    Enabled="False" />
                                                                <asp:TextBox ID="CalAnswerh" runat="server" Enabled="False" />
                                                                <asp:DropDownList ID="drpAnswerh" runat="server" Enabled="False" />
                                                                <asp:HiddenField ID="HiddenIDh" runat="server" />
                                                                <asp:HiddenField ID="HiddenNameh" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </div>

                                        <asp:Panel ID="pnlInPutArea" runat="server">
                                          
                                            <div class="row">
                                            	<div class="col">
                                                    <h2><asp:Label runat="server" ID="lblActiveProcess" /></h2>
                                                </div>
                                            	<div class="col text-right">
                                                    <asp:Label runat="server" ID="lblActiveProcessEDT" />

                                                    <asp:LinkButton ID="imgShowUnitNote" CssClass="btn btn-default" runat="server" ToolTip="Open Client Notes"
                                                        OnClientClick="OpenUnitNote(); return false;">
                                                        <span class="oi oi-info"></span>
                                                    </asp:LinkButton>

                                                    <asp:LinkButton ID="imgShowPartList" CssClass="btn btn-default" runat="server" ToolTip="Open Part List"
                                                        OnClientClick="OpenPartList(); return false;">
                                                        <span class="oi oi-wrench"></span>                                                            
                                                    </asp:LinkButton>

                                                    <asp:LinkButton ID="imgShowIFSPONumbers" CssClass="btn btn-default" runat="server" ToolTip="Open IFS PO Number List"
                                                        OnClientClick="OpenIFSPONumberListForLines(); return false;">
                                                        <span class="oi oi-arrow-circle-top"></span>
                                                    </asp:LinkButton>

                                                    <asp:LinkButton ID="imgShowPartReturnList" CssClass="btn btn-default" runat="server" ToolTip="Open Return Part List"
                                                        OnClientClick="OpenReturnPartList(); return false;">
                                                        <span class="oi oi-wrench"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                            <div class="card p-2 mt-2 mb-3">
                                                <asp:GridView ID="gd1" CssClass="table" runat="server" DataKeyField="QuestionID" AutoGenerateColumns="False" ShowHeader="False">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Wrap="False" ShowHeader="False">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="HiddenID" runat="server" />
                                                                <asp:Label runat="server" ID="Description" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ShowHeader="False">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="Num3Digit" runat="server" Enabled="False" MaxLength="3" />
                                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="Num3Digit"
                                                                    ValidChars="0123456789" />
                                                                <asp:TextBox ID="Text20Digit" runat="server" Enabled="False" MaxLength="20" />
                                                                <asp:TextBox ID="Text3Digit" runat="server" Enabled="False" MaxLength="3" />
                                                                <asp:TextBox ID="Text10Digit" runat="server" Enabled="False" MaxLength="10" />
                                                                <asp:TextBox ID="Text18Digit" runat="server" Enabled="False" MaxLength="18" />
                                                                <asp:TextBox ID="Text50Digit" runat="server" Enabled="False" MaxLength="50" />
                                                                <asp:TextBox ID="CurrencyAnswer" runat="server" ClientIDMode="Predictable" />
                                                                <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="CurrencyAnswer"
                                                                    Mask="9{9}.99" MaskType="Number" InputDirection="RightToLeft" />
                                                                <asp:TextBox ID="NumericAnswer" runat="server" ClientIDMode="Predictable" />
                                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="NumericAnswer"
                                                                    ValidChars="0123456789" />
                                                                <asp:TextBox ID="TextAnswer" runat="server" ClientIDMode="Predictable" MaxLength="200" />
                                                                <asp:RadioButtonList ID="RadioAnswer" CssClass="radiolist-inline" runat="server" RepeatLayout="UnorderedList"
                                                                    ClientIDMode="Predictable" />
                                                                <asp:CheckBoxList ID="checkAnswer" CssClass="checklist-inline" runat="server" RepeatLayout="UnorderedList"
                                                                    ClientIDMode="Predictable" />
                                                                <asp:TextBox ID="CalAnswer" runat="server" ReadOnly="false" ClientIDMode="Predictable" />
                                                                <asp:CalendarExtender ID="CalendarExtender1" CssClass="calendarStyle" runat="server" TargetControlID="CalAnswer" Format="MM/dd/yyyy" />
                                                                <asp:DropDownList ID="drpAnswer" AutoPostBack="true" runat="server" ClientIDMode="Predictable" EnableFilterSearch="true" EnableServerFiltering="true"  />
                                                                <asp:HiddenField ID="HiddenName" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>

                                        </asp:Panel>
                                    </asp:Panel>

                                    <asp:Panel ID="pnlInputTargetArea" runat="server">
                                        
                                        <asp:Table ID="Table1" CssClass="table" runat="server">
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <h2><asp:Label ID="lblTarget" runat="server" Text="Target" /></h2>
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Label ID="lblTargetEDT" CssClass="text-right" runat="server" />
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>

                                        <%-- Used for Bulk Move purposes --%>
                                        <asp:GridView ID="GridView4" CssClass="table" runat="server" DataKeyField="QuestionID" AutoGenerateColumns="False" ShowHeader="False">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-Wrap="False" ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="Description" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="CurrencyAnswer" runat="server" ClientIDMode="Predictable" />
                                                        <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="CurrencyAnswer"
                                                            Mask="9{9}.99" MaskType="Number" InputDirection="RightToLeft" />
                                                        <asp:TextBox ID="NumericAnswer" runat="server" ClientIDMode="Predictable" />
                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="NumericAnswer"
                                                            ValidChars="0123456789" />
                                                        <asp:TextBox ID="TextAnswer" runat="server" ClientIDMode="Predictable" MaxLength="200"/>
                                                        <asp:RadioButtonList ID="RadioAnswer" CssClass="radiolist-inline" runat="server" RepeatLayout="UnorderedList"
                                                            ClientIDMode="Predictable" />
                                                        <asp:CheckBoxList ID="checkAnswer" CssClass="checklist-inline" runat="server" RepeatLayout="UnorderedList"
                                                            ClientIDMode="Predictable" />
                                                        <asp:TextBox ID="CalAnswer" runat="server" ReadOnly="True" ClientIDMode="Predictable" />
                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="CalAnswer" Format="MM/dd/yyyy" />
                                                        <asp:DropDownList ID="drpAnswer" runat="server" ClientIDMode="Predictable" />
                                                        <asp:HiddenField ID="HiddenName" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextAnswer" MaxLength="200" runat="server" />
                                                        <asp:RadioButtonList ID="RadioAnswer" CssClass="radiolist-inline" runat="server" RepeatLayout="UnorderedList" />
                                                        <asp:CheckBoxList ID="checkAnswer" CssClass="checklist-inline" runat="server" RepeatLayout="UnorderedList" />
                                                        <asp:TextBox ID="CalAnswer" runat="server" />
                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="CalAnswer" />
                                                        <asp:DropDownList ID="drpAnswer" runat="server" />
                                                        <asp:HiddenField ID="HiddenID" runat="server" />
                                                        <asp:HiddenField ID="HiddenName" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>

                                    </asp:Panel>

                                </ContentTemplate>
                            </asp:TabPanel>

                            <asp:TabPanel runat="server" ID="TabPanelBulkProcess" Enabled="true" Visible="False" CssClass="tab-panel">
                                
                                <HeaderTemplate>
                                   
                                    <asp:Label ID="Label7" runat="server" Text="Bulk" />
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <asp:Panel ID="Panel6" runat="server">
                                        <h5>
                                            Paste list</h5>
                                        <asp:RadioButtonList ID="PasteDeliminator" CssClass="radiolist-inline" runat="server"
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem Text="Excel" Value="Excel" Selected="True" />
                                            <asp:ListItem Text="Comma" Value="Comma" />
                                            <asp:ListItem Text="Space" Value="Space" />
                                        </asp:RadioButtonList>
                                        <asp:TextBox ID="txtPasteParse" runat="server" ToolTip="" TextMode="MultiLine" />
                                        <asp:Button ID="btnPasteParse" runat="server" Text="Process Devices" />
                                        <asp:TextBox ID="txtBulkProcess" runat="server" ToolTip="xxxx" TextMode="MultiLine" />
                                        <asp:Panel ID="Panel8" runat="server">
                                            <asp:GridView ID="grdBulkResults" CssClass="table" runat="server" AutoGenerateColumns="False"
                                                DataKeyNames="IMEI">
                                                <Columns>
                                                    <asp:BoundField DataField="IMEI" HeaderText="IMEI" />
                                                    <asp:BoundField DataField="Message" HeaderText="Message" />
                                                </Columns>
                                            </asp:GridView>
                                            <asp:Button ID="btnBulkResults" runat="server" Text="Download Process Results" />
                                        </asp:Panel>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:TabPanel>

                            <asp:TabPanel runat="server" ID="TabPanelSKU" Enabled="true" Visible="True" CssClass="tab-panel">
                                <HeaderTemplate>
                                    <asp:Label ID="Label1" runat="server" Text="SKU" />
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <asp:Panel ID="Panel17" runat="server">
                                        <asp:Button ID="btnSKURefresh" runat="server" Text="Refresh" />
                                        <asp:GridView ID="grdSKUHistory" CssClass="table" runat="server" AutoGenerateColumns="False" DataKeyNames="ReceiveDetailSKUChangeLogID">
                                            <Columns>
                                                <%--<asp:BoundField DataField="Status" HeaderText="Status" />--%>
                                                <asp:BoundField DataField="SKU" HeaderText="SKU" />
                                                <asp:BoundField DataField="CreateDate" HeaderText="Date" />
                                                <asp:BoundField DataField="CreateUser" HeaderText="User" />
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:TabPanel>
  
                            <asp:TabPanel runat="server" ID="tabBulkAdvance" Enabled="true" HeaderText="Bulk Advance" Visible="False" CssClass="tab-panel">
                                <ContentTemplate>
                                    <h2><asp:Label ID="lblBulkCount" runat="server" Text="Count: 0" ToolTip="Count" /></h2>
                                    <asp:Repeater ID="NextStepBulk" runat="server">
                                        <ItemTemplate>
                                            <asp:Button ID="btnNextStepBulk" runat="server" UseSubmitBehavior="False" />
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <asp:Panel ID="Panel2" runat="server">
                                        <asp:ListBox ID="lstBukAdvance" runat="server" SelectionMode="Multiple" ViewStateMode="Inherit" ClientIDMode="Static" />
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:TabPanel>

                            <asp:TabPanel runat="server" ID="TabPanelClientDetailx" Enabled="true" HeaderText="Search" Visible="False" CssClass="tab-panel">
                                <ContentTemplate>
                                    <asp:Panel ID="Panel3" runat="server">
                                        <asp:Button ID="btnSearchRefresh" runat="server" Text="Refresh" />
                                        <asp:GridView ID="gvSearchResult" CssClass="table" runat="server" AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Select">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="imgSelect" CssClass="btn btn-default" runat="server" HeaderText="Select" ToolTip="Select Item as source for New entry">
                                                            <span class="oi oi-plus"></span>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="imgDelete" CssClass="btn btn-default" runat="server" HeaderText="Select" ToolTip="Delete entry">
                                                            <span class="oi oi-trash"></span>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ReceiveDetailBulkID" HeaderText="ID" ReadOnly="True" Visible="false" />
                                                <asp:BoundField DataField="ProcessName" HeaderText="Process" />
                                                <asp:BoundField DataField="ESN" HeaderText="ESN" />
                                                <asp:BoundField DataField="QTY" HeaderText="QTY" />
                                                <asp:BoundField DataField="RMANumber" HeaderText="RMA" />
                                                <asp:BoundField DataField="ProjectName" HeaderText="Project" />
                                                <asp:TemplateField HeaderText="D">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="imgDetail" CssClass="btn btn-default" runat="server" HeaderText="D" ToolTip="Detail"
                                                            OnClientClick="return false;">
                                                            <span class="oi oi-info"></span>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="LastUpdateDate" HeaderText="LastUpdateDate" />
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:TabPanel>

                            <asp:TabPanel runat="server" ID="TabPanelClientDetail" Enabled="true" Visible="True" CssClass="tab-panel">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHistoryTab" runat="server" Text="History" />
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <asp:Panel ID="Panel14" runat="server">
                                        <asp:Button ID="btnHistoryRefresh" runat="server" Text="Refresh" />
                                        <asp:GridView ID="grdHistory" CssClass="table" runat="server" AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:BoundField DataField="ReceiveDetailID" HeaderText="ID" ReadOnly="True" Visible="false" />
                                                <asp:BoundField DataField="ProcessText" HeaderText="Process" />
                                                <asp:BoundField DataField="MiscText" HeaderText="Note" />
                                                <asp:BoundField DataField="CreateDate" HeaderText="Date" />
                                                <asp:BoundField DataField="CreateUser" HeaderText="User" />
                                                <asp:TemplateField HeaderText="D">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="imgDelete" CssClass="btn btn-default" runat="server" ToolTip="Delete">
                                                            <span class="oi oi-trash"></span>
                                                        </asp:LinkButton>
                                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="imgDelete"
                                                            ConfirmText="Are you sure you want to delete this file?">
                                                        </asp:ConfirmButtonExtender>
                                                        <asp:LinkButton ID="imgMoveProcessUp" CssClass="btn btn-default" runat="server" ToolTip="Move Process Log Up">
                                                            <span class="oi oi-arrow-top"></span>
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="imgMoveProcessDown" CssClass="btn btn-default" runat="server" ToolTip="Move Process Log Down">
                                                            <span class="oi oi-arrow-bottom"></span>
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="imgChangeProcess" CssClass="btn btn-default" runat="server" ToolTip="Change Process">
                                                            <span class="oi oi-wrench"></span>
                                                        </asp:LinkButton>
                                                    </ItemTemplate> 
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:TabPanel>

                            <asp:TabPanel runat="server" ID="TabPanelItemVersion" Enabled="true" Visible="True" CssClass="tab-panel">
                                <HeaderTemplate>
                                    <asp:Label ID="lblVersionTab" runat="server" Text="Version" />
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <asp:Panel ID="Panel4" runat="server">
                                        <asp:Button ID="btnVersionRefresh" runat="server" Text="Refresh" />
                                        <asp:GridView ID="grdVersion" CssClass="table" runat="server" AutoGenerateColumns="False" DataKeyNames="ReceiveDetailID">
                                            <Columns>
                                                <asp:BoundField DataField="ReceiveDetailID" HeaderText="ID" ReadOnly="True" Visible="true" />
                                                <asp:BoundField DataField="ProjectName" HeaderText="Project" />
                                                <asp:BoundField DataField="StatusName" HeaderText="Status" />
                                                <asp:BoundField DataField="ESN" HeaderText="ESN" />
                                                <asp:BoundField DataField="Version" HeaderText="Version" />
                                                <asp:BoundField DataField="CreateDate" HeaderText="Date" />
                                                <asp:BoundField DataField="CreateUser" HeaderText="User" />
                                                <asp:TemplateField HeaderText="V">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="imgOpen" CssClass="btn btn-default" runat="server" ToolTip="Open"
                                                            OnClientClick='<%# Eval("ReceiveDetailID","LoadSheetDataDetail({0}); return false;")%>'>
                                                            <span class="oi oi-warning"></span>
                                                        </asp:LinkButton>

                                                        <%--<asp:ImageButton ID="imgGraveYard" runat="server" HeaderText="" ImageUrl="~/Images/PUSHPINB.bmp"
                                                            ToolTip="Send this version to GraveYard" OnClientClick='<%# Eval("ReceiveDetailID","MoveToGraveYard({0}); return false;")%>'>
                                                        </asp:ImageButton>
                                                        <asp:ImageButton ID="imgGraveYardBack" runat="server" HeaderText="" ImageUrl="~/Images/PUSHPINB.bmp"
                                                            ToolTip="Return this version from GraveYard" OnClientClick='<%# Eval("ReceiveDetailID","MoveBackFromGraveYard({0}); return false;")%>'>
                                                        </asp:ImageButton>
                                                        <asp:ImageButton ID="imgVersion" runat="server" HeaderText="" ImageUrl="~/Images/PUSHPINB.bmp"
                                                            ToolTip="Set Version to 000"></asp:ImageButton>
                                                        <asp:ImageButton ID="imgVerUp" runat="server" HeaderText="" ImageUrl="~/Images/ADD.bmp"
                                                            ToolTip="Advance Version"></asp:ImageButton>
                                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender_Version" runat="server" TargetControlID="imgVersion"
                                                            ConfirmText="Are you sure you want to re-version this file to zero?">
                                                        </asp:ConfirmButtonExtender>
                                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtenderVersionUP" runat="server" TargetControlID="imgVerUp"
                                                            ConfirmText="Are you sure you want to advance Version?">
                                                        </asp:ConfirmButtonExtender>--%>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:TabPanel>

                            <asp:TabPanel runat="server" ID="TabPanelIDCLocationHistory" Enabled="true" Visible="True" CssClass="tab-panel">
                                <HeaderTemplate>
                                    <asp:Label ID="Label5" runat="server" Text="IDC Location" />
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <asp:Panel ID="Panel19" runat="server">
                                        <asp:Button ID="btnIDCLocationRefresh" runat="server" Text="Refresh" />
                                        <asp:GridView ID="grdIDCLocationHistory" CssClass="table" runat="server" AutoGenerateColumns="False" DataKeyNames="LocationLogID">
                                            <Columns>
                                                <asp:BoundField DataField="Status" HeaderText="Status" />
                                                <asp:BoundField DataField="Location" HeaderText="Location" />
                                                <asp:BoundField DataField="CreateDate" HeaderText="Date" />
                                                <asp:BoundField DataField="CreateUser" HeaderText="User" />
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:TabPanel>

                            <asp:TabPanel runat="server" ID="TabPanelLocationHistory" Enabled="true" Visible="True" CssClass="tab-panel">
                                <HeaderTemplate>
                                    <asp:Label ID="Label2" runat="server" Text="Location" />
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <asp:Panel ID="Panel13" runat="server">
                                        <asp:Button ID="btnLocationRefresh" runat="server" Text="Refresh" />
                                        <asp:GridView ID="grdLocationHistory" CssClass="table" runat="server" AutoGenerateColumns="False"
                                            DataKeyNames="ReceiveDetailIFSLocationLogID">
                                            <Columns>
                                                <%--<asp:BoundField DataField="Status" HeaderText="Status" />>--%>
                                                <asp:BoundField DataField="IFSLocation" HeaderText="Location" />
                                                <asp:BoundField DataField="CreateDate" HeaderText="Date" />
                                                <asp:BoundField DataField="CreateUser" HeaderText="User" />
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:TabPanel>

                            <asp:TabPanel runat="server" ID="TabPanel2" Enabled="true" Visible="True" CssClass="tab-panel">
                                <HeaderTemplate>
                                    <asp:Label ID="Label4" runat="server" Text="Condition" />
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <asp:Panel ID="Panel18" runat="server">
                                        <asp:Button ID="btnConditionRefresh" runat="server" Text="Refresh"/>
                                        <asp:GridView ID="grdConditionHistory" CssClass="table" runat="server" AutoGenerateColumns="False"
                                            DataKeyNames="ReceiveDetailConditionChangeLogID">
                                            <Columns>
                                                <%--<asp:BoundField DataField="Status" HeaderText="Status" />--%>
                                                <asp:BoundField DataField="IFS_Condition" HeaderText="Condition" />
                                                <asp:BoundField DataField="CreateDate" HeaderText="Date" />
                                                <asp:BoundField DataField="CreateUser" HeaderText="User" />
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:TabPanel>

                            <asp:TabPanel runat="server" ID="TabPanelBlackbelt" Enabled="true" Visible="True" CssClass="tab-panel">
                                <HeaderTemplate>
                                    <asp:Label ID="Label6" runat="server" Text="Blackbelt" />
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <asp:Panel ID="Panel5" runat="server">
                                        <asp:Button ID="btnBlackbeltRefresh" runat="server" Text="Refresh" />
                                        <asp:GridView ID="grdBlackBelt" CssClass="table" runat="server" AutoGenerateColumns="False" DataKeyNames="LocationLogID">
                                            <Columns>
                                                <asp:BoundField DataField="Status" HeaderText="Status" />
                                                <asp:BoundField DataField="Location" HeaderText="Location" />
                                                <asp:BoundField DataField="CreateDate" HeaderText="Date" />
                                                <asp:BoundField DataField="CreateUser" HeaderText="User" />
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:TabPanel>

                            <asp:TabPanel runat="server" ID="tabBillingPoints" Enabled="true" HeaderText="Billing Points" Visible="False" CssClass="tab-panel">
                                <ContentTemplate>
                                    <asp:Panel ID="Panel7" runat="server">
                                        <asp:Button ID="btnBillingRefresh" runat="server" Text="Refresh" />
                                        <asp:GridView ID="grdBillingPoints" CssClass="table" runat="server" AutoGenerateColumns="False" DataKeyNames="ReceiveDetailID">
                                            <Columns>
                                                <asp:BoundField DataField="ReceiveDetailID" HeaderText="ID" ReadOnly="True" Visible="true" />
                                                <asp:BoundField DataField="ProjectID" HeaderText="PjID" />
                                                <asp:BoundField DataField="ProcessID" HeaderText="PrID" />
                                                <asp:TemplateField HeaderText="Process Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProcessName" runat="server" Text="Process Name" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="RateValue" HeaderText="Rate/Value" />
                                                <asp:BoundField DataField="PostedDate" HeaderText="Posted Date" />
                                                <asp:BoundField DataField="PostedUser" HeaderText="Posted User" />
                                                <asp:BoundField DataField="CreateDate" HeaderText="Date" />
                                                <asp:BoundField DataField="CreateUser" HeaderText="User" />
                                                <asp:TemplateField HeaderText="V">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="imgDelete" CssClass="btn btn-default" runat="server" ToolTip="Delete Billing Point">
                                                            <span class="oi oi-trash"></span>
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="imgAddBillingPoint" CssClass="btn btn-default" runat="server" ToolTip="Add Billing point">
                                                            <span class="oi oi-plus"></span>                                                                
                                                        </asp:LinkButton>
                                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtenderVersionUP" runat="server" TargetControlID="imgDelete"
                                                            ConfirmText="Are you sure you want to Delete this billing point?" />
                                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender_Version" runat="server" TargetControlID="imgAddBillingPoint"
                                                            ConfirmText="Are you sure you want to Add a Billing Point?" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:TabPanel>

                            <asp:TabPanel runat="server" ID="TabAuthorization" Enabled="true" Visible="True" CssClass="tab-panel">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAuthorizationTab" runat="server" Text="Authorization Log" />
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <asp:Button ID="btnEditAuthorization" runat="server" Text="Edit" Visible="False" />
                                    <asp:Button ID="btnAddAuthorization" runat="server" Text="Add" OnClientClick=" OpenAuthorization();return false;" />
                                    <asp:Button ID="btnAuthorizeLogRefresh" runat="server" Text="Refresh" />

                                    <asp:GridView ID="grdAuthorizationLog" CssClass="table" runat="server" AutoGenerateColumns="False"
                                        DataKeyNames="ReceiveDetailAuthorizationLogID">
                                        <Columns>
                                            <asp:BoundField DataField="ReceiveDetailID" HeaderText="ID" ReadOnly="True" Visible="false" />
                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus" runat="server" Text="Label" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Note" HeaderText="Note" />
                                            <asp:BoundField DataField="EstimateFee" HeaderText="Estimate" />
                                            <asp:BoundField DataField="FreightFee" HeaderText="Freight" />
                                            <asp:BoundField DataField="HST" HeaderText="HST" />
                                            <asp:BoundField DataField="Total" HeaderText="Total" />
                                            <asp:TemplateField HeaderText="Received">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReceived" runat="server" Text="Label" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Authorized">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAuthorized" runat="server" Text="Label" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Declined">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDelcined" runat="server" Text="Label" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rejected">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRejected" runat="server" Text="Label" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Requested">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRequested" runat="server" Text="Label" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbtnRevive" CssClass="btn btn-default" runat="server">Revive</asp:LinkButton>
                                                    <asp:LinkButton ID="lnkbtnExpire" CssClass="btn btn-default" runat="server">Expire</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:TabPanel>

                        </asp:TabContainer>

                        <div class="form-row">
                            <div class="col">
                                <asp:TextBox ID="Display_ESN_B" runat="server" ClientIDMode="Static" Enabled="True" ReadOnly="True" />
                            </div>
                            <div class="col">
                                <asp:TextBox ID="Display_MSG_B" runat="server" ClientIDMode="Static" Enabled="True" ReadOnly="True" />
                            </div>
                            <div class="col">
                                <asp:TextBox ID="txtStatusPanel_B" runat="server" ClientIDMode="Static" Enabled="True" ReadOnly="True" />
                            </div>
                        </div>

                        <div class="form-row">
            	            <div class="col">
                                <asp:Button ID="btnBagTag_b" runat="server" Text="Print Form ++" />
                                <asp:Button ID="btnSave_b" runat="server" Text="Save **" />
                            </div>
                        </div>

                    </asp:Panel>  

                </div>
            </div>

            <div id="wndUnitNote" class="modal" tabindex="-1" role="dialog">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Customer Notes</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <asp:Panel ID="pnlUnitNote" runat="server" />
                        </div>
                        <div class="modal-footer">
                            <input type="Button" value="Close" onclick="wndUnitNote.Close()" />
                        </div>
                    </div>
                </div>
            </div>

            <div id="wndAuthorize" class="modal" tabindex="-1" role="dialog">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Authorization Request</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <asp:HiddenField ID="HiddenField1" runat="server" />
                            <asp:HiddenField ID="HiddenField2" runat="server" />
                            <asp:TextBox ID="txtEstimateFee" runat="server" placeholder="Estimate Fee" />
                            <asp:TextBox ID="txtFreightFee" runat="server" placeholder="Freight Fee" />
                            <asp:TextBox ID="txtHST" runat="server" placeholder="HST" />
                            <asp:TextBox ID="TxtTotal" runat="server" placeholder="Total" />
                            <asp:TextBox ID="txtAuthorizeNote" runat="server" Wrap="True" TextMode="MultiLine" MaxLength="250" placeholder="Notes" />
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnCancelAuthorize" runat="server" Text="Cancel" OnClientClick="Authorization_Cancel(); return false;" />
                            <asp:Button ID="btnSaveAuthorize" runat="server" Text="Save" OnClientClick="Authorization_Save();" />
                        </div>
                    </div>
                </div>
            </div>

            <div id="wndSelectRepairReport" class="modal" tabindex="-1" role="dialog">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Repair Report</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <asp:Button ID="btnRepairEstimate" runat="server" Text="Estimate" OnClientClick="OpenRepairForm('E'); return false;" />
                            <asp:Button ID="btnRepairRepair" runat="server" Text="Repair" OnClientClick="OpenRepairForm('R'); return false;" />
                            <asp:Button ID="btnRepairPacking" runat="server" Text="Packing Slip" OnClientClick="OpenRepairForm('P'); return false;" />
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnRepairCancel" runat="server" Text="Cancel" OnClientClick="CloseSelectRepairReport(); return false;" />
                        </div>
                    </div>
                </div>
            </div>

            <div id="wndESNFound" class="modal" tabindex="-1" role="dialog">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">ESN/IMEI Number on file</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <asp:HiddenField ID="hdnESNID" runat="server" />
                            <asp:HiddenField ID="hdnESNNumber" runat="server" />

                            <div class="alert alert-warning mb-0" role="alert">
                                <asp:Label ID="lblESNFoundText" runat="server" Text="ESN/IMEI Already on file!" />
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnAlreadyFound" runat="server" Text="Refer to Traking and Inspection?"
                                OnClientClick="AlreadyFound_AddNew_Cancel(); return false;" />
                            <asp:Button ID="ClientTransfer" runat="server" Text="Client Submitted Transfer?"
                                OnClientClick="AlreadyFound_TransferIN_OK(); return false;" />
                            <asp:Button ID="TransferToMSC" runat="server" Text="Transfer to MSC?"
                                OnClientClick="AlreadyFound_TransferToMSC_OK(); return false;" />
                            <asp:Button ID="btnKittingDefective" runat="server" Text="Defective Unit?"
                                OnClientClick="KittingDefectiveCancel(); return false;" />
                        </div>
                    </div>
                </div>
            </div>

            <div id="wndESNFoundClient" class="modal" tabindex="-1" role="dialog">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">ESN/IMEI Already on file!</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="alert alert-warning mb-0" role="alert">
                                <asp:Label ID="Label3" runat="server" Text="ESN/IMEI Already on file!" />
                            </div>
                        </div>
                        <div class="modal-footer">
                            <input type="button" value="ESN/IMEI Already on file?" onclick="$('#wndESNFoundClient').modal('hide'); return false;" />                            
                        </div>
                    </div>
                </div>
            </div>

            <div id="wndSendEmailWindow" class="modal" tabindex="-1" role="dialog">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Send Email</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <asp:Panel ID="EmailSend" runat="server">
                                <a class="btn btn-default" href="mailto:xxxxxxxxx@BM.ca?subject=Sample&body=This is an example.">Send</a>
                            </asp:Panel>
                        </div>
                        <!--<div class="modal-footer"></div>-->
                    </div>
                </div>
            </div>

            <div id="wndSelectProcess" class="modal" tabindex="-1" role="dialog">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Select Process</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <asp:HiddenField ID="hdnSelectedLogID" runat="server" />
                            <asp:DropDownList ID="drpProcessList" runat="server" />
                        </div>
                        <div class="modal-footer">
                            <input type="button" value="Cancel" onclick="SelectProcess_Cancel()" />
                            <input type="button" value="Continue with change?" onclick="SelectProcess_OK()" />
                        </div>
                    </div>
                </div>
            </div>

            <div id="wndSwitchIMEI" class="modal" tabindex="-1" role="dialog">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Switch IMEI</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <asp:TextBox ID="txtNewIMEI" runat="server" placeholder="New IMEI Number" />
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnSwitchIMEICancel" runat="server" Text="Cancel" OnClientClick="SwitchIMEICancel(); return false;" />
                            <asp:Button ID="btnSwitchIMEIOK" runat="server" Text="Switch" OnClientClick="SwitchIMEIOK(); return false;" />
                        </div>
                    </div>
                </div>
            </div>
            
            <div id="wndSelectClientLocation" class="modal" tabindex="-1" role="dialog">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Select Client Location</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <asp:TextBox ID="txtsClientName" runat="server" placeholder="Client Name" />
                            <asp:TextBox ID="txtsLocationName" runat="server" placeholder="Location Name" />
                            <asp:TextBox ID="txtsStreet" runat="server" placeholder="Street" />
                            <asp:TextBox ID="txtsPostalCode" runat="server" placeholder="Postal Code" />
                            <asp:Panel ID="pnlSearchResult" runat="server">
                                <table id="XX"></table>
                            </asp:Panel>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClientClick="SearchClient(); return false;" />
                        </div>
                    </div>
                </div>
            </div>

            <div id="wndPartReturnList" class="modal" tabindex="-1" role="dialog">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Part Return List</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <asp:Panel ID="pnlPartReturnList" runat="server" />
                            <asp:Panel ID="pnlPickedReturnList" runat="server" />
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnPickReturnCancel" runat="server" Text="Cancel" OnClientClick="PickReturnCancel(); return false;" />
                            <asp:Button ID="btnPickReturnSave" runat="server" Text="Save" OnClientClick="PickReturnSave(); return false;" />
                        </div>
                    </div>
                </div>
            </div>

            <div id="wndIMEIBulk" class="modal" tabindex="-1" role="dialog">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Bulk Process IMEI Numbers</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <asp:TextBox ID="txtOrderNumber" runat="server" />
                            <input type="button" class="d-block" value="Load IMEI List from Order Entry Number" onclick="LoadOrderEntryIMEIList_OK();return false;" />
                            
                            <asp:Label ID="lblIMEIStatus" runat="server" Text="Status:" />
                            <asp:TextBox ID="txtIMEIList" runat="server" TextMode="MultiLine" />

                            <asp:Label ID="lblIMEICount" runat="server" Text="Count:" />
                        </div>
                        <div class="modal-footer">
                            <input type="button" value="Cancel" onclick="IMEIBulk_Cancel(); return false;" />
                            <input type="button" value="Count?" onclick="IMEIBulk_Count(); return false;" />
                            <input type="button" value="Process IMEI List?" onclick="IMEIBulk_OK(); return false;" />
                        </div>
                    </div>
                </div>
            </div>

            <div id="wndPartList" class="modal" tabindex="-1" role="dialog">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Part List</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <asp:Panel ID="pnlPartList" runat="server" />
                            <asp:Panel ID="pnlPickedList" runat="server" />
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnPickCancel" runat="server" Text="Cancel" OnClientClick="PickCancel(); return false;" />
                            <asp:Button ID="btnPickSave" runat="server" Text="Save" OnClientClick="PickSave(); return false;" />
                        </div>
                    </div>
                </div>
            </div>

            <div id="wndPurchaseOrderList" class="modal" tabindex="-1" role="dialog">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Purchase Order List</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <asp:Panel ID="pnlPOList" runat="server" />
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnPOPickCancel" runat="server" Text="Cancel" OnClientClick="PickPOCancel(); return false;" />
                            <%--<asp:Button ID="btnPOPickSave" runat="server" Text="Save" OnClientClick="PickPOSave(); return false;" />--%>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ContentPlaceHolderID="js" runat="server">

    <script type="text/javascript">
        var sessionTimeout = "<%= Session.Timeout %>";

        function SetSessionTimeout() {
            sessionTimeout = "<%= Session.Timeout %>";
            DisplaySessionTimeout();
        }

        function DisplaySessionTimeout() {
            //        alert('x' + sessionTimeout + 'x');
            //assigning minutes left to session timeout to Label
            document.getElementById("<%= lblSessionTime.ClientID %>").innerText = sessionTimeout;
            sessionTimeout = sessionTimeout - 1;

            //if session is not less than 0
            if (sessionTimeout >= 0)
            //call the function again after 1 minute delay
                window.setTimeout("DisplaySessionTimeout()", 60000);
            //            window.setTimeout("DisplaySessionTimeout()", 30000);
            //            window.setTimeout("DisplaySessionTimeout()", 15000);
            else {
                //show message box
                //execute logout;
                //            alert("Your current Session is over.");
            }
        }
    </script>

    <script type="text/javascript">

        // Setup Global Variables
        var dirty = false;
        var DataList = {};
        var ReturnDataList = {};
        var DoSetESN = true;
        var Timer1 = new StopWatch();

        ///////////////////////
        // Initialize some Values.
        MCL("ScanKey").value = '';

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

        function BeginRequestHandler(sender, args) {
            if (args._postBackElement.id != "SkinTable1") {
                $('#loading').show();
            }
        }

        function EndRequestHandler(sender, args) {
            $('#loading').hide();
            $("select").select2({
                theme: "bootstrap4"
            }); 

        }

        // ************************************************************************

        function xRefresh(panelName) {
            alert(panelName);

        }

        ///////////////////////////////////////

        function RecordContact(Message, Note) {
            var service = new WebServer_01();
            //           Message = "XXXXXXX";
            //           Note = "YYYYYYYY";
            //    alert("OpenEmailWindow");
            service.RecordEmailContact(MCL('RECEIVEDETAILID').value, Message, Note, MCL('Username').value, null, null, null);
        }

        //*************************************************************************

        function MCL(ControlName) {
            switch (ControlName.toUpperCase()) {

                //case "WNDESNFOUND": return $find('< %=this.wndESNFound.ClientID%>'); break;
                //case "WNDESNFOUNDCLIENT": return $find('< %=this.wndESNFoundClient.ClientID%>'); break;
                //case "WNDESNFOUNDCLIENT": return $find('< %=this.wndESNFoundClient.ClientID%>'); break;
                //case "WNDIMEIBULK": return $find('< %=this.wndIMEIBulk.ClientID%>'); break;
                //case "WNDSELECTPROCESS": return $find('< %=this.wndSelectProcess.ClientID%>'); break;
                //case "WNDSENDEMAILWINDOW": return $find('< %=this.wndSendEmailWindow.ClientID%>'); break;
                //case "WNDPARTLIST": return $find('< %=this.wndPartList.ClientID%>'); break;
                //case "WNDPURCHASEORDERLIST": return $find('< %=this.wndPurchaseOrderList.ClientID%>'); break;
                //case "WNDPARTRETURNLIST": return $find('< %=this.wndPartReturnList.ClientID%>'); break;
                //case "WNDSELECTCLIENTLOCATION": return $find('< %=this.wndSelectClientLocation.ClientID%>'); break;
                //case "WNDAUTHORIZE": return $find('< %=this.wndAuthorize.ClientID%>'); break;
                //case "WNDSWITCHIMEI": return $find('< %=this.wndSwitchIMEI.ClientID%>'); break;
                //case "WNDSELECTREPAIRREPORT": return $find('< %=this.wndSelectRepairReport.ClientID%>'); break;
                //case "WNDUNITNOTE": return $find('< %=this.wndUnitNote.ClientID%>'); break; 
                case "PNLUNITNOTE": return $get("<%= pnlUnitNote.ClientID %>"); break;
                //case "PNLPOPICKEDLIST": return $get("< %= pnlPOPickedList.ClientID %>"); break;    
                case "PNLPOLIST": return $get("<%= pnlPOList.ClientID %>"); break;
                case "PNLPICKEDLIST": return $get("<%= pnlPickedList.ClientID %>"); break;
                case "PNLPICKEDRETURNLIST": return $get("<%= pnlPickedReturnList.ClientID %>"); break;
                case "TXTNEWIMEI": return $get("<%= txtNewIMEI.ClientID %>"); break;
                case "PNLSEARCHRESULT": return $get("<%= pnlSearchResult.ClientID %>"); break;
                case "TXTSCLIENTNAME": return $get("<%= txtsClientName.ClientID %>"); break;
                case "TXTSLOCATIONNAME": return $get("<%= txtsLocationName.ClientID %>"); break;
                case "TXTSSTREET": return $get("<%= txtsStreet.ClientID %>"); break;
                case "TXTSPOSTALCODE": return $get("<%= txtsPostalCode.ClientID %>"); break;
                case "PNLPARTLIST": return $get("<%= pnlPartList.ClientID %>"); break;
                case "PNLPARTRETURNLIST": return $get("<%= pnlPartReturnList.ClientID %>"); break;
                case "HDNESNNUMBER": return $get("<%= hdnESNNumber.ClientID %>"); break;
                case "HDNESNID": return $get("<%= hdnESNID.ClientID %>"); break;
                case "EMAILSEND": return $get("<%= EmailSend.ClientID %>"); break;
                case "HDNSELECTEDLOGID": return $get("<%= hdnSelectedLogID.ClientID %>"); break;
                case "DRPPROCESSLIST": return $get("<%= drpProcessList.ClientID %>"); break;
                case "BTNHISTORYREFRESH": return $get("<%= btnHistoryRefresh.ClientID %>"); break;
                case "CLIENTTRANSFER": return $get("<%= ClientTransfer.ClientID %>"); break;
                case "TRANSFERTOMSC": return $get("<%= TransferToMSC.ClientID %>"); break;
                case "BTNALREADYFOUND": return $get("<%= btnAlreadyFound.ClientID %>"); break;
                case "BTNKITTINGDEFECTIVE": return $get("<%= btnKittingDefective.ClientID %>"); break;
                case "LBLESNFOUNDTEXT": return $get("<%= lblESNFoundText.ClientID %>"); break;
                case "LBLVERSIONTAB": return $get("<%= lblVersionTab.ClientID %>"); break;
                case "LBLPROCESSHEADER": return $get("<%= lblProcessHeader.ClientID %>"); break;
                case "LBLAUTHORIZATIONTAB": return $get("<%= lblAuthorizationTab.ClientID %>"); break;
                case "LBLHISTORYTAB": return $get("<%= lblHistoryTab.ClientID %>"); break;
                case "LBLDATATAB": return $get("<%= lblDataTab.ClientID %>"); break;
                case "LBLCARRIERLOCK": return $get("<%= lblCarrierLock.ClientID %>"); break;
                case "HDNCLIENTIDX": return $get("<%= hdnClientIDx.ClientID %>"); break;
                case "HDNCARRIERIDX": return $get("<%= hdnCarrierIDx.ClientID %>"); break;
                case "HDNMANUFACTURERIDX": return $get("<%= hdnManufacturerIDx.ClientID %>"); break;
                case "HDNMODELIDX": return $get("<%= hdnModelIDx.ClientID %>"); break;
                case "HDNCOLOURIDX": return $get("<%= hdnColourIDx.ClientID %>"); break;
                case "HDNMEMORYIDX": return $get("<%= hdnMemoryIDx.ClientID %>"); break;
                case "PARTNUMBERIDS": return $get("<%= hdnPartNumberIDs.ClientID %>"); break;
                case "HDNPONUMBERIDS": return $get("<%= hdnPONumberIDs.ClientID %>"); break;
                case "HDNPOVENDORIDS": return $get("<%= hdnPOVendorIDs.ClientID %>"); break;
                case "HDNPOLINENUMBERIDS": return $get("<%= hdnPOLineNumberIDs.ClientID %>"); break;
                case "HDNPOUNNITCOSTIDS": return $get("<%= hdnPOUnnitCostIDs.ClientID %>"); break;
                case "AR": return $get("<%= HdnAuthorizeRequired.ClientID %>"); break;
                case "CLIENTLOCATIONEMAIL": return $get("<%= HdnClientLocationEmail.ClientID %>"); break;
                case "CLIENTLOCATIONEMAIL2": return $get("<%= HdnClientLocationEmail2.ClientID %>"); break;
                case "SCANKEY": return $get("<%= ScanKey.ClientID %>"); break;
                case "HEADERDATA": return $get("<%= HdnHeaderData.ClientID %>"); break;
                case "KEEPUNITACTIVE": return $get("<%= HdnKeepUnitActive.ClientID %>"); break;
                case "RECEIVEHEADERID": return $get("<%= hdnReceiveHeaderID.ClientID %>"); break;
                case "RECEIVEDETAILBULKID": return $get("<%= hdnReceiveDetailBulkID.ClientID %>"); break;
                case "RECEIVEDETAILID": return $get("<%= hdnReceiveDetailID.ClientID %>"); break;
                case "ESN": return $get("<%= txtESN.ClientID %>"); break;
                case "ESNVERSION": return $get("<%= txtESNVersion.ClientID %>"); break;
                case "LASTESN": return $get("<%= hdnLastESN.ClientID %>"); break;
                case "LASTESNVERSION": return $get("<%= hdnLastESNVersion.ClientID %>"); break;
                case "RMA": return $get("<%= txtRMA.ClientID %>"); break;
                //case "RMAROW": return $get("< %= lblRMA_Row.ClientID %>"); break;
                case "WAITINGFORRMAROW": return $get("<%= tr_WaitingForRMA.ClientID %>"); break;
                case "WAITINGFORRMACHECK": return $get("<%= imgWaitingForRMACheck.ClientID %>"); break;
                case "WAITINGFORRMAX": return $get("<%= imgWaitingForRMAX.ClientID %>"); break;
                case "WAITINGFORRMATEXT": return $get("<%= imgWaitingForRMAX.ClientID %>"); break;
                case "PTAG": return $get("<%= txtProjectTag.ClientID %>"); break;
                //case "PTAGROW": return $get("< %= lblPTag_Row.ClientID %>"); break;
                case "WAITINGFORPTAGROW": return $get("<%= tr_WaitingForPTag.ClientID %>"); break;
                case "WAITINGFORPTAGCHECK": return $get("<%= imgWaitingForPTagCheck.ClientID %>"); break;
                case "WAITINGFORPTAGX": return $get("<%= imgWaitingForPTagX.ClientID %>"); break;
                case "WAITINGFORPTAGTEXT": return $get("<%= lblWaitingForPTagText.ClientID %>"); break;
                case "WAITINGFORIMEIROW": return $get("<%= tr_WaitingForIMEI.ClientID %>"); break;
                case "WAITINGFORIMEICHECK": return $get("<%= imgWaitingForIMEICheck.ClientID %>"); break;
                case "WAITINGFORIMEIX": return $get("<%= imgWaitingForIMEIX.ClientID %>"); break;
                case "WAITINGFORIMEITEXT": return $get("<%= lblWaitingForIMEIText.ClientID %>"); break;
                case "WAITINGFORCLIENTROW": return $get("<%= tr_WaitingForClient.ClientID %>"); break;
                case "WAITINGFORCLIENTCHECK": return $get("<%= imgWaitingForClientCheck.ClientID %>"); break;
                case "WAITINGFORCLIENTX": return $get("<%= imgWaitingForClientX.ClientID %>"); break;
                case "WAITINGFORCLIENTTEXT": return $get("<%= lblWaitingForClientText.ClientID %>"); break;
                case "ESN": return $get("<%= txtESN.ClientID %>"); break;
                case "QTY": return $get("<%= txtQTY.ClientID %>"); break;
                case "DATERECEIVED": return $get("<%= txtDateReceived.ClientID %>"); break;
                case "CLIENTLOCATIONID": return $get("<%= hdnClientLocationID.ClientID %>"); break;
                case "TXTBULKPROCESS": return $get("<%= txtBulkProcess.ClientID %>"); break;
                    
                case "CLIENTNAME": return $get("<%= txtClientName.ClientID %>"); break;
                case "STORENUMBER": return $get("<%= txtStoreNumber.ClientID %>"); break;
                case "STORESUFFIX": return $get("<%= txtStoreSuffix.ClientID %>"); break;
                case "CLIENTADDRESS": return $get("<%= txtClientAddress.ClientID %>"); break;
                case "CURRENTPROCESS": return $get("<%= HdnCurrentProcess.ClientID %>"); break;
                case "CURRENTPROCESSID": return $get("<%= HdnCurrentProcessID.ClientID %>"); break;
                case "NEXTSTEP": return $get("<%= HdnNextStep.ClientID %>"); break;
                case "NEXTSTEPID": return $get("<%= HdnNextStepID.ClientID %>"); break;
                case "NEXTPROCESS": return $get("<%= HdnNextProcess.ClientID %>"); break;
                case "NEXTPROCESSID": return $get("<%= HdnNextProcessID.ClientID %>"); break;
                case "PROJSETUP": return $get("<%= HdnProjSetup.ClientID %>"); break;
                case "AUTOPRINT": return $get("<%= chkAutoPrintBagTag.ClientID %>"); break;
                case "LBLMAKEMODELTITLE": return $get("<%= lblMakeModelTitle.ClientID %>"); break;
                case "LBLIMEICOUNT": return $get("<%= lblIMEICount.ClientID %>"); break;
                case "LBLIMEISTATUS": return $get("<%= lblIMEIStatus.ClientID %>"); break;
                case "TXTORDERNUMBER": return $get("<%= txtOrderNumber.ClientID %>"); break;
                case "TXTIMEILIST": return $get("<%= txtIMEIList.ClientID %>"); break;
                case "BTNSAVE": return $get("<%= btnSave.ClientID %>"); break;
                case "BTNTSAVE": return $get("<%= btnTSave.ClientID %>"); break;
                case "BTNSAVE_B": return $get("<%= btnSave_b.ClientID %>"); break;
                case "BTNNEXTPROCESS": return $get("<%= btnNextProcess.ClientID %>"); break;
                case "DRPPROJECTLIST": return $get("<%=drpProjectList.ClientID %>"); break;
                case "INPUTAREA": return $get("<%= pnlInPutArea.ClientID %>"); break;
                case "HEADERAREA": return $get("<%= pnlHeaderArea.ClientID %>"); break;
                case "INPUTTARGETAREA": return $get("<%= pnlInputTargetArea.ClientID %>"); break;
                case "STATUSPANEL": return $get("<%= txtStatusPanel.ClientID %>"); break;
                case "DISPLAY_ESN": return $get("<%= Display_ESN.ClientID %>"); break;
                case "DISPLAY_MSG": return $get("<%= Display_MSG.ClientID %>"); break;
                case "STATUSPANEL_B": return $get("<%= txtStatusPanel_B.ClientID %>"); break;
                case "DISPLAY_ESN_B": return $get("<%= Display_ESN_B.ClientID %>"); break;
                case "DISPLAY_MSG_B": return $get("<%= Display_MSG_B.ClientID %>"); break;
                case "T1X": return $get("<%= t1x.ClientID %>"); break;
                case "SEARCHRETURNMODE": return $get("<%= hdnSearchReturnMode.ClientID %>"); break;
                case "LBLACTIVEPROCESS": return $get("<%= lblActiveProcess.ClientID %>"); break;
                case "CHKPROCESSCHECKLIST": return $get("<%= chkProcessCheckList.ClientID %>"); break;
                case "HDNSOURCEORTARGET": return $get("<%= hdnSourceOrTarget.ClientID %>"); break;
                case "LBLTARGETEDT": return $get("<%= lblTargetEDT.ClientID %>"); break;
                case "LBLACTIVEPROCESSEDT": return $get("<%= lblActiveProcessEDT.ClientID %>"); break;
                case "USERNAME": return $get("<%= hdnUserName.ClientID %>"); break;
                case "STEPUP": return $get("<%= hdnStepUp.ClientID %>"); break;
                case "SCANKEYHISTORY": return $get("<%= ScanKeyHistory.ClientID %>"); break;
                case "HDNALLOWDUPADD": return $get("<%= hdnAllowDupAdd.ClientID %>"); break;
                case "LSTHISTORY": return $get("<%= lstHistory.ClientID %>"); break;
                case "TXTHISTORYCOUNT": return $get("<%= txtHistoryCount.ClientID %>"); break;
                case "PNLHEADER_01": return $get("<%= pnlHeader_01.ClientID %>"); break;
                case "PNLMAINENTRY": return $get("<%= pnlmainentry.ClientID %>"); break;
                case "HDNMANDITORYFIELDS": return $get("<%= hdnManditoryFields.ClientID %>"); break;
                case "HDNISPROCESSREADONLY": return $get("<%= hdnIsProcessReadOnly.ClientID %>"); break;
                case "HDNBINID": return $get("<%= hdnBinID.ClientID %>"); break;
                case "HDNLABDESTINATIONID": return $get("<%= hdnLabDestinationID.ClientID %>"); break;
                case "HDNOPENTIME": return $get("<%= hdnOpenTime.ClientID %>"); break;
                case "HDNCARRIERID": return $get("<%= hdnCarrierID.ClientID %>"); break;
                case "HDNMANUFACTURERID": return $get("<%= hdnManufacturerID.ClientID %>"); break;
                case "HDNMODELID": return $get("<%= hdnModelID.ClientID %>"); break;
                case "HDNCOLOURID": return $get("<%= hdnColourID.ClientID %>"); break;
                case "HDNMEMORYID": return $get("<%= hdnMemoryID.ClientID %>"); break;
                case "HDNALLOWVERSIONFIND": return $get("<%= hdnAllowVersionFind.ClientID %>"); break;
                case "TXTTOOLTIP": return $get("<%= txtToolTip.ClientID %>"); break;
                case "HDNISMASTERLINKED": return $get("<%= hdnisMasterLinked.ClientID %>"); break;
                case "HDNQUESTIONIDLIST": return $get("<%= hdnQuestionIDList.ClientID %>"); break;
                case "HDNQUESTIONCLIENTIDLIST": return $get("<%= hdnQuestionClientIDList.ClientID %>"); break;
                case "HDNSAVEPROCESSID": return $get("<%= hdnSaveProcessID.ClientID %>"); break;
                case "HDNFORCEPRINTONSAVE": return $get("<%= hdnForcePrintOnSave.ClientID %>"); break;
                case "FORCEPRINTONSAVE": return $get("<%= hdnForcePrintOnSave.ClientID %>"); break;
                case "HDNISTHREADEDSAVE": return $get("<%= hdnIsThreadedSave.ClientID %>"); break;
                case "ISTHREADEDSAVE": return $get("<%= hdnIsThreadedSave.ClientID %>"); break;
                case "OPTIONKEYREPLACEIMEI": return $get("<%= hdnOptionKeyReplaceIMEI.ClientID %>"); break;
                case "HDNALLOWPROJECTPASSTHROUGH": return $get("<%= hdnAllowProjectPassThrough.ClientID %>"); break;
                case "HDNDEALERPORTAL": return $get("<%= hdnDealerPortal.ClientID %>"); break;
                case "LBLPROJECTCLIENTLOCATIONBINTITLE": return $get("<%= lblProjectClientLocationBinTitle.ClientID %>"); break;
                case "ISIMEIBULK": return $get("<%= hdnIsIMEIBulk.ClientID %>"); break;
                case "HDNISIMEIBULK": return $get("<%= hdnIsIMEIBulk.ClientID %>"); break;
                case "HDNCALLEDFROM": return $get("<%= hdnCalledFrom.ClientID %>"); break;
                case "CALLEDFROM": return $get("<%= hdnCalledFrom.ClientID %>"); break;
                case "HDNPROJECTDEPENDENCIES": return $get("<%= hdnProjectDependencies.ClientID %>"); break;
                case "PROJECTDEPENDENCIES": return $get("<%= hdnProjectDependencies.ClientID %>"); break;
                case "PROCESSDEPENDENCIES": return $get("<%= hdnProcessDependencies.ClientID %>"); break;
                case "CHKSTICKY": return $get("<%= chkSticky.ClientID %>"); break;
                case "STICKY": return $get("<%= chkSticky.ClientID %>"); break;
                case "HDNSTICKYDATA": return $get("<%= hdnStickyData.ClientID %>"); break;
                case "HDNALLOWXBINX": return $get("<%= hdnAllowXBINX.ClientID %>"); break;
                case "ALLOWXBINX": return $get("<%= hdnAllowXBINX.ClientID %>"); break;
                case "STICKYDATA": return $get("<%= hdnStickyData.ClientID %>"); break;
                case "AUTOSAVE": return $get("<%= chkAutoSaveOnScan.ClientID %>"); break;
                case "DOAUTHORIZE": return $get("<%= hdnDoAuthorize.ClientID %>"); break;
                case "ISCLIENTSCREEN": return $get("<%= isClientScreen.ClientID %>"); break;
                case "SPROJECTOVERRIDE": return $get("<%= hdnisSecondaryProjectOverride.ClientID %>"); break;
                case "HDNFORCELOADID": return $get("<%= hdnForceLoadID.ClientID %>"); break;
                case "HDNFORCELOADPID": return $get("<%= hdnForceLoadPID.ClientID %>"); break;
                case "HDNFORCELOADPROCESSNAME": return $get("<%= hdnForceLoadProcessName.ClientID %>"); break;
                case "BTNJUMPPROJECT": return $get("<%= btnJumpProject.ClientID %>"); break;
                case "HDNROLELIST": return $get("<%= hdnRoleList.ClientID %>"); break;
                default: return null;
            }
        }

        ///////////////////////////////////////////////////////////////////////

        function OpenUnit(ID, PID, ProcessName) {

            if (ID.length == 0 || PID.length == 0) { return; }
            MCL('hdnForceLoadID').value = ID;
            MCL('hdnForceLoadPID').value = PID;
            MCL('hdnForceLoadProcessName').value = ProcessName;
            //           document.forms[0].submit();
            //           __doPostBack(MCL('drpProjectList'), 0);
            //           alert('<%= drpProjectList.ClientID %>');
            //           __doPostBack('ctl00$cMC$drpProjectList','');
            //setTimeout('__doPostBack(\'ctl00$cMC$drpProjectList\',\'\')', 0);

            MCL('btnJumpProject').click()                //.trigger('click')

            //           __doPostBack('<%= btnJumpProject.ClientID %>', '');
            //__doPostBack('', "");
            return;

            ////           alert('Open New Unit:' + ProcessName);
            ////           return;

            //           //var pstring = GetParameterStream(GetReportParameterList("CLIENTSUBMIT"));
            //           //var pstring = "ESN=A00000291E3942&ID=3251&PID=1"

            //           var pstring = "ID=" + ID + "&PID=" + PID + "&PName=" + ProcessName;
            //           //var pstring = "ESN=A00000291E3942&ID=3251&PID=5"


            //           // var WindowToOpen = "RPT_SpotCountReport.aspx";
            //           var WindowToOpen = "Receive.aspx";

            //           if (pstring.length > 0) {
            //               WindowToOpen = WindowToOpen + "?" + pstring
            //           }
            //           var win = window.open(WindowToOpen, "_blank", "", true);
            //           //var win = window.open(WindowToOpen, "_blank", "menubar", true);
            //           return;
        }
        

    </script>
    <script type="text/javascript">
        // $(document).ready(function () { 
        //     $("select").select2({
        //     // theme: "bootstrap4"
        //     });

        // });
  
  $(document).ready(function() {
    // Loop through each <select> element
    $('select').each(function() {
      // Check if the <select> element has more than 5 <option> elements
      if ($(this).find('option').length > 5) {
        // Initialize Select2 for this <select>
        $(this).select2({
            theme: "bootstrap4"
          });
      }
    });
  });


    </script>
</asp:Content>
