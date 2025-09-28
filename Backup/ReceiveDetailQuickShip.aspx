<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReceiveDetailQuickShip.aspx.cs" Inherits="BW_WebApp.ReceiveDetailQuickShip" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="cMC" runat="server">
    <asp:HiddenField ID="hdnUserName" runat="server" ClientIDMode="Static" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <h1>Bulk Shipping</h1>
            <asp:Label ID="lblWarningMessage" CssClass="d-block" runat="server" />
            <div class="row">
            	<div class="col-md">
                    <label>Courier:</label>
                    <asp:DropDownList ID="drpCourier" runat="server" ToolTip="Carrier" />

                    <label>Waybill:</label>
                    <asp:HiddenField ID="lastWayBill" runat="server" />
                    <asp:TextBox ID="txtWaybill" runat="server" ClientIDMode="Static" />

                    <label>IMEI/ESN:</label>
                    <asp:TextBox ID="txtESN" runat="server" ClientIDMode="Static" />
            
                    <asp:Button ID="btnClear" runat="server" Text="Clear" />
                </div>
            	<div class="col-md">
                    <asp:TextBox ID="txtCount" runat="server" ClientIDMode="Static" Enabled="False" Text="0" ToolTip="Count" />
                    <asp:HiddenField ID="ESNList" runat="server" />
                    <asp:ListBox ID="lstHistory" runat="server" SelectionMode="Single" ViewStateMode="Inherit" ClientIDMode="Static" />
                </div>
            </div>
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

            //           alert("HERE YOU ARE");

            if (WayBill().length == 0) { SetWayBillFocus(); return false; }
            if (ESNNumber().length == 0) { return false; }

            RecordDetail();

            return false;
        }

        function RecordDetail() {
            var service = new WebServer_01();
            var esn = ESNNumber();
            var waybill = WayBill();
            Timer1.Start();

            var control = document.getElementById('<%= drpCourier.ClientID %>');
            var selectedvalue = control.options[control.selectedIndex].value;
            service.ShipUnit(esn, waybill, selectedvalue, UserName(), OnAddSuccess, null, null);
            // OnAddSuccess(esn)

        }

        function OnAddSuccess(result) {
            var n = result.indexOf("Error");
            Timer1.Stop();
            if (n >= 0) {
                alert(result);
            }
            RecordHistory(result);
            $get("<%= txtESN.ClientID %>").value = "";
            SetESNFocus();
        }

        function CleanData() {
            //           var p = ParseValue();
            //           if (p == 0) {
            //               var waybill = WayBill();
            //               if (waybill.length > 22) {
            //                   waybill = waybill.slice(11, -11);
            //                   SetWayBill(waybill);
            //               }
            //           }
        }

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
                newOption.text = Value + ' (' + Timer1.ElapsedMilliseconds + 'ms)';
                Source.options[Source.length] = newOption; //Append the item in Target


                var count = $get("<%= txtCount.ClientID %>").value;
                count++;
                $get("<%= txtCount.ClientID %>").value = count;


                $get("<%= ESNList.ClientID %>").value = $get("<%= ESNList.ClientID %>").value + Value + ',';

            }

            else { alert("ESN already on list!"); }
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
        //           var list = $get("%= drpCourier.ClientID %>"); //Client ID of the radiolist
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

        function WayBill() {
            return $get("<%= txtWaybill.ClientID %>").value;
        }

        function SetWayBill(Value) {
            $get("<%= txtWaybill.ClientID %>").value = Value;
        }

        function LastWayBill() {
            return $get("<%= lastWayBill.ClientID %>").value;
        }

        function SetLastWayBill() {
            $get("<%= lastWayBill.ClientID %>").value = WayBill();
        }

        function ESNNumber() {
            return $get("<%= txtESN.ClientID %>").value;
        }

        // Set Focus Functions ---------------------------------
        function SetWayBillFocus() {
            SetFocus("<%= txtWaybill.ClientID %>");
            return;
        }

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


