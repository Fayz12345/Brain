<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Recv01.aspx.cs" Inherits="BW_WebApp.Recv01" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>
<%@ Register Assembly="Syncfusion.Tools.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89"  Namespace="Syncfusion.Web.UI.WebControls.Tools" TagPrefix="syncfusion" %>

<%--<%@ Register assembly="System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="System.Web.UI.HtmlControls" tagprefix="cc1" %>--%>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server">   
<%--    <link href="../Javascripts/jquery-ui-1.8.1.custom.css" rel="stylesheet" type="text/css" /> 
    <script src="../Javascripts/jquery-1.4.1-vsdoc.js" type="text/javascript"></script>
    <script src="../Javascripts/jquery-ui-1.8.1.custom.min.js" type="text/javascript"></script> --%>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="cMC" runat="server">
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


                <asp:HiddenField ID="hdnOptionKeyReplaceIMEI" runat="server" ClientIDMode="Static" />



                <asp:HiddenField ID="isClientScreen" runat="server" ClientIDMode="Static" />

                <%--<asp:HiddenField ID="hdnCheckBinNumber" runat="server" ClientIDMode="Static" />--%>
                <div id="headercontainer">
                <asp:Label runat="server" ID="lblProcessHeader" Text="" Width="98%"></asp:Label></div><br />
<%--                <br />
                <br />--%>

                <%--<asp:Button ID="bTest" runat="server" Text="Test" OnClientClick="test();return false;" />--%>


                Project:
                <asp:DropDownList ID="drpProjectList" runat="server" ToolTip="Project" AutoPostBack="True">
                </asp:DropDownList>
                <asp:HiddenField ID="HdnProjSetup" runat="server" />
                <asp:Button ID="btnBagTag" runat="server" Text="Print Form ++" />
                <asp:Button ID="btnClearData" runat="server" Text="Clear --" />
                <asp:Button ID="btnSave" runat="server" Text="Save **" />
                <asp:Button ID="btnTSave" runat="server" Text="TSave" Enabled="False" />
                <asp:Button ID="btnCheckBin" runat="server" Text="Check Bin" OnClientClick="ShowBinReport();return false;"/>
                <asp:Button ID="btnUnitView" runat="server" Text="Unit View" OnClientClick="ShowUnitViewReport();return false;" />
                <asp:Button ID="btnSwitch" runat="server" Text="Switch" ToolTip="Switch this unit with another" OnClientClick="SwitchIMEI();return false;"/>
                <asp:Button ID="btnNextProcess" runat="server" Text="P" Height="2px" Width="2px" style="display: none;visibility: hidden;"/>
                <asp:Button ID="btnJumpProject" runat="server" Text="P" Height="2px" Width="2px" style="display: none;visibility: hidden;"/>

                <asp:Button ID="btnShowTimeLog" runat="server" Text="Log" OnClientClick="ShowLogReport();return false;" />
                <asp:Button ID="btnClearLog" runat="server" Text="Clear Log" OnClientClick="ClearLogReport();return false;" />
                <asp:Label ID="lblSessionTime" runat="server" Text=""></asp:Label>

<%--

A table needs to be created here, one column for the report and the other column for pnlmainentry

--%>
            </asp:Panel>
            <asp:Table ID="Table8" runat="server">
                <asp:TableRow>
                    <asp:TableCell VerticalAlign="Top" HorizontalAlign="Left" width="15%">
                        <asp:Repeater ID="ViewProcess" runat="server">
                            <ItemTemplate>
                                <asp:Button ID="btnViewProcess" runat="server" Text="" UseSubmitBehavior="False" width="125px" class="button" /><br />
                            </ItemTemplate>
                        </asp:Repeater>
                    </asp:TableCell>
                    <asp:TableCell VerticalAlign="Top" HorizontalAlign="Left" Width="70%">
                        <asp:Panel ID="pnlmainentry" runat="server" HorizontalAlign="Left">
                            <table style="width: 100%;">
                                <tr>
                                    <td align="left" valign="top">

                                        <asp:Panel ID="pnlHeader" runat="server" HorizontalAlign="Left" Width="100%">
                                            <asp:Table ID="Table10" runat="server" Height="100%" Width="100%">
                                                <asp:TableRow>
                                                    <asp:TableCell VerticalAlign="Top" Width="70%">
                                                        <asp:TextBox ID="txtClientName" runat="server" Width="98%" TabIndex="1" Text="" ToolTip="Store Name" Enabled="False">
                                                        </asp:TextBox>
                                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtClientName"
                                                            WatermarkText="Client Name">
                                                        </asp:TextBoxWatermarkExtender>
                                                        <asp:TextBox ID="txtClientAddress" runat="server" Width="98%"
                                                            Text="" TextMode="MultiLine" Enabled="False" ToolTip="Location Address" Rows="4">
                                                        </asp:TextBox>
                                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" TargetControlID="txtClientAddress"
                                                            WatermarkText="Client Address">
                                                        </asp:TextBoxWatermarkExtender>


                                                    </asp:TableCell>
                                                    <asp:TableCell VerticalAlign="Top" HorizontalAlign="Right">
                                                        <asp:TextBox ID="txtStoreNumber" runat="server" Text="" ToolTip="Store Number" Enabled="False" Width="50%">
                                                        </asp:TextBox>
                                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtStoreNumber"
                                                            WatermarkText="Store Number">
                                                        </asp:TextBoxWatermarkExtender>
                                                        <asp:TextBox ID="txtStoreSuffix" runat="server" ClientIDMode="Static" Text="" ToolTip="Store Suffix" Enabled="False" Width="20%">
                                                        </asp:TextBox>
                                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtStoreSuffix"
                                                            WatermarkText="Store Suffix">
                                                        </asp:TextBoxWatermarkExtender>
                                                        &nbsp;
                                                        <asp:ImageButton ID="btnSearchClient" runat="server" ImageUrl="~/Images/Find_Search_64.png"
                                                            OnClientClick="OpenClientSearch();return false;" Width="15px" ToolTip="Search Clients"
                                                            ImageAlign="Middle" />

                                                        <asp:Label ID="lblESN" runat="server" Text="ESN/IMEI" AssociatedControlID="txtESN"></asp:Label>
                                                        <asp:TextBox ID="txtESN" runat="server" ClientIDMode="Static" TabIndex="3" Text=""
                                                            ToolTip="ESN/IMEI Number" Width="60%"></asp:TextBox>
                                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtESN"
                                                            WatermarkText="ESN/IMEI Number">
                                                        </asp:TextBoxWatermarkExtender>

                                                        <br />

                                                        <asp:Label ID="lblRMA_Row" runat="server" Text="RMA" AssociatedControlID="txtRMA"></asp:Label>
                                                        <asp:TextBox ID="txtRMA" runat="server" ClientIDMode="Static" TabIndex="2"
                                                            Text="" ToolTip="RMA Number" Width="60%"></asp:TextBox>
                                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender9" runat="server" TargetControlID="txtRMA"
                                                            WatermarkText="RMA Number">
                                                        </asp:TextBoxWatermarkExtender>
                                                        <br />
                                                        <asp:Label ID="lblPTag_Row" runat="server" Text="Project Tag" AssociatedControlID="txtProjectTag"></asp:Label>
                                                        <asp:TextBox ID="txtProjectTag" runat="server" ClientIDMode="Static"
                                                            TabIndex="2" Text="" ToolTip="Project Tag" Width="60%"></asp:TextBox>
                                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtProjectTag"
                                                            WatermarkText="Project Tag">
                                                        </asp:TextBoxWatermarkExtender>

                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell >
                                                        <asp:TextBox ID="Display_ESN" runat="server" Width="25%" ClientIDMode="Static" BackColor="#4DDDF9"
                                                            Text="" Enabled="True" Style="text-align: left" ForeColor="Black" Font-Bold="True"
                                                            ReadOnly="True"></asp:TextBox>
                                                        <asp:TextBox ID="Display_MSG" runat="server" Width="69%" ClientIDMode="Static" BackColor="#4DDDF9"
                                                            Text="" Enabled="True" Style="text-align: center" ForeColor="Black" Font-Bold="True"
                                                            ReadOnly="True"></asp:TextBox>
                                                    </asp:TableCell>
                                                    <asp:TableCell >
                                                        <asp:TextBox ID="txtStatusPanel" runat="server" Width="95%" ClientIDMode="Static"
                                                            BackColor="#4DDDF9" Text="" Enabled="True" Style="text-align: right" ForeColor="Black"
                                                            Font-Bold="True" ReadOnly="True"></asp:TextBox>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow >
                                                    <asp:TableCell VerticalAlign="Top" style="border: 2px solid #AAA;border-radius: 5px;background-color: #FFF;" HorizontalAlign="Right">

