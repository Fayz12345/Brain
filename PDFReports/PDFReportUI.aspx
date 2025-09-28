<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PDFReportUI.aspx.cs" Inherits="BW_WebApp.PDFReports.PDFReportUI" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Custom Reporting Module</title>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cMC" runat="server">
     <div id="IntroDiv" class="Header">
          <h1>Custom Report Module</h1><br />
     </div>
    <div class="spotlightbg">
        <asp:TabContainer ID="TabContainer1"  CssClass="tab-container" runat="server" Height="100%" Width="100%" Font-Overline="False">
            <asp:TabPanel ID="tabDespatchNote" HeaderText="Dispatch Note" runat="server" Visible="True" CssClass="tab-panel">
                <contenttemplate>
                    <div class="row">
                        <div class="col-md">
                            <strong>Dispatch Note</strong></h1>
                            <br />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md">
                            <asp:RadioButtonList ID="rdlReportParameter" runat="server">
                            <asp:ListItem Selected="True" Text="Packing Slip" Value="PSLIP"></asp:ListItem>
                            <asp:ListItem Selected="false" Text="Order Number" Value="ORDER"></asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:TextBox ID="txtPSlip" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md">
                            <asp:Button ID="btnDespatch" runat="server" Text="Download PDF" />
                            <asp:Button ID="btnBarcodeSample" runat="server" Text="Barcode Sample PDF" />
                            <asp:Button ID="btnDespatchExcel" runat="server" Text="Download Excel" />
                            <asp:Button ID="btnDespatchExcelAll" runat="server" Text="Download Excel All Attributes" />
                        </div>
                        <div class="col-md">
                            <asp:Button ID="btnOrderInvoice" runat="server" Text="Download Order Invoice PDF" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md">
                            <br />
                            <br />
                            <asp:Label ID="lblResults" runat="server" Width="100%"></asp:Label>
                        </div>
                    </div>
<%--                <asp:Panel ID="Panel1x" runat="server" Width="100%">
                    <table style="padding: 5px; font-size: 11px;" width="100%">
                        <tbody>
                            <tr>
                                <td>
                                <h1>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <br />
                                </td>
                            </tr>
                             <tr>
                                <td >
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </asp:Panel>--%>
            </contenttemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="TabAdminRpt" HeaderText="Admin Reports" runat="server" Visible="True" CssClass="tab-panel">
                <contenttemplate>
                    <div class="row">
                        <div class="col-md">
                            <h1>
                                <strong>System Admin Reports</strong></h1>
                            <br />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md">
                            <br />
                            <asp:Button ID="btnAnalyzePDF" runat="server" Text="System Analysis - PDF" />
                            <asp:Button ID="btnAnalyzeEXCEL" runat="server" Text="System Analysis - Excel" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md">
                            <br />
                            <br />
                            <asp:Label ID="Label2" runat="server" Width="100%"></asp:Label>
                        </div>
                    </div>

            </contenttemplate>
            </asp:TabPanel>
        </asp:TabContainer>





        <br />
    </div>


</asp:Content>
