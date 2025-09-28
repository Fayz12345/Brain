using System;
using System.Collections.Generic;
using System.Linq;
using BW_WebApp.DataManagers;


namespace BW_WebApp.Classes
{
    //[WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    #region API Out Classes
    [Serializable]
    public class TestClass
    {
        public string firstname = "";
        public string lastname = "";
        public int age = 0;

        public TestClass(string fName, string lName, int nAge)
        {
            firstname = fName;
            lastname = lName;
            age = nAge;
        }
        public TestClass()
        {
            firstname = "newf";
            lastname = "newl";
            age = 10;
        }

        public string JSON()
        {
            return ExtendedMethods.JsonSerializer(this);
        }

        public static TestClass FromJSON(string _JSONString)
        {
            return ExtendedMethods.JsonDeserialize<TestClass>(_JSONString);
        }
    }
    //           ///////////////////////////////////////////////////////////////



    [Serializable]
    public class API_UploadDeviceBatch_OutB : API_UploadDeviceBatch_Out
    {
        private string LogPath = "";

        public API_UploadDeviceBatch_OutB(API_UploadDeviceBatch_In Data, string lPath)
            : base(Data)
        {
            LogPath = lPath;
            RecordLogPath();
        }
        public API_UploadDeviceBatch_OutB(string _batch, string _client, string _project, string _username, string lPath)
            : base(_batch, _client, _project, _username)
        {
            LogPath = lPath;
        }
        public API_UploadDeviceBatch_OutB(string _batch, string _client, string _project, string _username, List<API_Device_Out> _devices, string lPath)
            : base(_batch, _client, _project, _username, _devices)
        {
            LogPath = lPath;
            RecordLogPath();
        }

        public API_UploadDeviceBatch_OutB(string lPath)
            : base()
        {
            LogPath = lPath;
            RecordLogPath();
        }

        private void RecordLogPath()
        {
            SystemDataManager sData = new SystemDataManager("jmccomb");
            sData.SetValue("APILog", LogPath);
        }
        public override string JSON()
        {
            API_UploadDeviceBatch_Out dta = new API_UploadDeviceBatch_Out(this);
            return ExtendedMethods.JsonSerializer(dta);
        }
        //public void DoLog(string Message)
        //{
        //    clsLog Plog;
        //    Plog = new clsLog(LogPath, "APIUpload_02_Log.txt", username, System.Configuration.ConfigurationManager.AppSettings["WriteLogUserNameToLog"]);
        //    if (System.Configuration.ConfigurationManager.AppSettings["WriteLog"].ToUpper() == "TRUE")
        //    {
        //        Plog.writeLogData = true;
        //    }
        //    Plog.LogMessageToFile(Message);
        //}

    }


    [Serializable]
    public class API_UploadDeviceBatch_Out
    {
        // [JsonIgnore]
        // private clsLog Plog;
        // [JsonIgnore]
        // private string PLogPath = "";
        // [JsonIgnore]
        public string TypeRec = "InsertDEVICE";
        public string batch = "";
        public string client = "";
        public string username = "";
        public string project = "";
        //public string user = "";
        public string process = "";
        public decimal logID = -1;
        public decimal ReceiveDetail_APIInsertBatchID = -1;
        public API_Status status = new API_Status();
        public List<API_Device_Out> devices = new List<API_Device_Out>();


        public void DoLog(string Message)
        {
            clsLog Plog;
            SystemDataManager sData = new SystemDataManager("jmccomb");
            string LogPath = sData.GetValue("APILog");
            Plog = new clsLog(LogPath, "APIUpload_02_Log.txt", username, System.Configuration.ConfigurationManager.AppSettings["WriteLogUserNameToLog"]);
            if (System.Configuration.ConfigurationManager.AppSettings["WriteLog"].ToUpper() == "TRUE")
            {
                Plog.writeLogData = true;
            }
            Plog.LogMessageToFile(Message);
        }

        public API_UploadDeviceBatch_Out(API_UploadDeviceBatch_In Data)
        {
            //PLogPath = lPath;
            //Plog = new clsLog(PLogPath, "APIUpload_02_Log.txt", username, System.Configuration.ConfigurationManager.AppSettings["WriteLogUserNameToLog"]);
            //if (System.Configuration.ConfigurationManager.AppSettings["WriteLog"].ToUpper() == "TRUE")
            //{
            //    Plog.writeLogData = true;
            //}
            //LogPath = lPath;
            batch = Data.batch;
            client = Data.client;
            username = Data.username;
            project = Data.project;
            process = Data.process;
            status.SetNew("New");
            if (process.Length >= 7 && process.Substring(0, 7).ToUpper() == "RECEIVE") { TypeRec = "RECEIVE"; }
            else if (process.Length >= 7 && process.Substring(0, 7).ToUpper() == "SHIPPING") { TypeRec = "SHIPPING"; }
            else { TypeRec = "UPDATE"; }

            status = isValidProcess();

            foreach (API_Device_In d in Data.devices)
            {
                devices.Add(new API_Device_Out(d.esn, d.version, d.attributes));
                //devices = new API_Device_Out(Data.devices);
            }
            logID = -1;
            ReceiveDetail_APIInsertBatchID = -1;
        }

