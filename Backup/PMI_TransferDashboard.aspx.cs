using System;
using System.Drawing;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Syncfusion.Grouping;
using BW_WebApp.DataManagers;

namespace BW_WebApp
{
    public partial class PMI_TransferDashboard : System.Web.UI.Page
    {



        protected void Page_Load(object sender, EventArgs e)
        {
            //MainGrid.AllowSorting = true;
            //MainGrid.SelectedIndexChanged += new EventHandler(MainGrid_SelectedIndexChanged);
            //MainGrid_B.SelectedRecordsChanged += new SelectedRecordsChangedEventHandler(MainGrid_B_SelectedRecordsChanged);
            //MainGrid_B.CurrentRecordContextChange += new CurrentRecordContextChangeEventHandler(MainGrid_B_CurrentRecordContextChange);
            //MainGrid.Sorting += new GridViewSortEventHandler(MainGrid_Sorting);
            //chkIncludeClosedMessages.CheckedChanged += new EventHandler(chkIncludeClosedMessages_CheckedChanged);
            this.MainGrid_B.ShowGroupDropArea = false;
            drpFileType.SelectedIndexChanged += new EventHandler(drpFileType_SelectedIndexChanged);
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

        void drpFileType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMainGrid();
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
            else
            {
                //btnEdit.Visible = false;
                //btnDelete.Visible = false;

            }
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
        //void SortedColumns_Changed(object sender, Syncfusion.Collections.ListPropertyChangedEventArgs e)
        //{
        //    if (e.Action == Syncfusion.Collections.ListPropertyChangedType.Add || e.Action == Syncfusion.Collections.ListPropertyChangedType.Insert)
        //    {

        //        SortColumnDescriptor sd = e.Item as SortColumnDescriptor;
        //        ////SortColumnDescriptor sd = this.GridGroupingControl1.TableDescriptor.Columns.GetColumnDescriptor("Name");
        //        //if (sd.Name == "Name")
        //        //{
        //        //    sd.Comparer = new NameComparer();
        //        //}
        //    }
        //    else if (!IsAlreadySorted)
        //    {
        //        if (e.Action == Syncfusion.Collections.ListPropertyChangedType.ItemPropertyChanged)
        //        {
        //            SortColumnDescriptor sd = e.Item as SortColumnDescriptor;
        //            //if (sd.Name == "Name")
        //            //{
        //            //    IsAlreadySorted = true;
        //            //    sd.Comparer = new NameComparer();
        //            //}
        //        }
        //    }
        //}
        //void MainGrid_Sorting(object sender, GridViewSortEventArgs e)
        //{
        //    string sortExpression = e.SortExpression;


        //    clsLinqDataContext ctx = new clsLinqDataContext();

        //    switch (e.SortExpression)
        //    {
        //        case "ESN":
        //            {
        //                MainGrid.DataSource = (from x in ctx.ReceiveDetailESNMessages.OrderBy(y => y.ESN)
        //                                       where x.StatusOpen == 1 || chkIncludeClosedMessages.Checked == true
        //                                       orderby x.ESN
        //                                       select new
        //                                       {
        //                                           x.ReceiveDetailESNMessageID,
        //                                           StatusOpen = (x.StatusOpen == 1 ? "Show" : "Archived"),
        //                                           StatusStop = (x.StatusStop == 1 ? "Stop Advance Past Receive" : ""),
        //                                           x.ESN,
        //                                           x.CreateDate,
        //                                           x.CreateUser,
        //                                           x.Message
        //                                       });
        //                break;
        //            }
        //        case "StatusOpen":
        //            {
        //                MainGrid.DataSource = (from x in ctx.ReceiveDetailESNMessages.OrderBy(y => y.ESN)
        //                                       where x.StatusOpen == 1 || chkIncludeClosedMessages.Checked == true
        //                                       orderby x.StatusOpen
        //                                       select new
        //                                       {
        //                                           x.ReceiveDetailESNMessageID,
        //                                           StatusOpen = (x.StatusOpen == 1 ? "Show" : "Archived"),
        //                                           StatusStop = (x.StatusStop == 1 ? "Stop Advance Past Receive" : ""),
        //                                           x.ESN,
        //                                           x.CreateDate,
        //                                           x.CreateUser,
        //                                           x.Message
        //                                       });
        //                break;
        //            }
        //        case "StatusStop":
        //            {
        //                MainGrid.DataSource = (from x in ctx.ReceiveDetailESNMessages.OrderBy(y => y.ESN)
        //                                       where x.StatusOpen == 1 || chkIncludeClosedMessages.Checked == true
        //                                       orderby x.StatusStop
        //                                       select new
        //                                       {
        //                                           x.ReceiveDetailESNMessageID,
        //                                           StatusOpen = (x.StatusOpen == 1 ? "Show" : "Archived"),
        //                                           StatusStop = (x.StatusStop == 1 ? "Stop Advance Past Receive" : ""),
        //                                           x.ESN,
        //                                           x.CreateDate,
        //                                           x.CreateUser,
        //                                           x.Message
        //                                       });
        //                break;
        //            }
        //        case "CreateDate":
        //            {
        //                MainGrid.DataSource = (from x in ctx.ReceiveDetailESNMessages.OrderBy(y => y.ESN)
        //                                       where x.StatusOpen == 1 || chkIncludeClosedMessages.Checked == true
        //                                       orderby x.CreateDate
        //                                       select new
        //                                       {
        //                                           x.ReceiveDetailESNMessageID,
        //                                           StatusOpen = (x.StatusOpen == 1 ? "Show" : "Archived"),
        //                                           StatusStop = (x.StatusStop == 1 ? "Stop Advance Past Receive" : ""),
        //                                           x.ESN,
        //                                           x.CreateDate,
        //                                           x.CreateUser,
        //                                           x.Message
        //                                       });
        //                break;
        //            }
        //        case "CreateUser":
        //            {
        //                MainGrid.DataSource = (from x in ctx.ReceiveDetailESNMessages.OrderBy(y => y.ESN)
        //                                       where x.StatusOpen == 1 || chkIncludeClosedMessages.Checked == true
        //                                       orderby x.CreateUser
        //                                       select new
        //                                       {
        //                                           x.ReceiveDetailESNMessageID,
        //                                           StatusOpen = (x.StatusOpen == 1 ? "Show" : "Archived"),
        //                                           StatusStop = (x.StatusStop == 1 ? "Stop Advance Past Receive" : ""),
        //                                           x.ESN,
        //                                           x.CreateDate,
        //                                           x.CreateUser,
        //                                           x.Message
        //                                       });
        //                break;
        //            }
        //    }
        //    MainGrid.DataBind();

        //}



        //void chkIncludeClosedMessages_CheckedChanged(object sender, EventArgs e)
        //{
        //    UpdateMainGrid();
        //}


        protected void UpdateMainGrid()
        {
            clsLinqDataContext ctx = new clsLinqDataContext();

            List<PMI_TransferData> d = null;


            //var zz = from x in ctx.MasterFileTransferDetailLogs
            //         //where x.HolidayDate >= DateTime.Now
            //         select new PMI_TransferData(x.MasterFileTransferLogID, x.MasterFileTransferDetailLogID, x.MasterFileTransferLog.ClientID,
            //             x.ClientLocationID, x.ReceiveDetailPreReceiveID, x.ReceiveDetailID, x.MasterFileTransferLog.TransferType,
            //             x.MasterFileTransferLog.Status, x.MasterFileTransferLog.FileName, x.MasterFileTransferLog.Name, x.MasterFileTransferLog.CreateDate,
            //             x.MasterFileTransferLog.CreateUser, x.MasterFileTransferLog.LastUpdateDate, x.MasterFileTransferLog.LastUpdateUser,


            //             x.MasterFileTransferLog.TransferDate,
            //             x.ESN, x.Status, x.StatusMessage, x.RecordText, x.CreateDate, x.CreateUser,
            //             x.LastUpdateDate, x.LastUpdateUser);


            if (drpFileType.SelectedValue.ToUpper() == "ALL")
            {
                d = (from x in ctx.MasterFileTransferDetailLogs
                     //where x.HolidayDate >= DateTime.Now
                     select new PMI_TransferData(x.MasterFileTransferLogID, x.MasterFileTransferDetailLogID, x.MasterFileTransferLog.ClientID,
                         x.ClientLocationID, x.ReceiveDetailPreReceiveID, x.ReceiveDetailID, x.MasterFileTransferLog.TransferType,
                         x.MasterFileTransferLog.Status, x.MasterFileTransferLog.FileName, x.MasterFileTransferLog.Name, x.MasterFileTransferLog.CreateDate,
                         x.MasterFileTransferLog.CreateUser, x.MasterFileTransferLog.LastUpdateDate, x.MasterFileTransferLog.LastUpdateUser,
                         x.MasterFileTransferLog.TransferDate,
                         x.ESN, x.Status, x.StatusMessage, x.RecordText, x.CreateDate, x.CreateUser,
                         x.LastUpdateDate, x.LastUpdateUser)).ToList();
            }
            else
            {
                d = (from x in ctx.MasterFileTransferDetailLogs
                     where x.MasterFileTransferLog.TransferType == drpFileType.SelectedValue
                     select new PMI_TransferData(x.MasterFileTransferLogID, x.MasterFileTransferDetailLogID, x.MasterFileTransferLog.ClientID,
                         x.ClientLocationID, x.ReceiveDetailPreReceiveID, x.ReceiveDetailID, x.MasterFileTransferLog.TransferType,
                         x.MasterFileTransferLog.Status, x.MasterFileTransferLog.FileName, x.MasterFileTransferLog.Name, x.MasterFileTransferLog.CreateDate,
                         x.MasterFileTransferLog.CreateUser, x.MasterFileTransferLog.LastUpdateDate, x.MasterFileTransferLog.LastUpdateUser,
                         x.MasterFileTransferLog.TransferDate,
                         x.ESN, x.Status, x.StatusMessage, x.RecordText, x.CreateDate, x.CreateUser,
                         x.LastUpdateDate, x.LastUpdateUser)).ToList();

//yyyyMMdd_hhmmss


            }
            MainGrid_B.DataSource = d;                          //.OrderBy(x=> x.HolidayDate);
            MainGrid_B.DataBind();
            SetLastSelectedRecord();


            btnNotReceived.Visible = false;
            if (drpFileType.SelectedValue.ToUpper() == "FILE01" || drpFileType.SelectedValue.ToUpper() == "ALL") { btnNotReceived.Visible = true; }

        }

        private void SetLastSelectedRecord()
        {

            //if (EditKeyID.Text.Length > 0)
            //{
            //    decimal KeyID = decimal.Parse(EditKeyID.Text);
            //    foreach (Record x in MainGrid_B.CurrentTable.Records)
            //    {
            //        PMI_TransferData r = (PMI_TransferData)x.GetData();
            //        //if (r.MasterHolidayID == KeyID)
            //        //{
            //        //    MainGrid_B.CurrentTable.CurrentRecord = x;
            //        //}
            //    }
            //}
        }

        protected void btnStartPMITransfer_Click(object sender, EventArgs e)
        {
            FileTransferLogManager m = new FileTransferLogManager(User.Identity.Name);
            m.TriggerToRunPMITransfer();
            ScriptManager.RegisterStartupScript(this, GetType(), "Done", "alert('PMI Transfer Set to go? The Actual transfer will begin when next cycle runs.');", true);


        }


        //#region QuesionDetail

        //protected void btnAdd_Click(object sender, EventArgs e)
        //{
        //    AddName.Text = "";
        //    pnlMainView.Visible = false;
        //    pnlAdd.Visible = true;
        //}
        //protected void btnEdit_Click(object sender, EventArgs e)
        //{
        //    if (MainGrid_B.CurrentTable.CurrentRecord != null)
        //    {
        //        HolidayData h = (HolidayData)MainGrid_B.CurrentTable.CurrentRecord.GetData();
        //        decimal KeyID = h.MasterHolidayID;
        //        using (clsLinqDataContext ctx = new clsLinqDataContext())
        //        {
        //            MasterHoliday o = ctx.MasterHolidays.FirstOrDefault(x => x.MasterHolidayID == KeyID);
        //            if (o != null)
        //            {
        //                EditKeyID.Text = o.MasterHolidayID.ToString();
        //                EditName.Text = o.Name;
        //                EditDate.Text = o.HolidayDate.ToString("MM/dd/yyyy");
        //            }
        //            pnlMainView.Visible = false;
        //            pnlEdit.Visible = true;
        //            //}
        //        }
        //    }
        //}




        //protected void EditOK_Click(object sender, EventArgs e)
        //{

        //    ////if (MainGrid_B.CurrentTable.CurrentRecord != null)
        //    ////{
        //    ////    CurRec = (MessageQueueData)MainGrid_B.CurrentTable.CurrentRecord.GetData();
        //    ////}

        //    //if (EditMessage.Text.Length == 0) { return; }
        //    decimal KeyID = decimal.Parse(EditKeyID.Text);
        //    using (clsLinqDataContext ctx = new clsLinqDataContext())
        //    {
        //        MasterHoliday h = ctx.MasterHolidays.FirstOrDefault(x => x.MasterHolidayID == KeyID);
        //        {
        //            if (h != null)
        //            {
        //                DateTime hd = DateTime.Now;
        //                if (DateTime.TryParse(EditDate.Text, out hd) == false) { hd = DateTime.Now; }
        //                h.HolidayDate = hd;
        //                h.LastUpdateDate = DateTime.Now;
        //                h.LastUpdateUser = User.Identity.Name;
        //                h.Name = EditName.Text;
        //                ctx.SubmitChanges();
        //            }

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
        //    if (AddName.Text.Length == 0) { return; }

        //    using (clsLinqDataContext ctx = new clsLinqDataContext())
        //    {
        //        DateTime hd = DateTime.Now;
        //        if (DateTime.TryParse(AddDate.Text, out hd) == false) { hd = DateTime.Now; }

        //        MasterHoliday h = new MasterHoliday();
        //        h.HolidayDate = hd;
        //        h.Name = AddName.Text;
        //        h.Active = 1;
        //        h.CreateUser = User.Identity.Name;
        //        h.LastUpdateDate = DateTime.Now;
        //        h.LastUpdateUser = User.Identity.Name;

        //        ctx.MasterHolidays.InsertOnSubmit(h);
        //        ctx.SubmitChanges();

        //        EditKeyID.Text = h.MasterHolidayID.ToString();
        //        //ReceiveDetailESNMessage m = ctx.ReceiveDetailESNMessages.FirstOrDefault(x => x.ESN == hdnESNtoAdd.Value && x.StatusOpen == 1);
        //        //if (m != null)
        //        //{
        //        //    ScriptManager.RegisterStartupScript(this, GetType(), "addesn", "alert('ESN(" + hdnESNtoAdd.Value + ") already on file.');", true);
        //        //    return;
        //        //}
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


    public class PMI_TransferData
    {
        public decimal MasterFileTransferLogID { get; set; }
        public decimal MasterFileTransferDetailLogID { get; set; }
        public decimal ClientID { get; set; }
        public decimal ClientLocationID { get; set; }
        public decimal ReceiveDetailPreReceiveID { get; set; }
        public decimal ReceiveDetailID { get; set; }
        public string TransferType { get; set; }
        public string Status { get; set; }
        public string FileName { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateUser { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public string LastUpdateUser { get; set; }
        public DateTime? TransferDate { get; set; }
        public string ESN { get; set; }
        public string StatusDetail { get; set; }
        public string StatusMessage { get; set; }
        public string RecordText { get; set; }
        public DateTime CreateDateDetail { get; set; }
        public string CreateUserDetail { get; set; }
        public DateTime LastUpdateDateDetail { get; set; }
        public string LastUpdateUserDetail { get; set; }

        public PMI_TransferData(decimal masterFileTransferLogID, decimal masterFileTransferDetailLogID, decimal? clientID,
        decimal clientLocationID, decimal? receiveDetailPreReceiveID, decimal? receiveDetailID, string transferType,
        string status, string fileName, string name, DateTime createDate, string createUser, DateTime lastUpdateDate,
        string lastUpdateUser, DateTime? transferDate, string eSN, string statusDetail, string statusMessage,
        string recordText, DateTime createDateDetail, string createUserDetail, DateTime lastUpdateDateDetail, string lastUpdateUserDetail)
        {
            MasterFileTransferLogID = masterFileTransferLogID;
            MasterFileTransferDetailLogID = masterFileTransferDetailLogID;
            ClientID = (clientID == null) ? -1 : (decimal)clientID;
            ClientLocationID = clientLocationID;
            ReceiveDetailPreReceiveID = (receiveDetailPreReceiveID == null) ? -1 : (decimal)receiveDetailPreReceiveID;
            ReceiveDetailID = (receiveDetailID == null) ? -1 : (decimal)receiveDetailID;
            TransferType = transferType;
            Status = status;
            FileName = fileName;
            Name = name;
            CreateDate = createDate;
            CreateUser = createUser;
            LastUpdateDate = lastUpdateDate;
            LastUpdateUser = lastUpdateUser;

            TransferDate = transferDate;
            ESN = eSN;
            StatusDetail = statusDetail;
            StatusMessage = statusMessage;
            RecordText = recordText;
            CreateDateDetail = createDateDetail;
            CreateUserDetail = createUserDetail;
            LastUpdateDateDetail = lastUpdateDateDetail;
            LastUpdateUserDetail = lastUpdateUserDetail;
        }


    }

    //public class HolidayData
    //{
    //    public decimal MasterHolidayID { get; set; }
    //    public string Name { get; set; }
    //    public DateTime HolidayDate { get; set; }

    //    public HolidayData(decimal ID, string name, DateTime holidayDate)
    //    {
    //        MasterHolidayID = ID;
    //        Name = name;
    //        HolidayDate = holidayDate;
    //    }
    //}

}