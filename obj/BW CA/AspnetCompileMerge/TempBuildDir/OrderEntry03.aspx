<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrderEntry03.aspx.cs" Inherits="BW_WebApp.OrderEntry03" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cMC" runat="server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
        <Scripts>
            <asp:ScriptReference Path="~/Scripts/ReceiveSpecific.js" NotifyScriptLoaded="true" />
        </Scripts>
    </asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdnAddressUpdated" Value="" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnOrderTotalCountRequired" runat="server" Value="0" />
            <asp:HiddenField ID="hdnOrderDetailID" runat="server" Value="" />
            <asp:HiddenField ID="hdnCountRequired" runat="server" Value="0" />
            <asp:HiddenField ID="hdnUserName" runat="server" ClientIDMode="Static" />

            <asp:HiddenField ID="hdnCarrierID" runat="server" ClientIDMode="Static" Value="-1"/>
            <asp:HiddenField ID="hdnManufacturerID" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnModelID" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnColourID" runat="server" ClientIDMode="Static" Value="-1" />
            <asp:HiddenField ID="hdnGradeID" runat="server" ClientIDMode="Static" />

            <asp:HiddenField ID="hdnProjtagESNlist" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="OrderDetailLineID" runat="server" />

            <asp:Panel ID="pnlmainentry" runat="server">
            
                <asp:Button ID="btnRefeshx" runat="server" Text="Refresh" />
                <asp:TabContainer runat="server" ID="tabMain" CssClass="tab-container" ActiveTabIndex="0" AutoPostBack="True">

                    <asp:TabPanel runat="server" ID="TabPanelNew" CssClass="tab-panel" Enabled="true" HeaderText="New" Visible="False">
                        <ContentTemplate>
                            <asp:Button ID="btnNew" runat="server" Text="New" />
                        </ContentTemplate>
                    </asp:TabPanel>

                    <asp:TabPanel runat="server" ID="TabPanelPick" CssClass="tab-panel" Enabled="true" HeaderText="Pick/Pack" Visible="False">
                        <ContentTemplate><br /></ContentTemplate>
                    </asp:TabPanel>

                    <asp:TabPanel runat="server" ID="TabPanelShip" CssClass="tab-panel" Enabled="true" HeaderText="Ship" Visible="False">
                        <ContentTemplate>
                            <label>Courier:</label>
                            <asp:DropDownList ID="drpCourier" CssClass="w-md-50" runat="server" />
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel runat="server" ID="TabPanelDone" CssClass="tab-panel" Enabled="true" HeaderText="Done" Visible="False">
                        <ContentTemplate><br />
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel runat="server" ID="TabPanelTrash" CssClass="tab-panel" Enabled="true" HeaderText="Trash" Visible="False">
                        <ContentTemplate><br />
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel runat="server" ID="TabPanelSearch" CssClass="tab-panel" Enabled="true" HeaderText="Search" Visible="True">
                        <ContentTemplate>
                            <h3><asp:Label ID="Label1" runat="server" Text="Search Orders" /></h3>
                            <asp:Panel runat="server" ID="pnlParameters" CssClass="w-md-50">
                                <label>Status:</label>
                                <asp:DropDownList ID="drpStatus" runat="server" />

                                <div class="form-check-inline">
                                    <asp:CheckBox ID="chkReceived" runat="server" ToolTip="If unchecked, this will be excluded from the filter" />
                                    <label>Order Begin/End Date:</label>
                                </div>

                                <asp:TextBox ID="txtBeginDate_s" runat="server"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtBeginDate_s" />

                                <asp:TextBox ID="txtEndDate_s" runat="server"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtEndDate_s" />

                                <label>Acc Order Number:</label>
                                <asp:TextBox ID="txtIFSOrderNumber_s" runat="server" ToolTip="Accounting Order Number" />

                                <label>Order Number:</label>
                                <asp:TextBox ID="txtOrderNumber_s" runat="server" ToolTip="Order Number" />

                                <label>Client Name:</label>
                                <asp:TextBox ID="txtClient_s" runat="server" ToolTip="Client Name" />

                                <label>Customer PO:</label>
                                <asp:TextBox ID="txtCustomerPO_s" runat="server" ToolTip="Customer PO" />

                                <label>City:</label>
                                <asp:TextBox runat="server" ID="txtCity_s" ToolTip="City" />

                                <label>WayBill Number:</label>
                                <asp:TextBox ID="txtWaybillNumber_s" runat="server" ToolTip="Waybill Number" />

                                <label>Postal Code:</label>
                                <asp:TextBox runat="server" ID="txtPostalCode_s" ToolTip="Postal Code" />

                                <label>Country:</label>
                                <asp:TextBox runat="server" ID="TxtCountry_s" ToolTip="Country" />


                                <label>Project Tag:</label>
                                <asp:TextBox ID="txtProjectTag_s" runat="server" ToolTip="Project Tag" />

                                <label>Phone Number:</label>
                                <asp:TextBox runat="server" ID="txtPhoneNumber_s" ToolTip="Phone Number" />

                                <label>Email Address:</label>
                                <asp:TextBox runat="server" ID="txtEmailAddress_s" ToolTip="Email Address" />

                                <asp:Label ID="lblMessage" runat="server" Text="" />
                            </asp:Panel>
                            <asp:Button ID="btnSearch_Order" runat="server" Text="Search" />
                            <asp:Button ID="btnEmail_Details" runat="server" Text="Email Results" />
                            <asp:TextBox runat="server" ID="txtEmailResultsAddress" ToolTip="Email Address" />
                            <asp:GridView ID="grdTempDetail" CssClass="table" runat="server" DataKeyField="OrderHeaderID" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:TemplateField HeaderText="P">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="imgOpen" CssClass="btn btn-default p-1" runat="server" ToolTip="Open" CommandArgument="Open">
                                                <span class="oi oi-info"></span>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="imgPrint" CssClass="btn btn-default p-1" runat="server" ToolTip="Print" CommandArgument="Print">
                                                <span class="oi oi-print"></span>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Status" HeaderText="Status" ReadOnly="True" />
                                    <asp:BoundField DataField="QTY" HeaderText="QTY" ReadOnly="True" />
                                    <asp:BoundField DataField="QTYPacked" HeaderText="Packed" ReadOnly="True" />
                                    <asp:BoundField DataField="OrderNumber" HeaderText="Order Number" ReadOnly="True" />
                                    <asp:BoundField DataField="IFSOrderNo" HeaderText="Acc Order#" ReadOnly="True" />
                                    <asp:BoundField DataField="CustomerPO" HeaderText="Customer PO" ReadOnly="True" />
                                    <asp:BoundField DataField="WayBillNumber" HeaderText="Waybill" ReadOnly="True" />
                                    <asp:BoundField DataField="ProjectTag" HeaderText="Project Tag" ReadOnly="True" />
                                    <asp:BoundField DataField="OrderDate" HeaderText="Order Date" ReadOnly="True" />
                                    <asp:BoundField DataField="CreateDate" HeaderText="Create Date" ReadOnly="True" />
                                    <asp:BoundField DataField="PickPackDate" HeaderText="Pick/Pack" ReadOnly="True" />
                                    <asp:BoundField DataField="ShippedDate" HeaderText="Ship Date" ReadOnly="True" />
                                    <%--<asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="True" />--%>
                                    <asp:BoundField DataField="CompanyName" HeaderText="Company" ReadOnly="True" />
                                    <asp:BoundField DataField="ContactName" HeaderText="Contact" ReadOnly="True" />
                                    <asp:BoundField DataField="AddressLine1" HeaderText="Address 1" ReadOnly="True" />
                                    <asp:BoundField DataField="AddressLine2" HeaderText="Address 2" ReadOnly="True" />
                                    <asp:BoundField DataField="City" HeaderText="City" ReadOnly="True" />
                                    <asp:BoundField DataField="PostalCode" HeaderText="Postal Code" ReadOnly="True" />
                                    <asp:BoundField DataField="Country" HeaderText="Country" ReadOnly="True" />
                                    <asp:BoundField DataField="PhoneNumber" HeaderText="Phone Number" ReadOnly="True" />
                                    <asp:BoundField DataField="FaxNumber" HeaderText="Fax Number" ReadOnly="True" />
                                    <asp:BoundField DataField="EmailAddress" HeaderText="Email" ReadOnly="True" />
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:TabPanel>

                    <asp:TabPanel runat="server" ID="TabFilter" CssClass="tab-panel" Enabled="true" HeaderText="Filter" Visible="True">
                        <ContentTemplate>
                            <div class="w-md-50">
                                <asp:CheckBox ID="chkFilterOrderDate" CssClass="d-block mb-1" runat="server" Text="Filter List by Order Date:" ToolTip="Filter on Order Entry Date" />
                                <asp:TextBox ID="OrderStartDate" runat="server" ToolTip="Order Entry Filter Start Date" />
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="OrderStartDate" Format="MM/dd/yyyy" />
                                <asp:TextBox ID="OrderEndDate" runat="server" ToolTip="Order Entry Filter End Date" />
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="OrderEndDate" Format="MM/dd/yyyy" />
                            </div>
                        </ContentTemplate>
                    </asp:TabPanel>

                    <asp:TabPanel runat="server" ID="TabMove" CssClass="tab-panel" Enabled="true" HeaderText="Move" Visible="True">
                        <ContentTemplate>
                            <div class="w-md-50">
                                <asp:Label ID="Label2" runat="server" Text="Move Order Number:" />
                                <asp:TextBox ID="txtOrderNumber" runat="server" ToolTip="Order Number" />

                                <asp:Label ID="Label3" runat="server" Text="To:" />
                                <asp:DropDownList ID="drpMoveList" runat="server">
                                    <asp:ListItem Text="New" />
                                    <asp:ListItem Text="Pick/Pack" />
                                    <asp:ListItem Text="Ship" />
                                    <asp:ListItem Text="Done" />
                                    <%--<asp:ListItem Text="Trash" />--%>
                                </asp:DropDownList>

                                <asp:Button ID="btnPanelMove" runat="server" Text="Go" ToolTip="Move Order number" />
                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnPanelMove" ConfirmText="Are you sure you want to Make this move?" />
                            </div>
                        </ContentTemplate>
                    </asp:TabPanel>

                </asp:TabContainer>

                <asp:Panel ID="pnlHeaderArea" CssClass="table-responsive" runat="server">
                    <asp:GridView ID="GridView2" CssClass="table" runat="server" DataKeyField="OrderHeaderID" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField ItemStyle-Wrap="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEdit" CssClass="btn btn-default p-1" runat="server" ToolTip="Edit">
                                        <span class="oi oi-pencil"></span>
                                    </asp:LinkButton>
                                    <%--<asp:LinkButton ID="btnDelete" CssClass="btn btn-default" runat="server">Delete</asp:LinkButton>--%>
                                    <%--<asp:ConfirmButtonExtender runat="server" TargetControlID="btnDelete"
                                        ConfirmText="Are you sure you want to Delete this file?" />--%>
                                    <asp:LinkButton ID="imgPrint" CssClass="btn btn-default p-1" runat="server" ToolTip="Print">
                                        <span class="oi oi-print"></span>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnInvoice" CssClass="btn btn-default p-1" runat="server" ToolTip="dispatch">
                                        <span class="oi oi-spreadsheet"></span>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnInvoicePDF" CssClass="btn btn-default p-1" runat="server" ToolTip="dispatch PDF">
                                        <span class="oi oi-document"></span>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnInvoiceDetail" CssClass="btn btn-default p-1" runat="server" ToolTip="dispatch Detail">
                                        <span class="oi oi-briefcase"></span>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnInvoiceDetail01" CssClass="btn btn-default p-1" runat="server" ToolTip="dispatch Detail B">
                                        <span class="oi oi-action-redo"></span>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="RequestUser" HeaderText="Request User" ReadOnly="True" Visible="True" />--%>
                            <asp:BoundField DataField="OrderNumber" HeaderText="Order" ReadOnly="True" Visible="True" />
                            <asp:BoundField DataField="IFSOrderNo" HeaderText="Acc Order#" ReadOnly="True" Visible="True"/>
                            <asp:BoundField DataField="MiscDesc" HeaderText="Note" ReadOnly="True" Visible="True" />
                            <asp:BoundField DataField="Site" HeaderText="Site" ReadOnly="True" Visible="True" />
                            <asp:BoundField DataField="CustomerPO" HeaderText="SO" ReadOnly="True" Visible="True" />
                            <asp:BoundField DataField="OrderDate" HeaderText="Order Date" ReadOnly="True" Visible="True" />
                            <asp:BoundField DataField="PickPackDate" HeaderText="Pick/Pack Date" ReadOnly="True" Visible="True" />
                            <asp:BoundField DataField="ShippedDate" HeaderText="Shipped Date" ReadOnly="True" Visible="True" />
                            <asp:BoundField DataField="WaybillNumber" HeaderText="Waybill" ReadOnly="True" Visible="True" />
                            <asp:BoundField DataField="CreateDate" HeaderText="Create Date" ReadOnly="True" Visible="True" />
                            <asp:TemplateField HeaderText="Move" ItemStyle-Wrap="False">
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpMoveTo" runat="server">
                                        <asp:ListItem Text="Move" />
                                        <asp:ListItem Text="New" />
                                        <asp:ListItem Text="Pick/Pack" />
                                        <asp:ListItem Text="Ship" />
                                        <%--<asp:ListItem Text="Bill" />--%>
                                        <asp:ListItem Text="Done" />
                                        <asp:ListItem Text="Trash" />
                                        <%--<asp:ListItem Text="Archive" />--%>
                                    </asp:DropDownList>
                                    <asp:LinkButton ID="btnMove" CssClass="btn btn-default" runat="server" CommandArgument="<%# Container.DataItemIndex %>">Move</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>

            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>



    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlNew" runat="server">
                <h2><asp:Label ID="lblAddOrder" runat="server" Text="Add Order" /></h2>
                <asp:Button ID="btnScanPack" runat="server" Text="Scan Pick" />
                <asp:HiddenField ID="hdnOrderHeaderID" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnOrderQTYTotal" runat="server" Value="0" />
                <asp:HiddenField ID="hdnOrderQTYTotalLeftToPick" runat="server" Value="0" />
                <asp:HiddenField ID="hdnOrderQTYTotalPicked" runat="server" Value="0" />
                <asp:HiddenField ID="hdntxtReference" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnBillClientLocationID" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnBillCompanyName" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnBillContactName" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnBillAddressLine1" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnBillAddressLine2" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnBillCity" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnBillStateOrProvince" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnBillPostalCode" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnBillCountry" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnBillPhoneNumber" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnBillFaxNumber" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnBillNotes" runat="server" ClientIDMode="Static" />
