<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IFSLocationStatus.aspx.cs" Inherits="GMPI_WebApp.IFSLocationManagement.IFSLocationStatus" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="cMC" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlMainView" runat="server">
                <asp:Panel ID="Panel3" runat="server" HorizontalAlign="Left" Width="100%">
                    <br />
                    <asp:Button ID="btnAdd" runat="server" Text="Add" onclick="btnAdd_Click" />
                    <asp:Button ID="btnEdit" runat="server" Text="Edit" Visible="False" 
                        onclick="btnEdit_Click" />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" Visible="False" 
                        onclick="btnDelete_Click" />
                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Continue with Delete?"
                        Enabled="True" TargetControlID="btnDelete">
                    </asp:ConfirmButtonExtender>
                    <asp:Label ID="lblRecordTitle" runat="server" Text="Maintenance IFSLocation Status"></asp:Label>
                </asp:Panel>
                <asp:Panel ID="pnlMainGrid" runat="server" Height="2in" Width="100%" ScrollBars="Auto"
                    HorizontalAlign="Left">
                    <asp:GridView ID="MainGrid" runat="server" Width="100%" AutoGenerateSelectButton="True"
                        DataKeyNames="MasterIFSLocationStatusID" AutoGenerateColumns="False" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                        <SelectedRowStyle CssClass="srowstyle" />

                        <Columns>
                            <asp:BoundField DataField="MasterIFSLocationStatusID" HeaderText="ID" ReadOnly="True" Visible="false">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-HorizontalAlign="Left">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </asp:Panel>
            <asp:Panel ID="pnlAdd" runat="server">
                <table>
                    <tr>
                        <td id="AddTemplateHeader" colspan="2" style="border-style: none none solid none;
                            border-width: thick; border-color: #F5F5F5; text-align: center; vertical-align: middle;">
                            <h1>
                                Add IFSLocation Status</h1>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; vertical-align: top;">
                            Status:
                        </td>
                        <td>
                            <asp:TextBox ID="AddStatus" runat="server" ></asp:TextBox><br />
                        </td>
                    </tr>
 
                    <tr>
                        <td colspan="2" style="border-style: solid none none none; border-width: thick; border-color: #F5F5F5;
                            text-align: left; vertical-align: middle;">
                            <asp:Button ID="AddOK" runat="server" Text="OK" onclick="AddOK_Click" />
                            <asp:Button ID="AddCancel" runat="server" Text="Cancel" 
                                onclick="AddCancel_Click1" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlEdit" runat="server">
                <table>
                    <tr>
                        <td id="Td1" colspan="2" style="border-style: none none solid none; border-width: thick;
                            border-color: #F5F5F5; text-align: center; vertical-align: middle;">
                            <h1>
                                Edit IFSLocation Status</h1>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; vertical-align: top;">
                            Status:
                        </td>
                        <td>
                            <asp:TextBox ID="EditStatus" runat="server" BackColor="#FFFF66"></asp:TextBox><br />
                            <asp:TextBox ID="EditKeyID" runat="server" BackColor="#FFFF66" ReadOnly="True" Visible="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="border-style: solid none none none; border-width: thick; border-color: #FFCC66;
                            text-align: left; vertical-align: middle;">
                            <asp:Button ID="EditOK" runat="server" Text="OK" onclick="EditOK_Click" />
                            <asp:Button ID="EditCancel" runat="server" Text="Cancel" onclick="EditCancel_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>



        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



