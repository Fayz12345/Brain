<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard_Dealer.aspx.cs" Inherits="BW_WebApp.Dashboard_Dealer" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="cMC" runat="server"> 
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            

            <asp:HiddenField runat="server" ID="hdnUserName" Value="" />
            <asp:HiddenField ID="hdnUserIPAddress" runat="server" Value=""/>

            <div class="row">
                <div class="col-md">
                    <asp:Image ID="imgGMPLogo" runat="server" ImageUrl="~/Images/Logo.jpg" />
                </div>
                <div class="col-md">
                    <h1><asp:Label ID="lblName" runat="server" Text="Dashboard - Dealer" /></h1>
                    <asp:DropDownList ID="drpClientLocations" runat="server" />
                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" />
                </div>
            </div>

            <asp:TabContainer runat="server" ID="t1x" CssClass="tab-container" ActiveTabIndex="0">
                <asp:TabPanel runat="server" ID="tb1x" CssClass="tab-panel" Enabled="true" HeaderText="Open">
                    <ContentTemplate>
                        <asp:Label ID="Label1" runat="server" Text="ESN/IMEI Records" />
                            <asp:GridView ID="gvDashboard" CssClass="table" runat="server" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="ServiceRequestNumber" HeaderText="Service Request #" />
                                <asp:BoundField DataField="ESN" HeaderText="ESN/IMEI" />
                                <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" />
                                <asp:BoundField DataField="UnitStatus" HeaderText="Unit Status" />
                                <asp:BoundField DataField="Status" HeaderText="Authorization" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="imgAuthorize" CssClass="btn btn-default" runat="server" ToolTip="Authorize/Decline">
                                            <span class="oi oi-task"></span>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="imgPrint" CssClass="btn btn-default" runat="server" ToolTip="Print">
                                            <span class="oi oi-print"></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Open">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="imgOpen" CssClass="btn btn-default" runat="server" ToolTip="Open Unit Summary">
                                            <span class="oi oi-info"></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel runat="server" ID="TabPanel1" CssClass="tab-panel" Enabled="true" HeaderText="Closed">
                    <ContentTemplate>
                        <asp:Label ID="Label3" runat="server" Text="ESN/IMEI Records" />
                        <asp:GridView ID="gvDashboardClosed" CssClass="table" runat="server" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="ServiceRequestNumber" HeaderText="Service Request #" />
                                <asp:BoundField DataField="ESN" HeaderText="ESN/IMEI" />
                                <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" />
                                <asp:BoundField DataField="UnitStatus" HeaderText="Unit Status" />
                                <asp:BoundField DataField="Status" HeaderText="Authorization" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="imgAuthorize" CssClass="btn btn-default" runat="server" ToolTip="Authorize/Decline">
                                            <span class="oi oi-task"></span>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="imgPrint" CssClass="btn btn-default" runat="server" ToolTip="Print">
                                            <span class="oi oi-print"></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Open">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="imgOpen" CssClass="btn btn-default" runat="server" ToolTip="Open Unit Summary">
                                            <span class="oi oi-info"></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>

            <div id="wndUnitSummary" class="modal" tabindex="-1" role="dialog">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">
                                <asp:Label ID="lblTitle" runat="server" Text="Unit Summary" />
                            </h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <asp:HiddenField ID="hdnReceiveDetailID" runat="server" />
                            <asp:HiddenField ID="hdnReceiveDetailAuthorizationLogID" runat="server" />

                            <div class="row">
                            	<div class="col-md">
                                    <label>Service Request #:</label>
                                    <%--<asp:TextBox ID="txtClientReference" runat="server" Enabled="False" />--%>
                                    <asp:TextBox ID="txtServiceRequestNumber" runat="server" Enabled="False" />
                            
                                    <label>Customer Name:</label>
                                    <asp:TextBox ID="txtCustomerName" runat="server" Enabled="False" />
                            
                                    <label>ESN/IMEI:</label>
                                    <asp:TextBox ID="txtESN" runat="server" Enabled="False" />
                            
                                    <label>Original IMEI:</label>
                                    <asp:TextBox ID="txtOriginalIMEI" runat="server" Enabled="False" />
                            
                                    <label>Warranty Type:</label>
                                    <asp:TextBox ID="txtWarrantyType" runat="server" Enabled="False" />
                            
                                    <label>First Fault:</label>
                                    <asp:TextBox ID="txtFaultCode" runat="server" Enabled="False" />

                                    <label>Second Fault:</label>
                                    <asp:TextBox ID="txtFaultCode2" runat="server" Enabled="False" />
                           
                                    <label>Date Submitted:</label>
                                    <asp:TextBox ID="txtDateSubmitted" runat="server" Enabled="False" />
                            
                                    <label>Date Received at IMM:</label>
                                    <asp:TextBox ID="txtGMPReceivedDate" runat="server" Enabled="False" />

                                    <label>Unit Status:</label>
                                    <asp:TextBox ID="txtCurrentProcess" runat="server" Enabled="False" />
                            
                                    <label>Repair Date:</label>
                                    <asp:TextBox ID="txtRepairDate" runat="server" Enabled="False" />

                                    <label>Repair Fee:</label>
                                    <asp:TextBox ID="txtRepairFee" runat="server" Enabled="False" />        
                                </div>

                            	<div class="col-md">
                                    <label>Customer Notes:</label>
                                    <asp:TextBox ID="txtCustomerNotes" runat="server" Enabled="False" TextMode="MultiLine" />
                            
                                    <label>Store Comments:</label>
                                    <asp:TextBox ID="txtStoreComments" runat="server" Enabled="False" TextMode="MultiLine" />
                            
                                    <label><asp:Label ID="lblAssessment" runat="server" Text="Unit Assessment:" /></label>
                                    <asp:TextBox ID="txtAssessment" runat="server" Enabled="False"  TextMode="MultiLine" />

                                    <label>Ship Date:</label>
                                    <asp:TextBox ID="txtGMPMSCShippedDate" runat="server" Enabled="False" />
                            
                                    <label>Outgoing waybill:</label>
                                    <asp:TextBox ID="txtOutBoundWayBill_S" runat="server" Enabled="False" />
                            
                                    <label>Courier - Out:</label>
                                    <asp:TextBox ID="txtCourier" runat="server" Enabled="False" />

                                    <label>Authorization:</label>
                                    <asp:TextBox ID="txtAuthorizationStatus" runat="server" Enabled="False" />

                                    <label>Authorization Name:</label>
                                    <asp:TextBox ID="txtAuthorizationName" runat="server" Enabled="False" />
                                    
                                    <div class="form-row">
                                    	<div class="col-6">
                                            <label class="mr-1"><asp:Label ID="lblEstimate" runat="server" Text="Estimate: $1000" /></label>
                                        </div>
                                    	<div class="col-6">
                                            <label class="mr-1"><asp:Label ID="lblFreight" runat="server" Text="Freight: $1000" /></label>
                                        </div>
                                    	<div class="col-6">
                                            <label class="mr-1"><asp:Label ID="lblHST" runat="server" Text="HST: $1000" /></label>
                                        </div>
                                    	<div class="col-6">
                                            <label class="mr-1"><asp:Label ID="lblTotal" runat="server" Text="Total: $1000" /></label>
                                        </div>
                                    </div>

                                    <asp:Button ID="btnAuthorize" runat="server" Text="Authorize" OnClientClick="Authorize(); return false;" />
                                    <asp:Button ID="btnDecline" runat="server" Text="Decline" OnClientClick="Decline(); return false;" />
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnCancel" runat="server" Text="Close" OnClientClick="CloseUnitSummary();return false;" />
                        </div>
                    </div>
                </div>
            </div>

            <div id="wndGetPIN" class="modal" tabindex="-1" role="dialog">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">PIN</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <asp:HiddenField ID="hdnPassword" runat="server" />
                            <div class="row">
                            	<div class="col-md-6">
                                    <label>PIN:</label>
                                    <input id="PinInput" runat="server" type="password" />
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="Button1" runat="server" Text="Done" OnClientClick="ClosePinWindow();return false;"/>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ContentPlaceHolderID="js" runat="server">
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

        function ClosePinWindow() {
            var Password = $get('<%= hdnPassword.ClientID%>').value;
            var GPassword = $get('<%= PinInput.ClientID%>').value;
            $get('<%= PinInput.ClientID%>').value = "";
            $('#wndGetPIN').modal('hide');
            //          var GPassword = prompt('PIN:', ' ');
            if (GPassword == null || GPassword.length == 0) { return; }
            if (GPassword == Password) {
                $get('<%= btnAuthorize.ClientID%>').style.visibility = "visible";
                $get('<%= btnAuthorize.ClientID%>').disabled = true;
                $get('<%= btnDecline.ClientID%>').style.visibility = "visible";
                $get('<%= btnDecline.ClientID%>').disabled = true;
                $get('<%= lblEstimate.ClientID%>').style.visibility = "visible";
                //              $get('<%= lblFreight.ClientID%>').style.visibility = "visible";
                //              $get('<%= lblHST.ClientID%>').style.visibility = "visible";
                //              $get('<%= lblTotal.ClientID%>').style.visibility = "visible";

                $get('<%= txtAuthorizationName.ClientID%>').disabled = false;
                $get('<%= txtAuthorizationName.ClientID%>').style.background = '#FFFFCC';
                $get('<%= txtAuthorizationName.ClientID%>').focus();

                $('#wndUnitSummary').modal('show');
            }
            else { alert("Invalid PIN"); }

        }


        function OpenUnitSummary(ReceiveDetailID, ReceiveDetailAuthorizationLogID, ShowAuthorize, Password) {
            ClearRecordData();
            fillRecordData(ReceiveDetailID, ReceiveDetailAuthorizationLogID);
            $get('<%= hdnReceiveDetailID.ClientID%>').value = ReceiveDetailID;
            $get('<%= hdnPassword.ClientID%>').value = Password;


            $get('<%= hdnReceiveDetailAuthorizationLogID.ClientID%>').value = ReceiveDetailAuthorizationLogID;
            //$get('<%= txtAuthorizationName.ClientID%>').style.background = '#0066CC';

            if (ShowAuthorize == false) {
                $get('<%= btnAuthorize.ClientID%>').style.visibility = "hidden";
                $get('<%= btnDecline.ClientID%>').style.visibility = "hidden";
                $get('<%= lblEstimate.ClientID%>').style.visibility = "hidden";
                $get('<%= lblFreight.ClientID%>').style.visibility = "hidden";
                $get('<%= lblHST.ClientID%>').style.visibility = "hidden";
                $get('<%= lblTotal.ClientID%>').style.visibility = "hidden";
                $get('<%= txtAuthorizationName.ClientID%>').disabled = true;

                $('#wndUnitSummary').modal('show');
            }
            else {
                if (Password.length > 0) {
                    $('#wndGetPIN').modal('show');
                }
            }

        }

        function NameChange() {
            var text = $get('<%= txtAuthorizationName.ClientID%>').value;
            $get('<%= btnAuthorize.ClientID%>').disabled = true;
            $get('<%= btnDecline.ClientID%>').disabled = true;
            $get('<%= txtAuthorizationName.ClientID%>').style.background = '#FFC0C1';
            if (text.length > 0) {
                $get('<%= txtAuthorizationName.ClientID%>').style.background = '#FFFFCC';
                $get('<%= btnAuthorize.ClientID%>').disabled = false;
                $get('<%= btnDecline.ClientID%>').disabled = false;
            }
            $get('<%= txtAuthorizationName.ClientID%>').focus();
        }

        function ProcessUnitSummary() {
            CloseUnitSummary();
        }

        function CloseUnitSummary() {
            $('#wndUnitSummary').modal('hide');
        }

        function ClearRecordData() {

            $get('<%= txtServiceRequestNumber.ClientID%>').value = 'One moment please';
            $get('<%= txtCustomerName.ClientID%>').value = '.....';
            $get('<%= txtESN.ClientID%>').value = '';
            $get('<%= txtOriginalIMEI.ClientID%>').value = '';
            $get('<%= txtWarrantyType.ClientID%>').value = '';

            $get('<%= txtFaultCode.ClientID%>').value = '';
            $get('<%= txtDateSubmitted.ClientID%>').value = '';
            $get('<%= txtFaultCode2.ClientID%>').value = '';
            $get('<%= txtGMPReceivedDate.ClientID%>').value = '';
            $get('<%= txtRepairDate.ClientID%>').value = '';
            $get('<%= txtCurrentProcess.ClientID%>').value = '';

            //          $get('<%= btnAuthorize.ClientID%>').value = ReceiveDetailID;

            $get('<%= txtRepairFee.ClientID%>').value = '';
            $get('<%= txtCustomerNotes.ClientID%>').value = '';
            $get('<%= txtStoreComments.ClientID%>').value = '';
            $get('<%= txtAuthorizationStatus.ClientID%>').value = '';
            $get('<%= txtAuthorizationName.ClientID%>').value = '';
            $get('<%= txtAssessment.ClientID%>').value = '';
            $get('<%= txtGMPMSCShippedDate.ClientID%>').value = '';
            $get('<%= txtOutBoundWayBill_S.ClientID%>').value = '';
            $get('<%= txtCourier.ClientID%>').value = '';

            $get('<%= lblEstimate.ClientID%>').innerHTML = '';
            $get('<%= lblFreight.ClientID%>').innerHTML = '';
            $get('<%= lblHST.ClientID%>').innerHTML = '';
            $get('<%= lblTotal.ClientID%>').innerHTML = '';

            $get('<%= lblAssessment.ClientID%>').innerHTML = 'Unit Assessment:';
        }

        function fillRecordData(ReceiveDetailID, ReceiveDetailAuthorizationLogID) {
            var service = new WebServer_01();
            var rValue = service.GetDashboardReceiveDetail(ReceiveDetailID, ReceiveDetailAuthorizationLogID, $get('<%= hdnUserName.ClientID %>').value, onfillRecordData);
        }

        function onfillRecordData(result) {
            result = '({' + result + '})';
            var resultList = eval(result);

            //          rData.AddValuePair("isAuthorize", "");

            $get('<%= txtServiceRequestNumber.ClientID%>').value = resultList.txtServiceRequestNumber;


            $get('<%= txtCustomerName.ClientID%>').value = resultList.txtCustomerName;
            $get('<%= txtESN.ClientID%>').value = resultList.txtESN;
            $get('<%= txtOriginalIMEI.ClientID%>').value = resultList.txtOriginalIMEI;
            $get('<%= txtWarrantyType.ClientID%>').value = resultList.txtWarrantyType;
            $get('<%= txtFaultCode.ClientID%>').value = resultList.txtFaultCode;
            $get('<%= txtDateSubmitted.ClientID%>').value = resultList.txtDateSubmitted;
            $get('<%= txtFaultCode2.ClientID%>').value = resultList.txtFaultCode2;
            $get('<%= txtGMPReceivedDate.ClientID%>').value = resultList.txtGMPReceivedDate;
            $get('<%= txtRepairDate.ClientID%>').value = resultList.txtRepairDate;
            $get('<%= txtCurrentProcess.ClientID%>').value = resultList.txtCurrentProcess;

            //          $get('<%= btnAuthorize.ClientID%>').value = ReceiveDetailID;

            $get('<%= txtRepairFee.ClientID%>').value = resultList.txtRepairFee;
            $get('<%= txtCustomerNotes.ClientID%>').value = resultList.txtCustomerNotes;
            $get('<%= txtStoreComments.ClientID%>').value = resultList.txtStoreComments;



            $get('<%= txtAuthorizationStatus.ClientID%>').value = resultList.txtAuthorizationStatus;
            $get('<%= txtAuthorizationName.ClientID%>').value = resultList.txtAuthorizationName;
            $get('<%= txtAssessment.ClientID%>').value = resultList.txtAssessment;
            $get('<%= txtGMPMSCShippedDate.ClientID%>').value = resultList.txtGMPMSCShippedDate;
            $get('<%= txtOutBoundWayBill_S.ClientID%>').value = resultList.txtOutBoundWayBill_S;
            $get('<%= txtCourier.ClientID%>').value = resultList.txtCourier;

            if (resultList.txtRepairFee.length > 0) { $get('<%= lblAssessment.ClientID%>').innerHTML = 'Repair Notes: '; $get('<%= txtAssessment.ClientID%>').value = resultList.txtRepairNotes; }

            if (resultList.lblEstimate.length > 0) { $get('<%= lblEstimate.ClientID%>').innerHTML = 'Estimate: ' + resultList.lblEstimate; }
            //          if (resultList.lblFreight.length > 0) { $get('<%= lblFreight.ClientID%>').innerHTML = 'Freight: ' + resultList.lblFreight; }
            //          if (resultList.lblHST.length > 0) { $get('<%= lblHST.ClientID%>').innerHTML = 'HST: ' + resultList.lblHST; }
            //          if (resultList.lblTotal.length > 0) { $get('<%= lblTotal.ClientID%>').innerHTML = 'Total: ' + resultList.lblTotal; }
        }

        //      function Authorize() {
        //          var service = new WebServer_01();
        //          var rValue = service.AuthorizeAuthorization($get('<%= hdnReceiveDetailAuthorizationLogID.ClientID %>').value,
        //                                                         $get('<%= txtAuthorizationName.ClientID %>').value,
        //                                                         $get('<%= hdnUserName.ClientID %>').value, onDeclineAuthorize);
        //      }

        function Authorize() {
            var service = new WebServer_01();
            var rValue = service.DealerAuthorized($get('<%= hdnReceiveDetailAuthorizationLogID.ClientID %>').value,
                                                $get('<%= txtAuthorizationName.ClientID %>').value,
                                                $get('<%= hdnUserName.ClientID %>').value,
                                                $get('<%= hdnUserIPAddress.ClientID %>').value,

                                                onAuthorize, onAuthorizeb);
        }

        function Decline() {
            var service = new WebServer_01();
            var rValue = service.DeclineAuthorization($get('<%= hdnReceiveDetailAuthorizationLogID.ClientID %>').value,
                                                    $get('<%= txtAuthorizationName.ClientID %>').value,
                                                    $get('<%= hdnUserName.ClientID %>').value, onDecline, onDeclineb);
        }

        function onWebServerError(Result) {
            alert('Error:' + Result.get_message());
        }

        function onDecline(result) {
            var answer = confirm('Print Authorization Declined form?')
            if (answer == true) {
                OpenAuthorizeReport($get('<%= hdnReceiveDetailAuthorizationLogID.ClientID %>').value)
            }
            //          $get('<%= btnRefresh.ClientID %>').click() = true;
        }

        function onDeclineb(result) {
            var answer = confirm('Print authorization declined form?')
            if (answer == true) {
                OpenAuthorizeReport($get('<%= hdnReceiveDetailAuthorizationLogID.ClientID %>').value)
            }
            //          $get('<%= btnRefresh.ClientID %>').click() = true;
        }

        function onAuthorize(result) {
            var answer = confirm('Print Authorization form?')
            if (answer == true) {
                OpenAuthorizeReport($get('<%= hdnReceiveDetailAuthorizationLogID.ClientID %>').value)
            }
            //          $get('<%= btnRefresh.ClientID %>').click() = true;
        }

        function onAuthorizeb(result) {
            var answer = confirm('Print authorization form?')
            if (answer == true) {
                OpenAuthorizeReport($get('<%= hdnReceiveDetailAuthorizationLogID.ClientID %>').value)
            }
            //          $get('<%= btnRefresh.ClientID %>').click() = true;
        }

        //      function OpenAuthorizeReport(ReceiveDetailAuthorizationLogID, Repair) {
        //          // if Pre Received XAUTX
        //          // Do this
        //          //else
        //          // do this
        //          if (Repair == "No") {
        //              return OpenAuthorizeReport_A(ReceiveDetailAuthorizationLogID);
        //          }
        //          else {
        //              return OpenRepairForm("R", ReceiveDetailAuthorizationLogID);
        //          }


        //      }


        //      function OpenbagTag(ReceiveDetailID) {
        //          var report = 'Bagtag';
        //          //           if (IsNumeric(MCL('hdnAllowProjectPassThrough').value) == false) {
        //          //               report = 'Bagtag';
        //          //           }

        //          var xDataList = {};
        //          xDataList['RPT'] = "BagTag";
        //          xDataList['RDID'] = ReceiveDetailID;
        //          xDataList['ESN'] = "X";

        //          var pstring = GetParameterStream(xDataList);
        //          var WindowToOpen = 'BagTag.aspx';
        //          if (pstring.length > 0) {
        //              WindowToOpen = WindowToOpen + '?' + pstring
        //          }
        //          var win = window.open(WindowToOpen, '_blank', 'menubar', true);
        //          // win.focus();
        //      }

        function OpenClientbagTag(ReceiveDetailID) {
            var xDataList = {};
            xDataList['RDID'] = ReceiveDetailID;
            var pstring = GetParameterStream(xDataList);
            //           var WindowToOpen = 'RPT_EXCEL_Out.aspx';
            var WindowToOpen = 'RPT_Submission.aspx';

            if (pstring.length > 0) {
                WindowToOpen = WindowToOpen + '?' + pstring
            }
            var win = window.open(WindowToOpen, '_blank', 'menubar', true);
            //          ScanFocus();
            return;
        }


        function OpenRepairForm(RPT, ReceiveDetailAuthorizationLogID) {
            // CloseSelectRepairReport();
            var xDataList = {};
            xDataList['A'] = "-1";
            xDataList['B'] = ReceiveDetailAuthorizationLogID;
            xDataList['C'] = RPT;
            var pstring = GetParameterStream(xDataList);
            var WindowToOpen = 'RPT_RepairForm.aspx';
            if (pstring.length > 0) {
                WindowToOpen = WindowToOpen + '?' + pstring
            }
            var win = window.open(WindowToOpen, '_blank', 'menubar', true);
            // win.focus();
        }

        function OpenAuthorizeReport(ReceiveDetailAuthorizationLogID) {
            var xDataList = {};
            xDataList["A"] = "";                                    // $get('<%= hdnReceiveDetailID.ClientID %>').value;
            xDataList["B"] = ReceiveDetailAuthorizationLogID;       // $get('<%= hdnReceiveDetailAuthorizationLogID.ClientID %>').value;

            //            var win = window.open("ViewDoc.aspx", "_blank", "status=no,toolbar=no,menubar=no,location=no,titlebar=no,width=600px,height=540px", true);
            var pstring = GetParameterStream(xDataList);

            var WindowToOpen = "RPT_Authorize_01.aspx";
            //var WindowToOpen = "RPT_BoxList.aspx";
            if (pstring.length > 0) {
                WindowToOpen = WindowToOpen + "?" + pstring
            }
            //            var win = window.open(WindowToOpen, "_blank", "width=100,height=50,menubar", true);
            var win = window.open(WindowToOpen, "_blank", "menubar", true);
            // win.focus();
            CloseUnitSummary();
            return false;
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
