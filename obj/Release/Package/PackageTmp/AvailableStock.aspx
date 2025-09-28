<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AvailableStock.aspx.cs" Inherits="BW_WebApp.AvailableStock" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>
<%@ Register Assembly="Syncfusion.Tools.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89"  Namespace="Syncfusion.Web.UI.WebControls.Tools" TagPrefix="syncfusion" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="cMC" runat="server">

    <asp:HiddenField ID="hdnKeys" runat="server" />
    <asp:HiddenField ID="hdnLastTreeSelectKeys" runat="server" />
    <asp:HiddenField ID="hdnUserName" runat="server" ClientIDMode="Static" />

    <h1><asp:Label ID="lblRecordTitle" runat="server" Text="Available - Stock for Sale" /></h1>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-lg-9">

                    <asp:Panel ID="pnlHeader" runat="server">

                        <h2><asp:Label runat="server" Text="Header Section" /></h2>
                        <asp:HiddenField ID="hdnOrderHeaderID" runat="server" ClientIDMode="Static" />
                        
                        <div class="row">
                            <div class="col">
                                <label class="d-block">Customer PO:</label>
                                <asp:TextBox ID="txtCustomerPONumber" runat="server" MaxLength="50" />
                        
                                <label class="d-block">Restrict to Project Tag:</label>
                                <asp:TextBox ID="txtProjectTag" runat="server" />
                        
                                <label class="d-block">Client:</label>
                                <asp:TextBox ID="txtBillClient" runat="server" ToolTip="Enter client location Key" />
                                <asp:Button ID="btnBillClientSearch" runat="server" Text="Search" />

                                <asp:LinkButton runat="server" OnClientClick="OpenClientSearch('Bill');return false;" ToolTip="Search Clients">
                                    <span class="oi oi-magnifying-glass"></span>
                                </asp:LinkButton>
                        
                                <asp:HiddenField ID="hdnBillClientLocationID" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnBillCompanyName" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnBillContactName" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnBillAddressLine1" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnBillAddressLine2" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnBillCity" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnBillStateOrProvince" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnBillPostalCode" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnBillPhoneNumber" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnBillFaxNumber" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnBillNotes" runat="server" ClientIDMode="Static" />

                                <label class="d-block">Name/Address:</label>
                                <asp:TextBox ID="txtBillNameAddresstext" runat="server" Rows="6" ReadOnly="True" TextMode="MultiLine" />
                                <asp:Button ID="btnBillClientEdit" runat="server" Text="Edit" />

                                <label class="d-block">Internal Note:</label>
                                <asp:TextBox ID="txtInternalNote" runat="server" Rows="6" TextMode="MultiLine" MaxLength="500" />
                            </div>

                            <div class="col">
                                <label class="d-block">PO Number:</label>
                                <asp:TextBox ID="txtOrderNumber" runat="server" MaxLength="500" ReadOnly="True" ToolTip="Auto Assigned" />
                                <%--<asp:TextBox ID="lblPurchaseOrderNumber" runat="server" Width="100%" MaxLength="50" />--%>
                        
                                <label class="d-block">Waybill Number:</label>
                                <asp:TextBox ID="txtWaybillNumber" runat="server" MaxLength="500" />
                        
                                <label class="d-block">Ship To:</label>
                                <asp:TextBox ID="txtShipClient" runat="server" ToolTip="Enter client location Key" />
                                <asp:Button ID="btnShipClientSearch" runat="server" Text="Search" />

                                <asp:LinkButton runat="server" ToolTip="Search Clients" OnClientClick="OpenClientSearch('Ship');return false;">
                                    <span class="oi oi-magnifying-glass"></span>
                                </asp:LinkButton>

                                <asp:HiddenField ID="hdnShipClientLocationID" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnShipCompanyName" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnShipContactName" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnShipAddressLine1" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnShipAddressLine2" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnShipCity" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnShipStateOrProvince" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnShipPostalCode" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnShipPhoneNumber" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnShipFaxNumber" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnShipNotes" runat="server" ClientIDMode="Static" />
                                <%--<asp:HiddenField ID="hdnPaid" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnPostPaid" runat="server" ClientIDMode="Static" />--%>
                        
                                <label class="d-block">Name/Address:</label>
                                <asp:TextBox ID="txtShipNameAddresstext" runat="server" Rows="6" ReadOnly="True" TextMode="MultiLine" />
                                <asp:Button ID="btnShipClientEdit" runat="server" Text="Edit" />

                                <label class="d-block">Delivery Note:</label>
                                <asp:TextBox ID="txtDeliveryNote" runat="server" Rows="6" TextMode="MultiLine" MaxLength="500" />
                                
                                <div class="form-check-inline">
                                    <asp:CheckBox ID="chkPaid" runat="server" />
                                    <label>Paid</label>
                                </div>
                                <%--Post Paid:--%>
                                <asp:CheckBox ID="chkPostPaid" runat="server" Visible="False" />
                        
                                <!-- JIM: You should hide this (display: none) when it's empty -->
                                <asp:DropDownList ID="drpAstockEmailSubject" runat="server">
                                    <asp:ListItem>Good</asp:ListItem>
                                    <asp:ListItem>Bad</asp:ListItem>
                                    <asp:ListItem>Other</asp:ListItem>
                                </asp:DropDownList>

                                <asp:Button ID="btnSaveSale" runat="server" Text="Save Sale" ToolTip="Save Sale" OnClientClick="SaveButtonClick(this);" />
                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" TargetControlID="btnSaveSale"
                                    ConfirmText="Are you sure you want to Save this Order?" />

                                <asp:Button ID="btnSaveClass" runat="server" Text="Save Template" ToolTip="Save Template" OnClientClick="SaveButtonClick(this);" />

                            </div>
                        </div>
                        
                    </asp:Panel>

                    <asp:Panel ID="pnlEditNameAddress" CssClass="w-md-50" runat="server" Visible="False">
                        <h2><asp:Label ID="lblEditAddress" runat="server" Text="Edit Address" /></h2>

                        <label>Company Name:</label>
                        <asp:TextBox ID="txtCompanyName" runat="server" MaxLength="50" />

                        <label>Contact Name:</label>
                        <asp:TextBox ID="txtContactName" runat="server" MaxLength="30" />

                        <label>Address Line 1:</label>
                        <asp:TextBox ID="txtAddressLine1" runat="server" MaxLength="50" />

                        <label>Address Line 2:</label>
                        <asp:TextBox ID="txtAddressLine2" runat="server" MaxLength="50" />

                        <label>City:</label>
                        <asp:TextBox ID="txtCity" runat="server" MaxLength="50" />

                        <label>State / Province:</label>
                        <asp:TextBox ID="txtStateOrProvince" runat="server" MaxLength="20" />

                        <label>Zip / Postal Code:</label>
                        <asp:TextBox ID="txtPostalCode" runat="server" MaxLength="20" />

                        <label>Phone:</label>
                        <asp:TextBox ID="txtPhoneNumber" runat="server" MaxLength="30" />

                        <label>Fax:</label>
                        <asp:TextBox ID="txtFaxNumber" runat="server" MaxLength="30" />

                        <label>Notes:</label>
                        <asp:TextBox ID="txtNotes" runat="server" Rows="6" TextMode="MultiLine" />
                        
                        <asp:HiddenField ID="hdnClientType" runat="server" />
                        <asp:Button ID="btnEditNameAddressOK" runat="server" Text="OK" />
                        <asp:Button ID="btnEditNameAddressCancel" runat="server" Text="Cancel" />
                    </asp:Panel>

                    <%--<asp:HoverMenuExtender ID="hme2" runat="Server" TargetControlID="TreeData" PopupControlID="PopupMenu"
                        HoverCssClass="popupHover" PopupPosition="Left" OffsetX="0" OffsetY="0" PopDelay="50" />--%>

                    <div class="card mb-3">
                        <syncfusion:TreeView ID="TreeData" CssClass="tree-view" runat="server" OnNodeExpanded="TreeView1_NodeExpanded"
                            ClientSideOnContextMenu="NodeOnContextMenu(this)" EditNode="False" ClientSideOnNodeSelect="NodeOnSelect(this)" />
                    </div>

                    <hr class="d-lg-none">

                </div>

                <div class="col-lg-3">
                    <asp:HiddenField ID="hdnLstHistory" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="hdnListHistoryValue" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="hdntxtHistoryCount" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="hdntxtHistorySellPrice" runat="server" ClientIDMode="Static" />

                    <asp:Button ID="btnHeader" runat="server" Text="Header" OnClientClick="showScreen('Header'); return false;" />
                    <asp:Button ID="btnDetail" runat="server" Text="Detail" class="button" />
                    <div class="form-check-inline">
                        <asp:CheckBox ID="chkViewGrade" runat="server" Text="Grade" AutoPostBack="True" />
                    </div>

                    <div class="form-row">
                        <div class="col">
                            <asp:TextBox ID="txtHistoryCount" runat="server" ReadOnly="True" Text="0" ToolTip="Unit Count" />
                        </div>
                        <div class="col">
                            <asp:TextBox ID="txtHistorySellPrice" runat="server" ReadOnly="True" Text="0" ToolTip="Total Selling Price" />
                        </div>
                    </div>
                    
                    <asp:ListBox ID="lstHistory" CssClass="multi-line-select resize-y" runat="server" SelectionMode="Single" ViewStateMode="Inherit" ClientIDMode="Static" />
                    <asp:Button ID="btnRemoveAll" runat="server" Text="Clear/New" UseSubmitBehavior="False" ClientIDMode="Static"
                        OnClientClick="ResetHistory(); return false;" ToolTip="Clear History List" />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" UseSubmitBehavior="False" ClientIDMode="Static"
                        OnClientClick="DeleteHistory(); return false;" ToolTip="Delete Selected History Item" />
                    <asp:Button ID="btnRPT" runat="server" Text="RPT" UseSubmitBehavior="False"
                        ClientIDMode="Static" OnClientClick="PrintReport(); return false;" />
                </div>
            </div>

            <div id="wndSelectClientLocation" class="modal" tabindex="-1" role="dialog">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Search</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <asp:HiddenField ID="TargetAddress" runat="server" />

                            <label>Client Name:</label>
                            <asp:TextBox ID="txtsClientName" runat="server" />
                        
                            <label>Location Name:</label>
                            <asp:TextBox ID="txtsLocationName" runat="server" />
                        
                            <label>Street:</label>
                            <asp:TextBox ID="txtsStreet" runat="server" />
                        
                            <label>Postal Code:</label>
                            <asp:TextBox ID="txtsPostalCode" runat="server" />
                        
                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClientClick="SearchClient();return false;" />
                         
                            <asp:Panel ID="pnlSearchResult" runat="server" />
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

