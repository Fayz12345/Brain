using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
//using Factory_DataModel;
using BW_WebApp.DataManagers;
using Syncfusion.Web.UI.WebControls.Tools;

namespace BW_WebApp.Maintenance
{
    public partial class Maint_MasterCarrer_ColourUpdate : System.Web.UI.Page
    {

        private Syncfusion.Web.UI.WebControls.Tools.TreeView tview;
        protected void Page_Load(object sender, EventArgs e)
        {
            #region Header
            //btnBillClientSearch.Click += new EventHandler(btnBillClientSearch_Click);
            //btnShipClientSearch.Click += new EventHandler(btnShipClientSearch_Click);
            //btnBillClientEdit.Click += new EventHandler(btnBillClientEdit_Click);
            //btnShipClientEdit.Click += new EventHandler(btnShipClientEdit_Click);
            //btnEditNameAddressOK.Click += new EventHandler(btnEditNameAddressOK_Click);
            //btnEditNameAddressCancel.Click += new EventHandler(btnEditNameAddressCancel_Click);
            #endregion

            TreeData.NodeSelected += new Syncfusion.Web.UI.WebControls.Tools.TreeView.TreeViewNodeSelectEventHandler(TreeData_NodeSelected);
            btnSave.Click += new EventHandler(btnSave_Click);
            if (!IsPostBack)
            {
                hdnUserName.Value = User.Identity.Name;
                tview = this.TreeData;
                tview.Width = new Unit("100%");
                tview.Height = new Unit("100%");                // new Unit("445px");
                tview.BorderStyle = BorderStyle.None;
                LoadTreeDataTopLevel();
                BindQuestionsToList();
                //ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "ResetHistory();", true);
            }
        }

        void TreeData_NodeSelected(object sender, TreeViewNodeSelectEventArgs e)
        {
            string[] Keys = e.Node.Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            //drpManufacturer.Items.FindByText(Keys[0]);

            ListItem _ListItem = drpManufacturer.Items.FindByText(Keys[0].Trim());
            if (_ListItem == null) { drpManufacturer.SelectedIndex = 0; }
            else { drpManufacturer.SelectedIndex = drpManufacturer.Items.IndexOf(_ListItem); }

            _ListItem = drpModel.Items.FindByText(Keys[1].Trim());
            if (_ListItem == null) { drpModel.SelectedIndex = 0; }
            else { drpModel.SelectedIndex = drpModel.Items.IndexOf(_ListItem); }

            lblMessage.Text = "";                // e.Node.Value;
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            MasterCarrierManufacturerModelColourManager MM = new MasterCarrierManufacturerModelColourManager(User.Identity.Name);
            string UseThisModel = "";
            UseThisModel = drpModel.SelectedItem.Text;
            if (txtNewModel.Text.Length > 0)
            {
                UseThisModel = txtNewModel.Text;
            }
            lblMessage.Text = MM.UpdateStockColourCode(drpManufacturer.SelectedItem.Text, UseThisModel, drpEditStockColourCode.SelectedItem.Text);
        }

        void BindQuestionsToList()
        {
            QuestionManager qm = new QuestionManager(User.Identity.Name);
            List<Option> ol = qm.GetQuestionOptionList("Stock Colour Code");
            drpEditStockColourCode.Items.Clear();
            foreach (Option o in ol.OrderBy(x => x.Sequence))
            {
                ListItem x = new ListItem(o.OptionText.Trim(), o.OptionID.ToString());
                drpEditStockColourCode.Items.Add(x);
            }

            ol = qm.GetQuestionOptionList("Manufacturer");
            drpManufacturer.Items.Clear();
            foreach (Option o in ol.OrderBy(x => x.OptionText))
            {
                ListItem x = new ListItem(o.OptionText.Trim(), o.OptionID.ToString());
                drpManufacturer.Items.Add(x);
            }
            ol = qm.GetQuestionOptionList("Model");
            drpModel.Items.Clear();
            foreach (Option o in ol.OrderBy(x => x.OptionText))
            {
                ListItem x = new ListItem(o.OptionText.Trim(), o.OptionID.ToString());
                drpModel.Items.Add(x);
            }
        }

        //void chkViewGrade_CheckedChanged(object sender, EventArgs e)
        //{
        //    LoadTreeDataTopLevel();
        //    ResetData();
        //}

        //void btnReset_Click(object sender, EventArgs e)
        //{
        //    ResetData();
        //}
        //private void ResetData()
        //{
        //    tview = this.TreeData;
        //    foreach (TreeViewNode Manufacturer in tview.Items)
        //    {
        //        foreach (TreeViewNode Model in Manufacturer.Items)
        //        {
        //            foreach (TreeViewNode Colour in Model.Items)
        //            {
        //                if (Colour.Items.Count > 0)
        //                {
        //                    Colour.Items.Clear();
        //                    Colour.ExpandMode = TreeViewNodeExpandMode.ServerSideCallback;
        //                }
        //            }
        //        }
        //    }
        //    ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "RestoreBackEndData();showScreen('Detail');", true);
        //}
        //protected void LoadGradeData()
        //{
        //    try
        //    {
        //        tview = this.TreeData;
        //        tview.Items.Clear();
        //        ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
        //        //var data = rdm.GetAvailableStock_Grade().ToList();
        //        var data = rdm.GetAvailableStock_GradeLinq().ToList();
        //        foreach (var d in data)
        //        {
        //            string Grade = d.Grade;
        //            if (d.Grade.Trim().Length == 0) { Grade = "Blank"; }

