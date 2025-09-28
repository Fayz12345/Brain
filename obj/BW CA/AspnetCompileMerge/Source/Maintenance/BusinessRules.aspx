<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BusinessRules.aspx.cs" Inherits="BW_WebApp.Maintenance.BusinessRules" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="cMC" runat="server">

    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
        <Scripts>
            <asp:ScriptReference Path="~/Scripts/ReceiveSpecific.js" NotifyScriptLoaded="true" />
        </Scripts>
    </asp:ScriptManagerProxy>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional"> 
        <ContentTemplate>
            

            <asp:HiddenField ID="hdnQuestionIDList" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnQuestionClientIDList" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnIsProcessReadOnly" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnCarrierID" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnManufacturerID" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnModelID" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnColourID" runat="server" ClientIDMode="Static" />

            <asp:Panel ID="pnlMainView" runat="server">
                <h1><asp:Label ID="lblRecordTitle" runat="server" Text="Business Rules" /></h1>
                <p>Current Location / Current Process Questions and Answsers</p>

                <div class="row">
                	<div class="col-md-6">
                        <%--<asp:Label runat="server" ID="Label1" Text="Client" />
                        <asp:DropDownList ID="drpClient" runat="server">
                            <asp:ListItem Text="Client 1" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Client 2" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Client 3" Value="3"></asp:ListItem>
                        </asp:DropDownList>--%>
                
                        <asp:Label runat="server" ID="Label3" Text="Project" />
                        <asp:DropDownList ID="drpProject" runat="server">
                            <asp:ListItem Text="Model 1" Value="1" />
                            <asp:ListItem Text="Model 2" Value="2" />
                            <asp:ListItem Text="Model 3" Value="3" />
                        </asp:DropDownList>
                
                        <%--<asp:Label runat="server" ID="Label2" Text="Client Location" />
                        <asp:DropDownList ID="drpClientLocation" runat="server">
                            <asp:ListItem Text="ClientLocation 1" Value="1"></asp:ListItem>
                            <asp:ListItem Text="ClientLocation 2" Value="2"></asp:ListItem>
                            <asp:ListItem Text="ClientLocation 3" Value="3"></asp:ListItem>
                        </asp:DropDownList>--%>

                        <asp:Label runat="server" ID="Label4" Text="Process" />
                        <asp:DropDownList ID="drpProcess" runat="server">
                            <asp:ListItem Text="Process 1" Value="1" />
                            <asp:ListItem Text="Process 2" Value="2" />
                            <asp:ListItem Text="Process 3" Value="3" />
                        </asp:DropDownList>
                
                        <asp:Label runat="server" ID="Label3x" Text="Model" />
                        <asp:DropDownList ID="drpModel" runat="server">
                            <asp:ListItem Text="Model 1" Value="1" />
                            <asp:ListItem Text="Model 2" Value="2" />
                            <asp:ListItem Text="Model 3" Value="3" />
                        </asp:DropDownList>
                    </div>
                </div>

                <asp:TabContainer ID="TabContainer1" CssClass="tab-container" runat="server">
                    <asp:TabPanel ID="TabConditions" CssClass="tab-panel" runat="server" HeaderText="Conditions">
                        <ContentTemplate>
                            <asp:Label runat="server" Text="Event:" />
                            <asp:RadioButtonList ID="rdlEventType" CssClass="radiolist-inline" runat="server" RepeatLayout="UnorderedList">
                                <asp:ListItem Text="Open" Value="1" />
                                <asp:ListItem Text="Open Default" Value="2" />
                                <asp:ListItem Text="Answser" Value="3" />
                                <asp:ListItem Text="Save" Value="4" />
                            </asp:RadioButtonList>
                            <asp:Button ID="btnRefresh1" runat="server" Text="Refresh" />
                            <asp:Button ID="btnHide" runat="server" Text="Hide" ToolTip="Hide Unimportant Questions" />
                            <asp:Button ID="btnUnhide" runat="server" Text="Unhide" ToolTip="Unhide Unimportant Questions" />
                            <asp:Button ID="btnCopy" runat="server" Text="Copy" />
                            <asp:Button ID="btnSave" runat="server" Text="Save" />

                            <asp:Panel ID="Rules" runat="server" ScrollBars="Auto">
                                <asp:GridView ID="grdRules" CssClass="table" runat="server" DataKeyField="QuestionID" AutoGenerateColumns="False"
                                    ShowHeader="False">
                                    <Columns>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelected" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="imgClear" runat="server" HeaderText="Clear" ToolTip="Clear Checkboxes">
                                                    <span class="oi oi-pencil"></span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="imgColapseIn" runat="server" HeaderText="Select" ToolTip="Collapse">
                                                    <span class="oi oi-collapse-left"></span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="imgColapseOut" runat="server" HeaderText="Select" ToolTip="Expand" Visible="False">
                                                    <span class="oi oi-expand-right"></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="D">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="imgDelete" runat="server" ToolTip="Delete">
                                                    <span class="oi oi-trash"></span>
                                                </asp:LinkButton>
                                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="imgDelete"
                                                    ConfirmText="Are you sure you want to delete this file?" />
                                                <asp:LinkButton ID="imgMoveProcessUp" runat="server" ToolTip="Move Process Log Up">
                                                    <span class="oi oi-arrow-circle-top"></span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="imgMoveProcessDown" runat="server" ToolTip="Move Process Log Down">
                                                    <span class="oi oi-arrow-circle-bottom"></span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="imgChangeProcess" runat="server" ToolTip="Change Process">
                                                    <span class="oi oi-wrench"></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="HiddenID" runat="server" ClientIDMode="Static" />
                                                <asp:Label runat="server" ID="Description" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Wrap="False" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="Num3Digit" runat="server" Enabled="False" MaxLength="3" />
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="Num3Digit"
                                                    ValidChars="0123456789" />
                                                <asp:TextBox ID="Text20Digit" runat="server" Enabled="False" MaxLength="20" />
                                                <asp:TextBox ID="Text3Digit" runat="server" Enabled="False" MaxLength="3" />
                                                <asp:TextBox ID="Text10Digit" runat="server" Enabled="False" MaxLength="10" />
                                                <asp:TextBox ID="Text18Digit" runat="server" Enabled="False" MaxLength="18" />
                                                <asp:TextBox ID="Text50Digit" runat="server" Enabled="False" MaxLength="50" />
                                                <asp:TextBox ID="CurrencyAnswer" runat="server" ClientIDMode="Predictable" />
                                                <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="CurrencyAnswer"
                                                    Mask="9{9}.99" MaskType="Number" InputDirection="RightToLeft" />
                                                <asp:TextBox ID="NumericAnswer" runat="server" ClientIDMode="Predictable" />
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="NumericAnswer"
                                                    ValidChars="0123456789" />
                                                <asp:TextBox ID="TextAnswer" runat="server" ClientIDMode="Predictable" />
                                                <asp:RadioButtonList ID="RadioAnswer" CssClass="radiolist-inline" runat="server" ClientIDMode="Predictable"
                                                    RepeatLayout="UnorderedList" />
                                                <asp:CheckBoxList ID="checkAnswer" CssClass="checklist-inline" runat="server" ClientIDMode="Predictable"
                                                    RepeatLayout="UnorderedList" />
                                                <asp:TextBox ID="CalAnswer" runat="server" ReadOnly="True" ClientIDMode="Predictable" />
                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="CalAnswer"
                                                    Format="MM/dd/yyyy" />
                                                <asp:DropDownList ID="drpAnswer" runat="server" ClientIDMode="Predictable" />
                                                <asp:HiddenField ID="HiddenName" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>

                            <asp:Panel ID="ConditionSegments" runat="server" ScrollBars="Auto">
                                <asp:GridView ID="grdConditionSegments" CssClass="table" runat="server" DataKeyField="QuestionID" AutoGenerateColumns="False"
                                    ShowHeader="True">
                                    <Columns>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="imgDelete" runat="server" HeaderText="Delete" ToolTip="Delete">
                                                    <span class="oi oi-trash"></span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="imgLeftBracket" runat="server" HeaderText="Left" ToolTip="Left Bracket">
                                                    <span class="oi oi-chevron-left"></span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="imgRightBracket" runat="server" HeaderText="Right" ToolTip="Right Bracket">
                                                    <span class="oi oi-chevron-right"></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="True" HeaderText="And/Or">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="HiddenID" runat="server" ClientIDMode="Static" />
                                                <asp:DropDownList ID="drpAndOr" runat="server" ToolTip="And/Or">
                                                    <asp:ListItem Text="And" Value="0" />
                                                    <asp:ListItem Text="Or" Value="0" />
                                                </asp:DropDownList>
                                                <asp:Label runat="server" ID="Description" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="True" HeaderText="Column">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="HiddenID" runat="server" ClientIDMode="Static" />
                                                <asp:TextBox ID="TxtField" runat="server" ClientIDMode="Predictable" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="True" HeaderText="Operator">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="HiddenID" runat="server" ClientIDMode="Static" />
                                                <asp:DropDownList ID="drpOperator" runat="server" ToolTip="And/Or">
                                                    <asp:ListItem Text="Is equal to" Value="0" />
                                                    <asp:ListItem Text="is not equal to" Value="0" />
                                                    <asp:ListItem Text="contains" Value="0" />
                                                    <asp:ListItem Text="is not answered" Value="0" />
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="True" HeaderText="Value">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="HiddenID" runat="server" ClientIDMode="Static" />
                                                <asp:TextBox ID="TxtValue" runat="server" ClientIDMode="Predictable" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="imgUp" runat="server" HeaderText="Up" ToolTip="Move Up">
                                                    <span class="oi oi-arrow-circle-top"></span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="imgDown" runat="server" HeaderText="Down" ToolTip="Move Down">
                                                    <span class="oi oi-arrow-circle-bottom"></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="D">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="imgDelete" runat="server" ToolTip="Delete">
                                                    <span class="oi oi-trash"></span>
                                                </asp:LinkButton>
                                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="imgDelete"
                                                    ConfirmText="Are you sure you want to delete this file?">
                                                </asp:ConfirmButtonExtender>
                                                <asp:LinkButton ID="imgMoveProcessUp" runat="server" ToolTip="Move Process Log Up">
                                                    <span class="oi oi-arrow-circle-top"></span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="imgMoveProcessDown" runat="server" ToolTip="Move Process Log Down">
                                                    <span class="oi oi-arrow-circle-bottom"></span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="imgChangeProcess" runat="server" ToolTip="Change Process">
                                                    <span class="oi oi-wrench"></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>

                            <asp:Panel ID="Questions" runat="server" Width="100%" ScrollBars="Auto">
                                <asp:GridView ID="GridView4" CssClass="table" runat="server" DataKeyField="QuestionID" AutoGenerateColumns="False"
                                    ShowHeader="False">
                                    <Columns>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelected" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="imgClear" runat="server" HeaderText="Clear" ToolTip="Clear Checkboxes">
                                                    <span class="oi oi-pencil"></span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="imgColapseIn" runat="server" HeaderText="Select" ToolTip="Collapse">
                                                    <span class="oi oi-collapse-left"></span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="imgColapseOut" runat="server" HeaderText="Select" ToolTip="Expand" Visible="False">
                                                    <span class="oi oi-expand-right"></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="D">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="imgDelete" runat="server" ToolTip="Delete">
                                                    <span class="oi oi-trash"></span>
                                                </asp:LinkButton>
                                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="imgDelete"
                                                    ConfirmText="Are you sure you want to delete this file?" />
                                                <asp:LinkButton ID="imgMoveProcessUp" runat="server" ToolTip="Move Process Log Up">
                                                    <span class="oi oi-arrow-circle-top"></span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="imgMoveProcessDown" runat="server" ToolTip="Move Process Log Down">
                                                    <span class="oi oi-arrow-circle-bottom"></span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="imgChangeProcess" runat="server" ToolTip="Change Process">
                                                    <span class="oi oi-wrench"></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="HiddenID" runat="server" ClientIDMode="Static" />
                                                <asp:Label runat="server" ID="Description" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="Num3Digit" runat="server" Enabled="False" MaxLength="3" />
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="Num3Digit"
                                                    ValidChars="0123456789" />
                                                <asp:TextBox ID="Text20Digit" runat="server" Enabled="False" MaxLength="20" />
                                                <asp:TextBox ID="Text3Digit" runat="server" Enabled="False" MaxLength="3" />
                                                <asp:TextBox ID="Text10Digit" runat="server" Enabled="False" MaxLength="10" />
                                                <asp:TextBox ID="Text18Digit" runat="server" Enabled="False" MaxLength="18" />
                                                <asp:TextBox ID="Text50Digit" runat="server" Enabled="False" MaxLength="50" />
                                                <asp:TextBox ID="CurrencyAnswer" runat="server" ClientIDMode="Predictable" />
                                                <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="CurrencyAnswer"
                                                    Mask="9{9}.99" MaskType="Number" InputDirection="RightToLeft" />
                                                <asp:TextBox ID="NumericAnswer" runat="server" ClientIDMode="Predictable" />
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="NumericAnswer"
                                                    ValidChars="0123456789" />
                                                <asp:TextBox ID="TextAnswer" runat="server" ClientIDMode="Predictable" />
                                                <asp:RadioButtonList ID="RadioAnswer" CssClass="radiolist-inline" runat="server" ClientIDMode="Predictable"
                                                    RepeatLayout="UnorderedList" />
                                                <asp:CheckBoxList ID="checkAnswer" CssClass="checklist-inline" runat="server" ClientIDMode="Predictable"
                                                    RepeatLayout="UnorderedList" />
                                                <asp:TextBox ID="CalAnswer" runat="server" ReadOnly="True" ClientIDMode="Predictable" />
                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="CalAnswer" Format="MM/dd/yyyy" />
                                                <asp:DropDownList ID="drpAnswer" runat="server" ClientIDMode="Predictable" />
                                                <asp:HiddenField ID="HiddenName" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabPanel5" CssClass="tab-panel" runat="server" HeaderText="Prior Conditions">
                        <ContentTemplate>
                            <asp:DropDownList ID="drpPriorConditionProcess" runat="server">
                                <asp:ListItem Text="Process 1" Value="1" />
                                <asp:ListItem Text="Process 2" Value="2" />
                                <asp:ListItem Text="Process 3" Value="3" />
                            </asp:DropDownList>
                            <asp:Button ID="btnHideP" runat="server" Text="Hide"  ToolTip="Hide unImportant Questions"/>
                            <asp:Button ID="btnUnhideP" runat="server" Text="UnHide"  ToolTip="UnHide unImportant Questions"/>
                            <asp:Button ID="btnRefreshP" runat="server" Text="Refresh" />
                            <asp:Button ID="btnSaveP" runat="server" Text="Save" />
                                                  
                            <asp:Panel ID="Panel3" runat="server" ScrollBars="Auto">
                            
                                <asp:Label runat="server" ID="Label9" Text="Target:" />
                                <asp:Label runat="server" ID="Label10" Text="..." />
                            
                                <asp:GridView ID="grdPriorConditions" CssClass="table" runat="server" DataKeyField="QuestionID" AutoGenerateColumns="False"
                                    ShowHeader="False">
                                    <Columns>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelected" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="HiddenID" runat="server" ClientIDMode="Static" />
                                                <asp:Label runat="server" ID="Description" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="Num3Digit" runat="server" Enabled="False" MaxLength="3" />
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="Num3Digit"
                                                    ValidChars="0123456789" />
                                                <asp:TextBox ID="Text20Digit" runat="server" Enabled="False" MaxLength="20" />
                                                <asp:TextBox ID="Text3Digit" runat="server" Enabled="False" MaxLength="3" />
                                                <asp:TextBox ID="Text10Digit" runat="server" Enabled="False" MaxLength="10" />
                                                <asp:TextBox ID="Text18Digit" runat="server" Enabled="False" MaxLength="18" />
                                                <asp:TextBox ID="Text50Digit" runat="server" Enabled="False" MaxLength="50" />
                                                <asp:TextBox ID="CurrencyAnswer" runat="server" ClientIDMode="Predictable" />
                                                <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="CurrencyAnswer"
                                                    Mask="9{9}.99" MaskType="Number" InputDirection="RightToLeft" />
                                                <asp:TextBox ID="NumericAnswer" runat="server" ClientIDMode="Predictable" />
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="NumericAnswer"
                                                    ValidChars="0123456789" />
                                                <asp:TextBox ID="TextAnswer" runat="server" ClientIDMode="Predictable" />
                                                <asp:RadioButtonList ID="RadioAnswer" CssClass="radiolist-inline" runat="server" ClientIDMode="Predictable"
                                                    RepeatLayout="UnorderedList" />
                                                <asp:CheckBoxList ID="checkAnswer" CssClass="checklist-inline" runat="server" ClientIDMode="Predictable"
                                                    RepeatLayout="UnorderedList" />
                                                <asp:TextBox ID="CalAnswer" runat="server" ReadOnly="True" ClientIDMode="Predictable" />
                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="CalAnswer"
                                                    Format="MM/dd/yyyy" />
                                                <asp:DropDownList ID="drpAnswer" runat="server" ClientIDMode="Predictable" />
                                                <asp:HiddenField ID="HiddenName" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabPanel4" CssClass="tab-panel" runat="server" HeaderText="Results">
                        <ContentTemplate>
                            <asp:Button ID="btnHideR" runat="server" Text="Hide"  ToolTip="Hide unImportant Questions"/>
                            <asp:Button ID="btnUnHideR" runat="server" Text="UnHide"  ToolTip="UnHide unImportant Questions"/>
                            <asp:Button ID="btnRefreshR" runat="server" Text="Refresh" />
                            <asp:Button ID="btnSaveR" runat="server" Text="Save" />                         
                            <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                                <asp:Label runat="server" ID="Label5" Text="Target" />
                                <asp:Label runat="server" ID="Label6" Text="..." />

                                <asp:GridView ID="grdResults" CssClass="table" runat="server" DataKeyField="QuestionID" AutoGenerateColumns="False"
                                    ShowHeader="False">
                                    <SelectedRowStyle CssClass="srowstyle" />
                                    <Columns>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelected" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="HiddenID" runat="server" ClientIDMode="Static" />
                                                <asp:Label runat="server" ID="Description" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="Num3Digit" runat="server" Enabled="False" MaxLength="3" />
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="Num3Digit"
                                                    ValidChars="0123456789" />
                                                <asp:TextBox ID="Text20Digit" runat="server" Enabled="False" MaxLength="20" />
                                                <asp:TextBox ID="Text3Digit" runat="server" Enabled="False" MaxLength="3" />
                                                <asp:TextBox ID="Text10Digit" runat="server" Enabled="False" MaxLength="10" />
                                                <asp:TextBox ID="Text18Digit" runat="server" Enabled="False" MaxLength="18" />
                                                <asp:TextBox ID="Text50Digit" runat="server" Enabled="False" MaxLength="50" />
                                                <asp:TextBox ID="CurrencyAnswer" runat="server" ClientIDMode="Predictable" />
                                                <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="CurrencyAnswer"
                                                    Mask="9{9}.99" MaskType="Number" InputDirection="RightToLeft" />
                                                <asp:TextBox ID="NumericAnswer" runat="server" ClientIDMode="Predictable" />
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="NumericAnswer"
                                                    ValidChars="0123456789" />
                                                <asp:TextBox ID="TextAnswer" runat="server" ClientIDMode="Predictable" />
                                                <asp:RadioButtonList ID="RadioAnswer" CssClass="radiolist-inline" runat="server" ClientIDMode="Predictable"
                                                    RepeatLayout="UnorderedList" />
                                                <asp:CheckBoxList ID="checkAnswer" CssClass="checklist-inline" runat="server" ClientIDMode="Predictable"
                                                    RepeatLayout="UnorderedList" />
                                                <asp:TextBox ID="CalAnswer" runat="server" ReadOnly="True" ClientIDMode="Predictable" />
                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="CalAnswer"
                                                    Format="MM/dd/yyyy" />
                                                <asp:DropDownList ID="drpAnswer" runat="server" ClientIDMode="Predictable" />
                                                <asp:HiddenField ID="HiddenName" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <%--<asp:TabPanel ID="TabPanel3" CssClass="tab-panel" runat="server" HeaderText="Unknown">
                        <ContentTemplate></ContentTemplate>
                    </asp:TabPanel>--%>
                    <%--<asp:TabPanel ID="TabAnswer" CssClass="tab-panel" runat="server" HeaderText="Last Process">
                        <ContentTemplate>
                            <label>Last Location</label>
                            <label>Last Process / Questions and Answsers</label>
                        </ContentTemplate>
                    </asp:TabPanel>--%>
                    <%--<asp:TabPanel ID="TabPanel1" CssClass="tab-panel" runat="server" HeaderText="Next Process">
                        <ContentTemplate>
                            <label>Move to Next Process</label>
                            <label>Move to Next Location</label>
                        </ContentTemplate>
                    </asp:TabPanel>--%>
                </asp:TabContainer>
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

        function goTopofScreen() {
            window.scrollTo(0, 0);
        }

    </script>
</asp:Content>


