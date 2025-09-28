<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Maint_ClientAlowedProcess.aspx.cs" Inherits="BW_WebApp.Maintenance.Maint_ClientAlowedProcess" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cMC" runat="server">
    <asp:HiddenField ID="hdnClientID" runat="server" />
    <h1><asp:Label ID="lblClientTest_01" runat="server" Text="Allowed Processes for:" /></h1>
    <asp:TabContainer runat="server" ID="tabChild" CssClass="tab-container" ActiveTabIndex="0" BorderStyle="None">
        <asp:TabPanel runat="server" ID="tabAllowedProcesses" CssClass="tab-panel" Enabled="true" HeaderText="Allowed Processes">
            <ContentTemplate>
                <asp:Button ID="btnSaveAllowedProcesses" runat="server" Text="Update" />
                <asp:GridView ID="grdProcessList" runat="server" DataKeyNames="ProcessID" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="ProcessID" HeaderText="ID" ReadOnly="True" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkThisProcess" runat="server" />
                                <asp:HiddenField ID="hdnClientProcessDependenciesID" runat="server" />
                                <asp:HiddenField ID="hdnProcessID" runat="server" />
                                <asp:HiddenField ID="hdnClientID" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ScanKey" HeaderText="ScanKey">
                            <ItemStyle Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Name" HeaderText="Name">
                            <ItemStyle Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Description" HeaderText="Description">
                            <ItemStyle Wrap="False" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
</asp:Content>
