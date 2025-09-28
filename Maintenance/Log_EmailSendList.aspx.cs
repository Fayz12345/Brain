using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using BW_WebApp.DataManagers;
using Syncfusion.Grouping;

namespace BW_WebApp.Maintenance
{
    public partial class Log_EmailSendList : System.Web.UI.Page
    {
        //private bool _isAlreadySorted;
        //public bool IsAlreadySorted
        //{
        //    get
        //    {
        //        return _isAlreadySorted;
        //    }
        //    set
        //    {
        //        _isAlreadySorted = value;
        //    }
        //}
        //MessageQueueData CurRec = null;


        protected void Page_Load(object sender, EventArgs e)
        {
            //MainGrid.AllowSorting = true;
            //MainGrid.SelectedIndexChanged += new EventHandler(MainGrid_SelectedIndexChanged);
            //MainGrid_B.SelectedRecordsChanged += new SelectedRecordsChangedEventHandler(MainGrid_B_SelectedRecordsChanged);
            //MainGrid_B.CurrentRecordContextChange += new CurrentRecordContextChangeEventHandler(MainGrid_B_CurrentRecordContextChange);
            //MainGrid.Sorting += new GridViewSortEventHandler(MainGrid_Sorting);
            //chkIncludeClosedMessages.CheckedChanged += new EventHandler(chkIncludeClosedMessages_CheckedChanged);
            this.MainGrid_B.ShowGroupDropArea = false;

            //this.MainGrid_B.TableDescriptor.Appearance.AlternateRecordFieldCell.BackColor = Color.AliceBlue;
            this.MainGrid_B.Appearance.AlternateRecordFieldCell.Interior = new Syncfusion.Drawing.BrushInfo(System.Drawing.ColorTranslator.FromHtml("#E2E2E2"));
            //this.MainGrid_B.
            //MainGrid_B.CurrentTable.SelectedRecordsChanged += new SelectedRecordsChangedEventHandler(CurrentTable_SelectedRecordsChanged);
            //MainGrid_B.Table.SelectedRecordsChanged += new SelectedRecordsChangedEventHandler(Table_SelectedRecordsChanged);
            if (!IsPostBack)
            {
                clsLinqDataContext ctx = new clsLinqDataContext();
                //pnlAdd.Visible = false;
                //pnlEdit.Visible = false;

                UpdateMainGrid();
                //                this.DataBind();

            }
            //this.MainGrid_B.TableDescriptor.SortedColumns.Changed += new Syncfusion.Collections.ListPropertyChangedEventHandler(SortedColumns_Changed);

        }

        void Table_SelectedRecordsChanged(object sender, SelectedRecordsChangedEventArgs e)
        {
            if (e.SelectedRecord.Record != null)
            {
                //MessageQueueData rec = (MessageQueueData)e.SelectedRecord.Record.GetData();
                //decimal KeyID = rec.ReceiveDetailESNMessageID;
                //clsLinqDataContext ctx = new clsLinqDataContext();
                //ReceiveDetailESNMessage qu = ctx.ReceiveDetailESNMessages.FirstOrDefault(x => x.ReceiveDetailESNMessageID == KeyID);
                //if (qu != null)
                //{
                //    EditKeyID.Text = qu.ReceiveDetailESNMessageID.ToString();
                //    EditStatusOpen.Checked = (qu.StatusOpen == 1 ? true : false);
                //    EditStatusStop.Checked = (qu.StatusStop == 1 ? true : false);
                //    EditESN.Text = qu.ESN;
                //    EditMessage.Text = qu.Message;
                //}
                //btnEdit.Visible = true;
                //btnDelete.Visible = true;
            }
            //else
            //{
            //    btnEdit.Visible = false;
            //    btnDelete.Visible = false;

            //}
        }

