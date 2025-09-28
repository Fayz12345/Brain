<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrderEntry02.aspx.cs" Inherits="BW_WebApp.OrderEntry02" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"> 

	<link href="../Javascripts/jquery-ui-1.8.1.custom.css" rel="stylesheet" type="text/css" /> 
	<script src="../Javascripts/jquery-1.4.1-vsdoc.js" type="text/javascript"></script>
	<script src="../Javascripts/jquery-ui-1.8.1.custom.min.js" type="text/javascript"></script>

    <%--<script src="../Javascripts/jquery.timeentry.js" type="text/javascript"></script>--%>
	<%--<script src="../Javascripts/jquery.dataTables1_7.min.js" type="text/javascript"></script>--%>

    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cMC" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

                
            <asp:Panel ID="pnlxxxx" runat="server" Width="100%" Height="100%" HorizontalAlign="Left">
                <asp:HiddenField ID="hdnUserName" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnCarrierID" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnManufacturerID" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnModelID" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnColourID" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnGradeID" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnProjtagESNlist" runat="server" ClientIDMode="Static" />
                <asp:Panel ID="pnlmainentry" runat="server" Width="100%" Height="100%" HorizontalAlign="Left">
                    <table style="width: 100%;">
                        <tr>
                            <td align="left" valign="top">
                                <asp:TabContainer runat="server" ID="tabMain" Width="100%" ActiveTabIndex="0" BorderStyle="None"
                                    AutoPostBack="True" Style="overflow: auto; height: auto; max-width: 1000px">
                                    <asp:TabPanel runat="server" ID="TabPanelNew" Enabled="true" HeaderText="New" Width="100%"
                                        Height="100%" Visible="False">
                                        <ContentTemplate>
                                            <asp:Button ID="btnNew" runat="server" Text="New" />
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel runat="server" ID="TabPanelPick" Enabled="true" HeaderText="Pick/Pack"
                                        Width="100%" Visible="False">
                                        <ContentTemplate>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel runat="server" ID="TabPanelShip" Enabled="true" HeaderText="Ship" Width="100%"
                                        Height="100%" Visible="False">
                                        <ContentTemplate>
                                            Courier:
                                            <asp:DropDownList ID="drpCourier" runat="server">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel runat="server" ID="TabPanelSearch" Enabled="true" HeaderText="Search"
                                        Width="100%" Visible="True">
                                        <ContentTemplate>
                                            <asp:Label ID="Label4" runat="server" Text="Search Orders" Font-Bold="True" Font-Size="Large"></asp:Label>
                                            <asp:Panel runat="server" ID="pnlParameters">
                                                <asp:Table ID="Table4" runat="server" Width="100%">
                                                    <asp:TableRow>
                                                        <asp:TableCell Width="12%">
                                Status:
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="20%">
                                                            <asp:DropDownList ID="drpStatus" runat="server">
                                                            </asp:DropDownList>
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="20%">
                                                            Order Begin/End Date
                                                            <asp:CheckBox ID="chkReceived" runat="server" ToolTip="If unchecked, this will be excluded from the filter" />
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="20%">
                                                            <asp:TextBox ID="txtBeginDate_s" runat="server"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtBeginDate_s">
                                                            </asp:CalendarExtender>
                                                        </asp:TableCell>
                                                        <asp:TableCell>
                                                            <asp:TextBox ID="txtEndDate_s" runat="server"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtEndDate_s">
                                                            </asp:CalendarExtender>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow>
                                                        <asp:TableCell>
                                Order Number:
                                                        </asp:TableCell>
                                                        <asp:TableCell>
                                                            <asp:TextBox ID="txtOrderNumber_s" runat="server" ToolTip="Order Number"></asp:TextBox>
                                                        </asp:TableCell>
                                                        <asp:TableCell>
                                Client Name:
                                                        </asp:TableCell>
                                                        <asp:TableCell>
                                                            <asp:TextBox ID="txtClient_s" runat="server" ToolTip="Client Name"></asp:TextBox>
                                                        </asp:TableCell>
                                                        <asp:TableCell>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow>
                                                        <asp:TableCell>
                                Customer PO:
                                                        </asp:TableCell>
                                                        <asp:TableCell>
                                                            <asp:TextBox ID="txtCustomerPO_s" runat="server" ToolTip="Customer PO"></asp:TextBox>
                                                        </asp:TableCell>
                                                        <asp:TableCell>
                                City:
                                                        </asp:TableCell>
                                                        <asp:TableCell>
                                                            <asp:TextBox runat="server" ID="txtCity_s" ToolTip="City"></asp:TextBox>
                                                        </asp:TableCell>
                                                        <asp:TableCell>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow>
                                                        <asp:TableCell>
                                WayBill Number:
                                                        </asp:TableCell>
                                                        <asp:TableCell>
                                                            <asp:TextBox ID="txtWaybillNumber_s" runat="server" ToolTip="Waybill Number"></asp:TextBox>
                                                        </asp:TableCell>
                                                        <asp:TableCell>
                                Postal Code:
                                                        </asp:TableCell>
                                                        <asp:TableCell>
                                                            <asp:TextBox runat="server" ID="txtPostalCode_s" ToolTip="Postal Code"></asp:TextBox>
                                                        </asp:TableCell>
                                                        <asp:TableCell>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow>
                                                        <asp:TableCell>
                                Project Tag:
                                                        </asp:TableCell>
                                                        <asp:TableCell>
                                                            <asp:TextBox ID="txtProjectTag_s" runat="server" ToolTip="Project Tag"></asp:TextBox>
                                                        </asp:TableCell>
                                                        <asp:TableCell>
                                Phone Number:
                                                        </asp:TableCell>
                                                        <asp:TableCell>
                                                            <asp:TextBox runat="server" ID="txtPhoneNumber_s" ToolTip="Phone Number"></asp:TextBox>
                                                        </asp:TableCell>
                                                        <asp:TableCell>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow>
                                                        <asp:TableCell>
                                                        </asp:TableCell>
                                                        <asp:TableCell>
                                                        </asp:TableCell>
                                                        <asp:TableCell>
                                Email Address:
                                                        </asp:TableCell>
                                                        <asp:TableCell>
                                                            <asp:TextBox runat="server" ID="txtEmailAddress_s" ToolTip="Email Address"></asp:TextBox>
                                                        </asp:TableCell>
                                                        <asp:TableCell>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow ID="rowMessage">
                                                        <asp:TableCell ColumnSpan="2" Width="100%">
                                                            <asp:Label ID="lblMessage" runat="server" Text="" Width="100%" Height="100%"></asp:Label>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                </asp:Table>
                                            </asp:Panel>
                                            <asp:Button ID="btnSearch_Order" runat="server" Text="Search" />
                                            <asp:Button ID="btnEmail_Details" runat="server" Text="Email Results" />
                                            <asp:TextBox runat="server" ID="txtEmailResultsAddress" ToolTip="Email Address"></asp:TextBox>
                                            <asp:Panel ID="Panel3" runat="server" Style="overflow: auto; max-height: 400px; width: auto;"
                                                HorizontalAlign="Left">
                                                <asp:GridView ID="grdTempDetail" runat="server" DataKeyField="OrderHeaderID" AutoGenerateColumns="False"
                                                    AlternatingRowStyle-BackColor="#CCFFCC" BackColor="#FFFFCC">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="P">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgOpen" runat="server" HeaderText="" ImageUrl="~/Images/details_open.png"
                                                                    ToolTip="Open" CommandArgument="Open"></asp:ImageButton>
                                                                <asp:ImageButton ID="imgPrint" runat="server" HeaderText="" ImageUrl="~/Images/print_icon.gif"
                                                                    ToolTip="Print" CommandArgument="Print"></asp:ImageButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Status" HeaderText="Status" ReadOnly="True"></asp:BoundField>
                                                        <asp:BoundField DataField="QTY" HeaderText="QTY" ReadOnly="True"></asp:BoundField>
                                                        <asp:BoundField DataField="QTYPacked" HeaderText="Packed" ReadOnly="True"></asp:BoundField>
                                                        <asp:BoundField DataField="OrderNumber" HeaderText="Order Number" ReadOnly="True">
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CustomerPO" HeaderText="Customer PO" ReadOnly="True">
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="WayBillNumber" HeaderText="Waybill" ReadOnly="True"></asp:BoundField>
                                                        <asp:BoundField DataField="ProjectTag" HeaderText="Project Tag" ReadOnly="True">
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="OrderDate" HeaderText="Order Date" ReadOnly="True"></asp:BoundField>
                                                        <asp:BoundField DataField="CreateDate" HeaderText="Create Date" ReadOnly="True">
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PickPackDate" HeaderText="Pick/Pack" ReadOnly="True">
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ShippedDate" HeaderText="Ship Date" ReadOnly="True"></asp:BoundField>
                                                        <%--            <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="True"></asp:BoundField>--%>
                                                        <asp:BoundField DataField="CompanyName" HeaderText="Company" ReadOnly="True"></asp:BoundField>
                                                        <asp:BoundField DataField="ContactName" HeaderText="Contact" ReadOnly="True"></asp:BoundField>
                                                        <asp:BoundField DataField="AddressLine1" HeaderText="Address 1" ReadOnly="True">
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="AddressLine2" HeaderText="Address 2" ReadOnly="True">
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="City" HeaderText="City" ReadOnly="True"></asp:BoundField>
                                                        <asp:BoundField DataField="PostalCode" HeaderText="Postal Code" ReadOnly="True">
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PhoneNumber" HeaderText="Phone Number" ReadOnly="True">
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="FaxNumber" HeaderText="Fax Number" ReadOnly="True"></asp:BoundField>
                                                        <asp:BoundField DataField="EmailAddress" HeaderText="Email" ReadOnly="True"></asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel runat="server" ID="TabFilter" Enabled="true" HeaderText="Filter" Width="100%"
                                        Visible="True">
                                        <ContentTemplate>
                                            <asp:Label ID="Label1" runat="server" Text="Filter List by Order Date"></asp:Label>
                                            <asp:CheckBox ID="chkFilterOrderDate" runat="server" ToolTip="Filter on Order Entry Date" />
                                            <asp:TextBox ID="OrderStartDate" runat="server" ToolTip="Order Entry Filter Start Date"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="OrderStartDate"
                                                Format="MM/dd/yyyy">
                                            </asp:CalendarExtender>
                                            <asp:TextBox ID="OrderEndDate" runat="server" ToolTip="Order Entry Filter End Date"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="OrderEndDate"
                                                Format="MM/dd/yyyy">
                                            </asp:CalendarExtender>
                                            <br />
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel runat="server" ID="TabMove" Enabled="true" HeaderText="Move" Width="100%"
                                        Visible="True">
                                        <ContentTemplate>
                                            <asp:Label ID="Label2" runat="server" Text="Move Order Number:"></asp:Label>
                                            <asp:TextBox ID="txtOrderNumber" runat="server" ToolTip="Order Number"></asp:TextBox>
                                            <asp:Label ID="Label3" runat="server" Text=" To "></asp:Label>
                                            <asp:DropDownList ID="drpMoveList" runat="server">
                                                <asp:ListItem Text="New"></asp:ListItem>
                                                <asp:ListItem Text="Pick/Pack"></asp:ListItem>
                                                <asp:ListItem Text="Ship"></asp:ListItem>
                                                <%--<asp:ListItem Text="Bill"></asp:ListItem>--%>
                                                <asp:ListItem Text="Done"></asp:ListItem>
                                                <%--<asp:ListItem Text="Archive"></asp:ListItem>--%>
                                            </asp:DropDownList>
                                            <asp:Button ID="btnPanelMove" runat="server" Text="Go" ToolTip="Move Order number" />
                                            <br />
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                </asp:TabContainer>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top">
                                <asp:Panel ID="pnlHeaderArea" runat="server" Width="100%" ScrollBars="Auto">
                                    <asp:Label runat="server" ID="Location" Text="." CssClass="rightColumn"></asp:Label>
                                    <asp:GridView ID="GridView2" runat="server" DataKeyField="OrderHeaderID" AutoGenerateColumns="False"
                                        AlternatingRowStyle-BackColor="#E2E2E2" Width="100%" BackColor="#F5F5F5">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEdit" runat="server">Edit</asp:LinkButton>
                                                    <asp:LinkButton ID="btnDelete" runat="server">Delete</asp:LinkButton>
                                                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" TargetControlID="btnDelete"
                                                        ConfirmText="Are you sure you want to Delete this file?">
                                                    </asp:ConfirmButtonExtender>
                                                    <asp:LinkButton ID="imgPrint" runat="server">Print</asp:LinkButton>
                                                    <asp:LinkButton ID="btnInvoice" runat="server">Invoice</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="OrderNumber" HeaderText="Order" ReadOnly="True" Visible="True">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="IFSOrderNo" HeaderText="IFSOrder" ReadOnly="True" Visible="True">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CustomerPO" HeaderText="PO" ReadOnly="True" Visible="True">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="OrderDate" HeaderText="Order Date" ReadOnly="True" Visible="True">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PickPackDate" HeaderText="Pick/Pack Date" ReadOnly="True"
                                                Visible="True">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ShippedDate" HeaderText="Shipped Date" ReadOnly="True"
                                                Visible="True">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" ReadOnly="True" Visible="True">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="MiscDesc" HeaderText="Desc" ReadOnly="True" Visible="True">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="WaybillNumber" HeaderText="Waybill" ReadOnly="True" Visible="True">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Site" HeaderText="Site" ReadOnly="True" Visible="True">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Move">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="drpMoveTo" runat="server">
                                                        <asp:ListItem Text="*  Move  *"></asp:ListItem>
                                                        <asp:ListItem Text="New"></asp:ListItem>
                                                        <asp:ListItem Text="Pick/Pack"></asp:ListItem>
                                                        <asp:ListItem Text="Ship"></asp:ListItem>
                                                        <%--<asp:ListItem Text="Bill"></asp:ListItem>--%>
                                                        <asp:ListItem Text="Done"></asp:ListItem>
                                                        <%--<asp:ListItem Text="Archive"></asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                    <asp:LinkButton ID="btnMove" runat="server" CommandArgument="<%# Container.DataItemIndex %>">Move</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlNew" runat="server" Width="100%">
                    <table width="100%">
                        <tr>
                            <td id="AddTemplateHeader" colspan="4" style="border-style: none none solid none;
                                border-width: thick; border-color: #E2E2E2; text-align: left; vertical-align: top;">
                                <h1>
                                    <asp:Label ID="lblAddOrder" runat="server" Text="Add Order"></asp:Label></h1>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <asp:HiddenField ID="hdnOrderHeaderID" runat="server" ClientIDMode="Static" />
                                <table>
                                    <tr>
                                        <td style="text-align: right; vertical-align: top;">
                                            Customer PO Number:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCustomerPONumber" runat="server" Width="100%" MaxLength="50"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; vertical-align: top;">
                                            Restrict to Project Tag:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtProjectTag" runat="server" Width="100%"></asp:TextBox><br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; vertical-align: text-top;">
                                            Client:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtBillClient" runat="server" ToolTip="Enter client location Key"></asp:TextBox>
                                            <asp:Button ID="btnBillClientSearch" runat="server" Text="Search" />
                                            &nbsp;
                                            <asp:ImageButton ID="btnSearchClient" runat="server" ImageUrl="~/Images/Find_Search_64.png"
                                                OnClientClick="OpenClientSearch('Bill');return false;" Width="15px" ToolTip="Search Clients" />
                                            <br />
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
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; vertical-align: top;">
                                            Name/Address:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtBillNameAddresstext" runat="server" Rows="6" ReadOnly="True"
                                                TextMode="MultiLine" Width="100%"></asp:TextBox><br />
                                            <asp:Button ID="btnBillClientEdit" runat="server" Text="Edit" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; vertical-align: top;">
                                            Internal Note:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtInternalNote" runat="server" Rows="6" TextMode="MultiLine" Width="100%"
                                                MaxLength="500"></asp:TextBox><br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; vertical-align: top;">
                                            Project to pull Inventory:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="drpProjectList_New" runat="server" ToolTip="Project" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="top">
                                <table>
                                    <tr>
                                        <td style="text-align: right; vertical-align: top;">
                                            Purchase Order Number (PL#):
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPurchaseOrderNumber" runat="server" Text="" Width="100%"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; vertical-align: top;">
                                            IFS Order Number:
                                        </td>
                                        <td>
                                            <asp:Label ID="lblIFSOrderNumber" runat="server" Text="" Width="100%"></asp:Label>
                                        </td>
                                    </tr>
                                    <%--                                    <tr id="WayBillRow1b">
                                        <td style="text-align: right; vertical-align: top;" colspan="2">
                                            <br />
                                        </td>
                                    </tr>--%>
                                    <tr id="WayBillRow1a">
                                        <td style="text-align: right; vertical-align: top;">
                                            Waybill Number:
                                        </td>
                                        <td valign="top">
                                            <asp:TextBox ID="txtWaybillNumber" runat="server" Width="100%" MaxLength="500"></asp:TextBox>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; vertical-align: text-top;">
                                            Ship To:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtShipClient" runat="server" ToolTip="Enter client location Key"></asp:TextBox>
                                            <asp:Button ID="btnShipClientSearch" runat="server" Text="Search" />
                                            &nbsp;
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Find_Search_64.png"
                                                OnClientClick="OpenClientSearch('Ship');return false;" Width="15px" ToolTip="Search Clients" />
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
                                            <%--                                    <asp:HiddenField ID="hdnPaid" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hdnPostPaid" runat="server" ClientIDMode="Static" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; vertical-align: top;">
                                            Name/Address:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtShipNameAddresstext" runat="server" Rows="6" ReadOnly="True"
                                                TextMode="MultiLine" Width="100%"></asp:TextBox><br />
                                            <asp:Button ID="btnShipClientEdit" runat="server" Text="Edit" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; vertical-align: top;">
                                            Delivery Note:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDeliveryNote" runat="server" Rows="6" TextMode="MultiLine" Width="100%"
                                                MaxLength="500"></asp:TextBox><br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; vertical-align: top;">
                                        </td>
                                        <td>
                                            Paid:
                                            <asp:CheckBox ID="chkPaid" runat="server" />
                                            <%--                                            Post Paid:--%>
                                            <asp:CheckBox ID="chkPostPaid" runat="server" Visible="False" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="1" style="border-style: solid none none none; border-width: thick; border-color: #E2E2E2;
                                text-align: left; vertical-align: top;">
                                <asp:TextBox ID="txtFromBinSku" runat="server" ToolTip="Enter Sku for added Detail Lines."></asp:TextBox>
                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtFromBinSku"
                                    WatermarkText="Sku">
                                </asp:TextBoxWatermarkExtender>
                                <br />
                                <asp:Button ID="btnAddDetailLine" runat="server" Text="Add New Line" UseSubmitBehavior="False"
                                    class="button" />
                            </td>
                            <td colspan="3" style="border-style: solid none none none; border-width: thick; border-color: #E2E2E2;
                                text-align: right; vertical-align: top;">
                                <asp:TextBox ID="txtFromBin" runat="server" ToolTip="Enter bin number to load line detail from."></asp:TextBox>
                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtFromBin"
                                    WatermarkText="Bin">
                                </asp:TextBoxWatermarkExtender>
                                <br />
                                <asp:Button ID="btnFromBin" runat="server" Text="Add from Bin" UseSubmitBehavior="False"
                                    class="button" />
                                <asp:Button ID="btnFromBin_B" runat="server" Text="Add from Bin (b)" UseSubmitBehavior="False"
                                    class="button" OnClientClick="OpenAuthorization(); return false;" />
                                <%--                                <asp:Button ID="btnFromAvailableStock" runat="server" Text="Available stock"
                                    UseSubmitBehavior="False" class="button" Visible="False" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="border-style: solid none none none; border-width: thick; border-color: #E2E2E2;
                                text-align: left; vertical-align: middle;">
                                <asp:GridView ID="grdNewOrderDetailGrid" runat="server" DataKeyField="OrderDetailID"
                                    AutoGenerateColumns="False" AlternatingRowStyle-BackColor="#E2E2E2" Width="100%"
                                    BackColor="#F5F5F5">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" runat="server" CommandArgument="<%# Container.DataItemIndex %>">Edit</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Project_ID" HeaderText="Project_ID" ReadOnly="True" Visible="True">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="QTY" HeaderText="QTY" ReadOnly="True" Visible="True">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
<%--                                        <asp:BoundField DataField="UnitPrice" HeaderText="Unit Price" ReadOnly="True" Visible="True">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>--%>
                                        <asp:BoundField DataField="QTYPacked" HeaderText="Packed" ReadOnly="True" Visible="True">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="QTYInventoryLinked" HeaderText="Posted" ReadOnly="True"
                                            Visible="True">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IFSSKU" HeaderText="SKU" ReadOnly="True" Visible="True">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
<%--                                        <asp:BoundField DataField="Desc_Code" HeaderText="Attribute Code" ReadOnly="True"
                                            Visible="True" ItemStyle-Width="30%">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>--%>
                                        <asp:BoundField DataField="Location" HeaderText="Location" ReadOnly="True"
                                            Visible="True" ItemStyle-Width="30%">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Desc_Text" HeaderText="Attribute Desc" ReadOnly="True"
                                            Visible="True" ItemStyle-Width="30%">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="OrderDetailID" HeaderText="ID" ReadOnly="True">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Del">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkIsDeleted" runat="server" ToolTip="Check to Delete" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnPack" runat="server" CommandArgument="<%# Container.DataItemIndex %>">Pack</asp:LinkButton>
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
                                        <asp:BoundField DataField="Carrier" HeaderText="Carrier" ReadOnly="True" Visible="True"
                                            ItemStyle-Width="6%">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Manufacturer" HeaderText="Manufacturer" ReadOnly="True"
                                            Visible="True" ItemStyle-Width="6%">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Model" HeaderText="Model" ReadOnly="True" Visible="True"
                                            ItemStyle-Width="6%">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Colour" HeaderText="Colour" ReadOnly="True" Visible="True"
                                            ItemStyle-Width="6%">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Condition" HeaderText="Cond" ReadOnly="True" Visible="True"
                                            ItemStyle-Width="6%">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>

                                    </Columns>
                                </asp:GridView>
                                <asp:Button ID="btnNewOK" runat="server" Text="Save" />
                                <asp:Button ID="btnPostInventory" runat="server" Text="Serialize" ToolTip="Post Inventory Change from Serialized IMEI data WITH ATTRIBUTE LOCK" />
                                <asp:Button ID="btnPostInventory_NoLock" runat="server" Text="Serialize W/O Restriction"
                                    ToolTip="Post Inventory Change from Serialized IMEI data, WITHOUT ATTRIBUTE LOCK" />
                                <asp:Button ID="btnPostBulkInventory" runat="server" Text="Bulk" ToolTip="Post Inventory Change from Bulk IMEI data" />
                                <asp:Button ID="btnNewCancel" runat="server" Text="Cancel" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlNewDetailLine" runat="server">
                    <table width="100%">
                        <tr>
                            <td id="Td1" colspan="2" style="border-style: none none solid none; border-width: thick;
                                border-color: #FFCC66; text-align: center; vertical-align: middle;">
                                <h1>
                                    <asp:Label ID="lblNewOrderDetailLine" runat="server" Text="New Order Detail Line"></asp:Label>
                                </h1>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; vertical-align: top;">
                                QTY:
                            </td>
                            <td>
                                <asp:HiddenField ID="OrderDetailLineID" runat="server" />
                                <asp:TextBox ID="NewDetailQTY" runat="server" BackColor="#FFFF66"></asp:TextBox><br />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; vertical-align: top;">
                                Unit Selling Price:
                            </td>
                            <td>
                                <asp:TextBox ID="NewPriceUnit" runat="server" BackColor="#FFFF66"></asp:TextBox><br />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; vertical-align: top;">
                                SKU:
                            </td>
                            <td>
                                <asp:TextBox ID="NewDetailSKU" runat="server" BackColor="#FFFF66" MaxLength="10"
                                    ToolTip=""></asp:TextBox><br />
                                <%--ToolTip="The Bin where the Items are pulled from."></asp:TextBox><br />--%>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; vertical-align: top;">
                                Carrier/Make/Model:
                            </td>
                            <td>
                                <asp:CheckBox ID="chkRestricted" runat="server" Text="Master Restricted" ToolTip="Restrict Drop down to Master Table"
                                    Checked="True" TextAlign="Left" />
                                <br />
                                <asp:DropDownList ID="drpCarrier" runat="server" ToolTip="Carrier">
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
                                <asp:DropDownList ID="drpManufacturer" runat="server" ToolTip="Manufacturer">
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
                                <asp:DropDownList ID="drpModel" runat="server" ToolTip="Model">
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
                                <%--                                <asp:Button ID="btnGO" runat="server" Text="GO" OnClientClick="FillDetailAttributeCode();return false;"
                                    ToolTip="Click here to move the Attribute codes down." />--%>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; vertical-align: top;">
                                Attribute Text:
                            </td>
                            <td>
                                <asp:TextBox ID="NewDetailAttributeText" runat="server" BackColor="#FFFF66" Rows="2"
                                    TextMode="MultiLine" Width="100%" ReadOnly="True"></asp:TextBox><br />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; vertical-align: top;">
                                Attribute Codes:
                            </td>
                            <td>
                                <asp:TextBox ID="NewDetailAttributeCode" runat="server" BackColor="#FFFF66" Rows="2"
                                    TextMode="MultiLine" Width="100%" MaxLength="500" ReadOnly="True"></asp:TextBox><br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="border-style: solid none none none; border-width: thick; border-color: #FFCC66;
                                text-align: left; vertical-align: middle;">
                                <asp:Button ID="AddDetailOK" runat="server" Text="Save/Add Line" />
                                <asp:Button ID="AddDetailCancel" runat="server" Text="Cancel" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlEditNameAddress" runat="server">
                    <table width="100%">
                        <tr>
                            <td id="Td2" colspan="2" style="border-style: none none solid none; border-width: thick;
                                border-color: #E2E2E2; text-align: center; vertical-align: middle;">
                                <h1>
                                    <asp:Label ID="lblEditAddress" runat="server" Text="Edit Address"></asp:Label>
                                </h1>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; vertical-align: top;">
                                Company Name:
                            </td>
                            <td>
                                <asp:TextBox ID="txtCompanyName" runat="server" MaxLength="50"></asp:TextBox><br />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; vertical-align: top;">
                                Contact Name:
                            </td>
                            <td>
                                <asp:TextBox ID="txtContactName" runat="server" MaxLength="30"></asp:TextBox><br />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; vertical-align: top;">
                                Address Line1:
                            </td>
                            <td>
                                <asp:TextBox ID="txtAddressLine1" runat="server" MaxLength="50"></asp:TextBox><br />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; vertical-align: top;">
                                Address Line2:
                            </td>
                            <td>
                                <asp:TextBox ID="txtAddressLine2" runat="server" MaxLength="50"></asp:TextBox><br />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; vertical-align: top;">
                                City:
                            </td>
                            <td>
                                <asp:TextBox ID="txtCity" runat="server" MaxLength="50"></asp:TextBox><br />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; vertical-align: top;">
                                State/Province:
                            </td>
                            <td>
                                <asp:TextBox ID="txtStateOrProvince" runat="server" MaxLength="20"></asp:TextBox><br />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; vertical-align: top;">
                                Zip/PostalCode:
                            </td>
                            <td>
                                <asp:TextBox ID="txtPostalCode" runat="server" MaxLength="20"></asp:TextBox><br />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; vertical-align: top;">
                                Phone:
                            </td>
                            <td>
                                <asp:TextBox ID="txtPhoneNumber" runat="server" MaxLength="30"></asp:TextBox><br />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; vertical-align: top;">
                                Fax:
                            </td>
                            <td>
                                <asp:TextBox ID="txtFaxNumber" runat="server" MaxLength="30"></asp:TextBox><br />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; vertical-align: top;">
                                Notes:
                            </td>
                            <td>
                                <asp:TextBox ID="txtNotes" runat="server" Rows="6" TextMode="MultiLine" Width="100%"></asp:TextBox><br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="border-style: solid none none none; border-width: thick; border-color: #E2E2E2;
                                text-align: left; vertical-align: middle;">
                                <asp:HiddenField ID="hdnClientType" runat="server" />
                                <asp:Button ID="btnEditNameAddressOK" runat="server" Text="OK" />
                                <asp:Button ID="btnEditNameAddressCancel" runat="server" Text="Cancel" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlPackDetail" runat="server">
                    <asp:HiddenField ID="hdnOrderDetailID" runat="server" Value="" />
                    <asp:HiddenField ID="hdnCountRequired" runat="server" Value="0" />
                    <table width="100%">
                        <tr>
                            <td id="Td3" colspan="2" style="border-style: none none solid none; border-width: thick;
                                border-color: #E2E2E2; text-align: center; vertical-align: middle;">
                                <h1>
                                    <asp:Label ID="lblOrderNumber" runat="server" Text="Order Number"></asp:Label><br />
                                    <asp:Label ID="lblPackDetail" runat="server" Text="New Order Detail Line"></asp:Label>
                                </h1>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; vertical-align: top;">
                                Carton Number:
                            </td>
                            <td>
                                <asp:TextBox ID="txtSku" runat="server" MaxLength="10"></asp:TextBox><br />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; vertical-align: top;">
                                Scan ESN Here:
                            </td>
                            <td>
                                <asp:TextBox ID="ScanKey" runat="server" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; vertical-align: top;">
                                Count:
                            </td>
                            <td>
                                <asp:TextBox ID="txtCount" runat="server" ReadOnly="True">0</asp:TextBox><br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="border-style: solid none none none; border-width: thick; border-color: #E2E2E2;
                                text-align: left; vertical-align: middle;">
                                <asp:TextBox ID="txtESNScan" runat="server" TextMode="MultiLine" Width="98%" Height="40%"
                                    Rows="6"></asp:TextBox><br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="border-style: solid none none none; border-width: thick; border-color: #E2E2E2;
                                text-align: left; vertical-align: middle;" height="100%">
                                <asp:GridView ID="grdOrderDetailRecieve" runat="server" DataKeyField="OrderDetailReceiveDetailID"
                                    AutoGenerateColumns="False" AlternatingRowStyle-BackColor="#E2E2E2" Width="98%"
                                    BackColor="#F5F5F5">
                                    <Columns>
                                        <asp:BoundField DataField="ESN" HeaderText="ESN" ReadOnly="True" Visible="True">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SKU" HeaderText="Carton" ReadOnly="True" Visible="True">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="OrderDetailReceiveDetailID" HeaderText="ID" ReadOnly="True"
                                            Visible="True">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ReceiveDetailID" HeaderText="LinkID" ReadOnly="True" Visible="True">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Message" HeaderText="Message" ReadOnly="True" Visible="True">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="D">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkIsDeleted" runat="server" ToolTip="Check to Delete" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="border-style: solid none none none; border-width: thick; border-color: #E2E2E2;
                                text-align: left; vertical-align: middle;">
                                <asp:HiddenField ID="hdnIsDisabled" runat="server" />
                                <asp:Button ID="btnDetailRecieve_OK" runat="server" Text="OK" OnClientClick="ShouldDisableButton(this);" />
                                <%--<asp:Button ID="Button2" runat="server" Text="OK"  OnClientClick="DisableButton(this);"/>--%>
                                <%--<asp:Button ID="btnAddPostSerializeInventory" runat="server" Text="Serialize" ToolTip="Post Inventory Change from Serialized IMEI data" />--%>
                                <%--<asp:Button ID="btnAddPostBatchInventory" runat="server" Text="Bulk" ToolTip="Post Inventory Change from Bulk Inventory" />--%>
                                <asp:Button ID="btnDetailRecieve_Cancel" runat="server" Text="Cancel" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </asp:Panel>


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
            <syncfusion:Window ID="winGetPassword" Title="Authorization" runat="server" DraggingStyle="Original"
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
                                <asp:TextBox ID="txtAuthUserName" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="CurrentPasswordLabel" runat="server" AssociatedControlID="CurrentPassword">Password:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="CurrentPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Button ID="btnAuthorize" runat="server" Text="Authorize" OnClientClick="Authorize();" />
                                <asp:Button ID="btnAuthorizeCancel" runat="server" Text="Cancel" OnClientClick="AuthorizeCancel();return false;" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </syncfusion:Window>
        </ContentTemplate>
    </asp:UpdatePanel>
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
            var win = window.open(WindowToOpen, "_blank", "menubar", true);
            return;
        }




        ////////////////////////////////////////////////////////////
        // *****************************************************************************
        function OpenAuthorization() {
            $find('<%=this.winGetPassword.ClientID%>').Title = "Authorization Required";
            $find('<%=this.winGetPassword.ClientID%>').Open(null, null);
        }
        function Authorize() {
            $find('<%=winGetPassword.ClientID%>').Close();
        }
        function AuthorizeCancel() {
            $find('<%=winGetPassword.ClientID%>').Close();
        }

        // *****************************************************************************
        function OpenClientSearch(Address) {
            MCL("TargetAddress").value = Address;
            $find('<%=this.wndSelectClientLocation.ClientID%>').Title = "Client Search";
            $find('<%=this.wndSelectClientLocation.ClientID%>').Open(null, null);
        }



        function selx(ID) {
            $find('<%=wndSelectClientLocation.ClientID%>').Close();
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
            var SearchResults = $get("<%= pnlSearchResult.ClientID %>");
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
            var Carton = $get("<%= txtSku.ClientID %>").value;
            Carton = trim(Carton);
            if (Carton.length == 0) {
                alert("Carton required!");
                return;
            }
            var newESN = $get("<%= ScanKey.ClientID %>").value;
            newESN = trim(newESN);
            if (newESN.length > 0) {

                var totalrowcount = 0;
                var grid = window["<%= grdOrderDetailRecieve.ClientID %>"];
                if (grid == undefined) {
                    totalrowcount = 0
                }
                else {
                    totalrowcount = grid.rows.length;
                }
                var gridCount = parseInt(totalrowcount);
                // Remove one for the Grid Header row.
                if (gridCount > 0) { gridCount--; }

                $get("<%= ScanKey.ClientID %>").value = "";
                var rCount = $get("<%= hdnCountRequired.ClientID %>").value;
                var riCount = parseInt(rCount);
                var count = $get("<%= txtCount.ClientID %>").value;
                var iCount = parseInt(count);
                if (riCount > iCount + gridCount) {
                    var newESN = $get("<%= txtSku.ClientID %>").value + ":" + newESN;
                    var OldESN = $get("<%= txtESNScan.ClientID %>").value;
                    OldESN = OldESN.trim();
                    $get("<%= txtESNScan.ClientID %>").value = OldESN + " " + newESN;
                    iCount++;
                    $get("<%= txtCount.ClientID %>").value = iCount.toString();
                }
                if (riCount == iCount + gridCount) { iCount += gridCount; alert("Max QTY of Units reached! - " + iCount.toString()); }
            }
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
            var service = new WebServer_01();
            if (DropDownName == "Carrier") {
                var ctr = $get(MCL("hdnCarrierID").value);
                if (ctr == null) { return; }
                var rValue = service.GetManufacturerDropDownData_WithScanCode(GetDropDownValue(MCL("hdnCarrierID").value), MCL("UserName").value, onFillManufacturerList, null, null);
                return;
            }
            if (DropDownName == "Manufacturer") {
                var ctr = $get(MCL("hdnCarrierID").value);
                if (ctr == null) { return; }
                ctr = $get(MCL("hdnManufacturerID").value);
                if (ctr == null) { return; }
                var rValue = service.GetModelDropDownData_WithScanCode(GetDropDownValue(MCL("hdnCarrierID").value), GetDropDownValue(MCL("hdnManufacturerID").value), MCL("UserName").value, onFillModelList, null, null);
                return;
            }
            if (DropDownName == "Model") {
                var ctr = $get(MCL("hdnCarrierID").value);
                if (ctr == null) { return; }
                ctr = $get(MCL("hdnManufacturerID").value);
                if (ctr == null) { return; }
                ctr = $get(MCL("hdnModelID").value);
                if (ctr == null) { return; }
                var rValue = service.GetColourDropDownData_WithScanCode(GetDropDownValue(MCL("hdnCarrierID").value), GetDropDownValue(MCL("hdnManufacturerID").value), GetDropDownValue(MCL("hdnModelID").value), MCL("UserName").value, onFillColourList, null, null);
                return;
            }
        }

        function onFillManufacturerList(Result) {
            if (Result.length = 0 || MCL("chkRestricted").checked != true) { return; }
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
            if (Result.length = 0 || MCL("chkRestricted").checked != true) { return; }
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



            //            var DropDown = $get(MCL("hdnModelID").value)
            //            var CurrentValue = GetDropDownValue(MCL("hdnModelID").value);
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
            FillDropDown("Model");
            return;
        }

        function onFillColourList(Result) {
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



            //            var DropDown = $get(MCL("hdnColourID").value)
            //            var CurrentValue = GetDropDownValue(MCL("hdnColourID").value);
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


