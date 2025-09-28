<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard_FastTrack.aspx.cs" Inherits="BW_WebApp.Dashboard_FastTrack" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>
<%@ Register Assembly="Syncfusion.Grid.Grouping.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Grid.Grouping" TagPrefix="syncfusion" %>--%>

<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>
<%@ Register Assembly="Syncfusion.Tools.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89"  Namespace="Syncfusion.Web.UI.WebControls.Tools" TagPrefix="syncfusion" %>


<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="cMC" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            
            <asp:HiddenField ID="hdnLastTreeSelectKeys" runat="server" />
            <asp:HiddenField ID="hdnUserName" runat="server" />
            <asp:Panel ID="pnlMainView" runat="server">
                <asp:Panel ID="pnldrpProject" runat="server" HorizontalAlign="Left" Width="100%">
                    <asp:Label ID="lblRecordTitle" runat="server" Text="Dashboard - Fasttrack"></asp:Label>
                    <br />
                    Project:
                    <asp:DropDownList ID="drpProjectList" runat="server" ToolTip="Project" AutoPostBack="True">
                    </asp:DropDownList>
                    <br />
                </asp:Panel>
<%--                <asp:Panel ID="pnlMainGrid" runat="server" Style="overflow: auto; max-height: 500px;
                    width: auto;" HorizontalAlign="Left">--%>
                    <%--                    <syncfusion:GridGroupingControl ID="MainGrid_B" TabIndex="2" runat="server" Height="100%"
                        Width="100%" EnableCallbacks="True" DataSourceCachingMode="ViewState" StatusBarText="List Updating.."
                        BorderCollapse="Separate" ShowFocusedBorder="True" ShowGroupDropArea="False"
                        NestedTableGroupOptions-ShowFilterBarCondition="False" TopLevelGroupOptions-ShowFilterStatusMessage="False"
                        EnableAjaxPaging="False" PageSize="0" ReadOnly="True" PostBackOnRowDblClick="False"
                        ShowLoadingIndicatorOnCallback="True" ShowSearchBox="True" TableOptions-SelectionBackColor="#0CB3D3"
                        TableOptions-SelectionUnfocusedBackColor="#0CB3D3" EnsureCurrentRowVisibility="True"
                        ClientSideOnSelectionChanged="GridSelectionChangedx('oData')">
                        <TableDescriptor AllowEdit="false" AllowNew="false">
                            <Appearance>
                            </Appearance>
                                                        <VisibleColumns>
                                <syncfusion:GridVisibleColumnDescriptor Name="ID" />
                            </VisibleColumns>
                            <Columns>
                                <syncfusion:GridColumnDescriptor MappingName="ReceiveDetailID" HeaderText="ID">
                                </syncfusion:GridColumnDescriptor>
                                <syncfusion:GridColumnDescriptor MappingName="ESN" HeaderText="ESN">
                                </syncfusion:GridColumnDescriptor>
                                <syncfusion:GridColumnDescriptor MappingName="Version" HeaderText="Version">
                                </syncfusion:GridColumnDescriptor>
                                <syncfusion:GridColumnDescriptor MappingName="ProcessID" HeaderText="ProcessID">
                                </syncfusion:GridColumnDescriptor>
                                <syncfusion:GridColumnDescriptor MappingName="ProcessText" HeaderText="ProcessText">
                                </syncfusion:GridColumnDescriptor>
                                <syncfusion:GridColumnDescriptor MappingName="CreateDate" HeaderText="CreateDate">
                                </syncfusion:GridColumnDescriptor>
                                <syncfusion:GridColumnDescriptor MappingName="CreateUser" HeaderText="CreateUser">
                                </syncfusion:GridColumnDescriptor>
                                <syncfusion:GridColumnDescriptor MappingName="InProcessSeconds" HeaderText="InProcessSeconds">
                                </syncfusion:GridColumnDescriptor>
                                <syncfusion:GridColumnDescriptor MappingName="InProcessMinutes" HeaderText="InProcessMinutes">
                                </syncfusion:GridColumnDescriptor>
                                <syncfusion:GridColumnDescriptor MappingName="InProcessHours" HeaderText="InProcessHours">
                                </syncfusion:GridColumnDescriptor>
                                <syncfusion:GridColumnDescriptor MappingName="MinutesToYellow" HeaderText="MinutesToYellow">
                                </syncfusion:GridColumnDescriptor>
                                <syncfusion:GridColumnDescriptor MappingName="MinutesToRed" HeaderText="MinutesToRed">
                                </syncfusion:GridColumnDescriptor>
                                <syncfusion:GridColumnDescriptor MappingName="ColourBand" HeaderText="ColourBand">
                                </syncfusion:GridColumnDescriptor>
                            </Columns>
                        </TableDescriptor>
                    </syncfusion:GridGroupingControl>--%>
                <syncfusion:TreeView ID="TreeData" runat="server" Font-Names="Trebuchet MS" Font-Size="15px"
                    OnNodeExpanded="TreeView1_NodeExpanded" CssClass="TreeView" CustomCSS="Styles/TreeStyle.css"
                    ClientSideOnContextMenu="NodeOnContextMenu(this)" EditNode="False"
                    Width="100%" Height="100%" ClientSideOnNodeSelect="NodeOnSelect(this)" Visible="true">
                    <%--Style="display: none"--%>
