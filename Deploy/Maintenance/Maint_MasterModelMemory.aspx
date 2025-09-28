<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Maint_MasterModelMemory.aspx.cs" Inherits="BW_WebApp.Maintenance.Maint_MasterModelMemory" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>

<asp:Content ContentPlaceHolderID="cMC" runat="server"> 
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            

            <asp:Panel ID="pnlMainView" CssClass="w-sm-50" runat="server">
                <label>Model:</label>
                <asp:HiddenField ID="hdnModelID" runat="server" />
                <asp:DropDownList ID="drpModel" runat="server" ToolTip="Model To assign Memory" AutoPostBack="True" />
            </asp:Panel>

            <asp:Panel ID="pnlProjectProcess" runat="server">
                <h1><asp:Label ID="lblMemoryList" runat="server" Text="Memory List" /></h1>
                <div class="row">
                	<div class="col">
                        <label>Source:</label>
                        <asp:ListBox ID="lstMemorySource" runat="server" SelectionMode="Multiple" ViewStateMode="Inherit" ClientIDMode="Static" />
                    </div>
                	<div class="col-auto align-self-end">
                        <asp:Button ID="btnRight" CssClass="w-100" runat="server" Text=">" OnClientClick="MoveItem('lstMemorySource','lstMemoryTarget');return false;" />
                        <asp:Button ID="btnLeft" CssClass="w-100" runat="server" Text="<" OnClientClick="MoveItem('lstMemoryTarget','lstMemorySource');return false;" />
                    </div>
                	<div class="col">
                        <label>Target:</label>
                        <asp:ListBox ID="lstMemoryTarget" runat="server" ClientIDMode="Static" SelectionMode="Multiple" />
                    </div>
                </div>
                
                <asp:Button ID="btnSave" runat="server" Text="Save" onclientclick="GatherKeys('lstMemoryTarget','HiddenProcessIDs');" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                <asp:HiddenField ID="HiddenProcessIDs" runat="server" ClientIDMode="Static" />
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

        //       function PrintScanCodes(ModelID, IDName) {
        //           //            var win = window.open("ViewDoc.aspx", "_blank", "status=no,toolbar=no,menubar=no,location=no,titlebar=no,width=600px,height=540px", true);
        //           var xDataList = {};
        //           xDataList["Table"] = "Memory";
        //           xDataList["ID"] = ModelID;
        //           xDataList["MODEL"] = IDName;
        //           var pstring = GetParameterStream(xDataList);
        //           var WindowToOpen = "/Reports/RPT_ProjectScanCodeList.aspx";
        //           if (pstring.length > 0) {
        //               WindowToOpen = WindowToOpen + "?" + pstring
        //           }
        //           //            var win = window.open(WindowToOpen, "_blank", "width=100,height=50,menubar", true);
        //           var win = window.open(WindowToOpen, "_blank", "", true);
        //           //var win = window.open(WindowToOpen, "_blank", "menubar", true);
        //           // win.focus();
        //       }

        //       function GetParameterStream(ParmameterList) {
        //           var count = 0;
        //           var sb = new Sys.StringBuilder();
        //           for (var property in ParmameterList) {
        //               if (count > 0) { sb.append("&"); }
        //               sb.append(property + "=" + ParmameterList[property]);
        //               count += 1;
        //           }
        //           return sb.toString();
        //       }

</script>
</asp:Content>


