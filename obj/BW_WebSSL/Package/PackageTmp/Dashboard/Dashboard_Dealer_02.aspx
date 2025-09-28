<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard_Dealer_02.aspx.cs" Inherits="BW_WebApp.Dashboard_Dealer_02" %>

<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>





<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cMC" runat="server">


    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

        

                

        <asp:HiddenField runat="server" ID="hdnUserName" Value="" />
            <asp:HiddenField ID="hdnUserIPAddress" runat="server" Value=""/>
            <asp:Table ID="Table1" runat="server" Width="100%">
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Image ID="imgGMPLogo" runat="server" ImageUrl="~/Images/Logo.jpg" />


                    </asp:TableCell>
                    <asp:TableCell ID="TableCell1" runat="server" HorizontalAlign="Left" VerticalAlign="Top" Wrap="False">
                        <h1>
                            <asp:Label ID="lblName" runat="server" Text="Dashboard - Dealer xx1"></asp:Label>
                        </h1>
                        <br />
                        <asp:DropDownList ID="drpClientLocations" runat="server" Width="80%">
                            <asp:ListItem Text="Client 1" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Client 2" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Client 3" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Client 4" Value="4"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>



            <asp:TabContainer runat="server" ID="t1x" Width="100%" ActiveTabIndex="0">
                <asp:TabPanel runat="server" ID="tb1x" Enabled="true" HeaderText="Open" Width="100%"
                    Height="100%">
                    <HeaderTemplate>
                        <asp:Label ID="lblDataTab" runat="server" Text="Open"></asp:Label>
                    </HeaderTemplate>
                    <ContentTemplate>
                        <asp:Table ID="Table2" runat="server" Width="100%">
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:Label ID="Label1" runat="server" Text="ESN/IMEI Records"></asp:Label>
                                    <asp:Panel ID="Panel2" runat="server" Width="100%" Height="100%" ScrollBars="Auto">
                                        <asp:GridView ID="gvDashboard" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" Width="100%">
                                            <SelectedRowStyle CssClass="srowstyle" />
                                            <Columns>
                                                <asp:BoundField DataField="EQUSAN" HeaderText="EQU/SAN #" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ESN" HeaderText="ESN/IMEI" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="UnitStatus" HeaderText="Unit Status" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Status" HeaderText="Authorization" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgAuthorize" runat="server" HeaderText="*" ImageUrl="~/Images/Contract.png"
                                                            Width="15px" ToolTip="Authorize/Decline"></asp:ImageButton>
                                                        <asp:ImageButton ID="imgPrint" runat="server" HeaderText="*" ImageUrl="~/Images/print_icon.gif"
                                                            Width="15px" ToolTip="Print"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Open">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgOpen" runat="server" HeaderText="*" ImageUrl="~/Images/Info.png"
                                                            Width="15px" ToolTip="Open Unit Summary"></asp:ImageButton>
                                                        <br />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>




                                    </asp:Panel>
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel runat="server" ID="TabPanel1" Enabled="true" HeaderText="Closed" Width="100%"
                    Height="100%">
                    <HeaderTemplate>
                        <asp:Label ID="Label2" runat="server" Text="Closed"></asp:Label>
                    </HeaderTemplate>
                    <ContentTemplate>
                        <asp:Table ID="Table3" runat="server" Width="100%">
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:Label ID="Label3" runat="server" Text="ESN/IMEI Records"></asp:Label>
                                    <asp:Panel ID="Panel1" runat="server" Width="100%" Height="100%" ScrollBars="Auto">


                                        <asp:GridView ID="gvDashboardClosed" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" Width="100%">
                                            <SelectedRowStyle CssClass="srowstyle" />
                                            <Columns>
                                                <asp:BoundField DataField="EQUSAN" HeaderText="EQU/SAN #" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ESN" HeaderText="ESN/IMEI" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:BoundField>
