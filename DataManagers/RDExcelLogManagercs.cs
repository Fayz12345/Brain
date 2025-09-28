using System;
//using System.Diagnostics;


//using GMPDemo;
//using BusinessLayer;
//using Factory_DataModel;

namespace BW_WebApp.DataManagers
{
    public class RDExcelLogManagercs
    {
        string _IPAddress = string.Empty;
        string _UserName = string.Empty;
        string _BatchNumber = string.Empty;
        bool _IsTimerON = false;
        DateTime _StartDate = DateTime.Now;
        DateTime _EndDate = DateTime.Now;
        DateTime _LastEndDate = DateTime.Now;


        public String UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        public String Batch
        {
            get { return _BatchNumber; }
            set { _BatchNumber = value; }
        }
        public String IPAddress
        {
            get { return _IPAddress; }
            set { _IPAddress = value; }
        }
        public DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        public DateTime EndDate
        {
            get
            {
                if (_IsTimerON == true) { _EndDate = DateTime.Now; }
                _LastEndDate = _EndDate;
                return _EndDate;
            }
            set { _EndDate = value; _LastEndDate = _EndDate; }
        }

        public RDExcelLogManagercs(string batch, string username, string ipAddress)
        {
            UserName = username;
            IPAddress = ipAddress;
            Batch = batch;
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            _IsTimerON = false;
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
        public decimal TimeInMilliSecondsSinceLastSend()
        {
            TimeSpan span = new TimeSpan();
            if (_IsTimerON == false) { span = EndDate - _LastEndDate; }
            if (_IsTimerON == true) { span = DateTime.Now - _LastEndDate; }
            return (decimal)span.TotalMilliseconds;
        }

        public void LogEntry(string Status, string Type, string ESN, decimal ReceiveDetailID, string SourceData, string ResultData, bool ShowTimeSinceLastLogEntry)
        {
            LogEntry(Batch, Status, Type, ESN, ReceiveDetailID, SourceData, ResultData, ShowTimeSinceLastLogEntry);
        }

        public void LogEntry(string key, string Status, string Type, string ESN, decimal ReceiveDetailID, string SourceData, string ResultData, bool ShowTimeSinceLastLogEntry)
        {
            if (System.Configuration.ConfigurationManager.AppSettings["WriteIMEIExcelUploadLog"].ToUpper() == "TRUE")
            {
                using (clsLinqDataContext ctx = GetDataContext(UserName))
                {
                    decimal timeseconds = TimeInMilliSeconds();
                    if (ShowTimeSinceLastLogEntry == true)
                    {
                        timeseconds = TimeInMilliSecondsSinceLastSend();
                    }
                    ReceiveDetailExcelUploadLog sl = new ReceiveDetailExcelUploadLog();
                    sl.StartTimeDate = StartDate;
                    sl.EndTimeDate = EndDate;
                    sl.CreateIPAddress = IPAddress;
                    sl.RecordType = Type;
                    if (key.Length > 100) { key = key.Substring(key.Length - 100, 100); }
                    sl.KeyIdentifier = key;
                    sl.ESN = ESN;
                    sl.STATUS = Status;
                    sl.CreateUser = UserName;
                    sl.ReceiveDetailID = ReceiveDetailID;
                    sl.RecordDetailString = SourceData;
                    sl.Message = ResultData;
                    sl.SaveTimeMS = timeseconds;


                    ctx.ReceiveDetailExcelUploadLogs.InsertOnSubmit(sl);
                    ctx.SubmitChanges();
                    return;
                }
            }
        }
        //public void SaveTimeLogWorkScreen_PreProcess(decimal ReceiveDetailID, string RecordType, decimal ProcessID, string SaveData)
        //{
        //    if (System.Configuration.ConfigurationManager.AppSettings["WritePreWorkScreenSaveTimeLog"].ToUpper() == "TRUE")
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
        //            sl.ProcessID = ProcessID;
        //            sl.ReceiveDetailID = ReceiveDetailID;
        //            sl.RecordDetailString = SaveData;
        //            sl.SaveTimeMS = TimeInMilliSeconds();
        //            ctx.SystemTimeLogs.InsertOnSubmit(sl);
        //            ctx.SubmitChanges();
        //            return;
        //        }
        //    }
        //}
        //public void SaveTimeLogAssignPartsScreen(decimal MasterPartsRequestedLogID, decimal ReceiveDetailID, string SaveData)
        //{
        //    if (System.Configuration.ConfigurationManager.AppSettings["WriteAssignPartTimeLog"].ToUpper() == "TRUE")
        //    {
        //        using (clsLinqDataContext ctx = GetDataContext(UserName))
        //        {
        //            SystemTimeLog sl = new SystemTimeLog();
        //            sl.StartTimeDate = StartDate;
        //            sl.EndTimeDate = EndDate;
        //            sl.CreateIPAddress = IPAddress;
        //            sl.RecordType = "AssignPartNumber";

        //            sl.CreateUser = UserName;
        //            sl.LastUpdateUser = UserName;
        //            sl.LastUpdateDate = DateTime.Now;
        //            sl.MasterPartsRequestedLogID = MasterPartsRequestedLogID;
        //            sl.ReceiveDetailID = ReceiveDetailID;
        //            sl.RecordDetailString = SaveData;
        //            sl.SaveTimeMS = TimeInMilliSeconds();
        //            ctx.SystemTimeLogs.InsertOnSubmit(sl);
        //            ctx.SubmitChanges();
        //            return;
        //        }
        //    }
        //}


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