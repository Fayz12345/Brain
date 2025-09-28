using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using BW_WebApp.DataManagers;

namespace BW_WebApp.Maintenance
{
    public partial class Maint_MasterPartsClassType : System.Web.UI.Page
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
            MainGrid.DataSource = (from x in ctx.MasterPartsClassTypes.OrderBy(y => y.Class) select new { x.MasterPartsClassTypeID, x.Class });
            MainGrid.DataBind();
        }


        protected void MainGrid_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (MainGrid.SelectedIndex >= 0)
            {
                decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
                clsLinqDataContext ctx = new clsLinqDataContext();
                MasterPartsClassType qu = ctx.MasterPartsClassTypes.FirstOrDefault(x => x.MasterPartsClassTypeID == KeyID);
                if (qu != null)
                {
                    EditKeyID.Text = qu.MasterPartsClassTypeID.ToString();
                    EditStatus.Text = qu.Class;
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
            AddStatus.Text = "";
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
                MasterPartsClassType qu = ctx.MasterPartsClassTypes.FirstOrDefault(x => x.MasterPartsClassTypeID == KeyID);
                if (qu != null)
                {
                    ctx.MasterPartsClassTypes.DeleteOnSubmit(qu);
                    ctx.SubmitChanges();
                    UpdateMainGrid();
                }
            }
        }


        protected void EditOK_Click(object sender, EventArgs e)
        {
            if (EditStatus.Text.Length == 0) { return; }
            decimal KeyID = decimal.Parse(EditKeyID.Text);
            clsLinqDataContext ctx = new clsLinqDataContext();
            MasterPartsClassType qu = ctx.MasterPartsClassTypes.FirstOrDefault(x => x.MasterPartsClassTypeID == KeyID);
            if (qu != null)
            {
                qu.Class = EditStatus.Text;
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
            if (AddStatus.Text.Length == 0) { return; }
            clsLinqDataContext ctx = new clsLinqDataContext();
            MasterPartsClassType qu = new MasterPartsClassType();
            qu.Class = AddStatus.Text;

            qu.CreateDate = DateTime.Now;
            qu.CreateUser = User.Identity.Name;
            qu.LastUpdateDate = DateTime.Now;
            qu.LastUpdateUser = User.Identity.Name;

            ctx.MasterPartsClassTypes.InsertOnSubmit(qu);
            ctx.SubmitChanges();
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