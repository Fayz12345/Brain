<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Maint_MasterPartsTechAssign.aspx.cs" Inherits="BW_WebApp.Maintenance.Maint_MasterPartsTechAssign" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>
<%@ Register Assembly="Syncfusion.Grid.Grouping.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Grid.Grouping" TagPrefix="syncfusion" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>

<asp:Content ContentPlaceHolderID="cMC" runat="server">
    <asp:HiddenField ID="hdnUserName" runat="server" ClientIDMode="Static" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:TabContainer runat="server" ID="tabMain" CssClass="tab-container">

                <asp:TabPanel runat="server" ID="TabPanelNew" CssClass="tab-panel" Enabled="true" HeaderText="Assign Parts To Unit" Visible="false">
                    <ContentTemplate>

                        <h1>Assign Parts</h1>

                        <div class="row">
                        	<div class="col-12 col-md">
                                <asp:Label ID="lblWarningMessage" runat="server" Text="" />

                                <asp:Label runat="server" Text="Location:" AssociatedControlID="drpLocationList" />
                                <asp:DropDownList ID="drpLocationList" runat="server" ToolTip="Location where these parts are located" AutoPostBack="true" />

                                <asp:Label runat="server" Text="Technician:" AssociatedControlID="drpTechList" />
                                <asp:DropDownList ID="drpTechList" runat="server" ToolTip="Technician" AutoPostBack="true" />

                                <asp:Label ID="lblIMEI" runat="server" Text="IMEI:" AssociatedControlID="txtIMEI" />
                                <asp:TextBox ID="txtIMEI" runat="server" ClientIDMode="Static" />

                                <asp:Label ID="Label10" runat="server" Text="Part Number:" AssociatedControlID="txtPartNumber" />
                                <asp:TextBox ID="txtPartNumber" runat="server" ClientIDMode="Static" />

                                <asp:Button ID="btnAssign" runat="server" Text="Assign" />
                                <asp:Button ID="btnClear" runat="server" Text="Clear" />
                            </div>
                        	<div class="col-12 col-md">
                                <asp:TextBox ID="txtCount" runat="server" ClientIDMode="Static" Enabled="False" Text="0" ToolTip="Count" />
                                <asp:HiddenField ID="PartNumberList" runat="server" Value="" />
                                <asp:ListBox ID="lstHistory" runat="server" SelectionMode="Single" ViewStateMode="Inherit" ClientIDMode="Static" />
                            </div>
                        </div>
                        
                        <div class="w-md-50">
                            <h2>Search</h2>

                            <asp:Label runat="server" Text="Category:" AssociatedControlID="drpDropPart_03" />
                            <asp:DropDownList ID="drpDropPart_03" runat="server" ToolTip="Drop Part" AutoPostBack="true" />

                            <asp:Label runat="server" Text="Manufacturer:" AssociatedControlID="drpManufacturer_03" />
                            <asp:DropDownList ID="drpManufacturer_03" runat="server" ToolTip="Manufacturer" AutoPostBack="true">
                                <asp:ListItem Text="A" Value="A" />
                                <asp:ListItem Text="A" Value="A" />
                                <asp:ListItem Text="A" Value="A" />
                                <asp:ListItem Text="A" Value="A" />
                                <asp:ListItem Text="A" Value="A" />
                            </asp:DropDownList>

                            <asp:Button ID="btnRefresh" runat="server" Text="Go" />

                            <asp:Panel ID="pnlMainGridPN" runat="server">
                                <asp:GridView ID="MainGridPN" CssClass="table" runat="server" DataKeyNames="MasterPartsID" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="MasterPartsID" HeaderText="ID" ReadOnly="True" Visible="false" />
                                        <asp:TemplateField HeaderText="P">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="imgAssignPart" runat="server" ToolTip="Assign Part">
                                                    <span class="oi oi-arrow-circle-right"></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="PartNumber" HeaderText="MFG Part Number" ReadOnly="True" />
                                        <asp:BoundField DataField="GMPPartNumber" HeaderText="IMM Part Number" ReadOnly="True" />
                                        <asp:BoundField DataField="GMPPartDescription" HeaderText="Desc" ReadOnly="True" />
                                        <asp:BoundField DataField="Quantity" HeaderText="QTY" ReadOnly="True" />
                                        <asp:BoundField DataField="Model" HeaderText="Model" ReadOnly="True" />
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </div>

                    </ContentTemplate>
                </asp:TabPanel>

                <asp:TabPanel runat="server" ID="TabPanelPick" CssClass="tab-panel" Enabled="true" HeaderText="Distribution Dashboard" Visible="True">
                    <ContentTemplate>
                        
                        <h1>Distribution Dashboard</h1>

                        <div class="w-md-50">
                            <asp:Label runat="server" Text="Location:" AssociatedControlID="drpLocationList_02" />
                            <asp:DropDownList ID="drpLocationList_02" runat="server" ToolTip="Location where these parts are located" AutoPostBack="true" />

                            <asp:Label runat="server" Text="Technician:" AssociatedControlID="drpTechList_02" />
                            <asp:DropDownList ID="drpTechList_02" runat="server" ToolTip="Technician" AutoPostBack="true" />

                            <asp:Label runat="server" Text="Part Number:" AssociatedControlID="txtPartNumber_02" />
                            <asp:TextBox ID="txtPartNumber_02" runat="server" ClientIDMode="Static" ToolTip="Leave blank for all parts" />
                        </div>

                        <asp:TabContainer CssClass="tab-container" runat="server" ActiveTabIndex="0" AutoPostBack="True">

                            <asp:TabPanel CssClass="tab-panel" runat="server" Enabled="true" HeaderText="Currently Assigned Parts" Visible="True">
                                <ContentTemplate>
                                    <asp:Button ID="btnRefreshSummary" runat="server" Text="Refresh" />
                                    <asp:Panel ID="pnlSearchSummary" CssClass="mb-3" ScrollBars="Auto" runat="server">
                                        <div class="grid-grouping-control">
                                            <syncfusion:GridGroupingControl ID="grdSearchSummary" TabIndex="2" runat="server"
                                                EnableCallbacks="True" DataSourceCachingMode="ViewState"
                                                StatusBarText="List Updating.." BorderCollapse="Separate" ShowFocusedBorder="True"
                                                ShowGroupDropArea="False" NestedTableGroupOptions-ShowFilterBarCondition="False"
                                                TopLevelGroupOptions-ShowFilterStatusMessage="False" EnableAjaxPaging="False"
                                                PageSize="0" ReadOnly="True" PostBackOnRowDblClick="False" ShowLoadingIndicatorOnCallback="True"
                                                ShowSearchBox="True" EnsureCurrentRowVisibility="True">
                                                <TableDescriptor AllowEdit="false" AllowNew="false">
                                                    <%--<VisibleColumns>
                                                        <syncfusion:GridVisibleColumnDescriptor Name="ID" />
                                                    </VisibleColumns>--%>
                                                    <Columns>
                                                        <syncfusion:GridColumnDescriptor MappingName="Quantity" HeaderText="Quantity:" />
                                                        <syncfusion:GridColumnDescriptor MappingName="Manufacturer" HeaderText="Make:" />
                                                        <%--<syncfusion:GridColumnDescriptor MappingName="Model" HeaderText="Make" />--%>
                                                        <syncfusion:GridColumnDescriptor MappingName="ESN" HeaderText="ESN:" />
                                                        <syncfusion:GridColumnDescriptor MappingName="Description" HeaderText="Description:" />
                                                        <syncfusion:GridColumnDescriptor MappingName="TechName" HeaderText="Tech Name:" />
                                                        <%--<syncfusion:GridColumnDescriptor MappingName="Status" HeaderText="Status" />--%>
                                                        <syncfusion:GridColumnDescriptor MappingName="GMPPartNumber" HeaderText="IMM Part Number:" />
                                                    </Columns>
                                                </TableDescriptor>
                                            </syncfusion:GridGroupingControl>
                                        </div>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:TabPanel>

                            <asp:TabPanel CssClass="tab-panel" runat="server" Enabled="true" HeaderText="Distribution Log" Visible="True">
                                <ContentTemplate>
                                    <asp:Button ID="btnRefreshDetail" runat="server" Text="Refresh" />
                                    <asp:Panel ID="pnlSearchDetail" CssClass="mb-3" ScrollBars="Auto" runat="server">
                                        <div class="grid-grouping-control">
                                            <syncfusion:GridGroupingControl ID="grdSearchDetail" TabIndex="2" runat="server"
                                                EnableCallbacks="True" DataSourceCachingMode="ViewState"
                                                StatusBarText="List Updating.." BorderCollapse="Separate" ShowFocusedBorder="True"
                                                ShowGroupDropArea="False" NestedTableGroupOptions-ShowFilterBarCondition="False"
                                                TopLevelGroupOptions-ShowFilterStatusMessage="False" EnableAjaxPaging="False"
                                                PageSize="0" ReadOnly="True" PostBackOnRowDblClick="False" ShowLoadingIndicatorOnCallback="True"
                                                ShowSearchBox="True" EnsureCurrentRowVisibility="True">
                                                <TableDescriptor AllowEdit="false" AllowNew="false">
                                                    <%--<VisibleColumns>
                                                        <syncfusion:GridVisibleColumnDescriptor Name="ID" />
                                                    </VisibleColumns>--%>
                                                    <Columns>
                                                        <syncfusion:GridColumnDescriptor MappingName="IMEI" HeaderText="IMEI:" />
                                                        <syncfusion:GridColumnDescriptor MappingName="TechName" HeaderText="Tech Name:" />
                                                        <syncfusion:GridColumnDescriptor MappingName="Status" HeaderText="Status:" />
                                                        <syncfusion:GridColumnDescriptor MappingName="Manufacturer" HeaderText="Make:" />
                                                        <syncfusion:GridColumnDescriptor MappingName="Description" HeaderText="Description:" />
                                                        <syncfusion:GridColumnDescriptor MappingName="GMPPartNumber" HeaderText="IMM Part Number:" />
                                                        <syncfusion:GridColumnDescriptor MappingName="Quantity" HeaderText="Quantity:" />
                                                        <syncfusion:GridColumnDescriptor MappingName="CreateDate" HeaderText="CreateDate:" />
                                                        <syncfusion:GridColumnDescriptor MappingName="CreateUser" HeaderText="CreateUser:" />
                                                        <syncfusion:GridColumnDescriptor MappingName="UnitAssignedDate" HeaderText="UnitAssignedDate:" />
                                                        <syncfusion:GridColumnDescriptor MappingName="UnitAssignedUser" HeaderText="UnitAssignedUser:" />
                                                        <syncfusion:GridColumnDescriptor MappingName="ReturningDate" HeaderText="ReturningDate:" />
                                                        <syncfusion:GridColumnDescriptor MappingName="ReturningUser" HeaderText="ReturningUser:" />
                                                        <syncfusion:GridColumnDescriptor MappingName="ReturnedDate" HeaderText="ReturnedDate:" />
                                                        <syncfusion:GridColumnDescriptor MappingName="ReturnedUser" HeaderText="ReturnedUser:" />
                                                        <syncfusion:GridColumnDescriptor MappingName="ShippedDate" HeaderText="ShippedDate:" />
                                                        <syncfusion:GridColumnDescriptor MappingName="ShippedUser" HeaderText="ShippedUser:" />
                                                        <syncfusion:GridColumnDescriptor MappingName="LastUpdateDate" HeaderText="LastUpdateDate:" />
                                                        <syncfusion:GridColumnDescriptor MappingName="LastUpdateUser" HeaderText="LastUpdateUser:" />
                                                        <syncfusion:GridColumnDescriptor MappingName="MasterPartsID" HeaderText="MasterPartsID:" />
                                                        <syncfusion:GridColumnDescriptor MappingName="MasterPartsLinkTableID" HeaderText="MasterPartsLinkTableID:" />
                                                        <syncfusion:GridColumnDescriptor MappingName="MasterPartsTechAssignedLogID" HeaderText="MasterPartsTechAssignedLogID:" />
                                                    </Columns>
                                                </TableDescriptor>
                                            </syncfusion:GridGroupingControl>
                                        </div>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:TabPanel>

                        </asp:TabContainer>

                    </ContentTemplate>
                </asp:TabPanel>

            </asp:TabContainer>
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


        // SCANKEY PROCESSING GOES HERE
        function RecordScanKey(pText) {

            // alert("HERE YOU ARE");
            if (PartNumber().length == 0) { SetFocus(); return false; }
            document.getElementById('<%= btnAssign.ClientID %>').click();
            // RecordDetail();
            return false;
        }


        function RecordDetail() {
            var service = new WebServer_01();
            // var esn = ESNNumber();
            var control = document.getElementById('<%= drpLocationList.ClientID %>');
            var Location = control.options[control.selectedIndex].value;
            var control = document.getElementById('<%= drpTechList.ClientID %>');
            var Tech = control.options[control.selectedIndex].text;
            var Partnum = PartNumber();
            var ESN = ESN();
            var IsReturn = "N"
            // if (document.getElementById('<= chkReturn.ClientID %>').checked == true) { IsReturn = "T"; }
            // var UserName = "";
            ClearPartNumber();
            Timer1.Start();
            // alert('inside RecordDetail:' + Partnum + ":" + Tech + ","  + Location + ":" + UserName());

            // alert('Inside Record Detail');
            service.AssignPartNumber(ESN, Partnum, Tech, Location, IsReturn, UserName(), OnAddSuccess, onError, null);

            // service.ShipUnit(esn, waybill, selectedvalue, UserName(), OnAddSuccess, null, null);
            // OnAddSuccess(esn)

        }

        function OnAddSuccess(result) {
            var n = result.indexOf("Error");
            Timer1.Stop();
            // alert('inside OnAddSuccess');
            if (n >= 0) {
                alert(result);
            }
            RecordHistory(result);
            $get("<%= txtPartNumber.ClientID %>").value = "";
            SetFocus();
        }

        function onError(Result) {
            alert("Error:" + Result);
        }

        function CleanData() {
            //            var p = ParseValue();
            //            if (p == 0) {
            //                var waybill = WayBill();
            //                if (waybill.length > 22) {
            //                    waybill = waybill.slice(11, -11);
            //                    SetWayBill(waybill);
            //                }
            //            }
        }

        function RecordHistory(Value) {
            var Source = $get("<%= lstHistory.ClientID %>");
            // Check to see if the item is already there.
            var xc = Source.getElementsByTagName('option').length;
            for (var i = 0; i < xc; i++) {
                if (Source.options[i].value == Value)
                    return;
            }
            if (Source != null) {
                var newOption = new Option(); // Create a new instance of ListItem
                newOption.text = Value + ' (' + Timer1.ElapsedMilliseconds + 'ms)';
                Source.options[Source.length] = newOption; //Append the item in Target


                var count = $get("<%= txtCount.ClientID %>").value;
                count++;
                $get("<%= txtCount.ClientID %>").value = count;


                $get("<%= PartNumberList.ClientID %>").value = $get("<%= PartNumberList.ClientID %>").value + Value + ',';

            }
            else { alert("ESN already on list!"); }
        }


        function IsMessage() {
            if (LastWayBill() != WayBill()) {
                CleanData();
                var service = new WebServer_01();
                $get("<%= lblWarningMessage.ClientID %>").innerHTML = '';
                // service.WayBillMessageQueue(WayBill(), UserName(), SeeifMessage, null, null);

                // SeeifMessage();
            }
        }

        function SeeifMessage(result) {
            $get("<%= lblWarningMessage.ClientID %>").innerHTML = result;
            SetLastWayBill();
        }



        //       // Variable Functions ----------------------------------
        //       function ParseValue() {
        //           var list = $get("%= drpLocationList.ClientID %>"); //Client ID of the radiolist
        //           var inputs = list.getElementsByTagName("input");
        //           var selected;
        //           for (var i = 0; i < inputs.length; i++) {
        //               if (inputs[i].checked) {
        //                   selected = inputs[i];
        //                   break;
        //               }
        //           }
        //           if (selected) {
        //               return selected.value
        //           }
        //           return -1;
        //       }


        function UserName() {
            return $get("<%= hdnUserName.ClientID %>").value;
        }

        function LastWayBill() {
            //return $get("%= lastWayBill.ClientID %>").value;
            return "";
        }
        function SetLastWayBill() {
            //$get("%= lastWayBill.ClientID %>").value = WayBill();

        }

        function ClearPartNumber() {
            $get("<%= txtPartNumber.ClientID %>").value = "";
        }

        function PartNumber() {
            return $get("<%= txtPartNumber.ClientID %>").value;
        }
        function ESN() {
            return $get("<%= txtIMEI.ClientID %>").value;
        }
        // Set Focus Functions ---------------------------------
        function SetFocus() {
            SetFocus("<%= txtPartNumber.ClientID %>");
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