<%--                                                        <asp:CheckBox ID="chkOldSave" runat="server" ToolTip="Click here to save via the Old way."
                                                            Text="Old Save" TextAlign="Left" />
                                                        <asp:CheckBox ID="chkRunThreaded" runat="server" ToolTip="Click here to save via Thread."
                                                            Text="Thread" TextAlign="Left" />--%>
                                                        <asp:CheckBox ID="chkAutoSaveOnScan" runat="server" ToolTip="Click here to have the system automatically Save following an ESN scan."
                                                            Text="Auto Save" TextAlign="Left" />
                                                        <asp:CheckBox ID="chkAutoPrintBagTag" runat="server" ToolTip="Click here to Print Form auto printed once saved."
                                                            Text="Print" TextAlign="Left" />
                                                        <br />

                                                        <asp:Label ID="lblScanFieldText" runat="server" Text="Scan Field:" AssociatedControlID="ScanKey" Font-Size="Large"></asp:Label>
                                                        <asp:TextBox ID="ScanKey" runat="server" BackColor="#A5EBC5" ForeColor="Black" Width="77%" Font-Size="X-Large"></asp:TextBox>
                                                         <br />


                                                        <asp:TextBox ID="txtQTY" runat="server" ClientIDMode="Static" TabIndex="2" Text="" ToolTip="Quantity" Width="16%"></asp:TextBox>
                                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender8" runat="server" TargetControlID="txtQTY"
                                                            WatermarkText="Quantity">
                                                        </asp:TextBoxWatermarkExtender>
                                                        <asp:TextBox ID="ScanKeyHistory" runat="server" ToolTip="Scan Key History" Enabled="False" Width="38.5%"></asp:TextBox>
                                                        <asp:TextBox ID="txtDateReceived" runat="server" Text="" ToolTip="Date when Item arrived at GMP" Width="36%"></asp:TextBox>
                                                    </asp:TableCell>
                                                    <asp:TableCell style="border: 1px solid #AAA; border-radius: 5px;background-color: #FFF;">
                                                        <asp:Table ID="Table3" runat="server">

                                                            <asp:TableRow ID="tr_WaitingForIMEI">
                                                                <asp:TableCell VerticalAlign="Top" HorizontalAlign="Right" Wrap="False">
                                                                    <asp:Image ID="imgWaitingForIMEIX" runat="server" ImageUrl="~/Styles/images/delete-sm.png" />
                                                                    <asp:Image ID="imgWaitingForIMEICheck" runat="server" ImageUrl="~/Styles/images/check-sm.png" />
                                                                </asp:TableCell>
                                                                <asp:TableCell VerticalAlign="Top" HorizontalAlign="Left" Wrap="False">
                                                                    <asp:Label ID="lblWaitingForIMEIText" runat="server" Text="ESN/IMEI" Width="100%"></asp:Label>
                                                                </asp:TableCell>
                                                            </asp:TableRow>


                                                            <asp:TableRow ID="tr_WaitingForClient">
                                                                <asp:TableCell VerticalAlign="Top" HorizontalAlign="Right" Wrap="False">
                                                                    <asp:Image ID="imgWaitingForClientX" runat="server" ImageUrl="~/Styles/images/delete-sm.png" />
                                                                    <asp:Image ID="imgWaitingForClientCheck" runat="server" ImageUrl="~/Styles/images/check-sm.png" />
                                                                </asp:TableCell>
                                                                <asp:TableCell VerticalAlign="Top" HorizontalAlign="Left" Wrap="False" Width="100%">
                                                                    <asp:Label ID="lblWaitingForClientText" runat="server" Text="Client" Width="100%"></asp:Label>
                                                                </asp:TableCell>
                                                            </asp:TableRow>
                                                            <asp:TableRow ID="tr_WaitingForRMA">
                                                                <asp:TableCell VerticalAlign="Top" HorizontalAlign="Right" Wrap="False">
                                                                    <asp:Image ID="imgWaitingForRMAX" runat="server" ImageUrl="~/Styles/images/delete-sm.png" />
                                                                    <asp:Image ID="imgWaitingForRMACheck" runat="server" ImageUrl="~/Styles/images/check-sm.png" />
                                                                </asp:TableCell>
                                                                <asp:TableCell VerticalAlign="Top" HorizontalAlign="Left" Wrap="False">
                                                                    <asp:Label ID="lblWaitingForRMAText" runat="server" Text="RMA Number" Width="100%"></asp:Label>
                                                                </asp:TableCell>
                                                            </asp:TableRow>
                                                            <asp:TableRow ID="tr_WaitingForPTag">
                                                                <asp:TableCell VerticalAlign="Top" HorizontalAlign="Right" Wrap="False">
                                                                    <asp:Image ID="imgWaitingForPTagX" runat="server" ImageUrl="~/Styles/images/delete-sm.png" />
                                                                    <asp:Image ID="imgWaitingForPTagCheck" runat="server" ImageUrl="~/Styles/images/check-sm.png" />
                                                                </asp:TableCell>
                                                                <asp:TableCell VerticalAlign="Top" HorizontalAlign="Left" Wrap="False">
                                                                    <asp:Label ID="lblWaitingForPTagText" runat="server" Text="Project Tag" Width="100%"></asp:Label>
                                                                </asp:TableCell>
                                                            </asp:TableRow>
                                                        </asp:Table>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                            </asp:Table>
                                        </asp:Panel>
                                        <asp:TabContainer runat="server" ID="t1x" Width="100%" ActiveTabIndex="0">
                                            <asp:TabPanel runat="server" ID="tb1x" Enabled="true" HeaderText="Data" Width="100%"
                                                Height="100%">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblDataTab" runat="server" Text="Data"></asp:Label>
                                                </HeaderTemplate>
                                                <ContentTemplate>


<%--                <syncfusion:Splitter ID="Splitter1" runat="server" Layout="Vertical" Width="100%">
                    <syncfusion:SplitPane ID="SplitPane1" runat="server">--%>

                                                    <asp:Repeater ID="NextStep" runat="server">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnNextStep" runat="server" Text="" UseSubmitBehavior="False" />
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center" Width="100%">
                                                        <asp:Label ID="lblMakeModelTitle" runat="server" Text="" Font-Size="Large"></asp:Label>
                                                        <br />
                                                        <asp:Label ID="lblProjectClientLocationBinTitle" runat="server" Text="" Font-Size="Large"></asp:Label>
                                                        <br />
                                                        <asp:CheckBoxList ID="chkProcessCheckList" CellPadding="5" CellSpacing="5" RepeatLayout="Flow"
                                                            TextAlign="Right" runat="server" RepeatDirection="Horizontal" RepeatColumns="10">
                                                        </asp:CheckBoxList>
                                                    </asp:Panel>




                                                    <asp:CheckBox ID="chkSticky" runat="server" Text='Keep Static' Checked="False" ToolTip='Select to turn "Static" on/off'
                                                        TextAlign="Left" OnClick="javascript:SaveSticky()"/>

<%--                    </syncfusion:SplitPane>
                    <syncfusion:SplitterBar ID="SplitterBar1" runat="server" CollapseMode="both" />
                    <syncfusion:SplitPane ID="SplitPane2" runat="server" ScrollMode="Vertical" Height="50%">--%>

                                                    <asp:Panel ID="pnlxInPutAreax" runat="server">
                                                        <asp:Panel ID="pnlHeaderArea" runat="server" Width="100%" ScrollBars="Auto">
                                                            <asp:Label runat="server" ID="Location" Text="" CssClass="rightColumn"></asp:Label>

                                                            <asp:GridView ID="GridView2" runat="server" DataKeyField="QuestionID" AutoGenerateColumns="False"
                                                                 ShowHeader="False" Width="100%" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                                                 <SelectedRowStyle CssClass="srowstyle" />

                                                                <Columns>
                                                                    <asp:TemplateField ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Right"
                                                                        ItemStyle-Wrap="False" ShowHeader="False" >
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="Descriptionh" Text=""></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField ItemStyle-Wrap="False" ShowHeader="False">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="CurrencyAnswerH" runat="server" ClientIDMode="Predictable">
                                                                            </asp:TextBox>
                                                                            <asp:TextBox ID="NumericAnswerh" runat="server" ClientIDMode="Predictable">
                                                                            </asp:TextBox>
                                                                            <asp:TextBox ID="TextAnswerh" runat="server" Enabled="False">
                                                                            </asp:TextBox>

                                                                            <asp:TextBox ID="Num3Digith" runat="server" Enabled="False">
                                                                            </asp:TextBox>
                                                                            <asp:TextBox ID="Text20Digith" runat="server" Enabled="False">
                                                                            </asp:TextBox>
                                                                            <asp:TextBox ID="Text3Digith" runat="server" Enabled="False">
                                                                            </asp:TextBox>
                                                                            <asp:TextBox ID="Text10Digith" runat="server" Enabled="False">
                                                                            </asp:TextBox>
                                                                            <asp:TextBox ID="Text18Digith" runat="server" Enabled="False">
                                                                            </asp:TextBox>
                                                                            <asp:TextBox ID="Text50Digith" runat="server" Enabled="False">
                                                                            </asp:TextBox>



                                                                            <asp:RadioButtonList ID="RadioAnswerh" runat="server" RepeatDirection="Horizontal"
                                                                                RepeatColumns="4" Enabled="False">
                                                                            </asp:RadioButtonList>




