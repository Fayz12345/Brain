using System;
using System.Linq;
using System.Web.UI.WebControls;
using BW_WebApp.DataManagers;

namespace BW_WebApp
{
    public partial class PurchaseOrder : System.Web.UI.Page
    {
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    //btnProcess.Click += new EventHandler(btnProcess_Click);
        //    hdnUserName.Value = User.Identity.Name;
        //    if (!IsPostBack)
        //    {

        //        //ProjectManager pm = new ProjectManager(User.Identity.Name);
        //        //drpProjectList.DataValueField = "ProjectID";
        //        //drpProjectList.DataTextField = "Name";
        //        //drpProjectList.DataSource = pm.GetMasterActiveProjectList();
        //        //drpProjectList.DataBind();
        //        //drpProjectList.SelectedIndex = 0;


        //    }
        //    txtESN.Focus();
        //}

        //string _ConnectionString = string.Empty;

        //public string ConnectionString
        //{
        //    get
        //    {
        //        if (_ConnectionString.Length == 0)
        //        {
        //            System.Configuration.ConnectionStringSettingsCollection xconnectionString = WebConfigurationManager.ConnectionStrings;
        //            if (xconnectionString != null) { _ConnectionString = xconnectionString["GMP_DataEntities"].ConnectionString.ToString(); }
        //        }

        //        return _ConnectionString;
        //    }
        //    set { _ConnectionString = value; }
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            hdnUserName.Value = User.Identity.Name; ;
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
            var Data = from x in ctx.PurchaseOrderHeaders.OrderBy(y => y.ProjectTag) select x;
            MainGrid.DataSource = Data.ToList();
            MainGrid.DataBind();
        }
        protected void MainGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MainGrid.SelectedIndex >= 0)
            {
                decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
                clsLinqDataContext ctx = new clsLinqDataContext();
                PurchaseOrderHeader po = ctx.PurchaseOrderHeaders.FirstOrDefault(x => x.PurchaseOrderHeaderID == KeyID);
                if (po != null)
                {
                    EditKeyID.Text = po.PurchaseOrderHeaderID.ToString();
                    // EditStatus.Text = po.Status;
                    EditProjectTag.Text = po.ProjectTag;
                    EditPurchasePrice.Text = po.PurchasePrice.ToString();
                    EditPurchaseQTY.Text = po.PurchaseQTY.ToString();
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
            AddProjectTag.Text = "";
            AddPurchasePrice.Text = "0";
            AddPurchaseQTY.Text = "0";
            //AddStatus.Text = "";
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
                PurchaseOrderHeader po = ctx.PurchaseOrderHeaders.FirstOrDefault(x => x.PurchaseOrderHeaderID == KeyID);
                if (po != null)
                {
                    ctx.PurchaseOrderHeaders.DeleteOnSubmit(po);
                    ctx.SubmitChanges();
                    UpdateMainGrid();
                }
            }
        }


        protected void EditOK_Click(object sender, EventArgs e)
        {
            //if (EditStatus.Text.Length == 0) { return; }
            decimal nPurchasePrice = 0;
            decimal nPurchaseQTY = 0;
            decimal nUnitPrice = 0;
            if (decimal.TryParse(EditPurchasePrice.Text, out nPurchasePrice) == false) { return; }
            if (decimal.TryParse(EditPurchaseQTY.Text, out nPurchaseQTY) == false) { return; }
            if (nPurchaseQTY == 0) { return; }
            nUnitPrice = nPurchasePrice / nPurchaseQTY;
            decimal KeyID = decimal.Parse(EditKeyID.Text);

            clsLinqDataContext ctx = new clsLinqDataContext();
            PurchaseOrderHeader po = ctx.PurchaseOrderHeaders.FirstOrDefault(x => x.PurchaseOrderHeaderID == KeyID);
            if (po != null)
            {
                po.Status = "Updated";
                //po.ProjectTag = "PTAG";
                po.PurchasePrice = nPurchasePrice;
                po.PurchaseQTY = nPurchaseQTY;
                po.WayBillNumber = "";
                po.PurchaseUnitPrice = nUnitPrice;
                po.OrderNumber = "";
                po.Courier = "";
                po.CustomerPO = "";
                po.DeliveryNote = "";
                po.InternalNote = "";
                po.MiscDesc = "";
                //po.OrderDate =
                //po.ReceiveDate = 
                //po.ShippedDate = 
                po.LastUpdateDate = DateTime.Now;
                po.LastUpdateUser = User.Identity.Name;

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
            if (AddProjectTag.Text.Length == 0) { return; }
            decimal nPurchasePrice = 0;
            decimal nPurchaseQTY = 0;
            decimal nUnitPrice = 0;
            if (decimal.TryParse(AddPurchasePrice.Text, out nPurchasePrice) == false) { return; }
            if (decimal.TryParse(AddPurchaseQTY.Text, out nPurchaseQTY) == false) { return; }
            if (nPurchaseQTY == 0) { return; }
            nUnitPrice = nPurchasePrice / nPurchaseQTY;

            clsLinqDataContext ctx = new clsLinqDataContext();
            PurchaseOrderHeader po = new PurchaseOrderHeader();
            po.Status = "New";
            po.ProjectTag = AddProjectTag.Text;

            po.PurchasePrice = nPurchasePrice;
            po.PurchaseQTY = nPurchaseQTY;

            po.WayBillNumber = "";
            po.PurchaseUnitPrice = nUnitPrice;
            po.OrderNumber = "";
            po.Courier = "";
            po.CustomerPO = "";
            po.DeliveryNote = "";
            po.InternalNote = "";
            po.MiscDesc = "";
            //po.OrderDate =
            //po.ReceiveDate = 
            //po.ShippedDate = 
            po.CreateUser = User.Identity.Name;
            po.LastUpdateDate = DateTime.Now;
            po.LastUpdateUser = User.Identity.Name;
            ctx.PurchaseOrderHeaders.InsertOnSubmit(po);
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