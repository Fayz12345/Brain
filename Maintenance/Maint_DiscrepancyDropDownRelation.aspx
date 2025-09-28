<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Maint_DiscrepancyDropDownRelation.aspx.cs" Inherits="BW_WebApp.Maintenance.Maint_DiscrepancyDropDownRelation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="cMC" runat="server">
    <asp:HiddenField ID="hdnClientID" runat="server" />
    <h1><asp:Label ID="lbl_01" runat="server" Text="Discrepancy Dropdown Relation" /></h1>
    
    <asp:TabContainer runat="server" ID="tabChild" CssClass="tab-container">

        <asp:TabPanel runat="server" ID="tabTypeDiscrepancy" CssClass="tab-panel" Enabled="true" HeaderText="Type Discrepancy">
            <ContentTemplate>
                <div class="row">
                	<div class="col-md-6">
                        <label>Type:</label>
                        <asp:DropDownList ID="drpType" runat="server" ToolTip="Project" AutoPostBack="True" />
            
                        <label>Discrepancy:</label>
                        <asp:CheckBoxList ID="chkDiscrepancy1" CssClass="checklist-inline" runat="server" RepeatLayout="UnorderedList" />
            
                        <asp:Button ID="btnSaveTypeDisc" runat="server" Text="Save" />
                        <asp:Label ID="lblSaveTypeDisc" runat="server" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:TabPanel>

        <asp:TabPanel runat="server" ID="tabARestrictions" CssClass="tab-panel" Enabled="true" HeaderText="A Restrictions">
            <ContentTemplate>
                <div class="row">
                	<div class="col-md-6">
                        <label>Discrepancy:</label>
                        <asp:DropDownList ID="drpDiscrepancy2" runat="server" ToolTip="Project" AutoPostBack="True" />
                
                        <label>Outcome:</label>
                        <asp:CheckBoxList ID="chkOutcome" CssClass="checklist-inline" runat="server" RepeatLayout="UnorderedList"/>
                
                        <asp:Button ID="btnSaveDiscOut" runat="server" Text="Save" />
                        <asp:Label ID="lblSaveDiscOut" runat="server" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:TabPanel>

    </asp:TabContainer>
</asp:Content>
