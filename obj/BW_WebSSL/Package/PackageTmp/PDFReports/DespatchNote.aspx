<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DespatchNote.aspx.cs" Inherits="BW_WebApp.PDFReports.DespatchNote" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Custom Reporting Module</title>
    <br />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cMC" runat="server">
     <div id="IntroDiv" class="Header">Custom Report Module</div>
    <div class="spotlightbg">
        <asp:TabContainer ID="TabContainer1" runat="server" Height="100%" Width="100%" Font-Overline="False">
            <asp:TabPanel ID="tabDespatchNote" HeaderText="Despatch Note" runat="server" Visible="True">
                <contenttemplate>
                <asp:Panel ID="Panel1x" runat="server" Width="100%">
                    <table style="padding: 5px; font-size: 11px;" width="100%">
                        <tbody>
                            <tr>
                                <td>
                                <h1>
                                    <strong>Despatch Note</strong></h1>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblPacking_Slip" runat="server" Text="Packing Slip" AssociatedControlID="txtPSlip"></asp:Label>
                                    <asp:TextBox ID="txtPSlip" runat="server">AS098</asp:TextBox>
                                    <%--                                    <table>
                                        <tr>
                                            <td>
                                                Batch #:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tbBatchNumber" runat="server" ToolTip="Batch number to assign upload."></asp:TextBox><br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Client Location Scankey:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="CL_Skcankey" runat="server" ToolTip=""></asp:TextBox><br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Project to Load Data to:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="drpProjectList_New" runat="server" ToolTip="Project">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Run Threaded:
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkRunThreaded_Newa" runat="server" Visible="True" />
                                                <asp:CheckBox ID="chkCloneSeedData" runat="server" Visible="False" Enabled="False" />
                                            </td>
                                        </tr>
            

                                        <tr>
                                            <td>
                                                Force IMEI to 15 Characters Long:
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkForce15IMEI" runat="server" />
                                            </td>
                                        </tr>
                                    </table>--%>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <br />
                                    <asp:Button ID="btnDespatch" runat="server" Text="Despatch Report" />
                                </td>
                            </tr>
                             <tr>
                                <td >
                                    <br />
                                     Result<br />
                                    <asp:Label ID="lblResults" runat="server" Width="100%"></asp:Label>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </asp:Panel>
            </contenttemplate>
            </asp:TabPanel>
        </asp:TabContainer>





        <br />
    </div>


</asp:Content>
