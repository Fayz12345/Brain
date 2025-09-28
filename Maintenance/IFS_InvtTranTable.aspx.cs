using System;
//using Factory_DataModel;
using BW_WebApp.DataManagers;
// using DAL;

namespace BW_WebApp.Maintenance
{
    public partial class IFS_InvtTranTable : System.Web.UI.Page
    {
        //private string blank = "&nbsp;";

        //ClientManager CM = null;
        //clsLinqDataContext ctx = null;
        //decimal KeyID = -1;
        DeviceInventoryManager cm = null;
        //InvtTran_IF  cl = null;



        protected void Page_Load(object sender, EventArgs e)
        {
            cm = new DeviceInventoryManager(User.Identity.Name);
            //KeyID = -1;
            //cl = null;
            //if (MainGrid.SelectedValue != null)
            //{
            //    KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
            //    cl = cm.GetIFSInventoryTransaction(KeyID);
            //}
            btnRefresh.Click += new EventHandler(btnRefresh_Click);
            btnGatherData.Click += new EventHandler(btnGatherData_Click);

            if (!IsPostBack)
            {
                hdnUserName.Value = User.Identity.Name;
                txtBeginDate.Text = DateTime.Now.AddDays(-7).ToShortDateString();
                txtEndDate.Text = DateTime.Now.ToShortDateString();
            }
        }

        void btnGatherData_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void btnRefresh_Click(object sender, EventArgs e)
        {
            UpdateMainGrid();
        }

        //void MainGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //}
        protected void UpdateMainGrid()
        {
            DateTime BeginDate = DateTime.Now;               // = txtBeginDate.Text;
            DateTime EndDate = DateTime.Now;                 // = txtEndDate.Text;
            BeginDate = DateTime.Parse(txtBeginDate.Text);
            EndDate = DateTime.Parse(txtEndDate.Text);
            EndDate = EndDate.AddDays(1);
            int batch = -1;
            int.TryParse(txtBatch.Text, out batch);
            if (batch > 0)
            {
                MainGrid.DataSource = cm.GetInvtTran_IFSList(batch);
                MainGrid.DataBind();
            }
            else if (txtESN.Text.Length > 0)
            {
                MainGrid.DataSource = cm.GetInvtTran_IFSList(txtESN.Text, BeginDate, EndDate, chkReceived.Checked);
                MainGrid.DataBind();
            }
            else if (txtESN.Text.Length == 0)
            {
                MainGrid.DataSource = cm.GetInvtTran_IFSList(BeginDate, EndDate, chkReceived.Checked);
                MainGrid.DataBind();
            }
            else
            {
                MainGrid.DataSource = cm.GetInvtTran_IFSList(chkReceived.Checked);
                MainGrid.DataBind();
            }
        }
    }
}