<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Maint_ClientBillingPoints.aspx.cs" Inherits="BW_WebApp.Maintenance.Maint_ClientBillingPoints" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cMC" runat="server">
     <asp:HiddenField ID="hdnClientID" runat="server" />
    <h1>
        <asp:Label ID="lblClientTest_01" runat="server" Text="Billing Points for:"></asp:Label>
    </h1>
    <br />
    <br />
    <asp:TabContainer runat="server" ID="tabChild" Width="950px" ActiveTabIndex="0" BorderStyle="None">
        <asp:TabPanel runat="server" ID="tabBillingPoints" Enabled="true" HeaderText="Billing Points">
            <ContentTemplate>
                <asp:Button ID="btnSaveBillingPoints" runat="server" Text="Update Billing Points" />
                <asp:Button ID="btnLoadBillingPointsDefault" runat="server" Text="Load Default Billing Points" />
                <asp:Label ID="lblDefault" runat="server" Text=""></asp:Label>
                <asp:GridView ID="grdBillingPoints" runat="server" DataKeyNames="ClientBillingPointID"
                    AutoGenerateColumns="False" Width="100%" CssClass="mGrid" PagerStyle-CssClass="pgr"
                    AlternatingRowStyle-CssClass="alt">
                    <SelectedRowStyle CssClass="srowstyle" />
                    <Columns>
                        <asp:TemplateField HeaderText='Billing Point'>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkisBillingPoint" runat="server" ToolTip="Is Billing Point" />
                                <asp:TextBox ID="txtRateValue" runat="server" ToolTip="Rate/Value"></asp:TextBox>
                                <asp:HiddenField ID="hdnClientBillingPointID" runat="server" />
                                <asp:HiddenField ID="hdnProjectID" runat="server" />
                                <asp:HiddenField ID="hdnProcessID" runat="server" />
                                <asp:HiddenField ID="hdnClientID" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ProjectName" HeaderText="Project Name" HeaderStyle-HorizontalAlign="Left">
                            <ItemStyle HorizontalAlign="left" Wrap="True" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ProjectDescription" HeaderText="Project Description" HeaderStyle-HorizontalAlign="Left">
                            <ItemStyle HorizontalAlign="left" Wrap="True" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ProcessName" HeaderText="Process Name" HeaderStyle-HorizontalAlign="Left">
                            <ItemStyle HorizontalAlign="left" Wrap="True" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ProcessDescription" HeaderText="Process Description" HeaderStyle-HorizontalAlign="Left">
                            <ItemStyle HorizontalAlign="left" Wrap="True" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Description_Client" HeaderText="Description Client" HeaderStyle-HorizontalAlign="Left">
                            <ItemStyle HorizontalAlign="left" Wrap="True" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
</asp:Content>

