<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DevicePI.aspx.cs" Inherits="BW_WebApp.LocationManagement.DevicePI" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cMC" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            
            <asp:HiddenField ID="hdnUserName" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnCarrierID" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnManufacturerID" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnModelID" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnColourID" runat="server" ClientIDMode="Static" />
            <asp:TabContainer ID="TabContainer1" runat="server" CssClass="tab-container">
                <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="Device Physical Inventory Count" CssClass="tab-panel">
                    <ContentTemplate>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                            <asp:HiddenField ID="hdnBatchNumber" runat="server" ClientIDMode="Static" />
                                <table id="Table3" runat="server" width="100%">
                                    <tr>
                                        <td align="left" valign="top">
                                            <h1>
                                                Device Physical Inventory Count </h1>
                                        </td>
                                        <td align="right" valign="top">
<%--                                            <asp:CheckBox ID="chkUpdateIMEI" runat="server" Text="Update IMEI" TextAlign="right"
                                                Enabled="False" Checked="True" />
                                            <br />--%>
                                            <asp:Button ID="btnLockBatch" runat="server" Text="Lock Batch" Visible="False" UseSubmitBehavior="False" />
                                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnLockBatch"
                                                ConfirmText="Are you sure you want to Lock this batch?">
                                            </asp:ConfirmButtonExtender>
                                        </td>
                                    </tr>
                                </table>
                                <table id="Table2" runat="server" width="100%">
                                    <tr>
                                        <td align="left" valign="bottom" width="20%">
<%--                                            <asp:CheckBox ID="chkKitted" runat="server" Text="Kitted" />
                                            <asp:CheckBox ID="chkUnlocked" runat="server" Text="Unlocked" /><br />--%>
                                            <%--Batch--%>

                                        </td>
                                        <td align="left" valign="bottom" width="20%">

                                        </td>
                                        <td align="left" valign="bottom" width="20%">

                                        </td>
                                        <td align="left" valign="bottom">

                                        </td>
                                        <td align="left" valign="bottom">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtBatch" runat="server" ToolTip="Batch." MaxLength="25" Enabled="False" Visible="True"></asp:TextBox>
                                        </td>
                                        <td align="left" valign="top">
<%--                                            <asp:DropDownList ID="drpSite" runat="server">
                                            </asp:DropDownList>--%>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:DropDownList ID="drpProject" runat="server" ToolTip="Project">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left" valign="top">
