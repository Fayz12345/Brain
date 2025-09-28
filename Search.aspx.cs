using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using BW_WebApp.DataManagers;
//using Factory_DataModel.Classes;
//using Factory_DataModel;

//using System.Collections;
//using System.Data;
//using System.Data.SqlClient;
////using SoftArtisans.OfficeWriter.ExcelWriter; 
//using System.Text;
//using Syncfusion.XlsIO;

namespace BW_WebApp
{
    public partial class Search : System.Web.UI.Page
    {

        string CorrectPartsRole = "Administrators";

        protected void Page_Load(object sender, EventArgs e)
        {
            btnSearch.Click += new EventHandler(btnSearch_Click);
            grdTempDetail.RowDataBound += new GridViewRowEventHandler(grdTempDetail_RowDataBound);
            grdTempDetail.RowCommand += new GridViewCommandEventHandler(grdTempDetail_RowCommand);
            if (!IsPostBack)
            {
                ProjectManager pm = new ProjectManager(User.Identity.Name);
                List<Project> pl = pm.GetProjectList();
                drpProjectList.Items.Clear();
                ListItem z = new ListItem("All", "-1");
                drpProjectList.Items.Add(z);
                foreach (Project p in pl)
                {
                    ListItem x = new ListItem(p.Name, p.ProjectID.ToString());
                    drpProjectList.Items.Add(x);
                }



                ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
                List<ReceiveDetailStatus> rdl = rdm.GetReceiveDetailStatusList();
                drpStatus.Items.Clear();
                z = new ListItem("All", "-1");
                drpStatus.Items.Add(z);
                foreach (ReceiveDetailStatus o in rdl)
                {
                    ListItem x = new ListItem(o.Status, o.ReceiveDetailStatusID.ToString());
                    drpStatus.Items.Add(x);
                }

                QuestionManager qm = new QuestionManager(User.Identity.Name);
                List<Option> ol = qm.GetQuestionOptionList("Carrier");
                drpCarrier.Items.Clear();
                z = new ListItem("All", "-1");
                drpCarrier.Items.Add(z);
                foreach (Option o in ol)
                {
                    ListItem x = new ListItem(o.OptionText, o.OptionID.ToString());
                    drpCarrier.Items.Add(x);
                }

                ol = qm.GetQuestionOptionList("Manufacturer");
                drpManufacturer.Items.Clear();
                z = new ListItem("All", "-1");
                drpManufacturer.Items.Add(z);
                foreach (Option o in ol)
                {
                    ListItem x = new ListItem(o.OptionText, o.OptionID.ToString());
                    drpManufacturer.Items.Add(x);
                }

                ol = qm.GetQuestionOptionList("Model");
                drpModel.Items.Clear();
                z = new ListItem("All", "-1");
                drpModel.Items.Add(z);
                foreach (Option o in ol)
                {
                    ListItem x = new ListItem(o.OptionText, o.OptionID.ToString());
                    drpModel.Items.Add(x);
                }

                ol = qm.GetQuestionOptionList("Colour");
                drpColour.Items.Clear();
                z = new ListItem("All", "-1");
                drpColour.Items.Add(z);
                foreach (Option o in ol)
                {
                    ListItem x = new ListItem(o.OptionText, o.OptionID.ToString());
                    drpColour.Items.Add(x);
                }

                txtBeginDate.Text = DateTime.Now.AddDays(-7).ToShortDateString();
                txtEndDate.Text = DateTime.Now.ToShortDateString();

                txtBeginQC.Text = DateTime.Now.AddDays(-7).ToShortDateString();
                txtEndQC.Text = DateTime.Now.ToShortDateString();

                txtBeginShipped.Text = DateTime.Now.AddDays(-7).ToShortDateString();
                txtEndShipped.Text = DateTime.Now.ToShortDateString();
                //UpdateTemplateGrid("~/templates/Detail", grdTempDetail);
                //UpdateTemplateGrid("~/templates/DetailDate", grdTempDetailDate);
                //UpdateTemplateGrid("~/templates/Bulk", grdTempBulk);
                //UpdateTemplateGrid("~/templates/BSP", grdTempBSP);
            }
        }

