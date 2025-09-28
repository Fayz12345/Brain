using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
//using Factory_DataModel;
using BW_WebApp.DataManagers;

//using System.Drawing;
//using System.Reflection;
//using System.Web.UI;
//using Syncfusion.Web.UI.WebControls.Grid.Grouping;
//using System.Data;


using Syncfusion.Web.UI.WebControls.Tools;



namespace BW_WebApp
{
    public partial class Dashboard_FastTrack : System.Web.UI.Page
    {


        private Syncfusion.Web.UI.WebControls.Tools.TreeView tview;
        protected void Page_Load(object sender, EventArgs e)
        {
            //MainGrid.SelectedIndexChanged += new EventHandler(MainGrid_SelectedIndexChanged);
            //grdMasterBucketTransaction.SelectedIndexChanged += new EventHandler(grdMasterBucketTransaction_SelectedIndexChanged);
            //btnPrintScanCodes.Click += new EventHandler(btnPrintScanCodes_Click);
            drpProjectList.SelectedIndexChanged += new EventHandler(drpProjectList_SelectedIndexChanged);
            //btnCopyFrom.Click += new EventHandler(btnCopyFrom_Click);
            //btnCopyFrom.Attributes.Add("OnClick", "MoveProcessIn(); return false;");

            //ChildGrid.SelectedIndexChanged += new EventHandler(ChildGrid_SelectedIndexChanged);
            if (!IsPostBack)
            {
                hdnUserName.Value = User.Identity.Name;
                clsLinqDataContext ctx = new clsLinqDataContext();
                // ProcessManager qm = new ProcessManager(User.Identity.Name);
                //pnlAdd.Visible = false;
                //pnlEdit.Visible = false;
                //pnlProcessAnswer.Visible = false;
                //pnlProcessNextMove.Visible = false;
                //pnlProcessBinLocation.Visible = false;
                //pnlChild.Visible = false;
                //pnlAddOption.Visible = false;
                //pnlEditOption.Visible = false;

                //drpAddStatus.DataValueField = "ProcessStatusID";
                //drpAddStatus.DataTextField = "Status";
                //drpEditStatus.DataValueField = "ProcessStatusID";
                //drpEditStatus.DataTextField = "Status";
                //drpAddStatus.DataSource = (from x in ctx.ProcessStatus.OrderBy(y => y.Status) select new { x.ProcessStatusID, x.Status });
                //drpEditStatus.DataSource = (from x in ctx.ProcessStatus.OrderBy(y => y.Status) select new { x.ProcessStatusID, x.Status });
                //drpAddStatus.DataBind();
                //drpEditStatus.DataBind();
                BasicUserUtilities buu = new BasicUserUtilities(User.Identity.Name);
                ProjectManager pm = new ProjectManager(User.Identity.Name);
                List<Project> pl = pm.GetProjectList();
                drpProjectList.Items.Clear();
                //ListItem z = new ListItem("All", "-1");
                //drpProjectList.Items.Add(z);
                foreach (Project p in pl)
                {
                    ListItem x = new ListItem(p.Name, p.ProjectID.ToString());
                    drpProjectList.Items.Add(x);
                }
                drpProjectList.SelectedIndex = 0;

                tview = this.TreeData;
                tview.Width = new Unit("100%");
                tview.Height = new Unit("100%");                // new Unit("445px");
                //tview.ShowLines = true;
                //tview.AutoFormat = "Office2007 Blue";
                tview.BorderStyle = BorderStyle.None;
                //tview.BorderWidth = new Unit("1px");
                UpdateMainGrid();



                //SetColourCriteria();
                //UpdateMainGrid();
                //DataBind();
            }
            ////this.MainGrid_B.Appearance.AlternateRecordFieldCell.Interior = new Syncfusion.Drawing.BrushInfo(System.Drawing.Color.PaleGoldenrod);
            //this.MainGrid_B.Appearance.AlternateRecordFieldCell.Interior = new Syncfusion.Drawing.BrushInfo(System.Drawing.ColorTranslator.FromHtml("#E2E2E2"));

            ////btnAllRight.Attributes.Add("click", "MoveItem(lstSource,lstTarget);return false;");
            ////btnRight.Attributes.Add("click", "MoveItem(lstSource,lstTarget);return false;");
            ////btnAllLeft.Attributes.Add("click", "MoveItem(lstTarget,lstSource);return false;");
            ////btnLeft.Attributes.Add("click", "MoveItem(lstTarget,lstSource);return false;");
        }