        void CurrentTable_SelectedRecordsChanged(object sender, SelectedRecordsChangedEventArgs e)
        {
            if (e.SelectedRecord.Record != null)
            {
                //MessageQueueData rec = (MessageQueueData)e.SelectedRecord.Record.GetData();
                //decimal KeyID = rec.ReceiveDetailESNMessageID;
                //clsLinqDataContext ctx = new clsLinqDataContext();
                //ReceiveDetailESNMessage qu = ctx.ReceiveDetailESNMessages.FirstOrDefault(x => x.ReceiveDetailESNMessageID == KeyID);
                //if (qu != null)
                //{
                //    EditKeyID.Text = qu.ReceiveDetailESNMessageID.ToString();
                //    EditStatusOpen.Checked = (qu.StatusOpen == 1 ? true : false);
                //    EditStatusStop.Checked = (qu.StatusStop == 1 ? true : false);
                //    EditESN.Text = qu.ESN;
                //    EditMessage.Text = qu.Message;
                //}
                //btnEdit.Visible = true;
                //btnDelete.Visible = true;
            }
            else
            {
                //btnEdit.Visible = false;
                //btnDelete.Visible = false;

            }
        }
        void MainGrid_B_CurrentRecordContextChange(object sender, CurrentRecordContextChangeEventArgs e)
        {


            if (e.Record != null)
            {
                //MessageQueueData rec = (MessageQueueData)e.Record.GetData();
                //decimal KeyID = rec.ReceiveDetailESNMessageID;
                //clsLinqDataContext ctx = new clsLinqDataContext();
                //ReceiveDetailESNMessage qu = ctx.ReceiveDetailESNMessages.FirstOrDefault(x => x.ReceiveDetailESNMessageID == KeyID);
                //if (qu != null)
                //{
                //    EditKeyID.Text = qu.ReceiveDetailESNMessageID.ToString();
                //    EditStatusOpen.Checked = (qu.StatusOpen == 1 ? true : false);
                //    EditStatusStop.Checked = (qu.StatusStop == 1 ? true : false);
                //    EditESN.Text = qu.ESN;
                //    EditMessage.Text = qu.Message;
                //}
                //btnEdit.Visible = true;
                //btnDelete.Visible = true;
            }
            else
            {
                //btnEdit.Visible = false;
                //btnDelete.Visible = false;

            }
        }


        //void chkIncludeClosedMessages_CheckedChanged(object sender, EventArgs e)
        //{
        //    UpdateMainGrid();
        //}


        protected void UpdateMainGrid()
        {
            clsLinqDataContext ctx = new clsLinqDataContext();
            //MainGrid.DataSource = (from x in ctx.ReceiveDetailESNMessages.OrderBy(y => y.ESN)
            //                       where x.StatusOpen == 1 || chkIncludeClosedMessages.Checked == true
            //                       select new
            //                       {
            //                           x.ReceiveDetailESNMessageID,
            //                           StatusOpen = (x.StatusOpen == 1 ? "Show" : "Archived"),
            //                           StatusStop = (x.StatusStop == 1 ? "Stop Advance Past Receive" : ""),
            //                           x.ESN,
            //                           x.CreateDate,
            //                           x.CreateUser,
            //                           x.Message
            //                       });
            //MainGrid.DataBind();

            List<MessageQueueData> d = (from x in ctx.ReceiveDetailESNMessages.OrderBy(y => y.ESN)
                                        where x.StatusOpen == 1
                                        select new MessageQueueData(x.ReceiveDetailESNMessageID,
                                            (x.StatusOpen == 1 ? "Show" : "Archived"),
                                            (x.StatusStop == 1 ? "Stop Advance Past Receive" : ""),
                                            x.ESN,
                                            x.CreateDate,
                                            x.CreateUser,
                                            x.Message
                                        )).ToList();
            MainGrid_B.DataSource = d;
            MainGrid_B.DataBind();
            SetLastSelectedRecord();


            //btnEdit.Visible = false;
            //btnDelete.Visible = false;
            //if (d.Count > 0)
            //{
            //    btnEdit.Visible = true;
            //    btnDelete.Visible = true;
            //}



        }

        private void SetLastSelectedRecord()
        {

            //if (EditKeyID.Text.Length > 0)
            //{
            //    decimal KeyID = decimal.Parse(EditKeyID.Text);
            //    foreach (Record x in MainGrid_B.CurrentTable.Records)
            //    {
            //        MessageQueueData r = (MessageQueueData)x.GetData();
            //        if (r.ReceiveDetailESNMessageID == KeyID)
            //        {
            //            MainGrid_B.CurrentTable.CurrentRecord = x;
            //        }
            //    }
            //}
        }


        //void MainGrid_B_SelectedRecordsChanged(object sender, SelectedRecordsChangedEventArgs e)
        //{

        //    MessageQueueData rec = (MessageQueueData)e.SelectedRecord.Record.GetData();
        //    decimal KeyID = rec.ReceiveDetailESNMessageID;
        //    clsLinqDataContext ctx = new clsLinqDataContext();
        //    ReceiveDetailESNMessage qu = ctx.ReceiveDetailESNMessages.FirstOrDefault(x => x.ReceiveDetailESNMessageID == KeyID);
        //    if (qu != null)
        //    {
        //        EditKeyID.Text = qu.ReceiveDetailESNMessageID.ToString();
        //        EditStatusOpen.Checked = (qu.StatusOpen == 1 ? true : false);
        //        EditStatusStop.Checked = (qu.StatusStop == 1 ? true : false);
        //        EditESN.Text = qu.ESN;
        //        EditMessage.Text = qu.Message;
        //    }
        //    btnEdit.Visible = true;
        //    btnDelete.Visible = true;






