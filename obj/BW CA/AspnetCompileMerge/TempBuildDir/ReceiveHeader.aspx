<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReceiveHeader.aspx.cs" Inherits="BW_WebApp.ReceiveHeaderForm" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server">
	<link href="../Javascripts/jquery-ui-1.8.1.custom.css" rel="stylesheet" type="text/css" />
	<script src="../Javascripts/jquery-1.4.1-vsdoc.js" type="text/javascript"></script>
	<script src="../Javascripts/jquery-ui-1.8.1.custom.min.js" type="text/javascript"></script>

    <%--<script src="../Javascripts/jquery.timeentry.js" type="text/javascript"></script>--%>
	<%--<script src="../Javascripts/jquery.dataTables1_7.min.js" type="text/javascript"></script>--%>
    
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/select2.min.js"></script>

<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="cMC" runat="server">
<%--    <asp:ScriptManagerProxy ID="ScriptManager_Proxy" runat="server">
    </asp:ScriptManagerProxy>
--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdnClientLocationID" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnClientLocationIDNew" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnReceiveHeaderID" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnReceiveDetailBulkID" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnReceiveDetailID" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnLastESN" runat="server" ClientIDMode="Static" />

            <asp:HiddenField ID="hdnUserName" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnStepUp" runat="server" ClientIDMode="Static" />

            <h>Header Maintenance Screen</h>
            <br />
            <br />
            <asp:Button ID="btnBagTag" runat="server" Text="BagTag ++" />
            <asp:Button ID="btnClearData" runat="server" Text="Clear --" />
            <asp:Button ID="btnSave" runat="server" Text="Save **" />
            <asp:Panel ID="pnlmainentry" runat="server" Width="100%" Height="100%" HorizontalAlign="Left">
                <asp:Panel ID="Panel3" runat="server" HorizontalAlign="Left" Width="99%">
                    <asp:HiddenField ID="HdnProjSetup" runat="server" />
                    Scankey:
                    <asp:TextBox ID="ScanKey" runat="server" Width="25%" ClientIDMode="Static" TabIndex="1" BackColor="#4DDDF9"
                        ></asp:TextBox>
                    <asp:Label ID="lblMakeModelTitle" runat="server" Text="" Font-Size="Large"></asp:Label>
                    <asp:Table ID="Table2" runat="server" Height="100%" Width="100%">
                        <asp:TableRow>
                            <asp:TableCell VerticalAlign="Top">
                            ESN/IMEI Number:
                            </asp:TableCell>
                            <asp:TableCell VerticalAlign="Top">
                                <asp:TextBox ID="txtESN" runat="server" ClientIDMode="Static" TabIndex="3" Text=""
                                    ToolTip="Original ESN/IMEI Number" Enabled="False"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell VerticalAlign="Top">
                                <asp:TextBox ID="txtESNNew" runat="server" ClientIDMode="Static" TabIndex="3" Text=""
                                    ToolTip="ESN/IMEI Number"></asp:TextBox>
                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtESNNew"
                                    WatermarkText="ESN/IMEI Number">
                                </asp:TextBoxWatermarkExtender>
                                Status:
                                <asp:DropDownList ID="drpStatus" runat="server" ToolTip="Status">
                                </asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableCell VerticalAlign="Top" >
                            Project:
                            </asp:TableCell>
                            <asp:TableCell VerticalAlign="Top">
                                <asp:TextBox ID="txtProject" runat="server" Width="75%" ClientIDMode="Static" TabIndex="3"
                                    Text="" ToolTip="Original Project" Enabled="False"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell VerticalAlign="Top">
                                <asp:DropDownList ID="drpProjectListNew" runat="server" ToolTip="Project" Width="75%">
                                </asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow ID="RMARow">
                            <asp:TableCell VerticalAlign="Top">
                            RMA Number:
                            </asp:TableCell>
                            <asp:TableCell VerticalAlign="Top">
                                <asp:TextBox ID="txtRMA" runat="server" Width="98%" ClientIDMode="Static" TabIndex="2"
                                    Text="" ToolTip="Original RMA Number" Enabled="False"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell VerticalAlign="Top">
                                <asp:TextBox ID="txtRMANew" runat="server" Width="98%" ClientIDMode="Static" TabIndex="2"
                                    Text="" ToolTip="RMA Number"></asp:TextBox>
                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender9" runat="server" TargetControlID="txtRMANew"
                                    WatermarkText="RMA Number">
                                </asp:TextBoxWatermarkExtender>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="ProjectTagRow">
                            <asp:TableCell VerticalAlign="Top">
                            Project Tag:
                            </asp:TableCell>
                            <asp:TableCell VerticalAlign="Top">
                                <asp:TextBox ID="txtProjectTag" runat="server" Width="98%" ClientIDMode="Static"
                                    TabIndex="2" Text="" ToolTip="Original Project Tag" Enabled="False"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell VerticalAlign="Top">
                                <asp:TextBox ID="txtProjectTagNew" runat="server" Width="98%" ClientIDMode="Static"
                                    TabIndex="2" Text="" ToolTip="Project Tag"></asp:TextBox>
                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtProjectTagNew"
                                    WatermarkText="Project Tag">
                                </asp:TextBoxWatermarkExtender>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>


                    <asp:Table ID="Table4" runat="server" Height="100%" Width="100%">
                        <asp:TableRow>
                            <asp:TableCell VerticalAlign="Top" Width="45%">
                                <asp:Table ID="Table3" runat="server" Height="100%" Width="100%">
                                    <asp:TableRow>
                                        <asp:TableCell VerticalAlign="Top" Width="45%" ColumnSpan="2">
                                            <asp:TextBox ID="txtClientName" runat="server" Width="58%" ClientIDMode="Static"
                                                TabIndex="1" Text="" ToolTip="Original Store Name" Enabled="False">
                                            </asp:TextBox>
                                            <asp:TextBox ID="txtStoreNumber" runat="server" Width="19%" ClientIDMode="Static"
                                                Text="" ToolTip="Original Store Number" Enabled="False">
                                            </asp:TextBox>
                                            <asp:TextBox ID="txtStoreSuffix" runat="server" Width="18%" ClientIDMode="Static"
                                                Text="" ToolTip="Original Store Suffix" Enabled="False">
                                            </asp:TextBox>
                                            <asp:TextBox ID="txtClientAddress" runat="server" Width="98.5%" Height="100%" ClientIDMode="Static"
                                                Text="" TextMode="MultiLine" Enabled="False" ToolTip="Original Location Address" Rows="4" >
                                            </asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </asp:TableCell>
                            <asp:TableCell VerticalAlign="Top" Width="45%">
                                <asp:Table ID="Table1" runat="server" Height="100%" Width="100%">
                                    <asp:TableRow>
                                        <asp:TableCell VerticalAlign="Top" Width="45%" ColumnSpan="2">
                                            <asp:TextBox ID="txtClientNameNew" runat="server" Width="58%" ClientIDMode="Static"
                                                TabIndex="1" Text="" ToolTip="Store Name" Enabled="False">
                                            </asp:TextBox>
                                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtClientNameNew"
                                                WatermarkText="Client Name">
                                            </asp:TextBoxWatermarkExtender>
                                            <asp:TextBox ID="txtStoreNumberNew" runat="server" Width="19%" ClientIDMode="Static"
                                                Text="" ToolTip="Store Number" Enabled="False">
                                            </asp:TextBox>
                                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtStoreNumberNew"
                                                WatermarkText="Store Number">
                                            </asp:TextBoxWatermarkExtender>
                                            <asp:TextBox ID="txtStoreSuffixNew" runat="server" Width="18%" ClientIDMode="Static"
                                                Text="" ToolTip="Store Suffix" Enabled="False">
                                            </asp:TextBox>
                                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtStoreSuffixNew"
                                                WatermarkText="Store Suffix">
                                            </asp:TextBoxWatermarkExtender>
                                            <asp:TextBox ID="txtClientAddressNew" runat="server" Width="98.5%" Height="100%"
                                                ClientIDMode="Static" Text="" TextMode="MultiLine" Enabled="False" ToolTip="Location Address"
                                                Rows="4">
                                            </asp:TextBox>
                                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" TargetControlID="txtClientAddressNew"
                                                WatermarkText="Client Address">
                                            </asp:TextBoxWatermarkExtender>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>



                    <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center" Width="100%">
                        <asp:CheckBoxList ID="chkProcessCheckList" CellPadding="5" CellSpacing="5" RepeatLayout="Flow"
                            TextAlign="Right" runat="server" RepeatDirection="Horizontal" RepeatColumns="10">
                        </asp:CheckBoxList>
                    </asp:Panel>
                </asp:Panel>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">

        function SetUpScreen() {

            var ProjectSetup = $get('<%= HdnProjSetup.ClientID %>').value;
            var RMA = $get('<%= txtRMA.ClientID %>');
            var pTag = $get('<%= txtProjectTag.ClientID %>');
            var esn = $get('<%= txtESN.ClientID %>');

            var RMANew = $get('<%= txtRMANew.ClientID %>');
            var pTagNew = $get('<%= txtProjectTagNew.ClientID %>');
            var esnNew = $get('<%= txtESNNew.ClientID %>');

            var rowRMA = $get('<%= RMARow.ClientID %>');
            var rowProjectTag = $get('<%= ProjectTagRow.ClientID %>');

            rowRMA.style.visibility = "hidden";
            rowProjectTag.style.visibility = "hidden";
            esn.style.visibility = "visible";
            esnNew.style.visibility = "visible";

            if (ProjectSetup.indexOf("ZRMAZ") > -1) {
                rowRMA.style.visibility = "visible";
            }

            if (ProjectSetup.indexOf("ZPTAGZ") > -1) {
                rowProjectTag.style.visibility = "visible";
            }
        }



        function BlankData() {

            $get('<%= hdnClientLocationID.ClientID %>').value = "-1";
            $get("<%= hdnReceiveHeaderID.ClientID %>").value = "-1";
            $get("<%= hdnReceiveDetailBulkID.ClientID %>").value = "-1";
            $get("<%= hdnReceiveDetailID.ClientID %>").value = "-1";
            $get('<%= txtRMA.ClientID %>').value = "";
            $get('<%= txtProjectTag.ClientID %>').value = "";
            $get('<%= txtESN.ClientID %>').value = "";
            $get('<%= txtClientName.ClientID %>').value = "";
            $get('<%= txtStoreNumber.ClientID %>').value = "";
            $get('<%= txtStoreSuffix.ClientID %>').value = "";
            $get('<%= txtClientAddress.ClientID %>').value = "";
            $get('<%= txtRMANew.ClientID %>').value = "";
            $get('<%= txtProjectTagNew.ClientID %>').value = "";
            $get('<%= txtESNNew.ClientID %>').value = "";
            $get('<%= txtClientNameNew.ClientID %>').value = "";
            $get('<%= txtStoreNumberNew.ClientID %>').value = "";
            $get('<%= txtStoreSuffixNew.ClientID %>').value = "";
            $get('<%= txtClientAddressNew.ClientID %>').value = "";
            setDropDownList($get("<%= drpProjectListNew.ClientID %>"), 0);
            setDropDownList($get("<%= drpStatus.ClientID %>"), 0);

        }

        function ClearData() {
            //            $get("= txtProjectNew.ClientID %>").value = $get("= txtProject.ClientID %>").value;
            $get('<%= hdnClientLocationIDNew.ClientID %>').value = $get('<%= hdnClientLocationID.ClientID %>').value;
            $get('<%= txtProjectTagNew.ClientID %>').value = $get('<%= txtProjectTag.ClientID %>').value;
            $get('<%= txtRMANew.ClientID %>').value = $get('<%= txtRMA.ClientID %>').value;
            $get('<%= txtESNNew.ClientID %>').value = $get('<%= txtESN.ClientID %>').value;
            $get('<%= txtClientNameNew.ClientID %>').value = $get('<%= txtClientName.ClientID %>').value;
            $get('<%= txtStoreNumberNew.ClientID %>').value = $get('<%= txtStoreNumber.ClientID %>').value;
            $get('<%= txtStoreSuffixNew.ClientID %>').value = $get('<%= txtStoreSuffix.ClientID %>').value;
            $get('<%= txtClientAddressNew.ClientID %>').value = $get('<%= txtClientAddress.ClientID %>').value;
            setDropDownListText($get("<%= drpProjectListNew.ClientID %>"), $get('<%= txtProject.ClientID %>').value);
        }

        // SCANKEY PROCESSING GOES HERE ----------------------------------------<
        //        function RecordScanKey() {
        //            var newText = $get("<%= ScanKey.ClientID %>").value;
        //            $get("<%= ScanKey.ClientID %>").value = "";
        //            if (newText.length > 0) {
        //                //                if (newText == "++") { $get("<%= btnSave.ClientID %>").click(); return; }  // shortcut to the Save Button.
        //                if (newText == "DOSAVE") { DoSave(); return; }  // shortcut to the Save Button.
        //                if (newText == "**") { DoSave(); return; }  // shortcut to the Save Button.
        //                if (newText == "DOCLEAR") { ClearData(); return; }  // shortcut to the Save Button.
        //                if (newText == "--") { ClearData(); return; }  // shortcut to the Save Button.
        //                if (newText == "++") { GenerateBagTag(); return; }  // shortcut to the Save Button.
        //                if (newText.toUpperCase() == "BAGTAG") { GenerateBagTag(); return; }
        //                //                if (newText == "+1") { LoadData(); return; }  // shortcut for the Load button.
        //                var ClientLocationID = $get("<%= hdnClientLocationID.ClientID %>").value;
        //                var ItemScanCode = "XXX";
        //                var UserName = $get("<%= hdnUserName.ClientID %>").value;
        //                var StepUp = $get("<%= hdnStepUp.ClientID %>").value;

        //                var service = new WebServer_01();
        //                service.ScanCodeParse(ClientLocationID, ItemScanCode, "Receive", newText, UserName, StepUp, onSuccess, null, null);
        //            }
        //        }

        //        function ScanFocus() {
        //            var SCAN = $get('<%= ScanKey.ClientID %>');
        //            SCAN.focus();
        //            return;
        //        }

        //        ////////////////////////////////////////

        //        function onSuccess(result) {
        //            var B = result.split(":")
        ////            if (B[0] == "ClientLocation") { LoadClientLocation(B[1]); return; }
        //            if (B[0] == "ReceiveDetail") { LoadSheetDataDetail(B[1]); return; }
        ////            if (B[2] == "Unknown Scancode") { LoadScanNumber(B[3]); return; }
        //            // AddItem("", result);
        //            //UpdateFormScanData(B);
        //            BlankData();
        //            ScanFocus();
        //            return;
        //        }

        function LoadClientLocation(ID) {
            var service = new WebServer_01();
            var rValue = service.GetClientLocationData(ID, $get("<%= hdnUserName.ClientID %>").value, onClientLoadSuccess);
        }

        function onClientLoadSuccess(Result) {
            ClientData = eval('({' + Result + '})');
            if (ClientData != null) {
                document.getElementById("<%= txtClientNameNew.ClientID %>").value = ClientData.txtClientName
                $get("<%= hdnClientLocationIDNew.ClientID %>").value = ClientData.ClientLocationID;
                // $get("<%= txtClientName.ClientID %>").value = ClientData.txtClientName;
                $get("<%= txtStoreNumberNew.ClientID %>").value = ClientData.txtStoreNumber;
                $get("<%= txtStoreSuffixNew.ClientID %>").value = ClientData.txtStoreSuffix;
                $get("<%= txtClientAddressNew.ClientID %>").value = ClientData.txtClientAddress;
            }
        }
        /////////

        function DoSave() {
            DataList = GetParameterList();
            var ds = GetDataStream(DataList);
            var service = new WebServer_01();
            var rValue = service.UpdateDataDetailHeader(ds, onAddSaveSuccess);
        }

        function onAddSaveSuccess(result) {
            result = '({' + result + '})';
            var resultList = eval(result);
            if (resultList.Result == "Saved") {
                alert("Saved");
                LoadSheetDataDetail(resultList.ReceiveDetailID);
                return;
                //                var xDatalist = GetReceiveIDKeys();
                //                xDatalist.ReceiveHeaderID = resultList.ReceiveHeaderID;
                //                xDatalist.ReceiveDetailBulkID = resultList.ReceiveDetailBulkID;
                //                xDatalist.ReceiveDetailID = resultList.ReceiveDetailID;
                //                SetReceiveIDKeys(xDatalist);
                //                $get('<%= lblMakeModelTitle.ClientID %>').innerHTML = resultList.MMS;
            }
            //            $get("<%= hdnLastESN.ClientID %>").value = $get("<%= txtESN.ClientID %>").value;
            // remove the qty to allow it to be filled again.
            //            $get("<%= txtESN.ClientID %>").value = "";
            ScanFocus();
        }




        //////////////
        function LoadSheetDataDetail(ID) {
            var service = new WebServer_01();
            var rValue = service.GetDetailSheetData(ID, onDetailLoadSuccess);
        }

        function onDetailLoadSuccess(Data) {
            ClearData();
            xDataList = eval('({' + Data + '})');

            $get('<%= lblMakeModelTitle.ClientID %>').innerHTML = xDataList["MMS"];

            $get("<%= txtProject.ClientID %>").value = xDataList['Project'];
            $get('<%= hdnClientLocationID.ClientID %>').value = xDataList["CLID"];
            $get("<%= hdnReceiveHeaderID.ClientID %>").value = xDataList["RHID"];
            $get("<%= hdnReceiveDetailBulkID.ClientID %>").value = xDataList["RDBID"];
            $get("<%= hdnReceiveDetailID.ClientID %>").value = xDataList["RDID"];
            $get('<%= txtProjectTag.ClientID %>').value = xDataList["PROJTAG"];
            $get('<%= txtRMA.ClientID %>').value = xDataList["RMA"];
            $get('<%= txtESN.ClientID %>').value = xDataList["ESN"];
            $get('<%= txtClientName.ClientID %>').value = xDataList["CNAME"];
            $get('<%= txtStoreNumber.ClientID %>').value = xDataList["CNUM"];
            $get('<%= txtStoreSuffix.ClientID %>').value = xDataList["CSUF"];
            $get('<%= txtClientAddress.ClientID %>').value = xDataList["CADD"];

            //            $get("= txtProjectNew.ClientID %>").value = xDataList['Project'];

            setDropDownList($get("<%= drpProjectListNew.ClientID %>"), xDataList['ProjectID']);
            setDropDownList($get("<%= drpStatus.ClientID %>"), xDataList['StatusID']);

            $get('<%= hdnClientLocationIDNew.ClientID %>').value = xDataList["CLID"];
            $get('<%= txtProjectTagNew.ClientID %>').value = xDataList["PROJTAG"];
            $get('<%= txtRMANew.ClientID %>').value = xDataList["RMA"];
            $get('<%= txtESNNew.ClientID %>').value = xDataList["ESN"];
            $get('<%= txtClientNameNew.ClientID %>').value = xDataList["CNAME"];
            $get('<%= txtStoreNumberNew.ClientID %>').value = xDataList["CNUM"];
            $get('<%= txtStoreSuffixNew.ClientID %>').value = xDataList["CSUF"];
            $get('<%= txtClientAddressNew.ClientID %>').value = xDataList["CADD"];
            $get('<%= HdnProjSetup.ClientID %>').value = xDataList["SUFD"];
            SetUpScreen();
            //UpdateProcessCheckList(xDataList["CompProcList"]);
            ScanFocus();
        }

        function UpdateProcessCheckList(IDList) {
            if (IDList.length > 0) {
                var ProjectIDList = IDList.split(",");
                if (ProjectIDList.length > 0) {
                    var inputArea = $get('<%= chkProcessCheckList.ClientID %>');
                    var inputs = inputArea.getElementsByTagName("input"); //or document.forms[0].elements;
                    for (var i = 0; i < inputs.length; i++) {
                        if (inputs[i].type == "checkbox") {
                            var cBox = inputs[i];
                            var p = cBox.parentNode;
                            var currentValue = p.getAttribute("someValue");
                            var x = 0;
                            //                                if (parseInt(currentValue) == B[1]) {
                            inputs[i].checked = false;
                            for (x = 0; x < ProjectIDList.length; x++) {
                                if (currentValue == ProjectIDList[x]) {
                                    inputs[i].checked = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }


        ///////////////




        function LoadScanNumber(ScanNumber) {
            var ProjectSetup = $get('<%= HdnProjSetup.ClientID %>').value;

            PROJTAG = $get('<%= txtProjectTagNew.ClientID %>').value;
            if (PROJTAG == "Project Tag") { PROJTAG = ""; }
            RMA = $get('<%= txtRMANew.ClientID %>').value;
            if (RMA == "RMA Number") { RMA = ""; }
            if (RMA == "Work Order Number") { RMA = ""; }

            ESN = $get('<%= txtESNNew.ClientID %>').value;
            if (ESN == "ESN/IMEI Number") { ESN = ""; }

            if (ProjectSetup.indexOf("ZRMAZ") > -1) {
                if (RMA.length == 0) {
                    $get('<%= txtRMANew.ClientID %>').value = ScanNumber;
                    return;
                }
            }
            if (ProjectSetup.indexOf("ZPTAGZ") > -1) {
                if (PROJTAG.length == 0) {
                    $get('<%= txtProjectTagNew.ClientID %>').value = ScanNumber;
                    return;
                }
            }

            if (ESN.length == 0) {
                $get('<%= txtESNNew.ClientID %>').value = ScanNumber;
                return;
            }
            return
        }



        ////////////////////////////////////////
        function GenerateBagTag() {
            var ParameterList = GetReportParameterList("Bagtag");
            if (ParameterList["ESN"].length == 0 && ParameterList["LESN"].length == 0) {
                alert("You need to set a ESN Number and advance first");
                ScanFocus();
                return;
            }
            if (IsNumeric(ParameterList["CLID"]) == false) {
                alert("You must enter a Client first!");
                ScanFocus();
                return;
            }
            OpenbagTag(ParameterList);
        }

        function OpenbagTag(ParameterList) {
            //            var win = window.open("ViewDoc.aspx", "_blank", "status=no,toolbar=no,menubar=no,location=no,titlebar=no,width=600px,height=540px", true);
            var pstring = GetParameterStream(ParameterList);
            var WindowToOpen = "BagTag.aspx";
            if (pstring.length > 0) {
                WindowToOpen = WindowToOpen + "?" + pstring
            }
            //            var win = window.open(WindowToOpen, "_blank", "width=100,height=50,menubar", true);
            var win = window.open(WindowToOpen, "_blank", "menubar", true);
            // win.focus();
        }

        function GetParameterList() {
            var xDataList = {};

            var IndexValue = $get('<%=drpProjectListNew.ClientID %>').selectedIndex;
            xDataList["Project"] = $get('<%=drpProjectListNew.ClientID %>').options[IndexValue].text;
            xDataList["ProjectID"] = $get('<%=drpProjectListNew.ClientID %>').options[IndexValue].value;

            IndexValue = $get('<%=drpStatus.ClientID %>').selectedIndex;
            xDataList["Status"] = $get('<%=drpStatus.ClientID %>').options[IndexValue].text;
            xDataList["StatusID"] = $get('<%=drpStatus.ClientID %>').options[IndexValue].value;


            xDataList["ClientLocationID"] = $get('<%= hdnClientLocationID.ClientID %>').value;
            xDataList["ReceiveHeaderID"] = $get("hdnReceiveHeaderID").value;
            xDataList["ReceiveDetailBulkID"] = $get("hdnReceiveDetailBulkID").value;
            xDataList["ReceiveDetailID"] = $get("hdnReceiveDetailID").value;
            xDataList["CurUserName"] = $get("hdnUserName").value;
            xDataList["CurStepUp"] = $get("hdnStepUp").value;

            xDataList["PROJTAG"] = $get('<%= txtProjectTagNew.ClientID %>').value;
            xDataList["RMA"] = $get('<%= txtRMANew.ClientID %>').value;
            xDataList["ESN"] = $get('<%= txtESNNew.ClientID %>').value;

            // We need to remove the watermark
            if ($get('<%= txtProjectTagNew.ClientID %>').value == "Project Tag") { xDataList["PROJTAG"] = ""; }
            if ($get('<%= txtRMANew.ClientID %>').value == "RMA Number") { xDataList["RMA"] = ""; }
            if ($get('<%= txtRMANew.ClientID %>').value == "Work Order Number") { xDataList["RMA"] = ""; }


            if ($get('<%= txtESNNew.ClientID %>').value == "ESN/IMEI Number") { xDataList["ESN"] = ""; }
            return xDataList;
        }

        function GetReportParameterList(Report) {
            var xDataList = {};
            xDataList["RPT"] = Report;
            xDataList["ESN"] = $get('<%= txtESNNew.ClientID %>').value;
            xDataList["LESN"] = $get("<%= hdnLastESN.ClientID %>").value;
            xDataList["CLID"] = $get('<%= hdnClientLocationIDNew.ClientID %>').value;
            xDataList["RDID"] = $get("hdnReceiveDetailID").value;

            if ($get('<%= txtESNNew.ClientID %>').value == "ESN/IMEI Number") { xDataList["ESN"] = ""; }

            if (Report.toUpperCase() == "BAGTAG") {
                return xDataList;
            }

            var IndexValue = $get('<%=drpProjectListNew.ClientID %>').selectedIndex;
            xDataList["Project"] = $get('<%=drpProjectListNew.ClientID %>').options[IndexValue].text;
            xDataList["ProjectID"] = $get('<%=drpProjectListNew.ClientID %>').options[IndexValue].value;

            xDataList["RHID"] = $get("hdnReceiveHeaderID").value;
            xDataList["RDBID"] = $get("hdnReceiveDetailBulkID").value;
            xDataList["UserName"] = $get("hdnUserName").value;
            xDataList["CurStepUp"] = $get("hdnStepUp").value;

            xDataList["PROJTAG"] = $get('<%=txtProjectTagNew.ClientID %>').value;
            xDataList["RMA"] = $get('<%= txtRMANew.ClientID %>').value;

            // We need to remove the watermark
            if ($get('<%= txtRMANew.ClientID %>').value == "RMA Number") { xDataList["RMA"] = ""; }
            if ($get('<%= txtRMANew.ClientID %>').value == "Work Order Number") { xDataList["RMA"] = ""; }

            if ($get('<%= txtProjectTagNew.ClientID %>').value == "Project Tag") { xDataList["PROJTAG"] = ""; }
            return xDataList;
        }

        function GetReceiveIDKeys() {
            var xDataList = {};
            xDataList["ReceiveHeaderID"] = $get("<%= hdnReceiveHeaderID.ClientID %>").value;
            xDataList["ReceiveDetailBulkID"] = $get("<%= hdnReceiveDetailBulkID.ClientID %>").value;
            xDataList["ReceiveDetailID"] = $get("<%= hdnReceiveDetailID.ClientID %>").value;



            var IndexValue = $get('<%=drpProjectListNew.ClientID %>').selectedIndex;
            xDataList["ProjectID"] = $get('<%=drpProjectListNew.ClientID %>').options[IndexValue].value;
            xDataList["Project"] = $get('<%=drpProjectListNew.ClientID %>').options[IndexValue].text;
            IndexValue = $get('<%=drpStatus.ClientID %>').selectedIndex;
            xDataList["StatusID"] = $get('<%=drpStatus.ClientID %>').options[IndexValue].value;
            xDataList["Status"] = $get('<%=drpStatus.ClientID %>').options[IndexValue].text;
            return xDataList;
        }

        function SetReceiveIDKeys(xDataList) {
            $get("<%= hdnReceiveHeaderID.ClientID %>").value = xDataList["ReceiveHeaderID"];
            $get("<%= hdnReceiveDetailBulkID.ClientID %>").value = xDataList["ReceiveDetailBulkID"];
            $get("<%= hdnReceiveDetailID.ClientID %>").value = xDataList["ReceiveDetailID"];
            return xDataList;
        }






        function GetDataStream(DataList) {
            var count = 0;
            var sb = new Sys.StringBuilder();
            for (var property in DataList) {
                if (count > 0) { sb.append(","); }
                sb.append("'" + property + "':'" + DataList[property] + "'");
                count += 1;
            }
            return sb.toString();
        }

        function setDropDownList(elementRef, valueToSetTo) {
            var isFound = false;

            for (var i = 0; i < elementRef.options.length; i++) {
                if (elementRef.options[i].value == valueToSetTo) {
                    elementRef.options[i].selected = true;
                    isFound = true;
                }
            }
            if (isFound == false)
                elementRef.options[0].selected = true;
        }

        function setDropDownListText(elementRef, valueToSetTo) {
            var isFound = false;

            for (var i = 0; i < elementRef.options.length; i++) {
                if (elementRef.options[i].text == valueToSetTo) {
                    elementRef.options[i].selected = true;
                    isFound = true;
                }
            }
            if (isFound == false)
                elementRef.options[0].selected = true;
        }










    </script>



</asp:Content>






