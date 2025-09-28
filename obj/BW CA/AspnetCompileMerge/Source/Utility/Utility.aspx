<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Utility.aspx.cs" Inherits="BW_WebApp.Utility.Utility" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="cMC" runat="server">
    <asp:TabContainer CssClass="tab-container" runat="server">


        <asp:TabPanel ID="tabIMEI" CssClass="tab-panel" HeaderText="IMEI XLS Upload" runat="server" Visible="True">
            <ContentTemplate>
                <strong class="d-block mb-3">
                    This Utility will Create new Device records if a 000 version does not already exist.
                </strong>
                <div class="row">
                	<div class="col-md">
                        <label>Batch #:</label>
                        <asp:TextBox ID="tbBatchNumber" runat="server" ToolTip="Batch number to assign upload." />
                        <label>Client Location Scankey:</label>
                        <asp:TextBox ID="CL_Skcankey" runat="server" />
                        <label>Project to Load Data to:</label>
                        <asp:DropDownList ID="drpProjectList_New" runat="server" ToolTip="Project" />
                        <%--<label>IFS Transaction to Generate:</label>
                        <asp:RadioButtonList ID="IFSTransactionType" runat="server">
                            <asp:ListItem Text="PO_Initiate" Value="PO_Initiate" Selected="True" />
                            <asp:ListItem Text="PO_Receipt" Value="PO_Receipt" Selected="True" />
                            <asp:ListItem Text="Inv_Receipt" Value="Inv_Receipt" />
                            <asp:ListItem Text="DeviceAdjustIn" Value="DeviceAdjustIn" />
                        </asp:RadioButtonList>--%>
                        <%--<div class="form-check">
                            <asp:CheckBox ID="chkCloneSeedData" runat="server" Visible="True" Enabled="False" Text="Clone Seed Data" />
                        </div>--%>
                        <div class="form-check">
                            <asp:CheckBox ID="chkRunThreaded_Newa" runat="server" Visible="True" Text="Run Threaded" />
                        </div>
                        <div class="form-check">
                            <asp:CheckBox ID="chkCloneSeedData" runat="server" Visible="False" Enabled="False" />
                        </div>
                        <div class="form-check">
                            <asp:CheckBox ID="chkForce15IMEI" runat="server" Text="Force IMEI to 15 Characters Long" />
                        </div>
                        <hr class="d-md-none">
                    </div>
                	<div class="col-md">
                        <label>Please Select Excel file to load:</label>
                        <div class="input-group">
                            <div class="custom-file">
                                <asp:FileUpload ID="IMEIUploadFile" CssClass="custom-file-input" runat="server" />
                                <label class="custom-file-label">Choose file</label>
                            </div>
                        </div>
                        <asp:Button ID="btnIMEIUpload" runat="server" Text="Upload" OnClientClick="return true;" OnClick="btnIMEIUpload_Click" />
                        <asp:Label ID="lblIMEIUploadMSG" CssClass="d-block" runat="server" Visible="False" />
                        
                    </div>
                </div>
                <hr>
                <div class="row">
                	<div class="col-auto">
                        <div class="form-check">
                            <asp:CheckBox ID="chkCSV" runat="server" ToolTip="Export as CSV File" Text="CSV" />
                        </div>
                        <asp:Button runat="server" Text="Download Report" OnClientClick="return true;"  OnClick="btnIMEIDownload_Click" />
                    </div>
                	<div class="col text-right">
                        <asp:Button ID="btnShowTimeLog" runat="server" Text="Log" OnClientClick="ShowLogReport();return false;" Visible="false" />
                        <asp:Button ID="btnClearLog" runat="server" Text="Clear Log" OnClientClick="ClearLogReport();return false;" Visible="false" />
                    </div>
                </div>
                <hr>
                <p class="text-muted">
                    Data required: IMEI, Valid Carrier / Manufacturer / Model / Colour Combo, Other Valid attributes as desired.<br>
                    NOTE: Column position is not important, but Column Header Names need to equal the Attribute Name.<br>
                    NOTE: Column Data must equal Abbr Values if the data is a dropdown or checkbox type. e.g. Make, Model, Colour etc.
                </p>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabOrderEntryXLSUpload" CssClass="tab-panel" HeaderText="Order Entry IMEI XLS Upload" runat="server" Visible="True">
            <ContentTemplate>
                <strong class="d-block mb-3">
                    This Utility will Allow users to bulk load IME records to an Open SO... Or Ship C1CON devices.
                </strong>
                <div class="row">
                	<div class="col-md">
                        <label>Order Entry Number to append IMEI List to:</label>
                        <asp:TextBox ID="txtOrderEntryNumber" runat="server" placeholder="Enter 'C1CON' to ship C1CON devices."/>
                        <label>To IFS Location:</label>
                        <asp:TextBox ID="txtIFSLocation" runat="server" MaxLength="25" />
                        <hr class="d-md-none">
                    </div>
                	<div class="col-md">
                        <label>Please Select Excel file to load:</label>
                        <div class="input-group">
                            <div class="custom-file">
                                <asp:FileUpload ID="IMEIOrderUploadFile" CssClass="custom-file-input" runat="server" />
                                <label class="custom-file-label">Choose file</label>
                            </div>
                        </div>
                        <asp:Button ID="Button1" runat="server" Text="Upload" OnClientClick="return true;" OnClick="btnIMEIOrderUpload_Click" />
                        <asp:Label ID="lblOrderMessage" CssClass="d-block" runat="server" Visible="False" />
                    </div>
                </div>
                <hr>
                <p class="text-muted">
                    C1NA - Data required: IMEI (A), Carton Number (B).<br>
                    C1CON - Data required: IMEI (A), (OPTIONAL): Carton Number (B), Courier_Out (C), Out_Bound_Waybill_S (D), ShipTo (E), PO_No (F), Vendor_RMA (G), Unit_Destination (H), Replacement_SKU (I), RQ4_Transfer_Out (J).
                </p>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabSalesOrderIMEIQuerry" CssClass="tab-panel" HeaderText="Sales Order IMEI Query" runat="server" Visible="True">
            <ContentTemplate>
                <strong class="d-block mb-3">
                    This Utility will look up the IMEI and return Sales Order Details.
                </strong>
                <div class="row col-md-6">
                    <label>Select Excel file to load:</label>
                    <div class="input-group">
                        <div class="custom-file">
                            <asp:FileUpload ID="IMEISalesOrderUploadQuerry" CssClass="custom-file-input" runat="server" />
                            <label class="custom-file-label">Choose file</label>
                        </div>
                    </div>
                </div>
                <asp:Button ID="IMEISalesOrderUploadQueryFile" runat="server" Text="Upload" OnClientClick="return true;" OnClick="btnIMEISalesOrderUploadQuerry_Click" />
                <asp:Label ID="lblIMEISalesOrderUploadQuerry" runat="server" Visible="False" />
                <hr>
                <p class="text-muted">Data required: IMEI (A).</p>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabPurchaseOrderRefinement" CssClass="tab-panel" HeaderText="Purchase Order Refinement" runat="server" Visible="True">
            <ContentTemplate>
                <strong class="d-block mb-3">
                    This Utility will allow you to adjust what Purchase Order IMEI devices were loaded to.<br>
                    This Excel Expects IMEI Records for already loaded IMEI data, but where the IMEI was originally 
                    received on the wrong PO. This does not talk with IFS.
                </strong>
                <div class="row">
                	<div class="col-md-6">
                        <label>Please Select Excel file to load:</label>
                        <div class="input-group">
                            <div class="custom-file">
                                <asp:FileUpload ID="IMEIPOSwitchUploadFile" CssClass="custom-file-input" runat="server" />
                                <label class="custom-file-label">Choose file</label>
                            </div>
                        </div>
                        <asp:Button ID="btnIMEIPOSwitch" runat="server" Text="Upload" OnClientClick="return true;" OnClick="btnIMEIPOSwitch_Click" />
                        <asp:Label ID="lblPOSwitchMessage" runat="server" Visible="False" />
                    </div>
                </div>
                <hr>
                <p class="text-muted">Data required: IMEI (A), From PO (B), To PO (C), To PO Line (D).</p>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabOther1" CssClass="tab-panel" HeaderText="Other" runat="server" Visible="true">
            <ContentTemplate>
                <asp:TabContainer ID="TabOther" CssClass="tab-container" runat="server">
                    <asp:TabPanel ID="tabshowUnitData" CssClass="tab-panel" HeaderText="Show Unit Data" runat="server">
                        <ContentTemplate>
                            <div class="row">
                	            <div class="col-md-6">
                                    <label>ESN:</label>
                                    <asp:TextBox ID="TxtESN" runat="server" Text="A0002" />
                                    <label>Version:</label>
                                    <asp:TextBox ID="txtVersion" runat="server" MaxLength="3" Text="003" />
                                    <asp:Button ID="Button3" runat="server" Text="Show Unit Data" OnClientClick="ShowUnitData();return false;" />
                                    <asp:GridView ID="GridView6" runat="server" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabClean" CssClass="tab-panel" HeaderText="Clean Bins" runat="server"
                        ToolTip="Clean out Bins for Units with Versions > 000">
                        <ContentTemplate>
                            <strong class="d-block mb-3">
                                This Utility will remove the bin number for any Device that is not 000.
                            </strong>
                            <asp:Button ID="btnCleanBinGT0" runat="server" Text="Go" />
                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnCleanBinGT0"
                                ConfirmText="Are you sure you want to clear all Bins for non 000 versioned units?" />
                            <asp:Label ID="lblCleanBinMessage" runat="server" />
                            <hr>
                            <p class="text-muted">Data required: No upload. This acts on all non 000 devices.</p>
                        </ContentTemplate>
                    </asp:TabPanel>
                </asp:TabContainer>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabMoveLocation" CssClass="tab-panel" HeaderText="Move Location" runat="server" Visible="True">
            <ContentTemplate>
                <strong class="d-block mb-3">
                    This Utility will Allow users to bulk load an IMEI Location Move
                </strong>
                <div class="row">
                	<div class="col-md-6">
                        <label>Please Select Excel file to load:</label>
                        <div class="input-group">
                            <div class="custom-file">
                                <asp:FileUpload ID="upFile_LocationMove" CssClass="custom-file-input" runat="server" />
                                <label class="custom-file-label">Choose file</label>
                            </div>
                        </div>
                        <asp:Button ID="btnLocationMove" runat="server" Text="Upload" OnClientClick="return true;" OnClick="btnIMEILocationMoveUpload_Click" />
                        <asp:Label ID="lblLocationMoveMessage" runat="server" Visible="False" />
                    </div>
                </div>
                <hr>
                <p class="text-muted">Data required: IMEI (A), IFS Location (B).</p>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabPrereceive" CssClass="tab-panel" HeaderText="IMEI PreReceive Inventory Upload" runat="server" Visible="false">
            <ContentTemplate>
                <strong class="d-block mb-3">
                    This Utility will Preload IMEI records.
                </strong>
                <div class="row">
                	<div class="col-md-6">
                        <div class="form-check">
                            <asp:CheckBox ID="chkForce15IMEI_Pre" runat="server" Text="Force IMEI to 15 Characters Long" />
                        </div>
                        <label>Please Select Excel file to load:</label>
                        <div class="input-group">
                            <div class="custom-file">
                                <asp:FileUpload ID="IMEIUploadFile_PRI" CssClass="custom-file-input" runat="server" />
                                <label class="custom-file-label">Choose file</label>
                            </div>
                        </div>
                        <asp:Button ID="btnIMEIUpload_PRI" runat="server" Text="Upload" OnClientClick="FillLabelText('Upload Started');return true;"
                            OnClick="btnIMEIUpload_PRI_Click" />
                        <asp:Label ID="lblIMEIUploadMSG_PRI" runat="server" Visible="False" />
                    </div>
                </div>
                <hr>
                <asp:Button ID="btnShowTimeLog_P" runat="server" Text="Log" OnClientClick="ShowLogReport();return false;" />
                <asp:Button ID="btnClearLog_P" runat="server" Text="Clear Log" OnClientClick="ClearLogReport();return false;" />
                <hr>
                <p class="text-muted">
                    Data required: IMEI, Valid Carrier / Manufacturer / Model / Colour Combo, Other Valid attributes as desired.<br>
                    NOTE: Column position is not important, but Column Header Names need to equal the Attribute Name.<br>
                    NOTE: Column Data must equal Abbr Values if the data is a dropdown or checkbox type. e.g. Make, Model, Colour etc.<br>
                </p>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabPanel3z" CssClass="tab-panel" HeaderText="PreReceive Previous Record Upload" runat="server" Visible="false">
            <ContentTemplate>
                <strong class="d-block mb-3">
                    This Utility will Preload Previous IMEI records.
                </strong>
                <div class="row">
                	<div class="col-md-6">
                        <div class="form-check">
                            <asp:CheckBox ID="chkForce15IMEI_Prez" runat="server" Text="Force IMEI to 15 Characters Long" />
                        </div>
                        <label>Please Select Excel file to load:</label>
                        <div class="input-group">
                            <div class="custom-file">
                                <asp:FileUpload ID="IMEIUploadFile_PRIz" CssClass="custom-file-input" runat="server" />
                                <label class="custom-file-label">Choose file</label>
                            </div>
                        </div>
                        <asp:Button ID="btnIMEIUpload_PRIz" runat="server" Text="Upload" OnClientClick="FillLabelText('Upload Started');return true;"
                            OnClick="btnIMEIUpload_PRIz_Click" />
                        <asp:Label ID="lblIMEIUploadMSG_PRIz" runat="server" Visible="False" />
                    </div>
                </div>
                <hr>
                <asp:Button ID="btnShowTimeLog_Pz" runat="server" Text="Log" OnClientClick="ShowLogReport();return false;" />
                <asp:Button ID="btnClearLog_Pz" runat="server" Text="Clear Log" OnClientClick="ClearLogReport();return false;" />
            </ContentTemplate>
        </asp:TabPanel>
        <%--<asp:TabPanel ID="TabPanel5" CssClass="tab-panel" HeaderText="IMEI Reset Seed Data" runat="server" Visible="False">
            <ContentTemplate>
                <strong class="d-block mb-3">
                    This Utility will Reset IMEI Seed Data.
                </strong>
                <div class="row">
                	<div class="col-md-6">
                        <div class="form-check">
                            <asp:CheckBox ID="chkForce15IMEI_ResetSeed" runat="server" Text="Force IMEI to 15 Characters Long" />
                        </div>
                        <label>Please Select Excel file to load:</label>
                         <div class="input-group">
                            <div class="custom-file">
                                <asp:FileUpload ID="IMEIUploadFileSeedData" CssClass="custom-file-input" runat="server" />
                                <label class="custom-file-label">Choose file</label>
                            </div>
                        </div>
                        <asp:Button ID="btnIMEIUploadSeedData" runat="server" Text="Upload" OnClick="IMEIUploadFileSeedData_Click" />
                        <asp:Label ID="lblIMEIUploadMSGSeedData" runat="server" Visible="False" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:TabPanel>--%>
        <asp:TabPanel ID="TabUploaddAdjustOut" CssClass="tab-panel" HeaderText="IMEI Adjust Out" runat="server" Visible="False">
            <ContentTemplate>
                <strong class="d-block mb-3">
                    This Utility will Adjust Out existing IMEI version zero records.
                </strong>
                <div class="row">
                	<div class="col-md-6">
                        <label>Please Select Excel file to load:</label>
                        <div class="input-group">
                            <div class="custom-file">
                                <asp:FileUpload ID="FileUploadAdjustOut" CssClass="custom-file-input" runat="server" />
                                <label class="custom-file-label">Choose file</label>
                            </div>
                        </div>
                        <asp:Button ID="btnUploadAdjustOutData" runat="server" Text="Upload" OnClientClick="return true;"
                            OnClick="btnUploadAdjustOutData_Click" />
                        <asp:Label ID="lblMessageAdjustOut" runat="server" Visible="False" />
                    </div>
                </div>
                <hr>
                <p class="text-muted">
                    Data required: IMEI (A), Adjust Out Code (B) Reason Desc (C) you want to upload.<br>
                    NOTE: Reason Code should look like this 00 or 41 etc.
                </p>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabPanel4" CssClass="tab-panel" HeaderText="Roll IMEI/ESN records" runat="server" Visible="False">
            <ContentTemplate>
                <strong class="d-block mb-3">
                    This will Take IMEI/ESN records found within the uploaded XLS and Roll the versions.<br>
                    Please note that All versions for the IMEI/ESN number will be rolled. eg. 003 becomes 004, 002 becomes 003,
                    001 becomes 002 and 000 becomes 001 etc.
                </strong>
                <div class="row">
                	<div class="col-md-6">
                        <div class="form-check">
                            <asp:CheckBox ID="chnForce15IMEIRoll" runat="server" Text="Force IMEI to 15 Characters Long" />
                        </div>
                        <label>Please Select Excel file to load:</label>
                        <div class="input-group">
                            <div class="custom-file">
                                <asp:FileUpload ID="IMEIRollUploadFile" CssClass="custom-file-input" runat="server" />
                                <label class="custom-file-label">Choose file</label>
                            </div>
                        </div>
                        <asp:Button ID="Button2" runat="server" Text="Upload" OnClientClick="return true;" OnClick="btnIMEIRollUpload_Click" />
                        <asp:Label ID="lblRollMessage" runat="server" Visible="False" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabPanel1" CssClass="tab-panel" HeaderText="Discrepancy" runat="server" Visible="False">
            <ContentTemplate>
                <strong class="d-block mb-3">
                    Load discrepancy source historical data.
                </strong>
                <div class="row">
                	<div class="col-md-6">
                        <label>Please Select Excel file to load:</label>
                        <div class="input-group">
                            <div class="custom-file">
                                <asp:FileUpload ID="DiscrepancyFileUpload" CssClass="custom-file-input" runat="server" />
                                <label class="custom-file-label">Choose file</label>
                            </div>
                        </div>
                        <asp:Button ID="btnUploadDiscrepancy" runat="server" Text="Upload" OnClientClick="return true;" />
                        <asp:Button ID="btnVerifyDiscrepancy" runat="server" Text="Verify" OnClientClick="return true;" />
                        <%--<asp:Button ID="btnResetDiscrepancy" runat="server" Text="Reset Discrepancy"
                            ToolTip="WARNING - This will remove all discrepancy records in the database. This can not be reversed." />
                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" TargetControlID="btnResetDiscrepancy"
                            ConfirmText="Are you sure you want to Delete ALL DISCREPANCY RECORDS?" />--%>
                        <asp:Label ID="lblDiscrepancy" runat="server" Visible="False" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabSetVersion000" CssClass="tab-panel" HeaderText="Change IMEI Version back to 000" runat="server" Visible="False">
            <ContentTemplate>
                <strong class="d-block mb-3">
                    This Utility will change the version of the given IMEI/Version to 000.<br>
                    If there is already a 000 versioned device, the given IMEI/Version is skipped.
                </strong>
                <div class="row">
                	<div class="col-md-6">
                        <label>Please Select Excel file to load:</label>
                        <div class="input-group">
                            <div class="custom-file">
                                <asp:FileUpload ID="IMEIVERSION000" CssClass="custom-file-input" runat="server"
                                    ToolTip="Column A=ESN, B=Current Version, C=Result Status" />
                                <label class="custom-file-label">Choose file</label>
                            </div>
                        </div>
                        <asp:Button ID="btnIMEIVersionUpload" runat="server" Text="Upload" OnClick="btnIMEIVersionUpload_Click" />
                        <%--<asp:Button ID="Button3" runat="server" Text="Upload" OnClientClick="FillLabelText('Upload Started');return true;"
                            OnClick="btnIMEIUpload_Click" />--%>
                        <asp:Label ID="lblIMEIVersionUploadMSG" runat="server" Visible="False" />
                    </div>
                </div>
                <%--<hr>
                <asp:Button runat="server" Text="Log" OnClientClick="ShowLogReport();return false;" />
                <asp:Button runat="server" Text="Clear Log" OnClientClick="ClearLogReport();return false;" />--%>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabUploadAttributes" CssClass="tab-panel" HeaderText="IMEI Attribute Upload" runat="server" Visible="False">
            <ContentTemplate>
                <strong class="d-block mb-3">
                    This Utility will load Attribute values to existing IMEI version zero records.
                </strong>
                <div class="row">
                	<div class="col-md-6">
                        <div class="form-check">
                            <asp:CheckBox ID="cbUploadToLastVersion" runat="server" Text="Upload to last version processed"
                                ToolTip="upload to last version processed. e.g. 001, 002, 003 versions. Load to Version 001." />
                        </div>
                        <label>Please Select Excel file to load:</label>
                        <div class="input-group">
                            <div class="custom-file">
                                <asp:FileUpload ID="FileUploadIMEIAttribute" CssClass="custom-file-input" runat="server" />
                                <label class="custom-file-label">Choose file</label>
                            </div>
                        </div>
                        <asp:Button ID="btnUploadIMEIAttribute" runat="server" Text="Upload" OnClientClick="return RestrictUploadIMEIAttribute();"
                            OnClick="btnUploadIMEIAttribute_Click" />
                        <asp:Label ID="lblMessageIMEIAttribute" runat="server" Visible="False" />
                    </div>
                </div>
                <hr>
                <p class="text-muted">
                    Data required: IMEI (A), Attribute (B) you want to upload, Column C, D, E etc can each carry Attributes.<br>
                    NOTE: Column Header Names have to equal the Attribute Name it represents.
                    The data within the column should be the ABBR Value. If this is not possible, it needs to be the Value.
                </p>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabUploadSingleAttributes" CssClass="tab-panel" HeaderText="IMEI Single Attribute Upload" runat="server" Visible="False">
            <ContentTemplate>
                <strong class="d-block mb-3">
                    This Utility will load ONE Attribute value to existing IMEI version zero records.
                </strong>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:AjaxFileUpload ID="AjaxFileUpload1" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>




