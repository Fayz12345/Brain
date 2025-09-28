<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReceiveWaybill02.aspx.cs" Inherits="BW_WebApp.ReceiveWaybill02" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="cMC" runat="server">
    <asp:HiddenField ID="hdnUserName" runat="server" ClientIDMode="Static" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdnClientID" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnClientName" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnAttributType" runat="server" ClientIDMode="Static" />
            <h1>Pre-receive System</h1>
            <asp:Label ID="lblWarningMessage" runat="server" />
           
            <div class="row">
                <div class="col-md-4">
                    <asp:Label ID="Label2" runat="server" />
                    <label>Courier:</label>
                    <asp:RadioButtonList ID="ParseValue" CssClass="radiolist-inline" runat="server" RepeatLayout="UnorderedList">
                        <asp:ListItem Selected="True" Text="Purolator" Value="0" />
                        <asp:ListItem Text="Other" Value="1" />
                    </asp:RadioButtonList>
            
                    <label>Unit Type:</label>
                    <asp:DropDownList ID="drpMasterUnitType" runat="server" AutoPostBack="True">
                        <asp:ListItem Text="All" Value="0" />
                        <asp:ListItem Text="EMP" Value="1" />
                        <asp:ListItem Text="A001" Value="2" />
                        <asp:ListItem Text="A002" Value="3" />
                    </asp:DropDownList>
            
                    <label>Client:</label>
                    <div class="form-control bg-light">
                        <asp:Label ID="lblCompanyName" runat="server" Text="Company Name" />
                    </div>
            
                    <label>IDC Bin:</label>
                    <asp:DropDownList ID="drpIDCBinList" runat="server">
                        <asp:ListItem Text="All" Value="0" />
                        <asp:ListItem Text="EMP" Value="1" />
                        <asp:ListItem Text="A001" Value="2" />
                        <asp:ListItem Text="A002" Value="3" />
                    </asp:DropDownList>

                    <label>Dealer Waybill:</label>
                    <asp:HiddenField ID="lastWayBill" runat="server" />
                    <asp:TextBox ID="txtWaybill" runat="server" ClientIDMode="Static" />
            
                    <label>IMEI/ESN:</label>
                    <asp:TextBox ID="txtESN" runat="server" ClientIDMode="Static" />
                </div>

                <div class="col-md-4">
                    <asp:Label ID="Label1" runat="server" Text="Entry History:" />
                    
                    <div class="input-group">
                        <asp:TextBox ID="txtCount" runat="server" ClientIDMode="Static" Enabled="False" Text="0" ToolTip="Count" />
                        <div class="input-group-append">
                            <asp:Button ID="btnClear" runat="server" Text="Clear" UseSubmitBehavior="False" />
                        </div>
                    </div>
                    
                    <asp:ListBox ID="lstHistory" CssClass="h-100" runat="server" SelectionMode="Single" ViewStateMode="Inherit" ClientIDMode="Static" />
                </div>

                <div class="col-md-4">
                    <asp:Label ID="lblBinReport" runat="server" Text="IDC Bin Report:" />

                    <div class="input-group">
                        <asp:TextBox ID="txtBinCount" runat="server" ClientIDMode="Static" Enabled="False" Text="0" ToolTip="Count" />
                        <div class="input-group-append">
                            <asp:Button ID="btnRefreshBinList" runat="server" Text="Refresh" UseSubmitBehavior="False" />
                        </div>
                    </div>

                    <asp:ListBox ID="lstBinReportList" CssClass="h-100" runat="server" SelectionMode="Single" ViewStateMode="Inherit" ClientIDMode="Static" />
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
             //           alert("HERE YOU ARE:" + WayBill() + ":" + ESNNumber() + ":" + IDCBIN() + ":" );
             if (WayBill().length == 0) { SetWayBillFocus(); return false; }
             if (ESNNumber().length == 0) { SetESNFocus(); return false; }
             if (IDCBIN().length == 0) { SetIDCBINFocus(); return false; }
             //           alert("HERE YOU ARE");
             RecordDetail();
             return false;
         }


         function RecordDetail() {
             var service = new WebServer_01();
             var esn = ESNNumber();
             var waybill = WayBill();
             var MasterClient = 'Master Client';
             var MasterClientID = 'ClientID';
             var IDCBin = IDCBIN();
             var IDCProductType = IDCPRODUCTTYPE();
             if (IDCBin == "* Select *") {
                 RecordHistory("No Bin Selected! DATA NOT SAVED");
                 return;
             }
             MasterClient = $get("<%= hdnClientName.ClientID %>").value;
             MasterClientID = $get("<%= hdnClientID.ClientID %>").value;
             Timer1.Start();
             $get("<%= txtESN.ClientID %>").value = "";
             service.SaveReceiveWayBill_IDC(esn, IDCBin, waybill, MasterClientID, MasterClient, 'Courier', IDCProductType, UserName(), OnAddSuccess, null, null);
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
             //alert('Set ESN Focus');
             SetESNFocus();
         }



         function CleanData() {
             var p = ParseValue();
             if (p == 0) {
                 var waybill = WayBill();
                 if (waybill.length > 22) {
                     waybill = waybill.slice(11, -11);
                     SetWayBill(waybill);
                 }
             }
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
                 newOption.text = Value + ' (' + Timer1.ElapsedMilliseconds + 'ms)';
                 Source.options[Source.length] = newOption; //Append the item in Target
             }
         }


         function IsMessage() {
             if (LastWayBill() != WayBill()) {
                 CleanData();
                 alert("Inside looking for any message");
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



         // Variable Functions ----------------------------------
         function ParseValue() {
             var list = $get("<%= ParseValue.ClientID %>"); //Client ID of the radiolist
             var inputs = list.getElementsByTagName("input");
             var selected;
             for (var i = 0; i < inputs.length; i++) {
                 if (inputs[i].checked) {
                     selected = inputs[i];
                     break;
                 }
             }
             if (selected) {
                 return selected.value
             }
             return -1;
         }


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
         function IDCBIN() {
             var IndexValue = $get("<%= drpIDCBinList.ClientID %>").selectedIndex;
             var text = $get("<%= drpIDCBinList.ClientID %>").options[IndexValue].text;
             var dataString = $get("<%= drpIDCBinList.ClientID %>").options[IndexValue].value;
             var data = dataString.split(":");
             // in this case, the data we want to send is not the normal display or keyid value.
             return data[2];     //0=optionid,1=clientid,3=raw option text.
         }

         function IDCPRODUCTTYPE() {
             var IndexValue = $get("<%= drpMasterUnitType.ClientID %>").selectedIndex;
             return $get("<%= drpMasterUnitType.ClientID %>").options[IndexValue].text;
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
         function SetIDCBINFocus() {
             SetFocus("<%= drpIDCBinList.ClientID %>");
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