        void grdTempDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            LinkButton btnOpen = (LinkButton)e.CommandSource;
            string ESN = "";
            string id = "-1";
            string pid = "-1";
            string processName = "";
            string CommandArgument = btnOpen.CommandArgument;
            string[] data = CommandArgument.Split(',');

            if (btnOpen.ID.ToUpper() == "IMGANALYZE")
            {
                id = data[0];
                ScriptManager.RegisterStartupScript(this, GetType(), "Open Unit", "OpenUnitAnalysisRPT(" + id + ");", true);
            }

            else if (btnOpen.ID.ToUpper() == "IMGBAGTAG")
            {
                id = data[0];
                ScriptManager.RegisterStartupScript(this, GetType(), "Open BagTag", "OpenbagTag(" + id + ");", true);
            }
            else if (btnOpen.ID.ToUpper() == "IMAGEKITTING")
            {

                //'KITTING' || 'SHIPPING GMP SALES'


                id = data[0];
                ScriptManager.RegisterStartupScript(this, GetType(), "Open Kitting", "OpenKitting(" + id + ");", true);
            }






            else if (btnOpen.ID.ToUpper() == "IMGCORRECT")
            {

                //id = data[0];
                //decimal ID = -1;
                //decimal.TryParse(id, out ID);
                ESN = data[1];
                if (ESN.Length > 0)
                {
                    ReceiveDetailManager dm = new ReceiveDetailManager(User.Identity.Name);
                    dm.Utility_Maintenance(ESN);
                    ScriptManager.RegisterStartupScript(this, GetType(), "Data Reset", "alert('Cleanup done');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Data Reset", "alert('Cleanup done');", true);
                }
            }
            else if (btnOpen.ID.ToUpper() == "IMAGERESETBIN")
            {
                id = data[0];
                decimal ID = -1;
                if (decimal.TryParse(id, out ID) == true)
                {
                    ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
                    rdm.UpdateESNAttribute_Blank(ID, "Bin");
                    ScriptManager.RegisterStartupScript(this, GetType(), "Reset Bin", "alert('Bin Reset');", true);
                }
            }
            else if (btnOpen.ID.ToUpper() == "IMGPARTDETAILS")
            {
                id = data[0];
                ESN = data[1];
                decimal ID = -1;
                if (decimal.TryParse(id, out ID) == true)
                {
                    //ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
                    //rdm.UpdateESNAttribute_Blank(ID, "Bin");
                    ScriptManager.RegisterStartupScript(this, GetType(), "Open Unit", "OpenUnitPartScreen(" + id + ",'" + ESN + "');", true);
                    //ScriptManager.RegisterStartupScript(this, GetType(), "Reset Bin", "alert('Edit Part Detail');", true);
                }
            }



            else
            {
                id = data[0];
                pid = data[1];
                processName = data[2];
                ScriptManager.RegisterStartupScript(this, GetType(), "Open Unit", "OpenUnit(" + id + "," + pid + ",'" + processName + "');", true);
            }
        }

