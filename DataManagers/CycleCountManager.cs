using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Security;
using System.Web.UI.WebControls;


//using GMPDemo;
//using BusinessLayer;
//using Factory_DataModel;

namespace BW_WebApp.DataManagers
{

    public class CycleCountManager : DataManagers
    {

        //string UserName = string.Empty;
        public string[] UserRoles { get; set; }
        public BL_UserDataRestrictor UserRestrict = null;
        //public StatisticalManager SM = null;

        //public clsLinqDataContext GetDataContext(string UserName)
        //{
        //    clsLinqDataContext ctx = new clsLinqDataContext();
        //    ctx.UserName = UserName;
        //    return ctx;
        //}
        public CycleCountManager(string Username)
        {

            UserRoles = Roles.GetRolesForUser(Username);
            if (Username.Trim().Length == 0)
            {
                Username = "NullUser";
            }
            UserName = Username;
            UserRestrict = new BL_UserDataRestrictor(UserName);
            //SM = new StatisticalManager(UserName);
        }
        //public String UserName
        //{
        //    get { return UserName; }
        //    set { UserName = value; }
        //}
        //public string UserRoleString
        //{
        //    get
        //    {
        //        string[] selectedusersRoles = Roles.GetRolesForUser(UserName);
        //        string sRoles = "";
        //        foreach (string r in selectedusersRoles)
        //        {
        //            sRoles += r + ",";
        //        }
        //        return sRoles;
        //        // Returns a string link "Admin,Supervisor,Other"
        //    }
        //}


