<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard_ProdPlace_01.aspx.cs" Inherits="BW_WebApp.Dashboard_ProdPlace_01" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cMC" runat="server">
    <h1>Dashboard</h1>
    <asp:TabContainer runat="server" ID="MainTab" CssClass="tab-container" ActiveTabIndex="0" >
        <asp:TabPanel runat="server" ID="tb1x" CssClass="tab-panel" Enabled="true" HeaderText="Inventory QTY">
            <ContentTemplate>
                <div>
                    <asp:Button ID="btnRefreshINVQTY" runat="server" Text="Refresh" />
                    <asp:LinkButton ID="LinkButton1" CssClass="btn btn-default" runat="server" OnClientClick="printIt(); return false;"
                        ToolTip="Print" Visible="false">
                                <span class="oi oi-print"></span>
                    </asp:LinkButton>
                </div>
                <asp:Panel ID="Panel1" runat="server">
                    <div class="form-row">
                        <asp:Panel ID="Panel3" runat="server">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <label>
                                            Project:</label>
                                        <asp:DropDownList ID="drpProjectList" runat="server" ToolTip="Project" />
                                        &nbsp;
                                    </td>
                                    <td>
                                        <label>
                                            Product Placement :</label>
                                        <asp:DropDownList ID="drpProductPlacement" runat="server" ToolTip="Product Placement" />
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text="Filter on Role:" AssociatedControlID="txtRoleFilter"></asp:Label>
                                        <asp:TextBox ID="txtRoleFilter" runat="server" Text="" />
                                        <asp:Label ID="Label3" runat="server" Text="Example:Role1,Role2,Role3"></asp:Label>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="Panel4" runat="server" Style="overflow: auto; max-height: 500px; width: auto;"
                                HorizontalAlign="Left">
                                <asp:GridView ID="grdInventoryQTY" CssClass="table" runat="server" AutoGenerateColumns="false"
                                    ShowHeader="true" AutoGenerateSelectButton="false" SelectedRowStyle-HorizontalAlign="Right"
                                    AlternatingRowStyle-BackColor="#CCCCCC" BorderColor="#FF9966">
                                    <Columns>
                                        <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" ReadOnly="True" />
                                        <asp:BoundField DataField="Product_Place" HeaderText="Product Place" ReadOnly="True" />
                                        <asp:BoundField DataField="Manufacturer" HeaderText="Manufacturer" ReadOnly="True" />
                                        <asp:BoundField DataField="Model" HeaderText="Model" ReadOnly="True" />
                                        <asp:BoundField DataField="Not_Graded" HeaderText="Not Graded" ReadOnly="True" />
                                        <asp:BoundField DataField="Open_Package" HeaderText="Open Package" ReadOnly="True" />
                                        <asp:BoundField DataField="A" HeaderText="A" ReadOnly="True" />
                                        <asp:BoundField DataField="B" HeaderText="B" ReadOnly="True" />
                                        <asp:BoundField DataField="C" HeaderText="C" ReadOnly="True" />
                                        <asp:BoundField DataField="Total" HeaderText="Total" ReadOnly="True" />
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </asp:Panel>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:TabPanel>



    </asp:TabContainer>
    <asp:Timer ID="Timer1" runat="server" Interval="9000000" OnTick="UpdateStats" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="js" runat="server">
    <script type="text/javascript">

        //        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        //        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

        function BeginRequestHandler(sender, args) {
            if (args._postBackElement.id != "SkinTable1") {
                $('#loading').show();
            }
        }

        function EndRequestHandler(sender, args) {
            $('#loading').hide();
        }

        function printIt() {
            window.print();
        }

    </script>
</asp:Content>


