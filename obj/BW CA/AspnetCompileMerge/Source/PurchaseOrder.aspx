<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PurchaseOrder.aspx.cs" Inherits="BW_WebApp.PurchaseOrder" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>
<%@ Register Assembly="Syncfusion.Tools.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89"  Namespace="Syncfusion.Web.UI.WebControls.Tools" TagPrefix="syncfusion" %>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="cMC" runat="server">
    <asp:HiddenField ID="hdnUserName" runat="server" ClientIDMode="Static" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlMainView" runat="server">
                <h1>Purchase Order</h1>
                
                <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
                <asp:Button ID="btnEdit" runat="server" Text="Edit" Visible="False" OnClick="btnEdit_Click" />
                <asp:Button ID="btnDelete" runat="server" Text="Delete" Visible="False" OnClick="btnDelete_Click" />
                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Continue with Delete?"
                    Enabled="True" TargetControlID="btnDelete" />
                <asp:Label ID="lblRecordTitle" runat="server" />

                <asp:Panel ID="pnlMainGrid" runat="server">
                    <asp:GridView ID="MainGrid" CssClass="table" runat="server" AutoGenerateSelectButton="True" DataKeyNames="PurchaseOrderHeaderID" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="PurchaseOrderHeaderID" HeaderText="ID" ReadOnly="True" Visible="false" />
                            <asp:BoundField DataField="Status" HeaderText="Status" />
                            <asp:BoundField DataField="ProjectTag" HeaderText="Project Tag" />
                            <asp:BoundField DataField="PurchaseQTY" HeaderText="QTY" />
                            <asp:BoundField DataField="PurchasePrice" HeaderText="Purchase Price" />
                            <asp:BoundField DataField="PurchaseUnitPrice" HeaderText="Unit Price" />
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </asp:Panel>

            <asp:Panel ID="pnlAdd" runat="server">
                <h1>Add Purchase Order</h1>
                <div class="row">
                	<div class="col-md-6">
                        <%--<label>Status:</label>
                        <asp:TextBox ID="AddStatus" runat="server" />--%>

                        <label>Project Tag:</label>
                        <asp:TextBox ID="AddProjectTag" runat="server" />
                     
                        <label>Number of units purchased:</label>
                        <asp:TextBox ID="AddPurchaseQTY" runat="server" />
                    
                        <label>Purchase Price:</label>
                        <asp:TextBox ID="AddPurchasePrice" runat="server" />
                    
                        <asp:Button ID="AddOK" runat="server" Text="OK" onclick="AddOK_Click" />
                        <asp:Button ID="AddCancel" runat="server" Text="Cancel" onclick="AddCancel_Click1" />
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="pnlEdit" runat="server">
                <h1>Edit Purchase Order</h1>
                <div class="row">
                	<div class="col-md-6">
                        <asp:TextBox ID="EditKeyID" runat="server"  ReadOnly="True" Visible="False" />
                        
                        <label>Project Tag:</div>
                        <asp:TextBox ID="EditProjectTag" runat="server" />
                        
                        <label> Number of units purchased:</div>
                        <asp:TextBox ID="EditPurchaseQTY" runat="server" />
                        
                        <label>Purchase Price:</label>
                        <asp:TextBox ID="EditPurchasePrice" runat="server" />
                        
                        <asp:Button ID="EditOK" runat="server" Text="OK" onclick="EditOK_Click" />
                        <asp:Button ID="EditCancel" runat="server" Text="Cancel" onclick="EditCancel_Click" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

