<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IFS_InvtTranTable.aspx.cs" Inherits="BW_WebApp.Maintenance.IFS_InvtTranTable" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="cMC" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            

            <asp:Panel ID="pnlMainView" runat="server">
                <asp:HiddenField ID="hdnUserName" runat="server" ClientIDMode="Static" />

                <h1><asp:Label ID="lblRecordTitle" runat="server" Text="IFS Transaction Table" /></h1>

                <div class="row">
                	<div class="col">
                        <label>Batch:</label>
                        <asp:TextBox runat="server" ID="txtBatch" />
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtBatch" ValidChars="0123456789" />

                        <label>ESN:</label>
                        <asp:TextBox runat="server" ID="txtESN" />

                        <label>Begin:</label>
                        <asp:TextBox ID="txtBeginDate" runat="server" />
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtBeginDate" />

                        <label>End Date:</label>
                        <asp:TextBox ID="txtEndDate" runat="server" />
                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtEndDate" />
                    </div>
                	<div class="col">
                        <asp:CheckBox ID="chkMarkPulled" runat="server" Text="Tag Transaction Records as Done" />
                        <div>
                            <asp:Button ID="btnGatherData" runat="server" Text="Download IFS Upload File" Visible="True" ToolTip="Download Transaction"
                                OnClientClick='PrintReport(); return false;' />
                            <asp:CheckBox ID="chkDetail" runat="server" Text="Detail" />
                        </div>
                        <div>
                            <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Visible="True" />
                            <asp:CheckBox ID="chkReceived" runat="server" Text="Include Done Transactions" ToolTip="If unchecked, this will exclude Done Transactions" />
                        </div>
                    </div>
                </div>
                
                <asp:Panel ID="pnlMainGrid" runat="server">
                    <asp:GridView ID="MainGrid" CssClass="table" runat="server" AutoGenerateSelectButton="True" DataKeyNames="InvtTranID" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="InvtTranID" HeaderText="ID" ReadOnly="True" Visible="false" />
                            <asp:BoundField DataField="DirectiveID" HeaderText="DID" ReadOnly="True" />
                            <asp:BoundField DataField="StatusID" HeaderText="StatusID" ReadOnly="True" />
                            <asp:BoundField DataField="Directive" HeaderText="Directive" ReadOnly="True" />
                            <asp:BoundField DataField="ESN" HeaderText="ESN" ReadOnly="True" Visible="True" />
                            <asp:BoundField DataField="Version" HeaderText="VER" ReadOnly="True" Visible="True" />
                            <asp:BoundField DataField="Quantity" HeaderText="QTY" ReadOnly="True" Visible="True" />
                            <asp:BoundField DataField="IFSSite" HeaderText="Site" ReadOnly="True" Visible="True" />
                            <asp:BoundField DataField="IFSProject" HeaderText="IFS Project" ReadOnly="True" Visible="True" />
                            <asp:BoundField DataField="POVendor" HeaderText="PO Vendor" ReadOnly="True" Visible="True" />
                            <asp:BoundField DataField="PONumber" HeaderText="PO" ReadOnly="True" Visible="True" />
                            <asp:BoundField DataField="POReceiptDate" HeaderText="PO Receipt" ReadOnly="True" Visible="True" />
                            <asp:BoundField DataField="POLine" HeaderText="PO Line" ReadOnly="True" Visible="True" />
                            <asp:BoundField DataField="POCost" HeaderText="Unit Cost" ReadOnly="True" Visible="True" />
                            <asp:BoundField DataField="FromSku" HeaderText="From Sku" ReadOnly="True" />
                            <asp:BoundField DataField="FromLocation" HeaderText="FromLocation" ReadOnly="True" />
                            <asp:BoundField DataField="FromCondition" HeaderText="FromCondition" ReadOnly="True" />
                            <asp:BoundField DataField="ToSku" HeaderText="To Sku" ReadOnly="True" />
                            <asp:BoundField DataField="ToLocation" HeaderText="ToLocation" ReadOnly="True" />
                            <asp:BoundField DataField="ToCondition" HeaderText="ToCondition" ReadOnly="True" />
                            <asp:BoundField DataField="CreateSource" HeaderText="Create Source" ReadOnly="True" />
                            <asp:BoundField DataField="Process" HeaderText="Create Process" ReadOnly="True" />
                            <asp:BoundField DataField="CreatedDate" HeaderText="CreateDate" ReadOnly="True" />
                            <asp:BoundField DataField="CreateUser" HeaderText="CreateUser" ReadOnly="True" />
                             <asp:BoundField DataField="RetrievedDate" HeaderText="RetrievedDate" ReadOnly="True" />
                            <asp:BoundField DataField="RetrievedBatch" HeaderText="RetrievedBatch" ReadOnly="True" />
                        </Columns>
                    </asp:GridView>
                </asp:Panel>

            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ContentPlaceHolderID="js" runat="server">
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

        function PrintReport() {
            //           var xBatch = Batch();
            //           if (xBatch.length == 0) {
            //               alert('You must supply a batch');
            //               return;
            //           }

            var xDataList = {};
            xDataList["RPT"] = "IFSTrans";
            xDataList["ESN"] = ESN();
            xDataList["Batch"] = Batch();
            xDataList["StartDate"] = BeginDate();
            xDataList["EndDate"] = txtEndDate();
            xDataList["IncludeDone"] = IncludeDone();
            xDataList["RecordDone"] = RecordDone();
            xDataList["Detail"] = Detail();
            xDataList["USERNAME"] = UserName();
            var pstring = GetParameterStream(xDataList);
            // var WindowToOpen = "RPT_SpotCountReport.aspx";
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
            for (var property in ParmameterList) {
                if (count > 0) { sb.append("&"); }
                sb.append(property + "=" + ParmameterList[property]);
                count += 1;
            }
            return sb.toString();
        }


        // Variable Functions ----------------------------------
        function UserName() {
            return $get("<%= hdnUserName.ClientID %>").value;
        }

        function ESN() {
            return $get("<%= txtESN.ClientID %>").value;
        }

        function BeginDate() {
            var value = $get("<%= txtBeginDate.ClientID %>").value;
            return value;
        }

        function txtEndDate() {
            var value = $get("<%= txtEndDate.ClientID %>").value;
            return value;
        }

        function Batch() {
            var value = $get("<%= txtBatch.ClientID %>").value;
            return value;
        }

        function IncludeDone() {
            if ($get("<%= chkReceived.ClientID %>").checked == true) { return 'Y'; }
            else { return 'N'; }
        }

        function RecordDone() {
            if ($get("<%= chkMarkPulled.ClientID %>").checked == true) { return 'Y'; }
            else { return 'N'; }
        }

        function Detail() {
            if ($get("<%= chkDetail.ClientID %>").checked == true) { return 'Y'; }
            else { return 'N'; }
        }

    </script>
</asp:Content>