        //            TreeViewNode node = new TreeViewNode();
        //            node.ToolTip = "Grade:" + Grade;
        //            node.Text = d.QTY.ToString() + "/" + d.QTYReserved.ToString() + " - " + Grade;
        //            node.Value = Grade + ",G";
        //            //node.ImagePath = "folders.gif";
        //            node.ExpandMode = TreeViewNodeExpandMode.ServerSideCallback;
        //            tview.Items.Add(node);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}



        protected void TreeView1_NodeExpanded(object sender, Syncfusion.Web.UI.WebControls.Tools.TreeViewNodeEventArgs e)
        {
            string Index = "";
            string Manufacturer = "";
            string Model = "";
            string Colour = "";
            string Grade = "";
            string Carrier = "";

            string Key = e.Node.Value;
            string[] Keys = Key.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            Index = Keys[Keys.Length - 1];

            for (int i = 0; i < Index.Length; i++)
            {
                switch (Index.Substring(i, 1))
                {
                    case "M":
                        Manufacturer = Keys[i];
                        break;
                    case "m":
                        Model = Keys[i];
                        break;
                    case "C":
                        Colour = Keys[i];
                        break;
                    case "G":
                        Grade = Keys[i];
                        break;
                    case "c":
                        Carrier = Keys[i];
                        break;
                    default:
                        break;
                }
            }
            if (Keys.Length == 2)
            {
                LoadModel(e.Node, Manufacturer, "");
                return;
            }
            //if (Keys.Length == 3)
            //{
            //    LoadColour(e.Node, Manufacturer, Model, "");
            //    return;
            //}
            //if (Keys.Length == 4)
            //{
            //    LoadDetail(e.Node, Manufacturer, Model, Colour, "");
            //    return;
            //}
        }
        //protected void LoadManufacturerData(TreeViewNode Parentnode, string Grade)
        //{
        //    try
        //    {
        //        ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
        //        var data = rdm.GetAvailableStock_ManufacturerLinq(Grade).ToList();
        //        foreach (var d in data)
        //        {
        //            string Manufacturer = d.Manufacturer;
        //            if (d.Manufacturer.Trim().Length == 0) { Manufacturer = "Blank"; }

