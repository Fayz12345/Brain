<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CycleCount.aspx.cs" Inherits="BW_WebApp.Utility.CycleCount" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="cMC" runat="server">
    <asp:HiddenField ID="hdnUserName" runat="server" ClientIDMode="Static" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <h1>Cycle Counts</h1>
            <asp:Label ID="lblWarningMessage" runat="server" />
            
            <asp:TabContainer runat="server" ID="tabMain" CssClass="tab-container" ActiveTabIndex="0" AutoPostBack="False">
                <asp:TabPanel runat="server" ID="TabPanelNew" CssClass="tab-panel" Enabled="true" HeaderText="Cycle Conditions" Visible="True">
                    <ContentTemplate>
                        <table class="table" runat="server">
                            <tr>
                                <th>Include</th>
                                <th>Count</th>
                                <th>Type</th>
                                <th>Assumed</th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="InlcudeLocation" runat="server" />
                                </td>
                                <td>
                                    <asp:Label ID="IncludeCountLocation" runat="server" Text="0" />
                                </td>
                                <td>
                                    Location
                                </td>
                                <td>
                                    <div class="mh-200px overflow">
                                        <asp:CheckBoxList ID="chkLocation" CssClass="checklist-inline" runat="server" RepeatLayout="UnorderedList" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="IncludeSKU" runat="server" />
                                </td>
                                <td>
                                    <asp:Label ID="IncludeCountSKU" runat="server" Text="0" />
                                </td>
                                <td>
                                    SKU
                                </td>
                                <td>
                                    <div class="mh-200px overflow">
                                        <asp:CheckBoxList ID="chkSKU" CssClass="checklist-inline" runat="server" RepeatLayout="UnorderedList" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="IncludeCondition" runat="server" />
                                </td>
                                <td>
                                    <asp:Label ID="IncludeCountCondition" runat="server" Text="0" />
                                </td>
                                <td>
                                    Condition
                                </td>
                                <td>
                                    <div class="mh-200px overflow">
                                        <asp:CheckBoxList ID="chkCondition" CssClass="checklist-inline" runat="server" RepeatLayout="UnorderedList" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="IncludeManufacturer" runat="server" />
                                </td>
                                <td>
                                    <asp:Label ID="IncludeCountManufacturer" runat="server" Text="0" />
                                </td>
                                <td>
                                    Manufacturer
                                </td>
                                <td>
                                    <div class="mh-200px overflow">
                                        <asp:CheckBoxList ID="chkManufacturer" CssClass="checklist-inline" runat="server" RepeatLayout="UnorderedList" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="IncludeModel" runat="server" />
                                </td>
                                <td>
                                    <asp:Label ID="IncludeCountModel" runat="server" Text="0" />
                                </td>
                                <td>
                                    Model
                                </td>
                                <td>
                                    <div class="mh-200px overflow">
                                        <asp:CheckBoxList ID="chkModel" CssClass="checklist-inline" runat="server" RepeatLayout="UnorderedList" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="IncludeCarrier" runat="server" />
                                </td>
                                <td>
                                    <asp:Label ID="IncludeCountCarrier" runat="server" Text="0" />
                                </td>
                                <td>
                                    Carrier
                                </td>
                                <td>
                                    <div class="mh-200px overflow">
                                        <asp:CheckBoxList ID="chkCarrier" CssClass="checklist-inline" runat="server" RepeatLayout="UnorderedList" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="IncludeColour" runat="server" />
                                </td>
                                <td>
                                    <asp:Label ID="IncludeCountColour" runat="server" Text="0" />
                                </td>
                                <td>
                                    Colour
                                </td>
                                <td>
                                    <div class="mh-200px overflow">
                                        <asp:CheckBoxList ID="chkColour" CssClass="checklist-inline" runat="server" RepeatLayout="UnorderedList" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:TabPanel>

                <asp:TabPanel runat="server" ID="TabPanel1" CssClass="tab-panel" Enabled="true" HeaderText="Device Scan" Visible="True">
                    <ContentTemplate>
                        <div class="row">
                        	<div class="col-md">
                                <div>
                                    <asp:Button ID="btnNewScan" runat="server" Text="New" ToolTip="Start New Scan Cycle" />
                                    <asp:Button ID="btnDoneScan" runat="server" Text="Done" ToolTip="Done Scan Cycle"  />
                                    <asp:Button ID="btnReport" runat="server" Text="Report" ToolTip="Report Scan Cycle"  />
                                </div>
                        
                                <label>IFS Location Card Number:</label>
                                <asp:TextBox ID="IFSLocationCardNumber" runat="server" ClientIDMode="Static" />
                        
                                <label>Cycle Count:</label>
                                <asp:TextBox ID="TextBox2" runat="server" ClientIDMode="Static" />
                        
                                <label>IMEI/ESN:</label>
                                <asp:TextBox ID="txtESN" runat="server" ClientIDMode="Static" />
                            </div>
                            <div class="col-md">
                                <asp:TextBox ID="txtCount" runat="server" ClientIDMode="Static" Enabled="False" Text="0" ToolTip="Count" />
                                <asp:ListBox ID="lstHistory" runat="server" SelectionMode="Single" ViewStateMode="Inherit" ClientIDMode="Static" />
                            </div>
                        </div>

                    </ContentTemplate>
                </asp:TabPanel>

                <%--<asp:TabPanel runat="server" ID="TabPanel2" CssClass="tab-panel" Enabled="true" HeaderText="Summary" Visible="True">
                    <ContentTemplate></ContentTemplate>
                </asp:TabPanel>--%>

                <%--<asp:TabPanel runat="server" ID="TabPanel3" CssClass="tab-panel" Enabled="true" HeaderText="Manager Scan" Visible="True">
                    <ContentTemplate></ContentTemplate>
                </asp:TabPanel>--%>

            </asp:TabContainer>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ContentPlaceHolderID="js" runat="server">
    <script type="text/javascript">

        // Create a stopwatch "class."
        StopWatch = function () {
            this.StartMilliseconds = 0;
            this.ElapsedMilliseconds = 0;
        };

        StopWatch.prototype.Start = function () { this.StartMilliseconds = new Date().getTime(); };
        StopWatch.prototype.Stop = function () {
            this.ElapsedMilliseconds = new Date().getTime() - this.StartMilliseconds;
        };

        var Timer1 = new StopWatch();

        // SCANKEY PROCESSING GOES HERE ----------------------------------------<
        function RecordScanKey(pText) {

            // alert("HERE YOU ARE");

            // if (WayBill().length == 0) { SetWayBillFocus(); return false; }
            if (ESNNumber().length == 0) { return false; }
            RecordDetail();
            SetESNFocus();
            return false;
        }

        function RecordDetail() {
            var service = new WebServer_01();
            var esn = ESNNumber();
            //           var waybill = WayBill();
            //           var MasterClient = 'Master Client';
            //           var MasterClientID = 'ClientID';
            //           var IndexValue = $get("< %= drpMasterClient.ClientID %>").selectedIndex;
            //           MasterClient = $get("< %= drpMasterClient.ClientID %>").options[IndexValue].text;
            //           MasterClientID = $get("< %= drpMasterClient.ClientID %>").options[IndexValue].value;


            Timer1.Start();
            $get("<%= txtESN.ClientID %>").value = "";
            service.CycleCountIMEI('66', esn, UserName(), OnAddSuccess, null, null);
        }

        function OnAddSuccess(result) {
            //           if (WayBill().length == 0) { SetWayBillFocus(); return false; }
            //           if (ESNNumber().length == 0) { return false; }

            Timer1.Stop();
            var count = $get("<%= txtCount.ClientID %>").value;
            count++;
            $get("<%= txtCount.ClientID %>").value = count;

            //lblCount

            var LineText = result;
            RecordHistory(LineText);
            SetESNFocus();
        }

        //       function CleanData() {
        //           var p = ParseValue();
        //           if (p == 0) {
        //               var waybill = WayBill();
        //               if (waybill.length > 22) {
        //                   waybill = waybill.slice(11, -11);
        //                   SetWayBill(waybill);
        //               }
        //           }
        //       }

        function RecordHistory(Value) {
            var Source = $get("<%= lstHistory.ClientID %>");
            // Check to see if the item is already there.
            var xc = Source.getElementsByTagName('option').length;
            for (var i = 0; i < xc; i++) {
                if (Source.options[i].value == Value)
                    return;
            }
            //           var Count = MCL("txtHistoryCount");
            //           var count = Count.value;
            //           count++
            //           Count.value = count.toString();
            if (Source != null) {
                var newOption = new Option(); // Create a new instance of ListItem
                newOption.text = Value + ' (' + Timer1.ElapsedMilliseconds + 'ms)';
                Source.options[Source.length] = newOption; //Append the item in Target
            }
        }

        function IsMessage() {
            if (LastWayBill() != WayBill()) {
                CleanData();
                var service = new WebServer_01();
                $get("<%= lblWarningMessage.ClientID %>").innerHTML = '';
                service.WayBillMessageQueue(WayBill(), UserName(), SeeifMessage, null, null);

                // SeeifMessage();
            }
        }

        function SeeifMessage(result) {
            $get("<%= lblWarningMessage.ClientID %>").innerHTML = result;
            SetLastWayBill();
        }

        //       // Variable Functions ----------------------------------
        //       function ParseValue() {
        //           var list = $get("< %= ParseValue.ClientID %>"); //Client ID of the radiolist
        //           var inputs = list.getElementsByTagName("input");
        //           var selected;
        //           for (var i = 0; i < inputs.length; i++) {
        //               if (inputs[i].checked) {
        //                   selected = inputs[i];
        //                   break;
        //               }
        //           }
        //           if (selected) {
        //               return selected.value
        //           }
        //           return -1;
        //       }

        function UserName() {
            return $get("<%= hdnUserName.ClientID %>").value;
        }

        //       function WayBill() {
        //           return $get("< %= txtWaybill.ClientID %>").value;
        //       }

        //       function SetWayBill(Value) {
        //           $get("< %= txtWaybill.ClientID %>").value = Value;
        //       }

        //       function LastWayBill() {
        //           return $get("< %= lastWayBill.ClientID %>").value;
        //       }
        //       function SetLastWayBill() {
        //           $get("< %= lastWayBill.ClientID %>").value = WayBill();
        //       }

        function ESNNumber() {
            return $get("<%= txtESN.ClientID %>").value;
        }

        //       // Set Focus Functions ---------------------------------
        //       function SetWayBillFocus() {
        //           SetFocus("< %= txtWaybill.ClientID %>");
        //           return;
        //       }

        function SetESNFocus() {
            SetFocus("<%= txtESN.ClientID %>");
            return;
        }

        function SetFocus(Name) {
            $get(Name).focus();
            return;
        }

        // Other functions
        // If the element's string matches the regular expression it is all numbers
        function isNumeric(elem, helperMsg) {
            var numericExpression = /^[0-9]+$/;
            if (elem.value.match(numericExpression)) {
                return true;
            } else {
                //alert(helperMsg);
                //elem.focus();
                return false;
            }
        }

    </script>
</asp:Content>




