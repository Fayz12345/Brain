<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TestSendEmail.aspx.cs" Inherits="BW_WebApp.Utility.TestSendEmail" %>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="cMC" runat="server">
    <div class="row">
    	<div class="col-md-6">
            <label>To:</label>
            <asp:TextBox runat="server" ID="txtTo" />

            <label>From Address:</label>
            <asp:TextBox runat="server" ID="txtFromAddress" />

            <label>From Display:</label>
            <asp:TextBox runat="server" ID="txtFromDisplay" />

            <label>Subject:</label>
            <asp:TextBox runat="server" ID="txtSubject" />

            <label>Body:</label>
            <asp:TextBox runat="server" ID="txtBody" Rows="5" TextMode="MultiLine" />
            <asp:Button ID="btnSend" runat="server" Text="Send Email" />

            <label>Result:</label>
            <asp:Label CssClass="d-block" ID="lblResult" runat="server" />
        </div>
    </div>
</asp:Content>