<%--
                            <syncfusion:MultiSelectionDropDown
                                ID="checkAnswerh" runat="server" Width="383px" Font-Size="12px" Font-Names="Trebuchet MS"
                                AutoFormat="Office2007 Blue" CheckBoxListRepeatColumns="4" ClientIDMode="Predictable">
                                <CheckBoxListStyle BorderColor="#89AEDA" BackColor="#EEF6FF" BorderStyle="Ridge"
                                    ForeColor="Black" BorderWidth="3px" Font-Names="Trebuchet MS" Font-Size="12px" />
                                <Items>
                                    <asp:ListItem>Chai</asp:ListItem>
                                    <asp:ListItem>Chang</asp:ListItem>
                                    <asp:ListItem>Chef Anton's Cajun Seasoning</asp:ListItem>
                                    <asp:ListItem>Chef Anton's Gumbo Mix</asp:ListItem>
                                    <asp:ListItem>Grandma's Boysenberry Spread</asp:ListItem>
                                    <asp:ListItem>Uncle Bob's Organic Dried Pears</asp:ListItem>
                                    <asp:ListItem>Northwoods Cranberry Sauce</asp:ListItem>
                                    <asp:ListItem>Mishi Kobe Niku</asp:ListItem>
                                    <asp:ListItem>Ikura</asp:ListItem>
                                    <asp:ListItem>Queso Cabrales</asp:ListItem>
                                    <asp:ListItem>Queso Manchego La Pastora</asp:ListItem>
                                    <asp:ListItem>Konbu</asp:ListItem>
                                    <asp:ListItem>Tofu</asp:ListItem>
                                    <asp:ListItem>Genen Shouyu</asp:ListItem>
                                    <asp:ListItem>Pavlova</asp:ListItem>
                                    <asp:ListItem>Alice Mutton</asp:ListItem>
                                    <asp:ListItem>Carnarvon Tigers</asp:ListItem>
                                    <asp:ListItem>Teatime Chocolate Biscuits</asp:ListItem>
                                </Items>
                            </syncfusion:MultiSelectionDropDown>--%>




                                                                            <asp:CheckBoxList ID="checkAnswerh" CellPadding="5" CellSpacing="5" RepeatLayout="Table"
                                                                                TextAlign="Right" runat="server" RepeatDirection="Horizontal" RepeatColumns="4"
                                                                                Enabled="False">
                                                                            </asp:CheckBoxList>







                                                                            <asp:TextBox ID="CalAnswerh" runat="server" Enabled="False"></asp:TextBox>
                                                                            <asp:DropDownList ID="drpAnswerh" runat="server" Enabled="False">
                                                                            </asp:DropDownList>
                                                                            <asp:HiddenField ID="HiddenIDh" runat="server" />
                                                                            <asp:HiddenField ID="HiddenNameh" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnlInPutArea" runat="server" Width="100%" ScrollBars="Auto">
                                                            <b>
                                                                <asp:Table ID="Table9" Width="100%" runat="server">
                                                                    <asp:TableRow>
                                                                        <asp:TableCell>
                                                                            <asp:Label runat="server" ID="lblActiveProcess" Text=""></asp:Label>
                                                                        </asp:TableCell>
                                                                        <asp:TableCell HorizontalAlign="Right">
                                                                            <asp:Label runat="server" ID="lblActiveProcessEDT" Text="xxxx" Style="text-align: right"></asp:Label>

                                                                        <asp:ImageButton ID="imgShowUnitNote" runat="server" ImageUrl="~/Images/Info.png"
                                                                                 Width="15px" ToolTip="Open Client Notes" ImageAlign="Middle" OnClientClick="OpenUnitNote(); return false;" />
                                                                        <asp:ImageButton ID="imgShowPartList" runat="server" ImageUrl="~/Images/ChangeProcess.png"
                                                                                 Width="15px" ToolTip="Open Part List" ImageAlign="Middle" OnClientClick="OpenPartList(); return false;" />

                                                                        <asp:ImageButton ID="imgShowIFSPONumbers" runat="server" ImageUrl="~/Images/uparrow_inline.gif"
                                                                                 Width="15px" ToolTip="Open IFS PO Number List" ImageAlign="Middle" OnClientClick="OpenIFSPONumberListForLines(); return false;" />


                                                                        <asp:ImageButton ID="imgShowPartReturnList" runat="server" ImageUrl="~/Images/EditParts.gif"
                                                                                 Width="15px" ToolTip="Open Return Part List" ImageAlign="Middle" OnClientClick="OpenReturnPartList(); return false;" />
                                                                        </asp:TableCell>
                                                                    </asp:TableRow>
                                                                </asp:Table>
                                                            </b>
                                                            <asp:GridView ID="gd1" runat="server" DataKeyField="QuestionID" AutoGenerateColumns="False"
                                                                ShowHeader="False" Width="100%" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                                                <SelectedRowStyle CssClass="srowstyle" />
   
                                                                <Columns>
                                                                    <asp:TemplateField ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Right"
                                                                        ItemStyle-Wrap="True" ShowHeader="False" HeaderStyle-BorderStyle="NotSet">
                                                                        <ItemTemplate>
                                                                            <asp:HiddenField ID="HiddenID" runat="server" />
                                                                            <asp:Label runat="server" ID="Description" Text=""></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField ItemStyle-Wrap="False" ShowHeader="False">
                                                                        <ItemTemplate>


                                                                            <asp:TextBox ID="Num3Digit" runat="server" Enabled="False" MaxLength="3">
                                                                            </asp:TextBox>
                                                                             <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="Num3Digit"
                                                                                ValidChars="0123456789">
                                                                            </asp:FilteredTextBoxExtender>
                                                                            <asp:TextBox ID="Text20Digit" runat="server" Enabled="False" MaxLength="20">
                                                                            </asp:TextBox>


                                                                            <asp:TextBox ID="Text3Digit" runat="server" Enabled="False" MaxLength="3">
                                                                            </asp:TextBox>
                                                                            <asp:TextBox ID="Text10Digit" runat="server" Enabled="False" MaxLength="10">
                                                                            </asp:TextBox>
                                                                            <asp:TextBox ID="Text18Digit" runat="server" Enabled="False" MaxLength="18">
                                                                            </asp:TextBox>
                                                                            <asp:TextBox ID="Text50Digit" runat="server" Enabled="False" MaxLength="50">
                                                                            </asp:TextBox>




                                                                            <asp:TextBox ID="CurrencyAnswer" runat="server" ClientIDMode="Predictable">
                                                                            </asp:TextBox>
                                                                            <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="CurrencyAnswer"
                                                                                                    Mask="9{9}.99" MaskType="Number" InputDirection="RightToLeft" />


                                                                            <asp:TextBox ID="NumericAnswer" runat="server" ClientIDMode="Predictable">
                                                                            </asp:TextBox>
                                                                             <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="NumericAnswer"
                                                                                ValidChars="0123456789">
                                                                            </asp:FilteredTextBoxExtender>
                                                                            <asp:TextBox ID="TextAnswer" runat="server" ClientIDMode="Predictable" MaxLength="100" Width="96%">
                                                                            </asp:TextBox>
                                                                            <asp:RadioButtonList ID="RadioAnswer" runat="server" RepeatDirection="Horizontal"
                                                                                RepeatColumns="4" ClientIDMode="Predictable">
                                                                            </asp:RadioButtonList>



