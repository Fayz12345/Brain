<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UtilityChangeIMEI.aspx.cs" Inherits="BW_WebApp.Utility.UtilityChangeIMEI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="cMC" runat="server">
    <strong class="d-block mb-3">
        This Utility will allow the user to Change an incorrect IMEI to the correct IMEI.
    </strong>
    <div class="row">
    	<div class="col-md-6">
            <label>Incorrect IMEI:</label>
            <asp:TextBox ID="OriginalIMEI" runat="server" ToolTip="Incorrect IMEI" />

            <label>Version:</label>
            <asp:TextBox ID="Version" runat="server" MaxLength="3" ToolTip="Version" />

            <label>Correct IMEI:</label>
            <asp:TextBox ID="NewIMEI" runat="server" ToolTip="Corrected IMEI" />

            <asp:Button ID="btnSave" runat="server" Text="Save Changes" />
            <asp:Label ID="lblMessage" runat="server" />
        </div>
    </div>
</asp:Content>


