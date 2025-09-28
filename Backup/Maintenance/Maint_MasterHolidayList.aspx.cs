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

namespace BW_WebApp.Maintenance
{
    public partial class Maint_MasterHolidayList : System.Web.UI.Page
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
            chkIncludeClosedMessages.CheckedChanged += new EventHandler(chkIncludeClosedMessages_CheckedChanged);
            this.MainGrid_B.ShowGroupDropArea = false;

            //this.MainGrid_B.TableDescriptor.Appearance.AlternateRecordFieldCell.BackColor = Color.AliceBlue;
            this.MainGrid_B.Appearance.AlternateRecordFieldCell.Interior = new Syncfusion.Drawing.BrushInfo(System.Drawing.ColorTranslator.FromHtml("#E2E2E2"));
            //this.MainGrid_B.
            //MainGrid_B.CurrentTable.SelectedRecordsChanged += new SelectedRecordsChangedEventHandler(CurrentTable_SelectedRecordsChanged);
            //MainGrid_B.Table.SelectedRecordsChanged += new SelectedRecordsChangedEventHandler(Table_SelectedRecordsChanged);
            if (!IsPostBack)
            {
                clsLinqDataContext ctx = new clsLinqDataContext();
                pnlAdd.Visible = false;
                pnlEdit.Visible = false;

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
                btnEdit.Visible = true;
                btnDelete.Visible = true;
            }
            else
            {
                btnEdit.Visible = false;
                btnDelete.Visible = false;

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
                btnEdit.Visible = true;
                btnDelete.Visible = true;
            }
            else
            {
                btnEdit.Visible = false;
                btnDelete.Visible = false;

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
                btnEdit.Visible = true;
                btnDelete.Visible = true;
            }
            else
            {
                btnEdit.Visible = false;
                btnDelete.Visible = false;

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



        void chkIncludeClosedMessages_CheckedChanged(object sender, EventArgs e)
        {
            UpdateMainGrid();
        }


        protected void UpdateMainGrid()
        {
            clsLinqDataContext ctx = new clsLinqDataContext();

            //List<MessageQueueData> d = (from x in ctx.ReceiveDetailESNMessages.OrderBy(y => y.ESN)
            //                            where x.StatusOpen == 1 || chkIncludeClosedMessages.Checked == true
            //                            select new MessageQueueData(x.ReceiveDetailESNMessageID,
            //                                (x.StatusOpen == 1 ? "Show" : "Archived"),
            //                                (x.StatusStop == 1 ? "Stop Advance Past Receive" : ""),
            //                                x.ESN,
            //                                x.CreateDate,
            //                                x.CreateUser,
            //                                x.Message
            //                            )).ToList();
            //MainGrid_B.DataSource = d;
            //MainGrid_B.DataBind();
            //SetLastSelectedRecord();


            List<HolidayData> d = null;
            if (chkIncludeClosedMessages.Checked == false)
            {
                d = (from x in ctx.MasterHolidays
                     where x.HolidayDate >= DateTime.Now
                     select new HolidayData(x.MasterHolidayID, x.Name, x.HolidayDate)).ToList();
            }
            else
            {
                d = (from x in ctx.MasterHolidays
                     //where x.HolidayDate >= DateTime.Now
                     select new HolidayData(x.MasterHolidayID, x.Name, x.HolidayDate)).ToList();
            }
            MainGrid_B.DataSource = d;                          //.OrderBy(x=> x.HolidayDate);
            MainGrid_B.DataBind();
            SetLastSelectedRecord();

            btnEdit.Visible = false;
            btnDelete.Visible = false;
            if (d.Count > 0)
            {
                btnEdit.Visible = true;
                btnDelete.Visible = true;
            }
        }

        private void SetLastSelectedRecord()
        {

            if (EditKeyID.Text.Length > 0)
            {
                decimal KeyID = decimal.Parse(EditKeyID.Text);
                foreach (Record x in MainGrid_B.CurrentTable.Records)
                {
                    HolidayData r = (HolidayData)x.GetData();
                    if (r.MasterHolidayID == KeyID)
                    {
                        MainGrid_B.CurrentTable.CurrentRecord = x;
                    }
                }
            }
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

        #region QuesionDetail

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            AddName.Text = "";
            pnlMainView.Visible = false;
            pnlAdd.Visible = true;
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (MainGrid_B.CurrentTable.CurrentRecord != null)
            {
                HolidayData h = (HolidayData)MainGrid_B.CurrentTable.CurrentRecord.GetData();
                decimal KeyID = h.MasterHolidayID;
                using (clsLinqDataContext ctx = new clsLinqDataContext())
                {
                    MasterHoliday o = ctx.MasterHolidays.FirstOrDefault(x => x.MasterHolidayID == KeyID);
                    if (o != null)
                    {
                        EditKeyID.Text = o.MasterHolidayID.ToString();
                        EditName.Text = o.Name;
                        EditDate.Text = o.HolidayDate.ToString("MM/dd/yyyy");
                    }
                    pnlMainView.Visible = false;
                    pnlEdit.Visible = true;
                    //}
                }
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            // Delete the answers.

            if (MainGrid_B.CurrentTable.CurrentRecord != null)
            {
                HolidayData h = (HolidayData)MainGrid_B.CurrentTable.CurrentRecord.GetData();
                decimal KeyID = h.MasterHolidayID;
                using (clsLinqDataContext ctx = new clsLinqDataContext())
                {
                    MasterHoliday o = ctx.MasterHolidays.FirstOrDefault(x => x.MasterHolidayID == KeyID);
                    if (o != null)
                    {
                        ctx.MasterHolidays.DeleteOnSubmit(o);
                        ctx.SubmitChanges();
                        UpdateMainGrid();
                    }
                }
            }
        }
        protected void EditOK_Click(object sender, EventArgs e)
        {

            ////if (MainGrid_B.CurrentTable.CurrentRecord != null)
            ////{
            ////    CurRec = (MessageQueueData)MainGrid_B.CurrentTable.CurrentRecord.GetData();
            ////}

            //if (EditMessage.Text.Length == 0) { return; }
            decimal KeyID = decimal.Parse(EditKeyID.Text);
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                MasterHoliday h = ctx.MasterHolidays.FirstOrDefault(x => x.MasterHolidayID == KeyID);
                {
                    if (h != null)
                    {
                        DateTime hd = DateTime.Now;
                        if (DateTime.TryParse(EditDate.Text, out hd) == false) { hd = DateTime.Now; }
                        h.HolidayDate = hd;
                        h.LastUpdateDate = DateTime.Now;
                        h.LastUpdateUser = User.Identity.Name;
                        h.Name = EditName.Text;
                        ctx.SubmitChanges();
                    }

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
            UpdateMainGrid();
        }
        #endregion


        protected void AddOK_Click(object sender, EventArgs e)
        {
            if (AddName.Text.Length == 0) { return; }

            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                DateTime hd = DateTime.Now;
                if (DateTime.TryParse(AddDate.Text, out hd) == false) { hd = DateTime.Now; }

                MasterHoliday h = new MasterHoliday();
                h.HolidayDate = hd;
                h.Name = AddName.Text;
                h.Active = 1;
                h.CreateUser = User.Identity.Name;
                h.LastUpdateDate = DateTime.Now;
                h.LastUpdateUser = User.Identity.Name;

                ctx.MasterHolidays.InsertOnSubmit(h);
                ctx.SubmitChanges();

                EditKeyID.Text = h.MasterHolidayID.ToString();
                //ReceiveDetailESNMessage m = ctx.ReceiveDetailESNMessages.FirstOrDefault(x => x.ESN == hdnESNtoAdd.Value && x.StatusOpen == 1);
                //if (m != null)
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "addesn", "alert('ESN(" + hdnESNtoAdd.Value + ") already on file.');", true);
                //    return;
                //}
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

    [Serializable()]
    public class HolidayData
    {
        public decimal MasterHolidayID { get; set; }
        public string Name { get; set; }
        public DateTime HolidayDate { get; set; }

        public HolidayData(decimal ID, string name, DateTime holidayDate)
        {
            MasterHolidayID = ID;
            Name = name;
            HolidayDate = holidayDate;
        }
    }

}