<%--                            <syncfusion:MultiSelectionDropDown
                                ID="checkAnswer" runat="server" Width="383px" Font-Size="12px" Font-Names="Trebuchet MS"
                                AutoFormat="Office2007 Blue" CheckBoxListRepeatColumns="4" ClientIDMode="Predictable">
                                <CheckBoxListStyle BorderColor="#89AEDA" BackColor="#EEF6FF" BorderStyle="Ridge"
                                    ForeColor="Black" BorderWidth="3px" Font-Names="Trebuchet MS" Font-Size="12px" />
                                <Items>
                                    <asp:ListItem>Chai</asp:ListItem>
                                    <asp:ListItem>Chang</asp:ListItem>
                                    <asp:ListItem>Chef Anton's Cajun Seasoning</asp:ListItem>
                                    <asp:ListItem>Chef Anton's Gumbo Mix</asp:ListItem>
                                    <asp:ListItem>Grandma's Boysenberry Spread</asp:ListItem>
                                    <asp:ListItem>Uncle Bob's Organic Dried Pears</asp:ListItem>
                                    <asp:ListItem>Northwoods Cranberry Sauce</asp:ListItem>
                                    <asp:ListItem>Mishi Kobe Niku</asp:ListItem>
                                    <asp:ListItem>Ikura</asp:ListItem>
                                    <asp:ListItem>Queso Cabrales</asp:ListItem>
                                    <asp:ListItem>Queso Manchego La Pastora</asp:ListItem>
                                    <asp:ListItem>Konbu</asp:ListItem>
                                    <asp:ListItem>Tofu</asp:ListItem>
                                    <asp:ListItem>Genen Shouyu</asp:ListItem>
                                    <asp:ListItem>Pavlova</asp:ListItem>
                                    <asp:ListItem>Alice Mutton</asp:ListItem>
                                    <asp:ListItem>Carnarvon Tigers</asp:ListItem>
                                    <asp:ListItem>Teatime Chocolate Biscuits</asp:ListItem>
                                </Items>
                            </syncfusion:MultiSelectionDropDown>--%>



                                                                            <asp:CheckBoxList ID="checkAnswer" CellPadding="5" CellSpacing="5" RepeatLayout="Table"
                                                                                TextAlign="Right" runat="server" RepeatDirection="Horizontal" RepeatColumns="4"
                                                                                ClientIDMode="Predictable">
                                                                            </asp:CheckBoxList>

                                                                            <asp:TextBox ID="CalAnswer" runat="server" ReadOnly="True" ClientIDMode="Predictable"></asp:TextBox>
                                                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="CalAnswer"
                                                                                Format="MM/dd/yyyy">
                                                                            </asp:CalendarExtender>
                                                                            <asp:DropDownList ID="drpAnswer" runat="server" ClientIDMode="Predictable">
                                                                            </asp:DropDownList>
                                                                            <asp:HiddenField ID="HiddenName" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </asp:Panel>

                                                    <asp:Panel ID="pnlInputTargetArea" runat="server" Width="100%" ScrollBars="Auto">
                                                        <b>
                                                            <asp:Table ID="Table1" Width="100%" runat="server">
                                                                <asp:TableRow>
                                                                    <asp:TableCell>
                                                                        <asp:Label runat="server" ID="lblTarget" Text="Target"></asp:Label>
                                                                    </asp:TableCell>
                                                                    <asp:TableCell HorizontalAlign="Right">
                                                                        <asp:Label runat="server" ID="lblTargetEDT" Text="xxxx" Style="text-align: right"></asp:Label>
                                                                    </asp:TableCell>
                                                                </asp:TableRow>
                                                            </asp:Table>
                                                        </b>
                                                        <%--used for Bulk Move purposes--%>
                                                        <asp:GridView ID="GridView4" runat="server" DataKeyField="QuestionID" AutoGenerateColumns="False"
                                                           CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" ShowHeader="False" Width="100%">
                                        <SelectedRowStyle CssClass="srowstyle" />

                                                            <Columns>
                                                                <asp:TemplateField ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Right"
                                                                    ItemStyle-Wrap="False" ShowHeader="False" ItemStyle-Width="20%">
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" ID="Description" Text=""></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>



                                                                    <asp:TemplateField ItemStyle-Wrap="False" ShowHeader="False">
                                                                        <ItemTemplate>

                                                                            <asp:TextBox ID="CurrencyAnswer" runat="server" ClientIDMode="Predictable">
                                                                            </asp:TextBox>
                                                                            <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="CurrencyAnswer"
                                                                                                    Mask="9{9}.99" MaskType="Number" InputDirection="RightToLeft" />


                                                                            <asp:TextBox ID="NumericAnswer" runat="server" ClientIDMode="Predictable">
                                                                            </asp:TextBox>
                                                                             <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="NumericAnswer"
                                                                                ValidChars="0123456789">
                                                                            </asp:FilteredTextBoxExtender>
                                                                            <asp:TextBox ID="TextAnswer" runat="server" ClientIDMode="Predictable">
                                                                            </asp:TextBox>
                                                                            <asp:RadioButtonList ID="RadioAnswer" runat="server" RepeatDirection="Horizontal"
                                                                                RepeatColumns="4" ClientIDMode="Predictable">
                                                                            </asp:RadioButtonList>
                                                                            <asp:CheckBoxList ID="checkAnswer" CellPadding="5" CellSpacing="5" RepeatLayout="Table"
                                                                                TextAlign="Right" runat="server" RepeatDirection="Horizontal" RepeatColumns="4"
                                                                                ClientIDMode="Predictable">
                                                                            </asp:CheckBoxList>

                                                                            <asp:TextBox ID="CalAnswer" runat="server" ReadOnly="True" ClientIDMode="Predictable"></asp:TextBox>
                                                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="CalAnswer"
                                                                                Format="MM/dd/yyyy">
                                                                            </asp:CalendarExtender>
                                                                            <asp:DropDownList ID="drpAnswer" runat="server" ClientIDMode="Predictable">
                                                                            </asp:DropDownList>
                                                                            <asp:HiddenField ID="HiddenName" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>


<%--                                                                <asp:TemplateField ItemStyle-Wrap="True" ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="TextAnswer" runat="server">
                                                                        </asp:TextBox>
                                                                        <asp:RadioButtonList ID="RadioAnswer" runat="server" RepeatDirection="Horizontal"
                                                                            RepeatColumns="4">
                                                                        </asp:RadioButtonList>
                                                                        <asp:CheckBoxList ID="checkAnswer" CellPadding="5" CellSpacing="5" RepeatLayout="Table"
                                                                            TextAlign="Right" runat="server" RepeatDirection="Horizontal" RepeatColumns="4">
                                                                        </asp:CheckBoxList>
                                                                        <asp:TextBox ID="CalAnswer" runat="server"></asp:TextBox>
                                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="CalAnswer">
                                                                        </asp:CalendarExtender>
                                                                        <asp:DropDownList ID="drpAnswer" runat="server">
                                                                        </asp:DropDownList>
                                                                        <asp:HiddenField ID="HiddenID" runat="server" />
                                                                        <asp:HiddenField ID="HiddenName" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                            </Columns>
                                                        </asp:GridView>
<%--                                                        <asp:Table runat="server">
                                                        </asp:Table>
--%>

                                                    </asp:Panel>