        public API_UploadDeviceBatch_Out(API_UploadDeviceBatch_OutB Data)
        {
            //PLogPath = lPath;
            //Plog = new clsLog(PLogPath, "APIUpload_02_Log.txt", username, System.Configuration.ConfigurationManager.AppSettings["WriteLogUserNameToLog"]);
            //if (System.Configuration.ConfigurationManager.AppSettings["WriteLog"].ToUpper() == "TRUE")
            //{
            //    Plog.writeLogData = true;
            //}
            //LogPath = lPath;
            batch = Data.batch;
            client = Data.client;
            username = Data.username;
            project = Data.project;
            process = Data.process;
            status = Data.status;
            TypeRec = Data.TypeRec;
            foreach (API_Device_Out d in Data.devices)
            {
                devices.Add(d);
                //devices.Add(new API_Device_Out(d.esn, d.version, d.attributes));
                //devices = new API_Device_Out(Data.devices);
            }
            logID = Data.logID;
            ReceiveDetail_APIInsertBatchID = Data.ReceiveDetail_APIInsertBatchID;
        }
        public API_UploadDeviceBatch_Out(string _batch, string _client, string _project, string _username)
        {
            // PLogPath = lPath;
            // Plog = new clsLog(PLogPath, "APIUpload_02_Log.txt", username, System.Configuration.ConfigurationManager.AppSettings["WriteLogUserNameToLog"]);
            //if (System.Configuration.ConfigurationManager.AppSettings["WriteLog"].ToUpper() == "TRUE")
            //{
            //    Plog.writeLogData = true;
            //}
            //LogPath = lPath;
            batch = _batch;
            client = _client;
            username = _username;
            project = _project;
            logID = -1;
            ReceiveDetail_APIInsertBatchID = -1;
            status.SetNew("New");
        }
        public API_UploadDeviceBatch_Out(string _batch, string _client, string _project, string _username, List<API_Device_Out> _devices)
        {
            //PLogPath = lPath;
            //Plog = new clsLog(PLogPath, "APIUpload_02_Log.txt", username, System.Configuration.ConfigurationManager.AppSettings["WriteLogUserNameToLog"]);
            //if (System.Configuration.ConfigurationManager.AppSettings["WriteLog"].ToUpper() == "TRUE")
            //{
            //    Plog.writeLogData = true;
            //}
            //LogPath = lPath;
            batch = _batch;
            client = _client;
            username = _username;
            project = _project;
            logID = -1;
            ReceiveDetail_APIInsertBatchID = -1;
            devices = _devices;
            status.SetNew("New");
        }
        //public API_UploadDeviceBatch_Out(string lPath)
        //{
        //    LogPath = lPath;
        //    batch = "";
        //    client = "";
        //    username = "";
        //    project = "";
        //    logID = -1;
        //    ReceiveDetail_APIInsertBatchID = -1;
        //    status.status = "New";
        //    status.message = "";
        //}
        public API_UploadDeviceBatch_Out()
        {
            //LogPath = "";
            batch = "";
            client = "";
            username = "";
            project = "";
            logID = -1;
            ReceiveDetail_APIInsertBatchID = -1;
            status.status = "New";
            status.message = "";
        }