<%--                                                <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:BoundField>--%>
                                                <asp:BoundField DataField="UnitStatus" HeaderText="Unit Status" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Status" HeaderText="Authorization" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgAuthorize" runat="server" HeaderText="*" ImageUrl="~/Images/Contract.png"
                                                            Width="15px" ToolTip="Authorize/Decline"></asp:ImageButton>
                                                        <asp:ImageButton ID="imgPrint" runat="server" HeaderText="*" ImageUrl="~/Images/print_icon.gif"
                                                            Width="15px" ToolTip="Print"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Open">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgOpen" runat="server" HeaderText="*" ImageUrl="~/Images/Info.png"
                                                            Width="15px" ToolTip="Open Unit Summary"></asp:ImageButton>
                                                        <br />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
            <br />
            <br />
            <br />

           <syncfusion:Window ID="wndUnitSummary" Title="Unit Summary" runat="server" DraggingStyle="Original" CssClass="syncpopup">

               <asp:HiddenField ID="hdnReceiveDetailID" runat="server" />
               <asp:HiddenField ID="hdnReceiveDetailAuthorizationLogID" runat="server" />
               <asp:Panel ID="Panel9" runat="server" Width="100%" Height="100%">
                   <table id="Table6" runat="server" width="100%">
                       <tr>
                           <td valign="top" colspan="4" align="center">
                               <h1>
                                   <asp:Label ID="lblTitle" runat="server" Text="Unit Summary"></asp:Label>
                                   <br />
                               </h1>
                           </td>
                       </tr>
                       <tr>
                           <td align="right" valign="top">
                               EQU/SAN #:
                           </td>
                           <td align="left" valign="top">
                               <%--<asp:TextBox ID="txtClientReference" runat="server" Enabled="False"></asp:TextBox>--%>
                               <asp:TextBox ID="txtEQUSAN" runat="server" Enabled="False"></asp:TextBox>
                           </td>
<%--                           <td align="right" valign="top">
                               Customer Name:
                           </td>
                           <td align="left" valign="top">
                               <asp:TextBox ID="txtCustomerName" runat="server" Enabled="False"></asp:TextBox>
                           </td>--%>
                       </tr>


                       <tr>
                           <td align="right" valign="top">
                               ESN/IMEI:
                           </td>
                           <td align="left" valign="top">
                               <asp:TextBox ID="txtESN" runat="server" Enabled="False"></asp:TextBox>
                           </td>