<%--  </syncfusion:SplitPane>
</syncfusion:Splitter>--%>

                                                </ContentTemplate>
                                            </asp:TabPanel>
                                            <asp:TabPanel runat="server" ID="tabBulkAdvance" Enabled="true" HeaderText="Bulk Advance"
                                                Width="100%" Visible="False">
                                                <ContentTemplate>
                                                    <asp:Label ID="lblBulkCount" runat="server" Text="Count:0" ToolTip="Count"></asp:Label>
                                                    <asp:Repeater ID="NextStepBulk" runat="server">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnNextStepBulk" runat="server" Text="" UseSubmitBehavior="False" />
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <asp:Panel ID="Panel2" runat="server" Width="100%" Height="100%">
                                                        <asp:ListBox ID="lstBukAdvance" runat="server" Width="100%" Height="100%" BackColor="#FFFFCC"
                                                            SelectionMode="Multiple" ViewStateMode="Inherit" ClientIDMode="Static"></asp:ListBox>
                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </asp:TabPanel>
                                            <asp:TabPanel runat="server" ID="TabPanelClientDetailx" Enabled="true" HeaderText="Search"
                                                Width="100%" Height="100%" Visible="False">
                                                <ContentTemplate>
                                                    <asp:Panel ID="Panel3" runat="server" Width="100%" Height="100%" ScrollBars="Auto">
                                                        <asp:Button ID="btnSearchRefresh" runat="server" Text="Refresh" Width="100%" />
                                                        <asp:GridView ID="gvSearchResult" runat="server" AutoGenerateColumns="False" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                        <SelectedRowStyle CssClass="srowstyle" />

                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Select">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgSelect" runat="server" HeaderText="Select" ImageUrl="~/Images/ARROW30B.ICO"
                                                                            ToolTip="Select Item as source for New entry"></asp:ImageButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Delete">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgDelete" runat="server" HeaderText="Select" ImageUrl="~/Images/delete.bmp"
                                                                            ToolTip="Delete entry"></asp:ImageButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="ReceiveDetailBulkID" HeaderText="ID" ReadOnly="True" Visible="false">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ProcessName" HeaderText="Process" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ESN" HeaderText="ESN" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="QTY" HeaderText="QTY" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="RMANumber" HeaderText="RMA" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ProjectName" HeaderText="Project" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="D">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgDetail" runat="server" HeaderText="D" ImageUrl="~/Images/POSTITS.ICO"
                                                                            ToolTip="Detail" OnClientClick="return false;"></asp:ImageButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="LastUpdateDate" HeaderText="LastUpdateDate" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </asp:TabPanel>
                                            <asp:TabPanel runat="server" ID="TabPanelClientDetail" Enabled="true" Width="100%"
                                                Visible="True">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblHistoryTab" runat="server" Text="History"></asp:Label>
                                                </HeaderTemplate>
                                                <ContentTemplate>
                                                    <asp:Panel ID="Panel14" runat="server" Width="100%" Height="100%">
                                                        <asp:Button ID="btnHistoryRefresh" runat="server" Text="Refresh" Width="100%" />
                                                        <asp:GridView ID="grdHistory" runat="server" AutoGenerateColumns="False" 
                                                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                        <SelectedRowStyle CssClass="srowstyle" />

                                                            <Columns>
                                                                <asp:BoundField DataField="ReceiveDetailID" HeaderText="ID" ReadOnly="True" Visible="false">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ProcessText" HeaderText="Process" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="MiscText" HeaderText="Note" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CreateDate" HeaderText="Date" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CreateUser" HeaderText="User" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="D">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgDelete" runat="server" HeaderText="" ImageUrl="~/Images/delete.png"
                                                                            Width="15px" ToolTip="Delete"></asp:ImageButton>
                                                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="imgDelete"
                                                                            ConfirmText="Are you sure you want to delete this file?">
                                                                        </asp:ConfirmButtonExtender>
                                                                        <asp:ImageButton ID="imgMoveProcessUp" runat="server" HeaderText="" ImageUrl="~/Images/arrow_up.png"
                                                                            Width="15px" ToolTip="Move Process Log Up"></asp:ImageButton>
                                                                        <asp:ImageButton ID="imgMoveProcessDown" runat="server" HeaderText="" ImageUrl="~/Images/arrow_down.png"
                                                                            Width="15px" ToolTip="Move Process Log Down"></asp:ImageButton>
                                                                        <asp:ImageButton ID="imgChangeProcess" runat="server" HeaderText="" ImageUrl="~/Images/ChangeProcess.png"
                                                                            Width="15px" ToolTip="Change Process"></asp:ImageButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </asp:TabPanel>
                                            <asp:TabPanel runat="server" ID="TabPanelItemVersion" Enabled="true" Width="100%"
                                                Visible="True">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblVersionTab" runat="server" Text="Version"></asp:Label>
                                                </HeaderTemplate>
                                                <ContentTemplate>
                                                    <asp:Panel ID="Panel4" runat="server" Width="100%" Height="100%">
                                                        <asp:Button ID="btnVersionRefresh" runat="server" Text="Refresh" Width="100%" />
                                                        <asp:GridView ID="grdVersion" runat="server" AutoGenerateColumns="False" DataKeyNames="ReceiveDetailID" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                        <SelectedRowStyle CssClass="srowstyle" />

                                                            <Columns>
                                                                <asp:BoundField DataField="ReceiveDetailID" HeaderText="ID" ReadOnly="True" Visible="true">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ProjectName" HeaderText="Project" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="StatusName" HeaderText="Status" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ESN" HeaderText="ESN" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Version" HeaderText="Version" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CreateDate" HeaderText="Date" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CreateUser" HeaderText="User" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="V">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgOpen" runat="server" HeaderText="" ImageUrl="~/Images/alert-small.gif"
                                                                            ToolTip="Open" OnClientClick='<%# Eval("ReceiveDetailID","LoadSheetDataDetail({0}); return false;")%>'>
                                                                        </asp:ImageButton>
<%--                                                                        <asp:ImageButton ID="imgGraveYard" runat="server" HeaderText="" ImageUrl="~/Images/PUSHPINB.bmp"
                                                                            ToolTip="Send this version to GraveYard" OnClientClick='<%# Eval("ReceiveDetailID","MoveToGraveYard({0}); return false;")%>'>
                                                                        </asp:ImageButton>--%>
<%--                                                                        <asp:ImageButton ID="imgGraveYardBack" runat="server" HeaderText="" ImageUrl="~/Images/PUSHPINB.bmp"
                                                                            ToolTip="Return this version from GraveYard" OnClientClick='<%# Eval("ReceiveDetailID","MoveBackFromGraveYard({0}); return false;")%>'>
                                                                        </asp:ImageButton>--%>
                                                                        <%--<asp:ImageButton ID="imgVersion" runat="server" HeaderText="" ImageUrl="~/Images/PUSHPINB.bmp"
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


                                            <asp:TabPanel runat="server" ID="TabPanelIDCLocationHistory" Enabled="true" Width="100%"
                                                Visible="True">
                                                <HeaderTemplate>
                                                    <asp:Label ID="Label5" runat="server" Text="IDC Location"></asp:Label>
                                                </HeaderTemplate>
                                                <ContentTemplate>
                                                    <asp:Panel ID="Panel19" runat="server" Width="100%" Height="100%">
                                                        <asp:Button ID="btnIDCLocationRefresh" runat="server" Text="Refresh" Width="100%" />
                                                        <asp:GridView ID="grdIDCLocationHistory" runat="server" AutoGenerateColumns="False"
                                                            DataKeyNames="LocationLogID" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                                            AlternatingRowStyle-CssClass="alt">
                                                            <SelectedRowStyle CssClass="srowstyle" />
                                                            <Columns>
                                                                <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Location" HeaderText="Location" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CreateDate" HeaderText="Date" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CreateUser" HeaderText="User" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                        </asp:GridView>

                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </asp:TabPanel>


                                            <asp:TabPanel runat="server" ID="TabPanelLocationHistory" Enabled="true" Width="100%"
                                                Visible="True">
                                                <HeaderTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text="IFS Location"></asp:Label>
                                                </HeaderTemplate>
                                                <ContentTemplate>
                                                    <asp:Panel ID="Panel13" runat="server" Width="100%" Height="100%">
                                                        <asp:Button ID="btnLocationRefresh" runat="server" Text="Refresh" Width="100%" />
                                                        <asp:GridView ID="grdLocationHistory" runat="server" AutoGenerateColumns="False"
                                                            DataKeyNames="ReceiveDetailIFSLocationLogID" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                                            AlternatingRowStyle-CssClass="alt">
                                                            <SelectedRowStyle CssClass="srowstyle" />
                                                            <Columns>
<%--                                                                <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:BoundField>--%>
                                                                <asp:BoundField DataField="IFSLocation" HeaderText="Location" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CreateDate" HeaderText="Date" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CreateUser" HeaderText="User" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                        </asp:GridView>

                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </asp:TabPanel>
                                            <asp:TabPanel runat="server" ID="TabPanel1" Enabled="true" Width="100%"
                                                Visible="True">
                                                <HeaderTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text="SKU"></asp:Label>
                                                </HeaderTemplate>
                                                <ContentTemplate>
                                                    <asp:Panel ID="Panel17" runat="server" Width="100%" Height="100%">
                                                        <asp:Button ID="btnSKURefresh" runat="server" Text="Refresh" Width="100%" />
                                                        <asp:GridView ID="grdSKUHistory" runat="server" AutoGenerateColumns="False"
                                                            DataKeyNames="ReceiveDetailSKUChangeLogID" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                                            AlternatingRowStyle-CssClass="alt">
                                                            <SelectedRowStyle CssClass="srowstyle" />
                                                            <Columns>
<%--                                                                <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:BoundField>--%>
                                                                <asp:BoundField DataField="SKU" HeaderText="SKU" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CreateDate" HeaderText="Date" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CreateUser" HeaderText="User" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                        </asp:GridView>

                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </asp:TabPanel>
                                            <asp:TabPanel runat="server" ID="TabPanel2" Enabled="true" Width="100%"
                                                Visible="True">
                                                <HeaderTemplate>
                                                    <asp:Label ID="Label4" runat="server" Text="Condition"></asp:Label>
                                                </HeaderTemplate>
                                                <ContentTemplate>
                                                    <asp:Panel ID="Panel18" runat="server" Width="100%" Height="100%">
                                                        <asp:Button ID="btnConditionRefresh" runat="server" Text="Refresh" Width="100%" />
                                                        <asp:GridView ID="grdConditionHistory" runat="server" AutoGenerateColumns="False"
                                                            DataKeyNames="ReceiveDetailConditionChangeLogID" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                                            AlternatingRowStyle-CssClass="alt">
                                                            <SelectedRowStyle CssClass="srowstyle" />
                                                            <Columns>
