<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BucketBoard.aspx.cs" Inherits="BW_WebApp.BucketBoard" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="cMC" runat="server">
    <h1>WIP Board</h1>
    
    <%--<asp:Label ID="lblLastupdated" runat="server" Text="Last Updated" ToolTip="Last Time Updated" />--%>
    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" />
    <asp:LinkButton ID="btnPrint" CssClass="btn btn-default" runat="server" OnClientClick="printIt(); return false;" ToolTip="Print">
        <span class="oi oi-print"></span>
    </asp:LinkButton>

    <asp:TabContainer runat="server" ID="t1x" CssClass="tab-container" ActiveTabIndex="1" BorderStyle="None">
        <asp:TabPanel runat="server" ID="tb1x" CssClass="tab-panel" Enabled="true" HeaderText="Parameters">
            <ContentTemplate>
                <asp:Panel runat="server" ID="pnlParameters">
                     <div class="row">
                    	<div class="col-md">
                            <label>Client:</label>
                            <asp:DropDownList ID="drpClientList" runat="server" ToolTip="Client" />
                            
                            <label>Project:</label>
                            <asp:DropDownList ID="drpProjectList" runat="server" ToolTip="Project" />
                            
                            <label>Status:</label>
                            <asp:DropDownList ID="drpStatus" runat="server" ToolTip="Unit Status" />
                            
                            <label>Carrier:</label>
                            <asp:DropDownList ID="drpCarrier" runat="server" ToolTip="Carrier" />
                            
                            <label>Manufacturer:</label>
                            <asp:DropDownList ID="drpManufacturer" runat="server" ToolTip="Manufacturer" />
                            
                            <label>Model:</label>
                            <asp:DropDownList ID="drpModel" runat="server" ToolTip="Model" />
                            
                            <label>Colour:</label>
                            <asp:DropDownList ID="drpColour" runat="server" ToolTip="Colour" />
                            
                            <label>IMEI:</label>
                            <asp:TextBox ID="txtIMEI" runat="server" ToolTip="IMEI" />
                            
                            <label>SKU:</label>
                            <asp:TextBox runat="server" ID="txtSKU" ToolTip="SKU" />
                            
                            <label>Dealer Waybill:</label>
                            <asp:TextBox runat="server" ID="txtDealerWayBill" ToolTip="Dealer Waybill" />
                        </div>

                        <div class="col-md">
                            <label>User Name:</label>
                            <asp:DropDownList ID="drpUser" runat="server" ToolTip="User Name" />

                            <label>Client Location:</label>
                            <asp:TextBox ID="txtClient" runat="server" ToolTip="Client Location Code, Leave Blank for all." />
                           
                            <label>RMA:</label>
                            <asp:TextBox runat="server" ID="txtRMA" />
                            
                            <label>Project Tag:</label>
                            <asp:TextBox runat="server" ID="txtProjectTag" />
                            
                            <label>BinNumber:</label>
                            <asp:TextBox runat="server" ID="txtBinNumber" />
                            
                            <label>IMM Order Number:</label>
                            <asp:TextBox runat="server" ID="txtHobble" />
                            
                            <asp:CheckBox ID="chkReceived" CssClass="d-block mb-1" runat="server" Text="Received Begin/End Date:" ToolTip="If unchecked, this will be excluded from the filter" />
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
                            
                            <asp:CheckBox ID="chkQC" CssClass="d-block mb-1" runat="server" Text="QC Begin/End Date:" ToolTip="If unchecked, this will be excluded from the filter" />
                            <div class="form-row">
                            	<div class="col">
                                    <asp:TextBox ID="txtBeginQC" runat="server" />
                                    <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtBeginQC" />
                                </div>
                                <div class="col">
                                    <asp:TextBox ID="txtEndQC" runat="server" />                                    
                                    <asp:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtEndQC" />
                                </div>
                            </div>
                            
                            <asp:CheckBox ID="chkShipped" CssClass="d-block mb-1" runat="server" Text="Shipped Begin/End Date:" ToolTip="If unchecked, this will be excluded from the filter" />
                            <div class="form-row">
                            	<div class="col-md">
                                    <asp:TextBox ID="txtBeginShipped" runat="server" />
                                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtBeginShipped" />
                                </div>
                            	<div class="col-md">
                                    <asp:TextBox ID="txtEndShipped" runat="server" />
                                    <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtEndShipped" />
                                </div>
                            </div>
                            
                            <asp:CheckBox ID="chkShowSummary" CssClass="d-block mb-1" runat="server" Text="Report Summary Records" ToolTip="Report Summary Records" Checked="True" />
                            <asp:CheckBox ID="chkGroupProcess" CssClass="d-block mb-1" runat="server" Text="Group Process" ToolTip="Group Process Records" Checked="True" />
                        </div>
                    </div>

                    <asp:Label ID="lblMessage" runat="server" />
                </asp:Panel>
            </ContentTemplate>
        </asp:TabPanel>

        <asp:TabPanel runat="server" ID="TabPanel1" CssClass="tab-panel" Enabled="true" HeaderText="Stats">
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate> 
                        <asp:TabContainer ID="TabContainer1" CssClass="tab-container" runat="server">
                            <asp:TabPanel ID="TabPanel11" CssClass="tab-panel" runat="server" Enabled="true" HeaderText="Reporting Period">
                                <ContentTemplate>
                                    <%--<div>
                                        <asp:Label ID="lblDateTime" runat="server" Text="Date Time" />
                                    </div>--%>
                                    <asp:GridView ID="grdStatsToday" CssClass="table" runat="server" AutoGenerateColumns="False" ShowHeader="True">
                                        <Columns>
                                            <asp:BoundField DataField="Action" HeaderText="Action" ReadOnly="True" />
                                            <asp:BoundField DataField="Freq" HeaderText="Freq" ReadOnly="True" />
                                            <asp:BoundField DataField="FreqGrand" HeaderText="Grand" ReadOnly="True" />
                                            <asp:BoundField DataField="Client" HeaderText="Client" ReadOnly="True" />
                                            <asp:BoundField DataField="ClientLocation" HeaderText="Location" ReadOnly="True" />
                                            <asp:BoundField DataField="Project" HeaderText="Project" ReadOnly="True" />
                                            <asp:BoundField DataField="Process" HeaderText="Process" ReadOnly="True" />
                                            <asp:BoundField DataField="UserName" HeaderText="UserName" ReadOnly="True" />
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:TabPanel>
                            <asp:TabPanel ID="TabPanelx" CssClass="tab-panel" runat="server" Enabled="true" HeaderText="Prior Period">
                                <ContentTemplate>
                                    <%--<div>
                                        <asp:Label ID="lblPriorPeriod" runat="server" Text="Date Time" />
                                    </div>--%>
                                    <asp:GridView ID="grdStatsPrior" CssClass="table" runat="server" AutoGenerateColumns="False" ShowHeader="True">
                                        <Columns>
                                            <asp:BoundField DataField="Action" HeaderText="Action" ReadOnly="True" />
                                            <asp:BoundField DataField="Freq" HeaderText="Freq" ReadOnly="True" />
                                            <asp:BoundField DataField="FreqGrand" HeaderText="Grand" ReadOnly="True" />
                                            <asp:BoundField DataField="Client" HeaderText="Client" ReadOnly="True" />
                                            <asp:BoundField DataField="ClientLocation" HeaderText="Location" ReadOnly="True" />
                                            <asp:BoundField DataField="Project" HeaderText="Project" ReadOnly="True" />
                                            <asp:BoundField DataField="Process" HeaderText="Process" ReadOnly="True" />
                                            <asp:BoundField DataField="UserName" HeaderText="UserName" ReadOnly="True" />
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:TabPanel>
                            <asp:TabPanel ID="TabPanel2" CssClass="tab-panel" runat="server" Enabled="true" HeaderText="Month To Date">
                                <ContentTemplate>
                                    <asp:GridView ID="grdStatsMTD" CssClass="table" runat="server" AutoGenerateColumns="False" ShowHeader="True">
                                        <Columns>
                                            <asp:BoundField DataField="Action" HeaderText="Action" ReadOnly="True" />
                                            <asp:BoundField DataField="Freq" HeaderText="Freq" ReadOnly="True" />
                                            <asp:BoundField DataField="FreqGrand" HeaderText="Grand" ReadOnly="True" />
                                            <asp:BoundField DataField="Client" HeaderText="Client" ReadOnly="True" />
                                            <asp:BoundField DataField="ClientLocation" HeaderText="Location" ReadOnly="True" />
                                            <asp:BoundField DataField="Project" HeaderText="Project" ReadOnly="True" />
                                            <asp:BoundField DataField="Process" HeaderText="Process" ReadOnly="True" />
                                            <asp:BoundField DataField="UserName" HeaderText="UserName" ReadOnly="True" />
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:TabPanel>
                            <asp:TabPanel ID="TabPanel3" CssClass="tab-panel" runat="server" Enabled="true" HeaderText="Year To Date">
                                <ContentTemplate>
                                    <asp:GridView ID="grdStatsYTD" CssClass="table" runat="server" AutoGenerateColumns="False" ShowHeader="True">
                                        <Columns>
                                            <asp:BoundField DataField="Action" HeaderText="Action" ReadOnly="True" />
                                            <asp:BoundField DataField="Freq" HeaderText="Freq" ReadOnly="True" />
                                            <asp:BoundField DataField="FreqGrand" HeaderText="Grand" ReadOnly="True" />
                                            <asp:BoundField DataField="Client" HeaderText="Client" ReadOnly="True" />
                                            <asp:BoundField DataField="ClientLocation" HeaderText="Location" ReadOnly="True" />
                                            <asp:BoundField DataField="Project" HeaderText="Project" ReadOnly="True" />
                                            <asp:BoundField DataField="Process" HeaderText="Process" ReadOnly="True" />
                                            <asp:BoundField DataField="UserName" HeaderText="UserName" ReadOnly="True" />
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:TabPanel>
                        </asp:TabContainer>
                    </ContentTemplate> 
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        <asp:AsyncPostBackTrigger ControlID="btnRefresh" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </ContentTemplate>
        </asp:TabPanel>

        <asp:TabPanel runat="server" ID="TabPanel4" CssClass="tab-panel" Enabled="true" HeaderText="Daily">
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:TabContainer ID="TabContainer2" CssClass="tab-container" runat="server">
                            <asp:TabPanel ID="TabPanel5" CssClass="tab-panel" runat="server" Enabled="true" HeaderText="Reporting Period">
                                <ContentTemplate>
                                    <%--<div>
                                        <asp:Label ID="lblDateTimeDaily" runat="server" Text="Date Time" />
                                    </div>--%>
                                    <asp:GridView ID="grdStatsTodayDaily" CssClass="table" runat="server" AutoGenerateColumns="True" ShowHeader="True" />
                                </ContentTemplate>
                            </asp:TabPanel>
                        </asp:TabContainer>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        <asp:AsyncPostBackTrigger ControlID="btnRefresh" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
    <asp:Timer ID="Timer1" runat="server" Interval="30000" OnTick="UpdateStats" />
</asp:Content>
<asp:Content ContentPlaceHolderID="js" runat="server">
    <script type="text/javascript">

        function printIt() {
            window.print();
        }

    </script>
</asp:Content>


