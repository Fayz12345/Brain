using System;
using System.Linq;
using System.Web.UI.WebControls;
using BW_WebApp.DataManagers;

namespace BW_WebApp.Maintenance
{
    public partial class Maint_LabDestinationBinLookup : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            MainGrid.SelectedIndexChanged += new EventHandler(MainGrid_SelectedIndexChanged);
            if (!IsPostBack)
            {
                //clsLinqDataContext ctx = new clsLinqDataContext();
                //pnlAdd.Visible = false;
                pnlEdit.Visible = false;

                UpdateMainGrid();
                //this.DataBind();

            }

        }

        protected void UpdateMainGrid()
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                Question q = ctx.Questions.FirstOrDefault(x => x.Name.ToUpper() == "LAB DESTINATION");
                Option bin = (ctx.Questions.FirstOrDefault(x => x.Name.ToUpper() == "BIN").Options.FirstOrDefault());
                if (q != null && bin != null)
                {
                    //string BinNumber = "";
                    var RelatedBinData = (from x in ctx.Option_Text_Defaults select new { x.SourceOptionID, x.TargetOptionID, x.TargetText }).ToList();
                    var data = (from x in q.Options.OrderBy(y => y.OptionText) select new gData(x.OptionID, x.OptionText, "")).ToList();
                    foreach (var d in data)
                    {
                        var r = RelatedBinData.FirstOrDefault(x => x.SourceOptionID == d.OptionID);
                        if (r != null)
                        {
                            d.BinNumber = r.TargetText;
                        }
                        else
                        {
                            // we want to add one so it is available. It should be a one to one.
                            Option_Text_Default newot = new Option_Text_Default();
                            newot.TargetText = "";
                            newot.TargetOptionID = bin.OptionID;
                            newot.SourceOptionID = d.OptionID;
                            newot.CreateUser = User.Identity.Name;
                            newot.LastUpdateDate = DateTime.Now;
                            newot.LastUpdateUser = User.Identity.Name;
                            ctx.Option_Text_Defaults.InsertOnSubmit(newot);
                        }
                    }
                    ctx.SubmitChanges();
                    MainGrid.DataSource = data;
                    MainGrid.DataBind();
                }
            }
        }
        class gData
        {
            public decimal OptionID { get; set; }
            public string OptionText { get; set; }
            public string BinNumber { get; set; }
            public gData(decimal id, string text, string bin)
            {
                OptionID = id;
                OptionText = text;
                BinNumber = bin;
            }
        }
        protected void MainGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MainGrid.SelectedIndex >= 0)
            {
                decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
                using (clsLinqDataContext ctx = new clsLinqDataContext())
                {
                    Option qu = ctx.Options.FirstOrDefault(x => x.OptionID == KeyID);
                    if (qu != null)
                    {
                        EditKeyID.Text = qu.OptionID.ToString();
                        lblDestination.Text = qu.OptionText;
                        EditBin.Text = "";
                        Option_Text_Default d = ctx.Option_Text_Defaults.FirstOrDefault(x => x.SourceOptionID == KeyID);
                        if (d != null)
                        {
                            EditBin.Text = d.TargetText;
                        }
                    }
                }
                btnEdit.Visible = true;
                btnDelete.Visible = true;
            }
            else
            {
                btnEdit.Visible = false;
                btnDelete.Visible = false;
            }
        }

        #region QuesionDetail

        //protected void btnAdd_Click(object sender, EventArgs e)
        //{
        //    AddType.Text = "";
        //    pnlMainView.Visible = false;
        //    pnlAdd.Visible = true;

        //}

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            pnlMainView.Visible = false;
            pnlEdit.Visible = true;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            // Delete the answers.
            if (MainGrid.SelectedIndex >= 0)
            {
                decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
                using (clsLinqDataContext ctx = new clsLinqDataContext())
                {
                    Option_Text_Default d = ctx.Option_Text_Defaults.FirstOrDefault(x => x.SourceOptionID == KeyID);
                    if (d != null)
                    {
                        d.LastUpdateUser = User.Identity.Name;
                        d.LastUpdateDate = DateTime.Now;
                        d.TargetText = "";
                        ctx.SubmitChanges();
                        UpdateMainGrid();
                    }
                }
            }
        }


        protected void EditOK_Click(object sender, EventArgs e)
        {
            if (EditBin.Text.Length == 0) { return; }
            decimal KeyID = decimal.Parse(EditKeyID.Text);
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                Option_Text_Default d = ctx.Option_Text_Defaults.FirstOrDefault(x => x.SourceOptionID == KeyID);
                if (d != null)
                {
                    d.LastUpdateUser = User.Identity.Name;
                    d.LastUpdateDate = DateTime.Now;
                    d.TargetText = EditBin.Text;
                    ctx.SubmitChanges();
                }
            }
            UpdateMainGrid();
            pnlMainView.Visible = true;
            pnlEdit.Visible = false;
        }

        protected void EditCancel_Click(object sender, EventArgs e)
        {
            pnlMainView.Visible = true;
            pnlEdit.Visible = false;
        }
        #endregion


        //protected void AddOK_Click(object sender, EventArgs e)
        //{
        //    if (AddType.Text.Length == 0) { return; }
        //    clsLinqDataContext ctx = new clsLinqDataContext();
        //    OptionType qu = new OptionType();
        //    qu.Type = AddType.Text;
        //    qu.CreateDate = DateTime.Now;
        //    qu.CreateUser = User.Identity.Name;
        //    qu.LastUpdateDate = DateTime.Now;
        //    qu.LastUpdateUser = User.Identity.Name;

        //    ctx.OptionTypes.InsertOnSubmit(qu);
        //    ctx.SubmitChanges();
        //    UpdateMainGrid();
        //    pnlMainView.Visible = true;
        //    pnlAdd.Visible = false;
        //}

        //protected void AddCancel_Click1(object sender, EventArgs e)
        //{
        //    pnlMainView.Visible = true;
        //    pnlAdd.Visible = false;
        //}


    }
}