<%--                                                                <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:BoundField>--%>
                                                                <asp:BoundField DataField="IFS_Condition" HeaderText="Condition" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CreateDate" HeaderText="Date" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CreateUser" HeaderText="User" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                        </asp:GridView>

                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </asp:TabPanel>


                                            <asp:TabPanel runat="server" ID="tabBillingPoints" Enabled="true" HeaderText="Billing Points"
                                                Width="100%" Visible="False">
                                                <ContentTemplate>
                                                    <asp:Panel ID="Panel7" runat="server" Width="100%" Height="100%">
                                                        <asp:Button ID="btnBillingRefresh" runat="server" Text="Refresh" Width="100%" />
                                                        <asp:GridView ID="grdBillingPoints" runat="server" AutoGenerateColumns="False" DataKeyNames="ReceiveDetailID" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                        <SelectedRowStyle CssClass="srowstyle" />

                                                            <Columns>
                                                                <asp:BoundField DataField="ReceiveDetailID" HeaderText="ID" ReadOnly="True" Visible="true">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ProjectID" HeaderText="PjID" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ProcessID" HeaderText="PrID" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Process Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblProcessName" runat="server" Text="Process Name"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="RateValue" HeaderText="Rate/Value" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="PostedDate" HeaderText="Posted Date" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="PostedUser" HeaderText="Posted User" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CreateDate" HeaderText="Date" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CreateUser" HeaderText="User" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="V">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgDelete" runat="server" HeaderText="" ImageUrl="~/Images/PUSHPINB.bmp"
                                                                            ToolTip="Delete Billing Point"></asp:ImageButton>
                                                                        <asp:ImageButton ID="imgAddBillingPoint" runat="server" HeaderText="" ImageUrl="~/Images/ADD.bmp"
                                                                            ToolTip="Add Billing point"></asp:ImageButton>
                                                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtenderVersionUP" runat="server" TargetControlID="imgDelete"
                                                                            ConfirmText="Are you sure you want to Delete this billing point?">
                                                                        </asp:ConfirmButtonExtender>
                                                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender_Version" runat="server" TargetControlID="imgAddBillingPoint"
                                                                            ConfirmText="Are you sure you want to Add a Billing Point?">
                                                                        </asp:ConfirmButtonExtender>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </asp:TabPanel>
                                            <asp:TabPanel runat="server" ID="TabAuthorization" Enabled="true" Width="100%" Visible="True">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblAuthorizationTab" runat="server" Text="Authorization Log"></asp:Label>
                                                </HeaderTemplate>
                                                <ContentTemplate>
                                                    <asp:Panel ID="Panel10" runat="server">
                                                        <asp:Button ID="btnEditAuthorization" runat="server" Text="Edit" Visible="False" />
                                                        <asp:Button ID="btnAddAuthorization" runat="server" Text="Add" OnClientClick=" OpenAuthorization();return false;" />
                                                        <br />
                                                        <asp:Button ID="btnAuthorizeLogRefresh" runat="server" Text="Refresh" Width="100%" />
                                                        <div style="overflow: auto; height: auto; max-width: 800px">
                                                            <asp:GridView ID="grdAuthorizationLog" runat="server" AutoGenerateColumns="False"
                                                                DataKeyNames="ReceiveDetailAuthorizationLogID" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                        <SelectedRowStyle CssClass="srowstyle" />

                                                                <Columns>
                                                                    <asp:BoundField DataField="ReceiveDetailID" HeaderText="ID" ReadOnly="True" Visible="false"
                                                                        HeaderStyle-VerticalAlign="Top">
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="Status">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblStatus" runat="server" Text="Label"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="Note" HeaderText="Note" HeaderStyle-HorizontalAlign="Left"
                                                                        HeaderStyle-VerticalAlign="Top">
                                                                        <ItemStyle HorizontalAlign="left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="EstimateFee" HeaderText="Estimate" HeaderStyle-HorizontalAlign="Left"
                                                                        HeaderStyle-VerticalAlign="Top">
                                                                        <ItemStyle HorizontalAlign="left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="FreightFee" HeaderText="Freight" HeaderStyle-HorizontalAlign="Left"
                                                                        HeaderStyle-VerticalAlign="Top">
                                                                        <ItemStyle HorizontalAlign="left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="HST" HeaderText="HST" HeaderStyle-HorizontalAlign="Left"
                                                                        HeaderStyle-VerticalAlign="Top">
                                                                        <ItemStyle HorizontalAlign="left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Total" HeaderText="Total" HeaderStyle-HorizontalAlign="Left"
                                                                        HeaderStyle-VerticalAlign="Top">
                                                                        <ItemStyle HorizontalAlign="left" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="Received">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblReceived" runat="server" Text="Label"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Authorized">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblAuthorized" runat="server" Text="Label"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Declined">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDelcined" runat="server" Text="Label"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Rejected">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblRejected" runat="server" Text="Label"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Requested">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblRequested" runat="server" Text="Label"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkbtnRevive" runat="server">Revive</asp:LinkButton>
                                                                            <asp:LinkButton ID="lnkbtnExpire" runat="server">Expire</asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </asp:TabPanel>
                                        </asp:TabContainer>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </asp:TableCell>
                    <asp:TableCell VerticalAlign="Top" HorizontalAlign="Center" width="15%">
                        <asp:TextBox ID="txtHistoryCount" runat="server" Width="95%" ReadOnly="True" Text="0"></asp:TextBox><br />
                        <asp:Button ID="btnDelete" runat="server" Text="D " UseSubmitBehavior="False" ClientIDMode="Static" Width="100%"
                            OnClientClick='DeleteHistory(); return false;' ToolTip="Delete Selected History Item" class="button"/><br />
                        <asp:Button ID="btnRemoveAll" runat="server" Text="R" UseSubmitBehavior="False" ClientIDMode="Static" Width="100%"
                            OnClientClick='ResetHistory(); return false;' ToolTip="Reset History List" class="button" />

                        <br />
                        <asp:ListBox ID="lstHistory" runat="server" Height="100%" Width="100%" SelectionMode="Single"
                            ViewStateMode="Inherit" ClientIDMode="Static"></asp:ListBox>
                        <asp:Label ID="txtToolTip" runat="server" Text="" Width="95%" Style="text-align: center"
                            ></asp:Label>
                    </asp:TableCell>

                </asp:TableRow>
                <asp:TableRow>
                <asp:TableCell>
                </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="Display_ESN_B" runat="server" Width="19%" ClientIDMode="Static"
                            BackColor="#4DDDF9" Text="" Enabled="True" Style="text-align: left" ForeColor="Black"
                            Font-Bold="True" ReadOnly="True"></asp:TextBox>
                        <asp:TextBox ID="Display_MSG_B" runat="server" Width="45%" ClientIDMode="Static"
                            BackColor="#4DDDF9" Text="" Enabled="True" Style="text-align: center" ForeColor="Black"
                            Font-Bold="True" ReadOnly="True"></asp:TextBox>
                        <asp:TextBox ID="txtStatusPanel_B" runat="server" Width="30%" ClientIDMode="Static"
                            BackColor="#4DDDF9" Text="" Enabled="True" Style="text-align: right" ForeColor="Black"
                            Font-Bold="True" ReadOnly="True"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell>
                    </asp:TableCell>
                    <asp:TableCell VerticalAlign="Top" HorizontalAlign="Left" Width="70%">
                        <asp:Button ID="btnBagTag_b" runat="server" Text="Print Form ++" />
                        <asp:Button ID="btnSave_b" runat="server" Text="Save **" />
                    </asp:TableCell>
                    <asp:TableCell>
                    </asp:TableCell>
                </asp:TableRow>


            </asp:Table>



            <syncfusion:Window ID="wndUnitNote" runat="server" Height="260px"
            Width="410px" InitiallyShown="True"
            ClientObjectId="wndUnitNote" ResizeMode="FreeStyle" CssClass="syncpopup" ShowStatusBar="False" Title="Customer Notes">
            <table align="center" height="100%" width="100%">
                <tr>
                    <td id="Td27" valign="middle" align="center" runat="server">
                        <asp:Panel ID="pnlUnitNote" runat="server" Width="100%" Height="100%">
                        </asp:Panel>
                    </td>
                </tr>
                <tr id="Tr1" valign="middle" align="center" runat="server">
                <td>
                        <input type="Button" value="Close" style="font-family: Trebuchet MS; font-size: 12px;
                            width: 47px; height: 25px;" onclick="wndUnitNote.Close()" />
                </td>
                </tr>
            </table>
        </syncfusion:Window>





            <syncfusion:Window ID="wndAuthorize" Title="Authorization Request" 
                runat="server" CssClass="syncpopup">
                <asp:Panel ID="Panel11" runat="server" Width="100%" Height="100%" CssClass="syncpopuptext">
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                    <asp:HiddenField ID="HiddenField2" runat="server" />
                    <table id="Table7" runat="server" align="center" width="100%">
                        <tr>
                            <td id="Td16" runat="server" align="right">
                                Estimate Fee:
                            </td>
                            <td id="Td19" runat="server" align="left">
                                <asp:TextBox ID="txtEstimateFee" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td id="Td20" runat="server" align="right">
                                Freight Fee:
                            </td>
                            <td id="Td21" runat="server" align="left">
                                <asp:TextBox ID="txtFreightFee" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td id="Td22" runat="server" align="right">
                                HST:
                            </td>
                            <td id="Td23" runat="server" align="left">
                                <asp:TextBox ID="txtHST" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td id="Td24" runat="server" align="right">
                                Total:
                            </td>
                            <td id="Td25" runat="server" align="left">
                                <asp:TextBox ID="TxtTotal" runat="server"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td id="Td6" runat="server" align="left" colspan="2">
                                Note:
                            </td>
                        </tr>

                        <tr>
                            <td id="Td26" runat="server" align="left" colspan="2">
                                <asp:TextBox ID="txtAuthorizeNote" runat="server" Wrap="True" TextMode="MultiLine" Rows="10" MaxLength="250" Width="98%"></asp:TextBox>
                            </td>
                        </tr>


                        <tr id="Tr5" runat="server">
                            <td id="Td17" runat="server" align="center">
                                <asp:Button ID="btnSaveAuthorize" runat="server" Text="Save" OnClientClick="Authorization_Save();"  Width="80%"/>
                                <%--<asp:Button ID="Button1" runat="server" Text="Save" onclick="Authorization_Save();" />--%>
                            </td>
                            <td id="Td18" runat="server">
                                <asp:Button ID="btnCancelAuthorize" runat="server" Text="Cancel" OnClientClick="Authorization_Cancel();return false;" Width="80%"/>
                                <%--<asp:Button ID="Button2" runat="server" Text="Cancel" onclick="Authorization_Cancel();return false;" />--%>
                            </td>
                        </tr>

                    </table>
                </asp:Panel>
            </syncfusion:Window>
            <syncfusion:Window ID="wndSelectRepairReport" Title="Repair Report" 
                runat="server" CssClass="syncpopup">
                <asp:Panel ID="Panel15" runat="server" Width="100%" Height="100%" CssClass="syncpopuptext">
                    <asp:Button ID="btnRepairEstimate" runat="server" Text="Estimate" OnClientClick="OpenRepairForm('E');return false;"  Width="80%"/>
                    <asp:Button ID="btnRepairRepair" runat="server" Text="Repair"  OnClientClick="OpenRepairForm('R');return false;" Width="80%"/>
                    <asp:Button ID="btnRepairPacking" runat="server" Text="Packing Slip"  OnClientClick="OpenRepairForm('P');return false;" Width="80%"/>
                    <asp:Button ID="btnRepairCancel" runat="server" Text="Cancel"  OnClientClick="CloseSelectRepairReport();return false;" Width="80%"/>
                </asp:Panel>
            </syncfusion:Window>

            <syncfusion:Window ID="wndESNFound" Title="ESN/IMEI Number on file" 
                runat="server"  ScrollMode="None"  
                ShowIcon="False" CssClass="syncpopup_warning" ShowStatusBar="False" > 
                <asp:Panel ID="Panel1x" runat="server" Height="100%" Width="100%" CssClass="syncpopuptext">
                    <asp:HiddenField ID="hdnESNID" runat="server" />
                    <asp:HiddenField ID="hdnESNNumber" runat="server" />
                    <br />
                    <asp:Label ID="lblESNFoundText" runat="server" Text="ESN/IMEI Already on file!"></asp:Label>
                    <br />
                    <br />
