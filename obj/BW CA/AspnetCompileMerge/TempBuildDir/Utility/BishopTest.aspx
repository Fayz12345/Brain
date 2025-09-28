<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BishopTest.aspx.cs" Inherits="BW_WebApp.Utility.BishopTest" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cMC" runat="server">
    <asp:TabContainer ID="TabContainer1" CssClass="tab-container" runat="server">
        <asp:TabPanel ID="tabGet" CssClass="tab-panel" HeaderText="Get Functions" runat="server" Visible="True">
            <ContentTemplate>
                <div class="row">
                	<div class="col">
                        <label class="d-block">Get the Full SKU Catalogue:</label>
                        <asp:Button ID="btnGetCatalogueJSON" runat="server" Text="Catalogue Full JSON" />
                        <asp:Button ID="btnGetCatalogueXML" runat="server" Text="Catalogue Full XML" />
                        <hr>
                        <label class="d-block">Get the Catalogue for a single SKU:</label>
                        <asp:TextBox ID="txtSingleSKU" runat="server" ToolTip="" />
                        <asp:Button ID="btnGetCatalogueSingle" runat="server" Text="Catalogue Single" />
                        <hr>
                        <label class="d-block">Get the Catalogue for a comma delimited list of SKU:</label>
                        <asp:TextBox ID="txtSKUList" runat="server" ToolTip="" />
                        <asp:Button ID="btnGetCatalogueListJSON" runat="server" Text="Catalogue List JSON" />
                        <asp:Button ID="btnGetCatalogueListXML" runat="server" Text="Catalogue List XML" />
                        <hr>
                        <label class="d-block">Get Pick List Input Format:</label>
                        <asp:Button ID="btnPickListJSON" runat="server" Text="Pick List Put Format JSON" />
                        <asp:Button ID="btnPickListXML" runat="server" Text="Pick List Put Format XML" />
                        <hr>
                        <label class="d-block">Get PickList Status:</label>
                        <asp:Button ID="btnPickListStatusJSON" runat="server" Text="Pick List Status JSON" />
                        <asp:Button ID="btnPickListStatusXML" runat="server" Text="Pick List Status XML" />
                    </div>
                	<div class="col">
                        <label>Output:</label>
                        <asp:TextBox ID="txtOutput" CssClass="h-75" runat="server" TextMode="MultiLine" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
</asp:Content>


