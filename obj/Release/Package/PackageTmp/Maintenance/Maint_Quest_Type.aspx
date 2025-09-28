<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Maint_Quest_Type.aspx.cs" Inherits="BW_WebApp.Maintenance.Maint_Quest_Type" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="cMC" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlMainView" runat="server">

                <h1><asp:Label ID="lblRecordTitle" runat="server" Text="Maintenance Question Type" /></h1>

                <asp:Button ID="btnAdd" runat="server" Text="Add" onclick="btnAdd_Click" />
                <asp:Button ID="btnEdit" runat="server" Text="Edit" Visible="False" onclick="btnEdit_Click" />
                <asp:Button ID="btnDelete" runat="server" Text="Delete" Visible="False" onclick="btnDelete_Click" />
                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Continue with Delete?" Enabled="True" TargetControlID="btnDelete" />
                    
                <asp:GridView ID="MainGrid" CssClass="table table-nonfluid" runat="server" AutoGenerateSelectButton="True" DataKeyNames="QuestionTypeID" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="QuestionTypeID" HeaderText="ID" ReadOnly="True" Visible="false" />
                        <asp:BoundField DataField="Type" HeaderText="Type" />
                    </Columns>
                </asp:GridView>

            </asp:Panel>

            <asp:Panel ID="pnlAdd" runat="server">
                <h1>Add Question Type</h1>
                <label>Type:</label>
                <asp:TextBox ID="AddType" CssClass="w-md-50" runat="server" />
                <asp:Button ID="AddOK" runat="server" Text="OK" onclick="AddOK_Click" />
                <asp:Button ID="AddCancel" runat="server" Text="Cancel" onclick="AddCancel_Click1" />
            </asp:Panel>

            <asp:Panel ID="pnlEdit" runat="server">
                <h1>Edit Question Type</h1>
                <label>Type:</label>
                <asp:TextBox ID="EditType" CssClass="w-md-50" runat="server" />
                <asp:TextBox ID="EditKeyID" CssClass="w-md-50" runat="server" ReadOnly="True" Visible="False" />
                <asp:Button ID="EditOK" runat="server" Text="OK" onclick="EditOK_Click" />
                <asp:Button ID="EditCancel" runat="server" Text="Cancel" onclick="EditCancel_Click" />
            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


