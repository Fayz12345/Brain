<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Maint_ScanCommand.aspx.cs" Inherits="BW_WebApp.Maintenance.Maint_ScanCommand" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cMC" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            
            <asp:HiddenField ID="hdnUserName" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnCurrentIMEI" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnCarrier3ID" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnManufacturer3ID" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnModel3ID" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnColour3ID" runat="server" ClientIDMode="Static" />

            <asp:HiddenField ID="hdnScanComandLookupID" runat="server" ClientIDMode="Static" />

            <asp:HiddenField ID="hdnCarrierTEXT" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnManufacturerTEXT" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnModelTEXT" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnColourTEXT" runat="server" ClientIDMode="Static" />

            <h1>Scan Command Utility</h1>


    <%--        <asp:TabPanel ID="TabSKUUComboSearch3" CssClass="tab-panel" runat="server" HeaderText="Scan Command" ToolTip="Scan Command">
                    <ContentTemplate>--%>
                        <h3>Scan Command Setup Utility</h3>
                        <asp:Label ID="lblDeviceToResku3" runat="server" Text="" />
                        <table class="table">
                        	<tr>
                                <td>
                                    <asp:Button ID="btnSKUClear3" runat="server" Text="Clear" />
                                    <asp:Button ID="btnRefresh3" runat="server" Text="Refresh" />
                                </td>
                                <td>
                                </td>
                                <td></td>
                            </tr>
                        
                            <tr>
                                <td colspan="3">
                                Scan Command:<br />
                                    <asp:TextBox ID="txtUPCCode" runat="server" Width="100%"></asp:TextBox>
                                </td>
                                <td ><br />
                                    <asp:Button ID="btnDeleteCommand" runat="server" Text="Delete" />
                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnDeleteCommand"
                                            ConfirmText="Are you sure you want this command deleted?" />
                                </td>
                            </tr>
                            <tr>
                        	    <td>Carrier:</td>
                                <td>
                                    <asp:DropDownList ID="drpCarrier3" runat="server" AutoPostBack="True" />
                                </td>
                                <td>
                                    <asp:Button ID="btnSelectCarrier" runat="server" Text="Add" />
                                </td>
                                <td>
                                    <asp:Label ID="lblCarrierABBR3" runat="server" Text="ABBR" />
                                </td>
                            </tr>
                            <tr>
                        	    <td>Manufacturer:</td>
                                <td>
                                    <asp:DropDownList ID="drpManufacturer3" runat="server" AutoPostBack="True" />
                                </td>
                                <td>
                                    <asp:Button ID="btnSelectManufacturer" runat="server" Text="Add" />
                                </td>
                                <td>
                                    <asp:Label ID="lblManufacturerABBR3" runat="server" Text="ABBR" />
                                </td>
                            </tr>
                            <tr>
                                <td>Model:</td>
                                <td>
                                    <asp:DropDownList ID="drpModel3" runat="server" AutoPostBack="True" />
                                </td>
                                <td>
                                    <asp:Button ID="btnSelectModel" runat="server" Text="Add" />
                                </td>
                                <td>
                                    <asp:Label ID="lblModelABBR3" runat="server" Text="ABBR" />
                                </td>
                            </tr>
                            <tr>
                                <td>Colour:</td>
                                <td>
                                    <asp:DropDownList ID="drpColour3" runat="server" AutoPostBack="True" />
                                </td>
                                <td>
                                    <asp:Button ID="btnAddColour" runat="server" Text="Add" />
                                </td>
                                <td>
                                    <asp:Label ID="lblColourABBR3" runat="server" Text="ABBR" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="btnSaveSKUSegments" runat="server" Text="Save SKU Segments" Visible="false" />
                                </td>
                                <td colspan="2">

                                </td>
                            </tr>

                            <tr>
                                <td>
                                    Question:
                                    <asp:DropDownList ID="drpQuestion" runat="server" AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    Option:
                                    <asp:DropDownList ID="drpOption" runat="server" AutoPostBack="False">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                <br />
                                    <asp:Button ID="btnAddQuestion" runat="server" Text="Add" />
                                </td>
                                <td>Value:
                                    <asp:TextBox ID="txtResponceText" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                <asp:GridView ID="MainGrid" CssClass="table" runat="server" AutoGenerateSelectButton="false"
                                    DataKeyNames="ScanComandLookupID" AutoGenerateColumns="False">
                                    <SelectedRowStyle CssClass="srowstyle" />
                                    <Columns>
                                        <asp:BoundField DataField="ScanComandLookupID" HeaderText="ID" ReadOnly="True" Visible="false" />
                                        <asp:BoundField DataField="ScanCode" HeaderText="ScanCode" ReadOnly="false" />
                                        <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="True" />
                                        <asp:BoundField DataField="OptionText" HeaderText="OptionText" ReadOnly="True" />
                                        <asp:BoundField DataField="SetValue" HeaderText="SetValue" ReadOnly="True" />
                                        <asp:BoundField DataField="CommandString" HeaderText="CommandString" ReadOnly="false" />
                                        <asp:BoundField DataField="ScanKey" HeaderText="ScanKey" ReadOnly="True" />
                                        <asp:BoundField DataField="ChainSequence" HeaderText="ChainSequence" ReadOnly="True"  Visible="false"/>
                                        <asp:BoundField DataField="ScanComandLookupAttributeListID" HeaderText="ScanComandLookupAttributeListID" ReadOnly="True"  Visible="false"/>
                                        <asp:BoundField DataField="OptionID" HeaderText="OptionID" ReadOnly="True"  Visible="false"/>
                                        <asp:BoundField DataField="QuestionID" HeaderText="QuestionID" ReadOnly="True"  Visible="false"/>
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="imgDelete" CssClass="btn btn-default" runat="server" ToolTip="Delete">
                                        <span class="oi oi-print"></span>
                                                </asp:LinkButton>
                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="imgDelete"
                                            ConfirmText="Are you sure you want this Link deleted?" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                </td>
                                <td colspan="3">
                                    <asp:Button ID="btnRefresh2" runat="server" Text="Refresh" Width="100%" Height="100%" />
                                </td>
                            </tr>
                        </table>
<%--                    </ContentTemplate>
                </asp:TabPanel>--%>

            <asp:Panel ID="pnlHome" runat="server">
                <table class="table">
                    <tr>
                        <td colspan="4">
                            <asp:TextBox ID="MasterTableMessage" runat="server" ReadOnly="True" TextMode="MultiLine" Rows="4" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
       </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="js" runat="server">


</asp:Content>
