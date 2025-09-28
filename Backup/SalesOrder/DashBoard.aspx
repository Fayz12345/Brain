<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DashBoard.aspx.cs" Inherits="BW_WebApp.SalesOrder.DashBoard" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="cMC" runat="server">

    <asp:HiddenField ID="hdnAddressUpdated" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnSODetailID" runat="server" />
    <asp:HiddenField ID="hdnUserName" runat="server" ClientIDMode="Static" />

    <h1><asp:Label ID="lblPageTitle" runat="server" Text="Sales Orders" /></h1>

    <asp:Panel ID="SalesOrderSection" runat="server">
        <asp:Panel ID="pnlmainentry" runat="server">
            <asp:TabContainer runat="server" ID="tabMain" CssClass="tab-container" ActiveTabIndex="0" AutoPostBack="True">
                <asp:TabPanel runat="server" ID="TabPanelNew" CssClass="tab-panel" Enabled="true" HeaderText="Open" Visible="true">
                    <ContentTemplate>
                        <asp:Button ID="btnNew" runat="server" Text="New" />
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel runat="server" ID="TabPanelPick" CssClass="tab-panel" Enabled="true" HeaderText="Pick/Pack" Visible="true">
                    <ContentTemplate></ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel runat="server" ID="TabPanelShip" CssClass="tab-panel" Enabled="true" HeaderText="Ship" Visible="False">
                    <ContentTemplate>
                        <label>Courier:</label>
                        <asp:DropDownList ID="drpCourier" runat="server" />
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel runat="server" ID="TabPanelSearch" CssClass="tab-panel" Enabled="true" HeaderText="Search" Visible="True">
                    <ContentTemplate>
                        <h3><asp:Label runat="server" Text="Search Orders" /></h3>
                        <asp:Panel runat="server" ID="pnlParameters">
                            
                            <div class="row">
                            	<div class="col-md-6">
                                    <label>Status:</label>
                                    <asp:DropDownList ID="drpStatus" runat="server" />
                            
                                    <asp:CheckBox ID="chkReceived" CssClass="d-block mb-1" runat="server" Text="Order Begin/End Date:"
                                        ToolTip="If unchecked, this will be excluded from the filter" />
                                    <div class="form-row">
                            	        <div class="col">
                                            <asp:TextBox ID="txtBeginDate_s" runat="server" />
                                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtBeginDate_s" />
                                        </div>
                            	        <div class="col">
                                            <asp:TextBox ID="txtEndDate_s" runat="server" />
                                            <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtEndDate_s" />
                                        </div>
                                    </div>
                        
                                    <label>IFS Order Number:</label>
                                    <asp:TextBox ID="txtIFSOrderNumber_s" runat="server" ToolTip="IFS Order Number" />
                        
                                    <label>Order Number:</label>
                                    <asp:TextBox ID="txtOrderNumber_s" runat="server" ToolTip="Order Number" />
                        
                                    <label>Client Name:</label>
                                    <asp:TextBox ID="txtClient_s" runat="server" ToolTip="Client Name" />
                                
                                    <label>Customer PO:</label>
                                    <asp:TextBox ID="txtCustomerPO_s" runat="server" ToolTip="Customer PO" />
                                </div>
                                <div class="col-md-6">
                                    <label>City:</label>
                                    <asp:TextBox runat="server" ID="txtCity_s" ToolTip="City" />
                        
                                    <label>WayBill Number:</label>
                                    <asp:TextBox ID="txtWaybillNumber_s" runat="server" ToolTip="Waybill Number" />
                        
                                    <label>Postal Code:</label>
                                    <asp:TextBox runat="server" ID="txtPostalCode_s" ToolTip="Postal Code" />

                                    <label>Project Tag:</label>
                                    <asp:TextBox ID="txtProjectTag_s" runat="server" ToolTip="Project Tag" />
                        
                                    <label>Phone Number:</label>
                                    <asp:TextBox runat="server" ID="txtPhoneNumber_s" ToolTip="Phone Number" />
                        
                                    <label>Email Address:</label>
                                    <asp:TextBox runat="server" ID="txtEmailAddress_s" ToolTip="Email Address" />
                                </div>
                            </div>
                            <asp:Label ID="lblMessage" runat="server" />

                        </asp:Panel>
                        <hr>
                        <div class="row">
                        	<div class="col-md">
                                <asp:Button ID="btnSearch_Order" runat="server" Text="Search" />
                            </div>
                            <div class="col-md">
                                <div class="input-group">
                                    <asp:TextBox runat="server" ID="txtEmailResultsAddress" ToolTip="Email Address" placeholder="Email" />
                                    <div class="input-group-append">
                                        <asp:Button ID="btnEmail_Details" runat="server" Text="Email Results" />
                                    </div>
                                </div>
                            </div>
                        </div>


                        <asp:GridView ID="grdTempDetail" CssClass="table" runat="server" DataKeyField="OrderHeaderID" AutoGenerateColumns="False">
                            <Columns>
                                <asp:TemplateField HeaderText="P">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="imgOpen" CssClass="btn btn-default" runat="server" ToolTip="Open" CommandArgument="Open">
                                            <span class="oi oi-plus"></span>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="imgPrint" CssClass="btn btn-default" runat="server" ToolTip="Print" CommandArgument="Print">
                                            <span class="oi oi-print"></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Status" HeaderText="Status" ReadOnly="True" />
                                <asp:BoundField DataField="QTY" HeaderText="QTY" ReadOnly="True" />
                                <asp:BoundField DataField="QTYPacked" HeaderText="Packed" ReadOnly="True" />
                                <asp:BoundField DataField="OrderNumber" HeaderText="Order Number" ReadOnly="True" />
                                <asp:BoundField DataField="IFSOrderNo" HeaderText="IFS Order#" ReadOnly="True" />
                                <asp:BoundField DataField="CustomerPO" HeaderText="Customer PO" ReadOnly="True" />
                                <asp:BoundField DataField="WayBillNumber" HeaderText="Waybill" ReadOnly="True" />
                                <asp:BoundField DataField="ProjectTag" HeaderText="Project Tag" ReadOnly="True" />
                                <asp:BoundField DataField="OrderDate" HeaderText="Order Date" ReadOnly="True" />
                                <asp:BoundField DataField="CreateDate" HeaderText="Create Date" ReadOnly="True" />
                                <asp:BoundField DataField="PickPackDate" HeaderText="Pick/Pack" ReadOnly="True" />
                                <asp:BoundField DataField="ShippedDate" HeaderText="Ship Date" ReadOnly="True" />
                                <asp:BoundField DataField="CompanyName" HeaderText="Company" ReadOnly="True" />
                                <asp:BoundField DataField="ContactName" HeaderText="Contact" ReadOnly="True" />
                                <asp:BoundField DataField="AddressLine1" HeaderText="Address 1" ReadOnly="True" />
                                <asp:BoundField DataField="AddressLine2" HeaderText="Address 2" ReadOnly="True" />
                                <asp:BoundField DataField="City" HeaderText="City" ReadOnly="True" />
                                <asp:BoundField DataField="PostalCode" HeaderText="Postal Code" ReadOnly="True" />
                                <asp:BoundField DataField="PhoneNumber" HeaderText="Phone Number" ReadOnly="True" />
                                <asp:BoundField DataField="FaxNumber" HeaderText="Fax Number" ReadOnly="True" />
                                <asp:BoundField DataField="EmailAddress" HeaderText="Email" ReadOnly="True" />
                            </Columns>
                        </asp:GridView>

                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel runat="server" ID="TabFilter" CssClass="tab-panel" Enabled="true" HeaderText="Filter" Visible="True">
                    <ContentTemplate>
                        <div class="row">
                        	<div class="col-md-6">
                                <asp:CheckBox ID="chkFilterOrderDate" CssClass="d-block mb-1" runat="server" Text="Filter List by Order Date:"
                                    ToolTip="Filter on Order Entry Date" />
                                <div class="form-row">
                        	        <div class="col">
                                        <asp:TextBox ID="OrderStartDate" runat="server" ToolTip="Order Entry Filter Start Date" />
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="OrderStartDate" Format="MM/dd/yyyy" />
                                    </div>
                                    <div class="col">
                                        <asp:TextBox ID="OrderEndDate" runat="server" ToolTip="Order Entry Filter End Date" />
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="OrderEndDate" Format="MM/dd/yyyy" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel runat="server" ID="TabMove" CssClass="tab-panel" Enabled="true" HeaderText="Move" Visible="True">
                    <ContentTemplate>
                        <div class="row">
                        	<div class="col-md-6">
                                <label>Move Order Number:</label>
                                <asp:TextBox ID="txtOrderNumber" runat="server" ToolTip="Order Number" />

                                <label>To:</label>
                                <asp:DropDownList ID="drpMoveList" runat="server">
                                    <asp:ListItem Text="New" />
                                    <asp:ListItem Text="Pick/Pack" />
                                    <asp:ListItem Text="Ship" />
                                    <asp:ListItem Text="Done" />
                                    <%--<asp:ListItem Text="Trash" />--%>
                                </asp:DropDownList>

                                <asp:Button ID="btnPanelMove" runat="server" Text="Go" ToolTip="Move Order number" />
                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" TargetControlID="btnPanelMove"
                                    ConfirmText="Are you sure you want to Make this move?" />
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>

            <asp:Panel ID="pnlHeaderArea" runat="server">
                <asp:GridView ID="GridView2" CssClass="table" runat="server" DataKeyField="OrderHeaderID" AutoGenerateColumns="False">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEdit" runat="server">Edit</asp:LinkButton>
                                <asp:LinkButton ID="imgPrint" runat="server">Print</asp:LinkButton>
                                <asp:LinkButton ID="btnInvoice" runat="server">Invoice</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="RequestUser" HeaderText="Request User" ReadOnly="True" Visible="True" />
                        <asp:BoundField DataField="OrderNumber" HeaderText="Order" ReadOnly="True" Visible="True" />
                        <asp:BoundField DataField="IFSOrderNo" HeaderText="IFSOrder" ReadOnly="True" Visible="True" />
                        <asp:BoundField DataField="MiscDesc" HeaderText="Note" ReadOnly="True" Visible="True" />
                        <asp:BoundField DataField="Site" HeaderText="Site" ReadOnly="True" Visible="True" />
                        <asp:BoundField DataField="CustomerPO" HeaderText="SO" ReadOnly="True" Visible="True" />
                        <asp:BoundField DataField="OrderDate" HeaderText="Order Date" ReadOnly="True" Visible="True" />
                        <asp:BoundField DataField="PickPackDate" HeaderText="Pick/Pack Date" ReadOnly="True" Visible="True" />
                        <asp:BoundField DataField="ShippedDate" HeaderText="Shipped Date" ReadOnly="True" Visible="True" />
                        <asp:BoundField DataField="WaybillNumber" HeaderText="Waybill" ReadOnly="True" Visible="True" />
                        <asp:BoundField DataField="CreateDate" HeaderText="Create Date" ReadOnly="True" Visible="True" />
                        <asp:TemplateField HeaderText="Move">
                            <ItemTemplate>
                                <asp:DropDownList ID="drpMoveTo" runat="server">
                                    <asp:ListItem Text="Move" />
                                    <asp:ListItem Text="Open" />
                                    <asp:ListItem Text="Pick/Pack" />
                                    <asp:ListItem Text="Ship" />
                                    <asp:ListItem Text="Done" />
                                    <asp:ListItem Text="Trash" />
                                </asp:DropDownList>
                                <asp:LinkButton ID="btnMove" runat="server" CommandArgument="<%# Container.DataItemIndex %>">Move</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </asp:Panel>
        </asp:Panel>

        <asp:Panel ID="pnlNew" runat="server">
            <h3><asp:Label ID="lblAddOrder" runat="server" Text="Add" /></h3>
            <%--<asp:Button ID="btnScanPack" runat="server" Text="Scan Pick" />--%>
            
            <asp:HiddenField ID="hdnOrderHeaderID" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnOrderQTYTotal" runat="server" Value="0" />
            <asp:HiddenField ID="hdnOrderQTYTotalLeftToPick" runat="server" Value="0" />
            <asp:HiddenField ID="hdnOrderQTYTotalPicked" runat="server" Value="0" />
            
            <div class="row">

                <div class="col-md-4">
                    <label>Customer PO Number:</label>
                    <asp:TextBox ID="txtCustomerPONumber" runat="server" MaxLength="50" />
                </div>
                <div class="col-md-4">
                    <label>Reference:</label>
                    <asp:HiddenField ID="hdntxtReference" runat="server" ClientIDMode="Static" />
                    <asp:TextBox ID="txtReference" runat="server" MaxLength="50" />
                </div>
                <div class="col-md-4">
                    <label>Sales Order Number:</label>
                    <div class="form-control bg-light">
                        <asp:Label ID="lblPurchaseOrderNumber" runat="server" />
                    </div>
                </div>

            	<div class="col-md">

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

                    <label>Scankey:</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtBillClient" runat="server" MaxLength="20" />
                        <div class="input-group-append">
                            <asp:LinkButton ID="btnBillClientSearch" CssClass="btn btn-default" runat="server">
                                <span class="oi oi-arrow-circle-right"></span>
                            </asp:LinkButton>
                        </div>
                    </div>

                    <label>Name/Address:</label>
                    <asp:TextBox ID="txtBillNameAddresstext" runat="server" ReadOnly="True" TextMode="MultiLine" />

                    <asp:LinkButton ID="imgSearchClientLocation" CssClass="btn btn-default" runat="server" OnClientClick="OpenClientSearch(); return false;"
                        ToolTip="Search Clients">
                        <span class="oi oi-magnifying-glass"></span>
                    </asp:LinkButton>

                    <asp:Button ID="btnBillClientEdit" runat="server" Text="Edit" />                                                                            

                    <asp:LinkButton ID="btnSetShipToFromClient" CssClass="btn btn-default" runat="server">
                        <span class="oi oi-arrow-circle-right"></span>
                    </asp:LinkButton>

                    <label class="d-block">Internal Note:</label>
                    <asp:HiddenField ID="hdnInternalNote" runat="server" ClientIDMode="Static" />
                    <asp:TextBox ID="txtInternalNote" runat="server" TextMode="MultiLine" MaxLength="500" />
            
                    <asp:Button ID="btnGenerateSalesOrder" runat="server" Text="Generate Sales Order" />
                    <hr class="d-md-none">
                </div>

                <div class="col-md">
                    
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

                    <label>Scankey:</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtShipClient" runat="server" MaxLength="20" />
                        <div class="input-group-append">
                            <asp:LinkButton ID="btnShipClientSearch" CssClass="btn btn-default" runat="server">
                                <span class="oi oi-arrow-circle-right"></span>
                            </asp:LinkButton>
                        </div>
                    </div>

                    <label>Ship To Name/Address:</label>
                    <asp:TextBox ID="txtShipNameAddresstext" runat="server" ReadOnly="True" TextMode="MultiLine" />

                    <asp:LinkButton ID="imgSearchShipToLocation" CssClass="btn btn-default" runat="server" OnClientClick="OpenClientSearch(); return false;" ToolTip="Search">
                        <span class="oi oi-magnifying-glass"></span>
                    </asp:LinkButton>
                    <asp:Button ID="btnShipClientEdit" runat="server" Text="Edit" />

                    <label class="d-block">Delivery Note:</label>
                    <asp:HiddenField ID="hdnDeliveryNote" runat="server" ClientIDMode="Static" />
                    <asp:TextBox ID="txtDeliveryNote" runat="server" TextMode="MultiLine" MaxLength="500" />
            
                </div>

            </div>

            <asp:GridView ID="grdSODetail" CssClass="table" runat="server" DataKeyField="SODetailID" AutoGenerateColumns="False" AllowPaging="false"
                AllowSorting="false">
                <Columns>
                    <asp:BoundField DataField="Project_ID" HeaderText="Project_ID" ReadOnly="True" Visible="True" />
                    <asp:BoundField DataField="Line_NO" HeaderText="Line #" ReadOnly="True" Visible="True" />
                    <asp:BoundField DataField="QTY" HeaderText="QTY" ReadOnly="True" Visible="True" />
                    <asp:BoundField DataField="QTYPacked" HeaderText="Packed" ReadOnly="True" Visible="True" />
                    <asp:BoundField DataField="IFSSKU" HeaderText="SKU" ReadOnly="True" Visible="True" />
                    <asp:BoundField DataField="Location" HeaderText="Location" ReadOnly="True" Visible="True" />
                    <asp:BoundField DataField="Condition" HeaderText="Cond" ReadOnly="True" Visible="True" />
                    <asp:BoundField DataField="OrderDetailID" HeaderText="ID" ReadOnly="True" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="btnPack" runat="server" CommandArgument="<%# Container.DataItemIndex %>">View</asp:LinkButton>
                            <%--<asp:HiddenField ID="hdnOrderNumber" runat="server" />
                            <asp:HiddenField ID="hdnQTY" runat="server" />
                            <asp:HiddenField ID="hdnStockID" runat="server" />
                            <asp:HiddenField ID="hdnManufacturer" runat="server" />
                            <asp:HiddenField ID="hdnModel" runat="server" />
                            <asp:HiddenField ID="hdnColour" runat="server" />
                            <asp:HiddenField ID="hdnGrade" runat="server" />
                            <asp:HiddenField ID="hdnCondition" runat="server" />
                            <asp:HiddenField ID="hdnCarrier" runat="server" />--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <hr>

            <asp:HiddenField ID="hdnSOHeaderID" runat="server" />
            <asp:HiddenField ID="hdnCurrentPage" runat="server" Value="0" />
            <asp:HiddenField ID="hdnMaxPage" runat="server" Value="0" />

            <div class="row">
            	<div class="col">
                    <asp:LinkButton ID="btnPageDown" runat="server" CssClass="btn btn-default">
                        <span class="oi oi-arrow-circle-left"></span>
                    </asp:LinkButton>
                    <asp:Label ID="lblPage" runat="server" Text="Page 1/10" />
                    <asp:LinkButton ID="btnPageUp" runat="server" CssClass="btn btn-default">
                        <span class="oi oi-arrow-circle-right"></span>
                    </asp:LinkButton>
                </div>
                <div class="col text-right">
                    <asp:Button ID="btnNewOK" runat="server" Text="OK" />
                </div>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlEditNameAddress" runat="server">
            <h3><asp:Label ID="lblEditAddress" runat="server" Text="Edit Address" /></h3>
            <div class="row">
            	<div class="col-md">
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
                </div>
                <div class="col-md">
                    <label>Zip / Postal Code:</label>
                    <asp:TextBox ID="txtPostalCode" runat="server" MaxLength="20" />
            
                    <label>Phone:</label>
                    <asp:TextBox ID="txtPhoneNumber" runat="server" MaxLength="30" />
            
                    <label>Fax:</label>
                    <asp:TextBox ID="txtFaxNumber" runat="server" MaxLength="30" />
            
                    <label>Notes:</label>
                    <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" />
                </div>
            </div>

            <asp:HiddenField ID="hdnClientType" runat="server" />
                <asp:Button ID="btnEditNameAddressOK" runat="server" Text="OK" />
                <asp:Button ID="btnEditNameAddressCancel" runat="server" Text="Cancel" />
        </asp:Panel>

    </asp:Panel>

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

        function pageLoad() {
            var manager = Sys.WebForms.PageRequestManager.getInstance();
            manager.add_endRequest(endRequest);
        }

        // Search Order Screen Code

        function OpenOrder(ID, Status) {

            if (ID.length == 0 || Status.length == 0) { return; }

            //var pstring = GetParameterStream(GetReportParameterList("CLIENTSUBMIT"));
            //var pstring = "ESN=A00000291E3942&ID=3251&PID=1"

            var pstring = "ID=" + ID + "&Status=" + Status;
            //var pstring = "ESN=A00000291E3942&ID=3251&PID=5"


            // var WindowToOpen = "RPT_SpotCountReport.aspx";
            var WindowToOpen = "OrderEntry.aspx";

            if (pstring.length > 0) {
                WindowToOpen = WindowToOpen + "?" + pstring
            }
            var win = window.open(WindowToOpen, "_blank", "", true);
            //var win = window.open(WindowToOpen, "_blank", "menubar", true);
            return;
        }

        function OKToProceed() {
            return false;
        }

        function ExportPickPackingShipReport(RPT, ID) {

            //            alert("Print " + RPT);
            //            return;

            var xDataList = {};
            xDataList["RPT"] = RPT;
            xDataList["ID"] = ID.toString();
            //           var pstring = GetParameterStream(GetReportParameterList("Bagtag"));
            var pstring = GetParameterStream(xDataList);
            // var WindowToOpen = "RPT_SpotCountReport.aspx";
            var WindowToOpen = "/Reports/RPT_EXCEL_Out.aspx";
            if (pstring.length > 0) {
                WindowToOpen = WindowToOpen + "?" + pstring
            }
            //            alert('xxxxx' + WindowToOpen);
            var win = window.open(WindowToOpen, "_blank", "menubar", true);
            return;
        }

        ////////////////////////////////////////////////////////////
        // *****************************************************************************
        //        function OpenAuthorization() {
        //            $find('< %=this.winGetPassword.ClientID%>').Title = "Authorization Required";
        //            $find('< %=this.winGetPassword.ClientID%>').Open(null, null);
        //        }

        //        function AuthorizeCancel() {
        //            $find('< %=winGetPassword.ClientID%>').Close();
        //        }

        //        function ShouldDisableButton(btn) {
        //            // var btn = $get(btnName);
        //            var isDisabled = $get("< %= hdnIsDisabled.ClientID %>");
        //            if (isDisabled.value != 'No') {
        //                return false;
        //            }
        //            isDisabled.value = 'Yes';
        //            return true;
        //        }

        function DisableButton(btn) {
            // var btn = $get(btnName);
            btn.disabled = true;
            return false;
        }

        function endRequest(sender, args) {
            window.scrollTo(0, 0);
        }

        function LTrim(value) {

            var re = /\s*((\S+\s*)*)/;
            return value.replace(re, "$1");

        }

        // Removes ending whitespaces
        function RTrim(value) {

            var re = /((\s*\S+)*)\s*/;
            return value.replace(re, "$1");

        }

        // Removes leading and ending whitespaces
        function trim(value) {

            return LTrim(RTrim(value));

        }

        //        function RecordScanKey() {
        //            var newESN = MCL('ESN').value;
        //            if (newESN.length == 0) { return; }

        //            var Carton = MCL('Carton').value;
        //            var IFSLocation = MCL('IFSLocation').value;
        //            var UserName = MCL('UserName').value;
        //            var OrderHeaderID = MCL('ORDERHEADERID').value;
        //            MCL('ESN').value = '';

        //            Carton = trim(Carton);
        //            if (Carton.length == 0) {
        //                alert("Carton required!");
        //                return;
        //            }
        //            IFSLocation = trim(IFSLocation);
        //            if (IFSLocation.length == 0) {
        //                alert("IFS Location required!");
        //                return;
        //            }
        //            newESN = trim(newESN);
        //            if (newESN.length == 0) {
        //                alert("ESN required!");
        //                return;
        //            }

        //            var count = $get("< %= txtCount.ClientID %>").value;
        //            var iCount = parseInt(count);
        //            iCount++;
        //            $get("< %= txtCount.ClientID %>").value = iCount.toString();

        //            var service = new WebServer_01();
        //            service.SalesOrderESNPicked(OrderHeaderID, IFSLocation, Carton, newESN, UserName, onPickIMEISuccess, onPickIMEIError, null);

        //            return;

        //        }

        function onPickIMEIError(Result) {
            alert("Error:" + Result.get_message());
        }

        function onPickIMEISuccess(result) {
            var B = result.split(":")
            if (B[0] == "Error") {
                alert(result);
                return;
            }
            AddToOutput(result);
            return;
        }

        function AddToOutput(result) {
            var B = result.split(":")
            MCL('LBLTOTALQTY').innerHTML = B[0];
            MCL('LBLTOTALPICKED').innerHTML = B[1];
            MCL('LBLTOTALREMAINING').innerHTML = B[2];

            var OUTPUTAREA = MCL('OUTPUTAREA').value;
            OUTPUTAREA = result + '/' + OUTPUTAREA;
            //OUTPUTAREA = resultB[3] + '/' + OUTPUTAREA;
            MCL('OUTPUTAREA').value = OUTPUTAREA;
        }

        function ExportPickPackingShipReport(RPT, ID) {
            var xDataList = {};
            xDataList["RPT"] = RPT;
            xDataList["ID"] = ID.toString();
            //           var pstring = GetParameterStream(GetReportParameterList("Bagtag"));
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

        function GetDropDownValue(Name) {
            var IndexValue = $get(Name).selectedIndex;
            var xValue = "";
            if (IndexValue > -1) {
                xValue = $get(Name).options[IndexValue].value;
            }
            return xValue;
        }

        function GetDropDownText(Name) {
            var IndexValue = $get(Name).selectedIndex;
            var xValue = "";
            if (IndexValue > -1) {
                xText = $get(Name).options[IndexValue].text;
            }
            return xText;
        }

        function addOption(selectbox, value, text, SelectedValue) {
            var optn = document.createElement("OPTION");
            optn.text = text;
            optn.value = value;
            if (value == SelectedValue) {
                optn.setAttribute("selected", "selected");
            }
            selectbox.options.add(optn);
        }

        //**************************************************************************************

        function MCL(ControlName) {
            switch (ControlName.toUpperCase()) {
                //                case "USERNAME": return $get("< %= hdnUserName.ClientID %>"); break; 
                //                case "ESN": return $get("< %= ScanKey.ClientID %>"); break; 
                //                case "CARTON": return $get("< %= txtSku.ClientID %>"); break; 
                //                case "IFSLOCATION": return $get("< %= txtIFSLocation.ClientID %>"); break; 
                //                case "ORDERHEADERID": return $get("< %= hdnOrderDetailID.ClientID %>"); break; 
                //                case "OUTPUTAREA": return $get("< %= txtESNScan.ClientID %>"); break; 


                //                case "LBLTOTALQTY": return $get("< %= lblTotalQTY.ClientID %>"); break; 
                //                case "LBLTOTALPICKED": return $get("< %= lblTotalPicked.ClientID %>"); break; 
                //                case "LBLTOTALREMAINING": return $get("< %= lblTotalRemaining.ClientID %>"); break; 


                //                case "BTNSHIPCLIENTSEARCH": return $get("< %= btnShipClientSearch.ClientID %>"); break;  



                default: return null;
            }
        }

    </script>
</asp:Content>