        void grdTempDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                decimal id = -1;
                Type type = e.Row.DataItem.GetType();               //.GetGenericArguments()[0]; 
                GetMasterDetailInventoryList_TemplateRawData_SearchGrid_02Result Data = ((GetMasterDetailInventoryList_TemplateRawData_SearchGrid_02Result)e.Row.DataItem);
                LinkButton bPrint = (LinkButton)e.Row.FindControl("imgOpen");
                if (bPrint != null)
                {
                    bPrint.Visible = true;
                    if ((Data.CurrentProcessName.Length >= 7 && Data.CurrentProcessName.Substring(0, 7).ToUpper() == "RECEIVE") // may have to include here "isIFSLocked. If Yes, then can open.
                        || (Data.CurrentProcessName.Length >= 8 && Data.CurrentProcessName.Substring(0, 8).ToUpper() == "SHIPPING"))
                    {
                        bPrint.Visible = false;
                    }

                    bPrint.CommandArgument = Data.ReceiveDetailID.ToString() + "," + Data.ProjectID.ToString() + "," + "";
                }
                bPrint = (LinkButton)e.Row.FindControl("imgOpenProcess");
                if (bPrint != null)
                {
                    bPrint.Visible = true;
                    if ((Data.CurrentProcessName.Length >= 7 && Data.CurrentProcessName.Substring(0, 7).ToUpper() == "RECEIVE")
                        || (Data.CurrentProcessName.Length >= 8 && Data.CurrentProcessName.Substring(0, 8).ToUpper() == "SHIPPING"))
                    {
                        bPrint.Visible = false;
                    }
                    bPrint.CommandArgument = Data.ReceiveDetailID.ToString() + "," + Data.ProjectID.ToString() + "," + Data.CurrentProcessName;
                }
                bPrint = (LinkButton)e.Row.FindControl("imgOpenQC");
                if (bPrint != null)
                {
                    bPrint.CommandArgument = Data.ReceiveDetailID.ToString() + "," + Data.ProjectID.ToString() + ",QC Assessment";
                }

                bPrint = (LinkButton)e.Row.FindControl("imgLastUser");
                if (bPrint != null)
                {
                    bPrint.ToolTip = "Last Update User: " + Data.LastUpdateUser + " (" + Data.LastUpdateDate.ToString() + ")";
                    bPrint.CommandArgument = Data.ReceiveDetailID.ToString() + "," + Data.ProjectID.ToString() + ",LastUserData";
                }


                bPrint = (LinkButton)e.Row.FindControl("imgAnalyze");
                if (bPrint != null)
                {
                    bPrint.CommandArgument = Data.ReceiveDetailID.ToString();
                }

                bPrint = (LinkButton)e.Row.FindControl("imgBagTag");
                if (bPrint != null)
                {
                    bPrint.CommandArgument = Data.ReceiveDetailID.ToString();
                }

                bPrint = (LinkButton)e.Row.FindControl("ImageKitting");
                if (bPrint != null)
                {
                    ProcessManager pm = new ProcessManager(User.Identity.Name);
                    decimal.TryParse(Data.ReceiveDetailID.ToString(), out id);
                    if (pm.HasUnitEnteredAtleasetOneofTheseProcesses(id, "KITTING,SHIPPING GMP SALES") == false) { bPrint.Visible = false; }
                    else { bPrint.CommandArgument = Data.ReceiveDetailID.ToString(); }
                }


                bPrint = (LinkButton)e.Row.FindControl("ImageResetBin");
                if (bPrint != null)
                {
                    bPrint.CommandArgument = Data.ReceiveDetailID.ToString();
                }
                bPrint = (LinkButton)e.Row.FindControl("imgCorrect");
                if (bPrint != null)
                {
                    bPrint.CommandArgument = Data.ReceiveDetailID.ToString() + "," + Data.ESN;
                }
                bPrint = (LinkButton)e.Row.FindControl("ImgPartDetails");
                if (bPrint != null)
                {
                    bPrint.Visible = false;
                    bPrint.CommandArgument = Data.ReceiveDetailID.ToString() + "," + Data.ESN;
                    if (User.IsInRole(CorrectPartsRole))
                    {
                        bPrint.Visible = true;
                    }
                }




