<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReceiveDetailAuthorization.aspx.cs" Inherits="BW_WebApp.ReceiveDetailAuthorization" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="cMC" runat="server">
    <asp:HiddenField ID="hdnUserName" runat="server" ClientIDMode="Static" />
    <h1>Repair Authorization</h1>
    <div class="row">
    	<div class="col-md-6">
            <label>Repair Authorization Number:</label>
            <asp:TextBox ID="txtWaybill" runat="server" ClientIDMode="Static" />

            <%--<label>Count:</label>
            <asp:TextBox ID="txtCount" runat="server" ClientIDMode="Static" />--%>

            <asp:ListBox ID="lstHistory" runat="server" SelectionMode="Single" ViewStateMode="Inherit" ClientIDMode="Static" />
            <asp:Button ID="RC" runat="server" Visible="False" />
        </div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="js" runat="server">
    <script type="text/javascript">

        // SCANKEY PROCESSING GOES HERE ----------------------------------------<
        function RecordScanKey(pText) {

            //           alert("HERE YOU ARE");

            if (WayBill().length == 0) {
                SetWayBillFocus();
                return false;
            }
            RecordDetail();
            SetWayBillFocus();
        }

        function RecordDetail() {
            var service = new WebServer_01();
            if (WayBill().length == 0) { SetWayBillFocus(); return false; }
            var AuthorizationNumber = WayBill();
            AuthorizationNumber = AuthorizationNumber.substr(5);
            var keys = AuthorizationNumber.split(":");
            //           alert("xx:" + AuthorizationNumber);
            //           alert("0:" + keys[0]);
            //           alert("1:" + keys[1]);

            service.AuthorizeAuthorization(keys[0], keys[1], UserName(), OnCountSuccess, onWebServerError, null);
            // service.AuthorizeRepair(keys[0], keys[1], UserName(), OnCountSuccess, null, null);
        }

        function OnCountSuccess(result) {
            if (WayBill().length == 0) { SetWayBillFocus(); return false; }
            var LineText = result;
            RecordHistory(LineText);
            $get("<%= txtWaybill.ClientID %>").value = "";
        }

        function onWebServerError(Result) {
            alert('Error:' + Result.get_message());
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


        // Set Focus Functions ---------------------------------
        function SetWayBillFocus() {
            SetFocus("<%= txtWaybill.ClientID %>");
            return;
        }

        function SetWayBillBlur() {
            SetFocus("<%= lstHistory.ClientID %>");
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