<%--
                 <div class="row">
  
                    <div class="col-md">
                        <div>
                            <label>Order Number:</label>
                            <asp:TextBox ID="xxxtxtOrderNumberEdit" runat="server" MaxLength="50" />
                        </div>

                    </div>              
                </div>--%>
               
                <div class="row">
  
                    <div class="col-md">
                        <div>
                            <label>Order Type:</label><br />
                            <asp:DropDownList ID="drpOrdertype" CssClass="w-md-50" runat="server" />
                        </div>
                    </div>    
                    
                    <div class="col-md">
                        <div>
                            <label>Sales Person:</label><br />
                            <%--<asp:TextBox ID="txtSalesPerson" runat="server" MaxLength="50" />--%>
                            <asp:DropDownList ID="drpSalesPerson" CssClass="w-md-50" runat="server" />
                    </div>    
                        </div>                    
                              
                </div>

                <div class="row">
                    <div class="col-md">
                        <div>
                            <label>
                                Customer PO Number:</label>
                            <asp:TextBox ID="txtCustomerPONumber" runat="server" MaxLength="50" />
                        </div>
                        <div>
                            <label>
                                Reference:</label>
                            <asp:TextBox ID="txtReference" runat="server" MaxLength="50" />
                        </div>


                    </div>
                    <div class="col-md">


                        <div>
                            <label>
                                Sales Order Number (PL#):</label>
                            <asp:TextBox ID="txtOrderNumberEdit" runat="server" MaxLength="50" />
                          <%--  <asp:Label ID="lblPurchaseOrderNumber" runat="server" />--%>
                        </div>

                        <div>
                            <label>
                                Restrict to Project Tag:</label>
                            <asp:TextBox ID="txtProjectTag" runat="server" Width="100%"></asp:TextBox>
                        </div>
                        <div>
                            <label>
                                Accounting Order Number:</label>
                            <asp:Label ID="lblIFSOrderNumber" runat="server" />
                        </div>
                        <div>
                            <label>
                                Waybill Number:</label>
                            <asp:HiddenField ID="hdnWaybillNumber" runat="server" ClientIDMode="Static" />
                            <asp:TextBox ID="txtWaybillNumber" runat="server" MaxLength="500" />
                        </div>
                    </div>
                </div>

                <asp:TabContainer runat="server" ID="TabContainer1" CssClass="tab-container" ActiveTabIndex="0" AutoPostBack="False">
                    <asp:TabPanel runat="server" ID="TabPanelxxx" CssClass="tab-panel" Enabled="true" HeaderText="Demographics"
                        Visible="True">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md">
                                    <div>
                                        <label>
                                            Name/Address:</label><br />
                                        <label>
                                            Client Location Scan Key:</label>
                                        <asp:TextBox ID="txtBillClient" runat="server" ToolTip="Enter client location Key. eg: BW1"></asp:TextBox>
                                        <asp:Button ID="btnBillClientSearch" runat="server" Text="Search" />
                                        &nbsp;
                                        <asp:ImageButton ID="btnSearchClient" runat="server" ImageUrl="~/Images/Find_Search_64.png"
                                            OnClientClick="OpenClientSearch('Bill');return false;" Width="15px" ToolTip="Search Clients"
                                            Visible="false" />
                                    </div>
                                    <div>
                                        <asp:TextBox ID="txtBillNameAddresstext" runat="server" Rows="6" ReadOnly="True"
                                            TextMode="MultiLine" />
                                        <asp:Button ID="btnBillClientEdit" CssClass="d-block" runat="server" Text="Edit" />
                                    </div>
                                    <div>
                                        <label>
                                            Internal Note:</label>
                                        <asp:HiddenField ID="hdnInternalNote" runat="server" ClientIDMode="Static" />
                                        <asp:TextBox ID="txtInternalNote" runat="server" Rows="6" TextMode="MultiLine" MaxLength="500" />
                                    </div>
                                    <div>
                                        <label>
                                            Project to pull Inventory:</label>
                                        <asp:DropDownList ID="drpProjectList_New" runat="server" ToolTip="Project" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md">
                                    <div>
                                        <label>
                                            Ship To Name/Address:</label><br />
                                        <label>
                                            Client Location Scan Key:</label>
                                        <asp:TextBox ID="txtShipClient" runat="server" ToolTip="Enter client location Key. eg: BW1"></asp:TextBox>
                                        <asp:Button ID="btnShipClientSearch" runat="server" Text="Search" />
                                        &nbsp;
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Find_Search_64.png"
                                            OnClientClick="OpenClientSearch('Ship');return false;" Width="15px" ToolTip="Search Clients"
                                            Visible="false" />
                                    </div>
                                    <asp:HiddenField ID="hdnShipClientLocationID" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hdnShipCompanyName" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hdnShipContactName" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hdnShipAddressLine1" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hdnShipAddressLine2" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hdnShipCity" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hdnShipStateOrProvince" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hdnShipPostalCode" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hdnShipCountry" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hdnShipPhoneNumber" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hdnShipFaxNumber" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hdnShipNotes" runat="server" ClientIDMode="Static" />
                                    <asp:TextBox ID="txtShipNameAddresstext" runat="server" Rows="6" ReadOnly="True"
                                        TextMode="MultiLine" />
                                    <asp:Button ID="btnAddressToBill" runat="server" Text="<<" ToolTip="Copy Address to Bill To" />
                                    <asp:Button ID="btnAddressToShip" runat="server" Text=">>" ToolTip="Copy Address to Ship To" />
                                    <asp:Button ID="btnShipClientEdit" runat="server" Text="Edit" /><br />
                                    <label>
                                        Delivery Note:</label>
                                    <asp:HiddenField ID="hdnDeliveryNote" runat="server" ClientIDMode="Static" />
                                    <asp:TextBox ID="txtDeliveryNote" runat="server" Rows="6" TextMode="MultiLine" MaxLength="500" />
                                    <div>
                                        <label>
                                            Paid:</label>
                                        <asp:CheckBox ID="chkPaid" runat="server" />
                                        <asp:CheckBox ID="chkPostPaid" runat="server" Visible="False" />
                                    </div>
                        <div>
                            <label>Currency:</label>
                            <asp:DropDownList ID="drpCurrency" CssClass="w-md-50" runat="server" />
                        </div>
                        <div>
                            <label>Freight:</label>
                                    <asp:TextBox ID="txtFreight" runat="server" />
                        </div>
                        <div>
                            <label>Tax Rate:</label>
                                    <asp:TextBox ID="txtTaxRate" runat="server" />
                        </div>
                                    <div>
                                        <label>
                                            Tax:</label>
                                        <asp:LinkButton ID="btnRefresh" CssClass="btn btn-default p-1" runat="server" ToolTip="Refresh">
                                        <span class="oi oi-reload"></span>
                                        </asp:LinkButton>
                                        <asp:TextBox ID="txtTax" runat="server" Enabled="false" />
                                        <asp:Label runat="server" ID="RefreshMessage" ></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel runat="server" ID="TabPanelx1" CssClass="tab-panel" Enabled="true" HeaderText="Detail Lines"
                        Visible="True">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md">
                                    <asp:TextBox ID="txtFromBinSku" runat="server" ToolTip="Enter Sku for added Detail Lines." Visible="false"></asp:TextBox>
                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtFromBinSku"
                                        WatermarkText="Sku">
                                    </asp:TextBoxWatermarkExtender>
                                    <br />
                                    <asp:Button ID="btnAddDetailLine" runat="server" Text="Add New Line" UseSubmitBehavior="False"
                                        class="button" />
                                </div>
                                <div class="col-md">
                                    <asp:TextBox ID="txtFromBin" runat="server" ToolTip="Enter bin number to load line detail from." Visible="false"></asp:TextBox>
                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtFromBin"
                                        WatermarkText="Bin">
                                    </asp:TextBoxWatermarkExtender>
                                    <asp:Button ID="btnFromBin" runat="server" Text="Add from Bin" UseSubmitBehavior="false"
                                        class="button" Visible="false"/>
                                    <asp:Button ID="btnFromBin_B" runat="server" Text="Add from Bin (b)" UseSubmitBehavior="false"
                                        class="button" OnClientClick="OpenAuthorization(); return false;" Visible="false"/>
                                </div>
                            </div>
                            <asp:HiddenField ID="hdnOrderHeaderxxID" runat="server" />
                            <asp:HiddenField ID="hdnCurrentPage" runat="server" Value="0" />
                            <asp:HiddenField ID="hdnMaxPage" runat="server" Value="0" />
                            <asp:LinkButton ID="btnPageDown" CssClass="btn btn-default" runat="server">
                    <span class="oi oi-arrow-circle-left"></span>
                            </asp:LinkButton>
                            <asp:Label ID="lblPage" runat="server" Text="Page 1/10" />
                            <asp:LinkButton ID="btnPageUp" CssClass="btn btn-default" runat="server">
                    <span class="oi oi-arrow-circle-right"></span>
                            </asp:LinkButton>
                            <hr>
                            <div class="table-responsive mb-3">
                                <asp:Panel ID="pnlMainGrid" runat="server" Style="overflow: auto; max-height: 400px;">
                                    <asp:GridView ID="grdNewOrderDetailGrid" CssClass="table" runat="server" DataKeyField="OrderDetailID"
                                        AutoGenerateColumns="False" AllowPaging="false" AllowSorting="false" PageSize="50">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEdit" CssClass="btn btn-default" runat="server" CommandArgument="<%# Container.DataItemIndex %>">Edit</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Project_ID" HeaderText="Project_ID" ReadOnly="True" Visible="True" />
                                            <asp:BoundField DataField="Line_NO" HeaderText="Line #" ReadOnly="True" Visible="True" />
                                            <asp:BoundField DataField="QTY" HeaderText="QTY" ReadOnly="True" Visible="True" />
                                            <asp:BoundField DataField="UnitPrice" HeaderText="UnitPrice" ReadOnly="True" Visible="True" />
                                            <asp:BoundField DataField="QTYPacked" HeaderText="Packed" ReadOnly="True" Visible="True" />
                                            <%--<asp:BoundField DataField="QTYInventoryLinked" HeaderText="Posted" ReadOnly="True" Visible="True" />--%>
                                            <%--<asp:BoundField DataField="IFSSKU" HeaderText="SKU" ReadOnly="True" Visible="True" />--%>
                                            <%--<asp:BoundField DataField="Location" HeaderText="Location" ReadOnly="True" Visible="True" />--%>
                                            <%-- <asp:BoundField DataField="Condition" HeaderText="Cond" ReadOnly="True" Visible="True" />--%>
                                            <asp:BoundField DataField="Desc_Code" HeaderText="Attribute Codes" ReadOnly="True"
                                                Visible="True" />
                                            <asp:BoundField DataField="Desc_Text" HeaderText="Attribute Desc" ReadOnly="True"
                                                Visible="True" />
                                            <asp:BoundField DataField="OrderDetailID" HeaderText="ID" ReadOnly="True" />
                                            <asp:TemplateField HeaderText="Del">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkIsDeleted" runat="server" ToolTip="Check to Delete" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnPack" CssClass="btn btn-default" runat="server" CommandArgument="<%# Container.DataItemIndex %>">View</asp:LinkButton>
                                                    <asp:HiddenField ID="hdnOrderNumber" runat="server" />
                                                    <asp:HiddenField ID="hdnQTY" runat="server" />
                                                    <asp:HiddenField ID="hdnStockID" runat="server" />
                                                    <asp:HiddenField ID="hdnManufacturer" runat="server" />
                                                    <asp:HiddenField ID="hdnModel" runat="server" />
                                                    <asp:HiddenField ID="hdnColour" runat="server" />
                                                    <asp:HiddenField ID="hdnGrade" runat="server" />
                                                    <asp:HiddenField ID="hdnCondition" runat="server" />
                                                    <asp:HiddenField ID="hdnCarrier" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </div>
                        </ContentTemplate>
                    </asp:TabPanel>
                </asp:TabContainer>
                <asp:Button ID="btnNewOK" runat="server" Text="OK" />
                <asp:Button ID="btnPostInventory" runat="server" Text="Serialize" ToolTip="Post Inventory Change from Serialized IMEI data WITH ATTRIBUTE LOCK" />
                <asp:Button ID="btnPostInventory_NoLock" runat="server" Text="Serialize W/O Restriction" ToolTip="Post Inventory Change from Serialized IMEI data, WITHOUT ATTRIBUTE LOCK" />
                <asp:Button ID="btnPostBulkInventory" runat="server" Text="Bulk" ToolTip="Post Inventory Change from Bulk IMEI data" />
                <asp:Button ID="btnNewCancel" runat="server" Text="Cancel" />
            </asp:Panel>

            <asp:Panel ID="pnlNewDetailLine" runat="server">
                    <div class="row">
                        <div class="col-md">
                            <div>
                                <h1>
                                    <asp:Label ID="lblNewOrderDetailLine" runat="server" Text="New Order Detail Line"></asp:Label>
                                </h1>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md">
                            <div>
                                <label>
                                     QTY:</label>
                                <asp:TextBox ID="NewDetailQTY" runat="server"></asp:TextBox><br />
                            </div>
                        </div>
                        <div class="col-md">
                            <div>
                                <label>
                                    Unit Selling Price:</label>
                                <asp:TextBox ID="NewPriceUnit" runat="server"></asp:TextBox><br />
                            </div>
                        </div>
                    </div>

 <%--                   <div class="row">
                        <div class="col-md">
                            <div>
                                <label>
                                    </label>
                                <asp:TextBox ID="NewDetailSKU" runat="server" MaxLength="10"
                                    ToolTip="" Visible="false"></asp:TextBox><br />
                            </div>
                        </div>
                    </div>--%><%--&nbsp;--%>
                    <div class="row" visible="false">
                        <asp:CheckBox ID="chkRestricted" runat="server" Text="Master Restricted " ToolTip="Restrict Drop down to Master Table"
                            Checked="True" TextAlign="Left" Visible="false"/>
                        <br />
                    </div>
                    <div class="row">
                        <div class="col-md">
                            <div>
                                <label>
                                    Part Number Scan:</label>
                            </div>
                        </div>
                        <div class="col-md">
                            <div>
                                <asp:TextBox ID="txtPartNumberScan" runat="server" Width="100%"></asp:TextBox>
                                <asp:CheckBox ID="chkDropManufacturer" runat="server" Text="Manufacturer" ToolTip="Include Manufacturer when drop to detail line."
                                    Checked="True" Visible="true" />
                                <asp:CheckBox ID="chkDropModel" runat="server" Text="Model" ToolTip="Include Model when drop to detail line."
                                    Checked="True" Visible="true" />
                                <asp:CheckBox ID="chkDropColour" runat="server" Text="Colour" ToolTip="Include Colour when drop to detail line."
                                    Checked="True" Visible="true" /><br />
                                <br />
                            </div>
                        </div>
                        <div class="col-md">
                            <asp:ImageButton ID="imgPartNumberAdd" runat="server" ImageUrl="~/Images/arrow_down.png"
                            Width="15px" ToolTip="Add via partnumber" CommandArgument="PARTNUMBERSCAN"/>

                            <asp:ImageButton ID="imgPartNumberRemove" runat="server" ImageUrl="~/Images/Delete.png"
                            Width="15px" ToolTip="Remove via partnumber" CommandArgument="PARTNUMERSCAN" Visible="false"/>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md">
                            <div>
                                <label>
                                    Carrier:</label>
                            </div>
                        </div>
                        <div class="col-md">
                            <div>
                                <asp:DropDownList ID="drpCarrier" runat="server" ToolTip="Carrier" AutoPostBack="true">
                                    <asp:ListItem Text="A" Value="A">
                                    </asp:ListItem>
                                    <asp:ListItem Text="A" Value="A">
                                    </asp:ListItem>
                                    <asp:ListItem Text="A" Value="A">
                                    </asp:ListItem>
                                    <asp:ListItem Text="A" Value="A">
                                    </asp:ListItem>
                                    <asp:ListItem Text="A" Value="A">
                                    </asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md">
                            <asp:ImageButton ID="ImgDownCarrier" runat="server" ImageUrl="~/Images/arrow_down.png"
                            Width="15px" ToolTip="Add Carrier to Attribute List" CommandArgument="DRPCARRIER"/>

                            <asp:ImageButton ID="imgRemoveCarrier" runat="server" ImageUrl="~/Images/Delete.png"
                            Width="15px" ToolTip="Remove Carrier from Attribute List" CommandArgument="DRPCARRIER"/>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md">
                            <div>
                                <label>
                                    Manufacturer:</label>
                            </div>
                        </div>
                        <div class="col-md">
                            <div>
                                <asp:DropDownList ID="drpManufacturer" runat="server" ToolTip="Manufacturer" AutoPostBack="true">
                                    <asp:ListItem Text="A" Value="A">
                                    </asp:ListItem>
                                    <asp:ListItem Text="A" Value="A">
                                    </asp:ListItem>
                                    <asp:ListItem Text="A" Value="A">
                                    </asp:ListItem>
                                    <asp:ListItem Text="A" Value="A">
                                    </asp:ListItem>
                                    <asp:ListItem Text="A" Value="A">
                                    </asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md">
                            <asp:ImageButton ID="ImgDownManufacturer" runat="server" ImageUrl="~/Images/arrow_down.png"
                            Width="15px" ToolTip="Add Manufacturer to Attribute List" CommandArgument="DRPMANUFACTURER"/>

                            <asp:ImageButton ID="imgRemoveManufacturer" runat="server" ImageUrl="~/Images/Delete.png"
                            Width="15px" ToolTip="Remove Manufacturer from Attribute List" CommandArgument="DRPMANUFACTURER"/>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md">
                            <div>
                                <label>
                                    Model:</label>
                            </div>
                        </div>
                        <div class="col-md">
                            <div>
                                <asp:DropDownList ID="drpModel" runat="server" ToolTip="Model" AutoPostBack="true">
                                    <asp:ListItem Text="A" Value="A">
                                    </asp:ListItem>
                                    <asp:ListItem Text="A" Value="A">
                                    </asp:ListItem>
                                    <asp:ListItem Text="A" Value="A">
                                    </asp:ListItem>
                                    <asp:ListItem Text="A" Value="A">
                                    </asp:ListItem>
                                    <asp:ListItem Text="A" Value="A">
                                    </asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md">
                            <asp:ImageButton ID="ImgDownModel" runat="server" ImageUrl="~/Images/arrow_down.png"
                            Width="15px" ToolTip="Add Model to Attribute List" CommandArgument="DRPMODEL"/>

                            <asp:ImageButton ID="imgRemoveModel" runat="server" ImageUrl="~/Images/Delete.png"
                            Width="15px" ToolTip="Remove Model from Attribute List" CommandArgument="DRPMODEL"/>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md">
                            <div>
                                <label>
                                    Colour:</label>
                            </div>
                        </div>
                        <div class="col-md">
                            <div>
                                <asp:DropDownList ID="drpColour" runat="server" ToolTip="Colour">
                                    <asp:ListItem Text="A" Value="A">
                                    </asp:ListItem>
                                    <asp:ListItem Text="A" Value="A">
                                    </asp:ListItem>
                                    <asp:ListItem Text="A" Value="A">
                                    </asp:ListItem>
                                    <asp:ListItem Text="A" Value="A">
                                    </asp:ListItem>
                                    <asp:ListItem Text="A" Value="A">
                                    </asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md">
                            <asp:ImageButton ID="ImgDownColour" runat="server" ImageUrl="~/Images/arrow_down.png"
                            Width="15px" ToolTip="Add Colour to Attribute List" CommandArgument="DRPCOLOUR"/>

                            <asp:ImageButton ID="imgRemoveColour" runat="server" ImageUrl="~/Images/Delete.png"
                            Width="15px" ToolTip="Remove Colour from Attribute List" CommandArgument="DRPCOLOUR"/>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-md">
                            <div>
                                <label>
                                    Question:</label>
                            </div>
                        </div>
                        <div class="col-md">
                            <div>
                                <asp:DropDownList ID="drpQuestion" runat="server" ToolTip="Question" AutoPostBack="True">
                                    <asp:ListItem Text="A" Value="A">
                                    </asp:ListItem>
                                    <asp:ListItem Text="A" Value="A">
                                    </asp:ListItem>
                                    <asp:ListItem Text="A" Value="A">
                                    </asp:ListItem>
                                    <asp:ListItem Text="A" Value="A">
                                    </asp:ListItem>
                                    <asp:ListItem Text="A" Value="A">
                                    </asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div>
                                <asp:DropDownList ID="drpOption" runat="server" ToolTip="Option">
                                    <asp:ListItem Text="A" Value="A">
                                    </asp:ListItem>
                                    <asp:ListItem Text="A" Value="A">
                                    </asp:ListItem>
                                    <asp:ListItem Text="A" Value="A">
                                    </asp:ListItem>
                                    <asp:ListItem Text="A" Value="A">
                                    </asp:ListItem>
                                    <asp:ListItem Text="A" Value="A">
                                    </asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div>
                                <td>Value:
                                    <asp:TextBox ID="txtResponceText" runat="server"></asp:TextBox>
                            </div>

                        </div>
                        <div class="col-md">
                            <br />
                            <br />
                            <br />
                            <asp:ImageButton ID="ImgDownQuestion" runat="server" ImageUrl="~/Images/arrow_down.png"
                                Width="15px" ToolTip="Add question to Attribute List" CommandArgument="DRPQUESTION" />
                            <asp:ImageButton ID="imgRemoveQuestion" runat="server" ImageUrl="~/Images/Delete.png"
                                Width="15px" ToolTip="Remove question from Attribute List" CommandArgument="DRPQUESTION" />
                        </div>
                    </div>





<%--                    <div class="row">
                        <div class="col-md">
                            <div>
                                <label>
                                    Grade:</label>
                            </div>
                        </div>
                        <div class="col-md">
                            <div>
                                <asp:DropDownList ID="drpGrade" runat="server" ToolTip="Grade">
                                    <asp:ListItem Text="A" Value="A">
                                    </asp:ListItem>
                                    <asp:ListItem Text="B" Value="B">
                                    </asp:ListItem>
                                    <asp:ListItem Text="C" Value="C">
                                    </asp:ListItem>
                                    <asp:ListItem Text="D" Value="D">
                                    </asp:ListItem>
                                    <asp:ListItem Text="E" Value="E">
                                    </asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md">
                            <asp:ImageButton ID="ImgDownGrade" runat="server" ImageUrl="~/Images/arrow_down.png"
                            Width="15px" ToolTip="Add Grade to Attribute List" CommandArgument="DRPGRADE"/>

                            <asp:ImageButton ID="imgRemoveGrade" runat="server" ImageUrl="~/Images/Delete.png"
                            Width="15px" ToolTip="Remove Grade to Attribute List" CommandArgument="DRPGRADE"/>
                        </div>
                    </div>--%>
<%--                    <div class="row">
                        <div class="col-md">
                            <div>
                                <label>
                                    Disposition:</label>
                            </div>
                        </div>
                        <div class="col-md">
                            <div>
                                <asp:DropDownList ID="drpDisposition" runat="server" ToolTip="Disposition">
                                    <asp:ListItem Text="A" Value="A">
                                    </asp:ListItem>
                                    <asp:ListItem Text="B" Value="B">
                                    </asp:ListItem>
                                    <asp:ListItem Text="C" Value="C">
                                    </asp:ListItem>
                                    <asp:ListItem Text="D" Value="D">
                                    </asp:ListItem>
                                    <asp:ListItem Text="E" Value="E">
                                    </asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md">
                            <asp:ImageButton ID="ImgDownDisposition" runat="server" ImageUrl="~/Images/arrow_down.png"
                            Width="15px" ToolTip="Add Disposition to Attribute List" CommandArgument="DRPDISPOSITION"/>

                            <asp:ImageButton ID="imgRemoveDisposition" runat="server" ImageUrl="~/Images/Delete.png"
                            Width="15px" ToolTip="Remove Disposition to Attribute List" CommandArgument="DRPDISPOSITION"/>
                        </div>
                    </div>--%>
                    <div class="row">
                        <div class="col-md">
                            <div>
                                <label>
                                    Attribute Codes:</label>
                                <asp:TextBox ID="NewDetailAttributeCode" runat="server" Rows="2"
                                    TextMode="MultiLine" Width="100%" MaxLength="500" ReadOnly="True"></asp:TextBox><br />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md">
                            <div>
                                <label>
                                    Attribute Text:</label>
                                <asp:TextBox ID="NewDetailAttributeText" runat="server" Rows="2"
                                    TextMode="MultiLine" Width="100%" ReadOnly="True"></asp:TextBox><br />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md">
                            <div>
                                <asp:Button ID="AddDetailOK" runat="server" Text="Save and Close" />
                                <asp:Button ID="AddDetailCancel" runat="server" Text="Cancel" />
                                <asp:Button ID="AddNext" runat="server" Text="Save" ToolTip="This will save the current line and ready for next line input." />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            <asp:Panel ID="pnlEditNameAddress" CssClass="w-md-50" runat="server">
                <div class="row">
                    <h1>
                        <asp:Label ID="lblEditAddress" runat="server" Text="Edit Address" /></h1>
                </div>
                <div class="row">
                    <div class="col-md">
                        <label>
                            Company Name:</label>
                        <asp:TextBox ID="txtCompanyName" runat="server" MaxLength="50" />
                        <label>
                            Contact Name:</label>
                        <asp:TextBox ID="txtContactName" runat="server" MaxLength="30" />
                        <label>
                            Phone:</label>
                        <asp:TextBox ID="txtPhoneNumber" runat="server" MaxLength="30" />
                        <label>
                            Fax:</label>
                        <asp:TextBox ID="txtFaxNumber" runat="server" MaxLength="30" />
                        <label>
                            Notes:</label>
                        <asp:TextBox ID="txtNotes" runat="server" Rows="6" TextMode="MultiLine" Width="100%" />
                    </div>
                    <div class="col-md">
                        <label>
                            Address Line1:</label>
                        <asp:TextBox ID="txtAddressLine1" runat="server" MaxLength="50" />
                        <label>
                            Address Line2:</label>
                        <asp:TextBox ID="txtAddressLine2" runat="server" MaxLength="50" />
                        <label>
                            City:</label>
                        <asp:TextBox ID="txtCity" runat="server" MaxLength="50" />
                        <label>
                            State/Province:</label>
                        <asp:TextBox ID="txtStateOrProvince" runat="server" MaxLength="20" />
                        <label>
                            Zip/PostalCode:</label>
                        <asp:TextBox ID="txtPostalCode" runat="server" MaxLength="20" />
                        <label>
                            Country:</label>
                        <asp:TextBox ID="txtCountry" runat="server" MaxLength="20" />
                    </div>
                </div>
                <div class="row">
                    <asp:HiddenField ID="hdnClientType" runat="server" />
                    <asp:Button ID="btnEditNameAddressOK" runat="server" Text="OK" />
                    <asp:Button ID="btnEditNameAddressCancel" runat="server" Text="Cancel" />
                </div>
            </asp:Panel>

            <asp:Panel ID="pnlPackDetail" runat="server">

                <h1><asp:Label ID="lblPackDetailOrderNumber" runat="server" Text="Order Number" /></h1>
                <%--<asp:Label ID="lblPackDetail" runat="server" Text="New Order Detail Line"></asp:Label>--%>

                <asp:GridView ID="grdOrderDetailRecieve" CssClass="table" runat="server" DataKeyField="OrderDetailReceiveDetailID"
                    AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="ESN" HeaderText="ESN" ReadOnly="True" Visible="True" />
                        <asp:BoundField DataField="SKU" HeaderText="Carton" ReadOnly="True" Visible="True" />
                        <asp:BoundField DataField="IFSSKU" HeaderText="SKU" ReadOnly="True" Visible="True" />
                        <%--<asp:BoundField DataField="IFSLocation" HeaderText="Location" ReadOnly="True" Visible="True" />
                        <asp:BoundField DataField="IFSConditionCode" HeaderText="Condition" ReadOnly="True" Visible="True" />--%>
                        <asp:BoundField DataField="OrderDetailReceiveDetailID" HeaderText="ID" ReadOnly="True" Visible="True" />
                        <asp:BoundField DataField="ReceiveDetailID" HeaderText="LinkID" ReadOnly="True" Visible="True" />
                        <asp:BoundField DataField="Message" HeaderText="Message" ReadOnly="True" Visible="True" />
                        <asp:TemplateField HeaderText="Dd">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkIsDeleted" runat="server" ToolTip="Check to Delete" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

                <asp:HiddenField ID="hdnIsDisabled" runat="server" />
                <asp:Button ID="btnDetailRecieve_OK" runat="server" Text="OK" OnClientClick="ShouldDisableButton(this);" />
                <%--<asp:Button ID="Button2" runat="server" Text="OK"  OnClientClick="DisableButton(this);"/>--%>
                <%--<asp:Button ID="btnAddPostSerializeInventory" runat="server" Text="Serialize" ToolTip="Post Inventory Change from Serialized IMEI data" />--%>
                <%--<asp:Button ID="btnAddPostBatchInventory" runat="server" Text="Bulk" ToolTip="Post Inventory Change from Bulk Inventory" />--%>
                <asp:Button ID="btnDetailRecieve_Cancel" runat="server" Text="Cancel" />

            </asp:Panel>

            <asp:Panel ID="pnlScanPack" runat="server">
            
                <h1><asp:Label ID="Label4" runat="server" Text="Pick/Pack" /></h1>

                <h1><asp:Label ID="lblOrderNumber" runat="server" Text="Order Number" /></h1>
                <%--<asp:Label ID="lblPackDetail" runat="server" Text="New Order Detail Line"></asp:Label>--%>

                <%--To IFS Location:--%>
                <asp:TextBox ID="txtIFSLocation" runat="server" MaxLength="25" Visible="false" Text="LOC-001-001-007" />

                <label>Carton Number:</label>
                <asp:TextBox ID="txtSku" runat="server" MaxLength="10" />

                <label>Total QTY:</label>
                <asp:Label ID="lblTotalQTY" runat="server" Text="0" />

                <label>Scan ESN Here:</label>
                <asp:TextBox ID="ScanKey" runat="server" MaxLength="50" />

                <label>QTY Picked:</label>
                <asp:Label ID="lblTotalPicked" runat="server" Text="0" />

                <label>Count:</label>
                <asp:TextBox ID="txtCount" runat="server" ReadOnly="True">0</asp:TextBox>

                <label>QTY Remaining:</label>
                <asp:Label ID="lblTotalRemaining" runat="server" Text="0" />
                <asp:TextBox ID="txtESNScan" runat="server" TextMode="MultiLine" Rows="6" />

                <asp:HiddenField ID="HiddenField5" runat="server" />
                <%--<asp:Button ID="Button1" runat="server" Text="OK" OnClientClick="ShouldDisableButton(this);" />--%>
                <%--<asp:Button ID="Button2" runat="server" Text="OK"  OnClientClick="DisableButton(this);"/>--%>
                <%--<asp:Button ID="btnAddPostSerializeInventory" runat="server" Text="Serialize" ToolTip="Post Inventory Change from Serialized IMEI data" />--%>
                <%--<asp:Button ID="btnAddPostBatchInventory" runat="server" Text="Bulk" ToolTip="Post Inventory Change from Bulk Inventory" />--%>
                <asp:Button ID="btnCloseScanPick" runat="server" Text="Close" />

            </asp:Panel>

            <div id="winGetPassword" class="modal" tabindex="-1" role="dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Authorization</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <label>Name:</label>
                        <asp:TextBox ID="txtAuthUserName" runat="server" />

                        <label>Password:</label>
                        <asp:TextBox ID="CurrentPassword" runat="server" CssClass="passwordEntry" TextMode="Password" />
                    </div>
                    <div class="modal-footer">
                        <%--<asp:Button ID="btnAuthorize" runat="server" Text="Authorize" OnClientClick="Authorize();" />--%>
                        <asp:Button ID="btnAuthorizeCancel" runat="server" Text="Cancel" OnClientClick="AuthorizeCancel();return false;" />
                    </div>
                </div>
            </div>
            <syncfusion:Window ID="wndSelectClientLocation" Title="Select Client Location" runat="server"
                DraggingStyle="Original" CssClass="syncpopup">
                <asp:Panel ID="Panel9" runat="server" Width="100%" Height="100%">
                    <asp:HiddenField ID="TargetAddress" runat="server" />
                    <table id="Table6" runat="server" align="center" width="100%">
                        <tr>
                            <td colspan="2" align="center">
                                <br />
                                <h1>
                                    Search</h1>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                                Client Name:
                            </td>
                            <td>
                                <asp:TextBox ID="txtsClientName" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Location Name:
                            </td>
                            <td>
                                <asp:TextBox ID="txtsLocationName" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Street:
                            </td>
                            <td>
                                <asp:TextBox ID="txtsStreet" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Postal Code:
                            </td>
                            <td>
                                <asp:TextBox ID="txtsPostalCode" runat="server"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClientClick="SearchClient();return false;" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <br />
                                <br />
                                <asp:Panel ID="pnlSearchResult" runat="server" Width="100%" Height="100%" ScrollBars="Auto">
                                    <table id="XX">
                                        <tr>
                                            <td>
                                                Select
                                            </td>
                                            <td>
                                                Client
                                            </td>
                                            <td>
                                                Location
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </syncfusion:Window>
            <syncfusion:Window ID="Window1" Title="Authorization" runat="server" DraggingStyle="Original"
                CssClass="syncpopup">
                <asp:Panel ID="Panel1" runat="server" Width="100%" Height="100%">
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                    <table id="Table1" runat="server" align="center" width="100%">
                        <tr>
                            <td colspan="2" align="center">
                                <h1>
                                    Authorization</h1>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Name:
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="CurrentPasswordLabel" runat="server" AssociatedControlID="CurrentPassword">Password:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox2" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Button ID="btnAuthorize" runat="server" Text="Authorize" OnClientClick="Authorize();" />
                                <asp:Button ID="Button2" runat="server" Text="Cancel" OnClientClick="AuthorizeCancel();return false;" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </syncfusion:Window>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="js" runat="server">
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
        function OpenAuthorization() {
            $('#winGetPassword').modal('show');
        }
        //        function Authorize() {
        //            $('#winGetPassword').modal('hide');
        //        }
        function AuthorizeCancel() {
            $('#winGetPassword').modal('hide');
        }

        //*****************************************************************************

        //        function DropToAttribute(ctrlSource) {
        //            alert("Here One");
        //            var Source = document.getElementById(ctrlSource);
        //            if ((Source != null)) {
        //                //while (Source.options.selectedIndex >= 0) {
        //                alert("Here Two");
        //                    var newOption = new Option(); // Create a new instance of ListItem
        //                    newOption.text = Source.options[Source.options.selectedIndex].text;
        //                    newOption.value = Source.options[Source.options.selectedIndex].value;
        //                    alert("Here Three " + newOption.text);
        //                    //Target.options[Target.length] = newOption; //Append the item in Target
        //                    //Source.remove(Source.options.selectedIndex);  //Remove the item from Source
        //               // }
        //            }
        ////            var DropDown = $get(Attribute);
        //            //            var CurrentValue = GetDropDownValue(Attribute);
        //            if (Attribute.toUpperCase() = 'drpCarrier') {
        //                //if (ProcessToSetUp.substr(0, 9).toUpperCase() == 'AUTHORIZE')
        //                var ctr = $get(MCL("hdnCarrierID").value);
        //                if (ctr == null) {
        //                    alert("Current Value: NOT FOUND");
        //                    return;
        //                 }
        //                 var CurrentValue = 'xxxxxxxxxxxxxxxxxx';               // GetDropDownValue(MCL("hdnCarrierID").value);
        //                 alert("Current Value:" + Attribute + ' - ' + CurrentValue);
        //                 CurrentValue = GetDropDownValue(MCL("hdnCarrierID").value);
        //                 alert("xxCurrent Value:" + Attribute + ' - ' + CurrentValue);
        //            }
        //}

        function OpenClientSearch(Address) {
            MCL("TargetAddress").value = Address;
            $find('< %=this.wndSelectClientLocation.ClientID%>').Title = "Client Search";
            $find('< %=this.wndSelectClientLocation.ClientID%>').Open(null, null);
        }



        function selx(ID) {
            $find('< %=wndSelectClientLocation.ClientID%>').Close();
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
            //            var SearchClientName = $get("< %= txtsClientName.ClientID %>").value;
            //            var SearchLocationName = $get("< %= txtsLocationName.ClientID %>").value;
            //            var SearchStreet = $get("< %= txtsStreet.ClientID %>").value;
            //            var SearchPostalCode = $get("< %= txtsPostalCode.ClientID %>").value;
            //            var service = new WebServer_01();
            //            var rValue = service.GetSearchClientLocationData(MCL("UserName").value, SearchClientName, SearchLocationName, SearchStreet, SearchPostalCode, onSearchClientSuccess, onWebServerError);


            var SearchClientName = $get("< %= txtsClientName.ClientID %>").value;
            var SearchLocationName = $get("< %= txtsLocationName.ClientID %>").value;

            var SearchStreet = $get("< %= txtsStreet.ClientID %>").value;
            var SearchPostalCode = $get("< %= txtsPostalCode.ClientID %>").value;
            var service = new WebServer_01();
            var rValue = service.GetSearchClientLocationData(MCL("UserName").value, SearchClientName, SearchLocationName, SearchStreet, SearchPostalCode, onSearchClientSuccess, onWebServerError);

        }

        function onWebServerError(Result) {
            alert("Error:" + Result.get_message());
        }


        function onSearchClientSuccess(Result) {
            var OutputHTML = "";
            var HeaderText = "<tr><td>Select</td> <td>ID</td>   <td>Client</td>  <td>Location Name</td>    <td>Location</td></tr>";
            var BodyText = "";

            //           ClientData = eval('({' + Result + '})');
            ClientData = eval('[' + Result + ']');               // Square brackets to denote an array of elements.
            var Quote = "'";
            for (var i = 0; i < ClientData.length; i++) {
                BodyText = BodyText + "<tr><td>"
                                       + '<button id="btn" name="btn" onClick="selx(' + Quote
                                       + ClientData[i].ScanKey + Quote
                                       + '); return false;">Select</button>'
                                       + "</td> <td>"
                                       + ClientData[i].ClientLocationID
                                       + "</td> <td>"
                                       + ClientData[i].txtClientName
                                       + "</td>   <td>"

                                       + ClientData[i].txtLocationName
                                       + "</td>   <td>"

                                       + ClientData[i].txtStoreNumber + " " + ClientData[i].txtStoreSuffix + " " + ClientData[i].txtClientAddress
                                       + "</td></tr>";
            }
            OutputHTML = "<table id='XX'>" + HeaderText + BodyText + "</table>"
            var SearchResults = $get("< %= pnlSearchResult.ClientID %>");
            SearchResults.innerHTML = OutputHTML;
        }
        // *****************************************************************************



        function ShouldDisableButton(btn) {
            // var btn = $get(btnName);
            var isDisabled = $get("<%= hdnIsDisabled.ClientID %>");
            if (isDisabled.value != 'No') {
                return false;
            }
            isDisabled.value = 'Yes';
            return true;
        }


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


        function RecordScanKey() {
            //alert('Here');
            var newESN = MCL('ESN').value;
            if (newESN.length == 0) { return; }
            //alert('inside recordScaney');
            var Carton = MCL('Carton').value;
            var IFSLocation = '';              // MCL('IFSLocation').value;
            var UserName = MCL('UserName').value;
            var OrderHeaderID = MCL('ORDERHEADERID').value;
            MCL('ESN').value = '';

            Carton = trim(Carton);
            //            if (Carton.length == 0) {
            //                alert("Carton required!");
            //                return;
            //            }
            //            IFSLocation = trim(IFSLocation);
            //            if (IFSLocation.length == 0) {
            //                alert("IFS Location required!");
            //                return;
            //            }
            newESN = trim(newESN);
            if (newESN.length == 0) {
                alert("ESN required!");
                return;
            }

            var count = $get("<%= txtCount.ClientID %>").value;
            var iCount = parseInt(count);
            iCount++;
            $get("<%= txtCount.ClientID %>").value = iCount.toString();

            var service = new WebServer_01();
            service.SalesOrderESNPicked(OrderHeaderID, IFSLocation, Carton, newESN, UserName, onPickIMEISuccess, onPickIMEIError, null);

            return;

        }

        function onPickIMEIError(Result) {
            alert("Error:" + Result.get_message());
        }

        function onPickIMEISuccess(result) {
            var B = result.split(":")
            //alert('results');
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

        //**************************************************************************************

        function FillDetailAttributeCode() {
        }

        function FillDropDown(DropDownName) {
            if (MCL("chkRestricted").checked != true) { return; }
            var service = new WebServer_01();
            if (DropDownName == "Carrier") {
                //alert("Inside Carrier");
                var ctr = $get(MCL("hdnCarrierID").value);
                if (ctr == null) { return; }
                //alert("Inside Carrier WS");
                var rValue = service.GetManufacturerDropDownData_WithScanCode(GetDropDownValue(MCL("hdnCarrierID").value), MCL("UserName").value, onFillManufacturerList, null, null);
                return;
            }
            if (DropDownName == "Manufacturer") {
                //alert("Inside Manufacturer");
                //                        var ctr = $get(MCL("hdnCarrierID").value);
                //                        if (ctr == null) { return; }
                ctr = $get(MCL("hdnManufacturerID").value);
                if (ctr == null) { return; }
                //alert("Inside Manufacturer WS");
                //alert("Calling GetModelDropDownData_WithScanCode");
                var rValue = service.GetModelDropDownData_WithScanCode("-1", GetDropDownValue(MCL("hdnManufacturerID").value), MCL("UserName").value, onFillModelList, null, null);
                return;
            }
            if (DropDownName == "Model") {
                //alert("Inside Model");
                var ctr = $get(MCL("hdnCarrierID").value);
                if (ctr == null) { return; }
                ctr = $get(MCL("hdnManufacturerID").value);
                if (ctr == null) { return; }
                ctr = $get(MCL("hdnModelID").value);
                if (ctr == null) { return; }
                //alert("Inside Model WS");
                var rValue = service.GetColourDropDownData_WithScanCode(-1, GetDropDownValue(MCL("hdnManufacturerID").value), GetDropDownValue(MCL("hdnModelID").value), MCL("UserName").value, onFillColourList, null, null);
                //var rValue = service.GetColourDropDownData_WithScanCode(GetDropDownValue(MCL("hdnCarrierID").value), GetDropDownValue(MCL("hdnManufacturerID").value), GetDropDownValue(MCL("hdnModelID").value), MCL("UserName").value, onFillColourList, null, null);
                return;
            }
        }

        function onFillManufacturerList(Result) {
            if (Result.length = 0 || MCL("chkRestricted").checked != true) { return; }
            //alert("onFillManufacturerList");
            var DropDown = $get(MCL("hdnManufacturerID").value);
            if (DropDown != null) {
                var CurrentValue = GetDropDownValue(MCL("hdnManufacturerID").value);
                while (DropDown.options.length > 0) DropDown.remove(0);
                // fill the Dropdown
                if (Result.length > 0) {
                    ClientData = eval('({' + Result + '})');
                    for (var key in ClientData) {
                        var attrName = key;
                        var attrValue = ClientData[key];
                        addOption(DropDown, key, ClientData[key], CurrentValue)
                    }
                }
            }
            //            var DropDown = $get(MCL("hdnManufacturerID").value)
            //            var CurrentValue = GetDropDownValue(MCL("hdnManufacturerID").value);
            //            // if (Result.length > 0 || MCL("hdnisMasterLinked").value == "True") { DropDown = ClearOptionsFast(DropDown); }
            //            if (Result.length > 0 || MCL("chkRestricted").checked != true) { while (DropDown.options.length > 0) DropDown.remove(0); }
            //            // fill the Dropdown
            //            if (Result.length > 0) {
            //                addOption(DropDown, key, "All", "-1")
            //                ClientData = eval('({' + Result + '})');
            //                for (var key in ClientData) {
            //                    var attrName = key;
            //                    var attrValue = ClientData[key];
            //                    addOption(DropDown, key, ClientData[key], CurrentValue)
            //                }
            //            }
            FillDropDown("Manufacturer");
            return;
        }

        function onFillModelList(Result) {

            //alert("Inside lModel");
            if (Result.length = 0 || MCL("chkRestricted").checked != true) { return; }

            //alert("Inside lModel");
            var DropDown = $get(MCL("hdnModelID").value)
            if (DropDown != null) {
                var CurrentValue = GetDropDownValue(MCL("hdnModelID").value);
                while (DropDown.options.length > 0) DropDown.remove(0);
                // fill the Dropdown
                if (Result.length > 0) {
                    ClientData = eval('({' + Result + '})');
                    for (var key in ClientData) {
                        var attrName = key;
                        var attrValue = ClientData[key];
                        addOption(DropDown, key, ClientData[key], CurrentValue)
                    }
                }
            }
            FillDropDown("Model");
            return;
        }
        function onFillColourList(Result) {
            return;
            if (Result.length = 0 || MCL("chkRestricted").checked != true) { return; }
            var DropDown = $get(MCL("hdnColourID").value)
            if (DropDown != null) {
                var CurrentValue = GetDropDownValue(MCL("hdnColourID").value);
                while (DropDown.options.length > 0) DropDown.remove(0);
                // fill the Dropdown
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
                case "USERNAME": return $get("<%= hdnUserName.ClientID %>"); break;
                case "ESN": return $get("<%= ScanKey.ClientID %>"); break;
                case "CARTON": return $get("<%= txtSku.ClientID %>"); break;
                case "IFSLOCATION": return $get("<%= txtIFSLocation.ClientID %>"); break;
                case "ORDERHEADERID": return $get("<%= hdnOrderDetailID.ClientID %>"); break;
                case "OUTPUTAREA": return $get("<%= txtESNScan.ClientID %>"); break;
                case "LBLTOTALQTY": return $get("<%= lblTotalQTY.ClientID %>"); break;
                case "LBLTOTALPICKED": return $get("<%= lblTotalPicked.ClientID %>"); break;
                case "LBLTOTALREMAINING": return $get("<%= lblTotalRemaining.ClientID %>"); break;
                case "CHKRESTRICTED": return $get("<%= chkRestricted.ClientID %>"); break;
                case "HDNCARRIERID": return $get("<%= hdnCarrierID.ClientID %>"); break;
                case "HDNMANUFACTURERID": return $get("<%= hdnManufacturerID.ClientID %>"); break;
                case "HDNMODELID": return $get("<%= hdnModelID.ClientID %>"); break;
                case "HDNCOLOURID": return $get("<%= hdnColourID.ClientID %>"); break;
                case "HDNGRADEID": return $get("<%= hdnGradeID.ClientID %>"); break;
                case "TARGETADDRESS": return $get("<%= TargetAddress.ClientID %>"); break;
                case "TXTBILLCLIENT": return $get("<%= txtBillClient.ClientID %>"); break;
                case "BTNBILLCLIENTSEARCH": return $get("<%= btnBillClientSearch.ClientID %>"); break;
                case "TXTSHIPCLIENT": return $get("<%= txtShipClient.ClientID %>"); break;
                case "BTNSHIPCLIENTSEARCH": return $get("<%= btnShipClientSearch.ClientID %>"); break;
                default: return null;
            }
        }


    </script>
</asp:Content>

