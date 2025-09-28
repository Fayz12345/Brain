<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Maint_Client_Status.aspx.cs" Inherits="BW_WebApp.Maintenance.Maint_Client_Status" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="cMC" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlMainView" runat="server">

                <h1><asp:Label ID="lblRecordTitle" runat="server" Text="Maintenance Client Status" /></h1>

                <asp:Button ID="btnAdd" runat="server" Text="Add" onclick="btnAdd_Click" />
                <asp:Button ID="btnEdit" runat="server" Text="Edit" Visible="False" onclick="btnEdit_Click" />
                <asp:Button ID="btnDelete" runat="server" Text="Delete" Visible="False" onclick="btnDelete_Click" />
                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Continue with Delete?" Enabled="True" TargetControlID="btnDelete" />

                <asp:Panel ID="pnlMainGrid" runat="server">
                    <asp:GridView ID="MainGrid" CssClass="table table-nonfluid" runat="server" AutoGenerateSelectButton="True" DataKeyNames="ClientStatusID" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="ClientStatusID" HeaderText="ID" ReadOnly="True" Visible="false" />
                            <asp:BoundField DataField="Status" HeaderText="Status" />
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </asp:Panel>

            <asp:Panel ID="pnlAdd" CssClass="w-md-50" runat="server">
                <h1>Add Client Status</h1>
                
                <label>Status:</label>
                <asp:TextBox ID="AddStatus" runat="server" />
                
                <asp:Button ID="AddOK" runat="server" Text="OK" onclick="AddOK_Click" />
                <asp:Button ID="AddCancel" runat="server" Text="Cancel" onclick="AddCancel_Click1" />
            </asp:Panel>

            <asp:Panel ID="pnlEdit" CssClass="w-md-50" runat="server">
                <h1>Edit Client Status</h1>
                
                <label>Status:</label>
                <asp:TextBox ID="EditStatus" runat="server" />
                <asp:TextBox ID="EditKeyID" runat="server" ReadOnly="True" Visible="False" />
                
                <asp:Button ID="EditOK" runat="server" Text="OK" onclick="EditOK_Click" />
                <asp:Button ID="EditCancel" runat="server" Text="Cancel" onclick="EditCancel_Click" />
            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