        //    //foreach (SelectedRecord rec in this.MainGrid_B.Table.SelectedRecords)
        //    //{
        //    //    //rec.
        //    //}

        //    //if (e.SelectedRecord.Record..SelectedIndex >= 0)
        //    //{
        //    //    decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
        //    //    clsLinqDataContext ctx = new clsLinqDataContext();
        //    //    ReceiveDetailESNMessage qu = ctx.ReceiveDetailESNMessages.FirstOrDefault(x => x.ReceiveDetailESNMessageID == KeyID);
        //    //    if (qu != null)
        //    //    {
        //    //        EditKeyID.Text = qu.ReceiveDetailESNMessageID.ToString();
        //    //        EditStatusOpen.Checked = (qu.StatusOpen == 1 ? true : false);
        //    //        EditStatusStop.Checked = (qu.StatusStop == 1 ? true : false);
        //    //        EditESN.Text = qu.ESN;
        //    //        EditMessage.Text = qu.Message;
        //    //    }

        //    //    btnEdit.Visible = true;
        //    //    btnDelete.Visible = true;
        //    //}
        //    //else
        //    //{
        //    //    btnEdit.Visible = false;
        //    //    btnDelete.Visible = false;
        //    //}
        //}


        //protected void MainGrid_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //    if (MainGrid.SelectedIndex >= 0)
        //    {
        //        decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
        //        clsLinqDataContext ctx = new clsLinqDataContext();
        //        ReceiveDetailESNMessage qu = ctx.ReceiveDetailESNMessages.FirstOrDefault(x => x.ReceiveDetailESNMessageID == KeyID);
        //        if (qu != null)
        //        {
        //            EditKeyID.Text = qu.ReceiveDetailESNMessageID.ToString();
        //            EditStatusOpen.Checked = (qu.StatusOpen == 1 ? true : false);
        //            EditStatusStop.Checked = (qu.StatusStop == 1 ? true : false);
        //            EditESN.Text = qu.ESN;
        //            EditMessage.Text = qu.Message;
        //        }

        //        btnEdit.Visible = true;
        //        btnDelete.Visible = true;
        //    }
        //    else
        //    {
        //        btnEdit.Visible = false;
        //        btnDelete.Visible = false;
        //    }
        //}

        //#region QuesionDetail

        //protected void btnAdd_Click(object sender, EventArgs e)
        //{

        //    if (hdnESNtoAdd.Value == "null") { return; }

        //    if (hdnESNtoAdd.Value.Length == 0)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "addesn", "alert('No ESN Given');", true);
        //        return;
        //    }
        //    using (clsLinqDataContext ctx = new clsLinqDataContext())
        //    {
        //        ReceiveDetailESNMessage m = ctx.ReceiveDetailESNMessages.FirstOrDefault(x => x.ESN == hdnESNtoAdd.Value && x.StatusOpen == 1);
        //        if (m != null)
        //        {
        //            ScriptManager.RegisterStartupScript(this, GetType(), "addesn", "alert('ESN(" + hdnESNtoAdd.Value + ") already on file.');", true);
        //            return;
        //        }
        //    }


        //    AddStatusOpen.Checked = true;
        //    AddStatusStop.Checked = true;
        //    AddESN.Text = hdnESNtoAdd.Value;
        //    AddESN.Enabled = false;
        //    AddMessage.Text = "";
        //    pnlMainView.Visible = false;
        //    pnlAdd.Visible = true;
        //}
        //protected void btnEdit_Click(object sender, EventArgs e)
        //{
        //    if (MainGrid_B.CurrentTable.CurrentRecord != null)
        //    {
        //        MessageQueueData rec = (MessageQueueData)MainGrid_B.CurrentTable.CurrentRecord.GetData();
        //        decimal KeyID = rec.ReceiveDetailESNMessageID;
        //        clsLinqDataContext ctx = new clsLinqDataContext();
        //        ReceiveDetailESNMessage qu = ctx.ReceiveDetailESNMessages.FirstOrDefault(x => x.ReceiveDetailESNMessageID == KeyID);
        //        if (qu != null)
        //        {
        //            EditKeyID.Text = qu.ReceiveDetailESNMessageID.ToString();
        //            EditStatusOpen.Checked = (qu.StatusOpen == 1 ? true : false);
        //            EditStatusStop.Checked = (qu.StatusStop == 1 ? true : false);
        //            EditESN.Text = qu.ESN;
        //            EditMessage.Text = qu.Message;

        //            pnlMainView.Visible = false;
        //            pnlEdit.Visible = true;
        //        }
        //    }
        //}
        //protected void btnDelete_Click(object sender, EventArgs e)
        //{
        //    // Delete the answers.

