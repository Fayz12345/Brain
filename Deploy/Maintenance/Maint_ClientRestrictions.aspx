<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Maint_ClientRestrictions.aspx.cs" Inherits="BW_WebApp.Maintenance.Maint_ClientRestrictions" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cMC" runat="server">
     <asp:HiddenField ID="hdnClientID" runat="server" />
    <h1>
        <asp:Label ID="lblClientTest_01" runat="server" Text="Question/Answer Restrictions for:"></asp:Label>
    </h1>
    <br />
    <br />
    <asp:TabContainer runat="server" ID="tabChild" Width="950px" ActiveTabIndex="0" BorderStyle="None">
        <asp:TabPanel runat="server" ID="tabQRestrictions" Enabled="true" HeaderText="Q Restrictions">
            <ContentTemplate>
                Project:
                <asp:DropDownList ID="drpProject" runat="server" ToolTip="Project" AutoPostBack="True">
                </asp:DropDownList>
                <asp:Button ID="btnUpdateQRestriction" runat="server" Text="Update Restrictions" />
                <asp:GridView ID="grdQuestions" runat="server" DataKeyNames="QuestionID" AutoGenerateColumns="False"
                    Width="100%" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                    <SelectedRowStyle CssClass="srowstyle" />
                    <Columns>
                        <asp:BoundField DataField="QuestionID" HeaderText="ID" ReadOnly="True">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkThisQuestion" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Description" HeaderText="ScanKey" HeaderStyle-HorizontalAlign="Left">
                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel runat="server" ID="tabARestrictions" Enabled="true" HeaderText="A Restrictions">
            <ContentTemplate>
                Question:
                <asp:DropDownList ID="drpQuestion" runat="server" ToolTip="Question" AutoPostBack="True">
                </asp:DropDownList>
                <asp:Button ID="btnUpdateARestriction" runat="server" Text="Update Restrictions" />
                <asp:GridView ID="grdAnswers" runat="server" DataKeyNames="QuestionID" AutoGenerateColumns="False"
                    Width="100%" ShowHeader="False" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                    <SelectedRowStyle CssClass="srowstyle" />
                    <Columns>
                        <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="True" ItemStyle-HorizontalAlign="Right"
                            ItemStyle-VerticalAlign="Top">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Description" HeaderText="Question" ReadOnly="True" ItemStyle-HorizontalAlign="Right"
                            ItemStyle-VerticalAlign="Top">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField ItemStyle-Wrap="False" ShowHeader="False" HeaderText="C<br />O<br />L<br />C<br />O<br />L<br />">
                            <ItemTemplate>
                                <asp:CheckBoxList ID="checkAnswer" CellPadding="5" CellSpacing="5" RepeatLayout="Table"
                                    TextAlign="Right" runat="server" RepeatDirection="Horizontal" RepeatColumns="4">
                                </asp:CheckBoxList>
                                <asp:HiddenField ID="HiddenName" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
</asp:Content>

