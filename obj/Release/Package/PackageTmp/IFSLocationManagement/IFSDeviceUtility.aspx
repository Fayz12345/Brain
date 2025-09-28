<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IFSDeviceUtility.aspx.cs" Inherits="BW_WebApp.IFSLocationManagement.IFSDeviceUtility" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cMC" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            
            <asp:HiddenField ID="hdnUserName" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnCurrentIMEI" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnCarrierID" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnManufacturerID" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnModelID" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnColourID" runat="server" ClientIDMode="Static" />
            <asp:TabContainer ID="TabContainer1" runat="server">
                <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="Device Inventory Distribution (IFS)">
                    <ContentTemplate>
                        <table id="Table1" runat="server" width="100%">
                            <tr>
                                <td align="left" valign="top" colspan="3">
                                    <asp:Label ID="lblMessage" runat="server" Text="" Height="100%" Width="100%"></asp:Label>
                                </td>
                            </tr>
                            <tr id="Tr1" runat="server">
                                <td id="Td1" align="center" colspan="3" runat="server">
                                    <h1>
                                        Device Utilities</h1>
                                </td>
                            </tr>
                            <tr id="Tr2" runat="server">
                                <td id="Td3" runat="server" align="right" valign="top">
                                    <asp:Panel ID="Panel6" runat="server" Style="overflow: auto; min-width: 300px;" HorizontalAlign="Left"
                                        Width="100%">
                                        From<br />
                                        <asp:TabContainer ID="TabContainer3" runat="server">
                                            <asp:TabPanel ID="TabDIDMFromESN" runat="server" HeaderText="ESN" ToolTip="ESN">
                                                <ContentTemplate>
                                                    Select by ESN IMEI<br />
                                                    <br />
                                                    <asp:Panel ID="Panel7" runat="server" Style="overflow: auto; max-height: 360px; min-height: 360px;
                                                        max-width: 390px;" HorizontalAlign="Left" Width="100%" DefaultButton="btrRecord">
                                                        <table id="Table8" runat="server" width="100%">
                                                            <tr>
                                                                <td style="border-style: solid none none none; border-width: thin; border-color: #FFCC66;
                                                                    text-align: right; vertical-align: top;">
                                                                    ESN:
                                                                </td>
                                                                <td style="border-style: solid none none none; border-width: thin; border-color: #FFCC66;
                                                                    text-align: left; vertical-align: top;">
                                                                    <asp:TextBox ID="txtESN" runat="server" BackColor="#CCFFFF" ForeColor="Black" Font-Size="Larger"
                                                                        MaxLength="20" ToolTip="ESN List Number."></asp:TextBox>
                                                                </td>
                                                                <td style="border-style: solid none none none; border-width: thin; border-color: #FFCC66;
                                                                    text-align: right; vertical-align: top;">
                                                                    <asp:Button ID="btrRecord" runat="server" Text=">" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </asp:TabPanel>
                                            <asp:TabPanel ID="TabDIDMFromBin" runat="server" HeaderText="Bin" ToolTip="Bin">
                                                <ContentTemplate>
                                                    Select by bin.<br />
                                                    <br />
                                                    <asp:Panel ID="Panel1" runat="server" Style="overflow: auto; max-height: 360px; min-height: 360px;
                                                        max-width: 390px;" HorizontalAlign="Left" Width="100%" DefaultButton="btnRecordBin">
                                                        <table id="Table7" runat="server" width="100%">
                                                            <tr>
                                                                <td style="border-style: solid none none none; border-width: thin; border-color: #FFCC66;
                                                                    text-align: right; vertical-align: top;">
                                                                    BIN:
                                                                </td>
                                                                <td style="border-style: solid none none none; border-width: thin; border-color: #FFCC66;
                                                                    text-align: left; vertical-align: top;">
                                                                    <asp:TextBox ID="lblBinNumber" runat="server" BackColor="#CCFFFF" ForeColor="Black"
                                                                        Font-Size="Larger" ToolTip="Bin Number for which the Location number is updated."></asp:TextBox>
                                                                </td>
                                                                <td style="border-style: solid none none none; border-width: thin; border-color: #FFCC66;
                                                                    text-align: right; vertical-align: top;">
                                                                    <asp:Button ID="btnRecordBin" runat="server" Text=">" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </asp:TabPanel>
                                            <asp:TabPanel ID="TabDIDMFromLocation" runat="server" HeaderText="Location" ToolTip="IFS Location">
                                                <ContentTemplate>
                                                    Select by IFS location<br />
                                                    <br />
                                                    <asp:Panel ID="Panel8" runat="server" Style="overflow: auto; max-height: 360px; min-height: 360px;
                                                        max-width: 390px;" HorizontalAlign="Left" Width="100%" DefaultButton="btrRecordLocation">
                                                        <table id="Table6" runat="server" width="100%">
                                                            <tr>
                                                                <td style="border-style: solid none none none; border-width: thin; border-color: #FFCC66;
                                                                    text-align: right; vertical-align: top;">
                                                                    Location:
                                                                </td>
                                                                <td style="border-style: solid none none none; border-width: thin; border-color: #FFCC66;
                                                                    text-align: left; vertical-align: top;">
                                                                    <asp:TextBox ID="IFSMoveFromLocationOnly" runat="server" ToolTip="IFS Location for which the Location number is updated."
                                                                        BackColor="#CCFFFF" ForeColor="Black" Font-Size="Larger"></asp:TextBox>
                                                                </td>
                                                                <td style="border-style: solid none none none; border-width: thin; border-color: #FFCC66;
                                                                    text-align: right; vertical-align: top;">
                                                                    <asp:Button ID="btrRecordLocation" runat="server" Text=">" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </asp:TabPanel>
                                            <asp:TabPanel ID="TabDIDMFromSite" runat="server" HeaderText="Site" ToolTip="Site">
                                                <ContentTemplate>
                                                    Select by site.<br />
                                                    <br />
                                                    <asp:Panel ID="Panel9" runat="server" Style="overflow: auto; max-height: 360px; min-height: 360px;
                                                        max-width: 395px;" HorizontalAlign="Left" Width="100%" DefaultButton="btnRecordSite">
                                                        <table id="Table4" runat="server" width="100%">
                                                            <tr>
                                                                <td style="border-style: solid none none none; border-width: thin; border-color: #FFCC66;
                                                                    text-align: right; vertical-align: top;">
                                                                    Site:
                                                                </td>
                                                                <td style="border-style: solid none none none; border-width: thin; border-color: #FFCC66;
                                                                    text-align: left; vertical-align: top;">
                                                                    <asp:DropDownList ID="drpMoveIFSSite" runat="server" BackColor="#CCFFFF" ForeColor="Black"
                                                                        Font-Size="Larger">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="border-style: solid none none none; border-width: thin; border-color: #FFCC66;
                                                                    text-align: right; vertical-align: top;">
                                                                    <asp:Button ID="btnRecordSite" runat="server" Text=">" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" valign="top">
                                                                    Project:
                                                                </td>
                                                                <td align="left" valign="top" colspan="2">
                                                                    <asp:DropDownList ID="drpMoveIFSProject" runat="server" BackColor="#CCFFFF" ForeColor="Black"
                                                                        Font-Size="Larger" Width="98%">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" valign="top">
                                                                    SKU:
                                                                </td>
                                                                <td align="left" valign="top" colspan="2">
                                                                    <asp:TextBox ID="txtFromSku" runat="server" ToolTip="IFS Sku." MaxLength="25" BackColor="#CCFFFF"
                                                                        ForeColor="Black" Font-Size="Larger"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" valign="top">
                                                                    Location:
                                                                </td>
                                                                <td align="left" valign="top" colspan="2">
                                                                    <asp:TextBox ID="txtFromLocation" runat="server" ToolTip="IFS Location." MaxLength="20"
                                                                        BackColor="#CCFFFF" ForeColor="Black" Font-Size="Larger"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" valign="top">
                                                                    Condition:
                                                                </td>
                                                                <td align="left" valign="top" colspan="2">
                                                                    <asp:DropDownList ID="drpMoveIFSCondition" runat="server" BackColor="#CCFFFF" ForeColor="Black"
                                                                        Font-Size="Larger" Width="98%">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </asp:TabPanel>
                                            <asp:TabPanel ID="TabDIDMFromPaste" runat="server" HeaderText="Paste" ToolTip="Paste">
                                                <ContentTemplate>
                                                    Paste list.<br />
                                                    <br />
                                                    <asp:Panel ID="Panel10" runat="server" Style="overflow: auto; max-height: 360px;
                                                        min-height: 360px; max-width: 390px;" HorizontalAlign="Left" Width="100%" DefaultButton="btnPasteParse">
                                                        <table id="Table9" runat="server" width="100%">
                                                            <tr>
                                                                <td style="border-style: solid none none none; border-width: thin; border-color: #FFCC66;
                                                                    text-align: left; vertical-align: top;">
                                                                    <asp:RadioButtonList ID="PasteDeliminator" runat="server" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Text="Excel" Value="Excel" Selected="True"> </asp:ListItem>
                                                                        <asp:ListItem Text="Comma" Value="Comma"></asp:ListItem>
                                                                        <asp:ListItem Text="Space" Value="Space"></asp:ListItem>

                                                                       
                                                                    </asp:RadioButtonList>
                                                                </td>
                                                                <td style="border-style: solid none none none; border-width: thin; border-color: #FFCC66;
                                                                    text-align: right; vertical-align: top;">
                                                                    <asp:Button ID="btnPasteParse" runat="server" Text=">" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="border-style: solid none none none; border-width: thin; border-color: #FFCC66;
                                                                    text-align: left; vertical-align: top;" colspan="2">
                                                                    <asp:TextBox ID="txtPasteParse" runat="server" BackColor="#CCFFFF" ForeColor="Black"
                                                                        Font-Size="Larger" ToolTip="" Rows="14" Height="100%" Width="97%" TextMode="MultiLine"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </asp:TabPanel>
                                        </asp:TabContainer>
                                    </asp:Panel>
                                </td>
                                <td id="Td2" runat="server" align="Left" valign="top">
                                    List<br />
                                    <asp:Panel ID="Panel5" runat="server" Style="overflow: auto; min-width: 150px;" HorizontalAlign="Left"
                                        Width="100%">
                                        <asp:ImageButton ID="imgbtnClear" runat="server" ImageUrl="~/Images/deleted.gif"
                                            Width="20px" ToolTip="Clear Screen" ImageAlign="Middle" />
                                        <asp:ImageButton ID="imgbtnDeleteIMIE" runat="server" ImageUrl="~/Images/delete-pro.bmp"
                                            Width="20px" ToolTip="Delete IMEI from List" ImageAlign="Middle" />
                                        <asp:TextBox ID="txtCount" runat="server" ClientIDMode="Static" Enabled="False" Text="0"
                                            ToolTip="Count" Width="25%"></asp:TextBox><br />
                                        <asp:ListBox ID="lstHistory" runat="server" ToolTip="List of ESN/IMEI Numbers for which the Location number is updated."
                                            Height="100%" Width="100%" Rows="25" SelectionMode="Multiple"></asp:ListBox>
                                    </asp:Panel>
                                </td>
                                <td id="Td4" runat="server" align="right" valign="top">
                                    <asp:Panel ID="Panel4" runat="server" Style="overflow: auto; min-width: 420px;" HorizontalAlign="Left"
                                        Width="100%">
                                        To<br />
                                        <asp:TabContainer ID="TabContainer2" runat="server">
                                            <asp:TabPanel ID="TabDIDMToLoc" runat="server" HeaderText="IFS Lock" ToolTip="Turn off, Turn On Sending transactions to IFS for this Device">
                                                <ContentTemplate>
                                                    Turn On, Off This Device transactions to IFS (On Sends transactions to IFS, Off does not.<br />
                                                    <br />
                                                    <asp:Panel ID="Panel3" runat="server" Style="overflow: auto; max-height: 360px; min-height: 360px;
                                                        max-width: 415px;" HorizontalAlign="Left" Width="100%">

                                                        <asp:RadioButtonList ID="rdlTransactionOnOff" runat="server" RepeatDirection="Horizontal" ToolTip="Turn IFS Transactions On (Send)/Off (Do not send)">
                                                        <asp:ListItem Text="On" Value="On"></asp:ListItem>
                                                        <asp:ListItem Selected="True" Text="Off" Value="Off"></asp:ListItem>
                                                        </asp:RadioButtonList><br /><br />


                                                        <asp:Button ID="btnIFSLock" runat="server" Text="Go" UseSubmitBehavior="False" /><br />
                                                        <br />
                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </asp:TabPanel>
                                        </asp:TabContainer>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top" colspan="3">
                                    <asp:Label ID="lblMessageBTM" runat="server" Text="" Height="100%" Width="100%"></asp:Label>
                                </td>
                            </tr>
                        </table>
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
               //               alert(DTE.value);
               $('#loading').show();
           }
       }

       function EndRequestHandler(sender, args) {
           $('#loading').hide();
       }



       //----------------------------------------------------------------



       function RecordHistory(Value) {
           var Source = $get("<%= lstHistory.ClientID %>");
           // Check to see if the item is already there.
           var xc = Source.getElementsByTagName('option').length;
           for (var i = 0; i < xc; i++) {
               if (Source.options[i].value == Value)
                   return;
           }
           if (Source != null) {
               var newOption = new Option(); // Create a new instance of ListItem
               newOption.text = Value;
               Source.options[Source.length] = newOption; //Append the item in Target
           }
       }

       function SetESNFocus() {
           SetFocus("<%= txtESN.ClientID %>");
           return;
       }

       function SetFocus(Name) {
           $get(Name).focus();
           return;
       }


       function OpenFinishProductLabel() {
           var WindowToOpen = 'FinishProductLabel_Bulk.aspx';
           var win = window.open(WindowToOpen, '_blank', 'menubar', true);
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

       //-----------------------------------------------------------------------------------------
       //       function RecordScanAuto() {
       //           if (AutoReturn() == false) { return; }
       //           RecordScan();
       //       }


       //       function RecordScan() {
       //           var xESN = ESN();
       //           if (xESN.length == 0) { return; }
       //           var xBatch = Batch();
       //           var xUserName = UserName();

       //           if (xBatch.length == 0) {
       //               var service = new WebServer_01();
       //               service.GetNewPhysicalDeviceBatch(xUserName, onSuccessNewBatch, onServerError);
       //           } else { SaveScanData(); }
       //       }

       //       function onSuccessNewBatch(result) {
       //           SetBatchNumber(result);
       //           SaveScanData();
       //       }

       //       function SaveScanData() {
       //           var xESN = ESN();
       //           var xBatch = Batch();
       //           if (xESN.length == 0) { return; }
       //           var xUserName = UserName();

       //           var xLocationID = LocationID().toString(); ;
       //           var xIFSConditionID = ConditionID().toString(); ;
       //           var xIFSSite = Site();
       //           var xIFSProject = Project();
       //           var xIFSLocation = Location();
       //           var xIFSCondition = Condition();
       //           //           var xGrade = Grade();
       //           var xUpdateIMEI = UpdateIMEI();
       //           var sUpdateIMEI = '0';
       //           if (xUpdateIMEI == true) { sUpdateIMEI = '1'; }

       //           var xKitted = Kitted();
       //           var sKitted = '0';
       //           if (xKitted == true) { sKitted = '1'; }
       //           var xUnlocked = Unlocked();
       //           var sUnlocked = '0';
       //           if (xUnlocked == true) { sUnlocked = '1'; }
       //           var service = new WebServer_01();
       //           //           alert('Going In' + POReceiptDatex);
       //           service.LogPhysicalDeviceCount(xLocationID
       //                             , xIFSConditionID
       //                             , xESN
       //                             , xBatch
       //                             , xIFSSite
       //                             , xIFSProject
       //                             , ''
       //                             , xIFSLocation
       //                             , xIFSCondition
       //                             , ''
       //                             , sKitted
       //                             , sUnlocked
       //                             , sUpdateIMEI
       //                             , xUserName, onSuccessDeviceRecorded, onServerError);
       //       }

       //       function onSuccessDeviceRecorded(result) {
       //           $get("< %= lblPIMessage.ClientID %>").innerHTML = result;
       //           if (result.substring(0, 6) != 'Error:') {
       //               var c = Number(IMEICount());
       //               var d = c + 1;
       //               $get("< %= txtPMCount.ClientID %>").value = d;
       //           }
       //           else {
       //               var c = Number(IMEIErrorCount());
       //               var d = c + 1;
       //               $get("< %= ScanKey.ClientID %>").value = '';
       //               alert(result);
       //               $get("< %= txtPMCountError.ClientID %>").value = d;
       //           }
       //           $get("< %= ScanKey.ClientID %>").value = '';
       //           $get("< %= ScanKey.ClientID %>").focus()
       //           return;
       //       }

       //       function onServerError(Result) {
       //           alert('Error:' + Result.get_message());
       //       }










       // Variable Functions ----------------------------------
       function UserName() {
           return $get("<%= hdnUserName.ClientID %>").value;
       }

       //       function hdnBatchNumber() {
       //           return $get("< %= hdnBatchNumber.ClientID %>").value;
       //       }

       //       function SetBatchNumber(Value) {
       //           $get("< %= hdnBatchNumber.ClientID %>").value = Value;
       //           $get("< %= txtBatch.ClientID %>").value = Value;
       //       }       


       //       function ESN() {
       //           return $get("< %= ScanKey.ClientID %>").value;
       //       }
       //       function ScanKey() {
       //           var value = $get("< %= ScanKey.ClientID %>").value;
       //           return value;
       //       }
       //       function IMEICount() {
       //           var value = $get("< %= txtPMCount.ClientID %>").value;
       //           return value;
       //       }
       //       function IMEIErrorCount() {
       //           var value = $get("< %= txtPMCountError.ClientID %>").value;
       //           return value;
       //       }

       //       function Batch() {
       //           var value = $get("< %= txtBatch.ClientID %>").value;
       //           return value;
       //       }

       //       function Site() {
       //           var IndexValue = $get("< %= drpIFSSite.ClientID %>").selectedIndex;
       //           var value = $get("< %= drpIFSSite.ClientID %>").options[IndexValue].text;
       //           return value;
       //       }
       //       function Project() {
       //           var IndexValue = $get("< %= drpIFSProject.ClientID %>").selectedIndex;
       //           var value = $get("< %= drpIFSProject.ClientID %>").options[IndexValue].value;
       //           return value;
       //       }

       //       function Location() {
       //           var value = $get("< %= txtIFSLocation.ClientID %>").value;
       //           return value;
       //       }

       //       function LocationID() {
       //           return -1;
       //       }

       //       function Condition() {
       //           var IndexValue = $get("< %= drpIFSCondition.ClientID %>").selectedIndex;
       //           var value = $get("< %= drpIFSCondition.ClientID %>").options[IndexValue].text;
       //           return value;
       //       }
       //       function ConditionID() {
       //           var IndexValue = $get("< %= drpIFSCondition.ClientID %>").selectedIndex;
       //           var value = $get("< %= drpIFSCondition.ClientID %>").options[IndexValue].value;
       //           return value;
       //       }
       ////       function Grade() {
       ////           var IndexValue = $get("< %= drpGrade.ClientID %>").selectedIndex;
       ////           var value = $get("< %= drpGrade.ClientID %>").options[IndexValue].text;
       ////           return value;
       ////       }
       ////       

       //       function UpdateIMEI() {
       //           return $get("< %= chkUpdateIMEI.ClientID %>").checked;
       //       }
       //       function AutoReturn() {
       //           return $get("< %= chkAutoReturn.ClientID %>").checked;
       //       }


       //       function Kitted() {
       //           return $get("< %= chkKitted.ClientID %>").checked;
       //       }
       //       function Unlocked() {
       //           return $get("< %= chkUnlocked.ClientID %>").checked;
       //       }


       //       function beep() {
       //           var snd = new Audio("data:audio/wav;base64,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qJrx4KOQjahZxWbcZgztj2c49nKmkId44S71j0c8eV9yDK6uPRzx5X18eDvjvQ6yKo9ZSS6l//8elePK/Lf//IInrOF/FvDoADYAGBMGb7FtErm5MXMlmPAJQVgWta7Zx2go+8xJ0UiCb8LHHdftWyLJE0QIAIsI+UbXu67dZMjmgDGCGl1H+vpF4NSDckSIkk7Vd+sxEhBQMRU8j/12UIRhzSaUdQ+rQU5kGeFxm+hb1oh6pWWmv3uvmReDl0UnvtapVaIzo1jZbf/pD6ElLqSX+rUmOQNpJFa/r+sa4e/pBlAABoAAAAA3CUgShLdGIxsY7AUABPRrgCABdDuQ5GC7DqPQCgbbJUAoRSUj+NIEig0YfyWUho1VBBBA//uQZB4ABZx5zfMakeAAAAmwAAAAF5F3P0w9GtAAACfAAAAAwLhMDmAYWMgVEG1U0FIGCBgXBXAtfMH10000EEEEEECUBYln03TTTdNBDZopopYvrTTdNa325mImNg3TTPV9q3pmY0xoO6bv3r00y+IDGid/9aaaZTGMuj9mpu9Mpio1dXrr5HERTZSmqU36A3CumzN/9Robv/Xx4v9ijkSRSNLQhAWumap82WRSBUqXStV/YcS+XVLnSS+WLDroqArFkMEsAS+eWmrUzrO0oEmE40RlMZ5+ODIkAyKAGUwZ3mVKmcamcJnMW26MRPgUw6j+LkhyHGVGYjSUUKNpuJUQoOIAyDvEyG8S5yfK6dhZc0Tx1KI/gviKL6qvvFs1+bWtaz58uUNnryq6kt5RzOCkPWlVqVX2a/EEBUdU1KrXLf40GoiiFXK///qpoiDXrOgqDR38JB0bw7SoL+ZB9o1RCkQjQ2CBYZKd/+VJxZRRZlqSkKiws0WFxUyCwsKiMy7hUVFhIaCrNQsKkTIsLivwKKigsj8XYlwt/WKi2N4d//uQRCSAAjURNIHpMZBGYiaQPSYyAAABLAAAAAAAACWAAAAApUF/Mg+0aohSIRobBAsMlO//Kk4soosy1JSFRYWaLC4qZBYWFRGZdwqKiwkNBVmoWFSJkWFxX4FFRQWR+LsS4W/rFRb/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////VEFHAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAU291bmRib3kuZGUAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAMjAwNGh0dHA6Ly93d3cuc291bmRib3kuZGUAAAAAAAAAACU=");
       //           snd.play();
       //       }


       //-----------------------------------------------------------------------------------------------

       function SetupDropDown(DropDownName) {


           if (DropDownName == 'Lab Destination:' + DropDownName) {

           }
           else {

               FillDropDown(DropDownName);
           }
           return;
       }

       function FillDropDown(DropDownName) {



           var service = new WebServer_01();
           if (DropDownName == 'Carrier') {
               var x = $get("<%= hdnCarrierID.ClientID %>").value;

               if (x == null || x.length == 0) { return; }
               var ctr = $get($get("<%= hdnCarrierID.ClientID %>").value);
               if (ctr == null) { return; }
               var rValue = service.GetManufacturerDropDownData(GetDropDownValue($get("<%= hdnCarrierID.ClientID %>").value), UserName(), onFillManufacturerList, onFillManufacturerListError, null);
               return;
           }
           if (DropDownName == 'Manufacturer') {
               return;
               var x = $get("<%= hdnCarrierID.ClientID %>").value;
               if (x == null || x.length == 0) { return; }

               var ctr = $get($get("<%= hdnCarrierID.ClientID %>").value);
               if (ctr == null) { return; }
               ctr = $get($get("<%= hdnManufacturerID.ClientID %>").value);
               if (ctr == null) { return; }
               var rValue = service.GetModelDropDownData(GetDropDownValue($get("<%= hdnCarrierID.ClientID %>").value), GetDropDownValue($get("<%= hdnManufacturerID.ClientID %>").value), UserName(), onFillModelList, null, null);
               return;
           }
           if (DropDownName == 'Model') {
               var x = $get("<%= hdnCarrierID.ClientID %>").value;
               if (x == null || x.length == 0) { return; }
               var ctr = $get($get("<%= hdnCarrierID.ClientID %>").value);
               if (ctr == null) { return; }
               ctr = $get($get("<%= hdnManufacturerID.ClientID %>").value);
               if (ctr == null) { return; }
               ctr = $get($get("<%= hdnModelID.ClientID %>").value);
               if (ctr == null) { return; }
               var rValue = service.GetColourDropDownData(GetDropDownValue($get("<%= hdnCarrierID.ClientID %>").value), GetDropDownValue($get("<%= hdnManufacturerID.ClientID %>").value), GetDropDownValue($get("<%= hdnModelID.ClientID %>").value), UserName(), onFillColourList, null, null);
               return;
           }
       }

       function onFillManufacturerListError(Result) {
           alert('Error - onFillManufacturerListError:' + Result);
       }

       function onFillManufacturerList(Result) {


           //           if (MCL('hdnisMasterLinked').value != 'True') { return; }
           //           alert("FillManufacturerList:" + Result);
           var DropDown = $get($get("<%= hdnManufacturerID.ClientID %>").value);
           if (DropDown != null) {
               var CurrentValue = GetDropDownValue($get("<%= hdnManufacturerID.ClientID %>").value);
               while (DropDown.options.length > 0) DropDown.remove(0);
               if (Result.length > 0) {
                   ClientData = eval('({' + Result + '})');
                   for (var key in ClientData) {
                       var attrName = key;
                       var attrValue = ClientData[key];
                       addOption(DropDown, key, ClientData[key], CurrentValue)
                   }
               }
           }
           FillDropDown('Manufacturer');
           return;
       }


       function onFillModelList(Result) {
           //           if (MCL('hdnisMasterLinked').value != 'True') { return; }
           var DropDown = $get($get("<%= hdnModelID.ClientID %>").value)
           if (DropDown != null) {
               var CurrentValue = GetDropDownValue($get("<%= hdnModelID.ClientID %>").value);
               while (DropDown.options.length > 0) DropDown.remove(0);
               if (Result.length > 0) {
                   ClientData = eval('({' + Result + '})');
                   for (var key in ClientData) {
                       var attrName = key;
                       var attrValue = ClientData[key];
                       addOption(DropDown, key, ClientData[key], CurrentValue)
                   }
               }
           }
           FillDropDown('Model');
           return;
       }

       function onFillColourList(Result) {
           //           if (MCL('hdnisMasterLinked').value != 'True') { return; }
           var DropDown = $get($get("<%= hdnColourID.ClientID %>").value)
           if (DropDown != null) {
               var CurrentValue = GetDropDownValue($get("<%= hdnColourID.ClientID %>").value);
               while (DropDown.options.length > 0) DropDown.remove(0);
               if (Result.length > 0) {
                   ClientData = eval('({' + Result + '})');
                   for (var key in ClientData) {
                       var attrName = key;
                       var attrValue = ClientData[key];
                       addOption(DropDown, key, ClientData[key], CurrentValue)
                   }
               }
           }
           return;
       }

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

















       

    </script>
 </asp:Content>
