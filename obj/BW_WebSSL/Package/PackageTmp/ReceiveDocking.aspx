<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReceiveDocking.aspx.cs" Inherits="BW_WebApp.ReceiveDocking" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="cMC" runat="server">
    <asp:HiddenField ID="hdnUserName" runat="server" ClientIDMode="Static" />
    <h1>Receive Docking</h1>
    <div class="row">
        <div class="col-md">
            <label>Inbound Waybill Number:</label>
            <asp:TextBox ID="txtWaybill" runat="server" ClientIDMode="Static" />
    
            <label>Count:</label>
            <asp:TextBox ID="txtCount" runat="server" ClientIDMode="Static" />
    
            <asp:Button ID="RC" runat="server" Visible="False" />
        </div>
        <div class="col-md">
            <asp:ListBox ID="lstHistory" runat="server" SelectionMode="Single" ViewStateMode="Inherit" ClientIDMode="Static" />
        </div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="js" runat="server">
    <script type="text/javascript">

        // SCANKEY PROCESSING GOES HERE ----------------------------------------<
        function RecordScanKey(pText) {

            //           alert("HERE YOU ARE");

            if (WayBill().length == 0) { SetWayBillFocus(); return false; }
            if (WayBillCount().length == 0) { return false; }
            if (isNumeric($get("<%= txtCount.ClientID %>"), "") == false) { $get("<%= txtCount.ClientID %>").value = ""; return false; }
            RecordDetail();
            SetWayBillFocus();
        }

        function RecordDetail() {
            var service = new WebServer_01();
            service.CountAttribute("Dealer Waybill", WayBill(), WayBillCount(), UserName(), "1", OnCountSuccess, null, null);
        }

        function OnCountSuccess(result) {
            if (WayBill().length == 0) { SetWayBillFocus(); return false; }
            if (WayBillCount().length == 0) { return false; }
            if (isNumeric($get("<%= txtCount.ClientID %>"), "") == false) { $get("<%= txtCount.ClientID %>").value = ""; return false; }
            var LineText = "wb:" + WayBill() + " c:" + WayBillCount() + " a:" + result;
            RecordHistory(LineText);
            $get("<%= txtWaybill.ClientID %>").value = "";
            $get("<%= txtCount.ClientID %>").value = "";
        }

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
                newOption.text = Value;
                Source.options[Source.length] = newOption; //Append the item in Target
            }
        }

        // Variable Functions ----------------------------------
        function UserName() {
            return $get("<%= hdnUserName.ClientID %>").value;
        }

        function WayBill() {
            return $get("<%= txtWaybill.ClientID %>").value;
        }

        function WayBillCount() {
            return $get("<%= txtCount.ClientID %>").value;
        }

        // Set Focus Functions ---------------------------------
        function SetWayBillFocus() {
            SetFocus("<%= txtWaybill.ClientID %>");
            return;
        }

        function SetCountFocus() {
            SetFocus("<%= txtCount.ClientID %>");
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