<asp:Content ContentPlaceHolderID="js" runat="server">
    <script type="text/javascript">

        function PrintReport() {
            var xDataList = {};
            xDataList["RPT"] = "AVSTOCK";
            xDataList["KEY"] = MCL('TREEKEYS').value;
            xDataList["USERNAME"] = MCL('USERNAME').value;
            var pstring = GetParameterStream(xDataList);
            // var WindowToOpen = "RPT_SpotCountReport.aspx";
            var WindowToOpen = "/Reports/RPT_EXCEL_Out.aspx";
            if (pstring.length > 0) {
                WindowToOpen = WindowToOpen + "?" + pstring
            }
            var win = window.open(WindowToOpen, "_blank", "menubar", true);
            return;
        }

        function showScreen(show) {
            var Header = MCL('pnlHeader');
            var Detail = MCL('TreeData');
            if (Header == null || Detail == null) { return; }
            if (show == "Header") {
                ControlShow(Header);
                ControlHide(Detail);
            }
            else {
                ControlShow(Detail);
                ControlHide(Header);
            }
        }

        function ControlShow(cntrl) {
            cntrl.style.display = '';
        }

        function ControlHide(cntrl) {
            cntrl.style.display = 'none';
        }

        function ResetHistory() {
            MCL('HDNKEYS').value = "";
            MCL('TARGETADDRESS').value = "";
            MCL('TXTBILLCLIENT').value = "";
            //MCL('BTNBILLCLIENTSEARCH').value = "";
            MCL('TXTSHIPCLIENT').value = "";

            MCL('txtCustomerPONumber').value = "";
            MCL('txtProjectTag').value = "";
            MCL('txtInternalNote').value = "";
            MCL('txtOrderNumber').value = "New";
            MCL('txtWaybillNumber').value = "";
            MCL('txtShipNameAddresstext').value = "";
            MCL('txtBillNameAddresstext').value = "";

            MCL('hdnBillClientLocationID').value = "";
            MCL('hdnBillCompanyName').value = "";
            MCL('hdnBillContactName').value = "";
            MCL('hdnBillAddressLine1').value = "";
            MCL('hdnBillAddressLine2').value = "";
            MCL('hdnBillCity').value = "";
            MCL('hdnBillStateOrProvince').value = "";
            MCL('hdnBillPostalCode').value = "";
            MCL('hdnBillPhoneNumber').value = "";
            MCL('hdnBillFaxNumber').value = "";
            MCL('hdnBillNotes').value = "";

            MCL('hdnShipClientLocationID').value = "";
            MCL('hdnShipCompanyName').value = "";
            MCL('hdnShipContactName').value = "";
            MCL('hdnShipAddressLine1').value = "";
            MCL('hdnShipAddressLine2').value = "";
            MCL('hdnShipCity').value = "";
            MCL('hdnShipStateOrProvince').value = "";
            MCL('hdnShipPostalCode').value = "";
            MCL('hdnShipPhoneNumber').value = "";
            MCL('hdnShipFaxNumber').value = "";
            MCL('hdnShipNotes').value = "";

            if (MCL('lstHistory') == null) { return; }
            var Source = MCL('lstHistory');
            var Count = MCL('txtHistoryCount');
            MCL('txtHistorySellPrice').value = '0';
            Count.value = '0';
            if (Source != null) {
                var xc = Source.getElementsByTagName('option').length;
                for (var i = 0; i < xc; i++) {
                    Source.remove(0);
                    var count = Count.value;
                    Count.value = count.toString();
                    count--
                }
            }

            showScreen("Detail");
            ControlShow(MCL('btnDetail'));
            ControlHide(MCL('btnSaveSale'));
            UpdateBackEndData();
        }

        function DeleteHistory() {
            if (MCL('lstHistory') == null) { return; }
            var Source = MCL('lstHistory');
            var Count = MCL('txtHistoryCount');
            if (Source != null) {
                if (Source.options.selectedIndex >= 0) {
                    var Count = MCL('txtHistoryCount');
                    var count = Number(Count.value);

                    var AdjustValue = Source.options[Source.options.selectedIndex].value
                    var b = AdjustValue.split(',')
                    count -= Number(b[0]);
                    Count.value = count.toString();
                    var OriginalSellPrice = MCL('txtHistorySellPrice');
                    var originalSellPrice = Number(OriginalSellPrice.value);
                    var unitPriceElement = b.length - 1;       // The last element is the unit price

                    var newSellPrice = Number(b[0]) * Number(b[unitPriceElement]);
                    originalSellPrice -= newSellPrice;
                    OriginalSellPrice.value = originalSellPrice.toString();

                    Source.remove(Source.options.selectedIndex);
                }
            }
            UpdateBackEndData();
        }

        function RecordHistory(Value, qty, key, SellPrice) {
            if (MCL('lstHistory') == null) { return; }
            var Source = MCL('lstHistory');
            // Check to see if the item is already there.
            var xc = Source.getElementsByTagName('option').length;

            if (Source != null) {
                var newOption = new Option();
                newOption.text = Value;
                //newOption.value = qty.toString() + "(" + SellPrice.toString() + ")" + ":" + key + ":" + SellPrice.toString();
                newOption.value = qty.toString() + "," + key + "," + SellPrice.toString();
                Source.options[Source.length] = newOption;

                var Count = MCL('txtHistoryCount');
                var count = Number(Count.value);
                count += Number(qty);
                Count.value = count.toString();

                var OriginalSellPrice = MCL('txtHistorySellPrice');
                var originalSellPrice = Number(OriginalSellPrice.value);
                var newSellPrice = SellPrice * Number(qty);
                originalSellPrice += newSellPrice;
                OriginalSellPrice.value = originalSellPrice.toString();


                ControlShow(MCL('btnSaveSale'));
            }
            UpdateBackEndData();
        }

        function SellButton_Click(o, value) {
            // hobble = o.previousElementSibling;
            var qty = prompt("Quantity to Pick.", "1")
            if (isNaN(qty) == true) { alert('You must enter a quantity.'); return; }
            qty = Number(qty);
            if (qty < 1) { alert('Quanity must be greater than 0.'); return; }

            var UnitPrice = prompt("Unit Selling Price.", "0")
            UnitPrice = Number(UnitPrice);
            if (UnitPrice > 0) {
                RecordHistory(qty + '(' + UnitPrice + ')' + '/' + value, qty, value, UnitPrice);
            }
            else {
                RecordHistory(qty + '/' + value, qty, value, UnitPrice);
            }
            qty.value = '';
            //            var PopUpPannel = MCL('GradeMenu');
            //            ControlHide(PopUpPannel);
        }

        function NodeOnContextMenu(o) {
            //            var PopUpPannel = MCL('GradeMenu');
            //            ControlShow(PopUpPannel);
            //            PopUpPannel.style.position = 'absolute';
            //            PopUpPannel.style.top = o.Event.clientY + 'px';
            //            PopUpPannel.style.left = o.Event.clientX + 'px';
            //OpenClientSearch('Bill');
            //          var PopupContainer1 = MCL('PopupControlContainer1');
            //          PopupContainer1.ShowPopup(10, 10, true);
            if (o.Value.length > 0) { SellButton_Click(o, o.Value); }
            return false;
        }

        function NodeOnSelect(o) {
            MCL('TREEKEYS').value = o.Value;
        }

        function MCL(ControlName) {
            switch (ControlName.toUpperCase()) {
                case "HDNLSTHISTORY": return $get("<%= hdnLstHistory.ClientID %>"); break;
                case "HDNLISTHISTORYVALUE": return $get("<%= hdnListHistoryValue.ClientID %>"); break;
                case "HDNTXTHISTORYCOUNT": return $get("<%= hdntxtHistoryCount.ClientID %>"); break;
                case "HDNTXTHISTORYSELLPRICE": return $get("<%= hdntxtHistorySellPrice.ClientID %>"); break;
                case "LSTHISTORY": return $get("<%= lstHistory.ClientID %>"); break;
                case "TXTHISTORYCOUNT": return $get("<%= txtHistoryCount.ClientID %>"); break;
                case "TXTHISTORYSELLPRICE": return $get("<%= txtHistorySellPrice.ClientID %>"); break;
                case "HDNKEYS": return $get("<%= hdnKeys.ClientID %>"); break;
                case "PNLHEADER": return $get("<%= pnlHeader.ClientID %>"); break;
                case "TREEDATA": return $get("<%= TreeData.ClientID %>"); break;

                case "USERNAME": return $get("<%= hdnUserName.ClientID %>"); break;
                case "TARGETADDRESS": return $get("<%= TargetAddress.ClientID %>"); break;
                case "TXTBILLCLIENT": return $get("<%= txtBillClient.ClientID %>"); break;
                case "BTNBILLCLIENTSEARCH": return $get("<%= btnBillClientSearch.ClientID %>"); break;
                case "TXTSHIPCLIENT": return $get("<%= txtShipClient.ClientID %>"); break;
                case "BTNSHIPCLIENTSEARCH": return $get("<%= btnShipClientSearch.ClientID %>"); break;

                case "TXTCUSTOMERPONUMBER": return $get("<%= txtCustomerPONumber.ClientID %>"); break;
                case "TXTPROJECTTAG": return $get("<%= txtProjectTag.ClientID %>"); break;
                case "TXTINTERNALNOTE": return $get("<%= txtInternalNote.ClientID %>"); break;
                case "TXTORDERNUMBER": return $get("<%= txtOrderNumber.ClientID %>"); break;
                case "TXTWAYBILLNUMBER": return $get("<%= txtWaybillNumber.ClientID %>"); break;

                case "HDNBILLCLIENTLOCATIONID": return $get("<%= hdnBillClientLocationID.ClientID %>"); break;
                case "HDNBILLCOMPANYNAME": return $get("<%= hdnBillCompanyName.ClientID %>"); break;
                case "HDNBILLCONTACTNAME": return $get("<%= hdnBillContactName.ClientID %>"); break;
                case "HDNBILLADDRESSLINE1": return $get("<%= hdnBillAddressLine1.ClientID %>"); break;
                case "HDNBILLADDRESSLINE2": return $get("<%= hdnBillAddressLine2.ClientID %>"); break;
                case "HDNBILLCITY": return $get("<%= hdnBillCity.ClientID %>"); break;
                case "HDNBILLSTATEORPROVINCE": return $get("<%= hdnBillStateOrProvince.ClientID %>"); break;
                case "HDNBILLPOSTALCODE": return $get("<%= hdnBillPostalCode.ClientID %>"); break;
                case "HDNBILLPHONENUMBER": return $get("<%= hdnBillPhoneNumber.ClientID %>"); break;
                case "HDNBILLFAXNUMBER": return $get("<%= hdnBillFaxNumber.ClientID %>"); break;
                case "HDNBILLNOTES": return $get("<%= hdnBillNotes.ClientID %>"); break;

                case "HDNSHIPCLIENTLOCATIONID": return $get("<%= hdnShipClientLocationID.ClientID %>"); break;
                case "HDNSHIPCOMPANYNAME": return $get("<%= hdnShipCompanyName.ClientID %>"); break;
                case "HDNSHIPCONTACTNAME": return $get("<%= hdnShipContactName.ClientID %>"); break;
                case "HDNSHIPADDRESSLINE1": return $get("<%= hdnShipAddressLine1.ClientID %>"); break;
                case "HDNSHIPADDRESSLINE2": return $get("<%= hdnShipAddressLine2.ClientID %>"); break;
                case "HDNSHIPCITY": return $get("<%= hdnShipCity.ClientID %>"); break;
                case "HDNSHIPSTATEORPROVINCE": return $get("<%= hdnShipStateOrProvince.ClientID %>"); break;
                case "HDNSHIPPOSTALCODE": return $get("<%= hdnShipPostalCode.ClientID %>"); break;
                case "HDNSHIPPHONENUMBER": return $get("<%= hdnShipPhoneNumber.ClientID %>"); break;
                case "HDNSHIPFAXNUMBER": return $get("<%= hdnShipFaxNumber.ClientID %>"); break;
                case "HDNSHIPNOTES": return $get("<%= hdnShipNotes.ClientID %>"); break;


                case "TXTSHIPNAMEADDRESSTEXT": return $get("<%= txtShipNameAddresstext.ClientID %>"); break;
                case "TXTBILLNAMEADDRESSTEXT": return $get("<%= txtBillNameAddresstext.ClientID %>"); break;

                case "BTNSAVESALE": return $get("<%= btnSaveSale.ClientID %>"); break;
                case "BTNHEADER": return $get("<%= btnHeader.ClientID %>"); break;
                case "BTNDETAIL": return $get("<%= btnDetail.ClientID %>"); break;
                case "HDNLASTTREESELECTKEYS": return $get("<%= hdnLastTreeSelectKeys.ClientID %>"); break;
                case "TREEKEYS": return $get("<%= hdnLastTreeSelectKeys.ClientID %>"); break;



                default: return null;
            }
        }

        function RestoreBackEndData() {
            MCL('txtHistoryCount').value = MCL('HDNTXTHISTORYCOUNT').value;
            MCL('txtHistorySellPrice').value = MCL('HDNTXTHISTORYSELLPRICE').value;

            var Value = MCL('HDNLISTHISTORYVALUE').value.split("/");
            var Text = MCL('HDNLSTHISTORY').value.split("|");

            if (MCL('lstHistory') == null) { return; }
            var Source = MCL('lstHistory');

            // Clear the list... Incase any stayed behind
            if (Source != null) {
                var xc = Source.getElementsByTagName('option').length;
                for (var i = 0; i < xc; i++) {
                    Source.remove(0);
                }
            }

            for (i = 0; i < Text.length; i++) {
                if (Text[i].length > 0) {
                    var newOption = new Option();
                    newOption.text = Text[i];
                    newOption.value = Value[i];
                    Source.options[Source.length] = newOption;
                }
            }
        }

        function UpdateBackEndData() {
            var dataValue = "";
            var dataText = "";
            if (MCL('lstHistory') == null) { return; }
            var Source = MCL('lstHistory');
            if (Source != null) {
                for (var n = 0; n < Source.options.length; n++) {
                    dataValue += Source.options[n].value + "/";
                    dataText += Source.options[n].text + "|";       // The back slash is used inside the original string between the qty and the text string.
                }
            }
            MCL('hdnKeys').value = dataValue;
            MCL('HDNLISTHISTORYVALUE').value = dataValue;
            MCL('HDNLSTHISTORY').value = dataText;
            MCL('HDNTXTHISTORYCOUNT').value = MCL('txtHistoryCount').value;
            MCL('HDNTXTHISTORYSELLPRICE').value = MCL('txtHistorySellPrice').value;
        }

        function SaveButtonClick(o) {
            ////            var Key = MCL('txtHobbleName');
            ////            if (Key.value.length == 0 || Key.value.toUpperCase() == "ORDER KEY") {
            ////                alert('You must enter an ORDER KEY before saving!');
            ////                return false;
            ////            }
            //            var data = "";
            //            if (MCL('lstHistory') == null) { return; }
            //            var Source = MCL('lstHistory');
            //            if (Source != null) {
            //                for (var n = 0; n < Source.options.length; n++) {
            //                    data += Source.options[n].value + "/";
            //                }
            //            }
            //            MCL('hdnKeys').value = data;
            //            return true;
        }

        ////////////////////////////////////////////////////////////

        // *****************************************************************************
        function OpenClientSearch(Address) {
            MCL("TargetAddress").value = Address;
            $('#wndSelectClientLocation').modal('show');
        }

        function selx(ID) {
            $('#wndSelectClientLocation').modal('hide');
            if (MCL("TargetAddress").value == "Bill") {
                MCL("txtBillClient").value = ID;
                MCL("btnBillClientSearch").click();
            }
            if (MCL("TargetAddress").value == "Ship") {
                MCL("txtShipClient").value = ID;
                MCL("btnShipClientSearch").click();
            }

            // LoadClientLocation(ID);
        }

        function SearchClient() {
            //            var SearchClientName = $get("<%= txtsClientName.ClientID %>").value;
            //            var SearchLocationName = $get("<%= txtsLocationName.ClientID %>").value;
            //            var SearchStreet = $get("<%= txtsStreet.ClientID %>").value;
            //            var SearchPostalCode = $get("<%= txtsPostalCode.ClientID %>").value;
            //            var service = new WebServer_01();
            //            var rValue = service.GetSearchClientLocationData(MCL("UserName").value, SearchClientName, SearchLocationName, SearchStreet, SearchPostalCode, onSearchClientSuccess, onWebServerError);


            var SearchClientName = $get("<%= txtsClientName.ClientID %>").value;
            var SearchLocationName = $get("<%= txtsLocationName.ClientID %>").value;

            var SearchStreet = $get("<%= txtsStreet.ClientID %>").value;
            var SearchPostalCode = $get("<%= txtsPostalCode.ClientID %>").value;
            var service = new WebServer_01();
            var rValue = service.GetSearchClientLocationData(MCL("UserName").value, SearchClientName, SearchLocationName, SearchStreet, SearchPostalCode, onSearchClientSuccess, onWebServerError);

        }

        function onWebServerError(Result) {
            alert("Error:" + Result.get_message());
        }

        function onSearchClientSuccess(Result) {
            var OutputHTML = "";
            var HeaderText = "<hr> <tr><td>Select</td><td>ID</td><td>Client</td><td>Location Name</td><td>Location</td></tr>";
            var BodyText = "";

            //           ClientData = eval('({' + Result + '})');
            ClientData = eval('[' + Result + ']');               // Square brackets to denote an array of elements.
            var Quote = "'";
            for (var i = 0; i < ClientData.length; i++) {
                BodyText = BodyText + "<tr><td>"
                    + '<button id="btn" name="btn" onClick="selx(' + Quote
                    + ClientData[i].ScanKey + Quote
                    + '); return false;">Select</button>'
                    + "</td><td>"
                    + ClientData[i].ClientLocationID
                    + "</td><td>"
                    + ClientData[i].txtClientName
                    + "</td><td>"
                    + ClientData[i].txtLocationName
                    + "</td><td>"
                    + ClientData[i].txtStoreNumber + " " + ClientData[i].txtStoreSuffix + " " + ClientData[i].txtClientAddress
                    + "</td></tr>";
            }
            OutputHTML = "<table id='XX'>" + HeaderText + BodyText + "</table>"
            var SearchResults = $get("<%= pnlSearchResult.ClientID %>");
            SearchResults.innerHTML = OutputHTML;
        }

    </script>
</asp:Content>


