<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Maint_Project.aspx.cs" Inherits="BW_WebApp.Maintenance.Maint_Project" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="cMC" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <asp:Panel ID="pnlMainView" runat="server">
                <asp:Panel ID="Panel3" runat="server">
                    <h1><asp:Label ID="lblRecordTitle" runat="server" Text="Maintenance Project" /></h1>
                    <asp:Button ID="btnAdd" runat="server" Text="Add" onclick="btnAdd_Click" />
                    <asp:Button ID="btnEdit" runat="server" Text="Edit" Visible="False" onclick="btnEdit_Click" />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" Visible="False" onclick="btnDelete_Click" />
                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Continue with Delete?" Enabled="True" TargetControlID="btnDelete" />
                    <asp:Button ID="btnAddProcess" runat="server" Text="Process" Visible="False" />
                    <asp:TextBox ID="txtSelectChoice" runat="server"></asp:TextBox>
                </asp:Panel>

                <asp:Panel ID="pnlMainGrid" runat="server" ScrollBars="Auto">
                    <asp:GridView ID="MainGrid" CssClass="table" runat="server" AutoGenerateSelectButton="True" DataKeyNames="ProjectID" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="ProjectID" HeaderText="ID" ReadOnly="True" Visible="false" />
                            <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="True" />
                            <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True" />
                            <asp:BoundField DataField="BagTagName" HeaderText="Bagtag Name" ReadOnly="True" />
                            <asp:BoundField DataField="Gather_RMANumber" HeaderText="RMA" ReadOnly="True" />
                            <asp:BoundField DataField="GatherAuto_RMANumber" HeaderText="Auto" ReadOnly="True" />
                            <asp:BoundField DataField="Auto_RMANumber" HeaderText="RMA" ReadOnly="True" />
                            <asp:BoundField DataField="Auto_WorkOrder" HeaderText="WorkOrder" ReadOnly="True" />
                            <asp:BoundField DataField="Gather_ProjectTag" HeaderText="PTag" ReadOnly="True" />
                            <asp:BoundField DataField="GatherAuto_ProjectTag" HeaderText="Auto" ReadOnly="True" />
                            <asp:BoundField DataField="AllowProjectPassThrough" HeaderText="PP" ReadOnly="True" />
                            <asp:BoundField DataField="isMasterCarrierManufactuerLinked" HeaderText="MC" ReadOnly="True" />
                            <asp:BoundField DataField="isSecondaryProjectOverride" HeaderText="SPO" ReadOnly="True" />
                            <asp:TemplateField HeaderText="P">
                                <ItemTemplate>
                                    <asp:LinkButton ID="imgPrint" runat="server" ToolTip="Print Scancode Log">
                                        <span class="oi oi-print"></span>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </asp:Panel>

            <asp:Panel ID="pnlAdd" runat="server">
                <h1>Add Project</h1>

                <div class="row">
                  <div class="col-md">
                        <label>Name:</label>
                        <asp:TextBox ID="AddProjectName" runat="server" MaxLength="20" />

                        <label>Description:</label>
                        <asp:TextBox ID="AddProjectDescription" runat="server" MaxLength="50" />

                        <label>Status:</label>
                        <asp:DropDownList ID="drpAddStatus" runat="server" ToolTip="Question Status" />

                        <label>Bagtag Name:</label>
                        <asp:TextBox ID="AddBagTagName" runat="server" MaxLength="20" />

                        <label>Product Tag:</label>
                        <asp:TextBox ID="AddProductTag" runat="server" MaxLength="20" />
                    </div>
                  <div class="col-md align-self-md-center">
                        <asp:CheckBox ID="AddGatherRMA" CssClass="d-block" Text="Gather RMA" runat="server" />
                        <asp:CheckBox ID="AddGatherAutoRMA" CssClass="d-block" Text="Auto Assign RMA" runat="server" />
                        <asp:CheckBox ID="AddAutoRMANumber" CssClass="d-block" Text="Auto RMA Number" runat="server" />
                        <asp:CheckBox ID="AddAutoWorkOrder" CssClass="d-block" Text="Auto Work Order" runat="server" />
                        <asp:CheckBox ID="AddGatherProjectTag" CssClass="d-block" Text="Gather Project Tag" runat="server" />
                        <asp:CheckBox ID="AddGatherAutoProjectTag" CssClass="d-block" Text="Auto Assign Project Tag" runat="server" />
                        <asp:CheckBox ID="AddAllowProjectPassThrough" CssClass="d-block" Text="Allow Project Pass Through (Client Received)" runat="server" />
                        <asp:CheckBox ID="AddisSecondaryProjectOverride" CssClass="d-block" Text="Secondary Project Override" runat="server" />
                        <asp:CheckBox ID="AddisMasterCarrierManufacturerLinked" CssClass="d-block" Text="Master Carrier Manufacturer Link" runat="server" />
                    </div>
                </div>

                <asp:Button ID="AddOK" runat="server" Text="OK" OnClick="AddOK_Click" />
                <asp:Button ID="AddCancel" runat="server" Text="Cancel" OnClick="AddCancel_Click1" />
            </asp:Panel>

            <asp:Panel ID="pnlEdit" runat="server">

                <h1>Edit Project</h1>

                <div class="row">
                  <div class="col-md">
                        <label>Name:</label>
                        <asp:TextBox ID="EditProjectName" runat="server" MaxLength="20" />

                        <label>Description:</label>
                        <asp:TextBox ID="EditProjectDescription" runat="server" MaxLength="50" />

                        <label>Status:</label>
                        <asp:DropDownList ID="drpEditStatus" runat="server" ToolTip="Question Status" />

                        <label>Bagtag Name:</label>
                        <asp:TextBox ID="EditBagTagName" runat="server" MaxLength="20" />

                        <label>Product Tag:</label>
                        <asp:TextBox ID="EditProductTag" runat="server" MaxLength="20" />
                    </div>
                  <div class="col-md align-self-md-center">
                        <asp:CheckBox ID="EditGatherRMA" CssClass="d-block" Text="Gather RMA" runat="server" />
                        <asp:CheckBox ID="EditGatherAutoRMA" CssClass="d-block" Text="Auto Assign RMA" runat="server" />
                        <asp:CheckBox ID="EditAutoRMANumber" CssClass="d-block" Text="Auto RMA Number" runat="server" />
                        <asp:CheckBox ID="EditAutoWorkOrder" CssClass="d-block" Text="Auto Work Order" runat="server" />
                        <asp:CheckBox ID="EditGatherProjectTag" CssClass="d-block" Text="Gather Project Tag" runat="server" />
                        <asp:CheckBox ID="EditGatherAutoProjectTag" CssClass="d-block" Text="Auto Assign Project Tag" runat="server" />
                        <asp:CheckBox ID="EditAllowProjectPassThrough" CssClass="d-block" Text="Allow Project Pass Through (Client Received)" runat="server" />
                        <asp:CheckBox ID="EditisSecondaryProjectOverride" CssClass="d-block" Text="Secondary Project Override" runat="server" />
                        <asp:CheckBox ID="EditisMasterCarrierManufacturerLinked" CssClass="d-block" Text="Master Carrier Manufacturer Link" runat="server" />
                    </div>
                </div>

                <asp:Button ID="EditOK" runat="server" Text="OK" OnClick="EditOK_Click" />
                <asp:Button ID="EditCancel" runat="server" Text="Cancel" OnClick="EditCancel_Click" />
            </asp:Panel>

            <asp:Panel ID="pnlProjectProcess" runat="server">
                <h1>Process List</h1>
                <h5><asp:Label ID="lblProcessList" runat="server" Text="Process" /></h5>

                <div class="row">
                  <div class="col">
                        <label>Source:</label>
                        <asp:ListBox ID="lstProcessSource" runat="server" SelectionMode="Multiple" ViewStateMode="Inherit" ClientIDMode="Static" />
                    </div>
                  <div class="col-auto align-self-end">
                        <asp:Button ID="btnRight" CssClass="w-100" runat="server" Text=">" OnClientClick="MoveItem('lstProcessSource','lstProcessTarget'); return false;" />
                        <asp:Button ID="btnLeft" CssClass="w-100" runat="server" Text="<" OnClientClick="MoveItem('lstProcessTarget','lstProcessSource'); return false;" />
                    </div>
                  <div class="col">
                        <label>Target:</label>
                        <asp:ListBox ID="lstProcessTarget" runat="server" ClientIDMode="Static" SelectionMode="Multiple" />
                    </div>
                </div>

                <asp:Button ID="btnSave" runat="server" Text="OK" onclientclick="GatherKeys('lstProcessTarget','HiddenProcessIDs');" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                <asp:HiddenField ID="HiddenProcessIDs" runat="server" ClientIDMode="Static" />
            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="js" runat="server">
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

        function PrintScanCodes(ProjectID, IDName) {
            //            var win = window.open("ViewDoc.aspx", "_blank", "status=no,toolbar=no,menubar=no,location=no,titlebar=no,width=600px,height=540px", true);
            var xDataList = {};
            xDataList["Table"] = "Project";
            xDataList["ID"] = ProjectID;
            xDataList["PROJECT"] = IDName;
            var pstring = GetParameterStream(xDataList);
            var WindowToOpen = "/Reports/RPT_ProjectScanCodeList.aspx";
            if (pstring.length > 0) {
                WindowToOpen = WindowToOpen + "?" + pstring
            }
            //            var win = window.open(WindowToOpen, "_blank", "width=100,height=50,menubar", true);
            var win = window.open(WindowToOpen, "_blank", "", true);
            //var win = window.open(WindowToOpen, "_blank", "menubar", true);
            // win.focus();
        }

        function GetParameterStream(ParmameterList) {
            var count = 0;
            var sb = new Sys.StringBuilder();
            for (var property in ParmameterList) {
                if (count > 0) { sb.append("&"); }
                sb.append(property + "=" + ParmameterList[property]);
                count += 1;
            }
            return sb.toString();
        }

    </script>
</asp:Content>

