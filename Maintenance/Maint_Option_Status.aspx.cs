using System;
using System.Linq;
using System.Web.Configuration;
using BW_WebApp.DataManagers;

namespace BW_WebApp.Maintenance
{
    public partial class Maint_Option_Status : System.Web.UI.Page
    {
        string _ConnectionString = string.Empty;

        public string ConnectionString
        {
            get
            {
                if (_ConnectionString.Length == 0)
                {
                    System.Configuration.ConnectionStringSettingsCollection xconnectionString = WebConfigurationManager.ConnectionStrings;
                    //if (xconnectionString != null) { _ConnectionString = xconnectionString["GMP_DataEntities"].ConnectionString.ToString(); }
                    if (xconnectionString != null) { ConnectionString = xconnectionString["DefaultConnectionString"].ConnectionString.ToString(); }
                }

                return _ConnectionString;
            }
            set { _ConnectionString = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            MainGrid.SelectedIndexChanged += new EventHandler(MainGrid_SelectedIndexChanged);
            if (!IsPostBack)
            {
                clsLinqDataContext ctx = new clsLinqDataContext();
                pnlAdd.Visible = false;
                pnlEdit.Visible = false;

                UpdateMainGrid();
                //this.DataBind();

            }

        }

        protected void UpdateMainGrid()
        {
            clsLinqDataContext ctx = new clsLinqDataContext();
            MainGrid.DataSource = (from x in ctx.OptionStatus.OrderBy(y => y.Status) select new { x.OptionStatusID, x.Status });
            MainGrid.DataBind();
        }


        protected void MainGrid_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (MainGrid.SelectedIndex >= 0)
            {
                decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
                clsLinqDataContext ctx = new clsLinqDataContext();
                OptionStatus qu = ctx.OptionStatus.FirstOrDefault(x => x.OptionStatusID == KeyID);
                if (qu != null)
                {
                    EditKeyID.Text = qu.OptionStatusID.ToString();
                    EditStatus.Text = qu.Status;
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
            // Delete the StatusOptions.
            if (MainGrid.SelectedIndex >= 0)
            {
                decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
                clsLinqDataContext ctx = new clsLinqDataContext();
                OptionStatus qu = ctx.OptionStatus.FirstOrDefault(x => x.OptionStatusID == KeyID);
                if (qu != null)
                {
                    ctx.OptionStatus.DeleteOnSubmit(qu);
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
            OptionStatus qu = ctx.OptionStatus.FirstOrDefault(x => x.OptionStatusID == KeyID);
            if (qu != null)
            {
                qu.Status = EditStatus.Text;

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
            if (AddStatus.Text.Length == 0) { return; }
            clsLinqDataContext ctx = new clsLinqDataContext();
            OptionStatus qu = new OptionStatus();
            qu.Status = AddStatus.Text;
            qu.CreateDate = DateTime.Now;
            qu.CreateUser = User.Identity.Name;
            qu.LastUpdateDate = DateTime.Now;
            qu.LastUpdateUser = User.Identity.Name;

            ctx.OptionStatus.InsertOnSubmit(qu);
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