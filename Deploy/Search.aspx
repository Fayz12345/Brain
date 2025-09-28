<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="BW_WebApp.Search" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
 
<asp:Content ID="Content2" ContentPlaceHolderID="cMC" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <h1>Search</h1>

            <asp:TabContainer ID="TabContainer1" CssClass="tab-container" runat="server">

                <asp:TabPanel ID="TabPanel1" CssClass="tab-panel" runat="server" HeaderText="Basic Parameters">
                    <ContentTemplate>
                        <asp:Panel ID="pnlParameters" runat="server">

                            <div class="row">

                                <div class="col-lg-5 col-xl-4">
                                    <asp:Table ID="Table3" CssClass="table" runat="server">
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                Status:
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:DropDownList ID="drpStatus" runat="server" ToolTip="Unit Status">
                                                </asp:DropDownList>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                Project:
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:DropDownList ID="drpProjectList" runat="server" ToolTip="Project">
                                                </asp:DropDownList>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                IMEI:
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Left" Width="100%">
                                                    <asp:TextBox ID="txtIMEI" runat="server" ToolTip="IMEI"></asp:TextBox>
                                                </asp:Panel>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                Replacement IMEI:
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Panel ID="Panel4" runat="server" HorizontalAlign="Left" Width="100%">
                                                    <asp:TextBox ID="txtReplacementIMEI" runat="server" ToolTip="Replacement IMEI"></asp:TextBox>
                                                </asp:Panel>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                Project Tag:
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox runat="server" ID="txtProjectTag"></asp:TextBox>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                RMA:
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox runat="server" ID="txtRMA"></asp:TextBox>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </div>

                                <div class="col-lg-7 col-xl-8">
                                    <asp:Table ID="Table1" CssClass="table" runat="server">
                                        <asp:TableRow ID="rowReceivedDate">
                                            <asp:TableCell>
                                                <div class="form-check-inline">
                                                    <asp:CheckBox ID="chkReceived" runat="server" ToolTip="If unchecked, this will be excluded from the filter" />
                                                    Received Begin/End Date
                                                </div>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox ID="txtBeginDate" runat="server"></asp:TextBox>
                                                <!-- TODO: replace this -->
                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtBeginDate">
                                                </asp:CalendarExtender>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtEndDate">
                                                </asp:CalendarExtender>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow ID="rowQCDate">
                                            <asp:TableCell>
                                                <div class="form-check-inline">
                                                    <asp:CheckBox ID="chkQC" runat="server" ToolTip="If unchecked, this will be excluded from the filter" />
                                                    QC Begin/End Date                                                        
                                                </div>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox ID="txtBeginQC" runat="server"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtBeginQC">
                                                </asp:CalendarExtender>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox ID="txtEndQC" runat="server"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtEndQC">
                                                </asp:CalendarExtender>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow ID="rowShippedDate">
                                            <asp:TableCell>
                                                <div class="form-check-inline">
                                                    <asp:CheckBox ID="chkShipped" runat="server" ToolTip="If unchecked, this will be excluded from the filter" />
                                                    Shipped Begin/End Date
                                                </div>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox ID="txtBeginShipped" runat="server"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtBeginShipped">
                                                </asp:CalendarExtender>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox ID="txtEndShipped" runat="server"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtEndShipped">
                                                </asp:CalendarExtender>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                SKU:
                                            </asp:TableCell>
                                            <asp:TableCell ColumnSpan="2">
                                                <asp:TextBox runat="server" ID="txtIFSSku" ToolTip="SKU"></asp:TextBox>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                Location:
                                            </asp:TableCell>
                                            <asp:TableCell ColumnSpan="2">
                                                <asp:TextBox runat="server" ID="txtIFSLocation" ToolTip="Location"></asp:TextBox>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                Condition:
                                            </asp:TableCell>
                                            <asp:TableCell ColumnSpan="2">
                                                <asp:TextBox runat="server" ID="txtIFSCondtion" ToolTip="Condition"></asp:TextBox>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </div>

                            </div>

                            <div class="row">
                                <div class="col">
                                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:TabPanel>

                <asp:TabPanel ID="TabPanel2" CssClass="tab-panel" runat="server" HeaderText="Other">
                    <ContentTemplate>
                        <asp:Panel ID="Panel5" runat="server">

                            <div class="row">

                            	<div class="col-md-6">
                                    <asp:Table ID="Table5" runat="server">
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                Client (leave blank for all clients):
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Panel ID="Panel1" runat="server">
                                                    <asp:TextBox ID="txtClient" runat="server" ToolTip="Client Code"></asp:TextBox>
                                                </asp:Panel>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                Order Number:
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox runat="server" ID="txtHobble"></asp:TextBox>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                SKU:
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox runat="server" ID="txtSKU" ToolTip="SKU"></asp:TextBox>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                BinNumber:
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox runat="server" ID="txtBinNumber"></asp:TextBox>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </div>

                                <div class="col-md-6">
                                    <asp:Table ID="Table6" runat="server">
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                Carrier:
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:DropDownList ID="drpCarrier" runat="server" ToolTip="Carrier">
                                                </asp:DropDownList>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                Manufacturer:
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:DropDownList ID="drpManufacturer" runat="server" ToolTip="Manufacturer">
                                                </asp:DropDownList>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                Model:
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:DropDownList ID="drpModel" runat="server" ToolTip="Model">
                                                </asp:DropDownList>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                Colour:
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:DropDownList ID="drpColour" runat="server" ToolTip="Colour">
                                                </asp:DropDownList>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </div>

                            </div>

                            <div class="row">
                            	<div class="col">
                                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                </div>
                            </div>

                        </asp:Panel>
                    </ContentTemplate>
                </asp:TabPanel>

            </asp:TabContainer>

            <hr>

            <div class="form-check-inline float-right">
                <asp:CheckBox ID="chkShowGraveyard" runat="server" ToolTip="Report Graveyard Records" Text="Report graveyard records" />                
            </div>

            <asp:Button ID="btnSearch" runat="server" Text="Search" />

            <!-- TODO: find out how to make this show, then edit it propoerly -->
            
            <asp:Panel ID="Panel3" CssClass="overflow" runat="server">
                <asp:GridView ID="grdTempDetail" CssClass="table" runat="server" DataKeyField="Name"
                    AutoGenerateColumns="False">
                    <Columns>
                        <asp:TemplateField HeaderText="P" ItemStyle-Wrap="False" HeaderStyle-Wrap="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="imgOpen" CssClass="btn btn-default p-1 oi oi-spreadsheet" runat="server"
                                    HeaderText="" ToolTip="Open" />
                                <asp:LinkButton ID="imgOpenProcess" CssClass="btn btn-default p-1 oi oi-spreadsheet"
                                    runat="server" HeaderText="" ToolTip="Open Proccess" />
                                <%--<asp:LinkButton ID="imgOpenQC" CssClass="btn btn-default p-1 oi oi-spreadsheet runat="server" HeaderText="" ToolTip="Open QC Assessment" />--%>
                                <asp:LinkButton ID="imgLastUser" CssClass="btn btn-default p-1 oi oi-person" runat="server"
                                    HeaderText="" ToolTip="Last User" OnClientClick="return false;" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ProjectName" HeaderText="Project Name" ReadOnly="True"
                            HeaderStyle-Wrap="False">
                            <ItemStyle Wrap="false" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Status" HeaderText="Status" ReadOnly="True" HeaderStyle-Wrap="False">
                            <ItemStyle Wrap="false" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CurrentProcessName" HeaderText="Current Process Name"
                            ReadOnly="True" HeaderStyle-Wrap="False">
                            <ItemStyle Wrap="false" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ESN" HeaderText="ESN" ReadOnly="True" HeaderStyle-Wrap="False">
                            <ItemStyle Wrap="false" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Version" HeaderText="Version" ReadOnly="True" HeaderStyle-Wrap="False">
                            <ItemStyle Wrap="false" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SwappedESN" HeaderText="Swapped ESN" ReadOnly="True" HeaderStyle-Wrap="False">
                            <ItemStyle Wrap="false" />
                        </asp:BoundField>
                        <asp:BoundField DataField="IFSLocation" HeaderText="Location" ReadOnly="True" HeaderStyle-Wrap="False">
                            <ItemStyle Wrap="false" />
                        </asp:BoundField>
                        <asp:BoundField DataField="IFSCondition" HeaderText="Condition" ReadOnly="True" HeaderStyle-Wrap="False">
                            <ItemStyle Wrap="false" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SKU" HeaderText="SKU" ReadOnly="True" HeaderStyle-Wrap="False">
                            <ItemStyle Wrap="false" />
                        </asp:BoundField>
                        <asp:BoundField DataField="MakeModelString" HeaderText="Make Model" ReadOnly="True"
                            HeaderStyle-Wrap="False">
                            <ItemStyle Wrap="false" />
                        </asp:BoundField>
                        <asp:BoundField DataField="RMANumber" HeaderText="RMA Number" ReadOnly="True" HeaderStyle-Wrap="False">
                            <ItemStyle Wrap="false" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ProjectTag" HeaderText="Project Tag" ReadOnly="True" HeaderStyle-Wrap="False">
                            <ItemStyle Wrap="false" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ReceiveDate" HeaderText="Receive Date" ReadOnly="True"
                            HeaderStyle-Wrap="False">
                            <ItemStyle Wrap="false" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ShipDate" HeaderText="Ship Date" ReadOnly="True" HeaderStyle-Wrap="False">
                            <ItemStyle Wrap="false" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Date_QC" HeaderText="QC Date" ReadOnly="True" HeaderStyle-Wrap="False">
                            <ItemStyle Wrap="false" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CompanyName" HeaderText="Company Name" ReadOnly="True"
                            HeaderStyle-Wrap="False">
                            <ItemStyle Wrap="false" />
                        </asp:BoundField>
                        <asp:BoundField DataField="StoreNumber" HeaderText="Store Number" ReadOnly="True"
                            HeaderStyle-Wrap="False">
                            <ItemStyle Wrap="false" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Open" ItemStyle-Wrap="False" HeaderStyle-Wrap="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="imgAnalyze" CssClass="btn btn-default p-1 oi oi-info" runat="server"
                                    HeaderText="*" ToolTip="Download Unit Summary" />
                                <asp:LinkButton ID="imgBagTag" CssClass="btn btn-default p-1 oi oi-print" runat="server"
                                    HeaderText="*" ToolTip="Print Bag Tag" />
                                <asp:LinkButton ID="ImageKitting" CssClass="btn btn-default p-1 oi oi-print" runat="server"
                                    HeaderText="*" ToolTip="Print Kitting Label" />
                                <asp:LinkButton ID="ImgPartDetails" CssClass="btn btn-default p-1 oi oi-pencil" runat="server"
                                    HeaderText="*" ToolTip="Edit Unit Part Details" />
                                <asp:LinkButton ID="ImageResetBin" CssClass="btn btn-default p-1 oi oi-action-undo"
                                    runat="server" HeaderText="*" ToolTip="Reset the Bin to blank" />
                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" TargetControlID="ImageResetBin"
                                    ConfirmText="Are you sure you want to reset the bin to blank?" />
                                <asp:LinkButton ID="ImageCorrect" CssClass="btn btn-default p-1 oi oi-wrench" runat="server"
                                    HeaderText="*" ToolTip="Run Utility to correct known consistency issues" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </asp:Panel>

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

        function OpenUnit(ID, PID, ProcessName) {

            if (ID.length == 0 || PID.length == 0) { return; }

            //var pstring = GetParameterStream(GetReportParameterList("CLIENTSUBMIT"));
            //var pstring = "ESN=A00000291E3942&ID=3251&PID=1"

            var pstring = "ID=" + ID + "&PID=" + PID + "&PName=" + ProcessName;
            //var pstring = "ESN=A00000291E3942&ID=3251&PID=5"


            // var WindowToOpen = "RPT_SpotCountReport.aspx";
            var WindowToOpen = "Receive.aspx";

            if (pstring.length > 0) {
                WindowToOpen = WindowToOpen + "?" + pstring
            }
            var win = window.open(WindowToOpen, "_blank", "", true);
            //var win = window.open(WindowToOpen, "_blank", "menubar", true);
            return;
        }

        function OpenUnitPartScreen(ID, ESN) {
            if (ID.length == 0) { return; }

            //var pstring = GetParameterStream(GetReportParameterList("CLIENTSUBMIT"));
            //var pstring = "ESN=A00000291E3942&ID=3251&PID=1"

            var pstring = "ID=" + ID + "&ESN=" + ESN;
            //var pstring = "ESN=A00000291E3942&ID=3251&PID=5"
            // var WindowToOpen = "RPT_SpotCountReport.aspx";
            var WindowToOpen = "ReceiveDetailEditParts.aspx";

            if (pstring.length > 0) {
                WindowToOpen = WindowToOpen + "?" + pstring
            }
            var win = window.open(WindowToOpen, "_blank", "", true);
            //var win = window.open(WindowToOpen, "_blank", "menubar", true);
            return;
        }

        ///////////////////////////////////////////////////////////////////

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

        function OpenUnitAnalysisRPT(cmdText) {
            var xDataList = {};
            xDataList["RPT"] = "UNITANALYSIS";
            xDataList["ID"] = cmdText;
            var pstring = GetParameterStream(xDataList);
            // var WindowToOpen = "RPT_SpotCountReport.aspx";
            var WindowToOpen = "/Reports/RPT_EXCEL_Out.aspx";
            if (pstring.length > 0) {
                WindowToOpen = WindowToOpen + "?" + pstring
            }
            var win = window.open(WindowToOpen, "_blank", "menubar", true);
            return;
        }

        function OpenbagTag(cmdText) {
            var xDataList = {};
            xDataList["RPT"] = "Bagtag";
            xDataList["RDID"] = cmdText;
            xDataList["ISTHREAD"] = "N";
            var pstring = GetParameterStream(xDataList);

            var WindowToOpen = 'BagTag.aspx';
            if (pstring.length > 0) {
                WindowToOpen = WindowToOpen + '?' + pstring
            }
            var win = window.open(WindowToOpen, '_blank', 'menubar', true);
        }

        function OpenKitting(cmdText) {
            var xDataList = {};
            xDataList["RPT"] = "PRODUCTLABEL";
            xDataList["RDID"] = cmdText;
            xDataList["ISTHREAD"] = "N";
            var pstring = GetParameterStream(xDataList);
            var WindowToOpen = 'FinishProductLabel.aspx';
            if (pstring.length > 0) {
                WindowToOpen = WindowToOpen + '?' + pstring
            }
            var win = window.open(WindowToOpen, '_blank', 'menubar', true);

        }

        //        function ResetBin(cmdText) {
        //            alert(cmdText);
        ////            var xDataList = {};
        ////            xDataList["RPT"] = "UNITANALYSIS";
        ////            xDataList["ID"] = cmdText;
        ////            var pstring = GetParameterStream(xDataList);
        ////            // var WindowToOpen = "RPT_SpotCountReport.aspx";
        ////            var WindowToOpen = "RPT_EXCEL_Out.aspx";
        ////            if (pstring.length > 0) {
        ////                WindowToOpen = WindowToOpen + "?" + pstring
        ////            }
        ////            var win = window.open(WindowToOpen, "_blank", "menubar", true);
        ////            return;
        //        }


    </script>

</asp:Content>

