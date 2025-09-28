<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Maint_Option_Dependencies.aspx.cs" Inherits="BW_WebApp.Maintenance.Maint_Option_Dependencies" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="cMC" runat="server">
   <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <asp:Panel ID="pnlMainView" runat="server">
                <h1><asp:Label ID="lblRecordTitle" runat="server" Text="Maintenance Answer Dependencies"/></h1>
                <div class="form-row">
                	<div class="col-sm">
                        <label>Source Question:</label>
                        <asp:DropDownList ID="drpSourceQuestion" runat="server" ToolTip="Source Question" AutoPostBack="True" />
                        <%--<asp:Label ID="lblSource" runat="server" Text="Source"></asp:Label>--%>
                    </div>
                	<div class="col-sm">
                        <label>Target Question:</label>
                        <asp:DropDownList ID="drpTargetQuestion" runat="server" ToolTip="Target Question" AutoPostBack="True" />
                    </div>
                </div>

                <asp:Button ID="btnSave" runat="server" Text="Save" />
                <%--<asp:Label ID="lblTarget" runat="server" Text="Target"></asp:Label>--%>

                <hr>
                <div class="overflow">
                    <asp:GridView ID="GridView1" CssClass="table" runat="server" DataKeyField="OptionID" AutoGenerateColumns="False" ShowHeader="False">
                        <Columns>
                            <asp:BoundField DataField="OptionText" HeaderText="" />
                            <asp:TemplateField ItemStyle-Wrap="False" ShowHeader="True" HeaderText="">
                                <ItemTemplate>
                                    <asp:CheckBoxList ID="checkAnswer" CssClass="checklist-inline" RepeatLayout="Table" runat="server" RepeatDirection="Horizontal" RepeatColumns="4" />
                                    <asp:HiddenField ID="HiddenName" runat="server" />
                                    <hr>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