        //    if (MainGrid_B.CurrentTable.CurrentRecord != null)
        //    {
        //        MessageQueueData rec = (MessageQueueData)MainGrid_B.CurrentTable.CurrentRecord.GetData();
        //        decimal KeyID = rec.ReceiveDetailESNMessageID;
        //        using (clsLinqDataContext ctx = new clsLinqDataContext())
        //        {
        //            ReceiveDetailESNMessage m = ctx.ReceiveDetailESNMessages.FirstOrDefault(x => x.ReceiveDetailESNMessageID == KeyID);
        //            if (m != null)
        //            {
        //                ctx.ReceiveDetailESNMessages.DeleteOnSubmit(m);
        //                ctx.SubmitChanges();
        //                UpdateMainGrid();
        //            }
        //        }
        //    }
        //}
        //protected void EditOK_Click(object sender, EventArgs e)
        //{

        //    //if (MainGrid_B.CurrentTable.CurrentRecord != null)
        //    //{
        //    //    CurRec = (MessageQueueData)MainGrid_B.CurrentTable.CurrentRecord.GetData();
        //    //}

        //    if (EditMessage.Text.Length == 0) { return; }
        //    decimal KeyID = decimal.Parse(EditKeyID.Text);
        //    using (clsLinqDataContext ctx = new clsLinqDataContext())
        //    {
        //        ReceiveDetailESNMessage m = ctx.ReceiveDetailESNMessages.FirstOrDefault(x => x.ReceiveDetailESNMessageID == KeyID);
        //        if (m != null)
        //        {
        //            m.ESN = EditESN.Text;
        //            m.Message = EditMessage.Text;
        //            m.StatusOpen = (EditStatusOpen.Checked == true ? 1 : 0);
        //            m.StatusStop = (EditStatusStop.Checked == true ? 1 : 0);

        //            m.LastUpdateDate = DateTime.Now;
        //            m.LastUpdateUser = User.Identity.Name;
        //            ctx.SubmitChanges();
        //        }
        //    }
        //    UpdateMainGrid();
        //    pnlMainView.Visible = true;
        //    pnlEdit.Visible = false;
        //}
        //protected void EditCancel_Click(object sender, EventArgs e)
        //{

        //    pnlMainView.Visible = true;
        //    pnlEdit.Visible = false;
        //    UpdateMainGrid();
        //}
        //#endregion


        //protected void AddOK_Click(object sender, EventArgs e)
        //{
        //    if (AddMessage.Text.Length == 0) { return; }
        //    using (clsLinqDataContext ctx = new clsLinqDataContext())
        //    {
        //        ReceiveDetailESNMessage m = new ReceiveDetailESNMessage();

        //        m.ESN = AddESN.Text;
        //        m.Message = AddMessage.Text;
        //        m.StatusOpen = (AddStatusOpen.Checked == true ? 1 : 0);
        //        m.StatusStop = (AddStatusStop.Checked == true ? 1 : 0);

        //        m.CreateDate = DateTime.Now;
        //        m.CreateUser = User.Identity.Name;
        //        m.LastUpdateDate = DateTime.Now;
        //        m.LastUpdateUser = User.Identity.Name;

        //        ctx.ReceiveDetailESNMessages.InsertOnSubmit(m);
        //        ctx.SubmitChanges();
        //    }
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

    [Serializable()]


    public class EmailLogData
    {

        public string Source { get; set; }
        public string Status { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateUser { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public string LastUpdateUser { get; set; }
        public DateTime SendDate { get; set; }
        public string ProcessText { get; set; }
        public string ESN { get; set; }
        public string Version { get; set; }
        public decimal EmailLogID { get; set; }
        public decimal ReceiveDetailID { get; set; }
        public decimal ReceiveDetailProcessLogID { get; set; }

        //public decimal ReceiveDetailESNMessageID { get; set; }
        //public string StatusOpen { get; set; }
        //public string StatusStop { get; set; }
        //public string ESN { get; set; }
        //public DateTime CreateDate { get; set; }
        //public string CreateUser { get; set; }
        //public string Message { get; set; }

        public EmailLogData(string source, string status, string to, string subject, string body
            , DateTime createdate, string createuser, DateTime lastupdatedate, string lastupdateuser, DateTime senddate
            , string processtext, string esn, string version, decimal emaillogid, decimal receivedetailid, decimal receivedetailprocesslogid)
        {
            Source = source;
            Status = status;
            To = to;
            Subject = subject;
            Body = body;
            CreateDate = createdate;
            CreateUser = createuser;
            LastUpdateDate = lastupdatedate;
            LastUpdateUser = lastupdateuser;
            SendDate = senddate;
            ProcessText = processtext;
            ESN = esn;
            Version = version;
            EmailLogID = emaillogid;
            ReceiveDetailID = receivedetailid;
            ReceiveDetailProcessLogID = receivedetailprocesslogid;

        }
    }

}