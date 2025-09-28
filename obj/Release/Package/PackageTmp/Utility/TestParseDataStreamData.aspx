<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TestParseDataStreamData.aspx.cs" Inherits="BW_WebApp.Utility.TestParseDataStreamData" %>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="cMC" runat="server">
    <h1>Data Stream Parse Utility</h1>
    <label>Data stream data (WARNING: if you "process" the data stream, you will generate an IMEI inside the database. Run this only in Sandbox!):</label>
    <asp:TextBox ID="txtDataStream" runat="server" Rows="5" TextMode="MultiLine" />
    <div class="row">
    	<div class="col-md-6">
            <label>Data Stream (Value Pair per line):</label>
            <asp:TextBox ID="txtParseList" runat="server" Rows="5" TextMode="MultiLine" />
        </div>
    	<div class="col-md-6">
            <label>Processed Return Messages, Status:</label>
            <asp:TextBox ID="txtProcessMessageList" runat="server" Rows="5" TextMode="MultiLine" />
        </div>
    </div>
    <asp:Button ID="btnParseToLines" runat="server" Text="Parse to Lines" />
    <asp:Button ID="btnSubmitToProcess" runat="server" Text="Submit to process" />
</asp:Content>