                //HiddenField hField = (HiddenField)e.Row.FindControl("hdnProcessName");
                //if (hField != null)
                //{
                //    hField.Value = Data.CurrentProcessName;
                //}
                //hField = (HiddenField)e.Row.FindControl("hdnReceiveDetailID");
                //if (hField != null)
                //{
                //    hField.Value = Data.ReceiveDetailID.ToString();
                //}
                //hField = (HiddenField)e.Row.FindControl("hdnProjectID");
                //if (hField != null)
                //{
                //    hField.Value = Data.ProjectID.ToString();
                //}



            }
        }

        void btnSearch_Click(object sender, EventArgs e)
        {
            string ProjectName = drpProjectList.SelectedItem.Text;

            BasicUserUtilities buu = new BasicUserUtilities(User.Identity.Name);
            string ClientKey = buu.GetUserValidClientKey(User.Identity.Name, txtClient.Text);
            string ProjectTag = txtProjectTag.Text;
            string RMANumber = txtRMA.Text;
            //string sVersion = sVersion

            string BeginDateString = txtBeginDate.Text;
            string EndDateString = txtEndDate.Text;
            string QCBeginDateString = txtBeginQC.Text;
            string QCEndDateString = txtEndQC.Text;
            string ShippedBeginDateString = txtBeginShipped.Text;
            string ShippedEndDateString = txtEndShipped.Text;
            string BinNumber = txtBinNumber.Text;
            string Hobble = txtHobble.Text;

            string sStatus = drpStatus.SelectedItem.Text;
            string sClient = "";
            string sIMEI = txtIMEI.Text;
            string sRemplacementIMEI = txtReplacementIMEI.Text;
            string sCarrier = drpCarrier.SelectedItem.Text;
            string sManufacturer = drpManufacturer.SelectedItem.Text;
            string sModel = drpModel.SelectedItem.Text;
            string sColour = drpColour.SelectedItem.Text;
            string sSKU = txtSKU.Text;

            string ifsSKU = txtIFSSku.Text;
            string ifsCondition = txtIFSCondtion.Text;
            string ifsLocation = txtIFSLocation.Text;

            if (ProjectName.ToUpper() == "ALL") { ProjectName = ""; }
            if (sStatus.ToUpper() == "ALL") { sStatus = ""; }
            if (sCarrier.ToUpper() == "ALL") { sCarrier = ""; }
            if (sManufacturer.ToUpper() == "ALL") { sManufacturer = ""; }
            if (sModel.ToUpper() == "ALL") { sModel = ""; }
            if (sColour.ToUpper() == "ALL") { sColour = ""; }

            char ShowGraveYard = 'N';

            // if (chkShowGraveyard.Checked == true) { ShowGraveYard = "Y"; }
            if (chkReceived.Checked == false) { BeginDateString = ""; EndDateString = ""; }
            if (chkQC.Checked == false) { QCBeginDateString = ""; QCEndDateString = ""; }
            if (chkShipped.Checked == false) { ShippedBeginDateString = ""; ShippedEndDateString = ""; }
            if (chkShowGraveyard.Checked == true) { ShowGraveYard = 'Y'; ShowGraveYard = 'Y'; }
            ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
            List<GetMasterDetailInventoryList_TemplateRawData_SearchGrid_02Result> Data = rdm.GetMasterDetailInventoryList(ProjectName, ClientKey,
                RMANumber, ProjectTag, BeginDateString, EndDateString, QCBeginDateString, QCEndDateString, ShippedBeginDateString, ShippedEndDateString,
                BinNumber, Hobble, sStatus, sClient, sIMEI, sCarrier, sManufacturer, sModel, sColour, sSKU, ShowGraveYard, sRemplacementIMEI, ifsSKU, ifsLocation, ifsCondition);
            grdTempDetail.DataSource = Data;
            grdTempDetail.DataBind();
        }





        public string ParamaterString_Raw()
        {
            string rString = "";

            string ProjectName = drpProjectList.SelectedItem.Text;

            BasicUserUtilities buu = new BasicUserUtilities(User.Identity.Name);
            string ClientKey = buu.GetUserDefaultClientKey(User.Identity.Name);
            string ProjectTag = "";      // txtProjectTag.Text;
            string RMANumber = "";          // txtRMA.Text;

            string BeginDateString = txtBeginDate.Text;
            string EndDateString = txtEndDate.Text;
            string QCBeginDateString = txtBeginQC.Text;
            string QCEndDateString = txtEndQC.Text;
            string ShippedBeginDateString = txtBeginShipped.Text;
            string ShippedEndDateString = txtEndShipped.Text;
            string BinNumber = "";           // txtBinNumber.Text;
            string Hobble = "";           // txtHobble.Text;

            string sStatus = drpStatus.SelectedItem.Text;
            string sClient = "";
            string sIMEI = txtIMEI.Text;
            string sCarrier = drpCarrier.SelectedItem.Text;
            string sManufacturer = drpManufacturer.SelectedItem.Text;
            string sModel = drpModel.SelectedItem.Text;
            string sColour = drpColour.SelectedItem.Text;
            string sSKU = txtSKU.Text;

            if (ProjectName.ToUpper() == "ALL") { ProjectName = ""; }
            if (sStatus.ToUpper() == "ALL") { sStatus = ""; }
            if (sCarrier.ToUpper() == "ALL") { sCarrier = ""; }
            if (sManufacturer.ToUpper() == "ALL") { sManufacturer = ""; }
            if (sModel.ToUpper() == "ALL") { sModel = ""; }
            if (sColour.ToUpper() == "ALL") { sColour = ""; }

            string ShowGraveYard = "N";


            // if (chkShowGraveyard.Checked == true) { ShowGraveYard = "Y"; }
            if (chkReceived.Checked == false) { BeginDateString = ""; EndDateString = ""; }
            if (chkQC.Checked == false) { QCBeginDateString = ""; QCEndDateString = ""; }
            if (chkShipped.Checked == false) { ShippedBeginDateString = ""; ShippedEndDateString = ""; }

            rString = " '" + ProjectName + "',";
            rString += "'" + ClientKey + "',";
            rString += "'" + RMANumber + "',";
            rString += "'" + ProjectTag + "',";
            rString += "'" + BeginDateString + "',";
            rString += "'" + EndDateString + "',";
            rString += "'" + QCBeginDateString + "',";
            rString += "'" + QCEndDateString + "',";
            rString += "'" + ShippedBeginDateString + "',";
            rString += "'" + ShippedEndDateString + "',";
            rString += "'" + BinNumber + "',";
            rString += "'" + Hobble + "',";

            rString += "'" + sStatus + "',";
            rString += "'" + sClient + "',";
            rString += "'" + sIMEI + "',";
            rString += "'" + sCarrier + "',";
            rString += "'" + sManufacturer + "',";
            rString += "'" + sModel + "',";
            rString += "'" + sColour + "',";
            rString += "'" + sSKU + "',";


            rString += "'" + ShowGraveYard + "'";
            return rString;

            // @mProjectName nvarchar(50) = '',
            // @mClientCode nvarchar(50) = '',        -- Location/Dealer	
            // @mRMANumber nvarchar(50) = '',
            // @mProjectTag nvarchar(50) = '',
            // @mReceiveBeginDate nvarchar(10) = '',  -- Rx Date	
            // @mReceiveEndDate nvarchar(10) = '',
            // @mQCBeginDate nvarchar(10) = '',    
            // @mQCEndDate nvarchar(10) = '',  
            // @mShippedBeginDate nvarchar(10) = '',    
            // @mShippedEndDate nvarchar(10) = '',
            // @mBinNumber nvarchar(50) = '',  
            // @mHobble nvarchar(50) = '',  
            // -- Aditional Template fields.
            // @mStatus nvarchar(50) = '',
            // @mClient nvarchar(50) = '',  
            // @mIMEI nvarchar(50) = '',  	
            // @mCarrier nvarchar(50) = '',  
            // @mManufacturer nvarchar(50) = '',  	
            // @mModel nvarchar(50) = '',  
            // @mColour nvarchar(50) = '',  
            // @mSKU nvarchar(50) = '',  
            // @mShowGraveyard char(1) = 'N'   


        }




    }
}