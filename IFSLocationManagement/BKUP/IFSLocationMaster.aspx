<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IFSLocationMaster.aspx.cs" Inherits="GMPI_WebApp.IFSLocationManagement.IFSLocationMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>
<%@ Register Assembly="Syncfusion.Tools.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89"  Namespace="Syncfusion.Web.UI.WebControls.Tools" TagPrefix="syncfusion" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="cMC" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            
            <asp:Panel ID="pnlMainView" runat="server">
                <asp:Table ID="xxx" runat="server">
                    <asp:TableRow runat="server">
                        <asp:TableCell Wrap="False">
                            <asp:Label ID="Label1" runat="server" Text="Search IFS Location:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtSEG1" runat="server" MaxLength="3" Width="35px" TabIndex="1"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:CheckBox ID="chktxtSEG1" runat="server" Checked="True" />
                        </asp:TableCell>
                        <asp:TableCell>
                                        -
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtSEG2" runat="server" MaxLength="3" Width="35px" TabIndex="2"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:CheckBox ID="chktxtSEG2" runat="server" Checked="True" />
                        </asp:TableCell>
                        <asp:TableCell>
                            -
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtSEG3" runat="server" MaxLength="3" Width="35px" TabIndex="3"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:CheckBox ID="chktxtSEG3" runat="server" Checked="True" />
                        </asp:TableCell>
                        <asp:TableCell>
                            -
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtSEG4" runat="server" MaxLength="3" Width="35px" TabIndex="4"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:CheckBox ID="chktxtSEG4" runat="server" Checked="True" />
                        </asp:TableCell>
                        <asp:TableCell>
                            -
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Button ID="btnRefresh" runat="server" Text="Refresh" TabIndex="4" />
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Button ID="btnViewDetail" runat="server" Text="Detail" TabIndex="5" Visible="False" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <asp:Label ID="DetailMessage" runat="server" Text="" ForeColor="#CC0000"></asp:Label>
                <asp:Panel ID="pndDetail" runat="server" Height="540px" Width="100%" ScrollBars="Auto"
                    HorizontalAlign="Left" Visible="False">
                    <br />
                    <%--<asp:Button ID="btnCloseDetail" runat="server" Text="Close" />--%>
                    <asp:TabContainer ID="TabContainer1" runat="server">
                        <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="Devices" Visible="False">
                            <ContentTemplate>
                                <asp:ImageButton ID="imgDownloadGrid" runat="server" HeaderText="" ImageUrl="~/Images/arrow_down.png"
                                    ToolTip="Download" Width="25px"></asp:ImageButton>
                                <asp:GridView ID="grdViewDevices" runat="server" Width="100%" AutoGenerateSelectButton="True"
                                    DataKeyNames="ReceiveDetailID" AutoGenerateColumns="False" CssClass="mGrid"
                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                    <SelectedRowStyle CssClass="srowstyle" />
                                    <Columns>
                                        <asp:BoundField DataField="ESN" HeaderText="ESN" ReadOnly="True" Visible="false">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Version" HeaderText="Version" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ProjectName" HeaderText="ProjectName" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IFSLocation" HeaderText="IFSLocation" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SKU" HeaderText="SKU" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IFSCondition" HeaderText="IFSCondition" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:TabPanel>
                        <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="Parts" Visible="False">
                            <ContentTemplate>
                                <asp:ImageButton ID="ImageButton1" runat="server" HeaderText="" ImageUrl="~/Images/arrow_down.png"
                                    ToolTip="Download" Width="25px"></asp:ImageButton>
                                <asp:GridView ID="grdViewParts" runat="server" Width="100%" AutoGenerateSelectButton="True"
                                    DataKeyNames="MasterIFSLocationID" AutoGenerateColumns="False" CssClass="mGrid"
                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                    <SelectedRowStyle CssClass="srowstyle" />
                                    <Columns>
                                        <asp:BoundField DataField="QTY" HeaderText="QTY" ReadOnly="True" Visible="false">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IFSLocation" HeaderText="IFSLocation" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IsWip" HeaderText="IsWip" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Warehouse" HeaderText="Warehouse" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="GMPPartNumber" HeaderText="GMPPartNumber" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="GMPPartDescription" HeaderText="GMPPartDescription" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IsFrozen" HeaderText="IsFrozen" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CompanyName" HeaderText="CompanyName" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IFSLocationALT" HeaderText="IFSLocationALT" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:TabPanel>
                    </asp:TabContainer>
                </asp:Panel>
                <asp:Panel ID="pnlMasterList" runat="server" HorizontalAlign="Left" Width="100%">
                    <br />
                    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
                    <asp:Button ID="btnEdit" runat="server" Text="Edit" Visible="False" OnClick="btnEdit_Click" />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" Visible="False" OnClick="btnDelete_Click" />
                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Continue with Delete?"
                        Enabled="True" TargetControlID="btnDelete">
                    </asp:ConfirmButtonExtender>
                    <asp:Label ID="lblRecordTitle" runat="server" Text="IFS Location Master table"></asp:Label>
                    <asp:Panel ID="pnlMainGrid" runat="server" Height="540px" Width="100%" ScrollBars="Auto"
                        HorizontalAlign="Left">
                        <asp:GridView ID="MainGrid" runat="server" Width="100%" AutoGenerateSelectButton="True"
                            DataKeyNames="MasterIFSLocationID" AutoGenerateColumns="False" CssClass="mGrid"
                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                            <SelectedRowStyle CssClass="srowstyle" />
                            <Columns>
                                <asp:BoundField DataField="MasterIFSLocationID" HeaderText="ID" ReadOnly="True" Visible="false">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>


                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgFilterOnLocation" runat="server" HeaderText="" ImageUrl="~/Images/details_open.png"
                                                ToolTip="Move Location Segments into search." Width="15px"></asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgViewDevices" runat="server" HeaderText="" ImageUrl="~/Images/Files.png"
                                                ToolTip="View Devices in Location." Width="15px"></asp:ImageButton>
                                            <asp:ImageButton ID="imgDownloadDevices" runat="server" HeaderText="" ImageUrl="~/Images/arrow_down.png"
                                                ToolTip="Download Devices" Width="15px"></asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgViewParts" runat="server" HeaderText="" ImageUrl="~/Images/Files.png"
                                                ToolTip="View Parts in this location." Width="15px"></asp:ImageButton>
                                            <asp:ImageButton ID="imgDownloadParts" runat="server" HeaderText="" ImageUrl="~/Images/arrow_down.png"
                                                ToolTip="Download Parts" Width="15px"></asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