<%--                    <input type="button" value="Record Exists! Refer to Traking and Inspection?" onclick="AlreadyFound_AddNew_Cancel();return false;"
                        class="button" />--%>

                    <asp:Button ID="btnAlreadyFound" runat="server" Text="Record Exists! Refer to Traking and Inspection?" Width="80%"
                        OnClientClick="AlreadyFound_AddNew_Cancel();return false;" CssClass="button" />
                    <asp:Button ID="ClientTransfer" runat="server" Text="Client Submitted Transfer?" Width="80%"
                        OnClientClick="AlreadyFound_TransferIN_OK();return false;" CssClass="button" />
                    <asp:Button ID="TransferToMSC" runat="server" Text="Transfer to MSC?" Width="80%"
                        OnClientClick="AlreadyFound_TransferToMSC_OK();return false;" CssClass="button" />

                    <asp:Button ID="btnKittingDefective" runat="server" Text="Defective Unit?" Width="80%"
                        OnClientClick="KittingDefectiveCancel();return false;" CssClass="button" />


<%--                    <asp:Button ID="TranserFromMSC" runat="server" Text="This Unit is back from MSC?" Width="80%"
                        OnClientClick="AlreadyFound_TransferFromMSC_OK();return false;" CssClass="button" />--%>
                </asp:Panel>
            </syncfusion:Window>

            <syncfusion:Window ID="wndESNFoundClient" Title="ESN/IMEI Number already on file" 
                runat="server"  ScrollMode="None"  
                ShowIcon="False" CssClass="syncpopup_warning" > 
                <asp:Panel ID="Panel16" runat="server" Height="100%" Width="100%" CssClass="syncpopuptext">
                    <br />
                    <asp:Label ID="Label3" runat="server" Text="ESN/IMEI Already on file!"></asp:Label>
                    <br />
                    <br />
                    <input type="button" value="ESN/IMEI Already on file?" onclick="MCL('wndESNFoundClient').Close();;return false;"
                        class="button" />
                </asp:Panel>
            </syncfusion:Window>

            <syncfusion:Window ID="wndSendEmailWindow" Title="Send Email" 
                runat="server"  ScrollMode="None"  
                ShowIcon="False" CssClass="syncpopup_warning" >
                <asp:Panel ID="Panel5" runat="server" Height="100%" Width="100%" CssClass="syncpopuptext">
                    <br />
                    <br />
                    <br />
                    <asp:Panel ID="EmailSend" runat="server">
                        <a href="mailto:xxxxxxxxx@BM.ca?subject=Comments about the color blue&body=The following is everything I have to say about the color blue. %0A%0A Second paragraph. %0A%0A Third Paragraph etc.">
                            Send</a>
                    </asp:Panel>
                    <br />
                </asp:Panel>

            </syncfusion:Window>
            <syncfusion:Window ID="wndSelectProcess" Title="Select Process" runat="server" 
                CssClass="syncpopup">
                <asp:Panel ID="Panel6" runat="server" Width="100%" Height="100%" CssClass="syncpopuptext">
                    <asp:HiddenField ID="hdnSelectedLogID" runat="server" />
                    <table id="Table4" runat="server" align="center" width="100%">
                        <tr>
                            <td id="Td2" runat="server" align="center" colspan="2">
                                Process:
                                <asp:DropDownList ID="drpProcessList" runat="server">
                                </asp:DropDownList>
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr id="Tr2" runat="server">
                            <td id="Td3" runat="server" align="center">
                                <input type="button" value="Continue with change?" onclick="SelectProcess_OK()"  Width="80%"/>
                            </td>
                            <td id="Td4" runat="server">
                                <input type="button" value="Cancel" onclick="SelectProcess_Cancel()"  Width="80%"/>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </syncfusion:Window>
            <syncfusion:Window ID="wndSwitchIMEI" Title="Switch IMEI" runat="server"
                CssClass="syncpopup">
                <asp:Panel ID="Panel12" runat="server" Width="100%" Height="100%">
                    <table id="Table2" runat="server" align="center" width="100%">
                        <tr>
                            <td colspan="2" align="center">
                            <br />
                                <h1>Switch IMEI</h1>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                            <br />
                                New IMEI Number:
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtNewIMEI" runat="server"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td align="right">
                                <asp:Button ID="btnSwitchIMEIOK" runat="server" Text="Switch" OnClientClick="SwitchIMEIOK();return false;" Width="80%" />
                            </td>
                            <td align="left">
                                <asp:Button ID="btnSwitchIMEICancel" runat="server" Text="Cancel" OnClientClick="SwitchIMEICancel();return false;" Width="80%" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </syncfusion:Window>

            <syncfusion:Window ID="wndSelectClientLocation" Title="Select Client Location" runat="server"
                CssClass="syncpopup">
                <asp:Panel ID="Panel9" runat="server" Width="100%" Height="100%">
                    <table id="Table6" runat="server" align="center" width="100%">
                        <tr>
                            <td colspan="2" align="center">
                            <br />
                                <h1>Search</h1>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            <br />
                                Client Name:
                            </td>
                            <td>
                                <asp:TextBox ID="txtsClientName" runat="server"></asp:TextBox>
                            </td>
                        </tr>


                        <tr>
                            <td>
                                Location Name:
                            </td>
                            <td>
                                <asp:TextBox ID="txtsLocationName" runat="server"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                Street:
                            </td>
                            <td>
                                <asp:TextBox ID="txtsStreet" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Postal Code:
                            </td>
                            <td>
                                <asp:TextBox ID="txtsPostalCode" runat="server"></asp:TextBox>
                            </td>
                        </tr>



                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClientClick="SearchClient();return false;" Width="80%"/>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="2">
                            <br />
                                <asp:Panel ID="pnlSearchResult" runat="server" Width="100%" Height="100%" ScrollBars="Auto">
                                    <table id="XX">
                                        <tr>
                                            <td>
                                                Select
                                            </td>
                                            <td>
                                                Client
                                            </td>
                                            <td>
                                                Location
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>

                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </syncfusion:Window>
            <syncfusion:Window ID="wndPartReturnList" Title="Part Return List" runat="server" CssClass="syncpopup">
                <table id="Table12" runat="server" width="100%">
                    <tr>
                        <td id="Td28" runat="server" valign="top">
                            <asp:Panel ID="pnlPartReturnList" runat="server" Width="100%" Height="100%">
                            </asp:Panel>
                        </td>
                        <td id="Td29" runat="server" valign="top">
                            <asp:Panel ID="pnlPickedReturnList" runat="server" Width="100%" Height="100%">
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td id="Td30" runat="server" colspan="2">
                            <asp:Button ID="btnPickReturnSave" runat="server" Text="Save" OnClientClick="PickReturnSave(); return false;" />
                            <asp:Button ID="btnPickReturnCancel" runat="server" Text="Cancel"  OnClientClick="PickReturnCancel(); return false;" />
                        </td>
                    </tr>
                </table>


            </syncfusion:Window>

            <syncfusion:Window ID="wndIMEIBulk" Title="Bulk Process IMEI numbers" 
                runat="server" CssClass="syncpopup">
                <asp:Panel ID="Panel8" runat="server" Width="100%" Height="100%" CssClass="syncpopuptext">


                    <table id="Table5" runat="server" align="center" width="100%">
                        <tr>
                            <td id="Td13" runat="server" align="center" colspan="2">
                                <br />
                                <asp:TextBox ID="txtOrderNumber" runat="server" Height="100%" Width="99%"></asp:TextBox>
                                <br />
                            </td>
                            <td id="Td14" runat="server" align="center" colspan="1">
                                <br />
                                <input type="button" value="Load IMEI List from Order Entry Number" onclick="LoadOrderEntryIMEIList_OK();return false;"  Width="80%"/>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td id="Td15" runat="server" align="center" colspan="3">
                                <h1>
                                <asp:Label ID="lblIMEIStatus" runat="server" Text="Status:" Height="100%"></asp:Label>
                                </h1>
                            </td>
                        </tr>
                        <tr>
                            <td id="Td8" runat="server" align="center" colspan="3">
                                <br />
                                IMEI List:
                                <br />
                                <asp:TextBox ID="txtIMEIList" runat="server" Height="100%" Width="99%" TextMode="MultiLine" Rows="20"></asp:TextBox>
                                <br />
                                <br />
                            </td>
                        </tr>





                        <tr>
                            <td id="Td12" runat="server" align="center" colspan="3">
                                <asp:Label ID="lblIMEICount" runat="server" Text="Count:" Height="100%"></asp:Label>
                                <br />
                                <br />
                            </td>
                        </tr>


                        <tr id="Tr4" runat="server">
                            <td id="Td11" runat="server" align="center">
                                <input type="button" value="Count?" onclick="IMEIBulk_Count();return false;" />
                            </td>

                            <td id="Td9" runat="server" align="center">
                                <input type="button" value="Process IMEI List?" onclick="IMEIBulk_OK();return false;" />
                            </td>

                            <td id="Td10" runat="server">
                                <input type="button" value="Cancel" onclick="IMEIBulk_Cancel();return false;" />
                            </td>
                        </tr>
                    </table>





                </asp:Panel>
            </syncfusion:Window>

            <syncfusion:Window ID="wndPartList" Title="Part List" runat="server" CssClass="syncpopup">
                <table id="Table11" runat="server" width="100%">
                    <tr>
                        <td id="Td1" runat="server" valign="top">
                            <asp:Panel ID="pnlPartList" runat="server" Width="100%" Height="100%">
                            </asp:Panel>
                        </td>
                        <td id="Td5" runat="server" valign="top">
                            <asp:Panel ID="pnlPickedList" runat="server" Width="100%" Height="100%">
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td id="Td7" runat="server" colspan="2">
                            <asp:Button ID="btnPickSave" runat="server" Text="Save" OnClientClick="PickSave(); return false;" />
                            <asp:Button ID="btnPickCancel" runat="server" Text="Cancel"  OnClientClick="PickCancel(); return false;" />
                        </td>
                    </tr>
                </table>
            </syncfusion:Window>

            <syncfusion:Window ID="wndPurchaseOrderList" Title="Purchase Order List" runat="server" CssClass="syncpopup">
                <table id="Table13" runat="server" width="100%">
                    <tr>
                        <td id="Td31" runat="server" valign="top">
                            <asp:Panel ID="pnlPOList" runat="server" Width="100%" Height="100%">
                            </asp:Panel>
                        </td>