<%--                           <td align="right" valign="top">
                               Original IMEI:
                           </td>
                           <td align="left" valign="top">
                               <asp:TextBox ID="txtOriginalIMEI" runat="server" Enabled="False"></asp:TextBox>
                           </td>--%>

                       </tr>
                       <tr>
                           <td align="right" valign="top">
                               <br />
                           </td>
                           <td align="left" valign="top">
                               <br />
                               <%--<asp:TextBox ID="txtWarrantyType" runat="server" Enabled="False"></asp:TextBox>--%>
                           </td>
                           <td align="right" valign="top">
                               First Fault:
                           </td>
                           <td align="left" valign="top">
                               <asp:TextBox ID="txtFaultCode" runat="server" Enabled="False"></asp:TextBox>
                           </td>
                       </tr>
                       <tr>
                           <td align="right" valign="top">
                               Date Submitted:
                           </td>
                           <td align="left" valign="top">
                               <asp:TextBox ID="txtDateSubmitted" runat="server" Enabled="False"></asp:TextBox>
                           </td>
                           <td align="right" valign="top">
                               Second Fault:
                           </td>
                           <td align="left" valign="top">
                               <asp:TextBox ID="txtFaultCode2" runat="server" Enabled="False"></asp:TextBox>
                           </td>
                       </tr>
                       <tr>
                           <td align="right" valign="top">
                               Date Received at IMM:
                           </td>
                           <td align="left" valign="top">
                               <asp:TextBox ID="txtGMPReceivedDate" runat="server" Enabled="False"></asp:TextBox>
                           </td>
                           <td align="right" valign="top">
                               Repair Date:
                           </td>
                           <td align="left" valign="top">
                               <asp:TextBox ID="txtRepairDate" runat="server" Enabled="False"></asp:TextBox>
                           </td>
                       </tr>
                       <tr>
                           <td align="right" valign="top">
                               Unit Status:
                           </td>
                           <td align="left" valign="top">
                               <asp:TextBox ID="txtCurrentProcess" runat="server" Enabled="False"></asp:TextBox>
                           </td>
                           <td align="right" valign="top">
                              Repair Fee:
                           </td>
                           <td align="left" valign="top">
                               <asp:TextBox ID="txtRepairFee" runat="server" Enabled="False"></asp:TextBox>
                           </td>
                       </tr>

                       <tr>
                           <td align="right" valign="top">
                               Customer Notes:
                           </td>
                           <td align="left" valign="top" colspan="3">
                               <asp:TextBox ID="txtCustomerNotes" runat="server" Enabled="False" Rows="2" TextMode="MultiLine" Width="95%"></asp:TextBox>
                           </td>
                       </tr>

                       <tr>

                           <td align="right" valign="top">
                               Parts Returned:
                           </td>
                           <td align="left" valign="top" colspan="3">
                               <asp:TextBox ID="txtPartsReturned" runat="server" Enabled="False" Rows="2" TextMode="MultiLine" Width="95%"></asp:TextBox>
                          </td>
                       </tr>

                       <tr>
                           <td align="right" valign="top">
                               <asp:Label ID="lblAssessment" runat="server" Text="Unit Assessment:"></asp:Label>
                           </td>
                           <td align="left" valign="top" colspan="3">
                               <asp:TextBox ID="txtAssessment" runat="server" Enabled="False" Rows="2" TextMode="MultiLine" Width="95%"></asp:TextBox>
                           </td>
                       </tr>


                       <tr>
                           <td align="right" valign="top">
                               Authorization:
                           </td>
                           <td align="left" valign="top">
                               <asp:TextBox ID="txtAuthorizationStatus" runat="server" Enabled="False"></asp:TextBox>
                           </td>
                           <td align="left" valign="top" colspan="2">
                               <table id="Table4" runat="server" width="100%">
                                   <tr>
                                       <td valign="top" align="left">
                                           <asp:Label ID="lblEstimate" runat="server" Text="Estimate: $1000"></asp:Label>
                                       </td>
                                       <td valign="top" align="left">
                                           <asp:Label ID="lblFreight" runat="server" Text="Freight: $1000"></asp:Label>
                                       </td>
                                       <td valign="top" align="right">
                                           <asp:Label ID="lblHST" runat="server" Text="HST: $1000"></asp:Label>
                                       </td>
                                   </tr>
                               </table>
                           </td>

                       </tr>




                       <tr>
                           <td align="right" valign="top">
                               Authorization Name:
                           </td>
                           <td align="left" valign="top">
                               <asp:TextBox ID="txtAuthorizationName" runat="server" Enabled="False"></asp:TextBox>
                           </td>
                           <td align="left" valign="top">
                               <asp:Button ID="btnAuthorize" runat="server" Text="Authorize" OnClientClick="Authorize();return false;" />
                               <asp:Button ID="btnDecline" runat="server" Text="Decline" OnClientClick="Decline();return false;" />
                           </td>
                           <td align="right" valign="top">
                               <asp:Label ID="lblTotal" runat="server" Text="Total: $1000"></asp:Label>

                           </td>
                       </tr>



                       <tr>
                           <td align="right" valign="top">
                               Ship Date:
                           </td>
                           <td align="left" valign="top">
                               <asp:TextBox ID="txtGMPMSCShippedDate" runat="server" Enabled="False"></asp:TextBox>
                           </td>
                           <td align="right" valign="top">
                               Outgoing waybill:
                           </td>
                           <td align="left" valign="top">
                               <asp:TextBox ID="txtOutBoundWayBill_S" runat="server" Enabled="False"></asp:TextBox>
                           </td>
                       </tr>

                       <tr>
                           <td align="right" valign="top">
                               Courier - Out:
                           </td>
                           <td align="left" valign="top" colspan="3">
                               <asp:TextBox ID="txtCourier" runat="server" Enabled="False"></asp:TextBox>
                           </td>
                       </tr>

                       <tr>
                           <td colspan="4">
                               <br />
                               <asp:Button ID="btnCancel" runat="server" Text="Close" OnClientClick="CloseUnitSummary();return false;"
                                   Width="100%" />
                           </td>
                       </tr>
                   </table>
               </asp:Panel>
            </syncfusion:Window>

            <syncfusion:Window ID="wndGetPIN" Title="PIN" runat="server" CssClass="syncpopup">
                <asp:Panel ID="pnlPin" runat="server" Width="100%" Height="100%">
                <asp:HiddenField ID="hdnPassword" runat="server" />
                PIN:
                <input id="PinInput" runat="server" type="password" value=""/>
                <asp:Button ID="Button1" runat="server" Text="Done" OnClientClick="ClosePinWindow();return false;"/>
                </asp:Panel>
            </syncfusion:Window>

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

      function ClosePinWindow() {
          var Password = $get('<%= hdnPassword.ClientID%>').value;
          var GPassword = $get('<%= PinInput.ClientID%>').value;
          $get('<%= PinInput.ClientID%>').value = "";
          $find('<%=this.wndGetPIN.ClientID%>').Close();
          //          var GPassword = prompt('PIN:', ' ');
          if (GPassword == null || GPassword.length == 0) { return; }
          if (GPassword == Password) {
              $get('<%= btnAuthorize.ClientID%>').style.visibility = "visible";
              $get('<%= btnAuthorize.ClientID%>').disabled = true;
              $get('<%= btnDecline.ClientID%>').style.visibility = "visible";
              $get('<%= btnDecline.ClientID%>').disabled = true;
              $get('<%= lblEstimate.ClientID%>').style.visibility = "visible";
              //              $get('<%= lblFreight.ClientID%>').style.visibility = "visible";
              //              $get('<%= lblHST.ClientID%>').style.visibility = "visible";
              //              $get('<%= lblTotal.ClientID%>').style.visibility = "visible";

              $get('<%= txtAuthorizationName.ClientID%>').disabled = false;
              $get('<%= txtAuthorizationName.ClientID%>').style.background = '#FFFFCC';
              $get('<%= txtAuthorizationName.ClientID%>').focus();
              $find('<%=this.wndUnitSummary.ClientID%>').Open(null, null);
          }
          else { alert("Invalid PIN"); }

      }


      function OpenUnitSummary(ReceiveDetailID, ReceiveDetailAuthorizationLogID, ShowAuthorize, Password) {
          ClearRecordData();
          fillRecordData(ReceiveDetailID, ReceiveDetailAuthorizationLogID);
          $get('<%= hdnReceiveDetailID.ClientID%>').value = ReceiveDetailID;
          $get('<%= hdnPassword.ClientID%>').value = Password;


          $get('<%= hdnReceiveDetailAuthorizationLogID.ClientID%>').value = ReceiveDetailAuthorizationLogID;
          //$get('<%= txtAuthorizationName.ClientID%>').style.background = '#0066CC';

          if (ShowAuthorize == false) {
              $get('<%= btnAuthorize.ClientID%>').style.visibility = "hidden";
              $get('<%= btnDecline.ClientID%>').style.visibility = "hidden";
              $get('<%= lblEstimate.ClientID%>').style.visibility = "hidden";
              $get('<%= lblFreight.ClientID%>').style.visibility = "hidden";
              $get('<%= lblHST.ClientID%>').style.visibility = "hidden";
              $get('<%= lblTotal.ClientID%>').style.visibility = "hidden";
              $get('<%= txtAuthorizationName.ClientID%>').disabled = true;
              $find('<%=this.wndUnitSummary.ClientID%>').Open(null, null);
          }
          else {
              if (Password.length > 0) {
                  $find('<%=this.wndGetPIN.ClientID%>').Open(null, null);
              }
          }

      }




      function NameChange() {
          var text = $get('<%= txtAuthorizationName.ClientID%>').value;
          $get('<%= btnAuthorize.ClientID%>').disabled = true;
          $get('<%= btnDecline.ClientID%>').disabled = true;
          $get('<%= txtAuthorizationName.ClientID%>').style.background = '#FFC0C1';
          if (text.length > 0) {
              $get('<%= txtAuthorizationName.ClientID%>').style.background = '#FFFFCC';
              $get('<%= btnAuthorize.ClientID%>').disabled = false;
              $get('<%= btnDecline.ClientID%>').disabled = false;
          }
          $get('<%= txtAuthorizationName.ClientID%>').focus();
      }



      function ProcessUnitSummary() {
          CloseUnitSummary();
      }


      function CloseUnitSummary() {
          $find('<%=this.wndUnitSummary.ClientID%>').Close();
          // $get('<%=btnRefresh.ClientID%>').click();
      }

      function ClearRecordData() {

          $get('<%= txtEQUSAN.ClientID%>').value = 'One moment please';

          $get('<%= txtESN.ClientID%>').value = '';
          $get('<%= txtWarrantyType.ClientID%>').value = '';

          $get('<%= txtFaultCode.ClientID%>').value = '';
          $get('<%= txtDateSubmitted.ClientID%>').value = '';
          $get('<%= txtFaultCode2.ClientID%>').value = '';
          $get('<%= txtGMPReceivedDate.ClientID%>').value = '';
          $get('<%= txtRepairDate.ClientID%>').value = '';
          $get('<%= txtCurrentProcess.ClientID%>').value = '';

          //          $get('<%= btnAuthorize.ClientID%>').value = ReceiveDetailID;

          $get('<%= txtRepairFee.ClientID%>').value = '';
          $get('<%= txtCustomerNotes.ClientID%>').value = '';
          $get('<%= txtPartsReturned.ClientID%>').value = '';
          $get('<%= txtAuthorizationStatus.ClientID%>').value = '';
          $get('<%= txtAuthorizationName.ClientID%>').value = '';
          $get('<%= txtAssessment.ClientID%>').value = '';
          $get('<%= txtGMPMSCShippedDate.ClientID%>').value = '';
          $get('<%= txtOutBoundWayBill_S.ClientID%>').value = '';
          $get('<%= txtCourier.ClientID%>').value = '';

          $get('<%= lblEstimate.ClientID%>').innerHTML = '';
          $get('<%= lblFreight.ClientID%>').innerHTML = '';
          $get('<%= lblHST.ClientID%>').innerHTML = '';
          $get('<%= lblTotal.ClientID%>').innerHTML = '';

          $get('<%= lblAssessment.ClientID%>').innerHTML = 'Unit Assessment:';


      }


      function fillRecordData(ReceiveDetailID, ReceiveDetailAuthorizationLogID) {
          var service = new WebServer_01();
          var rValue = service.GetDashboardReceiveDetail(ReceiveDetailID, ReceiveDetailAuthorizationLogID, $get('<%= hdnUserName.ClientID %>').value, onfillRecordData);
      }

      function onfillRecordData(result) {
          result = '({' + result + '})';
          var resultList = eval(result);

          //          rData.AddValuePair("isAuthorize", "");

          $get('<%= txtEQUSAN.ClientID%>').value = resultList.txtEQUSAN;

          $get('<%= txtESN.ClientID%>').value = resultList.txtESN;
          $get('<%= txtWarrantyType.ClientID%>').value = resultList.txtWarrantyType;
          $get('<%= txtFaultCode.ClientID%>').value = resultList.txtFaultCode;
          $get('<%= txtDateSubmitted.ClientID%>').value = resultList.txtDateSubmitted;
          $get('<%= txtFaultCode2.ClientID%>').value = resultList.txtFaultCode2;
          $get('<%= txtGMPReceivedDate.ClientID%>').value = resultList.txtGMPReceivedDate;
          $get('<%= txtRepairDate.ClientID%>').value = resultList.txtRepairDate;
          $get('<%= txtCurrentProcess.ClientID%>').value = resultList.txtCurrentProcess;

          //          $get('<%= btnAuthorize.ClientID%>').value = ReceiveDetailID;

          $get('<%= txtRepairFee.ClientID%>').value = resultList.txtRepairFee;
          $get('<%= txtCustomerNotes.ClientID%>').value = resultList.txtCustomerNotes;
          $get('<%= txtPartsReturned.ClientID%>').value = resultList.txtPartsReturned;



          $get('<%= txtAuthorizationStatus.ClientID%>').value = resultList.txtAuthorizationStatus;
          $get('<%= txtAuthorizationName.ClientID%>').value = resultList.txtAuthorizationName;
          $get('<%= txtAssessment.ClientID%>').value = resultList.txtAssessment;
          $get('<%= txtGMPMSCShippedDate.ClientID%>').value = resultList.txtGMPMSCShippedDate;
          $get('<%= txtOutBoundWayBill_S.ClientID%>').value = resultList.txtOutBoundWayBill_S;
          $get('<%= txtCourier.ClientID%>').value = resultList.txtCourier;

          if (resultList.txtRepairFee.length > 0) { $get('<%= lblAssessment.ClientID%>').innerHTML = 'Repair Notes: '; $get('<%= txtAssessment.ClientID%>').value = resultList.txtRepairNotes; }

          if (resultList.lblEstimate.length > 0) { $get('<%= lblEstimate.ClientID%>').innerHTML = 'Estimate: ' + resultList.lblEstimate; }
          //          if (resultList.lblFreight.length > 0) { $get('<%= lblFreight.ClientID%>').innerHTML = 'Freight: ' + resultList.lblFreight; }
          //          if (resultList.lblHST.length > 0) { $get('<%= lblHST.ClientID%>').innerHTML = 'HST: ' + resultList.lblHST; }
          //          if (resultList.lblTotal.length > 0) { $get('<%= lblTotal.ClientID%>').innerHTML = 'Total: ' + resultList.lblTotal; }
      }


      //      function Authorize() {
      //          var service = new WebServer_01();
      //          var rValue = service.AuthorizeAuthorization($get('<%= hdnReceiveDetailAuthorizationLogID.ClientID %>').value,
      //                                                         $get('<%= txtAuthorizationName.ClientID %>').value,
      //                                                         $get('<%= hdnUserName.ClientID %>').value, onDeclineAuthorize);
      //      }

      function Authorize() {
          var service = new WebServer_01();
          var rValue = service.DealerAuthorized($get('<%= hdnReceiveDetailAuthorizationLogID.ClientID %>').value,
                                                         $get('<%= txtAuthorizationName.ClientID %>').value,
                                                         $get('<%= hdnUserName.ClientID %>').value,
                                                         $get('<%= hdnUserIPAddress.ClientID %>').value,

                                                         onAuthorize, onAuthorizeb);

      }

      function Decline() {
          var service = new WebServer_01();
          var rValue = service.DeclineAuthorization($get('<%= hdnReceiveDetailAuthorizationLogID.ClientID %>').value,
                                                    $get('<%= txtAuthorizationName.ClientID %>').value,
                                                    $get('<%= hdnUserName.ClientID %>').value, onDecline, onDeclineb);
      }

      function onWebServerError(Result) {
          alert('Error:' + Result.get_message());
      }

      function onDecline(result) {
          var answer = confirm('Print Authorization Declined form?')
          if (answer == true) {
              OpenAuthorizeReport($get('<%= hdnReceiveDetailAuthorizationLogID.ClientID %>').value)
          }
          //          $get('<%= btnRefresh.ClientID %>').click() = true;
      }

      function onDeclineb(result) {
          var answer = confirm('Print authorization declined form?')
          if (answer == true) {
              OpenAuthorizeReport($get('<%= hdnReceiveDetailAuthorizationLogID.ClientID %>').value)
          }
          //          $get('<%= btnRefresh.ClientID %>').click() = true;
      }
      function onAuthorize(result) {
          var answer = confirm('Print Authorization form?')
          if (answer == true) {
              OpenAuthorizeReport($get('<%= hdnReceiveDetailAuthorizationLogID.ClientID %>').value)
          }
          //          $get('<%= btnRefresh.ClientID %>').click() = true;
      }

      function onAuthorizeb(result) {
          var answer = confirm('Print authorization form?')
          if (answer == true) {
              OpenAuthorizeReport($get('<%= hdnReceiveDetailAuthorizationLogID.ClientID %>').value)
          }
          //          $get('<%= btnRefresh.ClientID %>').click() = true;
      }


      //      function OpenAuthorizeReport(ReceiveDetailAuthorizationLogID, Repair) {
      //          // if Pre Received XAUTX
      //          // Do this
      //          //else
      //          // do this
      //          if (Repair == "No") {
      //              return OpenAuthorizeReport_A(ReceiveDetailAuthorizationLogID);
      //          }
      //          else {
      //              return OpenRepairForm("R", ReceiveDetailAuthorizationLogID);
      //          }


      //      }


      //      function OpenbagTag(ReceiveDetailID) {
      //          var report = 'Bagtag';
      //          //           if (IsNumeric(MCL('hdnAllowProjectPassThrough').value) == false) {
      //          //               report = 'Bagtag';
      //          //           }

      //          var xDataList = {};
      //          xDataList['RPT'] = "BagTag";
      //          xDataList['RDID'] = ReceiveDetailID;
      //          xDataList['ESN'] = "X";

      //          var pstring = GetParameterStream(xDataList);
      //          var WindowToOpen = 'BagTag.aspx';
      //          if (pstring.length > 0) {
      //              WindowToOpen = WindowToOpen + '?' + pstring
      //          }
      //          var win = window.open(WindowToOpen, '_blank', 'menubar', true);
      //          // win.focus();
      //      }

      function OpenClientbagTag(ReceiveDetailID) {
          var xDataList = {};
          xDataList['RDID'] = ReceiveDetailID;
          var pstring = GetParameterStream(xDataList);
          //           var WindowToOpen = 'RPT_EXCEL_Out.aspx';
          var WindowToOpen = 'RPT_Submission_02.aspx';

          if (pstring.length > 0) {
              WindowToOpen = WindowToOpen + '?' + pstring
          }
          var win = window.open(WindowToOpen, '_blank', 'menubar', true);
          //          ScanFocus();
          return;
      }


      function OpenRepairForm(RPT, ReceiveDetailAuthorizationLogID) {
          // CloseSelectRepairReport();
          var xDataList = {};
          xDataList['A'] = "-1";
          xDataList['B'] = ReceiveDetailAuthorizationLogID;
          xDataList['C'] = RPT;
          var pstring = GetParameterStream(xDataList);
          var WindowToOpen = 'RPT_RepairForm.aspx';
          if (pstring.length > 0) {
              WindowToOpen = WindowToOpen + '?' + pstring
          }
          var win = window.open(WindowToOpen, '_blank', 'menubar', true);
          // win.focus();
      }


      function OpenAuthorizeReport(ReceiveDetailAuthorizationLogID) {
          var xDataList = {};
          xDataList["A"] = "";                                    // $get('<%= hdnReceiveDetailID.ClientID %>').value;
          xDataList["B"] = ReceiveDetailAuthorizationLogID;       // $get('<%= hdnReceiveDetailAuthorizationLogID.ClientID %>').value;

          //            var win = window.open("ViewDoc.aspx", "_blank", "status=no,toolbar=no,menubar=no,location=no,titlebar=no,width=600px,height=540px", true);
          var pstring = GetParameterStream(xDataList);

          var WindowToOpen = "RPT_Authorize_01.aspx";
          //var WindowToOpen = "RPT_BoxList.aspx";
          if (pstring.length > 0) {
              WindowToOpen = WindowToOpen + "?" + pstring
          }
          //            var win = window.open(WindowToOpen, "_blank", "width=100,height=50,menubar", true);
          var win = window.open(WindowToOpen, "_blank", "menubar", true);
          // win.focus();
          CloseUnitSummary();
          return false;
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