<%--                    <DefaultItemLookDisabled>
                        <StateDataDefault LeftImageCellCSSClass="tvImgCell" ItemRowCSSClass="tvItemRow" LeftImageCSSClass="tvImg"
                            ItemCSSClass="tvItemDisabled" CheckBoxCellCSSClass="tvCheckCell" RightImageCSSClass="tvArr"
                            TextCellCSSClass="tvTextCell" CheckBoxCSSClass="tvCheck" RightImageContainerCSSClass="tvArrCont"
                            RightImageCellCSSClass="tvArrCell" LeftImageContainerCSSClass="tvImgCont" TextContainerCSSClass="Def_TextContCss">
                        </StateDataDefault>
                    </DefaultItemLookDisabled>--%>
<%--                    <DefaultItemLook>
                        <StateDataExpanded LeftImageCellCSSClass="tvImgCell" ItemRowCSSClass="tvItemRow"
                            LeftImageCSSClass="tvImg" ItemCSSClass="tvItem" CheckBoxCellCSSClass="tvCheckCell"
                            RightImageCSSClass="tvArr" TextCellCSSClass="tvTextCell" CheckBoxCSSClass="tvCheck"
                            RightImageContainerCSSClass="tvArrCont" RightImageCellCSSClass="tvArrCell" LeftImageContainerCSSClass="tvImgCont"
                            TextContainerCSSClass="Def_TextContCss"></StateDataExpanded>
                        <StateDataActive LeftImageCellCSSClass="tvImgCell" ItemRowCSSClass="tvItemRow" LeftImageCSSClass="tvImg"
                            ItemCSSClass="tvItem" CheckBoxCellCSSClass="tvCheckCell" RightImageCSSClass="tvArr"
                            TextCellCSSClass="tvTextCell" CheckBoxCSSClass="tvCheck" RightImageContainerCSSClass="tvArrCont"
                            RightImageCellCSSClass="tvArrCell" LeftImageContainerCSSClass="tvImgCont" TextContainerCSSClass="Act_TextContCss">
                        </StateDataActive>
                        <StateDataHover LeftImageCellCSSClass="tvImgCell" ItemRowCSSClass="tvItemRow" LeftImageCSSClass="tvImg"
                            ItemCSSClass="tvItem" CheckBoxCellCSSClass="tvCheckCell" RightImageCSSClass="tvArr"
                            TextCellCSSClass="tvTextCell" CheckBoxCSSClass="tvCheck" RightImageContainerCSSClass="tvArrCont"
                            RightImageCellCSSClass="tvArrCell" LeftImageContainerCSSClass="tvImgCont" TextContainerCSSClass="Hov_TextContCss">
                        </StateDataHover>
                        <StateDataDefault LeftImageCellCSSClass="tvImgCell" ItemRowCSSClass="tvItemRow" LeftImageCSSClass="tvImg"
                            ItemCSSClass="tvItem" CheckBoxCellCSSClass="tvCheckCell" RightImageCSSClass="tvArr"
                            TextCellCSSClass="tvTextCell" CheckBoxCSSClass="tvCheck" RightImageContainerCSSClass="tvArrCont"
                            RightImageCellCSSClass="tvArrCell" LeftImageContainerCSSClass="tvImgCont" TextContainerCSSClass="Def_TextContCss">
                        </StateDataDefault>
                    </DefaultItemLook>--%>
                    <Items>
                    </Items>
                    <ItemLooks>
                        <syncfusion:TreeViewItemLook ID="LookRed">
                            <StateDataDefault ItemCSSClass="DefR_TextContCss" TextCellCSSClass="DefR_TextCellCss">
                            </StateDataDefault>
                            <StateDataHover TextContainerCSSClass="HovR_TextContCss"></StateDataHover>
                            <StateDataActive ItemCSSClass="ActR_TextContCss" ItemRowCSSClass="ActR_TextContCss" TextCellCSSClass="tvTextCont_R"
                                TextContainerCSSClass="ActR_TextContCss"></StateDataActive>
                            <StateDataExpanded ItemCSSClass="tvItem" TextCellCSSClass="tvTextCont_R">
                            </StateDataExpanded>
                        </syncfusion:TreeViewItemLook>
                        <syncfusion:TreeViewItemLook ID="LookYellow">
                            <StateDataDefault ItemCSSClass="tvItem" TextCellCSSClass="DefY_TextCellCss">
                            </StateDataDefault>
                            <StateDataHover TextContainerCSSClass="HovY_TextContCss"></StateDataHover>
                            <StateDataActive ItemCSSClass="tvItem" ItemRowCSSClass="tvItemRow" TextCellCSSClass="tvTextCont_Y"
                                TextContainerCSSClass="DefY_TextContCss"></StateDataActive>
                            <StateDataExpanded ItemCSSClass="tvItem" TextCellCSSClass="tvTextCont_Y">
                            </StateDataExpanded>
                        </syncfusion:TreeViewItemLook>
                        <syncfusion:TreeViewItemLook ID="LookGood">
                            <StateDataDefault ItemCSSClass="tvTextCont_R" TextCellCSSClass="tvTextCont_R">
                            </StateDataDefault>
                            <StateDataHover TextContainerCSSClass="tvTextCont_R"></StateDataHover>
                            <StateDataActive ItemCSSClass="tvTextCont_R" ItemRowCSSClass="tvTextCont_R" TextCellCSSClass="tvTextCont_R"
                                TextContainerCSSClass="tvTextCont_R"></StateDataActive>
                            <StateDataExpanded ItemCSSClass="tvTextCont_R" TextCellCSSClass="tvTextCont_R">
                            </StateDataExpanded>
                        </syncfusion:TreeViewItemLook>

                    </ItemLooks>
                </syncfusion:TreeView>
