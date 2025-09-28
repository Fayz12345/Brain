<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Maint_ErrorReporting.aspx.cs" Inherits="BW_WebApp.Maintenance.Maint_ErrorReporting" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="cMC" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdnForceSave" runat="server" />
            

            <h1>Error Reporting</h1>

            <div class="row">
            	<div class="col-md-6">
                    <label>IMEI:</label>
                    <asp:TextBox ID="txtIMEI" runat="server" ToolTip="IMEI associated with error being logged" />
                
                    <label>User Name:</label>
                    <asp:DropDownList ID="drpUserList" runat="server" ToolTip="User associated with report" />
                
                    <label>Date:</label>
                    <asp:TextBox ID="txtDate" runat="server" ToolTip="date of incident" />
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate" Format="MM/dd/yyyy" />
                
                    <label>Error Found:</label>
                    <asp:DropDownList ID="drpERR_Error_Found" runat="server" ToolTip="Error scenario" />
                
                    <label>Error Details:</label>
                    <asp:TextBox ID="txtERR_Error_Details" runat="server" ToolTip="Error Details" MaxLength="250" TextMode="MultiLine" />
                
                    <label>Action Taken:</label>
                    <asp:TextBox ID="txtERR_Action_Taken" runat="server" ToolTip="Action Taken" MaxLength="250" TextMode="MultiLine" />
                
                    <label>Action Taken By:</label>
                    <asp:DropDownList ID="drpActionTakenBy" runat="server" ToolTip="User Taken Action" />
                
                    <asp:Button ID="btnSave" runat="server" Text="Save" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" />
                </div>
            </div>
                
            <asp:Label ID="lblMessage" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

