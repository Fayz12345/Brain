<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IFSCycleCount.aspx.cs" Inherits="GMPI_WebApp.IFSCycleCount" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cMC" runat="server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            
            <asp:HiddenField ID="hdnUserName" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnBatchNumber" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnCycleInventoryCountIterationHeaderID" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnDeviceOrPart" runat="server" ClientIDMode="Static" Value="Device" />
            <table id="Table3" runat="server" width="100%">
                <tr>
                    <td align="left" valign="top">
                        <h1>
                            Cycle Count Inventory (IFS)</h1>
                    </td>
                    <td align="right" valign="top">
                        <asp:Button ID="btnLockBatch" runat="server" Text="Lock Batch" UseSubmitBehavior="False"/>
                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnLockBatch"
                            ConfirmText="Are you sure you want to Lock this batch?">
                        </asp:ConfirmButtonExtender><br />
                        <asp:Button ID="btnResetCount" runat="server" Text="Reset Counter" UseSubmitBehavior="False"/>
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top">
                        <asp:Label ID="Label1" runat="server" Text="IFS Location" AssociatedControlID="txtIFSLocation"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtIFSLocation" runat="server" ForeColor="Black"
                            Font-Size="Larger" MaxLength="20"></asp:TextBox>
                        <asp:Button ID="btnFindBatch" runat="server" Text="Find Batch" UseSubmitBehavior="False"/>
                    </td>
                    <td align="right" valign="top">
                        <asp:Label ID="Batch" runat="server" Text="Batch:" AssociatedControlID="txtBatch"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtBatch" runat="server" ForeColor="Black" Font-Size="Larger"
                            MaxLength="20" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table runat="server" width="100%">
                <tr>
                    <td align="left" valign="top">
                        <asp:TabContainer ID="TabDevicePart" runat="server" AutoPostBack="True">
                            <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="Device">
                                <ContentTemplate>
                                    <table id="Table2" runat="server" width="100%">
                                        <tr>
                                            <td align="left" valign="top">
                                                <asp:Label ID="lblScanFieldText" runat="server" Text="Scan IMEI" AssociatedControlID="ScanKey"></asp:Label>
                                                <br />
                                                <asp:TextBox ID="ScanKey" runat="server" BackColor="#A5EBC5" ForeColor="Black" Font-Size="X-Large"
                                                    MaxLength="50" Enabled="False"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:TabPanel>
                            <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="Part">
                                <ContentTemplate>
                                    <asp:HiddenField ID="HiddenField1" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hdnActiveTab" runat="server" ClientIDMode="Static" Value="A" />
                                    <asp:Panel ID="Panel1" runat="server">
                                        <table id="Table5" runat="server" width="100%">
                                            <tr>
                                                <td colspan="3">
                                                    <asp:TabContainer ID="TabPartFlow" runat="server" BorderStyle="None" AutoPostBack="True">
                                                        <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="QTY/SKU" BorderStyle="None">
                                                            <ContentTemplate>
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td align="left" valign="top">
                                                                            <asp:Label ID="Labelx4" runat="server" Text="QTY" AssociatedControlID="txtQTYx"></asp:Label>
                                                                            <br />
                                                                            <asp:TextBox ID="txtQTYx" runat="server" BackColor="#A5EBC5" ForeColor="Black" Font-Size="Larger"
                                                                                Enabled="False"></asp:TextBox>
                                                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtQTYx"
                                                                                ValidChars="0123456789">
                                                                            </asp:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td align="left" valign="top">
                                                                            <asp:Label ID="Label3" runat="server" Text="Scan Part Number" AssociatedControlID="ScanKeyP"></asp:Label>
                                                                            <br />
                                                                            <asp:TextBox ID="ScanKeyP" runat="server" BackColor="#A5EBC5" ForeColor="Black" Font-Size="X-Large"
                                                                                MaxLength="50" Enabled="False"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </asp:TabPanel>
                                                        <asp:TabPanel ID="TabPanel4" runat="server" HeaderText="SKU/QTY" BorderStyle="None">
                                                            <ContentTemplate>
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td align="left" valign="top">
                                                                            <asp:Label ID="Label7" runat="server" Text="Scan Part Number" AssociatedControlID="ScanKeyB"></asp:Label>
                                                                            <br />
                                                                            <asp:TextBox ID="ScanKeyB" runat="server" BackColor="#A5EBC5" ForeColor="Black" Font-Size="Larger"
                                                                                MaxLength="50" Enabled="False"></asp:TextBox>
                                                                        </td>
                                                                        <td align="left" valign="top">
                                                                            <asp:Label ID="Label6" runat="server" Text="QTY" AssociatedControlID="txtQTYxB"></asp:Label>
                                                                            <br />
                                                                            <asp:TextBox ID="txtQTYxB" runat="server" BackColor="#A5EBC5" ForeColor="Black" Font-Size="X-Large"
                                                                                Enabled="False"></asp:TextBox>
                                                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtQTYxB"
                                                                                ValidChars="0123456789">
                                                                            </asp:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </asp:TabPanel>
                                                    </asp:TabContainer>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:TabPanel>
                        </asp:TabContainer>
                    </td>
                    <td align="right" valign="top">
                        <table id="Table1" runat="server" width="100%">
                            <tr>
                                <td align="right" valign="top">
                                    <asp:CheckBox ID="chkAutoReturn" runat="server" Text="Auto Go/Enter" TextAlign="right"
                                        AutoPostBack="True" Checked="True" /><asp:Button ID="btnPICRecord" runat="server" Text="Go/Enter" OnClientClick="RecordScan(); return false;" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    <asp:TextBox ID="txtPMCount" runat="server" ToolTip="Valid IMEI Scans" Enabled="False"
                                        BorderStyle="None" ForeColor="Black" Text="0" Font-Size="X-Large" style="text-align:right"></asp:TextBox>:Valid<br />
                                    <asp:TextBox ID="txtPMCountError" runat="server" ToolTip="Error Scans" Enabled="False"
                                        BorderStyle="None" ForeColor="Black" Text="0" Font-Size="X-Large" style="text-align:right"></asp:TextBox>:Error
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:Label ID="lblResponceMessage" runat="server" Text="Message goes here" Font-Size="Large"></asp:Label>

        </ContentTemplate>
    </asp:UpdatePanel>
   <script type="text/javascript">

       Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
       Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

       function BeginRequestHandler(sender, args) {
           if (args._postBackElement.id != "SkinTable1") {
               //                ConfigureWaitingPopup(Popup);
               //               alert(DTE.value);
               $('#loading').show();
           }
       }

       function EndRequestHandler(sender, args) {
           $('#loading').hide();
       }




       function SetKeydownEvents() {
           // Device Setup
           $("#<%=ScanKey.ClientID %>").keypress(function (e) {
               var evt = e ? e : event;
               var keyCode = evt.keyCode;
               if (keyCode == 13 || keyCode == 9) {
                   SetActiveTab('A');
                   RecordScanAuto();
                   return false;
               }
           });
           // parts setup below
           $("#<%=txtQTYx.ClientID %>").keypress(function (e) {
               var evt = e ? e : event;
               var keyCode = evt.keyCode;
               if (keyCode == 13) {
                   SetActiveTab('A');
                   $get("<%= ScanKeyP.ClientID %>").focus();
                   return false;
               }
           });

           $("#<%=ScanKeyP.ClientID %>").keypress(function (e) {
               var evt = e ? e : event;
               var keyCode = evt.keyCode;
               if (keyCode == 13 || keyCode == 9) {
                   SetActiveTab('A');
                   RecordScanAuto();
                   return false;
               }
           });

           $("#<%=ScanKeyB.ClientID %>").keypress(function (e) {
               var evt = e ? e : event;
               var keyCode = evt.keyCode;
               if (keyCode == 13 || keyCode == 9) {
                   SetActiveTab('B');
                   $get("<%= txtQTYxB.ClientID %>").focus();
                   return false;
               }
           });

           $("#<%=txtQTYxB.ClientID %>").keypress(function (e) {
               var evt = e ? e : event;
               var keyCode = evt.keyCode;
               if (keyCode == 13) {
                   SetActiveTab('B');
                   RecordScanAuto();
                   return false;
               }
           });
       }
       function SetActiveTab(Value) {
           $get("<%= hdnActiveTab.ClientID %>").value = Value;
       }
       //----------------------------------------------------------------

       function SetFocus(Name) {
           $get(Name).focus();
           return;
       }

 

       // -------------------------------------------   PARTS

       function RecordScanAuto() {
           if (AutoReturn() == false) { return; }
           var tab = GetMainTab();
           if (tab == 'Device') { SaveScanData(); }
           else { SaveScanDataP(); }
       }

       function RecordScan() {
           var tab = GetMainTab();
           if (tab == 'Device') { SaveScanData(); }
           else { SaveScanDataP(); }
       }

       function SaveScanDataP() {
           var xBatch = Batch();
           var xUserName = UserName();
           var xIFSLocation = Location();
           var xQTY = QTY();
           var xPartNumber = PartNumber();

           if (xPartNumber.length == 0) { return; }
           if (xQTY.length == 0) { return; }
           if (xBatch.length == 0) { return; }
           //CleanupForNextScan();
           var isDevice = '1';
           var tab = GetMainTab();
           if (tab != 'Device') { isDevice = '0'; }
           var service = new WebServer_01();
           service.LogCycleCountScan(xPartNumber
            , xBatch
            , xQTY.toString()
            , isDevice
            , '-1'
            , xUserName, onSuccessPartRecorded, onServerError);

           return;
       }

       function CleanupForNextScan() {
           var tab = ActiveTab();
           if (tab == 'A') {
//               $get("<%= txtQTYx.ClientID %>").value = '';
               $get("<%= ScanKeyP.ClientID %>").value = '';
               $get("<%= ScanKeyP.ClientID %>").focus();
           }
           else {
//               $get("<%= txtQTYxB.ClientID %>").value = '';
               $get("<%= ScanKeyB.ClientID %>").value = '';
               $get("<%= ScanKeyB.ClientID %>").focus();
           }
       }

       function onSuccessPartRecorded(result) {
           $get("<%= lblResponceMessage.ClientID %>").innerHTML = result;
           if (result.substring(0, 6) != 'Error:') {
               var c = Number(IMEICount());
               var d = c + 1;
               $get("<%= txtPMCount.ClientID %>").value = d;
           }
           else {
               var c = Number(IMEIErrorCount());
               var d = c + 1;
               $get("<%= ScanKey.ClientID %>").value = '';
               alert(result);
               $get("<%= txtPMCountError.ClientID %>").value = d;
           }
           CleanupForNextScan();
           return;
       }




       //----------------------------------------------------------------------------------------- DEVICES


       function SaveScanData() {
           var xESN = ESN();
           if (xESN.length == 0) { return; }
           var xBatch = Batch();
           if (xBatch.length == 0) { return; }
           var xUserName = UserName();
           var xLocation = Location();
           MessageData = "ESN:" + xESN + " Batch:" + xBatch;
           $get("<%= ScanKey.ClientID %>").value = '';
           $get("<%= ScanKey.ClientID %>").focus();
           $get("<%= lblResponceMessage.ClientID %>").innerHTML = MessageData;

           var isDevice = '1';
           var tab = GetMainTab();
           if (tab != 'Device') { isDevice = '0'; }
           var service = new WebServer_01();
           service.LogCycleCountScan(xESN
            , xBatch
            , '1'
            , isDevice
            , '-1'
            , xUserName, onSuccessDeviceRecorded, onServerError);
       }

       function onSuccessDeviceRecorded(result) {
           $get("<%= lblResponceMessage.ClientID %>").innerHTML = result;
           if (result.substring(0, 6) != 'Error:') {
               var c = Number(IMEICount());
               var d = c + 1;
               $get("<%= txtPMCount.ClientID %>").value = d;
           }
           else {
               var c = Number(IMEIErrorCount());
               var d = c + 1;
               $get("<%= ScanKey.ClientID %>").value = '';
               alert(result);
               $get("<%= txtPMCountError.ClientID %>").value = d;
           }
           $get("<%= ScanKey.ClientID %>").value = '';
           $get("<%= ScanKey.ClientID %>").focus();
           return;
       }

       function onServerError(Result) {
           alert('Error:' + Result.get_message());
       }

       // Variable Functions ----------------------------------
       function UserName() {
           return $get("<%= hdnUserName.ClientID %>").value;
       }

       function hdnBatchNumber() {
           return $get("<%= hdnBatchNumber.ClientID %>").value;
       }

       function SetBatchNumber(Value) {
           $get("<%= hdnBatchNumber.ClientID %>").value = Value;
           $get("<%= txtBatch.ClientID %>").value = Value;
       }


       function ESN() {
           return $get("<%= ScanKey.ClientID %>").value;
       }

       function ScanKey() {
           var value = $get("<%= ScanKey.ClientID %>").value;
           return value;
       }
       function IMEICount() {
           var value = $get("<%= txtPMCount.ClientID %>").value;
           return value;
       }
       function IMEIErrorCount() {
           var value = $get("<%= txtPMCountError.ClientID %>").value;
           return value;
       }

       function Batch() {
           var value = $get("<%= txtBatch.ClientID %>").value;
           return value;
       }

       function Location() {
           var value = $get("<%= txtIFSLocation.ClientID %>").value;
           //alert('Getting Location:' + value);
           return value;
       }

       function LocationID() {
           return -1;
       }


       function GetMainTab() {
           return $get("<%= hdnDeviceOrPart.ClientID %>").value;
       }

       function ActiveTab() {
           return $get("<%= hdnActiveTab.ClientID %>").value;
       }
       function AutoReturn() {
           return $get("<%= chkAutoReturn.ClientID %>").checked;
       }