<%--                </asp:Panel>--%>





<%--<syncfusion:TreeView ID="TreeView1" runat="server" BorderColor="Gray" BorderStyle="Solid"
                            BorderWidth="1px" ImageBaseURL="images/HelpSystem" DataSourceID="XmlDataSource"
                            Height="320px" Width="240px">
                            <DataBindings>
                                <syncfusion:TreeViewItemBinding DataMember="TreeViewNode" TextField="Text" LookField="Look"
                                    ExpandedField="Expanded" ImagePathField="ImagePath" />
                            </DataBindings>
                            <ItemLooks>
                                <syncfusion:TreeViewItemLook ID="Barbones">
                                    <StateDataDefault TextContainerCSSClass="Def_TextContCss"></StateDataDefault>
                                    <StateDataActive TextContainerCSSClass="Act_TextContCss "></StateDataActive>
                                    <StateDataExpanded TextContainerCSSClass="Def_TextContCss"></StateDataExpanded>
                                    <StateDataHover TextContainerCSSClass="Hov_TextContCss"></StateDataHover>
                                </syncfusion:TreeViewItemLook>
                                <syncfusion:TreeViewItemLook ID="HelpSystem">
                                    <StateDataExpanded LeftImageURL="root.gif" TextContainerCSSClass="Def_TextContCss">
                                    </StateDataExpanded>
                                    <StateDataDefault TextContainerCSSClass="Def_TextContCss"></StateDataDefault>
                                    <StateDataActive TextContainerCSSClass="Act_TextContCss"></StateDataActive>
                                    <StateDataHover TextContainerCSSClass="Hov_TextContCss"></StateDataHover>
                                </syncfusion:TreeViewItemLook>
                                <syncfusion:TreeViewItemLook ID="MacOS9">
                                    <StateDataHover ItemRowCSSClass="Hov_Row_ItemCss" />
                                    <StateDataDefault ItemRowCSSClass="Def_Row_ItemCss" />
                                    <StateDataExpanded ItemRowCSSClass="Def_Row_ItemCss" />
                                    <StateDataActive ItemRowCSSClass="Act_Row_ItemCss" />
                                </syncfusion:TreeViewItemLook>
                                <syncfusion:TreeViewItemLook ID="VSNetBrowser">
                                    <StateDataDefault TextContainerCSSClass="Def_TextContCss"></StateDataDefault>
                                    <StateDataActive TextContainerCSSClass="Act_TextContCss"></StateDataActive>
                                    <StateDataExpanded TextContainerCSSClass="Def_TextContCss"></StateDataExpanded>
                                    <StateDataHover TextContainerCSSClass="Hov_TextContCss"></StateDataHover>
                                </syncfusion:TreeViewItemLook>
                                <syncfusion:TreeViewItemLook ID="windows2000">
                                    <StateDataExpanded LeftImageURL="folder_open.gif" TextContainerCSSClass="Def_TextContCss">
                                    </StateDataExpanded>
                                    <StateDataActive TextContainerCSSClass="Act_TextContCss"></StateDataActive>
                                    <StateDataHover TextContainerCSSClass="Hov_TextContCss"></StateDataHover>
                                    <StateDataDefault TextContainerCSSClass="Def_TextContCss"></StateDataDefault>
                                </syncfusion:TreeViewItemLook>
                                <syncfusion:TreeViewItemLook ID="windowslonghorn">
                                    <StateDataDefault TextContainerCSSClass="Node_TextCellCss"></StateDataDefault>
                                    <StateDataExpanded LeftImageURL="folder_open.gif" TextContainerCSSClass="Node_TextCellCss">
                                    </StateDataExpanded>
                                    <StateDataActive TextContainerCSSClass="Node_ActTextCellCss"></StateDataActive>
                                    <StateDataHover TextContainerCSSClass="Node_HovTextCellCss"></StateDataHover>
                                </syncfusion:TreeViewItemLook>
                                <syncfusion:TreeViewItemLook ID="winxpLook1">
                                    <StateDataDefault TextContainerCSSClass="Node_TextCellCss"></StateDataDefault>
                                    <StateDataExpanded LeftImageURL="folder_open.gif" TextContainerCSSClass="Node_TextCellCss">
                                    </StateDataExpanded>
                                    <StateDataActive TextContainerCSSClass="Node_ActTextCellCss"></StateDataActive>
                                    <StateDataHover TextContainerCSSClass="Node_HovTextCellCss"></StateDataHover>
                                </syncfusion:TreeViewItemLook>
                                <syncfusion:TreeViewItemLook ID="winxpLook2">
                                    <StateDataExpanded LeftImageURL="folder_open.gif"></StateDataExpanded>
                                </syncfusion:TreeViewItemLook>
                            </ItemLooks>
                            <DefaultItemLook>
                                <StateDataDefault LeftImageCSSClass="tvImg" ItemCSSClass="tvItem" RightImageCellCSSClass="tvArrCell"
                                    TextCellCSSClass="tvTextCell" LeftImageCellCSSClass="tvImgCell" LeftImageContainerCSSClass="tvImgCont"
                                    CheckBoxCellCSSClass="tvCheckCell" RightImageCSSClass="tvArr" TextContainerCSSClass="Def_TextContCss"
                                    CheckBoxCSSClass="tvCheck" RightImageContainerCSSClass="tvArrCont" ItemRowCSSClass="tvItemRow">
                                </StateDataDefault>
                                <StateDataExpanded LeftImageCSSClass="tvImg" ItemCSSClass="tvItem" RightImageCellCSSClass="tvArrCell"
                                    TextCellCSSClass="tvTextCell" LeftImageCellCSSClass="tvImgCell" LeftImageContainerCSSClass="tvImgCont"
                                    CheckBoxCellCSSClass="tvCheckCell" RightImageCSSClass="tvArr" TextContainerCSSClass="Def_TextContCss"
                                    CheckBoxCSSClass="tvCheck" RightImageContainerCSSClass="tvArrCont" ItemRowCSSClass="tvItemRow">
                                </StateDataExpanded>
                                <StateDataActive LeftImageCSSClass="tvImg" ItemCSSClass="tvItem" RightImageCellCSSClass="tvArrCell"
                                    TextCellCSSClass="tvTextCont" LeftImageCellCSSClass="tvImgCell" LeftImageContainerCSSClass="tvImgCont"
                                    CheckBoxCellCSSClass="tvCheckCell" RightImageCSSClass="tvArr" TextContainerCSSClass="Act_TextContCss"
                                    CheckBoxCSSClass="tvCheck" RightImageContainerCSSClass="tvArrCont" ItemRowCSSClass="tvItemRow">
                                </StateDataActive>
                                <StateDataHover LeftImageCSSClass="tvImg" ItemCSSClass="tvItem" RightImageCellCSSClass="tvArrCell"
                                    TextCellCSSClass="tvTextCell" LeftImageCellCSSClass="tvImgCell" LeftImageContainerCSSClass="tvImgCont"
                                    CheckBoxCellCSSClass="tvCheckCell" RightImageCSSClass="tvArr" TextContainerCSSClass="Hov_TextContCss"
                                    CheckBoxCSSClass="tvCheck" RightImageContainerCSSClass="tvArrCont" ItemRowCSSClass="tvItemRow">
                                </StateDataHover>
                            </DefaultItemLook>
                        </syncfusion:TreeView>
--%>


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

	    function GridSelectionChangedx() {
	        //alert("selected");
	        //alert(oData.Row.GetValue("oData.ProcessID"));
	    }
	    function GridSelectionChanged(oData) {
	        //alert("selected b");
	        //alert(oData.Row.GetValue("oData.ProcessID"));
	    }

	    function NodeOnSelect(o) {
	        MCL('TREEKEYS').value = o.Value;
	    }

	    function btnClick(o, data) {
	        alert(data);
	    }


	    function MCL(ControlName) {
	        switch (ControlName.toUpperCase()) {
	            case "TREEKEYS": return $get("<%= hdnLastTreeSelectKeys.ClientID %>"); break;
	            default: return null;
	        }
	    }


    </script>






</asp:Content>