        public static API_UploadDeviceBatch_Out FromJSON(string _JSONString)
        {
            return ExtendedMethods.JsonDeserialize<API_UploadDeviceBatch_Out>(_JSONString);
        }
        public void UpdateReceiveDetailID(string esn, decimal receivedetailid)
        {
            API_Device_Out d = devices.FirstOrDefault(x => x.esn == esn);
            if (d != null)
            {
                d.receivedetailid = receivedetailid;
            }
        }
        public void WriteLogStart(string JSONStringIn)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                ReceiveDetail_APIInsertLog log = new ReceiveDetail_APIInsertLog();
                log.CreateUser = username;
                log.JSONin = JSONStringIn;
                ctx.ReceiveDetail_APIInsertLogs.InsertOnSubmit(log);
                ctx.SubmitChanges();
                logID = log.ReceiveDetail_APIInsertLogID;
                status.SetNew("Log Start");
                DoLog("Started batch:" + batch + " Path:");
            }
        }

        public void WriteProcessLogFirstReply(string Reply)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                ReceiveDetail_APIInsertLog log = ctx.ReceiveDetail_APIInsertLogs.FirstOrDefault(x => x.ReceiveDetail_APIInsertLogID == logID);
                if (log == null) { return; }
                //log.DateTimeout = DateTime.Now;
                log.JSONFirstReply = Reply;
                //status.SetNew("Log Start Done");
                //ctx.ReceiveDetail_APIInsertLogs.InsertOnSubmit(log);
                ctx.SubmitChanges();
                //logID = log.ReceiveDetail_APIInsertLogID;
            }
        }
        public void WriteProcessLogEnd()
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                ReceiveDetail_APIInsertLog log = ctx.ReceiveDetail_APIInsertLogs.FirstOrDefault(x => x.ReceiveDetail_APIInsertLogID == logID);
                if (log == null) { return; }
                log.DateTimeout = DateTime.Now;
                log.JSONout = JSON();
                status.SetNew("Log Start Done");
                //ctx.ReceiveDetail_APIInsertLogs.InsertOnSubmit(log);
                ctx.SubmitChanges();
                DoLog("   WriteProcessLogEnd Finished");
                //logID = log.ReceiveDetail_APIInsertLogID;
            }
        }
        public void WriteInsert()
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                DoLog("WriteInsert");
                ReceiveDetail_APIInsertLog log = ctx.ReceiveDetail_APIInsertLogs.FirstOrDefault(x => x.ReceiveDetail_APIInsertLogID == logID);
                if (log == null) { return; }
                if (ReceiveDetail_APIInsertBatchID != null && ReceiveDetail_APIInsertBatchID != -1) { return; }
                status.SetPending("Waiting for process");
                ReceiveDetail_APIInsertBatch dbBatch = new DataManagers.ReceiveDetail_APIInsertBatch();
                dbBatch.TypeRec = TypeRec;
                dbBatch.batch = batch;
                dbBatch.client = client;
                dbBatch.CreateDate = DateTime.Now;
                dbBatch.project = project;
                dbBatch.process = process;
                dbBatch.username = username;
                dbBatch.Status = status.status;
                dbBatch.Message = status.message;
                //b.ReceiveDetail_APIInsertBatchID = ReceiveDetail_APIInsertBatchID;
                foreach (API_Device_Out d in devices)
                {
                    d.status.SetPending("Waiting for process");
                    d.status = isValidDevice(d.esn, d.version);
                    ReceiveDetail_APIInsertDevice dbDevice = new ReceiveDetail_APIInsertDevice();
                    dbDevice.ESN = d.esn;
                    dbDevice.Version = d.version;
                    dbDevice.Message = d.status.message;
                    dbDevice.Status = d.status.status;
                    dbDevice.ReceiveDetailID = d.receivedetailid;
                    foreach (API_DeviceAttribute_Out da in d.attributes)
                    {
                        da.status.SetPending("Waiting for process");
                        da.status = isValidAttribute(da.attribute, da.value);
                        ReceiveDetail_APIInsertDeviceAttribute dbAttribute = new ReceiveDetail_APIInsertDeviceAttribute();
                        dbAttribute.attribute = da.attribute;
                        dbAttribute.Message = da.status.message;
                        dbAttribute.status = da.status.status;
                        dbAttribute.value = da.value;
                        dbDevice.ReceiveDetail_APIInsertDeviceAttributes.Add(dbAttribute);
                    }
                    dbBatch.ReceiveDetail_APIInsertDevices.Add(dbDevice);
                }
                log.ReceiveDetail_APIInsertBatches.Add(dbBatch);
                status.SetNew("Batch Added");
                log.JSONFirstReply = JSON();
                //log.DateTimeout = DateTime.Now;
                //log.JSONout = JSON();
                ////ctx.ReceiveDetail_APIInsertLogs.InsertOnSubmit(log);
                ctx.SubmitChanges();
                //logID = log.ReceiveDetail_APIInsertLogID;
            }
        }
        public void Read(string _batch)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                ReceiveDetail_APIInsertBatch log = ctx.ReceiveDetail_APIInsertBatches.FirstOrDefault(x => x.batch == _batch);
                if (log == null) { return; }
                Read(log.ReceiveDetail_APIInsertID ?? -1);
            }
            return;
        }
        public void Read(decimal _logID)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                DoLog("Read LogID:" + _logID.ToString());
                //Plog.LogIt("Read Started");
                ReceiveDetail_APIInsertLog log = ctx.ReceiveDetail_APIInsertLogs.FirstOrDefault(x => x.ReceiveDetail_APIInsertLogID == _logID);
                //if (log == null)
                //{
                //    Plog.LogIt("Insert LogID:" + _logID.ToString() + " not found"); return;
                //}
                //if (log.ReceiveDetail_APIInsertBatches == null) { return; }
                ReceiveDetail_APIInsertBatch b = log.ReceiveDetail_APIInsertBatches.FirstOrDefault(x => x.ReceiveDetail_APIInsertID == _logID);
                //if (b == null) { Plog.LogIt("Insert LogBatch for LogID:" + _logID.ToString() + " not found"); return; }
                batch = b.batch;
                client = b.client;
                project = b.project;
                process = b.process;
                username = b.username;
                TypeRec = b.TypeRec;
                ReceiveDetail_APIInsertBatchID = b.ReceiveDetail_APIInsertBatchID;
                status.message = b.Message;
                status.status = b.Status;
                logID = b.ReceiveDetail_APIInsertID ?? -1;
                //Plog.LogIt("  Header Read");
                DoLog("Read HeaderDone LogID:" + _logID.ToString());
                foreach (ReceiveDetail_APIInsertDevice d in b.ReceiveDetail_APIInsertDevices)
                {
                    API_Device_Out xx = new API_Device_Out();
                    xx.esn = d.ESN;
                    xx.version = d.Version;
                    xx.receivedetailid = d.ReceiveDetailID ?? -1;
                    xx.status.status = d.Status;
                    xx.status.message = d.Message;
                    xx.ReceiveDetail_APIInsertDeviceID = d.ReceiveDetail_APIInsertDeviceID;
                    DoLog("   Read Device:" + d.ESN);
                    //Plog.LogIt("  Device:" + xx.esn + " Read");
                    foreach (ReceiveDetail_APIInsertDeviceAttribute da in d.ReceiveDetail_APIInsertDeviceAttributes)
                    {
                        API_DeviceAttribute_Out a = new API_DeviceAttribute_Out();
                        a.attribute = da.attribute;
                        a.status.message = da.Message;
                        a.status.status = da.status;
                        a.value = da.value;
                        a.ReceiveDetail_APIInsertDeviceAttributeID = da.ReceiveDetail_APIInsertDeviceAttributeID;
                        xx.attributes.Add(a);
                        DoLog("      Read Attribute:" + da.attribute + " V:" + da.value);
                        //Plog.LogIt("  Attribute:" + a.attribute + " Read");
                    }
                    devices.Add(xx);
                }
                DoLog("   Read Device DONE");
            }

            //using (clsLinqDataContext ctx = new clsLinqDataContext())
            //{
            //    ReceiveDetail_APIInsertLog log = ctx.ReceiveDetail_APIInsertLogs.FirstOrDefault(x => x.ReceiveDetail_APIInsertLogID == logID);
            //    if (log == null) { return; }
            //    log.DateTimeout = DateTime.Now;
            //    log.JSONout = JSON();
            //    //ctx.ReceiveDetail_APIInsertLogs.InsertOnSubmit(log);
            //    ctx.SubmitChanges();
            //    //logID = log.ReceiveDetail_APIInsertLogID;
            //}
        }
        public void Save()
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                ReceiveDetail_APIInsertLog log = ctx.ReceiveDetail_APIInsertLogs.FirstOrDefault(x => x.ReceiveDetail_APIInsertLogID == logID);
                if (log == null) { return; }
                if (ReceiveDetail_APIInsertBatchID == null || ReceiveDetail_APIInsertBatchID == -1) { return; }
                //ReceiveDetail_APIInsertBatch b = new DataManagers.ReceiveDetail_APIInsertBatch();
                ReceiveDetail_APIInsertBatch dbBatch = log.ReceiveDetail_APIInsertBatches.FirstOrDefault(x => x.ReceiveDetail_APIInsertBatchID == ReceiveDetail_APIInsertBatchID);
                if (dbBatch == null) { return; }
                dbBatch.batch = batch;
                dbBatch.client = client;
                dbBatch.project = project;
                dbBatch.username = username;
                dbBatch.Status = status.status;
                dbBatch.Message = status.message;
                dbBatch.TypeRec = TypeRec;
                //b.ReceiveDetail_APIInsertBatchID = ReceiveDetail_APIInsertBatchID;
                foreach (API_Device_Out d in devices)
                {
                    //ReceiveDetail_APIInsertDevice dd = new ReceiveDetail_APIInsertDevice();
                    ReceiveDetail_APIInsertDevice dbDevice = dbBatch.ReceiveDetail_APIInsertDevices.FirstOrDefault(x => x.ReceiveDetail_APIInsertDeviceID == d.ReceiveDetail_APIInsertDeviceID);
                    if (dbDevice == null) { continue; }
                    dbDevice.ESN = d.esn;
                    dbDevice.Message = d.status.message;
                    dbDevice.Status = d.status.status;
                    dbDevice.ReceiveDetailID = d.receivedetailid;
                    foreach (API_DeviceAttribute_Out da in d.attributes)
                    {
                        //ReceiveDetail_APIInsertDeviceAttribute a = new ReceiveDetail_APIInsertDeviceAttribute();
                        ReceiveDetail_APIInsertDeviceAttribute dbAttribute = dbDevice.ReceiveDetail_APIInsertDeviceAttributes.FirstOrDefault(x => x.ReceiveDetail_APIInsertDeviceAttributeID == da.ReceiveDetail_APIInsertDeviceAttributeID);
                        if (dbAttribute == null) { continue; }
                        dbAttribute.attribute = da.attribute;
                        dbAttribute.Message = da.status.message;
                        dbAttribute.status = da.status.status;
                        dbAttribute.value = da.value;
                        //dbDevice.ReceiveDetail_APIInsertDeviceAttributes.Add(dbAttribute);
                    }
                    //dbBatch.ReceiveDetail_APIInsertDevices.Add(dbDevice);
                }
                //log.ReceiveDetail_APIInsertBatches.Add(dbBatch);
                //log.DateTimeout = DateTime.Now;
                //log.JSONout = JSON();
                ////ctx.ReceiveDetail_APIInsertLogs.InsertOnSubmit(log);
                ctx.SubmitChanges();
                //logID = log.ReceiveDetail_APIInsertLogID;
            }
        }
        public void ProcessBatch(decimal _logID, string LogPath)
        {
            DoLog("ProcessBatch LogID:" + _logID.ToString());
            Read(_logID);
            status.message = "Ready Process";
            if (process.Length >= 7 && process.Substring(0, 7).ToUpper() == "RECEIVE")
            {

                DoLog("   Receive Process");
                //Plog.LogIt("  Starting the IMEIUploadProcessor");
                IMEIUploadProcessor PR = new IMEIUploadProcessor(this, LogPath);
                DoLog("   IMEIUploadProcessor Created");
                string js = PR.LoadIMEIData();
                DoLog("   Receive Process Loaded");
                status = AnyErrors();
                DoLog("   Receive Process WriteProcessLogEnd");
                WriteProcessLogEnd();
                // Plog.LogIt("  Save...");
                DoLog("   Receive Process Ready to Save");
                Save();
                DoLog("   Receive Process Save Complete");
                // Plog.LogIt("  DONE IMEIUploadProcessor");

            }
            else if (process.Length >= 7 && process.Substring(0, 7).ToUpper() == "SHIPPING")
            {
                DoLog("   Shipping Process");
                // This one needs to update device attributes and then send the device through "SHIPPING (Version Change)".
                //status.SetError("Unknown Process name:" + process);
                //WriteProcessLogEnd();
            }
            else
            {
                DoLog("   Other Process");
                // This one needs to update device attributes only.
                //status.SetError("Unknown Process name:" + process);
                //WriteProcessLogEnd();
            }
        }
        private API_Status AnyErrors()
        {
            int count = 0;
            foreach (API_Device_Out d in devices)
            {
                if (d.isError() == true) { count++; }
            }
            API_Status s = new API_Status();
            if (count > 0) { s.SetError("Devices in error:" + count.ToString() + " processed:" + devices.Count.ToString()); return s; }
            s.SetSuccess("Devices processed:" + devices.Count.ToString());
            return s;
        }
        public bool isError()
        {
            return status.isError();
        }
        public bool isWarning()
        {
            return status.isWarning();
        }
        public bool isSuccess()
        {
            return status.isSuccess();
        }
        public void SetSuccess(string _message) { status.SetSuccess(_message); }
        public void SetSuccess() { status.SetSuccess(); }
        public void SetWarning(string _message) { status.SetWarning(_message); }
        public void SetError(string _message) { status.SetError(_message); }
        public void SetThisStatus(string _message) { status.SetThisStatus(_message); }
        public virtual string JSON()
        {
            //API_UploadDeviceBatch_Out dta = new API_UploadDeviceBatch_Out(this);
            return ExtendedMethods.JsonSerializer(this);
        }
        public string ProcessResult(string _batch)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                ReceiveDetail_APIInsertBatch log = ctx.ReceiveDetail_APIInsertBatches.FirstOrDefault(x => x.batch == _batch);
                if (log == null) { return ""; }
                return ProcessResult(log.ReceiveDetail_APIInsertID ?? -1);
            }
        }
        public string ProcessResult(decimal _logID)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                string rValue = "";
                ReceiveDetail_APIInsertLog log = ctx.ReceiveDetail_APIInsertLogs.FirstOrDefault(x => x.ReceiveDetail_APIInsertLogID == _logID);
                if (log == null) { return rValue; }
                rValue = log.JSONout;
                return rValue;
            }
        }
        public string ProcessResultFirstSend(string _batch)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                ReceiveDetail_APIInsertBatch log = ctx.ReceiveDetail_APIInsertBatches.FirstOrDefault(x => x.batch == _batch);
                if (log == null) { return ""; }
                return ProcessResultFirstSend(log.ReceiveDetail_APIInsertID ?? -1);
            }
        }
        public string ProcessResultFirstSend(decimal _logID)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                DoLog("ProcessResultFirstSend LogID:" + _logID.ToString());
                string rValue = "";
                ReceiveDetail_APIInsertLog log = ctx.ReceiveDetail_APIInsertLogs.FirstOrDefault(x => x.ReceiveDetail_APIInsertLogID == _logID);
                if (log == null) { return rValue; }
                rValue = log.JSONFirstReply;

                return rValue;
            }
        }
        private API_Status isValidDevice(string esn, string version)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                if (version.Length == 0) { version = "000"; }
                API_Status rvalue = new API_Status();
                var x = ctx.ReceiveDetails.FirstOrDefault(y => y.ESN == esn && y.Version == version);
                if (TypeRec == "RECEIVE")
                {
                    if (x == null) { rvalue.SetPending(""); return rvalue; }
                    rvalue.SetError("Device already on file:" + x.ReceiveDetailID.ToString());
                }
                if (x != null) { rvalue.SetPending(""); return rvalue; }
                rvalue.SetError("Device not found");
                return rvalue;
            }
        }
        private API_Status isValidAttribute(string attName, string attValue)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                API_Status rvalue = new API_Status();
                if (attName.ToUpper() == "PROJECTTAG") { rvalue.SetPending(""); return rvalue; }
                var x = ctx.Questions.FirstOrDefault(y => y.Name.ToUpper() == attName.ToUpper());
                if (x == null)
                {
                    rvalue.SetError("Unknown Attribute Name:");
                    return rvalue;
                }
                rvalue.SetPending(""); return rvalue;
            }
        }
        private API_Status isValidProcess()
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                API_Status rvalue = new API_Status();
                var x = ctx.Processes.FirstOrDefault(y => y.Name.ToUpper() == process.ToUpper());
                if (x == null)
                {
                    rvalue.SetError("Invalid Process:");
                    return rvalue;
                }
                rvalue.SetPending(""); return rvalue;
            }
        }

    }
    [Serializable]
    public class API_UpdateDeviceAttribute_Out
    {
        private string TypeRec = "UpdateATTRIB";
        public string batch = "";
        public string username = "";
        public decimal logID = -1;
        public decimal ReceiveDetail_APIInsertBatchID = -1;
        public API_Status status = new API_Status();
        public List<API_Device_Out> devices = new List<API_Device_Out>();
        public API_UpdateDeviceAttribute_Out(API_UpdateDeviceAttribute_In Data)
        {
            batch = Data.batch;
            username = Data.username;
            status.status = "New";
            status.message = "";
            foreach (API_Device_In d in Data.devices)
            {
                devices.Add(new API_Device_Out(d.esn, d.version, d.attributes));
            }

            logID = -1;
            ReceiveDetail_APIInsertBatchID = -1;
        }
        public API_UpdateDeviceAttribute_Out(string _batch, string _username)
        {
            batch = _batch;
            username = _username;
            logID = -1;
            ReceiveDetail_APIInsertBatchID = -1;
            status.status = "New";
            status.message = "";
        }
        public API_UpdateDeviceAttribute_Out(string _batch, string _username, List<API_Device_Out> _devices)
        {
            batch = _batch;
            username = _username;
            logID = -1;
            ReceiveDetail_APIInsertBatchID = -1;
            devices = _devices;
            status.status = "New";
            status.message = "";
        }
        public API_UpdateDeviceAttribute_Out()
        {
            batch = "";
            username = "";
            logID = -1;
            ReceiveDetail_APIInsertBatchID = -1;
            status.status = "New";
            status.message = "";
        }
        public void UpdateReceiveDetailID(string esn, decimal receivedetailid)
        {
            API_Device_Out d = devices.FirstOrDefault(x => x.esn == esn);
            if (d != null)
            {
                d.receivedetailid = receivedetailid;
            }
        }
        public void WriteLogStart(string JSONStringIn)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                ReceiveDetail_APIInsertLog log = new ReceiveDetail_APIInsertLog();
                log.CreateUser = username;
                log.JSONin = JSONStringIn;
                log.JSONout = JSONStringIn;
                ctx.ReceiveDetail_APIInsertLogs.InsertOnSubmit(log);
                ctx.SubmitChanges();
                logID = log.ReceiveDetail_APIInsertLogID;
            }
        }
        public void WriteProcessLogEnd()
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                ReceiveDetail_APIInsertLog log = ctx.ReceiveDetail_APIInsertLogs.FirstOrDefault(x => x.ReceiveDetail_APIInsertLogID == logID);
                if (log == null) { return; }
                log.DateTimeout = DateTime.Now;
                log.JSONout = JSON();
                ctx.SubmitChanges();
            }
        }
        public void WriteInsert()
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                ReceiveDetail_APIInsertLog log = ctx.ReceiveDetail_APIInsertLogs.FirstOrDefault(x => x.ReceiveDetail_APIInsertLogID == logID);
                if (log == null) { return; }
                if (ReceiveDetail_APIInsertBatchID != null && ReceiveDetail_APIInsertBatchID != -1) { return; }
                ReceiveDetail_APIInsertBatch dbBatch = new DataManagers.ReceiveDetail_APIInsertBatch();
                dbBatch.batch = batch;
                dbBatch.TypeRec = TypeRec;
                dbBatch.client = "";
                dbBatch.project = "";
                dbBatch.username = username;
                dbBatch.Status = status.status;
                dbBatch.Message = status.message;
                //b.ReceiveDetail_APIInsertBatchID = ReceiveDetail_APIInsertBatchID;
                foreach (API_Device_Out d in devices)
                {
                    ReceiveDetail_APIInsertDevice dbDevice = new ReceiveDetail_APIInsertDevice();
                    dbDevice.ESN = d.esn;
                    dbDevice.Version = d.version;
                    dbDevice.Message = d.status.message;
                    dbDevice.Status = d.status.status;
                    dbDevice.ReceiveDetailID = d.receivedetailid;
                    foreach (API_DeviceAttribute_Out da in d.attributes)
                    {
                        ReceiveDetail_APIInsertDeviceAttribute dbAttribute = new ReceiveDetail_APIInsertDeviceAttribute();
                        dbAttribute.attribute = da.attribute;
                        dbAttribute.Message = da.status.message;
                        dbAttribute.status = da.status.status;
                        dbAttribute.value = da.value;
                        dbDevice.ReceiveDetail_APIInsertDeviceAttributes.Add(dbAttribute);
                    }
                    dbBatch.ReceiveDetail_APIInsertDevices.Add(dbDevice);
                }
                log.ReceiveDetail_APIInsertBatches.Add(dbBatch);
                ctx.SubmitChanges();
            }
        }
        public void Read(string _batch)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                ReceiveDetail_APIInsertBatch log = ctx.ReceiveDetail_APIInsertBatches.FirstOrDefault(x => x.batch == _batch);
                if (log == null) { return; }
                Read(log.ReceiveDetail_APIInsertID ?? -1);
            }
            return;
        }
        public void Read(decimal _logID)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                ReceiveDetail_APIInsertLog log = ctx.ReceiveDetail_APIInsertLogs.FirstOrDefault(x => x.ReceiveDetail_APIInsertLogID == _logID);
                if (log == null) { return; }
                if (log.ReceiveDetail_APIInsertBatches == null) { return; }
                ReceiveDetail_APIInsertBatch b = log.ReceiveDetail_APIInsertBatches.FirstOrDefault();
                batch = b.batch;
                //client = b.client;
                //project = b.project;
                username = b.username;
                ReceiveDetail_APIInsertBatchID = b.ReceiveDetail_APIInsertBatchID;
                status.message = b.Message;
                status.status = b.Status;
                TypeRec = b.TypeRec;
                logID = b.ReceiveDetail_APIInsertID ?? -1;
                foreach (ReceiveDetail_APIInsertDevice d in b.ReceiveDetail_APIInsertDevices)
                {
                    API_Device_Out xx = new API_Device_Out();
                    xx.esn = d.ESN;
                    xx.version = d.Version;
                    xx.receivedetailid = d.ReceiveDetailID ?? -1;
                    xx.status.status = d.Status;
                    xx.status.message = d.Message;
                    xx.ReceiveDetail_APIInsertDeviceID = d.ReceiveDetail_APIInsertDeviceID;
                    foreach (ReceiveDetail_APIInsertDeviceAttribute da in d.ReceiveDetail_APIInsertDeviceAttributes)
                    {
                        API_DeviceAttribute_Out a = new API_DeviceAttribute_Out();
                        a.attribute = da.attribute;
                        a.status.message = da.Message;
                        a.status.status = da.status;
                        a.value = da.value;
                        a.ReceiveDetail_APIInsertDeviceAttributeID = da.ReceiveDetail_APIInsertDeviceAttributeID;
                        xx.attributes.Add(a);
                    }
                    devices.Add(xx);
                }
            }
        }
        public void Save()
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                ReceiveDetail_APIInsertLog log = ctx.ReceiveDetail_APIInsertLogs.FirstOrDefault(x => x.ReceiveDetail_APIInsertLogID == logID);
                if (log == null) { return; }
                if (ReceiveDetail_APIInsertBatchID == null || ReceiveDetail_APIInsertBatchID == -1) { return; }
                //ReceiveDetail_APIInsertBatch b = new DataManagers.ReceiveDetail_APIInsertBatch();
                ReceiveDetail_APIInsertBatch dbBatch = log.ReceiveDetail_APIInsertBatches.FirstOrDefault(x => x.ReceiveDetail_APIInsertBatchID == ReceiveDetail_APIInsertBatchID);
                if (dbBatch == null) { return; }
                dbBatch.batch = batch;
                dbBatch.client = "";
                dbBatch.project = "";
                dbBatch.username = username;
                if (status.status == null)
                { status.status = status.Summary(); }
                dbBatch.Status = status.status;
                dbBatch.Message = status.message;
                dbBatch.TypeRec = TypeRec;
                foreach (API_Device_Out d in devices)
                {
                    ReceiveDetail_APIInsertDevice dbDevice = dbBatch.ReceiveDetail_APIInsertDevices.FirstOrDefault(x => x.ReceiveDetail_APIInsertDeviceID == d.ReceiveDetail_APIInsertDeviceID);
                    if (dbDevice == null) { continue; }
                    dbDevice.ESN = d.esn;
                    if (d.status == null)
                    { d.status = d.Summary(); }
                    dbDevice.Message = d.status.message;
                    dbDevice.Status = d.status.status;
                    dbDevice.ReceiveDetailID = d.receivedetailid;
                    foreach (API_DeviceAttribute_Out da in d.attributes)
                    {
                        ReceiveDetail_APIInsertDeviceAttribute dbAttribute = dbDevice.ReceiveDetail_APIInsertDeviceAttributes.FirstOrDefault(x => x.ReceiveDetail_APIInsertDeviceAttributeID == da.ReceiveDetail_APIInsertDeviceAttributeID);
                        if (dbAttribute == null) { continue; }
                        dbAttribute.attribute = da.attribute;
                        dbAttribute.Message = da.status.message;
                        dbAttribute.status = da.status.status;
                        dbAttribute.value = da.value;
                    }
                }
                ctx.SubmitChanges();
            }
        }
        public void ProcessBatch(decimal _logID, string LogPath)
        {
            Read(_logID);
            ///////
            //  Here we need to run through the devices and update their attributes.
            IMEIUploadAttributeProcessor PR = new IMEIUploadAttributeProcessor(LogPath);
            API_Status js = PR.ProcessData(this);
            status.SetProcessed(AnyErrors().message);
            WriteProcessLogEnd();
            Save();
        }
        private API_Status AnyErrors()
        {
            int count = 0;
            foreach (API_Device_Out d in devices)
            {
                if (d.isError() == true) { count++; }
            }
            API_Status s = new API_Status();
            if (count > 0) { s.SetError("Devices in error:" + count.ToString() + " processed:" + devices.Count.ToString()); return s; }
            s.SetSuccess("Devices processed:" + devices.Count.ToString());
            return s;
        }
        public bool isError()
        {
            return status.isError();
        }
        public bool isWarning()
        {
            return status.isWarning();
        }
        public bool isSuccess()
        {
            return status.isSuccess();
        }
        public void SetSuccess(string _message) { status.SetSuccess(_message); }
        public void SetSuccess() { status.SetSuccess(); }
        public void SetWarning(string _message) { status.SetWarning(_message); }
        public void SetError(string _message) { status.SetError(_message); }
        public void SetThisStatus(string _message) { status.SetThisStatus(_message); }

        public static API_UpdateDeviceAttribute_Out FromJSON(string _JSONString)
        {
            return ExtendedMethods.JsonDeserialize<API_UpdateDeviceAttribute_Out>(_JSONString);
        }
        public string JSON()
        {
            return ExtendedMethods.JsonSerializer(this);
        }
        public string ProcessResult(string _batch)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                ReceiveDetail_APIInsertBatch log = ctx.ReceiveDetail_APIInsertBatches.FirstOrDefault(x => x.batch == _batch);
                if (log == null) { return ""; }
                return ProcessResult(log.ReceiveDetail_APIInsertID ?? -1);
            }
        }
        public string ProcessResult(decimal _logID)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                string rValue = "";
                ReceiveDetail_APIInsertLog log = ctx.ReceiveDetail_APIInsertLogs.FirstOrDefault(x => x.ReceiveDetail_APIInsertLogID == _logID);
                if (log == null) { return rValue; }
                rValue = log.JSONout;
                return rValue;
            }
        }
    }
    //           ///////////////////////////////////////////////////////////////


    [Serializable]
    public class API_Device_Out
    {
        public string esn = "";
        public string version = "";
        public API_Status status = new API_Status();
        public decimal receivedetailid = -1;
        public decimal ReceiveDetail_APIInsertDeviceID = -1;
        public List<API_DeviceAttribute_Out> attributes = new List<API_DeviceAttribute_Out>();
        public API_Device_Out(string ESN, string Version, List<API_DeviceAttribute_In> _attributes)
        {
            ReceiveDetail_APIInsertDeviceID = -1;
            esn = ESN;
            version = Version;
            if (version.Trim().Length == 0) { version = "000"; }
            foreach (API_DeviceAttribute_In a in _attributes)
            {
                API_DeviceAttribute_Out a1 = new API_DeviceAttribute_Out(a.attribute, a.value);
                attributes.Add(a1);
            }
        }
        public API_Device_Out()
        {
            esn = "";
            version = "";
            attributes = new List<API_DeviceAttribute_Out>();
            ReceiveDetail_APIInsertDeviceID = -1;
        }
        public string GetAttributeValue(string Name)
        {
            string rValue = "";
            if (Name.ToUpper() == "ESN" || Name.ToUpper() == "IMEI") { return esn; }
            API_DeviceAttribute_Out a = attributes.FirstOrDefault(x => x.attribute.ToUpper() == Name.ToUpper());
            if (a != null) { rValue = a.value; }
            return rValue;
        }
        public void SetMessageStatus(string _message, string _status)
        {
            if (status.message.Length > 0) { status.message += " "; }
            status.message += _message;
            if (status.status.ToUpper() != "ERROR") { status.status = _status; }
            return;
        }
        public void SetAttributeMessage(string Name, string message, string _status)
        {
            API_DeviceAttribute_Out a = attributes.FirstOrDefault(x => x.attribute.ToUpper() == Name.ToUpper());
            if (a != null)
            {
                if (a.status.message.Length > 0) { a.status.message += " "; }
                a.status.message += message;
                a.status.status = _status;
                if (a.status.status.ToUpper() == "ERROR") { status.status = _status; }
            }
            return;
        }
        public void SetAttributeSuccess(string Name, string message)
        {
            API_DeviceAttribute_Out a = attributes.FirstOrDefault(x => x.attribute.ToUpper() == Name.ToUpper());
            if (a != null)
            {
                if (a.status.message.Length > 0) { a.status.message += " "; }
                a.status.SetSuccess(a.status.message += message);
            }
            return;
        }
        public void SetAttributeError(string Name, string message)
        {
            API_DeviceAttribute_Out a = attributes.FirstOrDefault(x => x.attribute.ToUpper() == Name.ToUpper());
            if (a != null)
            {
                if (a.status.message.Length > 0) { a.status.message += " "; }
                a.status.SetError(a.status.message += message);
                status.status = a.status.status;
            }
            return;
        }
        public API_Status SummaryErrorsOnly()
        {
            API_Status rValue = new API_Status();
            rValue.status = "Success";
            foreach (API_DeviceAttribute_Out a in attributes)
            {
                if (a.isError() == true) { rValue.status = "Error"; }
                string s = a.SummaryErrorsOnly();
                if (s.Length > 0) { rValue.message += " "; rValue.message += s; }
            }
            if (rValue.isError() == true) { return rValue; }
            return null;
        }
        public API_Status Summary()
        {
            API_Status rValue = new API_Status();
            rValue.status = "Success";
            foreach (API_DeviceAttribute_Out a in attributes)
            {
                if (a.isError() == true) { rValue.status = "Error"; }
                string s = a.Summary();
                if (s.Length > 0) rValue.message += " ";
                rValue.message += s;
            }
            return rValue;
        }
        private API_Status AnyErrors()
        {
            int count = 0;
            foreach (API_DeviceAttribute_Out d in attributes)
            {
                if (d.isError() == true) { count++; }
            }
            API_Status s = new API_Status();
            if (count > 0) { s.SetError("Attributes in error:" + count.ToString() + " processed:" + attributes.Count.ToString()); return s; }
            s.SetSuccess("Attributes processed:" + attributes.Count.ToString());
            return s;
        }
        public bool isError()
        {
            return status.isError();
        }
        public bool isWarning()
        {
            return status.isWarning();
        }
        public bool isSuccess()
        {
            return status.isSuccess();
        }
        public void SetSuccess(string _message) { status.SetSuccess(_message); }
        public void SetSuccess() { status.SetSuccess(); }
        public void SetWarning(string _message) { status.SetWarning(_message); }
        public void SetError(string _message) { status.SetError(_message); }
        public void SetThisStatus(string _message) { status.SetThisStatus(_message); }
        public void AddAttribute(string name, string value)
        {
            API_DeviceAttribute_Out a = new API_DeviceAttribute_Out(name, value);
            if (attributes.Contains(a) == false)
            {
                attributes.Add(a);
            }
        }
    }
    [Serializable]
    public class API_DeviceAttribute_Out
    {
        public string attribute = "";
        public string value = "";
        public decimal ReceiveDetail_APIInsertDeviceAttributeID = -1;
        public API_Status status = new API_Status();
        public API_DeviceAttribute_Out(string _name, string _value)
        {
            attribute = _name;
            value = _value;
        }
        public API_DeviceAttribute_Out()
        {
            attribute = "";
            value = "";
        }
        public string SummaryErrorsOnly()
        {
            if (isError() == false) { return ""; }
            return Summary();
        }
        public string Summary()
        {
            return "(" + attribute.Trim() + "/" + value.Trim() + ":" + status.Summary() + ")";
        }
        private API_Status AnyErrors()
        {
            return status;
        }
        public bool isError()
        {
            return status.isError();
        }
        public bool isWarning()
        {
            return status.isWarning();
        }
        public bool isSuccess()
        {
            return status.isSuccess();
        }
        public void SetSuccess(string _message) { status.SetSuccess(_message); }
        public void SetSuccess() { status.SetSuccess(); }
        public void SetWarning(string _message) { status.SetWarning(_message); }
        public void SetError(string _message) { status.SetError(_message); }
        public void SetThisStatus(string _message) { status.SetThisStatus(_message); }
    }
    [Serializable]
    public class API_Status
    {
        public string status = "";
        public string message = "";
        public API_Status() { }
        public API_Status(string _status, string _message) { status = _status; message = _message; }
        public void SetSuccess(string _message) { status = "Success"; message = _message; }
        public void SetPending(string _message) { status = "Pending"; message = _message; }
        public void SetSuccess() { status = "Success"; message = ""; }
        public void SetWarning(string _message) { status = "Warning"; message = _message; }
        public void SetNew(string _message) { status = "New"; message = _message; }
        public void SetError(string _message) { status = "Error"; message = _message; }
        public void SetProcessed(string _message) { status = "Processed"; message = _message; }
        public void SetThisStatus(string _message)
        {
            if (_message.ToUpper().Contains("ERROR:") == true)
            {
                _message = _message.Replace("Error:", "");
                _message = _message.Replace("ERROR:", "");
                SetError(_message);
                return;
            }
            if (_message.ToUpper().Contains("SUCCESS:") == true)
            {
                _message = _message.Replace("Success:", "");
                _message = _message.Replace("SUCCESS:", "");
                SetSuccess(_message);
                return;
            }
            _message = _message.Replace("Success:", "");
            _message = _message.Replace("SUCCESS:", "");
            SetSuccess(_message);
        }
        public bool isSuccess() { if (status.ToUpper() == "SUCCESS") { return true; } return false; }
        public bool isWarning() { if (status.ToUpper() == "WARNING") { return true; } return false; }
        public bool isPending() { if (status.ToUpper() == "PENDING") { return true; } return false; }
        public bool isError() { if (status.ToUpper() == "ERROR") { return true; } return false; }
        public bool isNew() { if (status.ToUpper() == "NEW") { return true; } return false; }
        public string SummaryErrorsOnly() { if (isError() == false) { return ""; } return Summary(); }
        public string Summary() { return status.Trim() + "/" + message.Trim(); }
    }
    #endregion


    //           ///////////////////////////////////////////////////////////////

    #region API In Classes
    [Serializable]
    public class API_UploadDeviceBatch_In
    {
        public string batch = "";
        public string client = "";
        public string username = "";
        public string project = "";
        public string process = "";
        public List<API_Device_In> devices = new List<API_Device_In>();
        public API_UploadDeviceBatch_In(string _batch, string _client, string _project, string _username)
        {
            batch = _batch;
            client = _client;
            username = _username;
            project = _project;
        }
        public API_UploadDeviceBatch_In(string _batch, string _client, string _project, string _username, List<API_Device_In> _devices)
        {
            batch = _batch;
            client = _client;
            username = _username;
            project = _project;
            devices = _devices;
        }
        public API_UploadDeviceBatch_In()
        {
            batch = "";
            client = "";
            username = "";
            project = "";
        }
        public static API_UploadDeviceBatch_In FromJSON(string _JSONString)
        {
            return ExtendedMethods.JsonDeserialize<API_UploadDeviceBatch_In>(_JSONString);
        }
        public string JSON()
        {
            return ExtendedMethods.JsonSerializer(this);
        }
    }
    [Serializable]
    public class API_UpdateDeviceAttribute_In
    {
        public string batch = "";
        public string username = "";
        public List<API_Device_In> devices = new List<API_Device_In>();
        public API_UpdateDeviceAttribute_In(string _batch, string _username)
        {
            batch = _batch;
            username = _username;
        }
        public API_UpdateDeviceAttribute_In(string _batch, string _username, List<API_Device_In> _devices)
        {
            batch = _batch;
            username = _username;
            devices = _devices;
        }
        public API_UpdateDeviceAttribute_In()
        {
            batch = "";
            username = "";
        }
        public static API_UpdateDeviceAttribute_In FromJSON(string _JSONString)
        {
            return ExtendedMethods.JsonDeserialize<API_UpdateDeviceAttribute_In>(_JSONString);
        }
        public string JSON()
        {
            return ExtendedMethods.JsonSerializer(this);
        }
    }

    [Serializable]
    public class API_Device_In
    {
        public string esn = "";
        public string version = "";
        public List<API_DeviceAttribute_In> attributes = new List<API_DeviceAttribute_In>();
        public API_Device_In(string ESN, string Version, List<API_DeviceAttribute_In> _attributes)
        {
            esn = ESN;
            version = Version;
            attributes = _attributes;
            if (version.Trim().Length == 0) { version = "000"; }
        }
        public API_Device_In()
        {
            esn = "";
            version = "";
        }
        public string GetAttributeValue(string Name)
        {
            string rValue = "";
            if (Name.ToUpper() == "ESN" || Name.ToUpper() == "IMEI") { return esn; }
            if (Name.ToUpper() == "VERSION") { return version; }
            API_DeviceAttribute_In a = attributes.FirstOrDefault(x => x.attribute.ToUpper() == Name.ToUpper());
            if (a != null) { rValue = a.value; }
            return rValue;
        }
        public void AddAttribute(string name, string value)
        {
            API_DeviceAttribute_In a = new API_DeviceAttribute_In(name, value);
            if (attributes.Contains(a) == false)
            {
                attributes.Add(a);
            }
        }
    }
    [Serializable]
    public class API_DeviceAttribute_In
    {
        public string attribute = "";
        public string value = "";
        public API_DeviceAttribute_In(string _name, string _value)
        {
            attribute = _name;
            value = _value;
        }
        public API_DeviceAttribute_In()
        {
            attribute = "";
            value = "";
        }

    }

    #endregion




}