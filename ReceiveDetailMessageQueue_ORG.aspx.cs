using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using GMPI_WebApp.DataManagers;

namespace GMPI_WebApp
{
    public partial class ReceiveDetailMessageQueue_ORG : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            MainGrid.AllowSorting = true;
            MainGrid.SelectedIndexChanged += new EventHandler(MainGrid_SelectedIndexChanged);
            MainGrid.Sorting += new GridViewSortEventHandler(MainGrid_Sorting);
            chkIncludeClosedMessages.CheckedChanged += new EventHandler(chkIncludeClosedMessages_CheckedChanged);
            if (!IsPostBack)
            {
                clsLinqDataContext ctx = new clsLinqDataContext();
                pnlAdd.Visible = false;
                pnlEdit.Visible = false;

                UpdateMainGrid();
                //                this.DataBind();

            }

        }

        void MainGrid_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;


            clsLinqDataContext ctx = new clsLinqDataContext();

            switch (e.SortExpression)
            {
                case "ESN":
                    {
                        MainGrid.DataSource = (from x in ctx.ReceiveDetailESNMessages.OrderBy(y => y.ESN)
                                               where x.StatusOpen == 1 || chkIncludeClosedMessages.Checked == true
                                               orderby x.ESN
                                               select new
                                               {
                                                   x.ReceiveDetailESNMessageID,
                                                   StatusOpen = (x.StatusOpen == 1 ? "Show" : "Archived"),
                                                   StatusStop = (x.StatusStop == 1 ? "Stop Advance Past Receive" : ""),
                                                   x.ESN,
                                                   x.CreateDate,
                                                   x.CreateUser,
                                                   x.Message
                                               });
                        break;
                    }
                case "StatusOpen":
                    {
                        MainGrid.DataSource = (from x in ctx.ReceiveDetailESNMessages.OrderBy(y => y.ESN)
                                               where x.StatusOpen == 1 || chkIncludeClosedMessages.Checked == true
                                               orderby x.StatusOpen
                                               select new
                                               {
                                                   x.ReceiveDetailESNMessageID,
                                                   StatusOpen = (x.StatusOpen == 1 ? "Show" : "Archived"),
                                                   StatusStop = (x.StatusStop == 1 ? "Stop Advance Past Receive" : ""),
                                                   x.ESN,
                                                   x.CreateDate,
                                                   x.CreateUser,
                                                   x.Message
                                               });
                        break;
                    }
                case "StatusStop":
                    {
                        MainGrid.DataSource = (from x in ctx.ReceiveDetailESNMessages.OrderBy(y => y.ESN)
                                               where x.StatusOpen == 1 || chkIncludeClosedMessages.Checked == true
                                               orderby x.StatusStop
                                               select new
                                               {
                                                   x.ReceiveDetailESNMessageID,
                                                   StatusOpen = (x.StatusOpen == 1 ? "Show" : "Archived"),
                                                   StatusStop = (x.StatusStop == 1 ? "Stop Advance Past Receive" : ""),
                                                   x.ESN,
                                                   x.CreateDate,
                                                   x.CreateUser,
                                                   x.Message
                                               });
                        break;
                    }
                case "CreateDate":
                    {
                        MainGrid.DataSource = (from x in ctx.ReceiveDetailESNMessages.OrderBy(y => y.ESN)
                                               where x.StatusOpen == 1 || chkIncludeClosedMessages.Checked == true
                                               orderby x.CreateDate
                                               select new
                                               {
                                                   x.ReceiveDetailESNMessageID,
                                                   StatusOpen = (x.StatusOpen == 1 ? "Show" : "Archived"),
                                                   StatusStop = (x.StatusStop == 1 ? "Stop Advance Past Receive" : ""),
                                                   x.ESN,
                                                   x.CreateDate,
                                                   x.CreateUser,
                                                   x.Message
                                               });
                        break;
                    }
                case "CreateUser":
                    {
                        MainGrid.DataSource = (from x in ctx.ReceiveDetailESNMessages.OrderBy(y => y.ESN)
                                               where x.StatusOpen == 1 || chkIncludeClosedMessages.Checked == true
                                               orderby x.CreateUser
                                               select new
                                               {
                                                   x.ReceiveDetailESNMessageID,
                                                   StatusOpen = (x.StatusOpen == 1 ? "Show" : "Archived"),
                                                   StatusStop = (x.StatusStop == 1 ? "Stop Advance Past Receive" : ""),
                                                   x.ESN,
                                                   x.CreateDate,
                                                   x.CreateUser,
                                                   x.Message
                                               });
                        break;
                    }
            }
            MainGrid.DataBind();

        }

        void chkIncludeClosedMessages_CheckedChanged(object sender, EventArgs e)
        {
            UpdateMainGrid();
        }


        protected void UpdateMainGrid()
        {
            clsLinqDataContext ctx = new clsLinqDataContext();
            MainGrid.DataSource = (from x in ctx.ReceiveDetailESNMessages.OrderBy(y => y.ESN)
                                   where x.StatusOpen == 1 || chkIncludeClosedMessages.Checked == true
                                   select new
                                   {
                                       x.ReceiveDetailESNMessageID,
                                       StatusOpen = (x.StatusOpen == 1 ? "Show" : "Archived"),
                                       StatusStop = (x.StatusStop == 1 ? "Stop Advance Past Receive" : ""),
                                       x.ESN,
                                       x.CreateDate,
                                       x.CreateUser,
                                       x.Message
                                   });
            MainGrid.DataBind();
        }


        protected void MainGrid_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (MainGrid.SelectedIndex >= 0)
            {
                decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
                clsLinqDataContext ctx = new clsLinqDataContext();
                ReceiveDetailESNMessage qu = ctx.ReceiveDetailESNMessages.FirstOrDefault(x => x.ReceiveDetailESNMessageID == KeyID);
                if (qu != null)
                {
                    EditKeyID.Text = qu.ReceiveDetailESNMessageID.ToString();
                    EditStatusOpen.Checked = (qu.StatusOpen == 1 ? true : false);
                    EditStatusStop.Checked = (qu.StatusStop == 1 ? true : false);
                    EditESN.Text = qu.ESN;
                    EditMessage.Text = qu.Message;
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

            if (hdnESNtoAdd.Value == "null") { return; }

            if (hdnESNtoAdd.Value.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "addesn", "alert('No ESN Given');", true);
                return;
            }
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                ReceiveDetailESNMessage m = ctx.ReceiveDetailESNMessages.FirstOrDefault(x => x.ESN == hdnESNtoAdd.Value && x.StatusOpen == 1);
                if (m != null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "addesn", "alert('ESN(" + hdnESNtoAdd.Value + ") already on file.');", true);
                    return;
                }
            }


            AddStatusOpen.Checked = true;
            AddStatusStop.Checked = true;
            AddESN.Text = hdnESNtoAdd.Value;
            AddESN.Enabled = false;
            AddMessage.Text = "";
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
                    ReceiveDetailESNMessage m = ctx.ReceiveDetailESNMessages.FirstOrDefault(x => x.ReceiveDetailESNMessageID == KeyID);
                    if (m != null)
                    {
                        ctx.ReceiveDetailESNMessages.DeleteOnSubmit(m);
                        ctx.SubmitChanges();
                        UpdateMainGrid();
                    }
                }
            }
        }


        protected void EditOK_Click(object sender, EventArgs e)
        {
            if (EditMessage.Text.Length == 0) { return; }
            decimal KeyID = decimal.Parse(EditKeyID.Text);
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                ReceiveDetailESNMessage m = ctx.ReceiveDetailESNMessages.FirstOrDefault(x => x.ReceiveDetailESNMessageID == KeyID);
                if (m != null)
                {
                    m.ESN = EditESN.Text;
                    m.Message = EditMessage.Text;
                    m.StatusOpen = (EditStatusOpen.Checked == true ? 1 : 0);
                    m.StatusStop = (EditStatusStop.Checked == true ? 1 : 0);

                    m.LastUpdateDate = DateTime.Now;
                    m.LastUpdateUser = User.Identity.Name;
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


        protected void AddOK_Click(object sender, EventArgs e)
        {
            if (AddMessage.Text.Length == 0) { return; }
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                ReceiveDetailESNMessage m = new ReceiveDetailESNMessage();

                m.ESN = AddESN.Text;
                m.Message = AddMessage.Text;
                m.StatusOpen = (AddStatusOpen.Checked == true ? 1 : 0);
                m.StatusStop = (AddStatusStop.Checked == true ? 1 : 0);

                m.CreateDate = DateTime.Now;
                m.CreateUser = User.Identity.Name;
                m.LastUpdateDate = DateTime.Now;
                m.LastUpdateUser = User.Identity.Name;

                ctx.ReceiveDetailESNMessages.InsertOnSubmit(m);
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