using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using GMPI_WebApp.DataManagers;

namespace GMPI_WebApp.IFSLocationManagement
{
    public partial class IFSLocationPurpose : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MainGrid.SelectedIndexChanged += new EventHandler(MainGrid_SelectedIndexChanged);
            if (!IsPostBack)
            {
                pnlAdd.Visible = false;
                pnlEdit.Visible = false;
                UpdateMainGrid();
            }
        }

        protected void UpdateMainGrid()
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                UpdateMainGrid(ctx);
            }
        }
        protected void UpdateMainGrid(clsLinqDataContext ctx)
        {
            MainGrid.DataSource = (from x in ctx.MasterIFSLocationPurposes.OrderBy(y => y.Purpose) select new { x.MasterIFSLocationPurposeID, x.Purpose });
            MainGrid.DataBind();
        }

        protected void MainGrid_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (MainGrid.SelectedIndex >= 0)
            {
                decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
                using (clsLinqDataContext ctx = new clsLinqDataContext())
                {
                    MasterIFSLocationPurpose qu = ctx.MasterIFSLocationPurposes.FirstOrDefault(x => x.MasterIFSLocationPurposeID == KeyID);
                    if (qu != null)
                    {
                        EditKeyID.Text = qu.MasterIFSLocationPurposeID.ToString();
                        EditPurpose.Text = qu.Purpose;
                    }

                    btnEdit.Visible = true;
                    btnDelete.Visible = true;
                }
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
            AddPurpose.Text = "";
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
                using (clsLinqDataContext ctx = new clsLinqDataContext())
                {
                    MasterIFSLocationPurpose qu = ctx.MasterIFSLocationPurposes.FirstOrDefault(x => x.MasterIFSLocationPurposeID == KeyID);
                    if (qu != null)
                    {
                        ctx.MasterIFSLocationPurposes.DeleteOnSubmit(qu);
                        ctx.SubmitChanges();
                        UpdateMainGrid(ctx);
                    }
                }
            }
        }


        protected void EditOK_Click(object sender, EventArgs e)
        {
            if (EditPurpose.Text.Length == 0) { return; }
            decimal KeyID = decimal.Parse(EditKeyID.Text);
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                MasterIFSLocationPurpose qu = ctx.MasterIFSLocationPurposes.FirstOrDefault(x => x.MasterIFSLocationPurposeID == KeyID);
                if (qu != null)
                {
                    qu.Purpose = EditPurpose.Text;
                    qu.LastUpdateDate = DateTime.Now;
                    qu.LastUpdateUser = User.Identity.Name;
                    ctx.SubmitChanges();
                }
                UpdateMainGrid(ctx);
                pnlMainView.Visible = true;
                pnlEdit.Visible = false;
            }
        }

        protected void EditCancel_Click(object sender, EventArgs e)
        {
            pnlMainView.Visible = true;
            pnlEdit.Visible = false;
        }

        #endregion


        protected void AddOK_Click(object sender, EventArgs e)
        {
            if (AddPurpose.Text.Length == 0) { return; }
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                MasterIFSLocationPurpose qu = new MasterIFSLocationPurpose();
                qu.Purpose = AddPurpose.Text;

                //qu.CreateDate = DateTime.Now;
                qu.CreateUser = User.Identity.Name;
                qu.LastUpdateDate = DateTime.Now;
                qu.LastUpdateUser = User.Identity.Name;

                ctx.MasterIFSLocationPurposes.InsertOnSubmit(qu);
                ctx.SubmitChanges();
                UpdateMainGrid(ctx);
                pnlMainView.Visible = true;
                pnlAdd.Visible = false;
            }
        }

        protected void AddCancel_Click1(object sender, EventArgs e)
        {
            pnlMainView.Visible = true;
            pnlAdd.Visible = false;
        }



    }
}