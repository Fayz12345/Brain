<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Maint_MasterPartsTransactionType.aspx.cs" Inherits="BW_WebApp.Maintenance.Maint_MasterPartsTransactionType" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %> 

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="cMC" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlMainView" runat="server">

                <h1><asp:Label ID="lblRecordTitle" runat="server" Text="Master Part Transaction Type" /></h1>

                <asp:Button ID="btnAdd" runat="server" Text="Add" onclick="btnAdd_Click" />
                <asp:Button ID="btnEdit" runat="server" Text="Edit" Visible="False" onclick="btnEdit_Click" />
                <asp:Button ID="btnDelete" runat="server" Text="Delete" Visible="False" onclick="btnDelete_Click" />
                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Continue with Delete?"
                    Enabled="True" TargetControlID="btnDelete" />
                    
                <asp:Panel ID="pnlMainGrid" CssClass="overflow mb-3" runat="server">
                    <asp:GridView ID="MainGrid" CssClass="table table-striped" runat="server" AutoGenerateSelectButton="True"
                        DataKeyNames="PartNumberBucketInventorysourceTypeID" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="PartNumberBucketInventorysourceTypeID" HeaderText="ID" ReadOnly="True" Visible="false" />
                            <asp:BoundField DataField="Type" HeaderText="Type" />
                            <asp:BoundField DataField="Factor" HeaderText="Factor" />
                            <asp:BoundField DataField="IFSDirectiveType" HeaderText="DirectiveType" />
                            <asp:BoundField DataField="IFSReasonCode" HeaderText="Reason" />
                            <asp:BoundField DataField="Role" HeaderText="Access Role" />
                        </Columns>
                    </asp:GridView>
                </asp:Panel>

            </asp:Panel>

            <asp:Panel ID="pnlAdd" CssClass="w-md-50" runat="server">
                <h1>Add Transaction Type</h1>
                
                <label>Type:</label>
                <asp:TextBox ID="AddType" runat="server" />
                
                <label>Factor:</label>
                <asp:TextBox ID="AddFactor" runat="server" />
                
                <label>IFS Directive Name:</label>
                <asp:TextBox ID="AddIFSDirective" runat="server" MaxLength="20" />
                
                <label>IFS Reason Code:</label>
                <asp:TextBox ID="AddIFSReasonCode" runat="server" MaxLength="3" />
                
                <label>Access Role:</label>
                <asp:TextBox ID="AddAccessRole" runat="server" MaxLength="50" ToolTip="Access Role, Leave blank for all" />

                <asp:Button ID="AddOK" runat="server" Text="OK" onclick="AddOK_Click" />
                <asp:Button ID="AddCancel" runat="server" Text="Cancel" onclick="AddCancel_Click1" />
            </asp:Panel>

            <asp:Panel ID="pnlEdit" CssClass="w-md-50" runat="server">
                <h1>Edit Transaction Type</h1>
                <label>Status:</label>
                <asp:TextBox ID="EditType" runat="server" />
                <asp:TextBox ID="EditKeyID" runat="server"  ReadOnly="True" Visible="False" />
                
                <label>Factor:</label>
                <asp:TextBox ID="EditFactor" runat="server" />
                
                <label>IFS Directive Name:</label>
                <asp:TextBox ID="EditIFSDirective" runat="server" MaxLength="20" />
                
                <label>IFS Reason Code:</label>
                <asp:TextBox ID="EditIFSReasonCode" runat="server" MaxLength="3" />
                
                <label>Access Role:</label>
                <asp:TextBox ID="editAccessRole" runat="server" MaxLength="50" ToolTip="Access Role, Leave blank for all" />
                
                <asp:Button ID="EditOK" runat="server" Text="OK" onclick="EditOK_Click" />
                <asp:Button ID="EditCancel" runat="server" Text="Cancel" onclick="EditCancel_Click" />
            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>




