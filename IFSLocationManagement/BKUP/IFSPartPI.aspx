<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IFSPartPI.aspx.cs" Inherits="GMPI_WebApp.IFSPartPI" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cMC" runat="server">
    <asp:HiddenField ID="hdnUserName" runat="server" ClientIDMode="Static" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            
            <asp:TabContainer ID="TabContainer1" runat="server">
                <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="Parts Inventory Distribution (IFS)" Visible="False">
                    <ContentTemplate>
                        <table id="Table2" runat="server">
                            <tr>
                                <td align="center" colspan="2">
                                    <h1>
                                        Parts Inventory Distribution (IFS)</h1>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <asp:Label ID="Label3" runat="server" Text="Warehouse" AssociatedControlID="drpLocationList"></asp:Label>
                                    <asp:DropDownList ID="drpLocationList" runat="server" ToolTip="Warehouse where these parts are located"
                                        AutoPostBack="false">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top">
                                    <table id="Table1" runat="server">
                                        <tr>
                                            <td align="left" colspan="2">
                                                <h1>
                                                    From Location</h1>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" valign="top" colspan="2">
                                                <asp:TextBox ID="IFSLocationFrom" runat="server" ToolTip="Part Location From." Font-Size="X-Large"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                GMP Part Number:
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtGMPPartNumber" runat="server" ToolTip="Part Number."></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="left" valign="top">
                                    <table id="Table3" runat="server">
                                        <tr>
                                            <td align="left" colspan="2">
                                                <h1>
                                                    To Location</h1>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" valign="top" colspan="2">
                                                <asp:TextBox ID="IFSLocationTo" runat="server" ToolTip="Part Location To." Font-Size="X-Large"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                QTY:
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtQTY" runat="server" ToolTip="Quantity."></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2" valign="top">
                                    <br />
                                    <asp:Button ID="btnUpdateLocation" runat="server" Text="Update" UseSubmitBehavior="False"
                                        Width="100%" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top" colspan="2">
                                    <br />
                                    <br />
                                    <asp:Label ID="lblMessage" runat="server" Text="" Height="100%" Width="100%"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="Part Physical Inventory Count">
                    <ContentTemplate>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                            <asp:HiddenField ID="hdnBatchNumber" runat="server" ClientIDMode="Static" />
                            <asp:HiddenField ID="hdnActiveTab" runat="server" ClientIDMode="Static" Value="A" />
                                <table id="Table4" runat="server" width="100%">
                                    <tr>
                                        <td align="left" valign="top">
                                            <h1>
                                                Part Physical Inventory Count (IFS)</h1>
                                        </td>
                                        <td align="right" valign="top">
                                            <asp:CheckBox ID="chkUpdateInventory" runat="server" Text="Update Inventory" TextAlign="right"
                                                Enabled="False" Checked="True" />
                                            <br />
