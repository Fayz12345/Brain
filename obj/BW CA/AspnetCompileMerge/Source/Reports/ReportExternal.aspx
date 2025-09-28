<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReportExternal.aspx.cs" Inherits="BW_WebApp.Reports.ReportExternal" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>
<%@ Register Assembly="Syncfusion.Tools.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89"  Namespace="Syncfusion.Web.UI.WebControls.Tools" TagPrefix="syncfusion" %>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="cMC" runat="server">
    
    <h1>Reporting Suite</h1>

    <%--<asp:Panel runat="server" ID="pnlUpload">
        <div class="row">
            <div class="col-sm">
                <div class="custom-file mb-2">
                    <asp:FileUpload ID="IMEIUploadFile" CssClass="custom-file-input" runat="server" />
                    <label class="custom-file-label">Choose file</label>
                </div>
            </div>
            <div class="col-sm text-sm-right">
                <asp:Button ID="btnIMEIUpload" runat="server" Text="Upload" OnClientClick="return true;" OnClick="btnUpload_Click" />
                <asp:Label ID="lblUploadMSG" runat="server" Visible="False" />
            </div>
        </div>
    </asp:Panel>--%>

    <asp:TabContainer ID="TabContainer2" CssClass="tab-container" runat="server">
        <asp:TabPanel ID="TabPanel7" CssClass="tab-panel" HeaderText="A" runat="server">
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

                            <label>IMEI/ESN:</label>
                            <asp:TextBox ID="txtIMEI" runat="server" ToolTip="IMEI" MaxLength="50" />

                            <label>Version:</label>
                            <asp:TextBox ID="txtIMEIVersion" runat="server" MaxLength="3" Text="000"
                                ToolTip="Used Used only for the reports:Detail Inventory and Detail Inventory with Client." />

                            <label>Replacement IMEI/ESN:</label>
                            <asp:TextBox ID="txtReplacementIMEI" runat="server" ToolTip="Replacement IMEI" />

                            <label>SKU:</label>
                            <asp:TextBox runat="server" ID="txtSKU" ToolTip="SKU" />
                            
                            <label>*Product Place:</label>
                            <asp:DropDownList ID="drpProductPlace" runat="server" ToolTip="Used only for the reports:Detail Inventory.">
                            </asp:DropDownList>
                        </div>
                    	<div class="col-md">
                            <label>Dealer Waybill:</label>
                            <asp:TextBox runat="server" ID="txtDealerWayBill" ToolTip="Dealer Waybill" />


                            <label>Client Location:</label>
                            <asp:TextBox ID="txtClient" runat="server" ToolTip="Client Location Code, Leave Blank for all." />

                            <label>RMA:</label>
                            <asp:TextBox runat="server" ID="txtRMA" />

                            <label>Project Tag:</label>
                            <asp:TextBox runat="server" ID="txtProjectTag" />

                            <label>Bin Number:</label>
                            <asp:TextBox runat="server" ID="txtBinNumber" />

                            <label>IMM Order Entry Client:</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtOrderEntryClientLocationKey" runat="server" ToolTip="Enter client location Scan Key here." />
                                <div class="input-group-append">
                                    <asp:LinkButton ID="btnSearchClient" CssClass="btn btn-default" runat="server" ToolTip="Search Clients"
                                        OnClientClick="alert('Not Implemented Yet');return false;">
                                        <span class="oi oi-magnifying-glass"></span>
                                    </asp:LinkButton>
                                </div>
                            </div>

                            <label>IMM Order Number:</label>
                            <asp:TextBox runat="server" ID="txtHobble" />

                            <asp:CheckBox ID="chkReceived" CssClass="d-inline-block mb-1" runat="server" Text="Received Begin/End Date:"
                                ToolTip="If unchecked, this will be excluded from the filter" />
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
                            
                            <asp:CheckBox ID="chkQC" CssClass="d-inline-block mb-1" runat="server" Text="QC Begin/End Date:" ToolTip="If unchecked, this will be excluded from the filter" />
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

                            <asp:CheckBox ID="chkFunctionTest" CssClass="d-inline-block mb-1" runat="server" Text="Function Test Begin/End Date:" ToolTip="Not Implemented Yet" />
                            <div class="form-row">
                            	<div class="col">
                                    <asp:TextBox ID="txtBeginFunctionTest" runat="server" ToolTip="Not Implemented Yet" />
                                    <asp:CalendarExtender ID="CalendarExtender13" runat="server" TargetControlID="txtBeginFunctionTest" />
                                </div>
                                <div class="col">
                                    <asp:TextBox ID="txtEndFunctionTest" runat="server" ToolTip="Not Implemented Yet" />
                                    <asp:CalendarExtender ID="CalendarExtender14" runat="server" TargetControlID="txtEndFunctionTest" />
                                </div>
                            </div>
                            
                            <asp:CheckBox ID="chkShipped" CssClass="d-inline-block mb-1" runat="server" Text="Shipped Begin/End Date:"
                                ToolTip="If unchecked, this will be excluded from the filter" />
                            <div class="form-row">
                            	<div class="col">
                                    <asp:TextBox ID="txtBeginShipped" runat="server" />
                                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtBeginShipped" />
                                </div>
                                <div class="col">
                                    <asp:TextBox ID="txtEndShipped" runat="server" />
                                    <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtEndShipped" />
                                </div>
                            </div>
                            
                            <!-- <asp:CheckBox ID="chkShowGraveyard" runat="server" Text="Report Graveyard Records" ToolTip="Report Graveyard Records" /> -->
                        </div>
                    </div>
                    <asp:CheckBox ID="chkCSV" runat="server" Text="CSV" ToolTip="Export as CSV File" />
                    <asp:Label ID="lblMessage" runat="server" />
                </asp:Panel>

                <asp:TabContainer ID="TabContainer1" CssClass="tab-container" runat="server">
                    <asp:TabPanel ID="Inventory" CssClass="tab-panel" HeaderText="Inventory" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="btnExportDetailInventoryList" runat="server" Text="Detail Inventory"
                                CommandArgument="GetMasterDetailInventoryList" CommandName="MasterInventoryList"
                                OnClientClick="WaitingMessage();" />
                            <asp:Button ID="btnExportDetailInventoryListPDF" runat="server" Text="Detail Inventory PDF"
                                CommandArgument="GetMasterDetailInventoryListPDF" CommandName="MasterInventoryList"
                                OnClientClick="WaitingMessage();" />
                            <asp:Button ID="btnExportDetailInventoryClientList" runat="server" Text="Detail Inventory with Client"
                                CommandArgument="GetMasterDetailInventoryClientList" CommandName="MasterInventoryClientList"
                                OnClientClick="WaitingMessage();" />
                            <%--<asp:Button ID="btnExportDetailInventoryProcessingLog" runat="server" Text="Detail Inventory ProcessingLog"
                                CommandArgument="GetMasterDetailInventoryProcessingLogList" CommandName="MasterInventoryProcessingLogList"
                                OnClientClick="WaitingMessage();" />--%>
                            <asp:Button ID="btnExportPartNumberInventory" runat="server" Text="Partnumber Inventory"
                                CommandArgument="GetMasterPartNumberInventoryList" CommandName="PartnumberInventoryList"
                                OnClientClick="WaitingMessage();" />
                            <asp:Button ID="btnExportDockingReport" runat="server" Text="Docking Report" CommandArgument="GetMasterDockingReportList"
                                CommandName="MasterDockingList" OnClientClick="WaitingMessage();" />
                            <asp:Button ID="btnAvailableStock" runat="server" Text="IMM Available Stock" Visible="false"
                                CommandArgument="GetMasterLocationAvailableUnits_TemplateRawData_01" CommandName="AvailableStockList"
                                OnClientClick="WaitingMessage();" />
                            <asp:Button ID="btnAvailableStockLocation" runat="server" Text="IMM Available Stock w/Location"
                                Visible="false" CommandArgument="GetMasterLocationAvailableUnits_TemplateRawData_01"
                                CommandName="AvailableStockList" OnClientClick="WaitingMessage();" />
                            <asp:Button ID="btnPreReceive" runat="server" Text="PreReceive" CommandArgument="GetMasterPreReceive_TemplateRawData_01"
                                CommandName="PreReceiveList" OnClientClick="WaitingMessage();" />
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="Billing" CssClass="tab-panel" HeaderText="Billing" runat="server" Visible="false">
                        <ContentTemplate>
                            <asp:CheckBox ID="chkBillingpoint" runat="server" Text="Billing Point Create Date:"
                                ToolTip="If unchecked, this will be excluded from the filter" />
                            <div class="form-row">
                            	<div class="col">
                                    <asp:TextBox ID="txtBPStartDate" runat="server" />
                                    <asp:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="txtBPStartDate" />
                                </div>
                            	<div class="col">
                                    <asp:TextBox ID="txtBPEndDate" runat="server" />
                                    <asp:CalendarExtender ID="CalendarExtender8" runat="server" TargetControlID="txtBPEndDate" />
                                </div>
                            </div>
                            
                            <asp:CheckBox ID="chkShowRecorded" CssClass="d-block" Text="Include completed transactions" runat="server" />
                            
                            <asp:Button ID="btnBilling" runat="server" Text="Billing Point Report" CommandArgument="Javascript" CommandName="Javascript" />
                            <div class="row">
                                <div class="col-sm">
                                    <div class="custom-file">
                                        <asp:FileUpload ID="FileUploadBilling" CssClass="custom-file-input" runat="server" />
                                        <label class="custom-file-label">Choose file</label>
                                    </div>
                                </div>
                                <div class="col-sm text-sm-right">
                                    <asp:Button ID="btnUploadBilling" runat="server" Text="Upload Billing File" OnClick="btnUploadBilling_Click" />
                                    <asp:Label ID="lblMsgBilling" runat="server" Visible="False" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="Dashboard" CssClass="tab-panel" HeaderText="DashBoard" runat="server" Visible="false">
                        <ContentTemplate>
                            <label>User Name:</label>
                            <asp:TextBox ID="txtUsername" runat="server" />
                            <asp:CheckBox ID="chkSummarize" Text="Summarize" runat="server" />
                            <asp:CheckBox ID="chkGroupProcess" Text="Group Process" runat="server" />
                            <asp:Button ID="btnStatistical" runat="server" Text="Statistical Summarized Raw Data"
                                CommandArgument="GetMasterStatistical_SummarizedRawData_01" CommandName="StatisticalSummarized" />
                            <asp:Button ID="btnStatisticalRaw" runat="server" Text="Statistical Raw Data" CommandArgument="GetMasterStatistical_RawData_01"
                                CommandName="StatisticalRaw.xls" />
                            <asp:Button ID="btnStatisticalBucket" runat="server" Text="Bucket Summarized Data"
                                CommandArgument="GetMasterStatistical_SummarizedRawBucketData_02" CommandName="BucketSummarized" />
                            <asp:Button ID="btnStatisticalRawBucket" runat="server" Text="Bucket Data (Raw)"
                                CommandArgument="GetMasterStatistical_RawBucketData_02" CommandName="BucketRaw" />
                            <asp:Button ID="btnStatisticalBucketDaily" runat="server" Text="Bucket Data (Daily)"
                                CommandArgument="GetMasterStatistical_SummarizedDailyBucketData_02" CommandName="BucketDaily" />
                            <asp:Button ID="btnBucketCounters" runat="server" Text="Bucket Counters" CommandArgument="Report_MasterBucketTransactions"
                                CommandName="BucketCounters.xls" />
                            <asp:Button ID="btnProcessAverageTime" runat="server" Text="Process Average Time"
                                CommandArgument="GetProcessAverageTime" CommandName="ProcessAverageTime" Visible="False" />
                            <asp:Button ID="btnOutFreq" runat="server" Text="Frequency Moved" CommandArgument="GetReportProcessOutFreq"
                                CommandName="GetReportProcessOutFreq" Visible="False" />
                            <asp:Button ID="btnBulkQty" runat="server" Text="Current Bulk Process Quantity" CommandArgument="GetReportBulkProcessFreq"
                                CommandName="GetReportBulkProcessFreq" Visible="False" />
                            <asp:Button ID="btnQty" runat="server" Text="Current Process Quantity" CommandArgument="GetReportProcessFreq"
                                CommandName="GetReportProcessFreq" Visible="False" />
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="Inventory_Billing" CssClass="tab-panel" HeaderText="Inventory Billing" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="btnExportDetailInventoryClientListAbriviated" runat="server" Text="Inventory Billing Report"
                                CommandArgument="GetMasterDetailInventoryClientList_TemplateRawData_01_Abriviated"
                                CommandName="GetMasterDetailInventoryClientList_TemplateRawData_01_Abriviated.xls"
                                OnClientClick="WaitingMessage();" />
                            <asp:Button ID="btnExportDetailInventoryClientListAbriviatedB" runat="server" Text="Inventory Billing Report - B"
                                CommandArgument="GetMasterDetailInventoryClientList_TemplateRawData_01_Abriviated_B"
                                CommandName="GetMasterDetailInventoryClientList_TemplateRawData_01_Abriviated_B.xls"
                                OnClientClick="WaitingMessage();" />
                            <%--<asp:Button ID="btnGenerateBilling" runat="server" Text="Generate Inventory Billing Report"
                                CommandArgument="GetMasterDetailInventoryClientList_TemplateRawData_01_Abriviated_ToTable" CommandName="GetMasterDetailInventoryClientList_TemplateRawData_01_Abriviated_ToTable.xls"
                                OnClientClick="WaitingMessage();" />
                            <asp:Button ID="btnDownloadBilling" runat="server" Text="Get Billing Report"
                                CommandArgument="Select * from ReceiveDetailBillingReport" CommandName="BillingReport.xls"
                                OnClientClick="WaitingMessage();" />--%>
                            <asp:Label ID="rMessage" runat="server" />
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="Location" CssClass="tab-panel" HeaderText="Location" runat="server">
                        <ContentTemplate>
                            <label class="d-block">Please Note: These reports will only work with Receive Begin/End Date, Bin Number, Location and IMEI search parameters"</label>
                            <label>Location:</label>
                            <div class="row">
                            	<div class="col-md-6">
                                    <asp:DropDownList ID="drpLocationList" runat="server" ToolTip="Location Number to assign units in bin." />
                                </div>
                            </div>
                            <%--<asp:Button ID="btnLocationReport" runat="server" Text="Location History"
                                CommandArgument="GetMasterLocationReportList"
                                CommandName="MasterLocationList.xls" OnClientClick="WaitingMessage();" />
                            <asp:Button ID="btnLocationUnitReport" runat="server" Text="Units in Location"
                                CommandArgument="GetMasterLocationUnitReportList"
                                CommandName="MasterLocationUnitList.xls" OnClientClick="WaitingMessage();" />
                            <asp:Button ID="btnLocationUnitSummary" runat="server" Text="Unit Location Summary"
                                CommandArgument="GetMasterLocationUnitHistoryList_TemplateRawData_01_Summary"
                                CommandName="LocationUnitSummary.xls" OnClientClick="WaitingMessage();" />--%>
                            <asp:Button ID="btnItemsInLocation" runat="server" Text="Items in Location"
                                CommandArgument="GetMasterLocationUnitHistoryList_TemplateRawData_01_Summary_02"
                                CommandName="MasterLocationSummary.xls" OnClientClick="WaitingMessage();" />
                            <asp:Button ID="btnLocationHistoryReport" runat="server" Text="Location History"
                                CommandArgument="GetLocationUnitHistory" CommandName="MasterLocationUnitList.xls"
                                OnClientClick="WaitingMessage();" />
                            <asp:Button ID="btnUnitLocationSummary" runat="server" Text="Unit Location Summary"
                                CommandArgument="xxxxxx" CommandName="LocationUnitSummary.xls" OnClientClick="WaitingMessage();" />
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabPanel2" CssClass="tab-panel" HeaderText="User Reports" runat="server">
                        <ContentTemplate>
                            <div class="row">
                            	<div class="col-md-6">
                                    <asp:CheckBox ID="chkLogDate" runat="server" Text="Begin/End Date:" ToolTip="If unchecked, this will be excluded from the filter" />
                                    <div class="form-row">
                            	        <div class="col">
                                            <asp:TextBox ID="txtLogDateBegin" runat="server" />
                                            <asp:CalendarExtender ID="CalendarExtender11" runat="server" TargetControlID="txtLogDateBegin" />
                                        </div>
                            	        <div class="col">
                                            <asp:TextBox ID="txtLogDateEnd" runat="server" />
                                            <asp:CalendarExtender ID="CalendarExtender12" runat="server" TargetControlID="txtLogDateEnd" />
                                        </div>
                                    </div>
                            
                                    <label>User Name:</label>
                                    <asp:DropDownList ID="drpUserList" runat="server" />
                                </div>
                            </div>
                            
                            <asp:Button ID="btnUserFrequency2" runat="server" Text="KPI Summary" CommandArgument="GetMasterDetailUserFrequencyList_TemplateRawData_03"
                                CommandName="UserFrequencyList2.xls" OnClientClick="WaitingMessage();" />
                            <asp:Button ID="btnUserFrequency" runat="server" Text="KPI Detail" CommandArgument="GetMasterDetailUserFrequencyList_TemplateRawData_04"
                                CommandName="UserFrequencyList.xls" OnClientClick="WaitingMessage();" />
                            <asp:Button ID="btnErrorReporting" runat="server" Text="Error Reporting" CommandArgument="GetMasterDetailErrorReporting"
                                CommandName="ErrorReportingList.xls" OnClientClick="WaitingMessage();" />
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="MiscReports" CssClass="tab-panel" HeaderText="Misc Reports" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="btnBoxReport" runat="server" Text="Box Report" CommandArgument="Javascript"
                                CommandName="BoxReport.xls" />
                            <asp:Button ID="btnBinTag" runat="server" Text="Bin Tag" CommandArgument="Javascript"
                                CommandName="BinTag.xls" />
                            <asp:Button ID="btnMasterCarrier" runat="server" Text="Master Carrier/Manufacturer Match"
                                CommandArgument="Javascript" CommandName="MasterCarrier.xls" />
                            <asp:Button ID="btnMasterCarrierFreq" runat="server" Text="Master Carrier/Manufacturer Frequency"
                                CommandArgument="Javascript" CommandName="MasterCarrier.xls" />
                            <asp:Button ID="btnAttributeHistory" runat="server" Text="ESN Attribute History"
                                CommandArgument="Javascript" CommandName="AttributeHistory.xls" />
                            <asp:Button ID="btnClientQuestionFreq" runat="server" Text="Client Question Freq"
                                CommandArgument="Javascript" CommandName="ClientQuestionFreq.xls" Visible="False" />
                            <asp:Button ID="btnPartsInventoryTransaction" runat="server" Text="Parts Inventory Transactions"
                                CommandArgument="Javascript" CommandName="PartsInventoryTransaction.xls" Visible="True" />
                            <asp:Button ID="btnDiscrepancyReport" runat="server" Text="Discrepancy Report" CommandArgument="Javascript"
                                CommandName="Discrepancy.xls" Visible="True" />
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabPanel3" CssClass="tab-panel" HeaderText="Turn Around" runat="server">
                        <ContentTemplate>
                            <%--<asp:Button ID="btnxTAT" runat="server" Text="Tat Report"
                                CommandArgument="Javascript" CommandName="TatReport.xls" />--%>
                            <asp:Button ID="btnTAT" runat="server" Text="TAT Report" CommandArgument="GetMasterLocationReportList"
                                CommandName="TatReport.xls" OnClientClick="WaitingMessage();" />
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="MasterTables" CssClass="tab-panel" HeaderText="Master Tables" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="btnExportScanCodeList" runat="server" Text="ScanCode" CommandArgument="GetMasterScanCodeList"
                                CommandName="MasterScanCodeList" />
                            <asp:Button ID="btnExportQuestionList" runat="server" Text="Question" CommandArgument="GetMasterQuestionList"
                                CommandName="MasterQuestionList" />
                            <asp:Button ID="btnExportProcessList" runat="server" Text="Process" CommandArgument="GetMasterProcessList"
                                CommandName="MasterProcessList" />
                            <asp:Button ID="btnExportProjectDefinition" runat="server" Text="Project Definition"
                                CommandArgument="GetMasterProjectDefinition" CommandName="GetMasterProjectDefinition" />
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabPanel4" CssClass="tab-panel" HeaderText="IT" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="btnExtractSamsung" runat="server" Text="Samsung" CommandArgument="ExtractSamsung"
                                CommandName="SamsungExtractFile.txt" OnClientClick="WaitingMessage();" />
                            <asp:Button ID="btnRawData" runat="server" Text="Inventory Raw Data" CommandArgument="GetMasterDetailInventoryClientList_RawData"
                                CommandName="InventoryRawData.xls" OnClientClick="WaitingMessage();" />
                            <asp:Button ID="btnExportDetailInventoryClientListBatched" runat="server" Text="Detail Inventory with Client (Batched IMEI)"
                                CommandArgument="GetMasterDetailInventoryClientList_TemplateRawData_01_IMEIBatch"
                                CommandName="MasterInventoryClientListBatchIMEI" OnClientClick="WaitingMessage();" />
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="custom-file mb-2">
                                        <asp:FileUpload ID="FileUploadIMEIBatch" CssClass="custom-file-input" runat="server" />
                                        <label class="custom-file-label">Choose file</label>
                                    </div>
                                </div>
                            </div>
                            <asp:Button ID="btnUploadIMEIBatch" runat="server" Text="Upload IMEI Batch File" OnClick="btnUploadIMEIBatch_Click" />
                            <asp:Label ID="lblMsgIMEIBatch" runat="server" Visible="False" />
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabPanel5" CssClass="tab-panel" HeaderText="IT Bin Storage" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="btnBinStorage" runat="server" Text="Bin Day Storage" Visible="false"
                                CommandArgument="GetReport_BinStorage" CommandName="InventoryRawData.xls" OnClientClick="WaitingMessage();" />
                            <asp:Button ID="btnBinStorageGrand" runat="server" Text="Bin Storage Grand Total"
                                CommandArgument="GetReport_BinStorage_Freq" CommandName="BinStorageGrand.xls"
                                OnClientClick="WaitingMessage();" />
                            <asp:Button ID="btnTest" runat="server" Text="Bin Storage Grand Total Test" Visible="false"
                                CommandArgument="GetReport_BinStorage_Freq" CommandName="BinStorageGrand.xls"
                                OnClientClick="WaitingMessage();" />
                            <asp:Button ID="btnBinStorageSummary" runat="server" Text="Bin Storage Summary" CommandArgument="GetReport_BinStorage_FreqSummary"
                                CommandName="BinStorageSummary.xls" OnClientClick="WaitingMessage();" />
                            <asp:Button ID="btnBinStorageDetail" runat="server" Text="Bin Storage Detail" CommandArgument="GetReport_BinStorage_Freq"
                                CommandName="BinStorageDetail.xls" OnClientClick="WaitingMessage();" />
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabPanel6" CssClass="tab-panel" HeaderText="Other" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="btnProcessWaitTime" runat="server" Text="Project - Process Wait Time"
                                Visible="true" CommandArgument="GetProcessWaitTime_CurrentProcess_01" CommandName="ProcessWaitTime.xls"
                                OnClientClick="WaitingMessage();" />
                            <asp:Button ID="Button2" runat="server" Text="Bin Storage Grand Total" Visible="false"
                                CommandArgument="GetReport_BinStorage_Freq" CommandName="BinStorageGrand.xls"
                                OnClientClick="WaitingMessage();" />
                            <asp:Button ID="Button3" runat="server" Text="Bin Storage Grand Total Test" Visible="false"
                                CommandArgument="GetReport_BinStorage_Freq" CommandName="BinStorageGrand.xls"
                                OnClientClick="WaitingMessage();" />
                            <asp:Button ID="Button4" runat="server" Text="Bin Storage Summary" Visible="false"
                                CommandArgument="GetReport_BinStorage_FreqSummary" CommandName="BinStorageSummary.xls"
                                OnClientClick="WaitingMessage();" />
                            <asp:Button ID="Button5" runat="server" Text="Bin Storage Detail" Visible="false"
                                CommandArgument="GetReport_BinStorage_Freq" CommandName="BinStorageDetail.xls"
                                OnClientClick="WaitingMessage();" />
                        </ContentTemplate>
                    </asp:TabPanel>
                </asp:TabContainer>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabPanel8" CssClass="tab-panel" HeaderText="B" runat="server">
            <ContentTemplate>
                <asp:TabContainer ID="TabContainer5" CssClass="tab-container" runat="server">
                    <asp:TabPanel ID="TabPanel13" CssClass="tab-panel" HeaderText="Devices" runat="server" Visible="True">
                        <ContentTemplate>
                            <div class="row">
                            	<div class="col-md-6">
                                    <label>Supplier:</label>
                                    <asp:TextBox runat="server" ID="txtSupplier25" ToolTip="If you want records for one Client Location Supplier, enter it here." />
                            
                                    <label>RMA Number:</label>
                                    <asp:TextBox runat="server" ID="txtRMANumber25" ToolTip="If you want records for one RMA number, enter it here." />
                                    <asp:Button ID="btnRMASummary" CssClass="d-block" runat="server" Text="RMA Summary" />
                            
                                    <label>Project Tag:</label>
                                    <asp:TextBox runat="server" ID="txtProjectTag25" ToolTip="If you want records for one Project Tag, enter it here." />
                            
                                    <label>PO Number:</label>
                                    <asp:TextBox runat="server" ID="txtPONumber25" ToolTip="If you want records for one Purchase Order Number, enter it here." />
                            
                                    <label>SO Number:</label>
                                    <asp:TextBox runat="server" ID="txtSONumber25" ToolTip="If you want records for one Sales Order Number, enter it here." />
                            
                                    <asp:CheckBox ID="chkBeginDate25" runat="server" Text="Received Begin/End Date:"
                                        ToolTip="If unchecked, this will be excluded from the filter" />
                                    <div class="form-row">
                                        <div class="col">
                                            <asp:TextBox ID="txtBeginDate25" runat="server" />
                                            <asp:CalendarExtender ID="CalendarExtender15" runat="server" TargetControlID="txtBeginDate25" />
                                        </div>
                                        <div class="col">
                                            <asp:TextBox ID="txtEndDate25" runat="server" />
                                            <asp:CalendarExtender ID="CalendarExtender16" runat="server" TargetControlID="txtEndDate25" />
                                        </div>
                                    </div>
                                    
                                    <asp:CheckBox ID="chkBeginShipped25" runat="server" Text="Shipped Begin/End Date:"
                                        ToolTip="If unchecked, this will be excluded from the filter" />
                                    <div class="form-row">
                                        <div class="col">
                                            <asp:TextBox ID="txtBeginShipped25" runat="server" />
                                            <asp:CalendarExtender ID="CalendarExtender21" runat="server" TargetControlID="txtBeginShipped25" />
                                        </div>
                                        <div class="col">
                                            <asp:TextBox ID="txtEndShipped25" runat="server" />
                                            <asp:CalendarExtender ID="CalendarExtender22" runat="server" TargetControlID="txtEndShipped25" />
                                        </div>
                                    </div>
                            
                                    <asp:CheckBox ID="chkShowGraveyard25" runat="server" Text="Show Graveyard" ToolTip="Include Grave Yard Records in Report." />
                                    <asp:CheckBox ID="chkCSV25" runat="server" Text="CSV" ToolTip="Export as CSV File" />
                                </div>
                            </div>
                            
                            <asp:Label ID="lblMessage25" runat="server" />
                            
                            <asp:Button ID="btnDetailInventory25" runat="server" Text="Detail Inventory" CommandArgument="GetMasterDetailInventoryList"
                                CommandName="MasterInventoryList" OnClientClick="WaitingMessage();" />
                            <%--<asp:Button ID="Button6" runat="server" Text="Detail Inventory with Client" CommandArgument="GetMasterDetailInventoryClientList"
                                CommandName="MasterInventoryClientList" OnClientClick="WaitingMessage();" />--%>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabPanel20" CssClass="tab-panel" HeaderText="Device Bin" runat="server" Visible="True">
                        <ContentTemplate>
                            <label>Bin Number or XBINX Key:</label>
                            <asp:TextBox ID="txtBinNumberx" runat="server" />
                            <asp:Button ID="btnReportByBin" runat="server" Text="Device Bin" />
                            <asp:Button ID="btnReportXBINXKey" runat="server" Text="XBINX Bin Key" />
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabPanel16" CssClass="tab-panel" HeaderText="IFSLocation Search" runat="server">
                        <ContentTemplate>
                            <label>Search Location:</label>
                            <div class="row">
                            	<div class="col-lg-8">
                                    <div class="form-row">
                            	        <div class="col-sm">
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <div class="input-group-text">
                                                        <asp:CheckBox ID="chktxtSEG1" runat="server" Checked="True" />
                                                    </div>
                                                </div>
                                                <asp:TextBox ID="txtSEG1" runat="server" MaxLength="3" TabIndex="1" />
                                            </div>
                                        </div>
                                        <div class="col-sm-auto">-</div>
                            	        <div class="col-sm">
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <div class="input-group-text">
                                                        <asp:CheckBox ID="chktxtSEG2" runat="server" Checked="True" />                                        
                                                    </div>
                                                </div>
                                                <asp:TextBox ID="txtSEG2" runat="server" MaxLength="3" TabIndex="2" />
                                            </div>
                                        </div>
                                        <div class="col-sm-auto">-</div>
                            	        <div class="col-sm">
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <div class="input-group-text">
                                                        <asp:CheckBox ID="chktxtSEG3" runat="server" Checked="True" />                                        
                                                    </div>
                                                </div>
                                                <asp:TextBox ID="txtSEG3" runat="server" MaxLength="3" TabIndex="2" />
                                            </div>
                                        </div>
                                        <div class="col-sm-auto">-</div>
                            	        <div class="col-sm">
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <div class="input-group-text">
                                                        <asp:CheckBox ID="chktxtSEG4" runat="server" Checked="True" />                                        
                                                    </div>
                                                </div>
                                                <asp:TextBox ID="txtSEG4" runat="server" MaxLength="3" TabIndex="2" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            
                            <asp:Button ID="btnRefresh" runat="server" Text="Refresh" TabIndex="4" />
                                  
                            <asp:Label ID="Location25TabMessage" runat="server" />

                            <asp:Panel ID="pnlMasterList" runat="server">
                                <asp:Label ID="lblRecordTitle" runat="server" Text="Location:" />
                                <asp:Panel ID="pnlMainGrid" runat="server" ScrollBars="Auto">
                                    <asp:GridView ID="MainGrid" CssClass="table" runat="server" AutoGenerateSelectButton="True"
                                        DataKeyNames="MasterIFSLocationID" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundField DataField="MasterIFSLocationID" HeaderText="ID" ReadOnly="True" Visible="false" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="imgFilterOnLocation" runat="server" ToolTip="Move Location Segments into search.">
                                                        <span class="oi oi-plus"></span>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="imgDownloadDevices" runat="server" ToolTip="Download Devices">
                                                        <span class="oi oi-data-transfer-download"></span>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="imgDownloadParts" runat="server" ToolTip="Download Parts">
                                                        <span class="oi oi-data-transfer-download"></span>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="IsWip" HeaderText="WIP" />
                                            <asp:BoundField DataField="IFSLocation" HeaderText="Location" />
                                            <asp:BoundField DataField="Description" HeaderText="Description" />
                                            <asp:BoundField DataField="DeviceRollup" HeaderText="Device Rollup" />
                                            <asp:BoundField DataField="PartRollup" HeaderText="Part Rollup" />
                                            <asp:BoundField DataField="PickLevel" HeaderText="Pick Level" />
                                            <asp:BoundField DataField="IsFrozen" HeaderText="Frozen" />
                                            <asp:BoundField DataField="IFSLocationALT" HeaderText="Alt Location" />
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabPanel15" CssClass="tab-panel" HeaderText="Device Location Report" runat="server" Visible="True">
                        <ContentTemplate>
                            <label class="d-block">This report will return all the devices (000) found within the locations and or
                            Conditions given below.</label>
                            <asp:RadioButtonList ID="rdbSite" CssClass="radiolist-inline" runat="server" RepeatLayout="UnorderedList">
                                <asp:ListItem Text="C1NA" Value="C1NA" Selected="True" />
                                <asp:ListItem Text="C1CON" Value="C1CON" />
                            </asp:RadioButtonList>

                            <div class="row">
                            	<div class="col-md">
                                    <label>Paste Location list below:</label>
                                    <asp:RadioButtonList ID="PasteDeliminator" CssClass="radiolist-inline" runat="server" RepeatLayout="UnorderedList">
                                        <asp:ListItem Text="Excel" Value="Excel" Selected="True" />
                                        <asp:ListItem Text="Comma" Value="Comma" />
                                        <asp:ListItem Text="Space" Value="Space" />
                                    </asp:RadioButtonList>
                                    <asp:TextBox ID="txtPasteParse" runat="server" TextMode="MultiLine" />
                                </div>
                                <div class="col-md">
                                    <label>Paste Condition (Abbr) list below:</label>
                                    <asp:RadioButtonList ID="PasteDeliminatorCondition" CssClass="radiolist-inline" runat="server" RepeatLayout="UnorderedList">
                                        <asp:ListItem Text="Excel" Value="Excel" Selected="True" />
                                        <asp:ListItem Text="Comma" Value="Comma" />
                                        <asp:ListItem Text="Space" Value="Space" />
                                    </asp:RadioButtonList>
                                    <asp:TextBox ID="txtPasteParseCondition" runat="server" TextMode="MultiLine" />
                                </div>
                            </div>
                            
                            <asp:Button ID="btnPasteParse" runat="server" Text="Device Location/Condition Report"
                                ToolTip="Print SFC data for Devices sitting in supplied locations/Conditions." />
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabPanel14" CssClass="tab-panel" HeaderText="Order Entry" runat="server" Visible="True">
                        <ContentTemplate>
                            <label>Order Entry Number:</label>
                            <div class="row">
                            	<div class="col-md-6">
                                    <asp:TextBox ID="txtOrderEntryNo" runat="server" />
                                </div>
                            </div>
                            <asp:Button ID="btnOrderEntryData" runat="server" Text="Order Entry Data" />
                            <asp:Button ID="btnOrderEntryDetailData" runat="server" Text="Order Entry Detail Data" />
                            <asp:Button ID="btnOrderEntryDetailDataIFS" runat="server" Text="Order Entry Detail Data SENT TO IFS" />
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabPanel12" CssClass="tab-panel" HeaderText="Purchase Order" runat="server" Visible="True">
                        <ContentTemplate>
                            <label>Purchase Order Number:</label>
                            <div class="row">
                            	<div class="col-md-6">
                                    <asp:TextBox ID="txtPurchaseOrderNumber" runat="server" />
                                </div>
                            </div>
                            <asp:Button ID="btnPurchaseOrderData" runat="server" Text="Purchase Order Data" />
                            <asp:Button ID="btnPurchaseOrderDetailData" runat="server" Text="Purchase Order Detail Data" />
                            <asp:Button ID="btnPurchaseOrderDetailDataIFS" runat="server" Text="Purchase Order Detail Data SENT TO IFS" />
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabPanel18" CssClass="tab-panel" HeaderText="Batch Number Reports" runat="server" Visible="True">
                        <ContentTemplate>
                            <label>Batch Number:</label>
                            <div class="row">
                            	<div class="col-md-6">
                                    <asp:TextBox ID="txtIFSBatchNumber" runat="server" />
                                </div>
                            </div>
                            <asp:Button ID="btnIFSBatchDetailDataIFS" runat="server" Text="Batch Detail Data SENT TO IFS" />
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabPanel19" CssClass="tab-panel" HeaderText="Device/Transaction Reports" runat="server" Visible="false">
                        <ContentTemplate>
                            <label>Paste IMEI list below:</label>
                            <asp:RadioButtonList ID="PasteDeliminatorIMEIList" CssClass="radiolist-inline" runat="server" RepeatLayout="UnorderedList">
                                <asp:ListItem Text="Excel" Value="Excel" Selected="True" />
                                <asp:ListItem Text="Comma" Value="Comma" />
                                <asp:ListItem Text="Space" Value="Space" />
                            </asp:RadioButtonList>
                            
                            <asp:TextBox ID="txtPasteParseIMEIList" runat="server" TextMode="MultiLine" />
                             <asp:Button ID="btnPasteParseIMEIList" runat="server" Text="SFC transactions sent to IFS"
                                ToolTip="A Transactions sent from supplied IMEI numbers." />
                        </ContentTemplate>
                    </asp:TabPanel>
                </asp:TabContainer>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabPanel9" CssClass="tab-panel" HeaderText="C" runat="server">
            <ContentTemplate>
                <asp:TabContainer ID="TabContainer4" CssClass="tab-container" runat="server">
                    <asp:TabPanel ID="TabPanel11" CssClass="tab-panel" HeaderText="Summary" runat="server">
                        <ContentTemplate>
                            <asp:CheckBoxList ID="chkIFSParameters" CssClass="checklist-inline" runat="server" RepeatLayout="UnorderedList">
                                <asp:ListItem Selected="True" Text="Site" Value="S" />
                                <asp:ListItem Selected="True" Text="Project" Value="P" />
                                <asp:ListItem Selected="True" Text="Location" Value="L" />
                                <asp:ListItem Selected="True" Text="SKU" Value="K" />
                                <asp:ListItem Selected="True" Text="Condition" Value="C" />
                                <asp:ListItem Selected="False" Text="Grade" Value="G" />
                            </asp:CheckBoxList>

                            <label>SKU only (device summary only):</label>
                            <asp:RadioButtonList ID="PasteDeliminatorSkuOnly" CssClass="radiolist-inline" runat="server" RepeatLayout="UnorderedList">
                                <asp:ListItem Text="Excel" Value="Excel" Selected="True" />
                                <asp:ListItem Text="Comma" Value="Comma" />
                                <asp:ListItem Text="Space" Value="Space" />
                            </asp:RadioButtonList>
                            
                            <div class="row">
                            	<div class="col-md-6">
                                    <asp:TextBox ID="txtDeviceSummarySkuOnly" runat="server" TextMode="MultiLine" />
                                </div>
                            </div>
                            
                            <asp:CheckBox ID="chkShowRollupLocations" CssClass="d-block" runat="server" ToolTip="Show Rollup Locations" Text="Show Rollup Locations" />

                            <asp:Button ID="btnDeviceSummaryIFS" runat="server" Text="Device Summary" />
                            <asp:Button ID="btnDeviceErrorReport" runat="server" Text="Device Error Report" />
                            <asp:Button ID="btnPartSummaryIFS" runat="server" Text="Part Summary" />

                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabSKUAnalyze" CssClass="tab-panel" HeaderText="SKU Analysis" runat="server" Visible="True">
                        <ContentTemplate>
                            <label>SKU to analyze:</label>
                            <div class="row">
                            	<div class="col-md-6">
                                    <asp:TextBox ID="txtDeviceSKUAnalyze" runat="server" />
                                </div>
                            </div>
                            <asp:Button ID="btnDeviceSKUAnalyzeeDetail" runat="server" Text="Device Detail" />
                            <asp:Button ID="btnDeviceSKUAnalyze" runat="server" Text="Device Summary" />
                            <asp:Button ID="btnPartSKUAnalyzeeDetail" runat="server" Text="Part Detail" Enabled="False" Visible="false" />
                            <asp:Button ID="btnPartSKUAnalyze" runat="server" Text="Part Summary" />
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabLocationAnalyze" CssClass="tab-panel" HeaderText="Location Analysis" runat="server" Visible="True">
                        <ContentTemplate>
                            <label>Location to Analyze</label>
                            <div class="row">
                            	<div class="col-md-6">
                                    <asp:TextBox ID="txtDeviceLocAnalyze" runat="server" />
                                </div>
                            </div>
                            <asp:Button ID="btnDeviceLocAnalyzeDetail" runat="server" Text="Device Detail" Enabled="True" />
                            <asp:Button ID="btnPartLocAnalyzeDetail" runat="server" Text="Part Detail" Enabled="False" Visible="false" />
                            <asp:Button ID="btnDeviceLocAnalyze" runat="server" Text="Device Summary" />
                            <asp:Button ID="btnPartLocAnalyze" runat="server" Text="Part Summary" />
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabPanel21" CssClass="tab-panel" HeaderText="Batch Detail" runat="server" Visible="True">
                        <ContentTemplate>
                            <label>Batches to report (Comma seperated):</label>
                            <div class="row">
                            	<div class="col-md-6">
                                    <asp:TextBox ID="txtIFSBatchList" runat="server" />
                                </div>
                            </div>
                            <asp:Button ID="btnGetIFS_GatherLogSummary" runat="server" Text="Batch Summary" />
                        </ContentTemplate>
                    </asp:TabPanel>
                </asp:TabContainer>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabPanel10" CssClass="tab-panel" HeaderText="Pricing" runat="server"
            Visible="true">
            <ContentTemplate>
                <asp:CheckBox ID="CheckBox3" runat="server" Text="xxx:" ToolTip="xxx" /><br />
                As of:
                <asp:TextBox ID="txtPricingAsOfDate" runat="server" />
                <asp:CalendarExtender ID="CalendarExtender19" runat="server" TargetControlID="txtPricingAsOfDate" /><br />
                <asp:Button ID="btnDownLoadPricing" runat="server" Text="Download UK Pricing template" />
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabTemplate" CssClass="tab-panel" HeaderText="Templates" runat="server" Visible="true">
            <ContentTemplate>
                <asp:TabContainer ID="TabContempDetail" CssClass="tab-container" runat="server">
                    <asp:TabPanel ID="TabTempDetail" CssClass="tab-panel" HeaderText="Detail" runat="server">
                        <ContentTemplate>
                            <div class="row">
                            	<div class="col-md-6">
                                    <div class="custom-file mb-2">
                                        <asp:FileUpload ID="FileUploadDetail" CssClass="custom-file-input" runat="server" />
                                        <label class="custom-file-label">Choose file</label>
                                    </div>
                                </div>
                            </div>
                            
                            <asp:Button ID="btnUploadDetail" runat="server" Text="Upload new template" OnClick="btnUploadDetail_Click" />
                            <asp:Button ID="btnRefreshDetailList" runat="server" Text="Refresh" OnClick="btnRefreshDetail_Click" />
                            <asp:Label ID="lblMsgDetail" CssClass="d-block" runat="server" Visible="False" />

                            <asp:GridView ID="grdTempDetail" CssClass="table table-nonfluid" runat="server" DataKeyField="Name" AutoGenerateColumns="False" ShowHeader="False">
                                <Columns>
                                    <asp:TemplateField HeaderText="P">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="imgPrint" runat="server" ToolTip="Print" Visible="false">
                                                <span class="oi oi-print"></span>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="True" />
                                    <asp:TemplateField HeaderText="U">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="imgDownLoad" runat="server" ToolTip="Download the Template File">
                                                <span class="oi oi-data-transfer-download"></span>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="imgDelete" runat="server" ToolTip="Delete">
                                                <span class="oi oi-trash"></span>
                                            </asp:LinkButton>
                                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="imgDelete"
                                                ConfirmText="Are you sure you want to delete this file?">
                                            </asp:ConfirmButtonExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabPanel1" CssClass="tab-panel" HeaderText="Detail With Client" runat="server" Visible="false">
                        <ContentTemplate>
                            <div class="row">
                            	<div class="col-md-6">
                                    <div class="custom-file mb-2">
                                        <asp:FileUpload ID="FileUploadDetailClient" CssClass="custom-file-input" runat="server" />
                                        <label class="custom-file-label">Choose file</label>
                                    </div>
                                </div>
                            </div>
                            
                            <asp:Button ID="btnUploadDetailClient" runat="server" Text="Upload new template" OnClick="btnUploadDetailClient_Click" />
                            <asp:Label ID="lblMsgDetailClient" runat="server" Visible="False" />

                            <asp:GridView ID="grdTempDetailClient" CssClass="table table-nonfluid" runat="server" DataKeyField="Name" AutoGenerateColumns="False"
                                ShowHeader="False">
                                <Columns>
                                    <asp:TemplateField HeaderText="P">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="imgPrint" runat="server" ToolTip="Print" Visible="false">
                                                <span class="oi oi-print"></span>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="True" />
                                    <asp:TemplateField HeaderText="U">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="imgDownLoad" runat="server" ToolTip="Download the Template File">
                                                <span class="oi oi-data-transfer-download"></span>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="imgDelete" runat="server" ToolTip="Delete">
                                                <span class="oi oi-trash"></span>
                                            </asp:LinkButton>
                                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="imgDelete"
                                                ConfirmText="Are you sure you want to delete this file?">
                                            </asp:ConfirmButtonExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabBilling" CssClass="tab-panel" HeaderText="Billing Points" runat="server" Visible="false">
                        <ContentTemplate>
                            <div class="row">
                            	<div class="col-md-6">
                                    <div class="custom-file mb-2">
                                        <asp:FileUpload ID="FileUploadBillingPoint" CssClass="custom-file-input" runat="server" />
                                        <label class="custom-file-label">Choose file</label>
                                    </div

                                    <asp:Button ID="btnUploadBillingPoint" runat="server" Text="Upload new template" OnClick="btnUploadBillingPoint_Click" />
                                    <asp:Label ID="lblMsgBillingPoint" CssClass="d-block" runat="server" Visible="False" />

                                    <asp:CheckBox ID="chkBillingpoint_Template" runat="server" Text="Billing Point Create Date:"
                                        ToolTip="If unchecked, this will be excluded from the filter" />
                                    <div class="form-row">
                                        <div class="col">
                                            <asp:TextBox ID="txtBPStartDate_Template" runat="server" />
                                            <asp:CalendarExtender ID="CalendarExtender9" runat="server" TargetControlID="txtBPStartDate_Template" />
                                        </div>
                                        <div class="col">
                                            <asp:TextBox ID="txtBPEndDate_Template" runat="server" />
                                            <asp:CalendarExtender ID="CalendarExtender10" runat="server" TargetControlID="txtBPEndDate_Template" />
                                        </div>
                                    </div>
                              
                                    <asp:CheckBox ID="chkShowRecorded_Template" Text="Include completed transactions" runat="server" />
                                </div>
                            </div>
                            
                            <asp:GridView ID="grdTempBillingPoint" CssClass="table table-nonfluid" runat="server" DataKeyField="Name" AutoGenerateColumns="False" ShowHeader="False">
                                <Columns>
                                    <asp:TemplateField HeaderText="P">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="imgPrint" runat="server" ToolTip="Print" Visible="false">
                                                <span class="oi oi-print"></span>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="True" />
                                    <asp:TemplateField HeaderText="U">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="imgDownLoad" runat="server" ToolTip="Download the Template File">
                                                <span class="oi oi-data-transfer-download"></span>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="imgDelete" runat="server" ToolTip="Delete">
                                                <span class="oi oi-trash"></span>
                                            </asp:LinkButton>
                                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="imgDelete"
                                                ConfirmText="Are you sure you want to delete this file?">
                                            </asp:ConfirmButtonExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabTempDetailDate" CssClass="tab-panel" HeaderText="DetailDate" runat="server" Visible="false">
                        <ContentTemplate>
                            <div class="row">
                            	<div class="col-md-6">
                                    <div class="custom-file mb-2">
                                        <asp:FileUpload ID="FileUploadDetailDate" CssClass="custom-file-input" runat="server" />
                                        <label class="custom-file-label">Choose file</label>
                                    </div>
                                </div>
                            </div>
                            
                            <asp:Button ID="btnUploadDetailDate" runat="server" Text="Upload new template" OnClick="btnUploadDetailDate_Click" />
                            <asp:Label ID="lblMsgDetailDate" runat="server" Visible="False" />

                            <asp:GridView ID="grdTempDetailDate" CssClass="table table-nonfluid" runat="server" DataKeyField="Name" AutoGenerateColumns="False"
                                ShowHeader="False">
                                <Columns>
                                    <asp:TemplateField HeaderText="P">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="imgPrint" runat="server" ToolTip="Print" Visible="false">
                                                <span class="oi oi-print"></span>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="imgDelete" runat="server" ToolTip="Delete">
                                                <span class="oi oi-trash"></span>
                                            </asp:LinkButton>
                                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="imgDelete"
                                                ConfirmText="Are you sure you want to delete this file?">
                                            </asp:ConfirmButtonExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="True" />
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabTempBulk" CssClass="tab-panel" HeaderText="Bulk" runat="server" Visible="false">
                        <ContentTemplate>
                            <div class="row">
                            	<div class="col-md-6">
                                    <div class="custom-file mb-2">
                                        <asp:FileUpload ID="FileUploadBulk" CssClass="custom-file-input" runat="server" />
                                        <label class="custom-file-label">Choose file</label>
                                    </div>
                                </div>
                            </div>

                            <asp:Button ID="btnUploadBulk" runat="server" Text="Upload template" OnClick="btnUploadBulk_Click" />
                            <asp:Label ID="lblMsgBulk" runat="server" Visible="False" />

                            <asp:GridView ID="grdTempBulk" CssClass="table table-nonfluid" runat="server" DataKeyField="Name" AutoGenerateColumns="False"
                                ShowHeader="False">
                                <Columns>
                                    <asp:TemplateField HeaderText="P">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="imgPrint" runat="server" ToolTip="Print" Visible="false">
                                                <span class="oi oi-print"></span>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="imgDelete" runat="server" ToolTip="Delete">
                                                <span class="oi oi-trash"></span>
                                            </asp:LinkButton>
                                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="imgDelete"
                                                ConfirmText="Are you sure you want to delete this file?">
                                            </asp:ConfirmButtonExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="True" />
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabTempBSP" CssClass="tab-panel" HeaderText="BSP" runat="server" Visible="false">
                        <ContentTemplate>
                            <div class="row">
                            	<div class="col-md-6">
                                    <div class="custom-file mb-2">
                                        <asp:FileUpload ID="FileUploadBSP" CssClass="custom-file-input" runat="server" />
                                        <label class="custom-file-label">Choose file</label>
                                    </div>
                                </div>
                            </div>

                            <asp:Button ID="btnUploadBSP" runat="server" Text="Upload template" OnClick="btnUploadBSP_Click" />
                            <asp:Label ID="lblMsgBSP" runat="server" Visible="False" />

                            <asp:GridView ID="grdTempBSP" CssClass="table table-nonfluid" runat="server" DataKeyField="Name" AutoGenerateColumns="False"
                                ShowHeader="False">
                                <Columns>
                                    <asp:TemplateField HeaderText="P">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="imgPrint" runat="server" ToolTip="Print" Visible="false">
                                                <span class="oi oi-print"></span>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="imgDelete" runat="server" ToolTip="Delete">
                                                <span class="oi oi-trash"></span>
                                            </asp:LinkButton>
                                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="imgDelete"
                                                ConfirmText="Are you sure you want to delete this file?">
                                            </asp:ConfirmButtonExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="True" />
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:TabPanel>
                </asp:TabContainer>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>