<%--                <div class="row">
                	<div class="col-md-6">
                        <label>Please Select Excel file to load:</label>
                         <div class="input-group">
                            <div class="custom-file">
                                <asp:FileUpload ID="FileUploadSingleIMEIAttribute" CssClass="custom-file-input" runat="server" />
                                <label class="custom-file-label">Choose file</label>
                            </div>
                        </div>
                        <asp:Button ID="btnUploadSingleIMEIAttribute" runat="server" Text="Upload" OnClientClick="return true;"
                            OnClick="btnUploadSingleIMEIAttribute_Click" />
                        <asp:Label ID="lblMessageSingleIMEIAttribute" runat="server" Visible="False" />
                    </div>
                </div>--%>
                <hr>
                <p class="text-muted">
                    Data required: IMEI (A), Attribute (B) you want to upload.<br>
                    NOTE: Column Header Name (B) has to equal the Attribute Name.
                </p>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabFullAttributeUpload" CssClass="tab-panel" HeaderText="Full Attribute Upload" runat="server" Visible="False">
            <ContentTemplate>
                <strong class="d-block mb-3">
                    This Utility will load Attribute values to existing Questions within the application Master.<br>
                    NOTE: This upload requires the Question Name field inside the excel file. This Utility also alows you to make an answer inactive.
                </strong>
                <div class="row">
                    <div class="col-md-6">
                        <label>Please Select Excel file containg job details:</label>
                        <div class="input-group">
                            <div class="custom-file">
                                <asp:FileUpload ID="FullAttributeUpload" CssClass="custom-file-input" runat="server"
                                    ToolTip="Column A=ESN, B=Current Version, C=Result Status" />
                                <label class="custom-file-label">Choose file</label>
                            </div>
                        </div>
                        <asp:Button ID="btnFullAttributeUpload" runat="server" Text="Upload" OnClick="btnFullAttributeUpload_Click" />
                        <asp:Label ID="MSGFullAttributeUpload" runat="server" Visible="False" />
                    </div>
                </div>
                <hr>
                <p class="text-muted">
                    Data required: Question Name (A), Delete (B), Scankey = leave blank to default (C), Answer (D), Abbreviation (E), Seq (F).<br>
                    NOTE: Anything in Column B will trigger a delete.
                </p>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="tabAttribute" CssClass="tab-panel" HeaderText="Attribute Upload" runat="server" Visible="False">
            <ContentTemplate>
                <strong class="d-block mb-3">
                    This Utility will load Attribute values to existing Questions within the Question Master.
                </strong>
                <div class="row">
                	<div class="col-md-6">
                        <label>Question Name to load data to:</label>
                        <asp:TextBox ID="txtQuestionName" runat="server" />
                    </div>
                    <div class="col-md-6">
                        <label>Please Select Excel file containg job details:</label>
                        <div class="input-group">
                            <div class="custom-file">
                                <asp:FileUpload ID="txtFilePath" CssClass="custom-file-input" runat="server" />
                                <label class="custom-file-label">Choose file</label>
                            </div>
                        </div>
                        <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
                        <asp:Label ID="lblMessage" runat="server" Visible="False" />
                    </div>
                </div>
                <hr>
                <p class="text-muted">
                    Data required: Scankey (A), Answer (B), Abbreviation (C), Seq (D).
                </p>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabUploadAttributeReplace" CssClass="tab-panel" HeaderText="Replace One Attribute with Another" runat="server" Visible="False">
            <ContentTemplate>
                <strong class="d-block mb-3">
                    This Utility will Replace one Attribute (source) with another (Target).<br>
                    All areas within the system, except the Master Carrier Manufactuerer Model Lookup table and the Master Model Memory Table, will be replaced.
                </strong>
                <div class="row">
                    <div class="col-md-6">
                        <label>Please Select Excel file to load:</label>
                        <div class="input-group">
                            <div class="custom-file">
                                <asp:FileUpload ID="AttributeReplaceFileUpload" CssClass="custom-file-input" runat="server"
                                    ToolTip="Column A=ESN, B=Current Version, C=Result Status" />
                                <label class="custom-file-label">Choose file</label>
                            </div>
                        </div>
                        <asp:Button ID="btnAttributeReplaceUpload" runat="server" Text="Upload" OnClientClick="return true;" OnClick="btnAttributeReplaceUpload_Click" />
                        <asp:Label ID="lblAttributeReplaceUploadMSG" runat="server" Visible="False" />
                    </div>
                </div>
                <hr>
                <p class="text-muted">
                    Data required: Column A is the Attribute name you wish to replace, Column B is the Source ABBR Value, Column C is the Target ABBR Value.<br />
                    NOTE: The SOURCE Attribute will be invalidated, taken out of the race.
                </p>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabUploadModelMemory" CssClass="tab-panel" HeaderText="Model-Memory Upload" runat="server" Visible="False">
            <ContentTemplate>
                <strong class="d-block mb-3">
                    This Utility will build the relationship between Model and Memory.<br>
                    Please note that this will not add new Model or Memory options - Just link existing options.
                </strong>
                <div class="row">
                    <div class="col-md-6">
                        <label>Please Select Excel file to load:</label>
                        <div class="input-group">
                            <div class="custom-file">
                                <asp:FileUpload ID="ModelMemoryFileUpload" CssClass="custom-file-input" runat="server"
                                    ToolTip="Column A=ESN, B=Current Version, C=Result Status" />
                                <label class="custom-file-label">Choose file</label>
                            </div>
                        </div>
                        <asp:Button ID="btnModelMemoryUpload" runat="server" Text="Upload" OnClientClick="return true;"  OnClick="btnModelMemoryUpload_Click" />
                        <asp:Label ID="lblModelMemoryUploadMSG" runat="server" Visible="False" />
                    </div>
                </div>
                <hr>
                <p class="text-muted">
                    Data required: Column A is the Model Abbr, Column B is a comma delimited list of Memory Abbr Values.<br>
                    NOTE: Any Model or Memory values that are not within the system now will NOT be added. They must be added first.
                </p>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
