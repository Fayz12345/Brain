<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TestParseEquation.aspx.cs" Inherits="BW_WebApp.Utility.TestParseEquation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="cMC" runat="server">
    <!-- TODO: finish this page -->
    <!-- TODO: section off page properly -->
    <!-- TODO: remove tab container... only one tab in use -->
    <asp:TabContainer runat="server" CssClass="tab-container" ActiveTabIndex="0">
        <asp:TabPanel runat="server" CssClass="tab-panel" Enabled="true" HeaderText="Rule Set">
            <ContentTemplate></ContentTemplate>
        </asp:TabPanel>

        <asp:TabPanel runat="server" CssClass="tab-panel" Enabled="true" HeaderText="Rule">
            <ContentTemplate>

                <div class="row">
                	<div class="col-md-7">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <asp:HiddenField ID="hdnSelectionStart" runat="server" Value="1" />

                                <label>Project:</label>
                                <asp:DropDownList ID="drpProject" runat="server" />

                                <label>Process:</label>
                                <asp:DropDownList ID="drpProcessList" runat="server" />

                                <label>Equation:</label>
                                <asp:TextBox runat="server" ID="txtEquation" TextMode="MultiLine" onblur="Javascript:SetLocation();" />
        
                                <!-- -->

                                <asp:TabContainer runat="server" CssClass="tab-container" ActiveTabIndex="0">
                                    <asp:TabPanel runat="server" CssClass="tab-panel" Enabled="true" HeaderText="English Version">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtEnglish" TextMode="MultiLine" />
                                            <asp:Button ID="btnEnglishVersion" runat="server" Text="Translate" />
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel runat="server" CssClass="tab-panel" Enabled="true" HeaderText="Token Version">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtToken" TextMode="MultiLine" />
                                            <asp:Button ID="btnTokenVersion" runat="server" Text="Translate" />
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel runat="server" CssClass="tab-panel" Enabled="true" HeaderText="Token Version With Sample Data">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtTokenWithData" TextMode="MultiLine" />
                                            <asp:Button ID="btnTokenVersionWithData" runat="server" Text="Translate" />
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel runat="server" CssClass="tab-panel" Enabled="true" HeaderText="Device Data">
                                        <ContentTemplate>
                                            <!-- TODO: delete this text if not needed -->
                                            <asp:TextBox runat="server" ID="txtData" TextMode="MultiLine"
                                                Text="'ISTHREAD':'N','hdnCalledFrom':'bSaveClick','DoAuthorize':'-1','a':'34','b':'36','c':'','d':'35','e':'',
                                                'f':'','g':'','v':'','h':'','i':'Receive','j':'','k':'Save','l':'jmccomb','m':'','n':'ZPTAGZZEDITZ','o':'iPhone',
                                                'p':'5','q':'0','r':'','s':'','t':'dddddddddddddd','u':'04/22/2015 01:35 PM','DD_243':'1','DD_250':'1','DD_269':'1',
                                                'DD_489':'1','DD_733':'1','DD_210':'1','CB_176':'0','CB_179':'1','RD_229':'1','RD_506':'1','RD_1012':'1',
                                                'TX_223':'04/22/2015','TX_226':'999','TX_746':'','TX_230':'04/22/2015','TX_236':'9999','TX_1018':''" />
                                            <asp:Button ID="dtaLoad" runat="server" Text="Refresh Sample Data" />
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                </asp:TabContainer>
                               
                                <!-- -->

                                <asp:Button ID="btnEvaluate" runat="server" Text="Evaluate" />
                                <asp:Label ID="lblResult" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                	<div class="col-md-5">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <asp:TabContainer runat="server" CssClass="tab-container" ActiveTabIndex="0">
                                    <asp:TabPanel runat="server" CssClass="tab-panel" Enabled="true" HeaderText="Attribute Names">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="chkProjectOnly" runat="server" Text="Project Only" />
                                            <asp:CheckBox ID="chkProcessOnly" runat="server" Text="Process Only" />
                                            <asp:Button ID="btnDisplayShowAttributeNamesList" CssClass="d-block" runat="server" Text="Show Attribute Names" />
                                            <label class="d-block">Attribute Names:</label>
                                            <asp:ListBox ID="AttributeNamesParameterList" runat="server" />
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel runat="server" CssClass="tab-panel" Enabled="true" HeaderText="Options">
                                        <ContentTemplate>
                                            <label>Options:</label>
                                            <asp:ListBox ID="MasterOptionList" runat="server" />
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel runat="server" CssClass="tab-panel" Enabled="true" HeaderText="Device Data">
                                        <ContentTemplate>
                                            <label>Device Data:</label>
                                            <asp:ListBox ID="lstDeviceData" runat="server" />
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                </asp:TabContainer>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                
            </ContentTemplate>
        </asp:TabPanel>

        <asp:TabPanel runat="server" CssClass="tab-panel" Enabled="true" HeaderText="Other">
            <ContentTemplate></ContentTemplate>
        </asp:TabPanel>

    </asp:TabContainer>
</asp:Content>
<asp:Content ContentPlaceHolderID="js" runat="server">
    <script type="text/javascript">
        
        function SetLocation() {
            $get("<%= hdnSelectionStart.ClientID %>").value = getCaret(<%= txtEquation.ClientID %>);
        }

        function InsertText(list) {
            var cursorPos = $get("<%= hdnSelectionStart.ClientID %>").value;
            var v = $get("<%= txtEquation.ClientID %>").value,
                textBefore = v.substring(0,  cursorPos ),
                textAfter  = v.substring( cursorPos, v.length );

            // var list = this;                      //document.getElementById('ListBox1');
            var indx = list.selectedIndex;
            var text = list[indx].value;
            $get("<%= txtEquation.ClientID %>").value = textBefore + '[' + text + ']' + textAfter;

            $get("<%= hdnSelectionStart.ClientID %>").value = cursorPos + text.length;

            setCursor(cursorPos + text.length);

            // alert('InsertText(' + text + ') at ' + $get("<%= hdnSelectionStart.ClientID %>").value);
        }

        function getCaret(el) {
            if (el.selectionStart) {
                return el.selectionStart;
            } else if (document.selection) {
                el.focus();

                var r = document.selection.createRange();
                if (r == null) {
                    return 0;
                }

                var re = el.createTextRange(),
                rc = re.duplicate();
                re.moveToBookmark(r.getBookmark());
                rc.setEndPoint('EndToStart', re);

                return rc.text.length;
            }
            return 0;
        }

        function setCursor(cursorPos) {
            $get("<%= txtEquation.ClientID %>").focus();
            var textbox = document.getElementById("<%= txtEquation.ClientID %>");
            if (textbox.createTextRange) {
                var range = textbox.createTextRange();
                range.moveStart('character', cursorPos);
                range.collapse();
                range.select();
            } 
            return false;
        }

    </script>
</asp:Content>