<%--                                            <asp:DropDownList ID="drpCondition" runat="server">
                                            </asp:DropDownList>--%>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:Button ID="btnResetCount" runat="server" Text="Reset Counter" UseSubmitBehavior="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:Label ID="Label1" runat="server" Text="Location" AssociatedControlID="ScanKey"></asp:Label>
                                            <br />
                                            <asp:TextBox ID="txtLocation" runat="server" BackColor="#A5EBC5" ForeColor="Black"
                                                Font-Size="Larger" MaxLength="20" Visible="True" Text=""></asp:TextBox>
                                        </td>
                                        <td align="left" valign="top" colspan="2">
                                            <asp:Label ID="lblScanFieldText" runat="server" Text="Scan IMEI" AssociatedControlID="ScanKey"></asp:Label>
                                            <br />
                                            <asp:TextBox ID="ScanKey" runat="server" BackColor="#A5EBC5" ForeColor="Black" Font-Size="X-Large"
                                                MaxLength="50"></asp:TextBox>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:CheckBox ID="chkAutoReturn" runat="server" Text="Auto Go/Enter" TextAlign="right"
                                                AutoPostBack="True" />
                                            <br />
                                            <asp:Button ID="btnPICRecord" runat="server" Text="Go/Enter" OnClientClick="RecordScan(); return false;" UseSubmitBehavior="False" />
                                        </td>
                                        <td align="left" valign="top">
                                            Valid:<asp:TextBox ID="txtPMCount" runat="server" ToolTip="Valid IMEI Scans" Enabled="False"
                                                BorderStyle="None" ForeColor="Black" Text="0" Font-Size="X-Large"></asp:TextBox>
                                                <br />
                                            Error:<asp:TextBox ID="txtPMCountError" runat="server" ToolTip="Error Scans" Enabled="False"
                                                BorderStyle="None" ForeColor="Black" Text="0" Font-Size="X-Large"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="5">
                                            <asp:Label ID="lblPIMessage" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
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
               //               alert(DTE.value);
               $('#loading').show();
           }
       }

       function EndRequestHandler(sender, args) {
           $('#loading').hide();
       }


       //            ScanKey.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13) || (event.which == 9) || (event.keyCode == 9)) {RecordScanAuto();return false;}} else {return true}; ");
       //            ScanKey.Attributes.Add("onblur", "RecordScanAuto();return false;");

       function KeydownLocation() {
           //           alert('set scankey events');
           SetScanKeyEvents();
           ////           alert('set location events');
           SettxtLocationEvents();
       }

       function SetScanKeyEvents() {
           $("#<%= ScanKey.ClientID %>").keypress(function (e) {
               var evt = e ? e : event;
               var keyCode = evt.keyCode;
               if (keyCode == 13 || keyCode == 9) {
                   RecordScanAuto();
                   return false;
               }
           });
           //           alert('Setting the Scankey keypress');

           //           $("#<%= ScanKey.ClientID %>").onblur(function () {
           //               RecordScanAuto();
           //               return false;
           //           });
           //           alert('Setting the Scankey blur');
       }


       function SettxtLocationEvents() {

           $("#<%= txtLocation.ClientID %>").keypress(function (e) {
               var evt = e ? e : event;
               var keyCode = evt.keyCode;
               if (keyCode == 13 || keyCode == 9) {
                   $get("<%= ScanKey.ClientID %>").value = '';
                   $get("<%= ScanKey.ClientID %>").focus();
                   return false;
               }
           });
           //           alert('Setting the Location');
           //           $("#<%=txtLocation.ClientID %>").onblur(function () {
           //               $get("<%= ScanKey.ClientID %>").value = '';
           //               $get("<%= ScanKey.ClientID %>").focus();
           //               return false;
           //           });
           //           alert('Setting the Location');
       }



       function SetFocus(Name) {
           $get(Name).focus();
           return;
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
       function RecordScanAuto() {
           if (AutoReturn() == false) { return; }
           RecordScan();
       }

       function RecordScan() {
           var xESN = ESN();
           if (xESN.length == 0) { return; }
           var xBatch = Batch();
           var xUserName = UserName();

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
           var xESN = ESN();
           var xBatch = Batch();
           if (xESN.length == 0) { return; }
           var xUserName = UserName();

           var xLocationID = LocationID().toString(); ;
           var xConditionID = ConditionID().toString(); ;
           var xSite = Site();
           var xProject = Project();
           var xProjectID = ProjectID();
           var xLocation = Location();
           var xCondition = Condition();
           var xUpdateIMEI = UpdateIMEI();
           var sUpdateIMEI = '0';
           if (xUpdateIMEI == true) { sUpdateIMEI = '1'; }

           var xKitted = Kitted();
           var sKitted = '0';
           if (xKitted == true) { sKitted = '1'; }
           var xUnlocked = Unlocked();
           var sUnlocked = '0';
           if (xUnlocked == true) { sUnlocked = '1'; }
           var service = new WebServer_01();
           //           alert('Going In' + POReceiptDatex);
           service.LogPhysicalDeviceCount(xLocationID
                             , xConditionID
                             , xProjectID
                             , xESN
                             , xBatch
                             , xSite
                             , xProject.substr(0, 10)
                             , ''
                             , xLocation
                             , xCondition
                             , ''
                             , sKitted
                             , sUnlocked
                             , sUpdateIMEI
                             , xUserName, onSuccessDeviceRecorded, onServerError);
       }

       function onSuccessDeviceRecorded(result) {
           $get("<%= lblPIMessage.ClientID %>").innerHTML = result;
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
           $get("<%= ScanKey.ClientID %>").focus()
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

       function Site() {
           return '';
           //var IndexValue = $get("< %= drpSite.ClientID %>").selectedIndex;
           //var value = $get("< %= drpSite.ClientID %>").options[IndexValue].text;
           //return value;
       }
       function Project() {
          // return '';
           var IndexValue = $get("<%= drpProject.ClientID %>").selectedIndex;
           var value = $get("<%= drpProject.ClientID %>").options[IndexValue].text;
           return value;
       }
       function ProjectID() {
           // return '';
           var IndexValue = $get("<%= drpProject.ClientID %>").selectedIndex;
           var value = $get("<%= drpProject.ClientID %>").options[IndexValue].value;
           return value;
       }
       function Location() {
           var value = $get("<%= txtLocation.ClientID %>").value;
           return value;
       }

       function LocationID() {
           return -1;
       }

       function Condition() {
           return '';
           //var IndexValue = $get("< %= drpCondition.ClientID %>").selectedIndex;
           //var value = $get("< %= drpCondition.ClientID %>").options[IndexValue].text;
           //return value;
       }
       function ConditionID() {
           return '';
           //var IndexValue = $get("< %= drpCondition.ClientID %>").selectedIndex;
           //var value = $get("< %= drpCondition.ClientID %>").options[IndexValue].value;
           //return value;
       }
       //       function Grade() {
       //           var IndexValue = $get("< %= drpGrade.ClientID %>").selectedIndex;
       //           var value = $get("< %= drpGrade.ClientID %>").options[IndexValue].text;
       //           return value;
       //       }
       //       

       function UpdateIMEI() {
           return false;
           //return $get("< %= chkUpdateIMEI.ClientID %>").checked;
       }
       function AutoReturn() {
           return $get("<%= chkAutoReturn.ClientID %>").checked;
       }


       function Kitted() {
           return false;
           //return $get("< %= chkKitted.ClientID %>").checked;
       }
       function Unlocked() {
           return false;
           //return $get("< %= chkUnlocked.ClientID %>").checked;
       }


       //       function beep() {
       //           var snd = new Audio("data:audio/wav;base64,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qJrx4KOQjahZxWbcZgztj2c49nKmkId44S71j0c8eV9yDK6uPRzx5X18eDvjvQ6yKo9ZSS6l//8elePK/Lf//IInrOF/FvDoADYAGBMGb7FtErm5MXMlmPAJQVgWta7Zx2go+8xJ0UiCb8LHHdftWyLJE0QIAIsI+UbXu67dZMjmgDGCGl1H+vpF4NSDckSIkk7Vd+sxEhBQMRU8j/12UIRhzSaUdQ+rQU5kGeFxm+hb1oh6pWWmv3uvmReDl0UnvtapVaIzo1jZbf/pD6ElLqSX+rUmOQNpJFa/r+sa4e/pBlAABoAAAAA3CUgShLdGIxsY7AUABPRrgCABdDuQ5GC7DqPQCgbbJUAoRSUj+NIEig0YfyWUho1VBBBA//uQZB4ABZx5zfMakeAAAAmwAAAAF5F3P0w9GtAAACfAAAAAwLhMDmAYWMgVEG1U0FIGCBgXBXAtfMH10000EEEEEECUBYln03TTTdNBDZopopYvrTTdNa325mImNg3TTPV9q3pmY0xoO6bv3r00y+IDGid/9aaaZTGMuj9mpu9Mpio1dXrr5HERTZSmqU36A3CumzN/9Robv/Xx4v9ijkSRSNLQhAWumap82WRSBUqXStV/YcS+XVLnSS+WLDroqArFkMEsAS+eWmrUzrO0oEmE40RlMZ5+ODIkAyKAGUwZ3mVKmcamcJnMW26MRPgUw6j+LkhyHGVGYjSUUKNpuJUQoOIAyDvEyG8S5yfK6dhZc0Tx1KI/gviKL6qvvFs1+bWtaz58uUNnryq6kt5RzOCkPWlVqVX2a/EEBUdU1KrXLf40GoiiFXK///qpoiDXrOgqDR38JB0bw7SoL+ZB9o1RCkQjQ2CBYZKd/+VJxZRRZlqSkKiws0WFxUyCwsKiMy7hUVFhIaCrNQsKkTIsLivwKKigsj8XYlwt/WKi2N4d//uQRCSAAjURNIHpMZBGYiaQPSYyAAABLAAAAAAAACWAAAAApUF/Mg+0aohSIRobBAsMlO//Kk4soosy1JSFRYWaLC4qZBYWFRGZdwqKiwkNBVmoWFSJkWFxX4FFRQWR+LsS4W/rFRb/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////VEFHAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAU291bmRib3kuZGUAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAMjAwNGh0dHA6Ly93d3cuc291bmRib3kuZGUAAAAAAAAAACU=");
       //           snd.play();
       //       }


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

















       

    </script>
 </asp:Content>