        //            TreeViewNode node = new TreeViewNode();
        //            node.ToolTip = "Manufacturer:" + Manufacturer;
        //            node.Text = d.QTY.ToString() + "/" + d.QTYReserved.ToString() + " - " + Manufacturer;
        //            node.Value = Manufacturer + "," + Grade + ",MG";
        //            //node.ImagePath = "folders.gif";
        //            node.ExpandMode = TreeViewNodeExpandMode.ServerSideCallback;
        //            Parentnode.Items.Add(node);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        private void LoadTreeDataTopLevel()
        {
            //if (chkViewGrade.Checked == true) { LoadGradeData(); }
            //else { 
            LoadManufacturerData();
            //}
        }
        protected void LoadManufacturerData()
        {
            try
            {
                tview = this.TreeData;
                tview.Items.Clear();
                //ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
                MasterCarrierManufacturerModelColourManager MM = new MasterCarrierManufacturerModelColourManager(User.Identity.Name);
                var data = MM.GetAvailableStock_ManufacturerLinq("").ToList();
                foreach (var d in data.OrderBy(x => x.Manufacturer))
                {
                    string Manufacturer = d.Manufacturer;
                    if (d.Manufacturer.Trim().Length == 0) { Manufacturer = "Blank"; }

                    TreeViewNode node = new TreeViewNode();
                    node.ToolTip = "Manufacturer:" + Manufacturer;
                    node.Text = d.QTY.ToString() + "/" + d.QTYReserved.ToString() + " - " + Manufacturer;
                    node.Value = Manufacturer + ",M";
                    //node.ImagePath = "folders.gif";
                    node.AutoPostBackOnNodeSelect = Syncfusion.Web.UI.WebControls.Tools.TreeViewControl.InheritableBool.False;
                    node.ExpandMode = TreeViewNodeExpandMode.ServerSideCallback;
                    tview.Items.Add(node);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void LoadModel(TreeViewNode Parentnode, string Manufacturer, string Grade)
        {
            try
            {

                //ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
                MasterCarrierManufacturerModelColourManager MM = new MasterCarrierManufacturerModelColourManager(User.Identity.Name);
                var data = MM.GetAvailableStock_ModelLinq(Manufacturer, Grade).ToList();
                foreach (var d in data.OrderBy(x => x.Model))
                {
                    string Model = d.Model;
                    if (d.Model.Trim().Length == 0) { Model = "Blank"; }

                    TreeViewNode node = new TreeViewNode();
                    node.ToolTip = "Model:" + Model;
                    node.Text = d.QTY.ToString() + "/" + d.QTYReserved.ToString() + " - " + Model;
                    node.Value = Manufacturer + "," + Model + ",Mm";                     // Manufacturer:Model:Colour
                    if (Grade.Length > 0) { node.Value = Manufacturer + "," + Model + "," + Grade + ",MmG"; }
                    node.AutoPostBackOnNodeSelect = Syncfusion.Web.UI.WebControls.Tools.TreeViewControl.InheritableBool.True;
                    //node.ImagePath = "folders.gif";
                    //node.ExpandMode = TreeViewNodeExpandMode.ServerSideCallback;
                    Parentnode.Items.Add(node);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //protected void LoadColour(TreeViewNode Parentnode, string Manufacturer, string Model, string Grade)
        //{
        //    try
        //    {
        //        ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
        //        var data = rdm.GetAvailableStock_ColourLinq(Manufacturer, Model, Grade).ToList();
        //        foreach (var d in data)
        //        {
        //            string Colour = d.Colour;
        //            if (d.Colour.Trim().Length == 0) { Colour = "Blank"; }

        //            TreeViewNode node = new TreeViewNode();
        //            node.ToolTip = "Colour:" + Colour;


        //            node.Text = d.QTY.ToString() + "/" + d.QTYReserved.ToString() + " - " + Colour;
        //            node.Value = Manufacturer + "," + Model + "," + Colour + ",MmC";                     // Manufacturer:Model:Colour
        //            if (Grade.Length > 0) { node.Value = Manufacturer + "," + Model + "," + Colour + "," + Grade + ",MmCG"; }
        //            //node.ImagePath = "folders.gif";
        //            node.ExpandMode = TreeViewNodeExpandMode.ServerSideCallback;
        //            Parentnode.Items.Add(node);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //protected void LoadDetail(TreeViewNode Parentnode, string Manufacturer, string Model, string Colour, string Grade)
        //{
        //    try
        //    {
        //        tview = this.TreeData;
        //        ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
        //        var data = rdm.GetAvailableStock_ColourDetailLinq(Manufacturer, Model, Colour, Grade).ToList();
        //        //var data = rmd
        //        if (data != null)
        //        {
        //            TreeViewTemplateControl tc = GenerateHeaderTemplate(Manufacturer, Model, Colour);
        //            tview.Templates.Add(tc);
        //            TreeViewNode node2 = new TreeViewNode();
        //            node2.TemplateID = tc.ID;
        //            Parentnode.Items.Add(node2);

        //            #region FillTemplate
        //            foreach (var d in data)
        //            {
        //                TreeViewTemplateControl tc1 = new TreeViewTemplateControl();
        //                string TemplateName = "Detail" + Manufacturer + Model + Colour + d.Grade + d.Carrier;
        //                tc1.ID = TemplateName;

        //                System.Web.UI.WebControls.ImageButton b1 = new System.Web.UI.WebControls.ImageButton();
        //                b1.ImageUrl = "~/Images/closed.gif";
        //                //b1.ImageUrl = "~/Images/green_light.png";
        //                // b1.Text = "+";
        //                b1.ToolTip = "Add Items";
        //                b1.Width = new Unit("15px");
        //                b1.Style["text-align"] = "center";
        //                string onClickEventx = "SellButton_Click(this,'" + Manufacturer + "," + Model + "," + Colour + "," + d.Grade + "," + d.Carrier + ",MmCGc'); return false;";
        //                b1.Attributes.Add("OnClick", onClickEventx);
        //                tc1.Controls.Add(b1);

        //                Label la7 = new Label();
        //                la7.ToolTip = "";
        //                la7.Text = "";
        //                la7.Width = new Unit("15px");
        //                tc1.Controls.Add(la7);


        //                Label la6 = new Label();
        //                la6.ToolTip = "Stock QTY";
        //                la6.Text = d.QTY.ToString();
        //                la6.Width = new Unit("30px");
        //                tc1.Controls.Add(la6);

        //                Label la6b = new Label();
        //                la6b.ToolTip = "QTY on reserve";
        //                la6b.Text = d.QTYReserved.ToString();
        //                la6b.Width = new Unit("35px");
        //                tc1.Controls.Add(la6b);


        //                Label la1 = new Label();
        //                la1.ToolTip = "Manufacturer";
        //                la1.Text = Manufacturer;
        //                la1.Width = new Unit("100px");
        //                tc1.Controls.Add(la1);
        //                Label la2 = new Label();
        //                la2.ToolTip = "Model";
        //                la2.Text = Model;
        //                la2.Width = new Unit("100px");
        //                tc1.Controls.Add(la2);
        //                Label la3 = new Label();
        //                la3.ToolTip = "Colour";
        //                la3.Text = Colour;
        //                la3.Width = new Unit("50px");
        //                tc1.Controls.Add(la3);
        //                Label la4 = new Label();
        //                la4.ToolTip = "Grade";
        //                la4.Text = d.Grade;
        //                la4.Width = new Unit("50px");
        //                tc1.Controls.Add(la4);
        //                Label la5 = new Label();
        //                la5.ToolTip = "Carrier";
        //                la5.Text = d.Carrier;
        //                la5.Width = new Unit("120px");
        //                tc1.Controls.Add(la5);

        //                //Label l1 = new Label();
        //                //l1.Text = "Selling qty:";
        //                //TextBox t1 = new TextBox();
        //                //t1.ToolTip = "QTY";
        //                //t1.Width = new Unit("20px");




        //                //Button b1 = new Button();
        //                //b1.Text = "Sell";

        //                //string onClickEvent = "SellButton_Click(this,'" + TemplateName + "'); return false;";
        //                //b1.Attributes.Add("OnClick", onClickEvent);

        //                //tc1.Controls.Add(l1);
        //                //tc1.Controls.Add(t1);

        //                //tc1.Controls.Add(l2);
        //                //tc1.Controls.Add(t2);

        //                //tc1.Controls.Add(b1);
        //                tview.Templates.Add(tc1);

        //                TreeViewNode node3 = new TreeViewNode();
        //                //node2.Text = "xsdcfv";
        //                node3.Value = Manufacturer + "," + Model + "," + Colour + "," + d.Grade + "," + d.Carrier + ",MmCGc";
        //                //node3.Selectable = true;
        //                node3.TemplateID = TemplateName;
        //                //node.ImagePath = "folders.gif";
        //                //node.ExpandMode = TreeViewNodeExpandMode.ServerSideCallback;
        //                Parentnode.Items.Add(node3);

        //            }
        //            #endregion
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //private static TreeViewTemplateControl GenerateHeaderTemplate(string Manufacturer, string Model, string Colour)
        //{
        //    TreeViewTemplateControl tc = new TreeViewTemplateControl();
        //    tc.ID = "Header" + Manufacturer + Model + Colour;

        //    Label l1x = new Label();
        //    l1x.Text = "";
        //    l1x.Width = new Unit("30px");
        //    l1x.BackColor = System.Drawing.Color.Ivory;
        //    tc.Controls.Add(l1x);

        //    Label la6x = new Label();
        //    la6x.ToolTip = "Stock QTY";
        //    la6x.Text = "QTY";
        //    la6x.Width = new Unit("30px");
        //    la6x.BackColor = l1x.BackColor;
        //    tc.Controls.Add(la6x);

        //    Label la6y = new Label();
        //    la6y.ToolTip = "QTY on reserve";
        //    la6y.Text = "Hold";
        //    la6y.Width = new Unit("35px");
        //    la6y.BackColor = System.Drawing.Color.Ivory;
        //    tc.Controls.Add(la6y);

        //    Label la1x = new Label();
        //    la1x.ToolTip = "Manufacturer";
        //    la1x.Text = "Manufacturer";
        //    la1x.Width = new Unit("100px");
        //    la1x.BackColor = la6x.BackColor;
        //    tc.Controls.Add(la1x);

        //    Label la2x = new Label();
        //    la2x.ToolTip = "Model";
        //    la2x.Text = "Model";
        //    la2x.Width = new Unit("100px");
        //    la2x.BackColor = la6x.BackColor;
        //    tc.Controls.Add(la2x);

        //    Label la3x = new Label();
        //    la3x.ToolTip = "Colour";
        //    la3x.Text = "Colour";
        //    la3x.Width = new Unit("50px");
        //    la3x.BackColor = la6x.BackColor;
        //    tc.Controls.Add(la3x);

        //    Label la4x = new Label();
        //    la4x.ToolTip = "Grade";
        //    la4x.Text = "Grade";
        //    la4x.Width = new Unit("50px");
        //    la4x.BackColor = la6x.BackColor;
        //    tc.Controls.Add(la4x);

        //    Label la5x = new Label();
        //    la5x.ToolTip = "Carrier";
        //    la5x.Text = "Carrier";
        //    la5x.Width = new Unit("120px");
        //    la5x.BackColor = la6x.BackColor;
        //    tc.Controls.Add(la5x);

        //    return tc;
        //}
        #region HeaderSaveArea

        //void btnSaveSale_Click(object sender, EventArgs e)
        //{
        //    string[] ToAddress = new string[] { "", "", "" };
        //    string SubjectText = "";
        //    string key = "";
        //    key = hdnKeys.Value;
        //    if (key.Length > 0)
        //    {
        //        clsOrderHeader OH = GatherSheetData();
        //        //OH.SaveHeaderData();  // Need this save to get the OrderNumber
        //        //txtOrderNumber.Text = OH.OrderNumber;
        //        //SubjectText += OH.ClientCompany.CompanyName;
        //        using (clsLinqDataContext ctx = new clsLinqDataContext())
        //        {
        //            OrderManager OM = new OrderManager(User.Identity.Name);
        //            CompanyDemographics dm = new CompanyDemographics(User.Identity.Name);
        //            var user = Membership.GetUser(User.Identity.Name);
        //            ToAddress[0] = dm.OrderEntryEmail;              // "jim.willson@hotmail.com";
        //            ToAddress[2] = dm.OrderEntryEmail;              // "jim.willson@hotmail.com";
        //            if (user != null && user.Email != null && user.Email.Length > 0)
        //            {
        //                if (user.Email.ToUpper().Contains("@GMPI.CA"))
        //                {
        //                    ToAddress[1] = user.Email;
        //                    if (ToAddress[2].Length > 0) { ToAddress[2] += ";"; }
        //                    ToAddress[2] += user.Email;
        //                }
        //            }
        //            string[] Records = key.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
        //            string Index = "";
        //            foreach (string r in Records)
        //            {
        //                string[] Fields = r.Split(new char[] { ',' });
        //                int Qty = 0;
        //                if (int.TryParse(Fields[0], out Qty) == false) { return; }
        //                if (Qty > 0)
        //                {
        //                    clsOrderDetailLine a = new clsOrderDetailLine();
        //                    a.Manufacturer = "";
        //                    a.Model = "";
        //                    a.Colour = "";
        //                    a.Grade = "";
        //                    a.Carrier = "";
        //                    Index = Fields[Fields.Length - 1];
        //                    for (int i = 0; i < Index.Length; i++)
        //                    {
        //                        switch (Index.Substring(i, 1))
        //                        {
        //                            case "M":
        //                                a.Manufacturer = Fields[i + 1];
        //                                break;
        //                            case "m":
        //                                a.Model = Fields[i + 1];
        //                                break;
        //                            case "C":
        //                                a.Colour = Fields[i + 1];
        //                                break;
        //                            case "G":
        //                                a.Grade = Fields[i + 1];
        //                                break;
        //                            case "c":
        //                                a.Carrier = Fields[i + 1];
        //                                break;
        //                            default:
        //                                break;
        //                        }
        //                    }
        //                    string[] CodeText = OM.CodeDetailLine(ctx, a.Manufacturer, a.Model, a.Colour, a.Grade, a.Carrier);
        //                    a.QTY = Qty;
        //                    a.SKU = "";
        //                    a.Desc_Code = CodeText[0];
        //                    a.Desc_Text = CodeText[1];
        //                    a.AvailableStock_OrderNumber = "";
        //                    a.AvailableStock_QTY = Qty;
        //                    a.ReservedAvailableStockID = -1;
        //                    OH.OrderDetailLines.Add(a);
        //                }
        //            }
        //            //ctx.SubmitChanges();
        //        }
        //        OH.SaveHeaderData();
        //        LoadTreeDataTopLevel();
        //        btnSaveSale.Style["display"] = "none";
        //        hdnKeys.Value = "";
        //        hdnListHistoryValue.Value = "";
        //        hdnLstHistory.Value = "";
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Saved", @"alert('Order Saved:" + OH.OrderNumber + "\\nEmail Sent to:" + ToAddress[0] + " " + ToAddress[1] + "');", true);


        //        // Send out an email with the Order Specifics.
        //        string StockEmailSubject = "";
        //        txtOrderNumber.Text = OH.OrderNumber;
        //        SubjectText += OH.ClientCompany.CompanyName;
        //        if (drpAstockEmailSubject.SelectedIndex >= 0) { StockEmailSubject = drpAstockEmailSubject.SelectedItem.Text; }
        //        string Subject = "Order:" + SubjectText + " - " + OH.OrderNumber + " (" + hdntxtHistoryCount.Value + " units) " + StockEmailSubject;
        //        hdntxtHistoryCount.Value = "0";
        //        if (ToAddress[2] != null && ToAddress[2].Length > 0)
        //        {
        //            string[] url = HttpContext.Current.Request.Url.AbsoluteUri.Split('/');
        //            StringBuilder sb = new StringBuilder();
        //            for (int i = 0; i < url.Count() - 1; i++)   // This should stop short of picking up the "current" aspx and replace it with the proper form.
        //            {
        //                sb.Append(url[i]);
        //                sb.Append("/");
        //            }
        //            string aStock = "AvailableStockForm.aspx?Order=" + OH.OrderNumber;
        //            string Pick = "RPT_EXCEL_Out.aspx?RPT=PIC&ID=" + OH.OrderHeaderID.ToString();

        //            StringBuilder Body = new StringBuilder();
        //            Body.Append("<a href=" + Uri.EscapeUriString(sb.ToString() + aStock) + ">" + HttpUtility.HtmlEncode(OH.OrderNumber) + "</a><br/><br>");
        //            Body.Append("<a href=" + Uri.EscapeUriString(sb.ToString() + Pick) + ">" + HttpUtility.HtmlEncode(OH.OrderNumber) + " PickList" + "</a><br/>");

        //            SendEmail eMail = new SendEmail();
        //            eMail.Email(ToAddress[2], Body.ToString(), Subject);
        //        }
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('No data to save.');", true);
        //    }
        //}

        //clsOrderHeader GatherSheetData()
        //{
        //ProjectManager pm = new ProjectManager(User.Identity.Name);
        //decimal PID = pm.GetProjectID(Config.ProjectStockForSaleFrom);
        //decimal ID = -1;

        //// Header Data.
        //if (decimal.TryParse(hdnOrderHeaderID.Value, out ID) == false) { ID = -1; }
        //clsOrderHeader OH = new clsOrderHeader(User.Identity.Name);
        //OH.OrderHeaderID = ID;
        //OH.UserName = User.Identity.Name;
        //OH.CustomerPO = txtCustomerPONumber.Text;
        //OH.OrderNumber = "";                                      //    lblPurchaseOrderNumber.Text;
        //OH.WaybillNumber = txtWaybillNumber.Text;
        //OH.ProjectTag = txtProjectTag.Text;
        //OH.Paid = chkPaid.Checked;
        //OH.PostPaid = chkPostPaid.Checked;

        //OH.ProjectID = PID;

        //// Company Data
        //if (decimal.TryParse(hdnBillClientLocationID.Value, out ID) == false) { ID = -1; }
        //OH.ClientCompany.ClientLocationID = ID;
        //OH.ClientCompany.CompanyName = hdnBillCompanyName.Value;
        //OH.ClientCompany.ContactName = hdnBillContactName.Value;
        //OH.ClientCompany.AddressLine1 = hdnBillAddressLine1.Value;
        //OH.ClientCompany.AddressLine2 = hdnBillAddressLine2.Value;
        //OH.ClientCompany.City = hdnBillCity.Value;
        //OH.ClientCompany.StateOrProvince = hdnBillStateOrProvince.Value;
        //OH.ClientCompany.PostalCode = hdnBillPostalCode.Value;
        //OH.ClientCompany.PhoneNumber = hdnBillPhoneNumber.Value;
        //OH.ClientCompany.FaxNumber = hdnBillFaxNumber.Value;
        //OH.ClientCompany.Notes = hdnBillNotes.Value;
        //OH.ClientCompany.CompanyType = "Client";

        //if (decimal.TryParse(hdnShipClientLocationID.Value, out ID) == false) { ID = -1; }
        //OH.ShipToCompany.ClientLocationID = ID;
        //OH.ShipToCompany.CompanyName = hdnShipCompanyName.Value;
        //OH.ShipToCompany.ContactName = hdnShipContactName.Value;
        //OH.ShipToCompany.AddressLine1 = hdnShipAddressLine1.Value;
        //OH.ShipToCompany.AddressLine2 = hdnShipAddressLine2.Value;
        //OH.ShipToCompany.City = hdnShipCity.Value;
        //OH.ShipToCompany.StateOrProvince = hdnShipStateOrProvince.Value;
        //OH.ShipToCompany.PostalCode = hdnShipPostalCode.Value;
        //OH.ShipToCompany.PhoneNumber = hdnShipPhoneNumber.Value;
        //OH.ShipToCompany.FaxNumber = hdnShipFaxNumber.Value;
        //OH.ShipToCompany.Notes = hdnShipNotes.Value;
        //OH.ShipToCompany.CompanyType = "ShipTo";

        //OH.DeliveryNote = txtDeliveryNote.Text;
        //OH.InternalNote = txtInternalNote.Text;

        ////////////////////////////////////////////////////////////

        //////Get Line Detail
        ////// get the data in the grid first.
        ////foreach (GridViewRow r in grdNewOrderDetailGrid.Rows)
        ////{
        ////    clsOrderDetailLine l = new clsOrderDetailLine();
        ////    decimal num = 0;
        ////    int _i = 0;
        ////    string str = (r.Cells[7].Text == blank ? "-1" : r.Cells[7].Text);
        ////    if (decimal.TryParse(str, out num) == false) { num = -1; }

        ////    l.OrderDetailID = num;
        ////    str = (r.Cells[1].Text == blank ? "0" : r.Cells[1].Text);
        ////    if (decimal.TryParse(str, out num) == false) { num = 0; }

        ////    l.QTY = num;
        ////    l.SKU = (r.Cells[4].Text == blank ? "" : r.Cells[4].Text);
        ////    l.Desc_Code = (r.Cells[5].Text == blank ? "" : r.Cells[5].Text);
        ////    l.Desc_Text = (r.Cells[6].Text == blank ? "" : r.Cells[6].Text);

        ////    HiddenField aON = (HiddenField)r.FindControl("hdnOrderNumber");
        ////    HiddenField aQ = (HiddenField)r.FindControl("hdnQTY");
        ////    HiddenField aID = (HiddenField)r.FindControl("hdnStockID");

        ////    l.AvailableStock_OrderNumber = aON.Value;
        ////    str = aQ.Value;
        ////    if (int.TryParse(str, out _i) == false) { num = 0; }
        ////    l.AvailableStock_QTY = _i;

        ////    str = aID.Value;
        ////    if (decimal.TryParse(str, out num) == false) { num = -1; }
        ////    l.ReservedAvailableStockID = num;


        ////    l.isDeleted = false;
        ////    CheckBox btn = (CheckBox)r.FindControl("chkIsDeleted");
        ////    if (btn.Checked == true) { l.isDeleted = true; }
        ////    // don't add any lines that are deleted and have not already been saved.
        ////    if (l.isDeleted == false || (l.isDeleted && l.OrderDetailID > 0)) { OH.OrderDetailLine = l; }
        ////}


        //    return OH;
        //}
        #endregion


        #region Header
        //void btnEditNameAddressOK_Click(object sender, EventArgs e)
        //{
        //    if (hdnClientType.Value.ToUpper() == "CLIENT")
        //    {
        //        hdnBillCompanyName.Value = txtCompanyName.Text;
        //        hdnBillContactName.Value = txtContactName.Text;
        //        hdnBillAddressLine1.Value = txtAddressLine1.Text;
        //        hdnBillAddressLine2.Value = txtAddressLine2.Text;
        //        hdnBillCity.Value = txtCity.Text;
        //        hdnBillStateOrProvince.Value = txtStateOrProvince.Text;
        //        hdnBillPostalCode.Value = txtPostalCode.Text;
        //        hdnBillPhoneNumber.Value = txtPhoneNumber.Text;
        //        hdnBillFaxNumber.Value = txtFaxNumber.Text;
        //        hdnBillNotes.Value = txtNotes.Text;
        //        txtBillNameAddresstext.Text = GetAddressString(hdnClientType.Value);
        //    }
        //    if (hdnClientType.Value.ToUpper() == "SHIPTO")
        //    {
        //        hdnShipCompanyName.Value = txtCompanyName.Text;
        //        hdnShipContactName.Value = txtContactName.Text;
        //        hdnShipAddressLine1.Value = txtAddressLine1.Text;
        //        hdnShipAddressLine2.Value = txtAddressLine2.Text;
        //        hdnShipCity.Value = txtCity.Text;
        //        hdnShipStateOrProvince.Value = txtStateOrProvince.Text;
        //        hdnShipPostalCode.Value = txtPostalCode.Text;
        //        hdnShipPhoneNumber.Value = txtPhoneNumber.Text;
        //        hdnShipFaxNumber.Value = txtFaxNumber.Text;
        //        hdnShipNotes.Value = txtNotes.Text;
        //        txtShipNameAddresstext.Text = GetAddressString("ShipTo");
        //        //hdnPaid.Value = (chkPaid.Checked == true)? "1":"0";
        //        //hdnPostPaid.Value = (chkPostPaid.Checked == true) ? "1" : "0";
        //    }
        //    pnlHeader.Visible = true;
        //    pnlEditNameAddress.Visible = false;
        //    ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "RestoreBackEndData();", true);
        //}
        //void btnEditNameAddressCancel_Click(object sender, EventArgs e)
        //{
        //    pnlHeader.Visible = true;
        //    pnlEditNameAddress.Visible = false;
        //    ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "RestoreBackEndData();", true);
        //}
        //void btnBillClientEdit_Click(object sender, EventArgs e)
        //{
        //    //pnlHeader.Visible = false;
        //    lblEditAddress.Text = "Client Address";
        //    hdnClientType.Value = "Client";
        //    txtCompanyName.Text = hdnBillCompanyName.Value;
        //    txtContactName.Text = hdnBillContactName.Value;
        //    txtAddressLine1.Text = hdnBillAddressLine1.Value;
        //    txtAddressLine2.Text = hdnBillAddressLine2.Value;
        //    txtCity.Text = hdnBillCity.Value;
        //    txtStateOrProvince.Text = hdnBillStateOrProvince.Value;
        //    txtPostalCode.Text = hdnBillPostalCode.Value;
        //    txtPhoneNumber.Text = hdnBillPhoneNumber.Value;
        //    txtFaxNumber.Text = hdnBillFaxNumber.Value;
        //    txtNotes.Text = hdnBillNotes.Value;
        //    pnlHeader.Visible = false;
        //    pnlEditNameAddress.Visible = true;
        //    ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "RestoreBackEndData();", true);
        //}
        //void btnShipClientEdit_Click(object sender, EventArgs e)
        //{
        //    //pnlHeader.Visible = false;
        //    lblEditAddress.Text = "Ship to Address";
        //    hdnClientType.Value = "ShipTo";
        //    //hdnShipClientLocationID.Value = "-1";
        //    txtCompanyName.Text = hdnShipCompanyName.Value;
        //    txtContactName.Text = hdnShipContactName.Value;
        //    txtAddressLine1.Text = hdnShipAddressLine1.Value;
        //    txtAddressLine2.Text = hdnShipAddressLine2.Value;
        //    txtCity.Text = hdnShipCity.Value;
        //    txtStateOrProvince.Text = hdnShipStateOrProvince.Value;
        //    txtPostalCode.Text = hdnShipPostalCode.Value;
        //    txtPhoneNumber.Text = hdnShipPhoneNumber.Value;
        //    txtFaxNumber.Text = hdnShipFaxNumber.Value;
        //    txtNotes.Text = hdnShipNotes.Value;
        //    pnlHeader.Visible = false;
        //    pnlEditNameAddress.Visible = true;
        //    ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "RestoreBackEndData();", true);
        //}
        //void btnBillClientSearch_Click(object sender, EventArgs e)
        //{
        //    // search and get the data from the database
        //    clsOrderHeaderCompany Company = new clsOrderHeaderCompany();
        //    Company.LoadCompanyFromClientLocation(txtBillClient.Text);

        //    //            hdnClientType.Value = "Client";
        //    hdnBillClientLocationID.Value = Company.ClientLocationID.ToString();
        //    hdnBillCompanyName.Value = Company.CompanyName;
        //    hdnBillContactName.Value = Company.ContactName;
        //    hdnBillAddressLine1.Value = Company.AddressLine1;
        //    hdnBillAddressLine2.Value = Company.AddressLine2;
        //    hdnBillCity.Value = Company.City;
        //    hdnBillStateOrProvince.Value = Company.StateOrProvince;
        //    hdnBillPostalCode.Value = Company.PostalCode;
        //    hdnBillPhoneNumber.Value = Company.PhoneNumber;
        //    hdnBillFaxNumber.Value = Company.FaxNumber;
        //    hdnBillNotes.Value = Company.Notes;

        //    txtBillNameAddresstext.Text = GetAddressString("CLIENT");
        //    ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "RestoreBackEndData();", true);

        //}
        //void btnShipClientSearch_Click(object sender, EventArgs e)
        //{
        //    clsOrderHeaderCompany Company = new clsOrderHeaderCompany();
        //    Company.LoadCompanyFromClientLocation(txtShipClient.Text);
        //    // search and get the data from the database
        //    hdnShipClientLocationID.Value = Company.ClientLocationID.ToString();
        //    hdnShipCompanyName.Value = Company.CompanyName;
        //    hdnShipContactName.Value = Company.ContactName;
        //    hdnShipAddressLine1.Value = Company.AddressLine1;
        //    hdnShipAddressLine2.Value = Company.AddressLine2;
        //    hdnShipCity.Value = Company.City;
        //    hdnShipStateOrProvince.Value = Company.StateOrProvince;
        //    hdnShipPostalCode.Value = Company.PostalCode;
        //    hdnShipPhoneNumber.Value = Company.PhoneNumber;
        //    hdnShipFaxNumber.Value = Company.FaxNumber;
        //    hdnShipNotes.Value = Company.Notes;
        //    txtShipNameAddresstext.Text = GetAddressString("ShipTo");
        //    ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "RestoreBackEndData();", true);
        //}
        //private string GetAddressString(string CompanyType)
        //{
        //    string rString = "";
        //    if (CompanyType.ToUpper() == "SHIPTO")
        //    {
        //        rString = hdnShipCompanyName.Value;
        //        if (hdnShipAddressLine1.Value.Length > 0) { rString += Environment.NewLine; }
        //        rString += hdnShipAddressLine1.Value;
        //        if (hdnShipAddressLine2.Value.Length > 0) { rString += Environment.NewLine; }
        //        rString += hdnShipAddressLine2.Value;
        //        if (hdnShipCity.Value.Length > 0) { rString += Environment.NewLine; }
        //        rString += hdnShipCity.Value;
        //        if (hdnShipStateOrProvince.Value.Length > 0) { rString += Environment.NewLine; }
        //        rString += hdnShipStateOrProvince.Value;
        //        if (hdnShipPostalCode.Value.Length > 0) { rString += Environment.NewLine; }
        //        rString += hdnShipPostalCode.Value;
        //    }
        //    if (CompanyType.ToUpper() == "CLIENT")
        //    {
        //        rString = hdnBillCompanyName.Value;
        //        if (hdnBillAddressLine1.Value.Length > 0) { rString += Environment.NewLine; }
        //        rString += hdnBillAddressLine1.Value;
        //        if (hdnBillAddressLine2.Value.Length > 0) { rString += Environment.NewLine; }
        //        rString += hdnBillAddressLine2.Value;
        //        if (hdnBillCity.Value.Length > 0) { rString += Environment.NewLine; }
        //        rString += hdnBillCity.Value;
        //        if (hdnBillStateOrProvince.Value.Length > 0) { rString += Environment.NewLine; }
        //        rString += hdnBillStateOrProvince.Value;
        //        if (hdnBillPostalCode.Value.Length > 0) { rString += Environment.NewLine; }
        //        rString += hdnBillPostalCode.Value;
        //    }
        //    return rString;
        //}
        #endregion
    }
}