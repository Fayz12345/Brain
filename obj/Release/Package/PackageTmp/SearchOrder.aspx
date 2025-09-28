<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SearchOrder.aspx.cs" Inherits="BW_WebApp.SearchOrder" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
 
<asp:Content ContentPlaceHolderID="cMC" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <h1>Search Orders</h1>

            <asp:Panel runat="server" ID="pnlParameters">
                <div class="row">
                	<div class="col-md">
                        <label>Status:</label>
                        <asp:DropDownList ID="drpStatus" runat="server" />
                
                        <label>Order Number:</label>
                        <asp:TextBox ID="txtIFSOrderNumber" runat="server" ToolTip="Order Number" />

                        <label>Order Number:</label>
                        <asp:TextBox ID="txtOrderNumber" runat="server" ToolTip="Order Number" />

                        <label>Customer PO:</label>
                        <asp:TextBox ID="txtCustomerPO" runat="server" ToolTip="Customer PO" />

                        <label>WayBill Number:</label>
                        <asp:TextBox ID="txtWaybillNumber" runat="server" ToolTip="Waybill Number" />
                        
                        <label>Project Tag:</label>
                        <asp:TextBox ID="txtProjectTag" runat="server" ToolTip="Project Tag" />
                    </div>
                	<div class="col-md">
                        <div class="form-check-inline">
                            <asp:CheckBox ID="chkReceived" runat="server" ToolTip="If unchecked, this will be excluded from the filter" />
                            <label>Order Begin/End Date:</label>
                        </div>
                        <div class="form-row">
                        	<div class="col">
                                <asp:TextBox ID="txtBeginDate" runat="server" />
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtBeginDate" />
                            </div>
                        	<div class="col">
                                <asp:TextBox ID="txtEndDate" runat="server" />
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtEndDate" />
                            </div>
                        </div>

                        <label>Client Name:</label>
                        <asp:TextBox ID="txtClient" runat="server" ToolTip="Client Name" />

                        <label>City:</label>
                        <asp:TextBox runat="server" ID="txtCity" ToolTip="City" />
                
                        <label>Postal Code:</label>
                        <asp:TextBox runat="server" ID="txtPostalCode" ToolTip="Postal Code" />
                
                        <label>Phone Number:</label>
                        <asp:TextBox runat="server" ID="txtPhoneNumber" ToolTip="Phone Number" />
                
                        <label>Email Address:</label>
                        <asp:TextBox runat="server" ID="txtEmailAddress" ToolTip="Email Address" />
                    </div>
                </div>
                
                <asp:Label ID="lblMessage" runat="server" Text="" />
            </asp:Panel>

            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" />
                    <div class="table-responsive">
                        <asp:GridView ID="grdTempDetail" CssClass="table" runat="server" DataKeyField="OrderHeaderID" AutoGenerateColumns="False">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="imgOpen" runat="server" ToolTip="Open" CommandArgument="Open">
                                            <span class="oi oi-info"></span>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="imgPrint" runat="server" ToolTip="Print" CommandArgument="Print">
                                            <span class="oi oi-print"></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Status" HeaderText="Status" ReadOnly="True" />
                                <asp:BoundField DataField="QTY" HeaderText="QTY" ReadOnly="True" />
                                <asp:BoundField DataField="QTYPacked" HeaderText="Packed" ReadOnly="True" />
                                <asp:BoundField DataField="IFSOrderNo" HeaderText="Order #" ReadOnly="True" />
                                <asp:BoundField DataField="OrderNumber" HeaderText="Order #" ReadOnly="True" />
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
                                <asp:BoundField DataField="PhoneNumber" HeaderText="Phone Number" ReadOnly="True" />
                                <asp:BoundField DataField="FaxNumber" HeaderText="Fax Number" ReadOnly="True" />
                                <asp:BoundField DataField="EmailAddress" HeaderText="Email" ReadOnly="True" />

                                <asp:TemplateField HeaderText="Utility">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="imgAnalyze" runat="server" ToolTip="Download Unit Summary"
                                            OnClientClick="alert('Not yet implemented!');return false;">
                                            <span class="oi oi-info"></span>
                                        </asp:LinkButton>
                                        <%--<asp:LinkButton ID="imgVersion" runat="server"
                                            ToolTip="Run Utility to Version any 000 records on this order. - This may take a moment to run.">
                                            <span class="oi oi-wrench"></span>
                                        </asp:LinkButton>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

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




        function ExportPickPackingShipReport(RPT, ID) {

            alert("Print " + RPT);
            return;


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

    </script>
</asp:Content>


