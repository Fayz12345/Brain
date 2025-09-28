using System;
using System.Linq;
using BW_WebApp.DataManagers;

namespace BW_WebApp.Maintenance
{
    public partial class Maint_Option_Type : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MainGrid.SelectedIndexChanged += new EventHandler(MainGrid_SelectedIndexChanged);
            if (!IsPostBack)
            {
                clsLinqDataContext ctx = new clsLinqDataContext();
                pnlAdd.Visible = false;
                pnlEdit.Visible = false;

                UpdateMainGrid();
            }
        }

        protected void UpdateMainGrid()
        {
            clsLinqDataContext ctx = new clsLinqDataContext();
            MainGrid.DataSource = (from x in ctx.OptionTypes.OrderBy(y => y.Type) select new { x.OptionTypeID, x.Type });
            MainGrid.DataBind();
        }


        protected void MainGrid_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (MainGrid.SelectedIndex >= 0)
            {
                decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
                clsLinqDataContext ctx = new clsLinqDataContext();
                OptionType qu = ctx.OptionTypes.FirstOrDefault(x => x.OptionTypeID == KeyID);
                if (qu != null)
                {
                    EditKeyID.Text = qu.OptionTypeID.ToString();
                    EditType.Text = qu.Type;
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

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            AddType.Text = "";
            pnlMainView.Visible = false;
            pnlAdd.Visible = true;

        }

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
                clsLinqDataContext ctx = new clsLinqDataContext();
                OptionType qu = ctx.OptionTypes.FirstOrDefault(x => x.OptionTypeID == KeyID);
                if (qu != null)
                {
                    ctx.OptionTypes.DeleteOnSubmit(qu);
                    ctx.SubmitChanges();
                    UpdateMainGrid();
                }
            }
        }


        protected void EditOK_Click(object sender, EventArgs e)
        {
            if (EditType.Text.Length == 0) { return; }
            decimal KeyID = decimal.Parse(EditKeyID.Text);
            clsLinqDataContext ctx = new clsLinqDataContext();
            OptionType qu = ctx.OptionTypes.FirstOrDefault(x => x.OptionTypeID == KeyID);
            if (qu != null)
            {
                qu.Type = EditType.Text;

                qu.CreateUser = User.Identity.Name;
                qu.LastUpdateDate = DateTime.Now;
                qu.LastUpdateUser = User.Identity.Name;

                ctx.SubmitChanges();
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


        protected void AddOK_Click(object sender, EventArgs e)
        {
            if (AddType.Text.Length == 0) { return; }
            clsLinqDataContext ctx = new clsLinqDataContext();
            OptionType ot = ctx.OptionTypes.FirstOrDefault(x => x.Type == AddType.Text);
            if (ot == null)
            {
                OptionType qu = new OptionType();
                qu.Type = AddType.Text;
                qu.CreateDate = DateTime.Now;
                qu.CreateUser = User.Identity.Name;
                qu.LastUpdateDate = DateTime.Now;
                qu.LastUpdateUser = User.Identity.Name;
                ctx.OptionTypes.InsertOnSubmit(qu);
                ctx.SubmitChanges();
            }
            UpdateMainGrid();
            pnlMainView.Visible = true;
            pnlAdd.Visible = false;
        }

        protected void AddCancel_Click1(object sender, EventArgs e)
        {
            pnlMainView.Visible = true;
            pnlAdd.Visible = false;
        }


    }
}