<%--                        <td id="Td32" runat="server" valign="top">
                            <asp:Panel ID="pnlPOPickedList" runat="server" Width="100%" Height="100%">
                            </asp:Panel>
                        </td>--%>
                    </tr>
                    <tr>
                        <td id="Td33" runat="server">
                            <%--<asp:Button ID="btnPOPickSave" runat="server" Text="Save" OnClientClick="PickPOSave(); return false;" />--%>
                            <asp:Button ID="btnPOPickCancel" runat="server" Text="Cancel"  OnClientClick="PickPOCancel(); return false;" />
                        </td>
                    </tr>
                </table>
            </syncfusion:Window>


        </ContentTemplate>
    </asp:UpdatePanel>




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
               //                ConfigureWaitingPopup(Popup);
               $('#loading').show();
           }
       }
       function EndRequestHandler(sender, args) {

           $('#loading').hide();
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

               case "WNDESNFOUND": return $find('<%=this.wndESNFound.ClientID%>'); break;
               case "WNDESNFOUNDCLIENT": return $find('<%=this.wndESNFoundClient.ClientID%>'); break;
               case "WNDESNFOUNDCLIENT": return $find('<%=this.wndESNFoundClient.ClientID%>'); break;



               case "WNDIMEIBULK": return $find('<%=this.wndIMEIBulk.ClientID%>'); break;
               case "WNDSELECTPROCESS": return $find('<%=this.wndSelectProcess.ClientID%>'); break;
               case "WNDSENDEMAILWINDOW": return $find('<%=this.wndSendEmailWindow.ClientID%>'); break;
               case "WNDPARTLIST": return $find('<%=this.wndPartList.ClientID%>'); break;
               case "WNDPURCHASEORDERLIST": return $find('<%=this.wndPurchaseOrderList.ClientID%>'); break;



               case "WNDPARTRETURNLIST": return $find('<%=this.wndPartReturnList.ClientID%>'); break;
               case "WNDSELECTCLIENTLOCATION": return $find('<%=this.wndSelectClientLocation.ClientID%>'); break;
               case "WNDAUTHORIZE": return $find('<%=this.wndAuthorize.ClientID%>'); break;
               case "WNDSWITCHIMEI": return $find('<%=this.wndSwitchIMEI.ClientID%>'); break;

               case "WNDSELECTREPAIRREPORT": return $find('<%=this.wndSelectRepairReport.ClientID%>'); break;
               case "WNDUNITNOTE": return $find('<%=this.wndUnitNote.ClientID%>'); break;
               case "PNLUNITNOTE": return $get("<%= pnlUnitNote.ClientID %>"); break;


               //               case "PNLPOPICKEDLIST": return $get("< %= pnlPOPickedList.ClientID %>"); break; 
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


               case "HDNCLIENTIDX": return $get("<%= hdnClientIDx.ClientID %>"); break;
               case "HDNCARRIERIDX": return $get("<%= hdnCarrierIDx.ClientID %>"); break;
               case "HDNMANUFACTURERIDX": return $get("<%= hdnManufacturerIDx.ClientID %>"); break;
               case "HDNMODELIDX": return $get("<%= hdnModelIDx.ClientID %>"); break;

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
               case "RMAROW": return $get("<%= lblRMA_Row.ClientID %>"); break;
               case "WAITINGFORRMAROW": return $get("<%= tr_WaitingForRMA.ClientID %>"); break;
               case "WAITINGFORRMACHECK": return $get("<%= imgWaitingForRMACheck.ClientID %>"); break;
               case "WAITINGFORRMAX": return $get("<%= imgWaitingForRMAX.ClientID %>"); break;
               case "WAITINGFORRMATEXT": return $get("<%= imgWaitingForRMAX.ClientID %>"); break;


               case "PTAG": return $get("<%= txtProjectTag.ClientID %>"); break;
               case "PTAGROW": return $get("<%= lblPTag_Row.ClientID %>"); break;
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
  
</asp:Content>