        protected void UpdateMainGrid()
        {
            try
            {
                tview = this.TreeData;
                tview.Items.Clear();

                //ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
                //var data = rdm.GetAvailableStock_ManufacturerLinq("").ToList();

                string ProjectName = "";
                //decimal ProjectID = -1;
                //decimal.TryParse(drpProjectList.SelectedItem.Value, out ProjectID);
                if (drpProjectList.SelectedItem != null)
                {
                    ProjectName = drpProjectList.SelectedItem.Text;
                }
                ProcessManager qm = new ProcessManager(User.Identity.Name);
                var data = from x in qm.GetProcesssWaitTimeSummaryGridData(ProjectName, "", "", "", "")
                           select x;

                //    //MainGrid_B.DataSource = (from x in qm.GetProcesssWaitTimeGridData(ProjectName, "", "", "", "")
                //    //                         select new ProcessWaitTimeGridData(x.ReceiveDetailID, 
                //    //                             x.ESN, 
                //    //                             x.Version,
                //    //                             x.ProcessID, 
                //    //                             x.ProcessText, 
                //    //                             x.CreateDate, 
                //    //                             x.CreateUser,
                //    //                             x.InProcessSeconds, 
                //    //                             x.InProcessMinutes, 
                //    //                             x.InProcessHours, 
                //    //                             x.MinutesToYellow, 
                //    //                             x.MinutesToRed, 
                //    //                             x.ColourBand)).OrderByDescending(x=> x.InProcessSeconds).ToList();

                foreach (var d in data)
                {
                    string ProcessText = d.ProcessName;
                    string ColourBand = d.ColourBand;
                    if (ProcessText.Trim().Length == 0) { ProcessText = "Blank"; }
                    if (ColourBand.ToUpper() == "BLINK") { ColourBand = "Too OLD"; }

                    TreeViewNode node = new TreeViewNode();
                    node.Look = "LookGood";
                    if (d.ColourBand.ToUpper() == "RED") { node.SetLook("LookRed"); }
                    if (d.ColourBand.ToUpper() == "YELLOW") { node.SetLook("LookYellow"); }


                    node.ToolTip = "Manufacturer:" + ProcessText;
                    node.Text = ProcessText + "(" + ColourBand + ") - " + d.Freq.ToString() + " units";                 //" + d.ProjectID.ToString() + " - " + d.ProcessID.ToString() ;
                    node.Value = d.ColourBand + "," + d.ProjectID.ToString() + "," + d.ProcessID.ToString() + ",-1";
                    //node.ImagePath = "folders.gif";
                    node.ExpandMode = TreeViewNodeExpandMode.ServerSideCallback;


                    tview.Items.Add(node);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void TreeView1_NodeExpanded(object sender, Syncfusion.Web.UI.WebControls.Tools.TreeViewNodeEventArgs e)
        {
            //string Index = "";
            //string Manufacturer = "";
            //string Model = "";
            //string Colour = "";
            //string Grade = "";
            //string Carrier = "";

            string Key = e.Node.Value;
            string[] Keys = Key.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            //Index = Keys[Keys.Length - 1];

            //for (int i = 0; i < Index.Length; i++)
            //{
            //    switch (Index.Substring(i, 1))
            //    {
            //        case "M":
            //            Manufacturer = Keys[i];
            //            break;
            //        case "m":
            //            Model = Keys[i];
            //            break;
            //        case "C":
            //            Colour = Keys[i];
            //            break;
            //        case "G":
            //            Grade = Keys[i];
            //            break;
            //        case "c":
            //            Carrier = Keys[i];
            //            break;
            //        default:
            //            break;
            //    }
            //}
            //if (chkViewGrade.Checked == true)
            //{

            //    if (Keys.Length == 2)
            //    {
            //        LoadManufacturerData(e.Node, Grade);
            //        return;
            //    }
            //    if (Keys.Length == 3)
            //    {
            //        LoadModel(e.Node, Manufacturer, Grade);
            //        return;
            //    }
            //    if (Keys.Length == 4)
            //    {
            //        LoadColour(e.Node, Manufacturer, Model, Grade);
            //        return;
            //    }
            //    if (Keys.Length == 5)
            //    {
            //        LoadDetail(e.Node, Manufacturer, Model, Colour, Grade);
            //        return;
            //    }
            //}
            //else
            //{
            //    if (Keys.Length == 2)
            //    {
            //        LoadModel(e.Node, Manufacturer, "");
            //        return;
            //    }
            //    if (Keys.Length == 3)
            //    {
            //        LoadColour(e.Node, Manufacturer, Model, "");
            //        return;
            //    }
            //    if (Keys.Length == 4)
            //    {
            //        LoadDetail(e.Node, Manufacturer, Model, Colour, "");
            //        return;
            //    }
            //}

            decimal ProjectID = -1;
            decimal ProcessID = -1;
            decimal ClientID = -1;
            if (decimal.TryParse(Keys[1], out ProjectID) == false) { ProjectID = -1; }
            if (decimal.TryParse(Keys[2], out ProcessID) == false) { ProcessID = -1; }
            if (decimal.TryParse(Keys[3], out ClientID) == false) { ClientID = -1; }

            LoadDetail(e.Node, Keys[0], ProjectID, ProcessID, ClientID);
            return;

        }


        // protected void LoadDetail(TreeViewNode Parentnode, string Manufacturer, string Model, string Colour, string Grade)
        protected void LoadDetail(TreeViewNode Parentnode, string ColourBand, decimal ProjectID, decimal ProcessID, decimal ClientID)
        {
            try
            {
                tview = this.TreeData;


                ProcessManager qm = new ProcessManager(User.Identity.Name);
                var data = from x in qm.GetProcesssWaitTime_DetailSliceGridData(ColourBand, ProjectID, ProcessID, ClientID)
                           select x;

                if (data != null)
                {
                    TreeViewTemplateControl tc = GenerateHeaderTemplate(ColourBand);
                    tview.Templates.Add(tc);
                    TreeViewNode node2 = new TreeViewNode();
                    node2.TemplateID = tc.ID;
                    Parentnode.Items.Add(node2);

                    #region FillTemplate
                    foreach (ProcessWaitTime_DetailSlice d in data.OrderByDescending(x => x.InProcessSeconds))
                    {
                        TreeViewTemplateControl tc1 = new TreeViewTemplateControl();
                        string TemplateName = "Detail" + ColourBand + d.ReceiveDetailID.ToString() + "_" + ProjectID.ToString() + "_" + ProcessID.ToString();
                        tc1.ID = TemplateName;

                        System.Web.UI.WebControls.ImageButton b1 = new System.Web.UI.WebControls.ImageButton();
                        b1.ImageUrl = "~/Images/closed.gif";
                        //b1.ImageUrl = "~/Images/green_light.png";
                        // b1.Text = "+";
                        b1.ToolTip = "Add Items";
                        b1.Width = new Unit("15px");
                        b1.Style["text-align"] = "center";
                        string onClickEvent = "btnClick(this,'" + TemplateName + "'); return false;";
                        b1.Attributes.Add("OnClick", onClickEvent);
                        tc1.Controls.Add(b1);



                        Label la6b = new Label();
                        la6b.ToolTip = "ESN";
                        la6b.Text = d.ESN + "(" + d.ProcessName + ")";
                        la6b.Width = new Unit("300px");
                        tc1.Controls.Add(la6b);


                        Label la7 = new Label();
                        la7.ToolTip = "Date/Time Unit went into Process";
                        la7.Text = d.CreateDate.ToShortDateString();
                        la7.Width = new Unit("200px");
                        tc1.Controls.Add(la7);


                        //Label la6 = new Label();
                        //la6.ToolTip = "Process";
                        //la6.Text = d.ProcessText;
                        ////la6.Width = new Unit("50px");
                        //tc1.Controls.Add(la6);



                        Label la1 = new Label();
                        la1.ToolTip = "Work Minutes in Process";
                        la1.Text = string.Format("{0:N2}", d.InprocessMinutes);
                        la1.Width = new Unit("150px");
                        tc1.Controls.Add(la1);

                        Label lHours = new Label();
                        lHours.ToolTip = "Work Hours in Process";
                        lHours.Text = string.Format("{0:N2}", d.InprocessMinutes / 60);
                        lHours.Width = new Unit("150px");
                        tc1.Controls.Add(lHours);

                        Label lDays = new Label();
                        lDays.ToolTip = "Work Days in Process";
                        lDays.Text = string.Format("{0:N2}", d.InprocessMinutes / 60 / 8);
                        lDays.Width = new Unit("150px");
                        tc1.Controls.Add(lDays);

                        //Label la2 = new Label();
                        //la2.ToolTip = "Model";
                        //la2.Text = Model;
                        //la2.Width = new Unit("100px");
                        //tc1.Controls.Add(la2);
                        //Label la3 = new Label();
                        //la3.ToolTip = "Colour";
                        //la3.Text = Colour;
                        //la3.Width = new Unit("50px");
                        //tc1.Controls.Add(la3);
                        //Label la4 = new Label();
                        //la4.ToolTip = "Grade";
                        //la4.Text = d.Grade;
                        //la4.Width = new Unit("50px");
                        //tc1.Controls.Add(la4);
                        //Label la5 = new Label();
                        //la5.ToolTip = "Carrier";
                        //la5.Text = d.Carrier;
                        //la5.Width = new Unit("120px");
                        //tc1.Controls.Add(la5);

                        ////Label l1 = new Label();
                        ////l1.Text = "Selling qty:";
                        ////TextBox t1 = new TextBox();
                        ////t1.ToolTip = "QTY";
                        ////t1.Width = new Unit("20px");




                        ////Button b1 = new Button();
                        ////b1.Text = "Sell";

                        ////string onClickEvent = "SellButton_Click(this,'" + TemplateName + "'); return false;";
                        ////b1.Attributes.Add("OnClick", onClickEvent);

                        ////tc1.Controls.Add(l1);
                        ////tc1.Controls.Add(t1);

                        ////tc1.Controls.Add(l2);
                        ////tc1.Controls.Add(t2);

                        ////tc1.Controls.Add(b1);
                        tview.Templates.Add(tc1);

                        TreeViewNode node3 = new TreeViewNode();
                        //node2.Text = "xsdcfv";
                        node3.Value = d.ColourBand + "," + d.ProjectID.ToString() + "," + d.ProcessID.ToString() + "," + d.ClientID.ToString();
                        node3.Selectable = true;
                        node3.TemplateID = TemplateName;
                        //node.ImagePath = "folders.gif";
                        //node.ExpandMode = TreeViewNodeExpandMode.ServerSideCallback;
                        Parentnode.Items.Add(node3);

                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private static TreeViewTemplateControl GenerateHeaderTemplate(string ColourBand)
        {
            TreeViewTemplateControl tc = new TreeViewTemplateControl();
            tc.ID = "Header" + ColourBand;

            Label l1x = new Label();
            l1x.Text = "ESN/Process";
            l1x.Width = new Unit("315px");
            l1x.BackColor = System.Drawing.Color.Ivory;
            tc.Controls.Add(l1x);

            Label la6x = new Label();
            la6x.ToolTip = "Date/Time Unit went into Process";
            la6x.Text = "Date";
            la6x.Width = new Unit("200px");
            la6x.BackColor = l1x.BackColor;
            tc.Controls.Add(la6x);

            Label la6y = new Label();
            la6y.ToolTip = "Work Minutes in Process";
            la6y.Text = "Minutes";
            la6y.Width = new Unit("150px");
            la6y.BackColor = System.Drawing.Color.Ivory;
            tc.Controls.Add(la6y);

            Label lHours = new Label();
            lHours.ToolTip = "Work Hours in Process";
            lHours.Text = "Hours";
            lHours.Width = new Unit("150px");
            lHours.BackColor = System.Drawing.Color.Ivory;
            tc.Controls.Add(lHours);

            Label lDays = new Label();
            lDays.ToolTip = "Work Days in Process";
            lDays.Text = "Days";
            lDays.Width = new Unit("150px");
            lDays.BackColor = System.Drawing.Color.Coral;
            tc.Controls.Add(lDays);
            //Label la1x = new Label();
            //la1x.ToolTip = "Manufacturer";
            //la1x.Text = "Manufacturer";
            //la1x.Width = new Unit("100px");
            //la1x.BackColor = la6x.BackColor;
            //tc.Controls.Add(la1x);

            //Label la2x = new Label();
            //la2x.ToolTip = "Model";
            //la2x.Text = "Model";
            //la2x.Width = new Unit("100px");
            //la2x.BackColor = la6x.BackColor;
            //tc.Controls.Add(la2x);

            //Label la3x = new Label();
            //la3x.ToolTip = "Colour";
            //la3x.Text = "Colour";
            //la3x.Width = new Unit("50px");
            //la3x.BackColor = la6x.BackColor;
            //tc.Controls.Add(la3x);

            //Label la4x = new Label();
            //la4x.ToolTip = "Grade";
            //la4x.Text = "Grade";
            //la4x.Width = new Unit("50px");
            //la4x.BackColor = la6x.BackColor;
            //tc.Controls.Add(la4x);

            //Label la5x = new Label();
            //la5x.ToolTip = "Carrier";
            //la5x.Text = "Carrier";
            //la5x.Width = new Unit("120px");
            //la5x.BackColor = la6x.BackColor;
            //tc.Controls.Add(la5x);

            return tc;
        }





        //void SetColourCriteria()
        //{
        //    GridConditionalFormatDescriptor gcfd1 = new GridConditionalFormatDescriptor();
        //    gcfd1.Name = "CriteriaRed";
        //    gcfd1.Expression = "[ColourBand] Like 'Red'";
        //    Color c1 = Color.FromName("Red");
        //    gcfd1.Appearance.AnyRecordFieldCell.TextColor = c1;
        //    gcfd1.Appearance.AnyRecordFieldCell.Font.Bold = false; // boldProduct.Checked;
        //    gcfd1.Appearance.AnyRecordFieldCell.Font.Underline = false; //  underlineProduct.Checked;
        //    gcfd1.Appearance.AnyRecordFieldCell.Font.Strikeout = false; //  strikeoutProduct.Checked;
        //    gcfd1.Appearance.AnyRecordFieldCell.Font.Italic = false; //  italicProduct.Checked;
        //    this.MainGrid_B.TableDescriptor.ConditionalFormats.Add(gcfd1);

        //    GridConditionalFormatDescriptor gcfd2 = new GridConditionalFormatDescriptor();
        //    gcfd1.Name = "CriteriaYellow";
        //    gcfd1.Expression = "[ColourBand] Like 'Yellow'";
        //    Color c2 = Color.FromName("Yellow");
        //    gcfd1.Appearance.AnyRecordFieldCell.TextColor = c2;
        //    gcfd1.Appearance.AnyRecordFieldCell.Font.Bold = false; // boldProduct.Checked;
        //    gcfd1.Appearance.AnyRecordFieldCell.Font.Underline = false; //  underlineProduct.Checked;
        //    gcfd1.Appearance.AnyRecordFieldCell.Font.Strikeout = false; //  strikeoutProduct.Checked;
        //    gcfd1.Appearance.AnyRecordFieldCell.Font.Italic = false; //  italicProduct.Checked;
        //    this.MainGrid_B.TableDescriptor.ConditionalFormats.Add(gcfd2);

        //}


        void drpProjectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMainGrid();
        }

        //protected void UpdateMainGrid()
        //{
        //    string ProjectName = "";
        //    //decimal ProjectID = -1;
        //    //decimal.TryParse(drpProjectList.SelectedItem.Value, out ProjectID);
        //    if (drpProjectList.SelectedItem != null)
        //    {
        //        ProjectName = drpProjectList.SelectedItem.Text;
        //    }
        //    ProcessManager qm = new ProcessManager(User.Identity.Name);
        //    //MainGrid_B.DataSource = (from x in qm.GetProcesssWaitTimeGridData(ProjectName, "", "", "", "")
        //    //                         select new ProcessWaitTimeGridData(x.ReceiveDetailID, 
        //    //                             x.ESN, 
        //    //                             x.Version,
        //    //                             x.ProcessID, 
        //    //                             x.ProcessText, 
        //    //                             x.CreateDate, 
        //    //                             x.CreateUser,
        //    //                             x.InProcessSeconds, 
        //    //                             x.InProcessMinutes, 
        //    //                             x.InProcessHours, 
        //    //                             x.MinutesToYellow, 
        //    //                             x.MinutesToRed, 
        //    //                             x.ColourBand)).OrderByDescending(x=> x.InProcessSeconds).ToList();
        //}

    }

    [Serializable()]
    public class ProcessWaitTimeGridData
    {


        public decimal ReceiveDetailID { get; set; }
        public string ESN { get; set; }
        public string Version { get; set; }
        public decimal ProcessID { get; set; }
        public string ProcessText { get; set; }
        public DateTime CreateDate { get; set; }

        public string CreateUser { get; set; }
        public decimal InProcessSeconds { get; set; }
        public decimal InProcessMinutes { get; set; }
        public decimal InProcessHours { get; set; }
        public decimal MinutesToYellow { get; set; }
        public decimal MinutesToRed { get; set; }

        public string ColourBand { get; set; }

        public ProcessWaitTimeGridData(decimal receiveDetailID, string eSN, string version
            , decimal processid, string processText, DateTime createDate, string createUser,
              decimal inProcessSeconds, decimal inProcessMinutes, decimal inProcessHours, decimal minutesToYellow,
              decimal minutesToRed, string colourBand)
        {
            ReceiveDetailID = receiveDetailID;
            ESN = eSN;
            Version = version;
            ProcessID = processid;
            ProcessText = processText;

            CreateDate = createDate;

            CreateUser = createUser;
            InProcessSeconds = inProcessSeconds;
            InProcessMinutes = inProcessMinutes;
            InProcessHours = inProcessHours;
            MinutesToYellow = minutesToYellow;
            MinutesToRed = minutesToRed;
            ColourBand = colourBand;
        }
    }


    //[Serializable()]
    //public class ProcessGridData
    //{
    //    public string ScanKey { get; set; }
    //    public string MacroKey { get; set; }
    //    public string Name { get; set; }
    //    public decimal ProcessID { get; set; }
    //    public string Description { get; set; }
    //    public string Description_Client { get; set; }
    //    public string Status { get; set; }
    //    public int Sequence { get; set; }
    //    public bool? AllowAdd { get; set; }
    //    public bool? AllowDelete { get; set; }
    //    public bool? AllowScan { get; set; }
    //    public bool? AllowSelect { get; set; }
    //    public bool? AllowUpdate { get; set; }
    //    public bool? ShowCompletedStatus { get; set; }
    //    public string ButtonText { get; set; }
    //    public string RMASuffix { get; set; }
    //    public bool? ShowTAT { get; set; }
    //    public bool? CanJumpProject { get; set; }
    //    public bool? isReadOnly { get; set; }
    //    public bool? TurnStickyOn { get; set; }
    //    public ProcessGridData(string scankey, string macrokey, string name
    //        , decimal processid, string description, string description_client, string status,
    //          int sequence, bool? allowadd, bool? allowdelete, bool? allowscan,
    //          bool? allowselect, bool? allowupdate, bool? showcompletedstatus, string buttontext, string rmasuffix,
    //          bool? showtat, bool? canjumpproject, bool? isreadonly, bool? turnstickyon)
    //    {
    //        ScanKey = scankey;
    //        MacroKey = macrokey;
    //        Name = name;
    //        ProcessID = processid;
    //        Description = description;
    //        Description_Client = description_client;
    //        Status = status;
    //        Sequence = sequence;
    //        AllowAdd = allowadd;
    //        AllowDelete = allowdelete;
    //        AllowScan = allowscan;
    //        AllowSelect = allowselect;
    //        AllowUpdate = allowupdate;
    //        ShowCompletedStatus = showcompletedstatus;
    //        ButtonText = buttontext;
    //        RMASuffix = rmasuffix;
    //        ShowTAT = showtat;
    //        CanJumpProject = canjumpproject;
    //        isReadOnly = isreadonly;
    //        TurnStickyOn = turnstickyon;
    //    }





    //}
}