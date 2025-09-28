<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard_QC_01.aspx.cs" Inherits="BW_WebApp.Dashboard_QC_01" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cMC" runat="server">
    <h1>Dashboard - QC</h1>
    <asp:TabContainer runat="server" ID="MainTab" CssClass="tab-container" ActiveTabIndex="0" >
        <asp:TabPanel runat="server" ID="TabPanel1x" CssClass="tab-panel" Enabled="true"
            HeaderText="QC 01">
            <ContentTemplate>
                <div>
                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" />
                    <asp:LinkButton ID="btnPrint" CssClass="btn btn-default" runat="server" OnClientClick="printIt(); return false;"
                        ToolTip="Print" Visible="false">
                                <span class="oi oi-print"></span>
                    </asp:LinkButton>
                </div>
                <asp:Panel ID="Panel2" runat="server">
                    <div class="form-row">
                        <asp:Panel ID="Panel8" runat="server">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkReceived" CssClass="d-block mb-1" runat="server" Text="as of Date:"
                                            ToolTip="If unchecked, this will default to Today" />
                                        <asp:TextBox ID="txtBeginDate" runat="server" Text="10/06/2016" />
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtBeginDate" />
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text="Filter on Role:" AssociatedControlID="txtRoleFilter"></asp:Label>
                                        <asp:TextBox ID="txtRoleFilter" runat="server" Text="" />
                                        <asp:Label ID="Label3" runat="server" Text="Example:Role1,Role2,Role3"></asp:Label>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text="Number of workdays to report:" AssociatedControlID="txtWorkDaysToReport" Visible="false"></asp:Label>
                                        <asp:TextBox ID="txtWorkDaysToReport" runat="server" Text="5" MaxLength="5"  Visible="false"/>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="pnlMainGrid" runat="server" Style="overflow: auto; max-height: 500px;
                                width: auto;" HorizontalAlign="Left">
                                <asp:GridView ID="grdTabRepair01" CssClass="table" runat="server" AutoGenerateColumns="false"
                                    ShowHeader="true" AutoGenerateSelectButton="false" SelectedRowStyle-HorizontalAlign="Right"
                                    AlternatingRowStyle-BackColor="#CCCCCC" BorderColor="#FF9966">
                                    <SelectedRowStyle CssClass="srowstyle" />
                                    <Columns>
                                        <asp:BoundField DataField="CreateUser" HeaderText="Rep" ReadOnly="True" />
                                        <asp:BoundField DataField="C0_Date" HeaderText="Period" ReadOnly="True" />
                                        <asp:BoundField DataField="C0_T" HeaderText="Total" ReadOnly="True" />
                                        <asp:BoundField DataField="C0_A" HeaderText="Activation" ReadOnly="True" />
                                        <asp:BoundField DataField="C0_B" HeaderText="Buffing" ReadOnly="True" />
                                        <asp:BoundField DataField="C0_C" HeaderText="Function Test" ReadOnly="True" />
                                        <asp:BoundField DataField="C0_D" HeaderText="Grade Improvement" ReadOnly="True" />
                                        <asp:BoundField DataField="C0_E" HeaderText="Grading" ReadOnly="True" />
                                        <asp:BoundField DataField="C0_F" HeaderText="Physical Damage" ReadOnly="True" />
                                        <asp:BoundField DataField="C0_G" HeaderText="Unlocking" ReadOnly="True" />
                                        
                               <%--         
                               <asp:BoundField DataField="C0_H" HeaderText="Device Status" ReadOnly="True" />
                               <asp:BoundField DataField="C1_Date" HeaderText="Period" ReadOnly="True" />
                                        <asp:BoundField DataField="C1_T" HeaderText="Total" ReadOnly="True" />
                                        <asp:BoundField DataField="C1_A" HeaderText="Activation" ReadOnly="True" />
                                        <asp:BoundField DataField="C1_B" HeaderText="Buffing" ReadOnly="True" />
                                        <asp:BoundField DataField="C1_C" HeaderText="Function Test" ReadOnly="True" />
                                        <asp:BoundField DataField="C1_D" HeaderText="Grade Improvement" ReadOnly="True" />
                                        <asp:BoundField DataField="C1_E" HeaderText="Grading" ReadOnly="True" />
                                        <asp:BoundField DataField="C1_F" HeaderText="Physical Damage" ReadOnly="True" />
                                        <asp:BoundField DataField="C1_G" HeaderText="Unlocking" ReadOnly="True" />
                                        <asp:BoundField DataField="C2_Date" HeaderText="Period" ReadOnly="True" />
                                        <asp:BoundField DataField="C2_T" HeaderText="Total" ReadOnly="True" />
                                        <asp:BoundField DataField="C2_A" HeaderText="Activation" ReadOnly="True" />
                                        <asp:BoundField DataField="C2_B" HeaderText="Buffing" ReadOnly="True" />
                                        <asp:BoundField DataField="C2_C" HeaderText="Function Test" ReadOnly="True" />
                                        <asp:BoundField DataField="C2_D" HeaderText="Grade Improvement" ReadOnly="True" />
                                        <asp:BoundField DataField="C2_E" HeaderText="Grading" ReadOnly="True" />
                                        <asp:BoundField DataField="C2_F" HeaderText="Physical Damage" ReadOnly="True" />
                                        <asp:BoundField DataField="C2_G" HeaderText="Unlocking" ReadOnly="True" />
                                        <asp:BoundField DataField="C_GT" HeaderText="Grand Total" ReadOnly="True" />--%>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </asp:Panel>
                        <asp:Panel ID="Panel6" runat="server" Visible="false">
                            <div class="col">
                                <asp:TextBox ID="txtEndDate" runat="server" />
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtEndDate" />
                            </div>
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