</asp:Content>

<asp:Content ContentPlaceHolderID="js" runat="server">
    <script type="text/javascript">

        function RestrictIMEIUpload() {
            //            alert("Inside Restrict IMEI upload");
            //            document.getElementById(target).disabled = false;
            //            if ($get("<%= btnIMEIUpload.ClientID %>").disabled == true) {alert('Please wait for run to complete before running a second time'); return false; }
            //            $get("<%= btnIMEIUpload.ClientID %>").disabled = true;
            return true;
        }

        function RestrictUploadIMEIAttribute() {
            //            alert("Inside Restrict btnUploadIMEIAttribute");
            //            document.getElementById(target).disabled = false;
            //            if ($get("<%= btnUploadIMEIAttribute.ClientID %>").disabled == false) { alert('Please wait for run to complete before running a second time'); return false; }
            //            $get("<%= btnUploadIMEIAttribute.ClientID %>").disabled = false;
            return true;
        }

        function FillLabelText(text) {
            // $get("<%= lblIMEIUploadMSG.ClientID %>").innerHTML = text;
        }

        function ClearLogReport() {
            var service = new WebServer_01();
            service.ClearLogFileUtility();
        }

        function ShowLogReport() {
            var xDataList = {};
            xDataList['RPT'] = 'LogUtility';
            var pstring = GetParameterStream(xDataList);
            var WindowToOpen = '/Reports/RPT_EXCEL_Out.aspx';
            if (pstring.length > 0) { WindowToOpen = WindowToOpen + '?' + pstring }
            var win = window.open(WindowToOpen, '_blank', 'menubar', true);
            return;
        }

        function ShowUnitData() {
            var xDataList = {};
            xDataList['RPT'] = 'UNITDATA';
            xDataList['ESN'] = $get("<%= TxtESN.ClientID %>").value;
            xDataList['VERSION'] = $get("<%= txtVersion.ClientID %>").value;
            var pstring = GetParameterStream(xDataList);
            var WindowToOpen = '/Reports/RPT_EXCEL_Out.aspx';
            if (pstring.length > 0) { WindowToOpen = WindowToOpen + '?' + pstring }
            var win = window.open(WindowToOpen, '_blank', 'menubar', true);
            return;
        }

    </script>
</asp:Content>

