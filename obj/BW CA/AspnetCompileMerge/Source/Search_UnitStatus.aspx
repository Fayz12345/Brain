<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Search_UnitStatus.aspx.cs" Inherits="BW_WebApp.Search_UnitStatus" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="cMC" runat="server">
    <h1>Unit Status:</h1>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
            	<div class="col-md-6">
                    <asp:Label ID="lblESN" runat="server" AssociatedControlID="txtESN">IMEI/ESN:</asp:Label>
                    <div class="input-group">
                        <asp:TextBox ID="txtESNSearch" runat="server" ToolTip='Enter IMEI/ESN here and hit the "Go" button.' />
                        <div class="input-group-append">
                            <asp:Button ID="btnSearch" runat="server" Text="Go" />
                        </div>
                    </div>
                </div>
            </div>
                
            <div runat="server" id="SearchData">
                <asp:HiddenField ID="hdnReceiveDetailID" runat="server" />
                <asp:HiddenField ID="hdnReceiveDetailAuthorizationLogID" runat="server" />

                <div class="row">
                	<div class="col-md">
                        <asp:Label ID="lblTitle" runat="server" Text="Unit Summary" />
                        <asp:Label ID="lblPath" runat="server" />

                        <label>Service Request #:</label>
                        <%--<asp:TextBox ID="txtClientReference" runat="server" Enabled="False" />--%>
                        <asp:TextBox ID="txtServiceRequestNumber" runat="server" Enabled="False" />
                        <%--<asp:TextBox ID="txtCustomerName" runat="server" Enabled="False" />--%>

                        <label>ESN/IMEI:</label>
                        <asp:TextBox ID="txtESN" runat="server" Enabled="False" />

                        <label>Original IMEI:</label>
                        <asp:TextBox ID="txtOriginalIMEI" runat="server" Enabled="False" />

                        <label>Warranty Type:</label>
                        <asp:TextBox ID="txtWarrantyType" runat="server" Enabled="False" />

                        <label>First Complaint:</label>
                        <asp:TextBox ID="txtFaultCode" runat="server" Enabled="False" />

                        <label>Second Complaint:</label>
                        <asp:TextBox ID="txtFaultCode2" runat="server" Enabled="False" />

                        <label>Date Submitted:</label>
                        <asp:TextBox ID="txtDateSubmitted" runat="server" Enabled="False" />

                        <label>Date Received at IMM:</label>
                        <asp:TextBox ID="txtGMPReceivedDate" runat="server" Enabled="False" />
                    
                        <label>Repair Date:</label>
                        <asp:TextBox ID="txtRepairDate" runat="server" Enabled="False" />
                    </div>
                    <div class="col-md">
                        <label>Unit Status:</label>
                        <asp:TextBox ID="txtCurrentProcess" runat="server" Enabled="False" />

                        <label><asp:Label ID="lblRepairFee" runat="server" Text="Repair Fee:" /></label>
                        <asp:TextBox ID="txtRepairFee" runat="server" Enabled="False" />
                    
                        <label><asp:Label ID="lblAssessment" runat="server" Text="Unit Assessment:" /></label>
                        <asp:TextBox ID="txtAssessment" runat="server" Enabled="False" TextMode="MultiLine" />

                        <label>Repair Notes:</label>
                        <asp:TextBox ID="txtRepairNotes" runat="server" Enabled="False" TextMode="MultiLine" />

                        <label>Ship Date:</label>
                        <asp:TextBox ID="txtGMPMSCShippedDate" runat="server" Enabled="False" />

                        <label>Outgoing Waybill:</label>
                        <asp:TextBox ID="txtOutBoundWayBill_S" runat="server" Enabled="False" />

                        <label>Courier - Out:</label>
                        <asp:TextBox ID="txtCourier" runat="server" Enabled="False" />

                        <span id="ErrorLine" runat="server">
                            <label><asp:Label ID="lblMiscError" runat="server" Text="System Error Message:" /></label>
                            <asp:TextBox ID="txtMiscError" runat="server" Enabled="False" TextMode="MultiLine" />
                        </span>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

