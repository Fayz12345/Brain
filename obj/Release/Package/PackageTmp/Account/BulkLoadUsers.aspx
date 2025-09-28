<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BulkLoadUsers.aspx.cs" Inherits="BM_WebApp.Account.BulkLoadUsers" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>




<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cMC" runat="server">
<h1>
Bulk upload account (with roles)
</h1>
<br />
<br />
        <asp:Panel ID="pnlUpload" runat="server">
            <div style="background: url(~/Images/hline.gif) repeat-x bottom #F2F2F2; padding: 8px 5px;
                border-bottom: 1px solid #ccc;">
                <asp:FileUpload ID="FileUploadXLS" runat="server" Width="80%" size="100"></asp:FileUpload>&nbsp;&nbsp;
                <asp:Button ID="btnUpload" runat="server" Text="Upload" /> 
                <br />
                <asp:Label ID="lblMsgDetail" runat="server" Visible="False" Font-Bold="True" ForeColor="#009933"></asp:Label>
            </div> 
        </asp:Panel>


</asp:Content>
