<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReceiveDetailMessageQueue_ORG.aspx.cs" Inherits="GMPI_WebApp.ReceiveDetailMessageQueue_ORG" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="cMC" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

        

            <asp:Panel ID="pnlMainView" runat="server">
                <asp:Panel ID="Panel3" runat="server" HorizontalAlign="Left" Width="100%">
                    <br />

                    <asp:HiddenField ID="hdnESNtoAdd" runat="server" />




                    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" OnClientClick="return GetESN();" />
                    <asp:Button ID="btnEdit" runat="server" Text="Edit" Visible="False" OnClick="btnEdit_Click" />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" Visible="False" OnClick="btnDelete_Click" />
                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Continue with Delete?"
                        Enabled="True" TargetControlID="btnDelete">
                    </asp:ConfirmButtonExtender>
                    <asp:Label ID="lblRecordTitle" runat="server" Text="IMEI Message Queue"></asp:Label>
                    <asp:CheckBox ID="chkIncludeClosedMessages" runat="server" Text="Include Archived"
                        ToolTip="Click here to inlcude Archived Messages." AutoPostBack="True" />
                </asp:Panel>







                <asp:Panel ID="pnlMainGrid" runat="server" Width="100%" ScrollBars="Auto"
                    HorizontalAlign="Left">
                    <asp:GridView ID="MainGrid" runat="server" Width="100%" AutoGenerateSelectButton="True"
                        DataKeyNames="ReceiveDetailESNMessageID" AutoGenerateColumns="False" CssClass="mGrid"
                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AllowSorting="true">
                        <SelectedRowStyle CssClass="srowstyle" />
                        <Columns>
                            <asp:BoundField DataField="ReceiveDetailESNMessageID" HeaderText="ID" ReadOnly="True"
                                Visible="false">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ESN" HeaderText="IMEI" HeaderStyle-HorizontalAlign="Left" SortExpression="ESN">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="StatusOpen" HeaderText="Show" HeaderStyle-HorizontalAlign="Left" SortExpression="StatusOpen">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="StatusStop" HeaderText="Locked" HeaderStyle-HorizontalAlign="Left" SortExpression="StatusStop">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CreateDate" HeaderText="Created" HeaderStyle-HorizontalAlign="Left" SortExpression="CreateDate">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CreateUser" HeaderText="Created by" HeaderStyle-HorizontalAlign="Left" SortExpression="CreateUser">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Message" HeaderText="Message" HeaderStyle-HorizontalAlign="Left">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </asp:Panel>
            <asp:Panel ID="pnlAdd" runat="server">
                <table>
                    <tr>
                        <td id="AddTemplateHeader" colspan="2" style="border-style: none none solid none;
                            border-width: thick; border-color: #F5F5F5; text-align: center; vertical-align: middle;">
                            <h1>
                                Add IMEI Message</h1>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; vertical-align: top;">
                            ESN:
                        </td>
                        <td>
                            <asp:TextBox ID="AddESN" runat="server"></asp:TextBox><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; vertical-align: top;">
                            Show:
                        </td>
                        <td>
                            <asp:CheckBox ID="AddStatusOpen" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; vertical-align: top;">
                            Locked:
                        </td>
                        <td>
                            <asp:CheckBox ID="AddStatusStop" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; vertical-align: top;">
                            Message:
                        </td>
                        <td>
                            <asp:TextBox ID="AddMessage" runat="server" MaxLength="250" TextMode="MultiLine"
                                Rows="5"></asp:TextBox><br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="border-style: solid none none none; border-width: thick; border-color: #F5F5F5;
                            text-align: left; vertical-align: middle;">
                            <asp:Button ID="AddOK" runat="server" Text="OK" OnClick="AddOK_Click" />
                            <asp:Button ID="AddCancel" runat="server" Text="Cancel" OnClick="AddCancel_Click1" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlEdit" runat="server">
                <table>
                    <tr>
                        <td id="Td1" colspan="2" style="border-style: none none solid none; border-width: thick;
                            border-color: #F5F5F5; text-align: center; vertical-align: middle;">
                            <h1>
                                Edit IMEI Message</h1>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; vertical-align: top;">
                            ESN:
                        </td>
                        <td>
                            <asp:TextBox ID="EditESN" runat="server"></asp:TextBox><br />
                            <asp:TextBox ID="EditKeyID" runat="server" ReadOnly="True" Visible="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; vertical-align: top;">
                            Show:
                        </td>
                        <td>
                            <asp:CheckBox ID="EditStatusOpen" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; vertical-align: top;">
                            Locked:
                        </td>
                        <td>
                            <asp:CheckBox ID="EditStatusStop" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; vertical-align: top;">
                            Message:
                        </td>
                        <td>
                            <asp:TextBox ID="EditMessage" runat="server" MaxLength="250" TextMode="MultiLine"
                                Rows="5"></asp:TextBox><br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="border-style: solid none none none; border-width: thick; border-color: #F5F5F5;
                            text-align: left; vertical-align: middle;">
                            <asp:Button ID="EditOK" runat="server" Text="OK" OnClick="EditOK_Click" />
                            <asp:Button ID="EditCancel" runat="server" Text="Cancel" OnClick="EditCancel_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>





    <script type="text/javascript">

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

        function BeginRequestHandler(sender, args) {
            if (args._postBackElement.id != "SkinTable1") {
                //                ConfigureWaitingPopup(Popup);
                $('#loading').show();
            }
        }
        function EndRequestHandler(sender, args) {

            $('#loading').hide();
        }





        //    function PrintReportView() {
        ////             $find('= rvBagTag.ClientID ').invokePrintDialog();
        //    }

        function GetESN() {
            $get("<%= hdnESNtoAdd.ClientID %>").value = prompt("ESN to add message to:", "");
            return true;
        }


</script>







</asp:Content>

