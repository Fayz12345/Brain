using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BW_WebApp.DataManagers
{
    public class TimeLogManager
    {
        string _IPAddress = string.Empty;
        string _UserName = string.Empty;
        bool _IsTimerON = false;
        DateTime StartDate = DateTime.Now;
        DateTime EndDate = DateTime.Now;
        decimal logID = -1;
        public decimal LogID
        {
            get { return logID; }
            //set { logID = value; }
        }
        public String UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        public String IPAddress
        {
            get { return _IPAddress; }
            set { _IPAddress = value; }
        }
        public TimeLogManager(string username, string ipAddress)
        {
            UserName = username;
            IPAddress = ipAddress;
        }
        public void StartTimer()
        {
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            _IsTimerON = true;
        }
        public void StopTimer()
        {
            EndDate = DateTime.Now;
            _IsTimerON = false;
        }
        public decimal TimeInMilliSeconds()
        {
            TimeSpan span = new TimeSpan();
            if (_IsTimerON == false) { span = EndDate - StartDate; }
            if (_IsTimerON == true) { span = DateTime.Now - StartDate; }
            return (decimal)span.TotalMilliseconds;
        }
        public void RecordBrowserTimetoLog(decimal LogID, decimal MilliSeconds)
        {
            if (System.Configuration.ConfigurationManager.AppSettings["WriteWorkScreenSaveTimeLog"].ToUpper() == "TRUE")
            {
                using (clsLinqDataContext ctx = GetDataContext(UserName))
                {
                    SystemTimeLog sl = ctx.SystemTimeLogs.FirstOrDefault(x => x.SystemTimeLogID == LogID);
                    if (sl != null)
                    {
                        // Update the BrowserTime
                        sl.SaveTimeBrowserMS = MilliSeconds;
                        ctx.SubmitChanges();
                        logID = sl.SystemTimeLogID;
                    }
                    return;
                }
            }
        }
        public void SaveTimeLogRoleList(decimal ReceiveDetailID, decimal ProcessID, string SaveData)
        {
            if (System.Configuration.ConfigurationManager.AppSettings["WriteLogRoleListTimeLog"].ToUpper() == "TRUE")
            {
                using (clsLinqDataContext ctx = GetDataContext(UserName))
                {
                    SystemTimeLog sl = new SystemTimeLog();
                    sl.StartTimeDate = StartDate;
                    sl.EndTimeDate = EndDate;
                    sl.CreateIPAddress = IPAddress;
                    sl.RecordType = "RoleList";

                    sl.CreateUser = UserName;
                    sl.LastUpdateUser = UserName;
                    sl.LastUpdateDate = DateTime.Now;
                    sl.ProcessID = ProcessID;
                    sl.ReceiveDetailID = ReceiveDetailID;
                    sl.RecordDetailString = SaveData;
                    sl.SaveTimeMS = TimeInMilliSeconds();
                    ctx.SystemTimeLogs.InsertOnSubmit(sl);
                    ctx.SubmitChanges();
                    logID = sl.SystemTimeLogID;
                    return;
                }
            }
        }
        public void SaveTimeLogWorkScreenLoad(decimal ReceiveDetailID, decimal ProcessID, string SaveData)
        {
            if (System.Configuration.ConfigurationManager.AppSettings["WriteWorkScreenSaveTimeLog"].ToUpper() == "TRUE")
            {
                using (clsLinqDataContext ctx = GetDataContext(UserName))
                {
                    SystemTimeLog sl = new SystemTimeLog();
                    sl.StartTimeDate = StartDate;
                    sl.EndTimeDate = EndDate;
                    sl.CreateIPAddress = IPAddress;
                    sl.RecordType = "WorkScreenLoad";

                    sl.CreateUser = UserName;
                    sl.LastUpdateUser = UserName;
                    sl.LastUpdateDate = DateTime.Now;
                    sl.ProcessID = ProcessID;
                    sl.ReceiveDetailID = ReceiveDetailID;
                    sl.RecordDetailString = SaveData;
                    sl.SaveTimeMS = TimeInMilliSeconds();
                    ctx.SystemTimeLogs.InsertOnSubmit(sl);
                    ctx.SubmitChanges();
                    logID = sl.SystemTimeLogID;
                    return;
                }
            }
        }
        public void SaveTimeLogWorkScreen(decimal ReceiveDetailID, decimal ProcessID, string SaveData)
        {
            if (System.Configuration.ConfigurationManager.AppSettings["WriteWorkScreenSaveTimeLog"].ToUpper() == "TRUE")
            {
                using (clsLinqDataContext ctx = GetDataContext(UserName))
                {
                    SystemTimeLog sl = new SystemTimeLog();
                    sl.StartTimeDate = StartDate;
                    sl.EndTimeDate = EndDate;
                    sl.CreateIPAddress = IPAddress;
                    sl.RecordType = "WorkScreenSave";

                    sl.CreateUser = UserName;
                    sl.LastUpdateUser = UserName;
                    sl.LastUpdateDate = DateTime.Now;
                    sl.ProcessID = ProcessID;
                    sl.ReceiveDetailID = ReceiveDetailID;
                    sl.RecordDetailString = SaveData;
                    sl.SaveTimeMS = TimeInMilliSeconds();
                    ctx.SystemTimeLogs.InsertOnSubmit(sl);
                    ctx.SubmitChanges();
                    logID = sl.SystemTimeLogID;
                    return;
                }
            }
        }
        public void SaveTimeLogWorkScreen_PreProcess(decimal ReceiveDetailID, string RecordType, decimal ProcessID, string SaveData)
        {
            if (System.Configuration.ConfigurationManager.AppSettings["WritePreWorkScreenSaveTimeLog"].ToUpper() == "TRUE")
            {
                using (clsLinqDataContext ctx = GetDataContext(UserName))
                {
                    SystemTimeLog sl = new SystemTimeLog();
                    sl.StartTimeDate = StartDate;
                    sl.EndTimeDate = EndDate;
                    sl.CreateIPAddress = IPAddress;
                    sl.RecordType = RecordType;

                    sl.CreateUser = UserName;
                    sl.LastUpdateUser = UserName;
                    sl.LastUpdateDate = DateTime.Now;
                    sl.ProcessID = ProcessID;
                    sl.ReceiveDetailID = ReceiveDetailID;
                    sl.RecordDetailString = SaveData;
                    sl.SaveTimeMS = TimeInMilliSeconds();
                    ctx.SystemTimeLogs.InsertOnSubmit(sl);
                    ctx.SubmitChanges();
                    logID = sl.SystemTimeLogID;
                    return;
                }
            }
        }
        public void SaveTimeLogSalesOrder(decimal OrderHeaderID, string RecordType, decimal OrderDetailID, string SaveData)
        {
            if (System.Configuration.ConfigurationManager.AppSettings["WriteSalesOrderSaveTimeLog"].ToUpper() == "TRUE")
            {
                using (clsLinqDataContext ctx = GetDataContext(UserName))
                {
                    SystemTimeLog sl = new SystemTimeLog();
                    sl.StartTimeDate = StartDate;
                    sl.EndTimeDate = EndDate;
                    sl.CreateIPAddress = IPAddress;
                    sl.RecordType = RecordType;

                    sl.CreateUser = UserName;
                    sl.LastUpdateUser = UserName;
                    sl.LastUpdateDate = DateTime.Now;
                    sl.ProcessID = OrderDetailID;
                    sl.ReceiveDetailID = OrderHeaderID;
                    sl.RecordDetailString = SaveData;
                    sl.SaveTimeMS = TimeInMilliSeconds();
                    ctx.SystemTimeLogs.InsertOnSubmit(sl);
                    ctx.SubmitChanges();
                    logID = sl.SystemTimeLogID;
                    return;
                }
            }
        }
        public void LogIt(string SaveData)
        {
            SaveTimeLogSalesOrder(-1, "Logit", -1, SaveData);
        }
        //public void LogIt(decimal OrderHeaderID, string RecordType, decimal OrderDetailID, string SaveData)
        //{
        //    if (System.Configuration.ConfigurationManager.AppSettings["WriteSalesOrderSaveTimeLog"].ToUpper() == "TRUE")
        //    {
        //        using (clsLinqDataContext ctx = GetDataContext(UserName))
        //        {
        //            SystemTimeLog sl = new SystemTimeLog();
        //            sl.StartTimeDate = StartDate;
        //            sl.EndTimeDate = EndDate;
        //            sl.CreateIPAddress = IPAddress;
        //            sl.RecordType = RecordType;

        //            sl.CreateUser = UserName;
        //            sl.LastUpdateUser = UserName;
        //            sl.LastUpdateDate = DateTime.Now;
        //            sl.ProcessID = OrderDetailID;
        //            sl.ReceiveDetailID = OrderHeaderID;
        //            sl.RecordDetailString = SaveData;
        //            sl.SaveTimeMS = TimeInMilliSeconds();
        //            ctx.SystemTimeLogs.InsertOnSubmit(sl);
        //            ctx.SubmitChanges();
        //            logID = sl.SystemTimeLogID;
        //            return;
        //        }
        //    }
        //}

        public void SaveTimeLogAssignPartsScreen(decimal MasterPartsRequestedLogID, decimal ReceiveDetailID, string SaveData)
        {
            if (System.Configuration.ConfigurationManager.AppSettings["WriteAssignPartTimeLog"].ToUpper() == "TRUE")
            {
                using (clsLinqDataContext ctx = GetDataContext(UserName))
                {
                    SystemTimeLog sl = new SystemTimeLog();
                    sl.StartTimeDate = StartDate;
                    sl.EndTimeDate = EndDate;
                    sl.CreateIPAddress = IPAddress;
                    sl.RecordType = "AssignPartNumber";

                    sl.CreateUser = UserName;
                    sl.LastUpdateUser = UserName;
                    sl.LastUpdateDate = DateTime.Now;
                    sl.MasterPartsRequestedLogID = MasterPartsRequestedLogID;
                    sl.ReceiveDetailID = ReceiveDetailID;
                    sl.RecordDetailString = SaveData;
                    sl.SaveTimeMS = TimeInMilliSeconds();
                    ctx.SystemTimeLogs.InsertOnSubmit(sl);
                    ctx.SubmitChanges();
                    logID = sl.SystemTimeLogID;
                    return;
                }
            }
        }

        public clsLinqDataContext GetDataContext(string UserName)
        {
            clsLinqDataContext ctx = new clsLinqDataContext();
            ctx.UserName = UserName;
            return ctx;
        }
        public clsLinqDataContext GetDataContext()
        {
            return GetDataContext(_UserName);
        }
    }

}