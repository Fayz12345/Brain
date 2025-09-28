<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TestParseDataStreamData.aspx.cs" Inherits="BW_WebApp.Utility.TestParseDataStreamData" %>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="cMC" runat="server">
    <h1>Data Stream Parse Utility</h1>
    <label>Data stream data (WARNING: if you "process" the data stream, you will generate an IMEI inside the database. Run this only in Sandbox!):</label>
    <asp:TextBox ID="txtDataStream" runat="server" Rows="5" TextMode="MultiLine" />
    <div class="row">
        <div class="col-md-6">


            <asp:Button ID="btnBack" runat="server" Text="Back" />
            <asp:TextBox ID="TestESN" runat="server">458510099995396</asp:TextBox>
            <asp:TextBox ID="TestBatch" runat="server">Jim96</asp:TextBox>
            <asp:Button ID="btnTestAPI" runat="server" Text="Test Device API" />
            <asp:Button ID="btnTestAttributeAPI" runat="server" Text="Test Attribute API" />
        </div>
        <div class="col-md-6">
        </div>


        <div class="col-md-6">
            <asp:Button ID="btnProcessResultBatch" runat="server" Text="Test API Status Batch"
                ToolTip="Batch" />
            <asp:Label ID="Label1" runat="server" AssociatedControlID="txtBatch">Batch</asp:Label>
            <asp:TextBox ID="txtBatch" runat="server">000100</asp:TextBox>
        </div>
        <div class="col-md-6">
            <asp:Button ID="btnProcessResultLogID" runat="server" Text="Test API Status BatchID" ToolTip="LogID" />
            <asp:Label ID="Label2" runat="server" AssociatedControlID="txtBatchID">Log ID</asp:Label>
            <asp:TextBox ID="txtBatchID" runat="server">14</asp:TextBox>
        </div>

    </div>

    <div class="row">
    	<div class="col-md-6">
            <label>Source Text:</label>
            <asp:TextBox ID="txtSource" runat="server" Rows="5" TextMode="MultiLine" />
        </div>
    	<div class="col-md-6">
            <label>Return Messages, Status:</label>
            <asp:TextBox ID="txtProcessMessageList" runat="server" Rows="5" TextMode="MultiLine" />
        </div>
    </div>


<%--

    <asp:Button ID="btnParseToLines" runat="server" Text="Parse to Lines" />
    <asp:Button ID="btnSubmitToProcess" runat="server" Text="Submit to process" />--%>
</asp:Content>
