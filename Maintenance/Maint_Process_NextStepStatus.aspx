<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Maint_Process_NextStepStatus.aspx.cs" Inherits="BW_WebApp.Maintenance.Maint_Process_NextStepStatus" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="cMC" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlMainView" runat="server">

                <h1><asp:Label ID="lblRecordTitle" runat="server" Text="Maintenance Process Next Step Status" /></h1>

                <asp:Button ID="btnAdd" runat="server" Text="Add" onclick="btnAdd_Click" />
                <asp:Button ID="btnEdit" runat="server" Text="Edit" Visible="False" onclick="btnEdit_Click" />
                <asp:Button ID="btnDelete" runat="server" Text="Delete" Visible="False" onclick="btnDelete_Click" />
                <asp:ConfirmButtonExtender runat="server" ConfirmText="Continue with Delete?" Enabled="True" TargetControlID="btnDelete" />
                    
                <asp:GridView ID="MainGrid" CssClass="table table-nonfluid" runat="server" AutoGenerateSelectButton="True" DataKeyNames="ProcessStatusID" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="ProcessStatusID" HeaderText="ID" ReadOnly="True" Visible="false" />
                        <asp:BoundField DataField="Status" HeaderText="Status" />
                    </Columns>
                </asp:GridView>

            </asp:Panel>

            <asp:Panel ID="pnlAdd" runat="server">
                <h1>Add Process Next Step Status</h1>
                <label>Status:</label>
                <asp:TextBox ID="AddStatus" CssClass="w-md-50" runat="server" />
                <asp:Button ID="AddOK" runat="server" Text="OK" onclick="AddOK_Click" />
                <asp:Button ID="AddCancel" runat="server" Text="Cancel" onclick="AddCancel_Click1" />
            </asp:Panel>

            <asp:Panel ID="pnlEdit" runat="server">
                <h1>Edit Process Next Step Status</h1>
                <label>Status:</label>
                <asp:TextBox ID="EditStatus" CssClass="w-md-50" runat="server" />
                <asp:TextBox ID="EditKeyID" CssClass="w-md-50" runat="server" ReadOnly="True" Visible="False" />
                <asp:Button ID="EditOK" runat="server" Text="OK" onclick="EditOK_Click" />
                <asp:Button ID="EditCancel" runat="server" Text="Cancel" onclick="EditCancel_Click" />
            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>