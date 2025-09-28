<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Maint_ClientAllowedProject.aspx.cs" Inherits="BW_WebApp.Maintenance.Maint_ClientAllowedProject" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cMC" runat="server">
    <asp:HiddenField ID="hdnClientID" runat="server" />
    <h1><asp:Label ID="lblClientTest_01" runat="server" Text="Allowed Projects for:" /></h1>
    <asp:TabContainer runat="server" ID="tabChild" CssClass="tab-container" ActiveTabIndex="0">
        <asp:TabPanel runat="server" ID="tabAllowedProjects" CssClass="tab-panel" Enabled="true" HeaderText="Allowed Projects">
            <ContentTemplate>
                <asp:Button ID="btnSaveAllowedProject" runat="server" Text="Update" />
                <asp:GridView ID="grdProjectList" CssClass="table" runat="server" DataKeyNames="ProjectID" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="ProjectID" HeaderText="ID" ReadOnly="True" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkThisProject" runat="server" />
                                <asp:HiddenField ID="hdnClientProjectDependenciesID" runat="server" />
                                <asp:HiddenField ID="hdnProjectID" runat="server" />
                                <asp:HiddenField ID="hdnClientID" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
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
