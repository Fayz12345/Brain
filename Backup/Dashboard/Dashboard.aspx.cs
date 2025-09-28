using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using BW_WebApp.DataManagers;

namespace BW_WebApp
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            btnRefresh.Click += new EventHandler(UpdateStats);
            if (IsPostBack == false)
            {
                SetupParameters();
                LoadStats();
            }
        }

        void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadStats();
        }

        protected void UpdateStats(object sender, EventArgs e)
        {
            LoadStats();
        }

        private void LoadStats()
        {

            //string ProjectName = drpProjectList.SelectedItem.Text;

            //BasicUserUtilities buu = new BasicUserUtilities(User.Identity.Name);
            //string ClientKey = buu.GetUserValidClientKey(User.Identity.Name, txtClient.Text);
            ////string ClientKey = "";
            //string ProjectTag = txtProjectTag.Text;
            //string RMANumber = txtRMA.Text;

            //string BeginDateString = txtBeginDate.Text;
            //string EndDateString = txtEndDate.Text;


            //string QCBeginDateString = txtBeginQC.Text;
            //string QCEndDateString = txtEndQC.Text;
            //string ShippedBeginDateString = txtBeginShipped.Text;
            //string ShippedEndDateString = txtEndShipped.Text;
            //string BinNumber = txtBinNumber.Text;
            //string Hobble = txtHobble.Text;

            //string sStatus = drpStatus.SelectedItem.Text;
            //string sClientID = drpClientList.SelectedItem.Value.ToString();
            //string sClient = "";
            //string sIMEI = txtIMEI.Text;
            //string sCarrier = drpCarrier.SelectedItem.Text;
            //string sManufacturer = drpManufacturer.SelectedItem.Text;
            //string sModel = drpModel.SelectedItem.Text;
            //string sColour = drpColour.SelectedItem.Text;
            //string sSKU = txtSKU.Text;
            //string UserName = drpUser.SelectedItem.Text;

            //if (UserName.ToUpper() == "ALL") { UserName = ""; }
            //if (ProjectName.ToUpper() == "ALL") { ProjectName = ""; }
            //if (sStatus.ToUpper() == "ALL") { sStatus = ""; }
            //if (sCarrier.ToUpper() == "ALL") { sCarrier = ""; }
            //if (sManufacturer.ToUpper() == "ALL") { sManufacturer = ""; }
            //if (sModel.ToUpper() == "ALL") { sModel = ""; }
            //if (sColour.ToUpper() == "ALL") { sColour = ""; }

            //char ShowGraveYard = 'N';
            //if (chkShowSummary.Checked == true) { ShowGraveYard = 'Y'; }


            //// if (chkShowGraveyard.Checked == true) { ShowGraveYard = "Y"; }
            //if (chkReceived.Checked == false) { BeginDateString = ""; EndDateString = ""; }
            //if (chkQC.Checked == false) { QCBeginDateString = ""; QCEndDateString = ""; }
            //if (chkShipped.Checked == false) { ShippedBeginDateString = ""; ShippedEndDateString = ""; }


            //decimal ClientID = -1;
            //if (decimal.TryParse(sClientID, out ClientID) == false) { ClientID = -1; }

            //DateUtils du = new DateUtils();
            //lblLastupdated.Text = DateTime.Now.ToString();


            //DateTime BeginDatePrior = DateTime.Now;
            //DateTime EndDatePrior = DateTime.Now;
            //string BeginDatePriorString = BeginDateString;
            //string EndDatePriorString = EndDateString;

            //if (chkReceived.Checked == false)
            //{
            //    BeginDateString = du.StartOfDay(DateTime.Now).ToString();
            //    EndDateString = du.EndOfDay(DateTime.Now).ToString();
            //    BeginDatePriorString = du.StartOfDay(du.YesterDay(BeginDatePrior)).ToString();
            //    EndDatePriorString = du.EndOfDay(du.YesterDay(EndDatePrior)).ToString();

            //    lblDateTime.Text = "From:" + BeginDateString + " To:" + EndDateString;
            //    lblPriorPeriod.Text = "From:" + BeginDatePriorString + " To:" + EndDatePriorString;
            //}
            //else
            //{
            //    // I need to look at the BeginDateString and EndDateString, go back to one date before and x days before that will become the begin date.

            //    int Days = du.DaysBetween(du.StringToDate(BeginDateString), du.StringToDate(EndDateString));
            //    EndDatePrior = du.StartOfDay(du.YesterDay(du.StringToDate(BeginDateString)));
            //    BeginDatePrior = du.StartOfDay(du.OffsetDays(EndDatePrior, Days * -1));

            //    lblDateTime.Text = "From:" + du.StartOfDay(du.StringToDate(BeginDateString)) + " To:" + du.EndOfDay(du.StringToDate(EndDateString));
            //    lblPriorPeriod.Text = "From:" + du.StartOfDay(BeginDatePrior) + " To:" + du.EndOfDay(EndDatePrior);
            //}

            ////StatisticalManager ms = new StatisticalManager(User.Identity.Name);
            //grdStatsToday.DataSource = ms.GetRawStatsSummarized(ClientID, ProjectName, ClientKey, RMANumber,
            //                                                    ProjectTag, du.MMDDYYYY(du.StringToDate(BeginDateString)), du.MMDDYYYY(du.StringToDate(EndDateString)), QCBeginDateString,
            //                                                    QCEndDateString, ShippedBeginDateString, ShippedEndDateString,
            //                                                    BinNumber, Hobble, sStatus, sClient, sIMEI, sCarrier, sManufacturer, sModel, sColour,
            //                                                    sSKU, ShowGraveYard, UserName);
            //grdStatsToday.DataBind();


            //grdStatsPrior.DataSource = ms.GetRawStatsSummarized(ClientID, ProjectName, ClientKey, RMANumber,
            //                                                    ProjectTag, du.MMDDYYYY(BeginDatePrior), du.MMDDYYYY(EndDatePrior), QCBeginDateString,
            //                                                    QCEndDateString, ShippedBeginDateString, ShippedEndDateString,
            //                                                    BinNumber, Hobble, sStatus, sClient, sIMEI, sCarrier, sManufacturer, sModel, sColour,
            //                                                    sSKU, ShowGraveYard, UserName);
            //grdStatsPrior.DataBind();


            //grdStatsMTD.DataSource = ms.GetRawStatsSummarized(ClientID, ProjectName, ClientKey, RMANumber,
            //                                                    ProjectTag, du.MMDDYYYY(du.StartOfMonth(DateTime.Now)), du.MMDDYYYY(du.EndOfMonth(DateTime.Now)), QCBeginDateString,
            //                                                    QCEndDateString, ShippedBeginDateString, ShippedEndDateString,
            //                                                    BinNumber, Hobble, sStatus, sClient, sIMEI, sCarrier, sManufacturer, sModel, sColour,
            //                                                    sSKU, ShowGraveYard, UserName);
            //grdStatsMTD.DataBind();


            //grdStatsYTD.DataSource = ms.GetRawStatsSummarized(ClientID, ProjectName, ClientKey, RMANumber,
            //                                                    ProjectTag, du.MMDDYYYY(du.StartOfYear(DateTime.Now)), du.MMDDYYYY(du.EndOfYear(DateTime.Now)), QCBeginDateString,
            //                                                    QCEndDateString, ShippedBeginDateString, ShippedEndDateString,
            //                                                    BinNumber, Hobble, sStatus, sClient, sIMEI, sCarrier, sManufacturer, sModel, sColour,
            //                                                    sSKU, ShowGraveYard, UserName);
            //grdStatsYTD.DataBind();
        }

        private void SetupParameters()
        {
            //DateUtils du = new DateUtils();
            //DateTime now = DateTime.Now;
            //lblNow.Text = now.ToString();

            //lblToday.Text = du.StartOfDay(now).ToString();

            //lblWeekStart.Text = du.StartOfWeek(now).ToString();
            //lblWeekEnd.Text = du.EndOfWeek(now).ToString();

            //lblMonthStart.Text = du.MMDDYYYY(du.StartOfMonth(now));
            //lblMonthEnd.Text = du.MMDDYYYY(du.EndOfMonth(now));
            //lblYearStart.Text = du.StartOfYear(now).ToString();
            //lblYearEnd.Text = du.EndOfYear(now).ToString();

            ClientManager cm = new ClientManager(User.Identity.Name);

            List<Client> cl = cm.SearchClientList("", "", "").OrderBy(x => x.CompanyName).ToList();
            drpClientList.Items.Clear();
            ListItem z = new ListItem("All", "-1");
            drpClientList.Items.Add(z);
            foreach (Client p in cl)
            {
                ListItem x = new ListItem(p.CompanyName, p.ClientID.ToString());
                drpClientList.Items.Add(x);
            }


            //drpClientList.DataValueField = "ClientID";
            //drpClientList.DataTextField = "CompanyName";
            //drpClientList.DataSource = cm.SearchClientList("", "", "").OrderBy(x => x.CompanyName);
            //drpClientList.DataBind();
            //drpClientList.SelectedIndex = 0;


            ProjectManager pm = new ProjectManager(User.Identity.Name);
            List<Project> pl = pm.GetProjectList();
            drpProjectList.Items.Clear();
            z = new ListItem("All", "-1");
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

            UserManager um = new UserManager(User.Identity.Name);
            List<string> ul = um.MasterUserList();
            if (ul.Count > 1) { drpUser.Items.Add(new ListItem("All", "-1")); }
            foreach (string s in ul)
            {
                drpUser.Items.Add(new ListItem(s, s));
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
        }


    }
}