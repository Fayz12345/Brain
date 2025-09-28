using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
//using Factory_DataModel;
using BW_WebApp.DataManagers;

namespace BW_WebApp.Maintenance
{
    public partial class Maint_MasterPartsTransactionType : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            MainGrid.SelectedIndexChanged += new EventHandler(MainGrid_SelectedIndexChanged);
            if (!IsPostBack)
            {
                //clsLinqDataContext ctx = new clsLinqDataContext();
                pnlAdd.Visible = false;
                pnlEdit.Visible = false;

                UpdateMainGrid();
                //this.DataBind();

            }

        }

        protected void UpdateMainGrid()
        {

            MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
            MainGrid.DataSource = mpm.GetMasterPartTransType();
            MainGrid.DataBind();
        }


        protected void MainGrid_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (MainGrid.SelectedIndex >= 0)
            {
                decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
                MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
                PartNumberBucketInventoryTransactionType tt = mpm.GetMasterPartTransType(KeyID);
                if (tt != null)
                {
                    EditKeyID.Text = tt.PartNumberBucketInventorysourceTypeID.ToString();
                    EditType.Text = tt.Type;
                    EditIFSDirective.Text = tt.IFSDirectiveType;
                    EditIFSReasonCode.Text = tt.IFSReasonCode;
                    editAccessRole.Text = tt.Role;
                    EditFactor.Text = tt.Factor.ToString();
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
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('At this time, you are not allowed to delete any items from this list');", true);
                //decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
                //clsLinqDataContext ctx = new clsLinqDataContext();
                //QuestionStatus qu = ctx.QuestionStatus.FirstOrDefault(x => x.QuestionStatusID == KeyID);
                //if (qu != null)
                //{
                //    ctx.QuestionStatus.DeleteOnSubmit(qu);
                //    ctx.SubmitChanges();
                //    UpdateMainGrid();
                //}
            }
        }
        protected void EditOK_Click(object sender, EventArgs e)
        {
            if (EditType.Text.Length == 0) { return; }
            decimal KeyID = decimal.Parse(EditKeyID.Text);


            MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
            PartNumberBucketInventoryTransactionType tt = mpm.GetMasterPartTransType(KeyID);
            if (tt != null)
            {
                decimal factor = 0;
                if (decimal.TryParse(EditFactor.Text, out factor) == false) { factor = 1; }
                tt.Factor = factor;
                tt.Type = EditType.Text;
                tt.IFSReasonCode = EditIFSReasonCode.Text;
                tt.Role = editAccessRole.Text;
                tt.IFSDirectiveType = EditIFSDirective.Text;
                mpm.UpdateType(tt);

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

            MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
            PartNumberBucketInventoryTransactionType tt = new PartNumberBucketInventoryTransactionType();
            decimal factor = 0;
            if (decimal.TryParse(EditFactor.Text, out factor) == false) { factor = 1; }
            tt.Factor = factor;
            tt.Type = AddType.Text;
            tt.IFSDirectiveType = AddIFSDirective.Text;
            tt.IFSReasonCode = AddIFSReasonCode.Text;
            tt.Role = AddAccessRole.Text;
            mpm.InsertType(tt);
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