<%--                                <asp:BoundField DataField="Purpose" HeaderText="Purpose" HeaderStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:BoundField>--%>
                                <asp:BoundField DataField="IsWip" HeaderText="WIP" HeaderStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="IFSLocation" HeaderText="IFS Location" HeaderStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Description" HeaderText="Description" HeaderStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DeviceRollup" HeaderText="Device Rollup" HeaderStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PartRollup" HeaderText="Part Rollup" HeaderStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PickLevel" HeaderText="Pick Level" HeaderStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="IsFrozen" HeaderText="is Frozen" HeaderStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="IFSLocationALT" HeaderText="Alt Location" HeaderStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </asp:Panel>
            </asp:Panel>
            <asp:Panel ID="pnlAdd" runat="server">
                <table>
                    <tr>
                        <td id="AddTemplateHeader" colspan="2" style="border-style: none none solid none;
                            border-width: thick; border-color: #F5F5F5; text-align: center; vertical-align: middle;">
                            <h1>
                                Add IFS Location</h1>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; vertical-align: top;">
                            Purpose:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpAddPurpose" runat="server" ToolTip="Question Status" BackColor="#FFFF66">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; vertical-align: top;">
                            Status:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpAddStatus" runat="server" ToolTip="Question Status" BackColor="#FFFF66">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; vertical-align: top;">
                            Is WIP:
                        </td>
                        <td>
                            <asp:CheckBox ID="AddisWhip" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; vertical-align: top;">
                            IFS Location:
                        </td>
                        <td>
                            <asp:TextBox ID="AddIFSLocation" runat="server" BackColor="#FFFF66" MaxLength="50"></asp:TextBox><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; vertical-align: top;">
                            Description:
                        </td>
                        <td>
                            <asp:TextBox ID="AddDescription" runat="server" BackColor="#FFFF66" MaxLength="100"></asp:TextBox><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; vertical-align: top;">
                            Device Rollup:
                        </td>
                        <td>
                            <asp:TextBox ID="AddDeviceRollup" runat="server" BackColor="#FFFF66" MaxLength="50"></asp:TextBox><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; vertical-align: top;">
                            Part Rollup:
                        </td>
                        <td>
                            <asp:TextBox ID="AddPartRollup" runat="server" BackColor="#FFFF66" MaxLength="50"></asp:TextBox><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; vertical-align: top;">
                            Pick Level:
                        </td>
                        <td>
                            <asp:TextBox ID="AddPickLevel" runat="server" BackColor="#FFFF66" MaxLength="10"></asp:TextBox><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; vertical-align: top;">
                            Is Frozen:
                        </td>
                        <td>
                            <asp:CheckBox ID="AddIsFrozen" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; vertical-align: top;">
                            IFS Alternate Location:
                        </td>
                        <td>
                            <asp:TextBox ID="AddAltIFSLocation" runat="server" BackColor="#FFFF66" MaxLength="50"></asp:TextBox><br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="border-style: solid none none none; border-width: thick; border-color: #F5F5F5;
                            text-align: left; vertical-align: middle;">
                            <asp:Button ID="AddOK" runat="server" Text="OK" OnClick="AddOK_Click" />
                            <asp:Button ID="AddCancel" runat="server" Text="Cancel" OnClick="AddCancel_Click1" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="border-style: solid none none none; border-width: thick; border-color: #F5F5F5;
                            text-align: left; vertical-align: middle;">
                            <asp:Label ID="AddMessage" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlEdit" runat="server">
                <asp:HiddenField ID="hdnEditKeyID" runat="server" />
                <table>
                    <tr>
                        <td id="Td1" colspan="2" style="border-style: none none solid none; border-width: thick;
                            border-color: #F5F5F5; text-align: center; vertical-align: middle;">
                            <h1>
                                Edit IFS Location</h1>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; vertical-align: top;">
                            Purpose:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpEditPurpose" runat="server" ToolTip="Question Status" BackColor="#FFFF66">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; vertical-align: top;">
                            Status:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpEditStatus" runat="server" ToolTip="Question Status" BackColor="#FFFF66">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; vertical-align: top;">
                            Is WIP:
                        </td>
                        <td>
                            <asp:CheckBox ID="EditisWhip" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; vertical-align: top;">
                            IFS Location:
                        </td>
                        <td>
                            <asp:TextBox ID="EditIFSLocation" runat="server" BackColor="#FFFF66" MaxLength="50"></asp:TextBox><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; vertical-align: top;">
                            Description:
                        </td>
                        <td>
                            <asp:TextBox ID="EditDescription" runat="server" BackColor="#FFFF66" MaxLength="100"></asp:TextBox><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; vertical-align: top;">
                            Device Rollup:
                        </td>
                        <td>
                            <asp:TextBox ID="EditDeviceRollup" runat="server" BackColor="#FFFF66" MaxLength="50"></asp:TextBox><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; vertical-align: top;">
                            Part Rollup:
                        </td>
                        <td>
                            <asp:TextBox ID="EditPartRollup" runat="server" BackColor="#FFFF66" MaxLength="50"></asp:TextBox><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; vertical-align: top;">
                            Pick Level:
                        </td>
                        <td>
                            <asp:TextBox ID="EditPickLevel" runat="server" BackColor="#FFFF66" MaxLength="10"></asp:TextBox><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; vertical-align: top;">
                            Is Frozen:
                        </td>
                        <td>
                            <asp:CheckBox ID="EditIsFrozen" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; vertical-align: top;">
                            IFS Alternate Location:
                        </td>
                        <td>
                            <asp:TextBox ID="EditAltIFSLocation" runat="server" BackColor="#FFFF66" MaxLength="50"></asp:TextBox><br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="border-style: solid none none none; border-width: thick; border-color: #FFCC66;
                            text-align: left; vertical-align: middle;">
                            <asp:Button ID="EditOK" runat="server" Text="OK" OnClick="EditOK_Click" />
                            <asp:Button ID="EditCancel" runat="server" Text="Cancel" OnClick="EditCancel_Click" />
                            <asp:Button ID="btnUnFreeze" runat="server" Text="Release Frozen Devices" ToolTip="This will release the Location and Move all Devices in Alt Location to unfrozen location." />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="border-style: solid none none none; border-width: thick; border-color: #F5F5F5;
                            text-align: left; vertical-align: middle;">
                            <asp:Label ID="EditMessage" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        // Setup Global Variables
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
        //------------------------------------------------------------------------------------
        function PrintReport(Report, Location) {
//            alert('xxxLocation:' + Location + ' - Report:' + Report);
            var xDataList = {};
            xDataList["RPT"] = Report;
            xDataList["KEY"] = Location;
            xDataList["USERNAME"] = '';
//            alert('1. Location:' + Location + ' - Report:' + Report);
            var pstring = GetParameterStream(xDataList);
            // var WindowToOpen = "RPT_SpotCountReport.aspx";
//            alert('2. Location:' + Location + ' - Report:' + Report);
            var WindowToOpen = "/Reports/RPT_EXCEL_Out.aspx";
            if (pstring.length > 0) {
                WindowToOpen = WindowToOpen + "?" + pstring
            }
            var win = window.open(WindowToOpen, "_blank", "menubar", true);
            return;
        }

        function GetParameterStream(ParmameterList) {
            var count = 0;
            var sb = new Sys.StringBuilder();
//            alert('xxxx');
            for (var property in ParmameterList) {
                if (count > 0) { sb.append("&"); }
                sb.append(property + "=" + ParmameterList[property]);
                count += 1;
            }
            return sb.toString();
        }
        //-------------------------------------------------------------------------------------

    </script>

</asp:Content>