//       function AutoReturnP() {
//           return $get("< %= chkAutoReturnP.ClientID %>").checked;
//       }

       function QTY() {
           var value = $get("<%= txtQTYx.ClientID %>").value;
           if (ActiveTab() == 'B') { value = $get("<%= txtQTYxB.ClientID %>").value; }
           return value;
       }

       function PartNumber() {
           var aTab = ActiveTab();
           var value = $get("<%= ScanKeyP.ClientID %>").value;
           if (ActiveTab() == 'B') { value = $get("<%= ScanKeyB.ClientID %>").value; }
           return value;
       }

       //-----------------------------------------------------------------------------------------------






       function GetDropDownValue(Name) {
           var IndexValue = $get(Name).selectedIndex;
           var xValue = '';
           if (IndexValue > -1) { xValue = $get(Name).options[IndexValue].value; }
           return xValue;
       }

       function GetDropDownText(Name) {
           var IndexValue = $get(Name).selectedIndex;
           var xValue = '';
           if (IndexValue > -1) { xText = $get(Name).options[IndexValue].text; }
           return xText;
       }



       function addOption(selectbox, value, text, SelectedValue) {
           var optn = document.createElement('OPTION');
           optn.text = text;
           optn.value = value;
           if (value == SelectedValue) { optn.setAttribute('selected', 'selected'); }
           selectbox.options.add(optn);
       }









      // -----------------------------------------------------------------

       function PrintReport() {
           var xBatch = Batch();
           if (xBatch.length == 0) {
               alert('You must supply a batch');
               return;
           }
           var xDataList = {};
           xDataList["RPT"] = "PHYSICALCOUNT";
           xDataList["KEY"] = xBatch;
           xDataList["USERNAME"] = UserName();
           var pstring = GetParameterStream(xDataList);
           // var WindowToOpen = "RPT_SpotCountReport.aspx";
           var WindowToOpen = "/Reports/RPT_EXCEL_Out.aspx";
           if (pstring.length > 0) {
               WindowToOpen = WindowToOpen + "?" + pstring
           }
           var win = window.open(WindowToOpen, "_blank", "menubar", true);
           return;
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