        public CycleInventoryCountTemplateHeader GetTemplate(decimal ID)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                return GetTemplate(ctx, ID);
            }
        }
        public CycleInventoryCountTemplateHeader GetTemplate(clsLinqDataContext ctx, decimal ID)
        {
            return ctx.CycleInventoryCountTemplateHeaders.FirstOrDefault(x => x.CycleInventoryCountTemplateHeaderID == ID);
        }


        public string LogCCBatchLocked(string Batch)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                return LogCCBatchLocked(ctx, Batch);
            }
        }
        public string LogCCBatchLocked(clsLinqDataContext ctx, string Batch)
        {
            decimal count = 0;
            var data = ctx.CycleCountInventoryCounts.Where(x => x.Batch == Batch && x.Status != "Invalid");
            foreach (CycleCountInventoryCount r in data)
            {
                r.isBatchLocked = true;
                r.Status = "Active";
                count++;
            }
            ctx.SubmitChanges();
            return "Batch Records Locked:" + count.ToString();
        }

        public List<vwGridCycleInventoryCount_B> GetCycleCountData(decimal CycleInventoryCountHeaderID, string Type, ref string QuerryString)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                return GetCycleCountData(ctx, CycleInventoryCountHeaderID, Type, ref QuerryString);
            }
        }
        public List<vwGridCycleInventoryCount_B> GetCycleCountData(clsLinqDataContext ctx, decimal CycleInventoryCountHeaderID, string Type, ref string QuerryString)
        {
            QuerryString = "Select * from vwGridCycleInventoryCount_B where Status = \'" + Type + "\' and CycleInventoryCountHeaderID = " + CycleInventoryCountHeaderID.ToString();
            return ctx.vwGridCycleInventoryCount_Bs.Where(x => x.Status == Type && x.CycleInventoryCountHeaderID == CycleInventoryCountHeaderID).OrderBy(x => x.Batch).OrderByDescending(x => x.Batch).ToList();
        }

        public string LogCycleCountInventoryCount(string Batch, decimal Quantity, string ESN, bool isDevice, decimal ClientLocationID, string UserName)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                return LogCycleCountInventoryCount(ctx, Batch, Quantity, ESN, isDevice, ClientLocationID, UserName);
            }
        }
        public string LogCycleCountInventoryCount(clsLinqDataContext ctx, string Batch, decimal Quantity, string ESN, bool isDevice, decimal ClientLocationID, string UserName)
        {
            string rMessage = "";
            ctx.CycleCountPhysicalScanAdd(Batch, Quantity, ESN, isDevice, ClientLocationID, UserName, ref rMessage);
            return rMessage;
        }

        #region Batch Utilities


        public string LogPhysicalInventoryBatchLocked(decimal CycleInventoryCountHeaderID, string Batch)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                return LogPhysicalInventoryBatchLocked(ctx, CycleInventoryCountHeaderID, Batch);
            }
        }
        public string LogPhysicalInventoryBatchLocked(clsLinqDataContext ctx, decimal CycleInventoryCountHeaderID, string Batch)
        {
            decimal count = 0;
            var data = ctx.CycleCountInventoryCounts.Where(x => x.Batch == Batch && x.Status != "Invalid");
            foreach (CycleCountInventoryCount r in data)
            {
                r.isBatchLocked = true;
                //r.Status = "Active";
                count++;
            }
            ctx.SubmitChanges();
            return "Batch Records Locked:" + count.ToString();
        }
        public string LogPhysicalInventoryBatchClean(decimal CycleInventoryCountHeaderID, string Batch)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                return LogPhysicalInventoryBatchClean(ctx, CycleInventoryCountHeaderID, Batch);
            }
        }
        public string LogPhysicalInventoryBatchClean(clsLinqDataContext ctx, decimal CycleInventoryCountHeaderID, string Batch)
        {
            //decimal count = 0;
            string rMessage = "";
            var data = ctx.Update_ReceiveDetailPhysicalCycleCountScanClean(Batch, ref rMessage);
            ctx.SubmitChanges();
            return rMessage;
        }
        public string LogPhysicalInventoryBatchInvalid(decimal CycleInventoryCountHeaderID, string Batch)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                return LogPhysicalInventoryBatchInvalid(ctx, CycleInventoryCountHeaderID, Batch);
            }
        }
        public string LogPhysicalInventoryBatchInvalid(clsLinqDataContext ctx, decimal CycleInventoryCountHeaderID, string Batch)
        {
            decimal count = 0;
            var data = ctx.CycleCountInventoryCounts.Where(x => x.Batch == Batch);
            foreach (CycleCountInventoryCount r in data)
            {
                r.Status = "Invalid";
                r.isBatchLocked = false;
                count++;
            }
            ctx.SubmitChanges();
            return "Batch Records Marked Invalid:" + count.ToString();
        }
        public string LogPhysicalInventoryBatchHold(decimal CycleInventoryCountHeaderID, string Batch)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                return LogPhysicalInventoryBatchHold(ctx, CycleInventoryCountHeaderID, Batch);
            }
        }
        public string LogPhysicalInventoryBatchHold(clsLinqDataContext ctx, decimal CycleInventoryCountHeaderID, string Batch)
        {
            decimal count = 0;
            var data = ctx.CycleCountInventoryCounts.Where(x => x.Batch == Batch);
            foreach (CycleCountInventoryCount r in data)
            {
                r.Status = "Hold";
                //r.isBatchLocked = true;
                count++;
            }
            ctx.SubmitChanges();
            return "Batch Records Marked Hold:" + count.ToString();
        }
        public string LogPhysicalInventoryBatchOpen(decimal CycleInventoryCountHeaderID, string Batch)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                return LogPhysicalInventoryBatchOpen(ctx, CycleInventoryCountHeaderID, Batch);
            }
        }
        public string LogPhysicalInventoryBatchOpen(clsLinqDataContext ctx, decimal CycleInventoryCountHeaderID, string Batch)
        {
            decimal count = 0;
            var data = ctx.CycleCountInventoryCounts.Where(x => x.Batch == Batch);
            foreach (CycleCountInventoryCount r in data)
            {
                r.Status = "Open";
                r.isBatchLocked = false;
                count++;
            }
            ctx.SubmitChanges();
            return "Batch Records Marked Open:" + count.ToString();
        }
        public string LogPhysicalInventoryBatchToSyncReady(decimal CycleInventoryCountHeaderID, string Batch)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                return LogPhysicalInventoryBatchToSyncReady(ctx, CycleInventoryCountHeaderID, Batch);
            }
        }
        public string LogPhysicalInventoryBatchToSyncReady(clsLinqDataContext ctx, decimal CycleInventoryCountHeaderID, string Batch)
        {
            string rMessage = "";
            ctx.IFS_GenerateInvtTran_PIDevice(Batch, UserName, ref rMessage);
            var data = ctx.CycleCountInventoryCounts.Where(x => x.Batch == Batch);
            foreach (CycleCountInventoryCount r in data)
            {
                r.Status = "SYNC Ready";
                r.isBatchLocked = true;
            }
            ctx.SubmitChanges();
            return rMessage;
        }
        public string LogPhysicalInventoryBatchToClosed(decimal CycleInventoryCountHeaderID, string Batch)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                return LogPhysicalInventoryBatchToClosed(ctx, CycleInventoryCountHeaderID, Batch);
            }
        }
        public string LogPhysicalInventoryBatchToClosed(clsLinqDataContext ctx, decimal CycleInventoryCountHeaderID, string Batch)
        {
            string rMessage = "";
            ctx.IFS_GenerateInvtTran_PIDevice(Batch, UserName, ref rMessage);
            var data = ctx.CycleCountInventoryCounts.Where(x => x.Batch == Batch);
            foreach (CycleCountInventoryCount r in data)
            {
                r.Status = "Closed";
                r.isBatchLocked = true;
            }
            ctx.SubmitChanges();
            return rMessage;
        }
        #endregion
        #region Template
        public List<vwGetCCTemplateHeader> TemplateGridData_Active(ref string QueryString)
        {
            using (clsLinqDataContext ctx = GetDataContext(UserName))
            {
                return TemplateGridData(ctx, "Active", ref QueryString);
            }
        }
        public List<vwGetCCTemplateHeader> TemplateGridData_Inactive(ref string QueryString)
        {
            using (clsLinqDataContext ctx = GetDataContext(UserName))
            {
                return TemplateGridData(ctx, "Inactive", ref QueryString);
            }
        }
        public List<vwGetCCTemplateHeader> TemplateGridData(string Status, ref string QueryString)
        {
            using (clsLinqDataContext ctx = GetDataContext(UserName))
            {
                return TemplateGridData(ctx, Status, ref QueryString);
            }
        }
        public List<vwGetCCTemplateHeader> TemplateGridData(clsLinqDataContext ctx, string Status, ref string QueryString)
        {
            QueryString = "Select * from vwGetCCTemplateHeader where Status = \'" + Status + "\'";
            return ctx.vwGetCCTemplateHeaders.Where(x => x.Status == Status).ToList();

        }

        public List<vwGetCCRunHeader> GridDataRun(string Status, ref string QueryString)
        {
            using (clsLinqDataContext ctx = GetDataContext(UserName))
            {
                return GridDataRun(ctx, Status, ref QueryString);
            }
        }
        public List<vwGetCCRunHeader> GridDataRun(clsLinqDataContext ctx, string Status, ref string QueryString)
        {
            QueryString = "Select * from vwGetCCRunHeader where Status = \'" + Status + "\'";
            return ctx.vwGetCCRunHeaders.Where(x => x.Status == Status).ToList();

        }

        public List<vwGetCCRunHeaderBatch> BatchGridData(decimal ID, ref string QueryString)
        {
            using (clsLinqDataContext ctx = GetDataContext(UserName))
            {
                return BatchGridData(ctx, ID, ref QueryString);
            }
        }
        public List<vwGetCCRunHeaderBatch> BatchGridData(clsLinqDataContext ctx, decimal ID, ref string QueryString)
        {
            //QueryString = "Select * from vwGetCCRunHeaderBatches where Status = \'" + Status + "\' and CycleInventoryCountHeaderID = " + ID;
            //return ctx.vwGetCCRunHeaderBatches.Where(x => x.CycleInventoryCountHeaderID == ID && x.Status == Status).ToList();
            QueryString = "Select * from vwGetCCRunHeaderBatches where CycleInventoryCountHeaderID = " + ID;
            return ctx.vwGetCCRunHeaderBatches.Where(x => x.CycleInventoryCountHeaderID == ID).ToList();
        }




        public List<CCRunBatchesScanResult_01> GridDataBatchScanResult(decimal ID, string Status, string Summarize, string Levels, bool ShowDevices, bool ShowParts, ref string QueryString)
        {
            using (clsLinqDataContext ctx = GetDataContext(UserName))
            {
                return GridDataBatchScanResult(ctx, ID, Status, Summarize, Levels, ShowDevices, ShowParts, ref QueryString);
            }
        }
        public List<CCRunBatchesScanResult_01> GridDataBatchScanResult(clsLinqDataContext ctx, decimal ID, string Status, string Summarize, string Levels, bool ShowDevices, bool ShowParts, ref string QueryString)
        {
            //List<CCRunBatchesScanResult_01> d1 = null;
            char cSummarize = 'N';
            char cShowDevices = 'Y';
            char cShowParts = 'Y';
            //if (Summarize == true) { cSummarize = 'Y'; }
            cSummarize = Summarize.ToCharArray()[0];
            if (ShowDevices == false) { cShowDevices = 'N'; }
            if (ShowParts == false) { cShowParts = 'N'; }
            QueryString = "exec GetReport_CCRunBatchesScanResults_02 " + ID + ",\'" + Status + "\',\'" + cSummarize + "\',\'" + Levels + "\',\'" + cShowDevices + "\',\'" + cShowParts + "\'";
            //var d = ctx.GetReport_CCRunBatchesScanResults_01(ID, Status, cSummarize, cShowDevices, cShowParts);
            ////var xd1 = d.ToList();
            //d1 = d.ToList();
            return ctx.GetReport_CCRunBatchesScanResults_02(ID, Status, cSummarize, Levels, cShowDevices, cShowParts).ToList();
        }



        public List<GridCCRunBatchesControl> GridDataBatchControl(decimal ID, string Status, bool Summarize, bool ShowDevices, bool ShowParts, ref string QueryString)
        {
            using (clsLinqDataContext ctx = GetDataContext(UserName))
            {
                return GridDataBatchControl(ctx, ID, Status, Summarize, ShowDevices, ShowParts, ref QueryString);
            }
        }
        public List<GridCCRunBatchesControl> GridDataBatchControl(clsLinqDataContext ctx, decimal ID, string Status, bool Summarize, bool ShowDevices, bool ShowParts, ref string QueryString)
        {
            List<GridCCRunBatchesControl> d1 = null;
            char cSummarize = 'N';
            char cShowDevices = 'Y';
            char cShowParts = 'Y';
            if (Summarize == true) { cSummarize = 'Y'; }
            if (ShowDevices == false) { cShowDevices = 'N'; }
            if (ShowParts == false) { cShowParts = 'N'; }
            QueryString = "exec GetReport_CCRunBatchesControl " + ID + ",\'" + Status + "\',\'" + cSummarize + "\',\'" + cShowDevices + "\',\'" + cShowParts + "\'";
            var d = ctx.GetReport_CCRunBatchesControl(ID, Status, cSummarize, cShowDevices, cShowParts);
            var xd1 = d.ToList();
            d1 = xd1.ToList();
            return d1;
        }

        public List<GridCCRunBatchesControl> GridDataBatchScan(decimal ID, string Status, bool Summarize, bool ShowDevices, bool ShowParts, ref string QueryString)
        {
            using (clsLinqDataContext ctx = GetDataContext(UserName))
            {
                return GridDataBatchScan(ctx, ID, Status, Summarize, ShowDevices, ShowParts, ref QueryString);
            }
        }
        public List<GridCCRunBatchesControl> GridDataBatchScan(clsLinqDataContext ctx, decimal ID, string Status, bool Summarize, bool ShowDevices, bool ShowParts, ref string QueryString)
        {
            List<GridCCRunBatchesControl> d1 = null;
            char cSummarize = 'N';
            char cShowDevices = 'Y';
            char cShowParts = 'Y';
            if (Summarize == true) { cSummarize = 'Y'; }
            if (ShowDevices == false) { cShowDevices = 'N'; }
            if (ShowParts == false) { cShowParts = 'N'; }
            QueryString = "exec GetReport_CCRunBatchesScan " + ID + ",\'" + Status + "\',\'" + cSummarize + "\',\'" + cShowDevices + "\',\'" + cShowParts + "\'";
            var d = ctx.GetReport_CCRunBatchesScan(ID, Status, cSummarize, cShowDevices, cShowParts);
            var xd1 = d.ToList();
            d1 = xd1.ToList();
            return d1;
        }

        public decimal SetRunStatus(decimal CycleInventoryCountHeaderID, string Status, ref string Message)
        {
            using (clsLinqDataContext ctx = GetDataContext(UserName))
            {
                return SetRunStatus(ctx, CycleInventoryCountHeaderID, Status, ref Message);
            }
        }
        public decimal SetRunStatus(clsLinqDataContext ctx, decimal CycleInventoryCountHeaderID, string Status, ref string Message)
        {
            CycleInventoryCountHeader TH = ctx.CycleInventoryCountHeaders.FirstOrDefault(x => x.CycleInventoryCountHeaderID == CycleInventoryCountHeaderID);
            if (TH != null)
            {
                string FromStatus = TH.Status;
                TH.LastUpdateDate = DateTime.Now;
                TH.LastUpdateUser = UserName;
                TH.Status = Status;
                ctx.SubmitChanges();

                if (Status.ToUpper() == "ACTIVE" && FromStatus.ToUpper() == "NEW")
                {
                    GenerateControlData(ctx, CycleInventoryCountHeaderID);
                    LockCycleCountLocations(ctx, CycleInventoryCountHeaderID);
                }

                Message = "Status Changed from:" + FromStatus + " to:" + Status;
                return TH.CycleInventoryCountHeaderID;
            }
            else
            {
                Message = "Run not found";
                return -1;
            }
        }

        private void GenerateControlData(clsLinqDataContext ctx, decimal CycleInventoryCountHeaderID)
        {
            string Message = "";
            GenerateTemplateCycleControl(CycleInventoryCountHeaderID, ref Message);
        }
        public bool GenerateTemplateCycleControl(decimal CycleInventoryCountHeaderID, ref string Message)
        {
            using (clsLinqDataContext ctx = GetDataContext(UserName))
            {
                return GenerateTemplateCycleControl(ctx, CycleInventoryCountHeaderID, "Inactive", ref Message);
            }
        }
        public bool GenerateTemplateCycleControl(clsLinqDataContext ctx, decimal CycleInventoryCountHeaderID, string Status, ref string Message)
        {
            CycleInventoryCountHeader TH = ctx.CycleInventoryCountHeaders.FirstOrDefault(x => x.CycleInventoryCountHeaderID == CycleInventoryCountHeaderID);
            if (TH != null)
            {
                ctx.CC_GenerateBatchControl(CycleInventoryCountHeaderID, UserName, ref Message);
                Message = "Activate Run Cycle Control Generated:" + Message;
                return true;
            }
            else
            {
                Message = "Count Run not found";
                return false;
            }

        }
        private void LockCycleCountLocations(clsLinqDataContext ctx, decimal CycleInventoryCountHeaderID)
        {
            //foreach (CycleInventoryCountIterationHeader Batchloc in ctx.CycleInventoryCountIterationHeaders.Where(x => x.CycleInventoryCountHeaderID == CycleInventoryCountHeaderID))
            //{
            //    IFSLocation Loc = new IFSLocation(Batchloc.IFSLocationID);
            //    if (Loc.IsWhip == false)
            //    {
            //        Batchloc.isBatchLocked = true;
            //        Batchloc.LastUpdateDate = DateTime.Now;
            //        Batchloc.LastUpdateUser = UserName;
            //        Loc.Freeze(ctx, UserName);  // Freeze does a ctx.submitchanges.
            //    }
            //}
        }


        public decimal SetBatchStatus(decimal CycleInventoryCountIterationHeaderID, string Status, ref string Message)
        {
            using (clsLinqDataContext ctx = GetDataContext(UserName))
            {
                return SetBatchStatus(ctx, CycleInventoryCountIterationHeaderID, Status, ref Message);
            }
        }
        public decimal SetBatchStatus(clsLinqDataContext ctx, decimal CycleInventoryCountIterationHeaderID, string Status, ref string Message)
        {
            CycleInventoryCountIterationHeader TH = ctx.CycleInventoryCountIterationHeaders.FirstOrDefault(x => x.CycleInventoryCountIterationHeaderID == CycleInventoryCountIterationHeaderID);
            if (TH != null)
            {
                string FromStatus = TH.Status;
                TH.LastUpdateDate = DateTime.Now;
                TH.LastUpdateUser = UserName;
                TH.Status = Status;
                ctx.SubmitChanges();
                Message = "Status Changed from:" + FromStatus + " to:" + Status;

                return TH.CycleInventoryCountIterationHeaderID;
            }
            else
            {
                Message = "Batch not found";
                return -1;
            }
        }

        public bool CC_ActivateBatchControl(decimal CycleInventoryCountIterationHeaderID, ref string Message)
        {
            using (clsLinqDataContext ctx = GetDataContext(UserName))
            {
                return CC_ActivateBatchControl(ctx, CycleInventoryCountIterationHeaderID, ref Message);
            }
        }
        public bool CC_ActivateBatchControl(clsLinqDataContext ctx, decimal CycleInventoryCountIterationHeaderID, ref string Message)
        {
            CycleInventoryCountIterationHeader TH = ctx.CycleInventoryCountIterationHeaders.FirstOrDefault(x => x.CycleInventoryCountIterationHeaderID == CycleInventoryCountIterationHeaderID);
            if (TH != null)
            {
                ctx.CC_GenerateBatchControl(CycleInventoryCountIterationHeaderID, UserName, ref Message);
                Message = "Batch Activated:" + Message;
                return true;
            }
            else
            {
                Message = "Batch not found";
                return false;
            }

        }

        public decimal BatchCloneToNew(decimal CycleInventoryCountIterationHeaderID, ref string Message)
        {
            using (clsLinqDataContext ctx = GetDataContext(UserName))
            {
                return BatchCloneToNew(ctx, CycleInventoryCountIterationHeaderID, ref Message);
            }
        }
        public decimal BatchCloneToNew(clsLinqDataContext ctx, decimal CycleInventoryCountIterationHeaderID, ref string Message)
        {
            CycleInventoryCountIterationHeader TH = ctx.CycleInventoryCountIterationHeaders.FirstOrDefault(x => x.CycleInventoryCountIterationHeaderID == CycleInventoryCountIterationHeaderID);
            if (TH != null)
            {
                CycleInventoryCountIterationHeader THClone = new CycleInventoryCountIterationHeader();
                THClone.Batch = "";
                THClone.CreateDate = DateTime.Now;
                THClone.CreateUser = UserName;
                THClone.CycleInventoryCountHeaderID = TH.CycleInventoryCountHeaderID;
                THClone.IFSLocationID = TH.IFSLocationID;
                THClone.isBatchLocked = TH.isBatchLocked;
                THClone.Note = TH.Note;
                THClone.AuthorityType = "";
                THClone.Status = "New";
                THClone.LastUpdateDate = DateTime.Now;
                THClone.LastUpdateUser = UserName;


                TH.LastUpdateDate = DateTime.Now;
                TH.LastUpdateUser = UserName;
                ctx.CycleInventoryCountIterationHeaders.InsertOnSubmit(THClone);
                ctx.SubmitChanges();
                Message = "Batch cloned and set to new";

                return THClone.CycleInventoryCountIterationHeaderID;
            }
            else
            {
                Message = "Batch not found";
                return -1;
            }
        }



        //public List<vwGetCCRunHeader> RunGridData_New()
        //{
        //    using (clsLinqDataContext ctx = GetDataContext(UserName))
        //    {
        //        return RunGridData(ctx).Where(x => x.Status == "New").ToList();
        //    }
        //}
        //public List<vwGetCCRunHeader> RunGridData_Active()
        //{
        //    using (clsLinqDataContext ctx = GetDataContext(UserName))
        //    {
        //        return RunGridData(ctx).Where(x => x.Status == "Active" || x.Status == "Open").ToList();
        //    }
        //}
        //public List<vwGetCCRunHeader> RunGridData_Inactive()
        //{
        //    using (clsLinqDataContext ctx = GetDataContext(UserName))
        //    {
        //        return RunGridData(ctx).Where(x => x.Status == "InActive" || x.Status == "Deleted").ToList();
        //    }
        //}
        //public List<vwGetCCRunHeader> RunGridData_Closed()
        //{
        //    using (clsLinqDataContext ctx = GetDataContext(UserName))
        //    {
        //        return RunGridData(ctx).Where(x => x.Status == "Closed" || x.Status == "Closed").ToList();
        //    }
        //}

        //public CycleInventoryCountTemplateHeader GetTemplate(decimal TemplateID)
        //{
        //    using (clsLinqDataContext ctx = GetDataContext(UserName))
        //    {
        //        return GetTemplate(ctx, TemplateID);
        //    }
        //}
        //public CycleInventoryCountTemplateHeader GetTemplate(clsLinqDataContext ctx, decimal TemplateID)
        //{
        //    return ctx.CycleInventoryCountTemplateHeaders.FirstOrDefault(x => x.CycleInventoryCountTemplateHeaderID == TemplateID);
        //}
        public CycleInventoryCountTemplateHeader GetTemplate(string Name)
        {
            using (clsLinqDataContext ctx = GetDataContext(UserName))
            {
                return GetTemplate(ctx, Name);
            }
        }
        public CycleInventoryCountTemplateHeader GetTemplate(clsLinqDataContext ctx, string Name)
        {
            return ctx.CycleInventoryCountTemplateHeaders.FirstOrDefault(x => x.Name.ToUpper() == Name.ToUpper());
        }

        public decimal AddTemplate(decimal TemplateID, string Status, string Name, string Note, string IFSSite, string IFSLocation, string IFSCondition, string Carriers, string Manufacturers, string Models, string Colours, ref string Message)
        {
            using (clsLinqDataContext ctx = GetDataContext(UserName))
            {
                return AddTemplate(ctx, TemplateID, Status, Name, Note, IFSSite, IFSLocation, IFSCondition, Carriers, Manufacturers, Models, Colours, ref Message);
            }
        }
        public decimal AddTemplate(clsLinqDataContext ctx, decimal TemplateID, string Status, string Name, string Note, string IFSSite, string IFSLocation, string IFSCondition, string Carriers, string Manufacturers, string Models, string Colours, ref string Message)
        {
            //Message = "xxxxxxxxxxxxxxxx";
            if (Name.Trim().Length == 0) { Message = "Name Required."; return -1; }
            CycleInventoryCountTemplateHeader TH = ctx.CycleInventoryCountTemplateHeaders.FirstOrDefault(x => x.Name.ToUpper() == Name.ToUpper());
            if (TH != null)
            {
                TH.LastUpdateDate = DateTime.Now;
                TH.LastUpdateUser = UserName;
                if (Note.Trim().Length > 0) { TH.Note = Note; }
                TH.Status = Status;
                TH.Carriers = Carriers;
                TH.Colours = Colours;
                TH.IFSCondition = IFSCondition;
                TH.IFSLocation = IFSLocation;
                TH.IFSSite = IFSSite;
                TH.Manufacturers = Manufacturers;
                TH.Models = Models;
                //AddTemplateLocations(TH, LocationData, ref Message);
                ctx.SubmitChanges();
                Message = "Template Updated. " + Message;
                return TH.CycleInventoryCountTemplateHeaderID;
            }
            else
            {
                TH = new CycleInventoryCountTemplateHeader();
                TH.CreateDate = DateTime.Now;
                TH.CreateUser = UserName;
                TH.LastUpdateDate = DateTime.Now;
                TH.LastUpdateUser = UserName;
                TH.Name = Name;
                TH.Note = Note;
                TH.Carriers = Carriers;
                TH.Colours = Colours;
                TH.IFSCondition = IFSCondition;
                TH.IFSLocation = IFSLocation;
                TH.IFSSite = IFSSite;
                TH.Manufacturers = Manufacturers;
                TH.Models = Models;
                TH.Status = Status;
                //AddTemplateLocations(TH, LocationData, ref Message);
                ctx.CycleInventoryCountTemplateHeaders.InsertOnSubmit(TH);
                ctx.SubmitChanges();
                Message = "Template Created. " + Message;
                return TH.CycleInventoryCountTemplateHeaderID;
            }
        }


        public bool GenerateTemplateCycle(decimal CycleInventoryCountTemplateHeaderID, ref string Message)
        {
            using (clsLinqDataContext ctx = GetDataContext(UserName))
            {
                return GenerateTemplateCycle(ctx, CycleInventoryCountTemplateHeaderID, "Inactive", ref Message);
            }
        }
        public bool GenerateTemplateCycle(clsLinqDataContext ctx, decimal CycleInventoryCountTemplateHeaderID, string Status, ref string Message)
        {
            CycleInventoryCountTemplateHeader TH = ctx.CycleInventoryCountTemplateHeaders.FirstOrDefault(x => x.CycleInventoryCountTemplateHeaderID == CycleInventoryCountTemplateHeaderID);
            if (TH != null)
            {
                ctx.CC_GenerateCycle(CycleInventoryCountTemplateHeaderID, UserName, ref Message);
                Message = "Run Cycle Generated:" + Message;
                return true;
            }
            else
            {
                Message = "Template not found";
                return false;
            }

        }





        public decimal SetTemplateStatus_Active(decimal CycleInventoryCountTemplateHeaderID, ref string Message)
        {
            using (clsLinqDataContext ctx = GetDataContext(UserName))
            {
                return SetTemplateStatus(ctx, CycleInventoryCountTemplateHeaderID, "Active", ref Message);
            }
        }
        public decimal SetTemplateStatus_Inactive(decimal CycleInventoryCountTemplateHeaderID, ref string Message)
        {
            using (clsLinqDataContext ctx = GetDataContext(UserName))
            {
                return SetTemplateStatus(ctx, CycleInventoryCountTemplateHeaderID, "Inactive", ref Message);
            }
        }
        public decimal SetTemplateStatus(clsLinqDataContext ctx, decimal CycleInventoryCountTemplateHeaderID, string Status, ref string Message)
        {
            CycleInventoryCountTemplateHeader TH = ctx.CycleInventoryCountTemplateHeaders.FirstOrDefault(x => x.CycleInventoryCountTemplateHeaderID == CycleInventoryCountTemplateHeaderID);
            if (TH != null)
            {
                TH.LastUpdateDate = DateTime.Now;
                TH.LastUpdateUser = UserName;
                TH.Status = Status;
                ctx.SubmitChanges();
                Message = "Status Changed";
                return TH.CycleInventoryCountTemplateHeaderID;
            }
            else
            {
                Message = "Template not found";
                return -1;
            }

        }

        //public decimal AddTemplateLocations(decimal TemplateID, string Status, string Name, string Note, List<IFSLocation> LocationData, ref string Message)
        //{
        //    using (clsLinqDataContext ctx = GetDataContext(UserName))
        //    {
        //        return AddTemplateLocations(ctx, TemplateID, Status, Name, Note, LocationData, ref Message);
        //    }
        //}
        //public decimal AddTemplateLocations(clsLinqDataContext ctx, decimal TemplateID, string Status, string Name, string Note, List<IFSLocation> LocationData, ref string Message)
        //{

        //    if (Name.Trim().Length == 0) { Message = "Name Required."; return -1; }
        //    CycleInventoryCountTemplateHeader TH = ctx.CycleInventoryCountTemplateHeaders.FirstOrDefault(x => x.Name.ToUpper() == Name.ToUpper());
        //    if (TH != null)
        //    {
        //        TH.LastUpdateDate = DateTime.Now;
        //        TH.LastUpdateUser = UserName;
        //        if (Note.Trim().Length > 0) { TH.Note = Note; }
        //        TH.Status = Status;
        //        AddTemplateLocations(TH, LocationData, ref Message);
        //        ctx.SubmitChanges();
        //        return TH.CycleInventoryCountTemplateHeaderID;
        //    }
        //    else
        //    {
        //        Message = "Template not found";
        //        return -1;
        //    }


        //}
        //void AddTemplateLocations(CycleInventoryCountTemplateHeader TH, List<IFSLocation> LocationData, ref string Message)
        //{
        //    int Count = 0;
        //    foreach (IFSLocation L in LocationData.Where(x=> x.isValid == true))
        //    {
        //        if (TH.CycleInventoryCountTemplateHeaderDetails.Any(x => x.IFSLocationID == L.ID) == false)
        //        {
        //            Count++;
        //            CycleInventoryCountTemplateHeaderDetail D = new CycleInventoryCountTemplateHeaderDetail();
        //            D.CreateDate = DateTime.Now;
        //            D.CreateUser = UserName;
        //            D.IFSLocationID = L.ID;
        //            D.LastUpdateDate = DateTime.Now;
        //            D.LastUpdateUser = UserName;
        //            TH.CycleInventoryCountTemplateHeaderDetails.Add(D);
        //        }
        //    }
        //    Message = "Locations Added:" + Count.ToString();

        //}

        public decimal DeleteTemplateLocations(decimal TemplateID, string Status, string Name, string Note, List<IFSLocation> LocationData, ref string Message)
        {
            using (clsLinqDataContext ctx = GetDataContext(UserName))
            {
                return DeleteTemplateLocations(ctx, TemplateID, Status, Name, Note, LocationData, ref Message);
            }
        }
        public decimal DeleteTemplateLocations(clsLinqDataContext ctx, decimal TemplateID, string Status, string Name, string Note, List<IFSLocation> LocationData, ref string Message)
        {

            if (Name.Trim().Length == 0) { Message = "Name Required."; return -1; }
            CycleInventoryCountTemplateHeader TH = ctx.CycleInventoryCountTemplateHeaders.FirstOrDefault(x => x.Name.ToUpper() == Name.ToUpper());
            if (TH != null)
            {
                TH.LastUpdateDate = DateTime.Now;
                TH.LastUpdateUser = UserName;
                if (Note.Trim().Length > 0) { TH.Note = Note; }
                TH.Status = Status;
                DeleteTemplateLocations(ctx, TH, LocationData, ref Message);
                ctx.SubmitChanges();
                return TH.CycleInventoryCountTemplateHeaderID;
            }
            else
            {
                Message = "Template not found";
                return -1;
            }


        }
        void DeleteTemplateLocations(clsLinqDataContext ctx, CycleInventoryCountTemplateHeader TH, List<IFSLocation> LocationData, ref string Message)
        {
            int Count = 0;
            //List<CycleInventoryCountTemplateHeaderDetail> DelList = new List<CycleInventoryCountTemplateHeaderDetail>();
            foreach (IFSLocation L in LocationData.Where(x => x.isValid == true))
            {
                CycleInventoryCountTemplateHeaderDetail D = TH.CycleInventoryCountTemplateHeaderDetails.FirstOrDefault(x => x.IFSLocationID == L.ID);
                if (D != null)
                {
                    Count++;
                    ctx.CycleInventoryCountTemplateHeaderDetails.DeleteOnSubmit(D);
                    //DelList.Add (D);
                }
            }
            Message = "Locations Deleted:" + Count.ToString();
        }
        #endregion

    }


    public class CycleCountPartManager : DataManagers
    {

        //string UserName = string.Empty;
        public string[] UserRoles { get; set; }
        public BL_UserDataRestrictor UserRestrict = null;

        //public clsLinqDataContext GetDataContext(string UserName)
        //{
        //    clsLinqDataContext ctx = new clsLinqDataContext();
        //    ctx.UserName = UserName;
        //    return ctx;
        //}
        public CycleCountPartManager(string Username)
        {
            UserName = Username;
            UserRoles = Roles.GetRolesForUser(UserName);
            if (UserName.Trim().Length == 0)
            {
                UserName = "NullUser";
            }
            UserRestrict = new BL_UserDataRestrictor(UserName);
        }
        //public String UserName
        //{
        //    get { return UserName; }
        //    set { UserName = value; }
        //}
        //public string UserRoleString
        //{
        //    get
        //    {
        //        string[] selectedusersRoles = Roles.GetRolesForUser(UserName);
        //        string sRoles = "";
        //        foreach (string r in selectedusersRoles)
        //        {
        //            sRoles += r + ",";
        //        }
        //        return sRoles;
        //    }
        //}


        public CycleInventoryCountTemplateHeaderPart GetTemplate(decimal ID)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                return GetTemplate(ctx, ID);
            }
        }
        public CycleInventoryCountTemplateHeaderPart GetTemplate(clsLinqDataContext ctx, decimal ID)
        {
            return ctx.CycleInventoryCountTemplateHeaderParts.FirstOrDefault(x => x.CycleInventoryCountTemplateHeaderPartsID == ID);
        }
        public string LogCCBatchLocked(string Batch)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                return LogCCBatchLocked(ctx, Batch);
            }
        }
        public string LogCCBatchLocked(clsLinqDataContext ctx, string Batch)
        {
            decimal count = 0;
            var data = ctx.CycleCountInventoryCountParts.Where(x => x.Batch == Batch && x.Status != "Invalid");
            foreach (CycleCountInventoryCountPart r in data)
            {
                r.isBatchLocked = true;
                r.Status = "Active";
                count++;
            }
            ctx.SubmitChanges();
            return "Batch Records Locked:" + count.ToString();
        }
        public List<vwGridCycleInventoryCountParts_B> GetCycleCountData(decimal CycleInventoryCountHeaderPartsID, string Type, ref string QuerryString)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                return GetCycleCountData(ctx, CycleInventoryCountHeaderPartsID, Type, ref QuerryString);
            }
        }
        public List<vwGridCycleInventoryCountParts_B> GetCycleCountData(clsLinqDataContext ctx, decimal CycleInventoryCountHeaderPartsID, string Type, ref string QuerryString)
        {
            QuerryString = "Select * from vwGridCycleInventoryCountParts_B where Status = \'" + Type + "\' and CycleInventoryCountHeaderPartsID = " + CycleInventoryCountHeaderPartsID.ToString();
            return ctx.vwGridCycleInventoryCountParts_Bs.Where(x => x.Status == Type && x.CycleInventoryCountHeaderPartsID == CycleInventoryCountHeaderPartsID).OrderBy(x => x.Batch).OrderByDescending(x => x.Batch).ToList();
        }

        public string LogCycleCountInventoryCount(string Batch, decimal Quantity, string ESN, bool isDevice, decimal ClientLocationID, string UserName)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                return LogCycleCountInventoryCount(ctx, Batch, Quantity, ESN, isDevice, ClientLocationID, UserName);
            }
        }
        public string LogCycleCountInventoryCount(clsLinqDataContext ctx, string Batch, decimal Quantity, string ESN, bool isDevice, decimal ClientLocationID, string UserName)
        {
            string rMessage = "";
            ctx.CycleCountPhysicalScanAdd(Batch, Quantity, ESN, isDevice, ClientLocationID, UserName, ref rMessage);
            return rMessage;
        }

        #region Batch Utilities


        public string LogPhysicalInventoryBatchLocked(decimal CycleInventoryCountHeaderPartsID, string Batch)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                return LogPhysicalInventoryBatchLocked(ctx, CycleInventoryCountHeaderPartsID, Batch);
            }
        }
        public string LogPhysicalInventoryBatchLocked(clsLinqDataContext ctx, decimal CycleInventoryCountHeaderPartsID, string Batch)
        {
            decimal count = 0;
            var data = ctx.CycleCountInventoryCountParts.Where(x => x.Batch == Batch && x.Status != "Invalid");
            foreach (CycleCountInventoryCountPart r in data)
            {
                r.isBatchLocked = true;
                count++;
            }
            ctx.SubmitChanges();
            return "Batch Records Locked:" + count.ToString();
        }
        public string LogPhysicalInventoryBatchClean(decimal CycleInventoryCountHeaderPartsID, string Batch)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                return LogPhysicalInventoryBatchClean(ctx, CycleInventoryCountHeaderPartsID, Batch);
            }
        }
        public string LogPhysicalInventoryBatchClean(clsLinqDataContext ctx, decimal CycleInventoryCountHeaderPartsID, string Batch)
        {
            string rMessage = "";
            //var data = ctx.Update_ReceiveDetailPhysicalCycleCountScanClean(Batch, ref rMessage);             -- JIM
            ctx.SubmitChanges();
            return rMessage;
        }
        public string LogPhysicalInventoryBatchInvalid(decimal CycleInventoryCountHeaderPartsID, string Batch)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                return LogPhysicalInventoryBatchInvalid(ctx, CycleInventoryCountHeaderPartsID, Batch);
            }
        }
        public string LogPhysicalInventoryBatchInvalid(clsLinqDataContext ctx, decimal CycleInventoryCountHeaderPartsID, string Batch)
        {
            decimal count = 0;
            var data = ctx.CycleCountInventoryCountParts.Where(x => x.Batch == Batch);
            foreach (CycleCountInventoryCountPart r in data)
            {
                r.Status = "Invalid";
                r.isBatchLocked = false;
                count++;
            }
            ctx.SubmitChanges();
            return "Batch Records Marked Invalid:" + count.ToString();
        }
        public string LogPhysicalInventoryBatchHold(decimal CycleInventoryCountHeaderPartsID, string Batch)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                return LogPhysicalInventoryBatchHold(ctx, CycleInventoryCountHeaderPartsID, Batch);
            }
        }
        public string LogPhysicalInventoryBatchHold(clsLinqDataContext ctx, decimal CycleInventoryCountHeaderPartsID, string Batch)
        {
            decimal count = 0;
            var data = ctx.CycleCountInventoryCountParts.Where(x => x.Batch == Batch);
            foreach (CycleCountInventoryCountPart r in data)
            {
                r.Status = "Hold";
                count++;
            }
            ctx.SubmitChanges();
            return "Batch Records Marked Hold:" + count.ToString();
        }
        public string LogPhysicalInventoryBatchOpen(decimal CycleInventoryCountHeaderPartsID, string Batch)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                return LogPhysicalInventoryBatchOpen(ctx, CycleInventoryCountHeaderPartsID, Batch);
            }
        }
        public string LogPhysicalInventoryBatchOpen(clsLinqDataContext ctx, decimal CycleInventoryCountHeaderPartsID, string Batch)
        {
            decimal count = 0;
            var data = ctx.CycleCountInventoryCountParts.Where(x => x.Batch == Batch);
            foreach (CycleCountInventoryCountPart r in data)
            {
                r.Status = "Open";
                r.isBatchLocked = false;
                count++;
            }
            ctx.SubmitChanges();
            return "Batch Records Marked Open:" + count.ToString();
        }
        public string LogPhysicalInventoryBatchToSyncReady(decimal CycleInventoryCountHeaderPartsID, string Batch)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                return LogPhysicalInventoryBatchToSyncReady(ctx, CycleInventoryCountHeaderPartsID, Batch);
            }
        }
        public string LogPhysicalInventoryBatchToSyncReady(clsLinqDataContext ctx, decimal CycleInventoryCountHeaderPartsID, string Batch)
        {
            string rMessage = "";
            ctx.IFS_GenerateInvtTran_PIDevice(Batch, UserName, ref rMessage);
            var data = ctx.CycleCountInventoryCountParts.Where(x => x.Batch == Batch);
            foreach (CycleCountInventoryCountPart r in data)
            {
                r.Status = "SYNC Ready";
                r.isBatchLocked = true;
            }
            ctx.SubmitChanges();
            return rMessage;
        }
        public string LogPhysicalInventoryBatchToClosed(decimal CycleInventoryCountHeaderPartsID, string Batch)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                return LogPhysicalInventoryBatchToClosed(ctx, CycleInventoryCountHeaderPartsID, Batch);
            }
        }
        public string LogPhysicalInventoryBatchToClosed(clsLinqDataContext ctx, decimal CycleInventoryCountHeaderPartsID, string Batch)
        {
            string rMessage = "";
            ctx.IFS_GenerateInvtTran_PIDevice(Batch, UserName, ref rMessage);
            var data = ctx.CycleCountInventoryCountParts.Where(x => x.Batch == Batch);
            foreach (CycleCountInventoryCountPart r in data)
            {
                r.Status = "Closed";
                r.isBatchLocked = true;
            }
            ctx.SubmitChanges();
            return rMessage;
        }
        #endregion


        #region Template
        public List<vwGetCCTemplateHeaderPart> TemplateGridData_Active(ref string QueryString)
        {
            using (clsLinqDataContext ctx = GetDataContext(UserName))
            {
                return TemplateGridData(ctx, "Active", ref QueryString);
            }
        }
        public List<vwGetCCTemplateHeaderPart> TemplateGridData_Inactive(ref string QueryString)
        {
            using (clsLinqDataContext ctx = GetDataContext(UserName))
            {
                return TemplateGridData(ctx, "Inactive", ref QueryString);
            }
        }
        public List<vwGetCCTemplateHeaderPart> TemplateGridData(string Status, ref string QueryString)
        {
            using (clsLinqDataContext ctx = GetDataContext(UserName))
            {
                return TemplateGridData(ctx, Status, ref QueryString);
            }
        }
        public List<vwGetCCTemplateHeaderPart> TemplateGridData(clsLinqDataContext ctx, string Status, ref string QueryString)
        {
            QueryString = "Select * from vwGetCCTemplateHeaderParts where Status = \'" + Status + "\'";
            return ctx.vwGetCCTemplateHeaderParts.Where(x => x.Status == Status).ToList();

        }

        public List<vwGetCCRunHeaderPart> GridDataRun(string Status, ref string QueryString)
        {
            using (clsLinqDataContext ctx = GetDataContext(UserName))
            {
                return GridDataRun(ctx, Status, ref QueryString);
            }
        }
        public List<vwGetCCRunHeaderPart> GridDataRun(clsLinqDataContext ctx, string Status, ref string QueryString)
        {
            QueryString = "Select * from vwGetCCRunHeaderParts where Status = \'" + Status + "\'";
            return ctx.vwGetCCRunHeaderParts.Where(x => x.Status == Status).ToList();

        }

        public List<vwGetCCRunHeaderBatchesPart> BatchGridData(decimal ID, ref string QueryString)
        {
            using (clsLinqDataContext ctx = GetDataContext(UserName))
            {
                return BatchGridData(ctx, ID, ref QueryString);
            }
        }
        public List<vwGetCCRunHeaderBatchesPart> BatchGridData(clsLinqDataContext ctx, decimal ID, ref string QueryString)
        {
            QueryString = "Select * from vwGetCCRunHeaderBatchesParts where CycleInventoryCountHeaderPartsID = " + ID;
            return ctx.vwGetCCRunHeaderBatchesParts.Where(x => x.CycleInventoryCountHeaderPartsID == ID).ToList();
        }

        public List<CCRunBatchesScanResult_01> GridDataBatchScanResult(decimal ID, string Status, string Summarize, string Levels, bool ShowDevices, bool ShowParts, ref string QueryString)
        {
            using (clsLinqDataContext ctx = GetDataContext(UserName))
            {
                return GridDataBatchScanResult(ctx, ID, Status, Summarize, Levels, ShowDevices, ShowParts, ref QueryString);
            }
        }
        public List<CCRunBatchesScanResult_01> GridDataBatchScanResult(clsLinqDataContext ctx, decimal ID, string Status, string Summarize, string Levels, bool ShowDevices, bool ShowParts, ref string QueryString)
        {
            char cSummarize = 'N';
            char cShowDevices = 'Y';
            char cShowParts = 'Y';
            cSummarize = Summarize.ToCharArray()[0];
            if (ShowDevices == false) { cShowDevices = 'N'; }
            if (ShowParts == false) { cShowParts = 'N'; }
            QueryString = "exec GetReport_CCRunBatchesScanResults_02 " + ID + ",\'" + Status + "\',\'" + cSummarize + "\',\'" + Levels + "\',\'" + cShowDevices + "\',\'" + cShowParts + "\'";
            return ctx.GetReport_CCRunBatchesScanResults_02(ID, Status, cSummarize, Levels, cShowDevices, cShowParts).ToList();
        }

        public List<GridCCRunBatchesControlPart> GridDataBatchControl(decimal ID, string Status, bool Summarize, bool ShowDevices, bool ShowParts, ref string QueryString)
        {
            using (clsLinqDataContext ctx = GetDataContext(UserName))
            {
                return GridDataBatchControl(ctx, ID, Status, Summarize, ShowDevices, ShowParts, ref QueryString);
            }
        }
        public List<GridCCRunBatchesControlPart> GridDataBatchControl(clsLinqDataContext ctx, decimal ID, string Status, bool Summarize, bool ShowDevices, bool ShowParts, ref string QueryString)
        {
            List<GridCCRunBatchesControlPart> d1 = null;
            char cSummarize = 'N';
            char cShowDevices = 'Y';
            char cShowParts = 'Y';
            if (Summarize == true) { cSummarize = 'Y'; }
            if (ShowDevices == false) { cShowDevices = 'N'; }
            if (ShowParts == false) { cShowParts = 'N'; }
            QueryString = "exec GetReport_CCRunBatchesControlPart " + ID + ",\'" + Status + "\',\'" + cSummarize + "\',\'" + cShowDevices + "\',\'" + cShowParts + "\'";
            var d = ctx.GetReport_CCRunBatchesControlParts(ID, Status, cSummarize, cShowDevices, cShowParts);
            var xd1 = d.ToList();
            d1 = xd1.ToList();
            return d1;
        }

        public List<GridCCRunBatchesControlPart> GridDataBatchScan(decimal ID, string Status, bool Summarize, bool ShowDevices, bool ShowParts, ref string QueryString)
        {
            using (clsLinqDataContext ctx = GetDataContext(UserName))
            {
                return GridDataBatchScan(ctx, ID, Status, Summarize, ShowDevices, ShowParts, ref QueryString);
            }
        }
        public List<GridCCRunBatchesControlPart> GridDataBatchScan(clsLinqDataContext ctx, decimal ID, string Status, bool Summarize, bool ShowDevices, bool ShowParts, ref string QueryString)
        {
            List<GridCCRunBatchesControlPart> d1 = null;
            char cSummarize = 'N';
            char cShowDevices = 'Y';
            char cShowParts = 'Y';
            if (Summarize == true) { cSummarize = 'Y'; }
            if (ShowDevices == false) { cShowDevices = 'N'; }
            if (ShowParts == false) { cShowParts = 'N'; }
            QueryString = "exec GetReport_CCRunBatchesScanPart " + ID + ",\'" + Status + "\',\'" + cSummarize + "\',\'" + cShowDevices + "\',\'" + cShowParts + "\'";
            var d = ctx.GetReport_CCRunBatchesScanParts(ID, Status, cSummarize, cShowDevices, cShowParts);
            var xd1 = d.ToList();
            d1 = xd1.ToList();
            return d1;
        }

        public decimal SetRunStatus(decimal CycleInventoryCountHeaderPartsID, string Status, ref string Message)
        {
            using (clsLinqDataContext ctx = GetDataContext(UserName))
            {
                return SetRunStatus(ctx, CycleInventoryCountHeaderPartsID, Status, ref Message);
            }
        }
        public decimal SetRunStatus(clsLinqDataContext ctx, decimal CycleInventoryCountHeaderPartsID, string Status, ref string Message)
        {
            CycleInventoryCountHeaderPart TH = ctx.CycleInventoryCountHeaderParts.FirstOrDefault(x => x.CycleInventoryCountHeaderPartsID == CycleInventoryCountHeaderPartsID);
            if (TH != null)
            {
                string FromStatus = TH.Status;
                TH.LastUpdateDate = DateTime.Now;
                TH.LastUpdateUser = UserName;
                TH.Status = Status;
                ctx.SubmitChanges();

                if (Status.ToUpper() == "ACTIVE" && FromStatus.ToUpper() == "NEW")
                {
                    GenerateControlData(ctx, CycleInventoryCountHeaderPartsID);
                    //LockCycleCountLocations(ctx, CycleInventoryCountHeaderPartsID);
                }

                Message = "Status Changed from:" + FromStatus + " to:" + Status;
                return TH.CycleInventoryCountHeaderPartsID;
            }
            else
            {
                Message = "Run not found";
                return -1;
            }
        }

        private void GenerateControlData(clsLinqDataContext ctx, decimal CycleInventoryCountHeaderPartsID)
        {
            string Message = "";
            GenerateTemplateCycleControl(CycleInventoryCountHeaderPartsID, ref Message);
        }
        public bool GenerateTemplateCycleControl(decimal CycleInventoryCountHeaderPartsID, ref string Message)
        {
            using (clsLinqDataContext ctx = GetDataContext(UserName))
            {
                return GenerateTemplateCycleControl(ctx, CycleInventoryCountHeaderPartsID, "Inactive", ref Message);
            }
        }
        public bool GenerateTemplateCycleControl(clsLinqDataContext ctx, decimal CycleInventoryCountHeaderPartsID, string Status, ref string Message)
        {
            CycleInventoryCountHeaderPart TH = ctx.CycleInventoryCountHeaderParts.FirstOrDefault(x => x.CycleInventoryCountHeaderPartsID == CycleInventoryCountHeaderPartsID);
            if (TH != null)
            {
                ctx.CC_GenerateBatchControlParts(CycleInventoryCountHeaderPartsID, UserName, ref Message);
                Message = "Activate Run Cycle Control Generated:" + Message;
                return true;
            }
            else
            {
                Message = "Count Run not found";
                return false;
            }

        }
        //private void LockCycleCountLocations(clsLinqDataContext ctx, decimal CycleInventoryCountHeaderPartsID)
        //{
        //    //foreach (CycleInventoryCountIterationHeader Batchloc in ctx.CycleInventoryCountIterationHeaders.Where(x => x.CycleInventoryCountHeaderPartsID == CycleInventoryCountHeaderPartsID))
        //    //{
        //    //    IFSLocation Loc = new IFSLocation(Batchloc.IFSLocationID);
        //    //    if (Loc.IsWhip == false)
        //    //    {
        //    //        Batchloc.isBatchLocked = true;
        //    //        Batchloc.LastUpdateDate = DateTime.Now;
        //    //        Batchloc.LastUpdateUser = UserName;
        //    //        Loc.Freeze(ctx, UserName);  // Freeze does a ctx.submitchanges.
        //    //    }
        //    //}
        //}

        public decimal SetBatchStatus(decimal CycleInventoryCountIterationHeaderID, string Status, ref string Message)
        {
            using (clsLinqDataContext ctx = GetDataContext(UserName))
            {
                return SetBatchStatus(ctx, CycleInventoryCountIterationHeaderID, Status, ref Message);
            }
        }
        public decimal SetBatchStatus(clsLinqDataContext ctx, decimal CycleInventoryCountIterationHeaderPartsID, string Status, ref string Message)
        {
            CycleInventoryCountIterationHeaderPart TH = ctx.CycleInventoryCountIterationHeaderParts.FirstOrDefault(x => x.CycleInventoryCountIterationHeaderPartsID == CycleInventoryCountIterationHeaderPartsID);
            if (TH != null)
            {
                string FromStatus = TH.Status;
                TH.LastUpdateDate = DateTime.Now;
                TH.LastUpdateUser = UserName;
                TH.Status = Status;
                ctx.SubmitChanges();
                Message = "Status Changed from:" + FromStatus + " to:" + Status;

                return TH.CycleInventoryCountIterationHeaderPartsID;
            }
            else
            {
                Message = "Batch not found";
                return -1;
            }
        }

        public bool CC_ActivateBatchControl(decimal CycleInventoryCountIterationHeaderPartsID, ref string Message)
        {
            using (clsLinqDataContext ctx = GetDataContext(UserName))
            {
                return CC_ActivateBatchControl(ctx, CycleInventoryCountIterationHeaderPartsID, ref Message);
            }
        }
        public bool CC_ActivateBatchControl(clsLinqDataContext ctx, decimal CycleInventoryCountIterationHeaderPartsID, ref string Message)
        {
            CycleInventoryCountIterationHeaderPart TH = ctx.CycleInventoryCountIterationHeaderParts.FirstOrDefault(x => x.CycleInventoryCountIterationHeaderPartsID == CycleInventoryCountIterationHeaderPartsID);
            if (TH != null)
            {
                ctx.CC_GenerateBatchControl(CycleInventoryCountIterationHeaderPartsID, UserName, ref Message);
                Message = "Batch Activated:" + Message;
                return true;
            }
            else
            {
                Message = "Batch not found";
                return false;
            }

        }

        public decimal BatchCloneToNew(decimal CycleInventoryCountIterationHeaderPartsID, ref string Message)
        {
            using (clsLinqDataContext ctx = GetDataContext(UserName))
            {
                return BatchCloneToNew(ctx, CycleInventoryCountIterationHeaderPartsID, ref Message);
            }
        }
        public decimal BatchCloneToNew(clsLinqDataContext ctx, decimal CycleInventoryCountIterationHeaderPartsID, ref string Message)
        {
            CycleInventoryCountIterationHeaderPart TH = ctx.CycleInventoryCountIterationHeaderParts.FirstOrDefault(x => x.CycleInventoryCountIterationHeaderPartsID == CycleInventoryCountIterationHeaderPartsID);
            if (TH != null)
            {
                CycleInventoryCountIterationHeaderPart THClone = new CycleInventoryCountIterationHeaderPart();
                THClone.Batch = "";
                THClone.CreateDate = DateTime.Now;
                THClone.CreateUser = UserName;
                THClone.CycleInventoryCountHeaderPartsID = TH.CycleInventoryCountHeaderPartsID;
                THClone.IFSLocationID = TH.IFSLocationID;
                THClone.isBatchLocked = TH.isBatchLocked;
                THClone.Note = TH.Note;
                THClone.AuthorityType = "";
                THClone.Status = "New";
                THClone.LastUpdateDate = DateTime.Now;
                THClone.LastUpdateUser = UserName;


                TH.LastUpdateDate = DateTime.Now;
                TH.LastUpdateUser = UserName;
                ctx.CycleInventoryCountIterationHeaderParts.InsertOnSubmit(THClone);
                ctx.SubmitChanges();
                Message = "Batch cloned and set to new";

                return THClone.CycleInventoryCountIterationHeaderPartsID;
            }
            else
            {
                Message = "Batch not found";
                return -1;
            }
        }

        public CycleInventoryCountTemplateHeaderPart GetTemplate(string Name)
        {
            using (clsLinqDataContext ctx = GetDataContext(UserName))
            {
                return GetTemplate(ctx, Name);
            }
        }
        public CycleInventoryCountTemplateHeaderPart GetTemplate(clsLinqDataContext ctx, string Name)
        {
            return ctx.CycleInventoryCountTemplateHeaderParts.FirstOrDefault(x => x.Name.ToUpper() == Name.ToUpper());
        }

        public decimal AddTemplate(decimal TemplateID, string Status, string Name, string Note, decimal ClientlocationID, string IFSSite, string IFSLocation, string PartList, ref string Message)
        {
            using (clsLinqDataContext ctx = GetDataContext(UserName))
            {
                return AddTemplate(ctx, TemplateID, Status, Name, Note, ClientlocationID, IFSSite, IFSLocation, PartList, ref Message);
            }
        }
        public decimal AddTemplate(clsLinqDataContext ctx, decimal TemplateID, string Status, string Name, string Note, decimal ClientlocationID, string IFSSite, string IFSLocation, string PartList, ref string Message)
        {
            //Message = "xxxxxxxxxxxxxxxx";
            if (Name.Trim().Length == 0) { Message = "Name Required."; return -1; }
            CycleInventoryCountTemplateHeaderPart TH = ctx.CycleInventoryCountTemplateHeaderParts.FirstOrDefault(x => x.Name.ToUpper() == Name.ToUpper());
            if (TH != null)
            {
                TH.LastUpdateDate = DateTime.Now;
                TH.LastUpdateUser = UserName;
                if (Note.Trim().Length > 0) { TH.Note = Note; }
                TH.Status = Status;
                TH.PartNumber = PartList;
                TH.ClientLocationID = ClientlocationID;
                TH.IFSLocation = IFSLocation;
                TH.IFSSite = IFSSite;
                //AddTemplateLocations(TH, LocationData, ref Message);
                ctx.SubmitChanges();
                Message = "Template Updated. " + Message;
                return TH.CycleInventoryCountTemplateHeaderPartsID;
            }
            else
            {
                TH = new CycleInventoryCountTemplateHeaderPart();
                TH.CreateDate = DateTime.Now;
                TH.CreateUser = UserName;
                TH.LastUpdateDate = DateTime.Now;
                TH.LastUpdateUser = UserName;
                TH.Name = Name;
                TH.Note = Note;
                TH.IFSLocation = IFSLocation;
                TH.ClientLocationID = ClientlocationID;
                TH.IFSSite = IFSSite;
                TH.PartNumber = PartList;
                TH.Status = Status;
                //AddTemplateLocations(TH, LocationData, ref Message);
                ctx.CycleInventoryCountTemplateHeaderParts.InsertOnSubmit(TH);
                ctx.SubmitChanges();
                Message = "Template Created. " + Message;
                return TH.CycleInventoryCountTemplateHeaderPartsID;
            }
        }


        public bool GenerateTemplateCycle(decimal CycleInventoryCountTemplateHeaderPartsID, ref string Message)
        {
            using (clsLinqDataContext ctx = GetDataContext(UserName))
            {
                return GenerateTemplateCycle(ctx, CycleInventoryCountTemplateHeaderPartsID, "Inactive", ref Message);
            }
        }
        public bool GenerateTemplateCycle(clsLinqDataContext ctx, decimal CycleInventoryCountTemplateHeaderPartsID, string Status, ref string Message)
        {
            CycleInventoryCountTemplateHeaderPart TH = ctx.CycleInventoryCountTemplateHeaderParts.FirstOrDefault(x => x.CycleInventoryCountTemplateHeaderPartsID == CycleInventoryCountTemplateHeaderPartsID);
            if (TH != null)
            {
                ctx.CC_GenerateCycleParts(CycleInventoryCountTemplateHeaderPartsID, UserName, ref Message);
                Message = "Run Part Cycle Generated:" + Message;
                return true;
            }
            else
            {
                Message = "Template not found";
                return false;
            }

        }

        public decimal SetTemplateStatus_Active(decimal CycleInventoryCountTemplateHeaderPartsID, ref string Message)
        {
            using (clsLinqDataContext ctx = GetDataContext(UserName))
            {
                return SetTemplateStatus(ctx, CycleInventoryCountTemplateHeaderPartsID, "Active", ref Message);
            }
        }
        public decimal SetTemplateStatus_Inactive(decimal CycleInventoryCountTemplateHeaderPartsID, ref string Message)
        {
            using (clsLinqDataContext ctx = GetDataContext(UserName))
            {
                return SetTemplateStatus(ctx, CycleInventoryCountTemplateHeaderPartsID, "Inactive", ref Message);
            }
        }
        public decimal SetTemplateStatus(clsLinqDataContext ctx, decimal CycleInventoryCountTemplateHeaderPartsID, string Status, ref string Message)
        {
            CycleInventoryCountTemplateHeaderPart TH = ctx.CycleInventoryCountTemplateHeaderParts.FirstOrDefault(x => x.CycleInventoryCountTemplateHeaderPartsID == CycleInventoryCountTemplateHeaderPartsID);
            if (TH != null)
            {
                TH.LastUpdateDate = DateTime.Now;
                TH.LastUpdateUser = UserName;
                TH.Status = Status;
                ctx.SubmitChanges();
                Message = "Status Changed";
                return TH.CycleInventoryCountTemplateHeaderPartsID;
            }
            else
            {
                Message = "Template not found";
                return -1;
            }

        }


        //public decimal DeleteTemplateLocations(decimal TemplateID, string Status, string Name, string Note, List<IFSLocation> LocationData, ref string Message)
        //{
        //    using (clsLinqDataContext ctx = GetDataContext(UserName))
        //    {
        //        return DeleteTemplateLocations(ctx, TemplateID, Status, Name, Note, LocationData, ref Message);
        //    }
        //}
        //public decimal DeleteTemplateLocations(clsLinqDataContext ctx, decimal TemplateID, string Status, string Name, string Note, List<IFSLocation> LocationData, ref string Message)
        //{

        //    if (Name.Trim().Length == 0) { Message = "Name Required."; return -1; }
        //    CycleInventoryCountTemplateHeaderPart TH = ctx.CycleInventoryCountTemplateHeaderParts.FirstOrDefault(x => x.Name.ToUpper() == Name.ToUpper());
        //    if (TH != null)
        //    {
        //        TH.LastUpdateDate = DateTime.Now;
        //        TH.LastUpdateUser = UserName;
        //        if (Note.Trim().Length > 0) { TH.Note = Note; }
        //        TH.Status = Status;
        //        DeleteTemplateLocations(ctx, TH, LocationData, ref Message);
        //        ctx.SubmitChanges();
        //        return TH.CycleInventoryCountTemplateHeaderPartsID;
        //    }
        //    else
        //    {
        //        Message = "Template not found";
        //        return -1;
        //    }


        //}
        //void DeleteTemplateLocations(clsLinqDataContext ctx, CycleInventoryCountTemplateHeaderPart TH, List<IFSLocation> LocationData, ref string Message)
        //{
        //    int Count = 0;
        //    foreach (IFSLocation L in LocationData.Where(x => x.isValid == true))
        //    {
        //        CycleInventoryCountTemplateHeaderPartsDetail D = TH.CycleInventoryCountTemplateHeaderDetails.FirstOrDefault(x => x.IFSLocationID == L.ID);
        //        if (D != null)
        //        {
        //            Count++;
        //            ctx.CycleInventoryCountTemplateHeaderPartsDetails.DeleteOnSubmit(D);
        //            //DelList.Add (D);
        //        }
        //    }
        //    Message = "Locations Deleted:" + Count.ToString();
        //}
        #endregion

    }

}