<%--                                            <asp:Button ID="btnDownloadPIC" runat="server" Text="Download Batch" OnClientClick='PrintReport(); return false;' />
                                            <br />--%>
                                            <asp:Button ID="btnLockBatch" runat="server" Text="Lock Batch" UseSubmitBehavior="False" />
                                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnLockBatch"
                                                ConfirmText="Are you sure you want to Lock this batch?">
                                            </asp:ConfirmButtonExtender>


                                        </td>
                                    </tr>
                                </table>
                                <asp:Panel ID="Panel1" runat="server">
                                    <table id="Table5" runat="server" width="100%">
                                        <tr>
                                            <td align="left" valign="top" width="20%">
                                                Batch
                                            </td>
                                            <td align="left" valign="top" width="20%">
                                                <asp:Label ID="Label2" runat="server" Text="Warehouse"></asp:Label>
                                            </td>
                                            <td align="left" valign="top" width="20%">
                                            </td>
                                            <td align="left" valign="top" width="20%">
                                            </td>
                                            <td align="left" valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" valign="top">
                                                <asp:TextBox ID="txtBatch" runat="server" ToolTip="Batch." MaxLength="25" Enabled="False"></asp:TextBox>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="drpPILocationList" runat="server" ToolTip="Warehouse where these parts are located"
                                                    AutoPostBack="false">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left" valign="top">
                                            </td>
                                            <td align="left" valign="top">
                                            </td>
                                            <td align="left" valign="top">
                                                <asp:Button ID="btnResetCount" runat="server" Text="Reset Counter" UseSubmitBehavior="False" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:TabContainer ID="TabContainer2" runat="server">
                                                    <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="Location/QTY/SKU" BorderStyle="None">
                                                        <ContentTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td align="left" valign="top">
                                                                        <asp:Label ID="Label1" runat="server" Text="IFS Location" AssociatedControlID="txtIFSLocation"></asp:Label>
                                                                        <br />
                                                                        <asp:TextBox ID="txtIFSLocation" runat="server" BackColor="#A5EBC5" ForeColor="Black"
                                                                            Font-Size="Larger" MaxLength="20"></asp:TextBox>
                                                                    </td>
                                                                    <td align="left" valign="top">
                                                                        <asp:Label ID="Label4" runat="server" Text="QTY" AssociatedControlID="txtQTYx"></asp:Label>
                                                                        <br />
                                                                        <asp:TextBox ID="txtQTYx" runat="server" BackColor="#A5EBC5" ForeColor="Black" Font-Size="Larger"></asp:TextBox>
                                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtQTYx"
                                                                            ValidChars="0123456789">
                                                                        </asp:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td align="left" valign="top">
                                                                        <asp:Label ID="lblScanFieldText" runat="server" Text="Scan Part Number" AssociatedControlID="ScanKey"></asp:Label>
                                                                        <br />
                                                                        <asp:TextBox ID="ScanKey" runat="server" BackColor="#A5EBC5" ForeColor="Black" Font-Size="X-Large"
                                                                            MaxLength="50"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:TabPanel>
                                                    <asp:TabPanel ID="TabPanel4" runat="server" HeaderText="Location/SKU/QTY" BorderStyle="None">
                                                        <ContentTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td align="left" valign="top">
                                                                        <asp:Label ID="Label5" runat="server" Text="IFS Location" AssociatedControlID="txtIFSLocationB"></asp:Label>
                                                                        <br />
                                                                        <asp:TextBox ID="txtIFSLocationB" runat="server" BackColor="#A5EBC5" ForeColor="Black"
                                                                            Font-Size="Larger" MaxLength="20"></asp:TextBox>
                                                                    </td>


                                                                    <td align="left" valign="top">
                                                                        <asp:Label ID="Label7" runat="server" Text="Scan Part Number" AssociatedControlID="ScanKeyB"></asp:Label>
                                                                        <br />
                                                                        <asp:TextBox ID="ScanKeyB" runat="server" BackColor="#A5EBC5" ForeColor="Black" Font-Size="Larger"
                                                                            MaxLength="50"></asp:TextBox>
                                                                    </td>
                                                                    <td align="left" valign="top">
                                                                        <asp:Label ID="Label6" runat="server" Text="QTY" AssociatedControlID="txtQTYxB"></asp:Label>
                                                                        <br />
                                                                        <asp:TextBox ID="txtQTYxB" runat="server" BackColor="#A5EBC5" ForeColor="Black" Font-Size="X-Large"></asp:TextBox>
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


                                        
                                            <td align="left" valign="top">
                                                <asp:CheckBox ID="chkAutoReturn" runat="server" Text="Auto Go/Enter" TextAlign="right"/>
                                                <br />
                                                <asp:Button ID="btnPICRecord" runat="server" Text="Go/Enter" OnClientClick="RecordScan(); return false;" UseSubmitBehavior="False" />
                                            </td>
                                            <td align="left" valign="top">
                                                Valid:<br />
                                                <asp:TextBox ID="txtPMCount" runat="server" ToolTip="Valid IMEI Scans" Enabled="False"
                                                    BorderStyle="None" ForeColor="Black" Text="0" Font-Size="X-Large" Width="50%"></asp:TextBox>
                                                <br />
                                                Error:<br />
                                                <asp:TextBox ID="txtPMCountError" runat="server" ToolTip="Error Scans" Enabled="False"
                                                    BorderStyle="None" ForeColor="Black" Text="0" Font-Size="X-Large" Width="50%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="5">
                                                <asp:Label ID="lblPIMessage" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>


                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>






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





       function KeydownLocation() {
           $("#<%=txtIFSLocation.ClientID %>").keydown(function (e) {
               var evt = e ? e : event;
               var keyCode = evt.keyCode;
               if (keyCode == 13) {
                   SetActiveTab('A');
                   $get("<%= txtQTYx.ClientID %>").focus();
                   return false;
               }
           });
           $("#<%=txtQTYx.ClientID %>").keypress(function (e) {
               var evt = e ? e : event;
               var keyCode = evt.keyCode;
               if (keyCode == 13) {
                   SetActiveTab('A');
                   $get("<%= ScanKey.ClientID %>").focus();
                   return false;
               }
           });
           $("#<%=ScanKey.ClientID %>").keypress(function (e) {
               var evt = e ? e : event;
               var keyCode = evt.keyCode;
               if (keyCode == 13 || keyCode == 9) {
                   //                   alert('Select Active Tabe A');
                   SetActiveTab('A');
                   //                   alert('Select Active Tabe Ax');
                   RecordScanAuto();
                   return false;
               }
           });


           $("#<%=txtIFSLocationB.ClientID %>").keydown(function (e) {
               var evt = e ? e : event;
               var keyCode = evt.keyCode;
               if (keyCode == 13) {
                   SetActiveTab('B');
                   $get("<%= ScanKeyB.ClientID %>").focus();
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
       // --------------------------------------------------------------------------------------------------------------
       function PrintReport() {
           var xBatch = Batch();
           if (xBatch.length == 0) {
               alert('You must supply a batch');
               return;
           }
           var xDataList = {};
           xDataList["RPT"] = "PARTPHYSICALCOUNT";
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

       // --------------------------------------------------------------------------------------------------------------

       function RecordScanAuto() {
           if (AutoReturn() == false) { return; }
           RecordScan();
       }


       function RecordScan() {
           //           alert('Tab:' + ActiveTab());
           var xBatch = Batch();
           var xUserName = UserName();

           var xIFSLocation = Location();
           var xQTY = QTY();
           var xPartNumber = PartNumber();

           if (xIFSLocation.length == 0) { return; }
           if (xPartNumber.length == 0) { return; }
           if (xQTY.length == 0) { return; }
           if (xBatch.length == 0) {
               var service = new WebServer_01();
               service.GetNewPhysicalDeviceBatch(xUserName, onSuccessNewBatch, onServerError);
           } else { SaveScanData(); }
       }

       function onSuccessNewBatch(result) {
           SetBatchNumber(result);
           SaveScanData();
       }


       function SaveScanData() {
           var xBatch = Batch();
           var xUserName = UserName();
           var xWarehouse = Warehouse();
           var xWarehouseID = WarehouseID();

           var xIFSLocation = Location();
           var xQTY = QTY();
           var xPartNumber = PartNumber();

           if (xIFSLocation.length == 0) { return; }
           if (xPartNumber.length == 0) { return; }
           if (xQTY.length == 0) { return; }
           if (xBatch.length == 0) { return; }

           var xUpdateInventory = UpdateInventory();
           var sUpdateInventory = '0';
           if (xUpdateInventory == true) { sUpdateInventory = '1'; }

           var service = new WebServer_01();

           //           alert('Going In' + POReceiptDatex);

           service.LogPhysicalPartCount(xPartNumber
                                        , xBatch
                                        , xQTY
                                        , xIFSLocation
                                        , xWarehouse
                                        , xWarehouseID
                                        , sUpdateInventory
                                        , xUserName, onSuccessDeviceRecorded, onServerError);
       }



       function onSuccessDeviceRecorded(result) {
           $get("<%= lblPIMessage.ClientID %>").innerHTML = result;
           //           //           alert('2Here:' + result);
           if (result.substring(0, 6) != 'Error:') {
               var c = Number(PartCount());
               var d = c + 1;
               $get("<%= txtPMCount.ClientID %>").value = d;
           }
           else {
               var c = Number(PartErrorCount());
               var d = c + 1;
               alert(result);
               $get("<%= txtPMCountError.ClientID %>").value = d;
           }
           $get("<%= txtIFSLocation.ClientID %>").value = '';
           $get("<%= txtQTYx.ClientID %>").value = '';
           $get("<%= ScanKey.ClientID %>").value = '';
           $get("<%= txtIFSLocationB.ClientID %>").value = '';
           $get("<%= txtQTYxB.ClientID %>").value = '';
           $get("<%= ScanKeyB.ClientID %>").value = '';
           $get("<%= txtIFSLocation.ClientID %>").focus();
           if (ActiveTab() == 'B') { $get("<%= txtIFSLocationB.ClientID %>").focus(); }
           return;
       }

       function onServerError(Result) {
           alert('Error:' + Result.get_message());
       }

       // ----------------------------------------------------------------------------------------------------------------------------------------


       // ----------------------------------------------------------------------------------------------------------------------------------------



       // Variable Functions ----------------------------------
       function UserName() {
           return $get("<%= hdnUserName.ClientID %>").value;
       }



       function ActiveTab() {
           return $get("<%= hdnActiveTab.ClientID %>").value;
       }

       function SetActiveTab(Value) {
           $get("<%= hdnActiveTab.ClientID %>").value = Value;
       }


       function hdnBatchNumber() {
           return $get("<%= hdnBatchNumber.ClientID %>").value;
       }

       function SetBatchNumber(Value) {
           $get("<%= hdnBatchNumber.ClientID %>").value = Value;
           $get("<%= txtBatch.ClientID %>").value = Value;
       }

       function Warehouse() {
           var IndexValue = $get("<%= drpPILocationList.ClientID %>").selectedIndex;
           var value = $get("<%= drpPILocationList.ClientID %>").options[IndexValue].text;
           return value;
       }


       function WarehouseID() {
           var IndexValue = $get("<%= drpPILocationList.ClientID %>").selectedIndex;
           var value = $get("<%= drpPILocationList.ClientID %>").options[IndexValue].value;
           return value;
       }

       function UpdateInventory() {
           return $get("<%= chkUpdateInventory.ClientID %>").checked;
       }

       function AutoReturn() {
           return $get("<%= chkAutoReturn.ClientID %>").checked;
       }

       function PartCount() {
           var value = $get("<%= txtPMCount.ClientID %>").value;
           return value;
       }

       function PartErrorCount() {
           var value = $get("<%= txtPMCountError.ClientID %>").value;
           return value;
       }

       function Batch() {
           var value = $get("<%= txtBatch.ClientID %>").value;
           return value;
       }


       // ----------------------------------------------
       function Location() {
           var value = $get("<%= txtIFSLocation.ClientID %>").value;
           if (ActiveTab() == 'B') { value = $get("<%= txtIFSLocationB.ClientID %>").value; }
           return value;
       }
       function QTY() {
           var value = $get("<%= txtQTYx.ClientID %>").value;
           if (ActiveTab() == 'B') { value = $get("<%= txtQTYxB.ClientID %>").value; }
           return value;
       }
       function PartNumber() {
           var value = $get("<%= ScanKey.ClientID %>").value;
           if (ActiveTab() == 'B') { value = $get("<%= ScanKeyB.ClientID %>").value; }
           return value;
       }
       // --------------------------------------------















    </script>
  

</asp:Content>