</asp:Content>

<asp:Content ContentPlaceHolderID="js" runat="server">

    <script type="text/javascript">
        // Setup Global Variables
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

        //------------------------------------------------------------------------------------

        function WaitingMessage() {
            //            var SearchResults = $get("<%= lblMessage.ClientID %>");
            //            SearchResults.innerHTML = "WORKING....";
        }


        //        function OpenBoxReport() {
        //            var xDataList = {};
        //            xDataList["RPT"] = "BOXRPT";
        //            xDataList["Client"] = $get('<%= txtClient.ClientID %>').value;
        //            xDataList["PROJECTTAG"] = $get('<%= txtProjectTag.ClientID %>').value;
        //            xDataList["BEGINDATE"] = $get('<%= txtBeginDate.ClientID %>').value;
        //            xDataList["ENDDATE"] = $get('<%= txtEndDate.ClientID %>').value;
        //            xDataList["BSHIPPED"] = $get('<%= txtBeginShipped.ClientID %>').value;
        //            xDataList["ESHIPPED"] = $get('<%= txtEndShipped.ClientID %>').value; 
        //            xDataList["BARSPERPAGE"] = "10";
        //            var IndexValue = $get('<%=drpProjectList.ClientID %>').selectedIndex;
        //            xDataList["PROJECT"] = $get('<%=drpProjectList.ClientID %>').options[IndexValue].text;
        //            //            xDataList["FIELDNAME"] = "Inbound Tracking";
        //            xDataList["FIELDNAME"] = "BoxNumber";
        //            //            var win = window.open("ViewDoc.aspx", "_blank", "status=no,toolbar=no,menubar=no,location=no,titlebar=no,width=600px,height=540px", true);
        //            var pstring = GetParameterStream(xDataList);

        //            var WindowToOpen = "RPT_BoxListPDF.aspx";
        //            //var WindowToOpen = "RPT_BoxList.aspx";
        //            if (pstring.length > 0) {
        //                WindowToOpen = WindowToOpen + "?" + pstring
        //            }
        //            //            var win = window.open(WindowToOpen, "_blank", "width=100,height=50,menubar", true);
        //            var win = window.open(WindowToOpen, "_blank", "menubar", true);
        //            // win.focus();
        //            return false;
        //        }

        function OpenBinTag() {
            var xDataList = {};
            xDataList["RPT"] = "BINTAG";
            xDataList["BinNumber"] = $get('<%= txtBinNumber.ClientID %>').value;
            var pstring = GetParameterStream(xDataList);
            var WindowToOpen = "BagTag.aspx";
            if (pstring.length > 0) {
                WindowToOpen = WindowToOpen + "?" + pstring
            }
            var win = window.open(WindowToOpen, "_blank", "menubar", true);
            // win.focus();
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

        function OpenInventoryRPT(RPT, cmdText, FileName, FieldList) {
            var xDataList = {};
            xDataList["RPT"] = RPT;
            xDataList["CMD"] = cmdText;
            xDataList["fName"] = FileName;
            xDataList["fList"] = FieldList;
            var pstring = GetParameterStream(xDataList);
            var WindowToOpen = "/Reports/RPT_EXCEL_Out.aspx";
            if (pstring.length > 0) {
                WindowToOpen = WindowToOpen + "?" + pstring
            }
            var win = window.open(WindowToOpen, "_blank", "menubar", true);
            return;
        }

        function PrintReport(Report, Location) {
            var xDataList = {};
            xDataList["RPT"] = Report;
            xDataList["KEY"] = Location;
            xDataList["USERNAME"] = '';
            var pstring = GetParameterStream(xDataList);
            var WindowToOpen = "/Reports/RPT_EXCEL_Out.aspx";
            if (pstring.length > 0) {
                WindowToOpen = WindowToOpen + "?" + pstring
            }
            var win = window.open(WindowToOpen, "_blank", "menubar", true);
            return;
        }

    </script>

</asp:Content>
