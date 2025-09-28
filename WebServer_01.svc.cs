using System;
using System.Collections;
using System.Collections.Generic;
//using System.Web.Script.Serialization;            // Used to generate json for back and forth
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Xml.Serialization;
// using DAL;

using BW_WebApp.Classes;
//using Factory_DataModel;
using BW_WebApp.DataManagers;

namespace BW_WebApp
{

    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]

    public partial class WebServer_01
    {
        //JimErrorLogManager logManager;
        string _ConnectionString = string.Empty;
        bool WriteLog = false;
        clsLog Log;
        public string ConnectionString
        {
            get { return _ConnectionString; }
            set { _ConnectionString = value; }
        }

        public WebServer_01()
        {
            Log = new clsLog(HttpContext.Current.Server.MapPath("~"), "WebServer_01_Log.txt", "JIM", System.Configuration.ConfigurationManager.AppSettings["WriteLogUserNameToLog"]);
            if (System.Configuration.ConfigurationManager.AppSettings["WriteLog"].ToUpper() == "TRUE")
            {
                WriteLog = true;
            }
            Log.writeLogData = WriteLog;
        }


        //private void SetTheChannel()
        //{
        //    BasicHttpBinding binding = new BasicHttpBinding();
        //    EndpointAddress endpoint = new EndpointAddress("web service url");
        //    ChannelFactory<WebServer_01> factory = new ChannelFactory<WebServer_01>(binding, endpoint);
        //    WebServer_01 proxy = factory.CreateChannel();
        //    proxy = factory.CreateChannel();
        //    //proxy.DoWork();
        //}


        //#region Fayzeen_RequestedAccessPoints

        //[OperationContract]
        ////[WebMethod]
        ////[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public TestClass TestConnectTest01(TestClass JSONDataString)
        //{
        //    return JSONDataString;
        //    //string xx = JSONDataString.Replace('{', ' ');
        //    //xx = xx.Replace('}', ' ');
        //    //xx = xx.Replace('"', ' ');
        //    //xx = xx.Replace(':', ' ');
        //    //string rValue = "{\"value\":\"" + xx + "\"}";
        //    //return rValue;
        //}

        //[OperationContract]
        //public string TestConnectTest(string JSONDataString)
        //{
        //    TestClass x = TestClass.FromJSON(JSONDataString);
        //    x.firstname = "Jim";
        //    x.lastname = "ME";
        //    x.age = 60;
        //    return x.JSON();

        //    //string xx = JSONDataString.Replace('{', ' ');
        //    //xx = xx.Replace('}', ' ');
        //    //xx = xx.Replace('"', ' ');
        //    //xx = xx.Replace(':', ' ');
        //    //string rValue = "{\"value\":\"" + xx + "\"}";
        //    //return rValue;
        //}

        //[OperationContract]
        //public API_UploadBatch_In BulkDeviceUploadTest(API_UploadBatch_In JSONData)
        //{
        //    //using (clsLinqDataContext ctx = new clsLinqDataContext())
        //    //{
        //    //    //API_UploadBatch x = API_UploadBatch.FromJSON(JSONDataString);
        //    //    //IMEIUploadProcessor PR = new IMEIUploadProcessor(JSONData);
        //    //    JSONData.WriteLogStart(JSONData.JSON());
        //    //    //string js = PR.LoadIMEIData();
        //    //    JSONData.WriteLogEnd();
        //        return JSONData;
        //    //}
        //}
        //[OperationContract]
        //public string BulkDeviceUploadTest01(API_UploadBatch_In JSONData)
        //{
        //    //using (clsLinqDataContext ctx = new clsLinqDataContext())
        //    //{
        //    //    //API_UploadBatch x = API_UploadBatch.FromJSON(JSONDataString);
        //    //    //IMEIUploadProcessor PR = new IMEIUploadProcessor(JSONData);
        //    //    JSONData.WriteLogStart(JSONData.JSON());
        //    //    //string js = PR.LoadIMEIData();
        //    //    JSONData.WriteLogEnd();
        //    if (JSONData == null) { return "JSONData = NULL"; }
        //    if (JSONData == null) { return JSONData.batch; }
        //    return JSONData.JSON();
        //    //}
        //}
        //[OperationContract]
        //public string BulkDeviceUploadTest02(API_UploadBatch_In JSONData)
        //{
        //    //using (clsLinqDataContext ctx = new clsLinqDataContext())
        //    //{
        //    //    //API_UploadBatch x = API_UploadBatch.FromJSON(JSONDataString);
        //    //    //IMEIUploadProcessor PR = new IMEIUploadProcessor(JSONData);
        //    //    JSONData.WriteLogStart(JSONData.JSON());
        //    //    //string js = PR.LoadIMEIData();
        //    //    JSONData.WriteLogEnd();
        //    if (JSONData == null) { return "JSONData = NULL"; }
        //    return JSONData.JSON();
        //    //}
        //}


        //[OperationContract]
        //public string BulkDeviceUpload(string JSONDataString)
        //{
        //    using (clsLinqDataContext ctx = new clsLinqDataContext())
        //    {
        //        API_UploadDeviceBatch_In x1 = API_UploadDeviceBatch_In.FromJSON(JSONDataString);
        //        API_UploadDeviceBatch_Out x = new API_UploadDeviceBatch_Out(x1);
        //        x.WriteLogStart(JSONDataString);
        //        x.WriteInsert();
        //        API_UploadDeviceBatch_Out re = new API_UploadDeviceBatch_Out();
        //        re.Read(x.logID);
        //        string rValue = re.JSON();
        //        string LogPath = HttpContext.Current.Server.MapPath("~");
        //        ThreadPool.QueueUserWorkItem(Report => RunBatch(re.logID, LogPath));
        //        // Spawn out a new 
        //        return rValue;
        //    }
        //}
        //[OperationContract]
        //public string BulkDeviceUploadStatus(string Batch)
        //{
        //    using (clsLinqDataContext ctx = new clsLinqDataContext())
        //    {
        //        //decimal x = 0;
        //        API_UploadDeviceBatch_Out re = new API_UploadDeviceBatch_Out();
        //        //decimal.TryParse(LogID, out x);
        //        re.Read(Batch);
        //        string rValue = re.JSON();
        //        return rValue;
        //    }
        //}
        //[OperationContract]
        //public string BulkDeviceUploadStatusID(string LogID)
        //{
        //    using (clsLinqDataContext ctx = new clsLinqDataContext())
        //    {
        //        decimal x = 0;
        //        API_UploadDeviceBatch_Out re = new API_UploadDeviceBatch_Out();
        //        decimal.TryParse(LogID, out x);
        //        re.Read(x);
        //        string rValue = re.JSON();
        //        return rValue;
        //    }
        //}
        //private void RunBatch(decimal ID, string LogPath)
        //{
        //    API_UploadDeviceBatch_Out re = new API_UploadDeviceBatch_Out();
        //    re.ProcessBatch(ID, LogPath);
        //}

        //[OperationContract]
        //public string GETUnitData(string ESN, string Version, string UserName, string DataRequested)
        //{
        //    using (clsLinqDataContext ctx = new clsLinqDataContext())
        //    {
        //        return ctx.GETUnitData(ESN, Version, UserName, DataRequested);
        //    }
        //}
        ////[OperationContract]
        ////public string RecordBrowserTimetoLog(string sLogID, string Time_ms, string UserName)
        ////{
        ////    string rValue = "Error:(" + sLogID + ") No action taken!";
        ////    decimal LogID = -1;
        ////    decimal.TryParse(sLogID, out LogID);
        ////    decimal MS = -1;
        ////    decimal.TryParse(Time_ms, out MS);
        ////    if (MS > 0 && LogID > 0)
        ////    {
        ////        TimeLogManager timelog = new TimeLogManager("jmccomb", "");
        ////        timelog.RecordBrowserTimetoLog(LogID, MS);
        ////        rValue = "Success:(" + sLogID + ") Saved!";
        ////    }
        ////    return rValue;
        ////}
        //#endregion

        #region Save

        #region Threaded Save
        [OperationContract]
        public string AddDataDetailThreaded(string Data, string Threaded)
        {
            return AddDataDetailThreadedGO(Data, Threaded, "", GetUserIPAddress());
        }

        public string AddDataDetailThreadedGO(string Data, string Threaded, string AlertString, string IPAddress)
        {
            return AddDataDetailThreadedGOB("", Data, Threaded, AlertString, IPAddress);
            #region comment
            ////decimal TimeInMilliSeconds = 0;
            //string IsThreaded = "N";

            //TimeLogManager timelog = new TimeLogManager("jmccomb", IPAddress);
            //timelog.StartTimer();
            //JsonString ScreenData = new JsonString(Data, true);
            //string CurUserName = ScreenData.GetValue("CurUserName");
            //timelog.UserName = CurUserName;
            //decimal ReceiveDetailID = ScreenData.GetValueDecimal("ReceiveDetailID", -1);
            //timelog.SaveTimeLogWorkScreen_PreProcess(ReceiveDetailID, "PreWorkScreenSave0b", -1, Data);

            //decimal ReceiveHeaderID = ScreenData.GetValueDecimal("ReceiveHeaderID", -1);
            //decimal ReceiveDetailBulkID = ScreenData.GetValueDecimal("ReceiveDetailBulkID", -1);
            //decimal ClientLocationID = ScreenData.GetValueDecimal("ClientLocationID", -1);
            //decimal ProjectID = ScreenData.GetValueDecimal("ProjectID", -1);
            //decimal CurrentProcessID = ScreenData.GetValueDecimal("CurProcessID", -1);
            //string ESN = ScreenData.GetValue("ESN");
            //string Project = ScreenData.GetValue("Project");
            //string CurProcess = ScreenData.GetValue("CurProcess");
            //string RMA = ScreenData.GetValue("RMA");
            //string ProjectTag = ScreenData.GetValue("PROJTAG");
            //string AuthorizationRequired = "N";
            //string MakeModelString = "";
            //string PCLBString = "";
            //string CompList = "";
            ////DateTime StartDate = DateTime.Now;


            //timelog.SaveTimeLogWorkScreen_PreProcess(ReceiveDetailID, "PreWorkScreenSave1", CurrentProcessID, Data);



            //// We need to get things ready to be received as json when going back to the page.
            //JsonString rString = new JsonString("Result", "NotSaved");
            //if (ESN.Length == 0)
            //{
            //    //rString = new JsonString("Result", "NotSaved");
            //    rString.AddValuePair("Error", "No ESN given");
            //    return rString.ToString();
            //}
            //if (CurProcess.ToUpper() == "BULKRECEIVE" || CurProcess.ToUpper() == "RECEIVEFROMBULK" || CurProcess.ToUpper() == "BULKMOVE")
            //{
            //    rString.AddValuePair("Error", "Not Saved! -- Invalid Process:" + CurProcess);
            //    return rString.ToString();
            //}
            ////bool addedNew = false;
            //ReceiveDetailManager rdm = new ReceiveDetailManager(CurUserName);
            //using (clsLinqDataContext ctx = rdm.GetDataContext(CurUserName))
            //{
            //    string IFSRMANumber = ScreenData.GetTextData(ctx, "IFS RMA Number");
            //    bool ISThereNow = rdm.isDetailThere(ctx, ESN);
            //    #region New Add edit checks
            //    //if (rdm.isDetailThere(ctx, ESN) == false && rdm.isOKToAddFromThisProcess(CurProcess) == false)
            //    if (ISThereNow == false && rdm.isOKToAddFromThisProcess(CurProcess) == false)
            //    {
            //        //rString = new JsonString("Result", "NotSaved");
            //        rString.AddValuePair("Error", "Unable to add from this process:" + CurProcess);
            //        return rString.ToString();
            //    }

            //    // If this is a new one, we need to make sure this is not moving into a "Frozen" area.
            //    //if (rdm.isDetailThere(ctx, ESN) == false)
            //    if (ISThereNow == false)
            //    {
            //        string rMessage = "";
            //        if (rdm.IsThisFrozen(Data, ref rMessage) == true)
            //        {
            //            rString = new JsonString("Result", "NotSaved");
            //            rString.AddValuePair("Error", rMessage);
            //            return rString.ToString();
            //        }
            //    }
            //    // Check to make sure the SKU is valid.
            //    if (ISThereNow == false)
            //    {
            //        string rMessage = "";
            //        if (ScreenData.IsSKUValid(ctx, ref rMessage) != true)
            //        {
            //            rString = new JsonString("Result", "NotSaved");
            //            rString.AddValuePair("Error", rMessage);
            //            return rString.ToString();
            //        }
            //    }
            //    #endregion
            //    ReceiveDetail rec = null;
            //    rec = rdm.ReceiveDetail_NewOrThisESN(ctx, ESN);
            //    if (rec == null)
            //    {
            //        //rString = new JsonString("Result", "NotSaved");
            //        rString.AddValuePair("Error", "Unable to get a new or existing Unit Record:" + CurProcess);
            //        return rString.ToString();
            //    }   // This may never happen because the function returns a new record if there is no 000 version.

            //    // If this is a receive screen, and we have a ReceiveDetailID already, then we need to stop/exit
            //    if (rec.ReceiveDetailID > 0 && rec.ReceiveDetailID != ReceiveDetailID && CurProcess.Length >= 7 && CurProcess.Substring(0, 7).ToUpper() == "RECEIVE")
            //    {
            //        rString.AddValuePair("Error", "Not Saved! -- ESN already on file!");
            //        return rString.ToString();
            //    }

            //    // We need to do some preliminary save stuff just in case we have a bagtag etc to be printed.
            //    //    The Very least we have to add the unit new if it is not there now.
            //    #region Do if new device
            //    if (rec.ReceiveDetailID < 1)
            //    {
            //        string IFS_POAdjustedData = "";
            //        string rMessage = "";
            //        decimal IFSPurchaseOrderDetailID = -1;
            //        rec.SKU = ScreenData.GetSKU();
            //        rec.IFSCondition = "GEN";
            //        rec.IFSLocation = rdm.GetIFSLocation(-1, CurProcess, ClientLocationID, "");
            //        rec.ClientLocationID = ClientLocationID;
            //        rec.ReceiveHeaderID = ReceiveHeaderID;
            //        if (RMA.Length == 0) { rec.RMANumber = IFSRMANumber; }
            //        else { rec.RMANumber = CleanData(RMA); }
            //        rec.ESN = CleanData(ESN);
            //        rec.ProjectTag = CleanData(ProjectTag);
            //        rec.ICB = "";
            //        rec.PIN = "";
            //        rec.ProjectName = CleanData(Project);
            //        rec.ProjectID = ProjectID;
            //        rec.ReceiveDate = DateTime.Now;
            //        rec.MiscNote = "";
            //        rec.ISFTransactionDirective = rdm.IFSDirective(ctx, "Ignore");
            //        rec.IFSPurchaseOrderDetailID = IFSPurchaseOrderDetailID;
            //        #region A Couple Quick Edit Checks
            //        if (rec.SKU == null || rec.SKU.Length == 0)
            //        {
            //            rString.AddValuePair("Error", "Error on Save, blank SKU. Please try again");
            //            return rString.ToString();
            //        }
            //        if (rec.IFSLocation == null || rec.IFSLocation.Length == 0)
            //        {
            //            rString.AddValuePair("Error", "Error on Save, blank Site Location. Please try again");
            //            return rString.ToString();
            //        }
            //        #endregion
            //        #region See if it requires a PO
            //        if (CurProcess.ToUpper() == "RECEIVE AGAINST PO")      // We only want to deal with the PO  Number if it comes in from this process screen
            //        {
            //            if (rdm.IsPODataValid02(Data, AlertString, ref IFSPurchaseOrderDetailID, ref IFS_POAdjustedData, ref rMessage) == false)
            //            {
            //                rString.AddValuePair("Error", rMessage);
            //                return rString.ToString();
            //            }
            //            Data = IFS_POAdjustedData;
            //        }
            //        #endregion
            //        //try {
            //        rdm.InsertReceiveDetail(ctx, rec, CurrentProcessID);
            //        //}
            //        //catch (Exception ex)
            //        //{
            //        //rMessage = "ERROR TEST";                    // ex.Message;
            //        //    rString = new JsonString("Result", "NotSaved");
            //        //    rString.AddValuePair("Error", rMessage);
            //        //    return rString.ToString();
            //        //}
            //        // we have got a problem, there should be an ID now that we inserted it.
            //        if (rec.ReceiveDetailID < 1)
            //        {
            //            rString.AddValuePair("Error", "Device data not saved");
            //            return rString.ToString();
            //        }
            //        // we need to ignore the inial save. Now we have things saved, we need to do the next save as a PO_Initiate.
            //        Process process = ctx.Processes.FirstOrDefault(x => x.ProcessID == CurrentProcessID);
            //        if (process != null && process.IFSDirectiveType != null && process.IFSDirectiveType.Length > 0)
            //        {
            //            rec.ISFTransactionDirective = rdm.IFSDirective(ctx, process.IFSDirectiveType);
            //        }
            //        else
            //        {
            //            rec.ISFTransactionDirective = rdm.IFSDirective(ctx, "PO_Receipt");
            //        }

            //        //addedNew = true;
            //        //// up till now, we may not know what these two ID fields are. 
            //        //// Now that we just inserted a new on, we need to update these keys.
            //        ReceiveHeaderID = rec.ReceiveHeaderID;
            //        ReceiveDetailID = rec.ReceiveDetailID;
            //        MakeModelString = rdm.MakeModelColourNickName(ctx, ReceiveDetailID);
            //        PCLBString = rdm.GetProjectClientLocationBinString(ctx, ReceiveDetailID);
            //        CompList = rdm.GetRequestProcessCompletionList(ctx, ReceiveDetailID);

            //        string IsAuthorizationRequired = rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Authorization");
            //        if (IsAuthorizationRequired.ToUpper() == "APPROVAL REQUIRED") { AuthorizationRequired = "Y"; }
            //    }
            //    #endregion

            //    ////////////////////////////////////////////////////////////////////////
            //    // we need to save the raw data to pull it for the bagtag.
            //    // Turned off code to run threaded.
            //    //if (Threaded.ToUpper() == "Y" && (addedNew == true || (CurProcess.Length > 6 && CurProcess.Substring(0, 7).ToUpper() == "KITTING") ||
            //    //                                 (CurProcess.Length > 7 && CurProcess.Substring(0, 8).ToUpper() == "SHIPPING")))
            //    //{
            //    //    IsThreaded = "Y";
            //    //    rdm.InsertReceiveDetail_BagtagPreSaveData(ctx, ReceiveDetailID, Data, ProjectID, CurrentProcessID);
            //    //}
            //    ////////////////////////////////////////////////////////////////////////
            //}
            //if (ReceiveDetailID < 1)
            //{
            //    rString.AddValuePair("Error", "Device data not saved");
            //    return rString.ToString();
            //}
            //// make sure that if this is a screen with parts, they have picked all parts related to the unit.
            //// Otherwise, there are problems and the unit is not saved.
            //if (IsThisAPartsProcess(Data) == true && AreAllAssignedPartsAccountedFor(Data) == false)
            //{
            //    rString.AddValuePair("Error", "Unused assigned parts. Data Not Saved");
            //    return rString.ToString();
            //}



            //timelog.SaveTimeLogWorkScreen_PreProcess(ReceiveDetailID, "PreWorkScreenSave2", CurrentProcessID, Data);
            //// I needed a return value so I remed out the code to run this threaded.
            ////if (Threaded.ToUpper() == "Y") { ThreadPool.QueueUserWorkItem(Report => RunThreadedSave(Data, IPAddress)); }    // This should go off and do it's thing giving control back to the browser. (Apear as if faster save.)
            ////else { RunThreadedSave(Data, IPAddress); }

            //string rMessagex = "";
            //rMessagex = RunThreadedSave(ReceiveDetailID, Data, IPAddress, timelog);
            //if (rMessagex.Length > 0)
            //{
            //    rString.AddValuePair("Error", rMessagex);
            //    return rString.ToString();
            //}


            //rString = new JsonString("Result", "Saved");
            //rString.AddValuePair("Cellbie", "");
            //if (CurProcess.ToUpper() == "RECEIVECELLBIE")
            //{
            //    Cellbie cellbie = new Cellbie(CurUserName);
            //    string ok = rdm.GetESNAttribute(ReceiveDetailID, "CellbieOK");
            //    string reason = rdm.GetESNAttribute(ReceiveDetailID, "CellbieReason");
            //    bool isOK = true;
            //    if (ok != "Yes") { isOK = false; }
            //    BrainDeviceTransaction info = cellbie.SendReceiveTransaction(ReceiveDetailID, ESN, isOK, reason, SysUtil.CellbieAPISimulate());
            //    if (info.Success == false)
            //    {
            //        rString.RemoveValuePair("Cellbie", "");
            //        rString.AddValuePair("Cellbie", info.CellbieError.Error);
            //    }
            //}
            //rString.AddValuePair("ReceiveHeaderID", ReceiveHeaderID.ToString());
            //rString.AddValuePair("ReceiveDetailBulkID", "-1");
            //rString.AddValuePair("ReceiveDetailID", ReceiveDetailID.ToString());
            ////rString.AddValuePair("SUFD", rdm.GetSetUpFieldDef(ctx, ProjectID));
            ////rString.AddValuePair("QTY", QTY);
            //rString.AddValuePair("CompProcList", CompList);
            //rString.AddValuePair("MMS", MakeModelString);
            //rString.AddValuePair("THREADED", IsThreaded);
            //rString.AddValuePair("AR", AuthorizationRequired);
            //rString.AddValuePair("PCLB", PCLBString);                           //lblProjectClientLocationBinTitle
            //timelog.StopTimer();
            //timelog.SaveTimeLogWorkScreen(ReceiveDetailID, CurrentProcessID, Data);
            //rString.AddValuePair("LogID", timelog.LogID.ToString());
            //return rString.ToString();
            #endregion
        }

        private static bool isThere(PairStringValue rec, string ESN)
        {
            if (rec.Key == ESN) { return true; }
            return false;
        }
        public List<PairStringValue> AddDataDetailThreadedGOBULK(string ListType, string ESNList, string Data, string Threaded, string AlertString, string IPAddress)
        {
            List<PairStringValue> ESNListWithMessage = new List<PairStringValue>();

            if (ESNList.Length > 0)
            {
                if (ListType.ToUpper() == "EXCEL")
                {
                    //List<string> data = ESNList.Split(new string[] { "\r\n", "\n", " " }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    List<string> data = ESNList.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    foreach (string x in data) { if (x.Length > 0) { ESNListWithMessage.Add(new PairStringValue(x, "")); } }
                }
                if (ListType.ToUpper() == "SPACE")
                {
                    List<string> data = ESNList.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    //foreach (string x in data) { if (x.Length > 0 && lstHistory.Items.FindByText(x) == null) { lstHistory.Items.Add(new ListItem(x)); } }
                    foreach (string x in data) { if (x.Length > 0) { ESNListWithMessage.Add(new PairStringValue(x, "")); } }
                }
                if (ListType.ToUpper() == "COMMA")
                {
                    List<string> data = ESNList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    //foreach (string x in data) { if (x.Length > 0 && lstHistory.Items.FindByText(x) == null) { lstHistory.Items.Add(new ListItem(x)); } }
                    foreach (string x in data) { if (x.Length > 0) { ESNListWithMessage.Add(new PairStringValue(x, "")); } }
                }
            }
            foreach (PairStringValue e in ESNListWithMessage)
            {
                e.Value = AddDataDetailThreadedGOB(e.Key, Data, Threaded, AlertString, IPAddress);
            }
            return ESNListWithMessage;
        }

        public string AddDataDetailThreadedGOB(string ESN, string Data, string Threaded, string AlertString, string IPAddress)
        {
            //decimal TimeInMilliSeconds = 0;
            string IsThreaded = "N";

            TimeLogManager timelog = new TimeLogManager("jmccomb", IPAddress);
            timelog.StartTimer();
            JsonString ScreenData = new JsonString(Data, true);
            if (ESN.Trim().Length == 0) { ESN = ScreenData.GetValue("ESN"); }
            string CurUserName = ScreenData.GetValue("CurUserName");
            timelog.UserName = CurUserName;
            decimal ReceiveDetailID = ScreenData.GetValueDecimal("ReceiveDetailID", -1);
            timelog.SaveTimeLogWorkScreen_PreProcess(ReceiveDetailID, "PreWorkScreenSave0b", -1, Data);
            //string ESN = ScreenData.GetValue("ESN");
            decimal ReceiveHeaderID = ScreenData.GetValueDecimal("ReceiveHeaderID", -1);
            decimal ReceiveDetailBulkID = ScreenData.GetValueDecimal("ReceiveDetailBulkID", -1);

            decimal ClientLocationID = ScreenData.GetValueDecimal("ClientLocationID", -1);
            decimal ProjectID = ScreenData.GetValueDecimal("ProjectID", -1);
            decimal CurrentProcessID = ScreenData.GetValueDecimal("CurProcessID", -1);
            string Project = ScreenData.GetValue("Project");
            string CurProcess = ScreenData.GetValue("CurProcess");
            string RMA = ScreenData.GetValue("RMA");
            string ProjectTag = ScreenData.GetValue("PROJTAG");
            string AuthorizationRequired = "N";
            string MakeModelString = "";
            string PCLBString = "";
            string CompList = "";
            //DateTime StartDate = DateTime.Now;

            timelog.SaveTimeLogWorkScreen_PreProcess(ReceiveDetailID, "PreWorkScreenSave1", CurrentProcessID, Data);


            // We need to get things ready to be received as json when going back to the page.
            JsonString rString = new JsonString("Result", "NotSaved");
            if (ESN.Length == 0)
            {
                //rString = new JsonString("Result", "NotSaved");
                rString.AddValuePair("Error", "No ESN given");
                return rString.ToString();
            }
            if (CurProcess.ToUpper() == "BULKRECEIVE" || CurProcess.ToUpper() == "RECEIVEFROMBULK" || CurProcess.ToUpper() == "BULKMOVE")
            {
                rString.AddValuePair("Error", "Not Saved! -- Invalid Process:" + CurProcess);
                return rString.ToString();
            }
            //bool addedNew = false;
            ReceiveDetailManager rdm = new ReceiveDetailManager(CurUserName);
            using (clsLinqDataContext ctx = rdm.GetDataContext(CurUserName))
            {
                string IFSRMANumber = ScreenData.GetTextData(ctx, "IFS RMA Number");
                bool ISThereNow = rdm.isDetailThere(ctx, ESN);
                #region New Add edit checks
                //if (rdm.isDetailThere(ctx, ESN) == false && rdm.isOKToAddFromThisProcess(CurProcess) == false)
                if (ISThereNow == false && rdm.isOKToAddFromThisProcess(CurProcess) == false)
                {
                    //rString = new JsonString("Result", "NotSaved");
                    rString.AddValuePair("Error", "Unable to add from this process:" + CurProcess);
                    return rString.ToString();
                }

                // If this is a new one, we need to make sure this is not moving into a "Frozen" area.
                //if (rdm.isDetailThere(ctx, ESN) == false)
                if (ISThereNow == false)
                {
                    string rMessage = "";
                    if (rdm.IsThisFrozen(Data, ref rMessage) == true)
                    {
                        rString = new JsonString("Result", "NotSaved");
                        rString.AddValuePair("Error", rMessage);
                        return rString.ToString();
                    }
                }
                // Check to make sure the SKU is valid.
                if (ISThereNow == false)
                {
                    string rMessage = "";
                    if (ScreenData.IsSKUValid(ctx, ref rMessage) != true)
                    {
                        rString = new JsonString("Result", "NotSaved");
                        rString.AddValuePair("Error", rMessage);
                        return rString.ToString();
                    }
                }
                #endregion
                ReceiveDetail rec = null;
                rec = rdm.ReceiveDetail_NewOrThisESN(ctx, ESN);
                if (rec == null)
                {
                    //rString = new JsonString("Result", "NotSaved");
                    rString.AddValuePair("Error", "Unable to get a new or existing Unit Record:" + CurProcess);
                    return rString.ToString();
                }   // This may never happen because the function returns a new record if there is no 000 version.

                // If this is a receive screen, and we have a ReceiveDetailID already, then we need to stop/exit
                if (rec.ReceiveDetailID > 0 && rec.ReceiveDetailID != ReceiveDetailID && CurProcess.Length >= 7 && CurProcess.Substring(0, 7).ToUpper() == "RECEIVE")
                {
                    rString.AddValuePair("Error", "Not Saved! -- ESN already on file!");
                    return rString.ToString();
                }

                // We need to do some preliminary save stuff just in case we have a bagtag etc to be printed.
                //    The Very least we have to add the unit new if it is not there now.
                #region Do if new device
                if (rec.ReceiveDetailID < 1)
                {
                    string IFS_POAdjustedData = "";
                    string rMessage = "";
                    decimal IFSPurchaseOrderDetailID = -1;
                    rec.SKU = ScreenData.GetSKU();
                    rec.IFSCondition = "GEN";
                    rec.IFSLocation = rdm.GetIFSLocation(-1, CurProcess, ClientLocationID, "");
                    rec.ClientLocationID = ClientLocationID;
                    rec.ReceiveHeaderID = ReceiveHeaderID;
                    if (RMA.Length == 0) { rec.RMANumber = IFSRMANumber; }
                    else { rec.RMANumber = CleanData(RMA); }
                    rec.ESN = CleanData(ESN);
                    rec.ProjectTag = CleanData(ProjectTag);
                    rec.ICB = "";
                    rec.PIN = "";
                    rec.ProjectName = CleanData(Project);
                    rec.ProjectID = ProjectID;
                    rec.ReceiveDate = DateTime.Now;
                    rec.MiscNote = "";
                    rec.ISFTransactionDirective = rdm.IFSDirective(ctx, "Ignore");
                    rec.IFSPurchaseOrderDetailID = IFSPurchaseOrderDetailID;
                    #region A Couple Quick Edit Checks
                    if (rec.SKU == null || rec.SKU.Length == 0)
                    {
                        rString.AddValuePair("Error", "Error on Save, blank SKU. Please try again");
                        return rString.ToString();
                    }
                    if (rec.IFSLocation == null || rec.IFSLocation.Length == 0)
                    {
                        rString.AddValuePair("Error", "Error on Save, blank Site Location. Please try again");
                        return rString.ToString();
                    }
                    #endregion
                    #region See if it requires a PO
                    if (CurProcess.ToUpper() == "RECEIVE AGAINST PO")      // We only want to deal with the PO  Number if it comes in from this process screen
                    {
                        if (rdm.IsPODataValid02(Data, AlertString, ref IFSPurchaseOrderDetailID, ref IFS_POAdjustedData, ref rMessage) == false)
                        {
                            rString.AddValuePair("Error", rMessage);
                            return rString.ToString();
                        }
                        Data = IFS_POAdjustedData;
                    }
                    #endregion
                    //try {
                    rdm.InsertReceiveDetail(ctx, rec, CurrentProcessID);
                    //}
                    //catch (Exception ex)
                    //{
                    //rMessage = "ERROR TEST";                    // ex.Message;
                    //    rString = new JsonString("Result", "NotSaved");
                    //    rString.AddValuePair("Error", rMessage);
                    //    return rString.ToString();
                    //}
                    // we have got a problem, there should be an ID now that we inserted it.
                    if (rec.ReceiveDetailID < 1)
                    {
                        rString.AddValuePair("Error", "Device data not saved");
                        return rString.ToString();
                    }
                    // we need to ignore the inial save. Now we have things saved, we need to do the next save as a PO_Initiate.
                    Process process = ctx.Processes.FirstOrDefault(x => x.ProcessID == CurrentProcessID);
                    if (process != null && process.IFSDirectiveType != null && process.IFSDirectiveType.Length > 0)
                    {
                        rec.ISFTransactionDirective = rdm.IFSDirective(ctx, process.IFSDirectiveType);
                    }
                    else
                    {
                        rec.ISFTransactionDirective = rdm.IFSDirective(ctx, "PO_Receipt");
                    }

                    //addedNew = true;
                    //// up till now, we may not know what these two ID fields are. 
                    //// Now that we just inserted a new on, we need to update these keys.
                    ReceiveHeaderID = rec.ReceiveHeaderID;
                    ReceiveDetailID = rec.ReceiveDetailID;
                    MakeModelString = rdm.MakeModelColourNickName(ctx, ReceiveDetailID);
                    PCLBString = rdm.GetProjectClientLocationBinString(ctx, ReceiveDetailID);
                    CompList = rdm.GetRequestProcessCompletionList(ctx, ReceiveDetailID);

                    string IsAuthorizationRequired = rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Authorization");
                    if (IsAuthorizationRequired.ToUpper() == "APPROVAL REQUIRED") { AuthorizationRequired = "Y"; }
                }
                #endregion

                ////////////////////////////////////////////////////////////////////////
                // we need to save the raw data to pull it for the bagtag.
                // Turned off code to run threaded.
                //if (Threaded.ToUpper() == "Y" && (addedNew == true || (CurProcess.Length > 6 && CurProcess.Substring(0, 7).ToUpper() == "KITTING") ||
                //                                 (CurProcess.Length > 7 && CurProcess.Substring(0, 8).ToUpper() == "SHIPPING")))
                //{
                //    IsThreaded = "Y";
                //    rdm.InsertReceiveDetail_BagtagPreSaveData(ctx, ReceiveDetailID, Data, ProjectID, CurrentProcessID);
                //}
                ////////////////////////////////////////////////////////////////////////
            }
            if (ReceiveDetailID < 1)
            {
                rString.AddValuePair("Error", "Device data not saved");
                return rString.ToString();
            }
            // make sure that if this is a screen with parts, they have picked all parts related to the unit.
            // Otherwise, there are problems and the unit is not saved.
            if (IsThisAPartsProcess(Data) == true && AreAllAssignedPartsAccountedFor(Data) == false)
            {
                rString.AddValuePair("Error", "Unused assigned parts. Data Not Saved");
                return rString.ToString();
            }



            timelog.SaveTimeLogWorkScreen_PreProcess(ReceiveDetailID, "PreWorkScreenSave2", CurrentProcessID, Data);
            // I needed a return value so I remed out the code to run this threaded.
            //if (Threaded.ToUpper() == "Y") { ThreadPool.QueueUserWorkItem(Report => RunThreadedSave(Data, IPAddress)); }    // This should go off and do it's thing giving control back to the browser. (Apear as if faster save.)
            //else { RunThreadedSave(Data, IPAddress); }

            string rMessagex = "";
            rMessagex = RunThreadedSave(ReceiveDetailID, Data, IPAddress, timelog);
            if (rMessagex.Length > 0)
            {
                rString.AddValuePair("Error", rMessagex);
                return rString.ToString();
            }


            rString = new JsonString("Result", "Saved");
            rString.AddValuePair("Cellbie", "");
            if (CurProcess.ToUpper() == "RECEIVECELLBIE")
            {
                Cellbie cellbie = new Cellbie(CurUserName);
                string ok = rdm.GetESNAttribute(ReceiveDetailID, "CellbieOK");
                string reason = rdm.GetESNAttribute(ReceiveDetailID, "CellbieReason");
                bool isOK = true;
                if (ok != "Yes") { isOK = false; }
                BrainDeviceTransaction info = cellbie.SendReceiveTransaction(ReceiveDetailID, ESN, isOK, reason, SysUtil.CellbieAPISimulate());
                if (info.Success == false)
                {
                    rString.RemoveValuePair("Cellbie", "");
                    rString.AddValuePair("Cellbie", info.CellbieError.Error);
                }
            }
            rString.AddValuePair("ReceiveHeaderID", ReceiveHeaderID.ToString());
            rString.AddValuePair("ReceiveDetailBulkID", "-1");
            rString.AddValuePair("ReceiveDetailID", ReceiveDetailID.ToString());
            //rString.AddValuePair("SUFD", rdm.GetSetUpFieldDef(ctx, ProjectID));
            //rString.AddValuePair("QTY", QTY);
            rString.AddValuePair("CompProcList", CompList);
            rString.AddValuePair("MMS", MakeModelString);
            rString.AddValuePair("THREADED", IsThreaded);
            rString.AddValuePair("AR", AuthorizationRequired);
            rString.AddValuePair("PCLB", PCLBString);                           //lblProjectClientLocationBinTitle
            timelog.StopTimer();
            timelog.SaveTimeLogWorkScreen(ReceiveDetailID, CurrentProcessID, Data);
            rString.AddValuePair("LogID", timelog.LogID.ToString());
            return rString.ToString();
        }




        private string RunThreadedSave(decimal ReceiveDetailID, string Data, string IPAddress, TimeLogManager timelog)
        {
            #region ThreadSave
            JsonString ScreenData = new JsonString(Data, true);
            decimal ReceiveHeaderID = ScreenData.GetValueDecimal("ReceiveHeaderID", -1);
            //decimal ReceiveDetailID = ScreenData.GetValueDecimal("ReceiveDetailID", -1);
            decimal ReceiveDetailBulkID = ScreenData.GetValueDecimal("ReceiveDetailBulkID", -1);
            decimal ClientLocationID = ScreenData.GetValueDecimal("ClientLocationID", -1);
            decimal ProjectID = ScreenData.GetValueDecimal("ProjectID", -1);
            decimal CurrentProcessID = ScreenData.GetValueDecimal("CurProcessID", -1);
            decimal ReceiveDetailAuthorizationLog = ScreenData.GetValueDecimal("DoAuthorize");
            decimal NextStepID = ScreenData.GetValueDecimal("NextStepID");
            int Qty = ScreenData.GetValueInt("QTY", 0);

            string CurUserName = ScreenData.GetValue("CurUserName");
            string ESN = ScreenData.GetValue("ESN");
            string Project = ScreenData.GetValue("Project");
            string CurProcess = ScreenData.GetValue("CurProcess");
            string RMA = ScreenData.GetValue("RMA");
            string ProjectTag = ScreenData.GetValue("PROJTAG");
            string ProjectSetup = ScreenData.GetValue("PROJSet");
            //string IPAddress = GetUserIPAddress();

            bool UpdateSKU = false;
            bool UpdateCondition = false;


            timelog.SaveTimeLogWorkScreen_PreProcess(ReceiveDetailID, "RTS-", CurrentProcessID, Data);

            ReceiveDetailManager rdm = new ReceiveDetailManager(CurUserName);
            using (clsLinqDataContext ctx = rdm.GetDataContext(CurUserName))
            {

                ReceiveDetail rec = null;
                string IFSRMANumber = ScreenData.GetTextData(ctx, "IFS RMA Number");

                if (RMA.Length == 0 && ProjectSetup.ToUpper().Contains("ZRMAZZAUTORZ")) { RMA = rdm.NextRMANumber(ctx, ClientLocationID, CurrentProcessID); }
                if (RMA.Length == 0 && ProjectSetup.ToUpper().Contains("ZRMAZZAUTOWZ")) { RMA = rdm.NextWorkOrderNumber(ctx); }
                rec = rdm.ReceiveDetail(ctx, ReceiveDetailID);
                if (rec == null) { return "Unable to save attributes."; }
                if (rec.ReceiveDetailID < 1) { return "Unable to save Attributes, Device master not found"; }


                rec.ProcessID = NextStepID;
                ////////////////////////////////////////////////////////////////////
                rec.QTYIntegrated = Qty;
                rec.MiscNote = "";

                if (rec.ReceiveDetailID > 0)
                {
                    if (rec.ClientLocationID < 1 && ClientLocationID > 0)
                    {
                        rec.ClientLocationID = ClientLocationID;
                        rdm.UpdateReceiveDetailClientLocation(ctx, rec.ReceiveDetailID, rec.ReceiveHeaderID, ClientLocationID);
                    }

                    // If the unit is shipped, we want to ignore this so an accounting transaction is not created. It will happen down below.
                    if (CurProcess.Length > 7 && CurProcess.Substring(0, 8).ToUpper() == "SHIPPING")  // This unit has been shipped
                    {
                        rec.ISFTransactionDirective = rdm.IFSDirective(ctx, "Ignore");
                    }

                    // We could now set the Location if required.
                    rec.IFSLocation = rdm.GetIFSLocation(rec.ReceiveDetailID, CurProcess, rec.ClientLocationID, rec.IFSLocation);
                    //rdm.UpdateReceiveDetail(ctx, rec, CurrentProcessID);
                }

                // up till now, we may not know what these two ID fields are. 
                ReceiveHeaderID = rec.ReceiveHeaderID;
                ReceiveDetailID = rec.ReceiveDetailID;

                #region Do Accounting and save the attributes.
                rdm.AddDetailProcessLog(ctx, ReceiveDetailID, CurrentProcessID);
                //rdm.AddBillingPoint(ctx, ReceiveDetailID, ClientLocationID, ProjectID, CurrentProcessID);
                // Process the Attribute data
                timelog.SaveTimeLogWorkScreen_PreProcess(ReceiveDetailID, "RTS-Save Detail", CurrentProcessID, Data);

                RecordDetailItemData_02(ctx, rdm, rec, ScreenData, CurUserName, false, CurrentProcessID, ref UpdateSKU, ref UpdateCondition);
                //////////////////////////////////////
                //rdm.RecordCosts(ctx, rec, CurProcess);

                //////// We no logner are doing prereceive.
                ////////    Turned back on Feb 13, 2019
                if (CurProcess.Length > 6 && CurProcess.Substring(0, 7).ToUpper() == "RECEIVE")
                {
                    rdm.OffsetPreReceive(ctx, ReceiveDetailID, ESN, IPAddress);
                }  // Bring in any pre receive data.
                ///////////////////////////////////////////////////////////////

                // if Current Process = "Track Public" then we need to do our PMI Prereceive.
                // There are attributes that do not get filled in via the receive process, so this needs to happen after the
                //       process where the attributes are filled in. (Tracking Info) NOT (Tracking Public)
                #endregion
                #region Tracking Info
                timelog.SaveTimeLogWorkScreen_PreProcess(ReceiveDetailID, "RTS-Tracking Info", CurrentProcessID, Data);

                if (CurProcess.Length > 12 && CurProcess.Substring(0, 13).ToUpper() == "TRACKING INFO")
                {
                    ReceiveDetailPreReceive rdpr = (from x in ctx.ReceiveDetailPreReceives
                                                    where x.ESN == ESN && x.ReceiveDetailID == ReceiveDetailID
                                                    select x).FirstOrDefault();
                    if (rdpr != null)
                    {
                        FileTransferLogManager ftlm = new FileTransferLogManager(CurUserName);
                        ftlm.AddNewTransferLogReceived_OnlyIfPreceived(ctx, rdpr);
                        //////////////////////rdm.OffsetPreReceive(ctx, ReceiveDetailID, ESN);
                    }
                }  // Bring in any pre receive data.



                #endregion
                #region KITTING
                timelog.SaveTimeLogWorkScreen_PreProcess(ReceiveDetailID, "RTS-Kitting", CurrentProcessID, Data);

                if (CurProcess.Length > 6 && CurProcess.Substring(0, 7).ToUpper() == "KITTING")  // 
                {
                    //rdm.SM.Kitted(ctx, rec.ReceiveDetailID, -1, rec.ClientLocationID, (decimal)rec.ProjectID, CurrentProcessID, CurUserName);
                    // Do Not Update in accordance to Jody's Instruction March 23, 2012 (phone call 10:31/Email)
                    //rdm.UpdateESNAttribute_Blank(ctx, nReceiveDetailID, "Bin");
                    //rdm.UpdateESNAttribute_Blank(ctx, nReceiveDetailID, "Location");
                    ////////////////////////////////////////////////////////////////////////////////////////////
                    bool isSwapped = rdm.SwapIMEI(ctx, ReceiveDetailID);
                }
                #endregion
                timelog.SaveTimeLogWorkScreen_PreProcess(ReceiveDetailID, "RTS-Shipping", CurrentProcessID, Data);
                #region SHIPPING
                if (CurProcess.Length > 7 && CurProcess.Substring(0, 8).ToUpper() == "SHIPPING")  // This unit has been shipped, we want the version changed from 000.  000 denotes GMP possesion, !000 denotes out the door.
                {
                    // As Per Jody's Instruction March 23, 2012 (phone call 10:31/Email)
                    rdm.UpdateESNAttribute_Blank(ctx, ReceiveDetailID, "Bin");
                    rdm.UpdateESNAttribute(ctx, ReceiveDetailID, "Location", "None");
                    ////////////////////////////////////////////////////////////////////////////////////////////
                    // As Per Jody's Instruction June 1, 2012 (Add SwapIMEI process to Shipping)
                    ////////////////////////////////////////////////////////////////////////////////////////////
                    //rdm.SM.OrderShipped(ctx, rec.ReceiveDetailID, -1, rec.ClientLocationID, (decimal)rec.ProjectID, CurrentProcessID, CurUserName);
                    FileTransferLogManager ftlm = new FileTransferLogManager(CurUserName);
                    ftlm.AddNewTransferLogShipped_OnlyIfReceived(ctx, rec);
                    rdm.RecordAssignedPartsShipped(ctx, rec.ReceiveDetailID);
                    IDC_LocationLogManager logman = new IDC_LocationLogManager(CurUserName);
                    logman.RecordShippedRecord(ctx, rec.ESN, rec.ReceiveDetailID, "", "", "");
                    AdvanceESNVersion01(ctx, ESN, CurUserName, "Work Shipping");
                    /////////////////////////////////////////////////////////////////////
                    // This needs to happen after the AdvanceESN because if the swap is done first
                    //      The Advance does not find it.
                    try { bool isSwapped = rdm.SwapIMEI(ctx, ReceiveDetailID); }
                    catch (Exception ex)
                    {
                    }
                    /////////////////////////////////////////////////////////////////////////////////////////
                    // Bring Back From MSC if it is a MSC Unit
                    rdm.CloneFromMSC(ctx, rec.ReceiveDetailID, (decimal)rec.ProjectID, CurrentProcessID);    // used for inhouse msc area
                    /////////////////////////////////////////////////////////////////////////////////////////
                }
                #endregion

                timelog.SaveTimeLogWorkScreen_PreProcess(ReceiveDetailID, "RTS-Request Parts", CurrentProcessID, Data);
                #region Request Parts
                if (CurProcess.Length > 12 && CurProcess.Substring(0, 13).ToUpper() == "REQUEST PARTS")  // This unit has been shipped, we want the version changed from 000.  000 denotes GMP possesion, !000 denotes out the door.
                {
                    SendEmail email = new SendEmail();
                    string rValue = email.SendPartsRequestedEmail(rec.ReceiveDetailID, GetUserIPAddress(), CurUserName);
                }
                #endregion

                timelog.SaveTimeLogWorkScreen_PreProcess(ReceiveDetailID, "RTS-Return Parts", CurrentProcessID, Data);
                #region Return Parts
                if (CurProcess.Length > 12 && CurProcess.Substring(0, 13).ToUpper() == "RETURN PARTS")  // This unit has been shipped, we want the version changed from 000.  000 denotes GMP possesion, !000 denotes out the door.
                {
                    SendEmail email = new SendEmail();
                    string rValue = email.SendPartsReturnedEmail(rec.ReceiveDetailID, GetUserIPAddress(), CurUserName);
                }
                #endregion

                timelog.SaveTimeLogWorkScreen_PreProcess(ReceiveDetailID, "RTS- _Repair", CurrentProcessID, Data);
                #region _Repair
                //if ((CurProcess.Length > 9 && CurProcess.Substring(0, 10).ToUpper() == "GMP REPAIR") || (CurProcess.Length > 6 && CurProcess.Substring(0, 7).ToUpper() == "_REPAIR"))
                if (CurProcess.Length > 6 && CurProcess.Substring(0, 7).ToUpper() == "_REPAIR")
                {
                    // Look to see if Authorization is required.
                    string IsAuthorizationRequired = rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Authorization");
                    if (IsAuthorizationRequired.ToUpper() == "APPROVAL REQUIRED")
                    {
                        //AuthorizationRequired = "Y";
                        rdm.UpdateESNAttribute_Blank(ctx, ReceiveDetailID, "Authorization");

                        ReceiveDetailAuthohrizationManager rdam = new ReceiveDetailAuthohrizationManager(CurUserName);
                        decimal ef = 0;
                        decimal ff = 0;
                        decimal hst = 0;
                        decimal total = 0;
                        string Note = "";
                        string Note1 = CleanData(rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Claim Action"));
                        string Note2 = CleanData(rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Claim Reason"));
                        string Note3 = CleanData(rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Claim Location"));

                        if (Note1.Length > 0) { Note = Note1 + "\n"; }
                        if (Note2.Length > 0) { Note += Note2 + "\n"; }
                        if (Note3.Length > 0) { Note += Note3; }

                        //Log.LogIt("Middle of Approval Required");

                        //string sef = CleanData(rdm.GetReceiveDetailItem_DataElement(ctx, nReceiveDetailID, "Est"));
                        string sef = CleanData(rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Estimate"));
                        string sff = CleanData(rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Freight"));
                        string shst = CleanData(rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "billing_hst"));
                        string stotal = CleanData(rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "billing_total"));

                        if (decimal.TryParse(sef, out ef) == false) { ef = 0; }
                        if (decimal.TryParse(sff, out ff) == false) { ff = 0; }
                        if (decimal.TryParse(shst, out hst) == false) { hst = 0; }
                        if (decimal.TryParse(stotal, out total) == false) { total = 0; }

                        rdam.AddNewRequest(ctx, ReceiveDetailID, ef, ff, hst, total, Note, "AUT");
                    }
                }

                if (ReceiveDetailAuthorizationLog > 0)         // we are saving on a unit that had an authorization scanned in.
                {
                    ReceiveDetailAuthohrizationManager rdam = new ReceiveDetailAuthohrizationManager(CurUserName);
                    rdam.Complete(ctx, ReceiveDetailAuthorizationLog, CurUserName);
                }
                #endregion
                #region Send out to MSC (Bridge Wireless)
                string message = "";
                timelog.SaveTimeLogWorkScreen_PreProcess(ReceiveDetailID, "RTS-MSC RH", CurrentProcessID, "length:" + CurProcess.Length.ToString() + " Current Process:" + CurProcess);
                if ((CurProcess.Length > 18 && CurProcess.Substring(0, 19).ToUpper() == "MSC REPAIR HANDLING") ||
                    (CurProcess.Length > 9 && CurProcess.Substring(0, 10).ToUpper() == "MSC REPAIR"))
                {
                    message = rdm.MoveToMSC(ReceiveDetailID);
                    timelog.SaveTimeLogWorkScreen_PreProcess(ReceiveDetailID, "RTS MRH", CurrentProcessID, message);
                    // Change the version 000 to 800
                }
                if (CurProcess.Length > 9 && CurProcess.Substring(0, 10).ToUpper() == "MSC RETURN")
                {
                    // Change the version back from 800 to 000
                    // This all happens in the SP:ProcessScanCode when opening the IMEI
                }
                #endregion

                timelog.SaveTimeLogWorkScreen_PreProcess(ReceiveDetailID, "RTS-Process Updates", CurrentProcessID, "length:" + CurProcess.Length.ToString() + " Current Process:" + CurProcess);
                #region Process accounting Updates
                rdm.ProcessIFSUpdates(ReceiveDetailID, CurProcess);
                #endregion

                timelog.SaveTimeLogWorkScreen_PreProcess(ReceiveDetailID, "RTS-RMA Receive", CurrentProcessID, "length:" + CurProcess.Length.ToString() + " Current Process:" + CurProcess);
                #region RMA RECEIVE
                if (CurProcess.Length > 10 && CurProcess.Substring(0, 11).ToUpper() == "RMA RECEIVE")
                {
                    rdm.ProcessRMAReturn(ReceiveDetailID);
                }
                #endregion
                #region  SwapIMEI
                /////////////////////////////////////////////////////////////////////
                // This has already happened in the Kitting and SHipping sections.
                //      Guy would like this to run for Save on Any process screen that carries the
                //      Replacement IMEI attribute.
                //      Dec 31, 2017
                try { bool isSwapped = rdm.SwapIMEI(ctx, ReceiveDetailID); }
                catch (Exception ex)
                {
                }

                #endregion
                return "";
            }
            #endregion

        }
        private void RecordDetailItemData_02(clsLinqDataContext ctx, ReceiveDetailManager bm, ReceiveDetail rd, JsonString SoucreData, string CurUserName, bool DoCalc, decimal CurrentProcessID, ref bool UpdateSKU, ref bool UpdateCondition)
        {
            try
            {
                List<TechAssignedKey> TechAssignedKeys = new List<TechAssignedKey>();
                var DetailItems = rd.ReceiveDetailItems;
                List<ReceiveDetailItem> DeleteList = new List<ReceiveDetailItem>();   // Old attributes that will be deleted  (mostly checkboxes, readial buttons and drop downs)
                MasterPartManager mpm = new MasterPartManager(CurUserName);
                List<List<string>> SourceDataAttributeLists = new List<List<string>>();
                SourceDataAttributeLists.Add(SoucreData.SourceDataNonCalcAttributes);
                SourceDataAttributeLists.Add(SoucreData.SourceDataCalcAttributes);

                #region  Reset the Checkboxes, radial buttons and dropdowns to null
                // We are only dealing with checkbox, readial button and dropdown attribute data.
                // The receivedetailitem records carries the optionid as a key pointing to the selected answer.
                // The user may have changed the answer, hence the old one needs to be deleted. Or recycled.
                // How I do this.
                // I get a list of all the questions and answers from the master tables. For the three types we are interested in.
                // I check that list against the source data sent in. If we are sent an answer for the question, we want to reset that question.
                // I place that question/answer record on my master QuestionID keep list. 
                // I now grab all the Question/Answer Records that have the same questionID found in the keep list.
                // It is time to go through all our DetailItems and see if we have any that match the above list. If we do, we want to keep track of it in case we need to delete it. It might get used again
                // 
                var dd1 = SoucreData.dbQuestionAnswerTable_TypeChecks;  // get all the question/Answer(option)s that we may need to reset.
                List<decimal> QuestionIDs = dd1.Where(x => SourceDataAttributeLists[0].Contains(x.jCoded.ToUpper()) == true).Select(y => y.QuestionID).ToList(); // get only those reset options that are in the source data.
                // get all of the dropdowns, chekcbox and radialbutton possible answers that have the same QuestionID as the options we found above. (Those found in the source data attributes.
                var dropdowns = dd1.Where(x => QuestionIDs.Contains(x.QuestionID) == true);
                foreach (var d in dropdowns)
                {
                    // get the found ReceiveDetailItem records and place them on a possible delete list.
                    var ris = DetailItems.Where(x => x.OptionID == d.OptionID);
                    foreach (ReceiveDetailItem ri in ris) { ri.Value = "0"; DeleteList.Add(ri); }
                }
                #endregion


                //JimErrorLogManager logManager = new JimErrorLogManager(CurUserName, "Att List:rd=" + rd.ReceiveDetailID.ToString());
                //logManager.IsActive = (System.Configuration.ConfigurationManager.AppSettings["WriteLog"].ToUpper() == "TRUE");
                //logManager.ReportMessage("Step 0 Attribute list save: ProcessID+" + CurrentProcessID.ToString());
                //bool UpdateSKU = false;
                //bool UpdateCondition = false;
                foreach (List<string> AttributeKeyList in SourceDataAttributeLists)
                {
                    foreach (string s in AttributeKeyList)
                    {
                        var o = SoucreData.dbQuestionAnswerTable.FirstOrDefault(x => x.jCoded.ToUpper() == s.ToUpper());
                        if (o != null)
                        {
                            if ((o.UseOptionValue == "1" && SoucreData.GetValue(s) == "1") || o.UseOptionValue != "1")
                            {
                                ReceiveDetailItem item = DetailItems.FirstOrDefault(x => x.OptionID == o.OptionID);

                                //if (o.Name.ToUpper() == "DEALER WAYBILL")
                                //{
                                //    string wb = SoucreData.GetValue(s);
                                //    if (wb.Length > 22)
                                //    {
                                //        wb = wb.Substring(11);
                                //        wb = wb.Substring(0, wb.Length - 10);
                                //        SoucreData.PutValue(s, wb);
                                //    }
                                //}

                                #region Inventory Parts
                                ReceiveDetailPartsUsage pu = null;
                                ReceiveDetailPartsUsage pur = null;
                                // If our Attribute is a part... we need to remove that part from inventory.
                                if (o.Name.Length > 5 && o.Name.Substring(0, 5).ToUpper() == "PART ")
                                {
                                    //logManager.ReportMessage("Step 0 Part attribute found:" + s + ":" + o.Name);


                                    decimal PartNumberID = -1;
                                    decimal IFSLocationID = -1;
                                    string value = SoucreData.GetValue(s);

                                    #region ":id="
                                    bool GenerateRequest = false;
                                    if (value.IndexOf(":id=") > -1)
                                    {
                                        //logManager.ReportMessage("Check Part, we have (:id=)  :" + value);
                                        #region Check the old one to see if there was one there originally,
                                        // 
                                        if (item != null)
                                        {

                                            ReceiveDetailPartsUsage rpu = item.ReceiveDetailPartsUsages.FirstOrDefault(xx => xx.StatusID == 1); // active transaction
                                            if (rpu != null)
                                            {

                                                //logManager.ReportMessage("We have an older one that needs to be reversed:" + value);
                                                rpu.StatusID = 2; rpu.LastUpdateUser = CurUserName; rpu.LastUpdateDate = DateTime.Now;
                                                if (rpu.PartNumberBucketInventoryPlacementID != null) { pur = mpm.ReverseTransaction(ctx, (decimal)rpu.PartNumberBucketInventoryPlacementID, rd.ReceiveDetailID, item.ReceiveDetailItemID, rpu.MasterPartsLinkTableID); }
                                            }
                                        }
                                        #endregion
                                        #region Record the current one
                                        // create the next one.
                                        string y = value.Substring(value.IndexOf(":id=") + 4);
                                        string x = "";
                                        if (y.IndexOf(":") > -1) // We have picked right from inventory without a request.
                                        {
                                            string[] key = y.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries); x = key[0];
                                            if (key.Count() == 2) { if (decimal.TryParse(key[1], out IFSLocationID) == true) { GenerateRequest = true; } }
                                        }
                                        else { x = y; }
                                        if (decimal.TryParse(x, out PartNumberID) == true)
                                        {
                                            if (GenerateRequest == true)
                                            {
                                                IFS_InventoryPartsManager ipm = new IFS_InventoryPartsManager(CurUserName);
                                                ipm.ReadyPartForConsumption(rd.ReceiveDetailID, PartNumberID, IFSLocationID);
                                            }
                                            //logManager.ReportMessage("Adding new part:" + value);
                                            value = value.Substring(0, value.IndexOf(":id="));
                                            SoucreData.PutValue(s, value);
                                            if (item == null)
                                            {
                                                //logManager.ReportMessage("No existing Item, queueing:" + value);
                                                TechAssignedKey key = new TechAssignedKey();
                                                key.MasterPartsLinkTableID = PartNumberID;
                                                key.ReceiveDetailID = rd.ReceiveDetailID;
                                                key.OptionID = o.OptionID;
                                                key.ReceiveDetailItemID = -1;
                                                TechAssignedKeys.Add(key);
                                                //pu = mpm.RemoveFromInventory(ctx, PartNumberID, 1, mpm.GetType("Used by tech"), "", rd.ReceiveDetailID, -1);
                                            }
                                            else
                                            {
                                                //logManager.ReportMessage("Found existing Item, updating:" + value);
                                                // Get Tech assigned part location. for Site Location
                                                pu = mpm.RemoveFromInventory(ctx, PartNumberID, rd.IFSLocation, 1, mpm.GetType("Used by tech"), "", rd.ReceiveDetailID, item.ReceiveDetailItemID);
                                            }
                                        }
                                        #endregion
                                    }
                                    #endregion
                                    #region XIDX
                                    else if (value.IndexOf("xidX") > -1)   // The part numbers can also be coded as "xidX" Case Sensitive. This is to allow for scanning of the partnumber in.
                                    {
                                        //logManager.ReportMessage("Check Part, we have (xidX=)  :" + value);
                                        #region Check the old one to see if there was one there originally,
                                        if (item != null)
                                        {
                                            //logManager.ReportMessage("We have an older one that needs to be reversed:" + value);
                                            ReceiveDetailPartsUsage rpu = item.ReceiveDetailPartsUsages.FirstOrDefault(xx => xx.StatusID == 1); // active transaction
                                            if (rpu != null)
                                            {
                                                rpu.StatusID = 2; rpu.LastUpdateUser = CurUserName; rpu.LastUpdateDate = DateTime.Now;
                                                if (rpu.PartNumberBucketInventoryPlacementID != null) { pur = mpm.ReverseTransaction(ctx, (decimal)rpu.PartNumberBucketInventoryPlacementID, rd.ReceiveDetailID, item.ReceiveDetailItemID, rpu.MasterPartsLinkTableID); }
                                            }
                                        }
                                        #endregion
                                        #region Record the current one
                                        // If there was, we need to reverse that one and create the next one.
                                        //string x = value.Substring(value.IndexOf("xidX") + 4);
                                        string y = value.Substring(value.IndexOf("xidX") + 4);
                                        string x = "";
                                        if (y.IndexOf(":") > -1) // We have picked right from inventory without a request.
                                        {
                                            string[] key = y.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries); x = key[0];
                                            if (key.Count() == 2) { if (decimal.TryParse(key[1], out IFSLocationID) == true) { GenerateRequest = true; } }
                                        }
                                        else { x = y; }




                                        if (decimal.TryParse(x, out PartNumberID) == true)
                                        {
                                            if (GenerateRequest == true)
                                            {
                                                IFS_InventoryPartsManager ipm = new IFS_InventoryPartsManager(CurUserName);
                                                ipm.ReadyPartForConsumption(rd.ReceiveDetailID, PartNumberID, IFSLocationID);
                                            }
                                            //logManager.ReportMessage("Adding new part:" + value);
                                            value = value.Substring(0, value.IndexOf("xidX"));
                                            SoucreData.PutValue(s, value);
                                            if (item == null)
                                            {
                                                //logManager.ReportMessage("No existing Item, queueing:" + value);
                                                TechAssignedKey key = new TechAssignedKey();
                                                key.MasterPartsLinkTableID = PartNumberID;
                                                key.ReceiveDetailID = rd.ReceiveDetailID;
                                                key.OptionID = o.OptionID;
                                                key.ReceiveDetailItemID = -1;
                                                TechAssignedKeys.Add(key);
                                                //pu = mpm.RemoveFromInventory(ctx, PartNumberID, 1, mpm.GetType("Used by tech"), "", rd.ReceiveDetailID, -1);
                                            }
                                            else
                                            {
                                                //logManager.ReportMessage("Found existing Item, updating:" + value);
                                                pu = mpm.RemoveFromInventory(ctx, PartNumberID, rd.IFSLocation, 1, mpm.GetType("Used by tech"), "", rd.ReceiveDetailID, item.ReceiveDetailItemID);
                                            }
                                        }
                                        #endregion
                                    }
                                    #endregion
                                    else
                                    #region Seeif any parts have been removed.
                                    {
                                        //logManager.ReportMessage("Check Part" + value);
                                        #region Check Part, Just a number:
                                        if (item != null && value.Length == 0)
                                        {
                                            //logManager.ReportMessage("Empty Part Number:");
                                            // Look to see if we have a prior part number here, if so, we need to remove it.
                                            ReceiveDetailPartsUsage rpu = item.ReceiveDetailPartsUsages.FirstOrDefault(xx => xx.StatusID == 1); // active transaction
                                            if (rpu != null)
                                            {
                                                //logManager.ReportMessage("Step 10:");
                                                rpu.StatusID = 2;
                                                rpu.LastUpdateDate = DateTime.Now;
                                                rpu.LastUpdateDate = DateTime.Now;
                                                if (rpu.PartNumberBucketInventoryPlacementID != null)
                                                {
                                                    pur = mpm.ReverseTransaction(ctx, (decimal)rpu.PartNumberBucketInventoryPlacementID, rd.ReceiveDetailID, item.ReceiveDetailItemID, rpu.MasterPartsLinkTableID);
                                                }
                                            }
                                            //// if the value is blank. Find possible posted inventory removal and reverse that.
                                        }
                                        else
                                        {
                                            // Nothing to do here
                                        }
                                        #endregion
                                    }
                                    #endregion
                                }
                                #endregion
                                //logManager.ReportMessage("Step 11:");
                                #region Update/Add new Item to database
                                if (item != null)
                                {
                                    if (o.Type.ToUpper() == "CALC") { item.Value = CalcuateValue(rd.ReceiveDetailID, o.OptionValue, CurUserName); }
                                    else { item.Value = o.UseOptionValue == "1" ? "1" : SoucreData.GetValue(s); }
                                    item.LastUpdateDate = DateTime.Now;
                                    item.LastUpdateUser = CurUserName;
                                    //We no longer need to do this transaction because the original one takes on a status = 2 (deleted)
                                    //if (pur != null) { pur.ReceiveDetailID = rd.ReceiveDetailID; item.ReceiveDetailPartsUsages.Add(pur); }
                                    if (pu != null) { pu.ReceiveDetailID = rd.ReceiveDetailID; item.ReceiveDetailPartsUsages.Add(pu); }
                                    ReceiveDetailItem i = DeleteList.FirstOrDefault(x => x.ReceiveDetailItemID == item.ReceiveDetailItemID);
                                    if (i != null) { DeleteList.Remove(i); }   // Just brought back an old record to life, we no longer want it deleted.
                                }
                                else
                                {
                                    item = new ReceiveDetailItem();
                                    if (o.Type.ToUpper() == "CALC") { item.Value = CalcuateValue(rd.ReceiveDetailID, o.OptionValue, CurUserName); }
                                    else { item.Value = o.UseOptionValue == "1" ? "1" : SoucreData.GetValue(s); }
                                    item.OptionID = o.OptionID;
                                    item.Version = 0;
                                    item.ReceiveDate = rd.ReceiveDate;
                                    item.ReceiveHeaderID = rd.ReceiveHeaderID;
                                    item.ReceiveDetailID = rd.ReceiveDetailID;
                                    item.LastUpdateUser = CurUserName;
                                    item.LastUpdateDate = DateTime.Now;
                                    item.CreateUser = CurUserName;
                                    //We no longer need to do this transaction because the original one takes on a status = 2 (deleted)
                                    //if (pur != null) { pur.ReceiveDetailID = rd.ReceiveDetailID; item.ReceiveDetailPartsUsages.Add(pur); }
                                    if (pu != null) { pu.ReceiveDetailID = rd.ReceiveDetailID; item.ReceiveDetailPartsUsages.Add(pu); }
                                    DetailItems.Add(item);
                                }
                                #endregion
                                //logManager.ReportMessage("Step 12:");
                            }
                            #region Unit Field Updated
                            if (o.Name.ToUpper() == "CARRIER") { rd.CarrierID = o.OptionID; rd.Carrier = o.OptionValue; UpdateSKU = true; }
                            if (o.Name.ToUpper() == "MANUFACTURER") { rd.ManufacturerID = o.OptionID; rd.Manufacturer = o.OptionValue; UpdateSKU = true; }
                            if (o.Name.ToUpper() == "MODEL") { rd.ModelID = o.OptionID; rd.Model = o.OptionValue; UpdateSKU = true; }
                            if (o.Name.ToUpper() == "COLOUR") { rd.ColourID = o.OptionID; rd.Colour = o.OptionValue; UpdateSKU = true; }
                            if (o.Name.ToUpper() == "GRADE") { rd.GradeID = o.OptionID; rd.Grade = o.OptionValue; }
                            if (o.Name.ToUpper() == "IFS RMA Number") { rd.RMANumber = SoucreData.GetValue(s); }
                            if (o.Name.ToUpper() == "IFS Conditions") { rd.IFSCondition = o.Abbr; }
                            //if (o.Name.ToUpper() == "IFS CONDITIONS") { rd.IFSCondition = o.Name; }
                            #endregion
                        }
                    }
                    ctx.ReceiveDetailItems.DeleteAllOnSubmit(DeleteList);
                    ctx.SubmitChanges();
                    #region Record the usage of the parts.
                    foreach (TechAssignedKey key in TechAssignedKeys)
                    {
                        ReceiveDetailItem item = ctx.ReceiveDetailItems.FirstOrDefault(x => x.ReceiveDetailID == rd.ReceiveDetailID && x.OptionID == key.OptionID);
                        if (item != null)
                        {
                            mpm.RemoveFromInventory(ctx, key.MasterPartsLinkTableID, rd.IFSLocation, 1, mpm.GetType("Used by tech"), "", key.ReceiveDetailID, item.ReceiveDetailItemID);
                            ctx.SubmitChanges();
                        }
                    }
                    TechAssignedKeys.Clear();
                    #endregion
                }
                return;
            }
            catch (Exception ex)
            {
                JimErrorLogManager logManagerz = new JimErrorLogManager(CurUserName, "Error");
                logManagerz.ReportMessage("ERROR:" + ex.Message + ":" + ex.StackTrace);
            }
        }
        private string RunThreadedXBINXSave(clsLinqDataContext ctx, ReceiveDetailManager rdm, ReceiveDetail rec, string Data, string IPAddress)
        {
            JsonString rString = new JsonString("Result", "Saved");

            if (rec == null)
            {
                rString = new JsonString("Result", "Error");
                return rString.ToString();
            }

            #region ThreadSave
            JsonString ScreenData = new JsonString(Data, true);
            decimal ReceiveHeaderID = ScreenData.GetValueDecimal("ReceiveHeaderID", -1);
            decimal ReceiveDetailID = ScreenData.GetValueDecimal("ReceiveDetailID", -1);
            decimal ReceiveDetailBulkID = ScreenData.GetValueDecimal("ReceiveDetailBulkID", -1);
            decimal ClientLocationID = ScreenData.GetValueDecimal("ClientLocationID", -1);
            decimal ProjectID = ScreenData.GetValueDecimal("ProjectID", -1);
            decimal CurrentProcessID = ScreenData.GetValueDecimal("CurProcessID", -1);
            decimal ReceiveDetailAuthorizationLog = ScreenData.GetValueDecimal("DoAuthorize");
            decimal NextStepID = ScreenData.GetValueDecimal("NextStepID");
            int Qty = ScreenData.GetValueInt("QTY", 0);

            string CurUserName = ScreenData.GetValue("CurUserName");
            string ESN = ScreenData.GetValue("ESN");
            string Project = ScreenData.GetValue("Project");
            string CurProcess = ScreenData.GetValue("CurProcess");
            string RMA = ScreenData.GetValue("RMA");
            string ProjectTag = ScreenData.GetValue("PROJTAG");
            string ProjectSetup = ScreenData.GetValue("PROJSet");
            //string IPAddress = GetUserIPAddress();

            bool UpdateSKU = false;
            bool UpdateCondition = false;

            rec.ProcessID = NextStepID;

            ////////////////////////////////////////////////////////////////////
            rec.QTYIntegrated = Qty;
            rec.MiscNote = "";

            if (rec.ReceiveDetailID > 0)
            {
                if (rec.ClientLocationID < 1 && ClientLocationID > 0)
                {
                    rec.ClientLocationID = ClientLocationID;
                    rdm.UpdateReceiveDetailClientLocation(ctx, rec.ReceiveDetailID, rec.ReceiveHeaderID, ClientLocationID);
                }

                // If the unit is shipped, we want to ignore this so an IFS transaction is not created. It will happen down below.
                if (CurProcess.Length > 7 && CurProcess.Substring(0, 8).ToUpper() == "SHIPPING")  // This unit has been shipped
                {
                    rec.ISFTransactionDirective = rdm.IFSDirective(ctx, "Ignore");
                }
                //rdm.UpdateReceiveDetail(ctx, rec, CurrentProcessID);
            }
            // up till now, we may not know what these two ID fields are. 
            ReceiveHeaderID = rec.ReceiveHeaderID;
            ReceiveDetailID = rec.ReceiveDetailID;

            #region Do Accounting and save the attributes.
            rdm.AddDetailProcessLog(ctx, ReceiveDetailID, CurrentProcessID);
            //rdm.AddBillingPoint(ctx, ReceiveDetailID, ClientLocationID, ProjectID, CurrentProcessID);
            // Process the Attribute data
            RecordDetailItemData_02(ctx, rdm, rec, ScreenData, CurUserName, false, CurrentProcessID, ref UpdateSKU, ref UpdateCondition);

            // if Current Process = "Track Public" then we need to do our PMI Prereceive.
            // There are attributes that do not get filled in via the receive process, so this needs to happen after the
            //       process where the attributes are filled in. (Tracking Info) NOT (Tracking Public)
            if (CurProcess.Length > 12 && CurProcess.Substring(0, 13).ToUpper() == "TRACKING INFO")
            {
                ReceiveDetailPreReceive rdpr = (from x in ctx.ReceiveDetailPreReceives
                                                where x.ESN == ESN && x.ReceiveDetailID == ReceiveDetailID
                                                select x).FirstOrDefault();
                if (rdpr != null)
                {
                    FileTransferLogManager ftlm = new FileTransferLogManager(CurUserName);
                    ftlm.AddNewTransferLogReceived_OnlyIfPreceived(ctx, rdpr);
                    //////////////////////rdm.OffsetPreReceive(ctx, ReceiveDetailID, ESN);
                }
            }  // Bring in any pre receive data.



            #endregion
            #region KITTING
            if (CurProcess.Length > 6 && CurProcess.Substring(0, 7).ToUpper() == "KITTING")  // 
            {
                //rdm.SM.Kitted(ctx, rec.ReceiveDetailID, -1, rec.ClientLocationID, (decimal)rec.ProjectID, CurrentProcessID, CurUserName);
                // Do Not Update in accordance to Jody's Instruction March 23, 2012 (phone call 10:31/Email)
                //rdm.UpdateESNAttribute_Blank(ctx, nReceiveDetailID, "Bin");
                //rdm.UpdateESNAttribute_Blank(ctx, nReceiveDetailID, "Location");
                ////////////////////////////////////////////////////////////////////////////////////////////
                bool isSwapped = rdm.SwapIMEI(ctx, ReceiveDetailID);
            }
            #endregion




            #region SHIPPING
            if (CurProcess.Length > 7 && CurProcess.Substring(0, 8).ToUpper() == "SHIPPING")  // This unit has been shipped, we want the version changed from 000.  000 denotes GMP possesion, !000 denotes out the door.
            {
                // As Per Jody's Instruction March 23, 2012 (phone call 10:31/Email)
                rdm.UpdateESNAttribute_Blank(ctx, ReceiveDetailID, "Bin");
                rdm.UpdateESNAttribute(ctx, ReceiveDetailID, "Location", "None");
                ////////////////////////////////////////////////////////////////////////////////////////////
                // As Per Jody's Instruction June 1, 2012 (Add SwapIMEI process to Shipping)
                ////////////////////////////////////////////////////////////////////////////////////////////
                //rdm.SM.OrderShipped(ctx, rec.ReceiveDetailID, -1, rec.ClientLocationID, (decimal)rec.ProjectID, CurrentProcessID, CurUserName);
                FileTransferLogManager ftlm = new FileTransferLogManager(CurUserName);
                ftlm.AddNewTransferLogShipped_OnlyIfReceived(ctx, rec);
                rdm.RecordAssignedPartsShipped(ctx, rec.ReceiveDetailID);
                IDC_LocationLogManager logman = new IDC_LocationLogManager(CurUserName);
                logman.RecordShippedRecord(ctx, rec.ESN, rec.ReceiveDetailID, "", "", "");
                AdvanceESNVersion01(ctx, ESN, CurUserName, "Work Shipping");
                /////////////////////////////////////////////////////////////////////
                // This needs to happen after the AdvanceESN because if the swap is done first
                //      The Advance does not find it.
                try { bool isSwapped = rdm.SwapIMEI(ctx, ReceiveDetailID); }
                catch (Exception ex)
                {
                }
                /////////////////////////////////////////////////////////////////////////////////////////
                // Bring Back From MSC if it is a MSC Unit
                rdm.CloneFromMSC(ctx, rec.ReceiveDetailID, (decimal)rec.ProjectID, CurrentProcessID);
                /////////////////////////////////////////////////////////////////////////////////////////
            }
            #endregion
            #region Request Parts
            if (CurProcess.Length > 12 && CurProcess.Substring(0, 13).ToUpper() == "REQUEST PARTS")  // This unit has been shipped, we want the version changed from 000.  000 denotes GMP possesion, !000 denotes out the door.
            {
                SendEmail email = new SendEmail();
                string rValue = email.SendPartsRequestedEmail(rec.ReceiveDetailID, GetUserIPAddress(), CurUserName);
            }
            #endregion
            #region Return Parts
            if (CurProcess.Length > 12 && CurProcess.Substring(0, 13).ToUpper() == "RETURN PARTS")  // This unit has been shipped, we want the version changed from 000.  000 denotes GMP possesion, !000 denotes out the door.
            {
                SendEmail email = new SendEmail();
                string rValue = email.SendPartsReturnedEmail(rec.ReceiveDetailID, GetUserIPAddress(), CurUserName);
            }
            #endregion
            #region GMPRepair
            //if (CurProcess.Length > 9 && CurProcess.Substring(0, 10).ToUpper() == "GMP REPAIR")
            if (CurProcess.Length > 6 && CurProcess.Substring(0, 7).ToUpper() == "_REPAIR")
            {
                // Look to see if Authorization is required.
                string IsAuthorizationRequired = rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Authorization");
                if (IsAuthorizationRequired.ToUpper() == "APPROVAL REQUIRED")
                {
                    //AuthorizationRequired = "Y";
                    rdm.UpdateESNAttribute_Blank(ctx, ReceiveDetailID, "Authorization");

                    ReceiveDetailAuthohrizationManager rdam = new ReceiveDetailAuthohrizationManager(CurUserName);
                    decimal ef = 0;
                    decimal ff = 0;
                    decimal hst = 0;
                    decimal total = 0;
                    string Note = "";
                    string Note1 = CleanData(rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Claim Action"));
                    string Note2 = CleanData(rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Claim Reason"));
                    string Note3 = CleanData(rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Claim Location"));

                    if (Note1.Length > 0) { Note = Note1 + "\n"; }
                    if (Note2.Length > 0) { Note += Note2 + "\n"; }
                    if (Note3.Length > 0) { Note += Note3; }

                    //Log.LogIt("Middle of Approval Required");

                    //string sef = CleanData(rdm.GetReceiveDetailItem_DataElement(ctx, nReceiveDetailID, "Est"));
                    string sef = CleanData(rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Estimate"));
                    string sff = CleanData(rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Freight"));
                    string shst = CleanData(rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "billing_hst"));
                    string stotal = CleanData(rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "billing_total"));

                    if (decimal.TryParse(sef, out ef) == false) { ef = 0; }
                    if (decimal.TryParse(sff, out ff) == false) { ff = 0; }
                    if (decimal.TryParse(shst, out hst) == false) { hst = 0; }
                    if (decimal.TryParse(stotal, out total) == false) { total = 0; }

                    rdam.AddNewRequest(ctx, ReceiveDetailID, ef, ff, hst, total, Note, "AUT");
                }
            }

            if (ReceiveDetailAuthorizationLog > 0)         // we are saving on a unit that had an authorization scanned in.
            {
                ReceiveDetailAuthohrizationManager rdam = new ReceiveDetailAuthohrizationManager(CurUserName);
                rdam.Complete(ctx, ReceiveDetailAuthorizationLog, CurUserName);
            }
            #endregion
            #region Process IFS Updates
            rdm.ProcessIFSUpdates(ReceiveDetailID, CurProcess);
            #endregion
            #region RMA RECEIVE
            if (CurProcess.Length > 10 && CurProcess.Substring(0, 11).ToUpper() == "RMA RECEIVE") { rdm.ProcessRMAReturn(ReceiveDetailID); }
            #endregion

            #region Send out to MSC (Bridge Wireless)
            string message = "";
            if (CurProcess.Length > 18 && CurProcess.Substring(0, 19).ToUpper() == "MSC REPAIR HANDLING")
            {
                message = rdm.MoveToMSC(ReceiveHeaderID);
                // Change the version 000 to 800
            }
            if (CurProcess.Length > 9 && CurProcess.Substring(0, 10).ToUpper() == "MSC RETURN")
            {
                // Change the version back from 800 to 000
            }
            #endregion
            //}
            #endregion
            rString = new JsonString("Result", "Saved");
            rString.AddValuePair("ReceiveHeaderID", ReceiveHeaderID.ToString());
            rString.AddValuePair("ReceiveDetailBulkID", "-1");
            rString.AddValuePair("ReceiveDetailID", ReceiveDetailID.ToString());
            rString.AddValuePair("rMessage", message);

            ////rString.AddValuePair("SUFD", rdm.GetSetUpFieldDef(ctx, ProjectID));
            ////rString.AddValuePair("QTY", QTY);
            //rString.AddValuePair("CompProcList", CompList);
            //rString.AddValuePair("MMS", MakeModelString);
            //rString.AddValuePair("THREADED", IsThreaded);
            //rString.AddValuePair("AR", AuthorizationRequired);
            //rString.AddValuePair("PCLB", PCLBString);                           //lblProjectClientLocationBinTitle
            ////timelog.StopTimer();
            ////timelog.SaveTimeLogWorkScreen(ReceiveDetailID, CurrentProcessID, Data);
            return rString.ToString();
        }
        private string RunThreadedXBINXSaveRestrictedProcesses(clsLinqDataContext ctx, ReceiveDetailManager rdm, ReceiveDetail rec, string Data, string IPAddress, JimErrorLogManager logManagerz)
        {
            JsonString rString = new JsonString("Result", "Saved");
            if (rec == null) { rString = new JsonString("Result", "Error"); return rString.ToString(); }

            #region ThreadSave
            JsonString ScreenData = new JsonString(Data, true);
            #region Extract Data
            decimal ReceiveHeaderID = ScreenData.GetValueDecimal("ReceiveHeaderID", -1);
            decimal ReceiveDetailID = ScreenData.GetValueDecimal("ReceiveDetailID", -1);
            decimal ReceiveDetailBulkID = ScreenData.GetValueDecimal("ReceiveDetailBulkID", -1);
            decimal ClientLocationID = ScreenData.GetValueDecimal("ClientLocationID", -1);
            decimal ProjectID = ScreenData.GetValueDecimal("ProjectID", -1);
            decimal CurrentProcessID = ScreenData.GetValueDecimal("CurProcessID", -1);
            decimal ReceiveDetailAuthorizationLog = ScreenData.GetValueDecimal("DoAuthorize");
            decimal NextStepID = ScreenData.GetValueDecimal("NextStepID");
            int Qty = ScreenData.GetValueInt("QTY", 0);

            string CurUserName = ScreenData.GetValue("CurUserName");
            string ESN = ScreenData.GetValue("ESN");
            string Project = ScreenData.GetValue("Project");
            string CurProcess = ScreenData.GetValue("CurProcess");
            string RMA = ScreenData.GetValue("RMA");
            string ProjectTag = ScreenData.GetValue("PROJTAG");
            string ProjectSetup = ScreenData.GetValue("PROJSet");

            //logManagerz.ReportMessage("CurUserName:" + CurUserName);
            //logManagerz.ReportMessage("ESN:" + ESN);
            //logManagerz.ReportMessage("ReceiveDetailID:" + ReceiveDetailID.ToString());
            //logManagerz.ReportMessage("RMA:" + RMA);
            //logManagerz.ReportMessage("Project:" + Project);
            //logManagerz.ReportMessage("ProjectID:" + ProjectID.ToString());
            //logManagerz.ReportMessage("CurProcess:" + CurProcess);
            //logManagerz.ReportMessage("CurrentProcessID:" + CurrentProcessID.ToString());
            //logManagerz.ReportMessage("ProjectTag:" + ProjectTag);
            //logManagerz.ReportMessage("ProjectSetup:" + ProjectSetup);
            #endregion
            //string IPAddress = GetUserIPAddress();

            bool UpdateSKU = false;
            bool UpdateCondition = false;

            rec.ProcessID = NextStepID;

            ////////////////////////////////////////////////////////////////////
            rec.QTYIntegrated = Qty;
            rec.MiscNote = "";

            if (rec.ReceiveDetailID > 0)
            {
                if (rec.ClientLocationID < 1 && ClientLocationID > 0)
                {
                    rec.ClientLocationID = ClientLocationID;
                    rdm.UpdateReceiveDetailClientLocation(ctx, rec.ReceiveDetailID, rec.ReceiveHeaderID, ClientLocationID);
                }

                // If the unit is shipped, we want to ignore this so an IFS transaction is not created. It will happen down below.
                if (CurProcess.Length > 7 && CurProcess.Substring(0, 8).ToUpper() == "SHIPPING")
                {
                    rec.ISFTransactionDirective = rdm.IFSDirective(ctx, "Ignore");
                    //// turned off
                    //// rdm.UpdateReceiveDetail(ctx, rec, CurrentProcessID);
                }
                // up till now, we may not know what these two ID fields are. 
                ReceiveHeaderID = rec.ReceiveHeaderID;
                ReceiveDetailID = rec.ReceiveDetailID;

                #region Do Accounting and save the attributes.
                rdm.AddDetailProcessLog(ctx, ReceiveDetailID, CurrentProcessID);
                // Process the Attribute data
                #region Save the attributes
                try
                {
                    //RecordDetailItemData_02(ctx, rdm, rec, ScreenData, CurUserName, false, CurrentProcessID, ref UpdateSKU, ref UpdateCondition);
                    RecordDetailItemData_02RestrictedProcesses(ctx, rdm, rec, ScreenData, CurUserName, false, CurrentProcessID, ref UpdateSKU, ref UpdateCondition);
                }
                catch (Exception ex)
                {
                    logManagerz.ReportMessage("Error:" + ex.Message + ":" + ex.StackTrace);
                }
                #endregion
                #endregion
                #region KITTING -- Turned off here
                //// Turned off, No Swapping IMEI via XBINX
                //if (CurProcess.Length > 6 && CurProcess.Substring(0, 7).ToUpper() == "KITTING") { bool isSwapped = rdm.SwapIMEI(ctx, ReceiveDetailID); }
                #endregion
                #region SHIPPING
                if (CurProcess.Length > 7 && CurProcess.Substring(0, 8).ToUpper() == "SHIPPING")  // This unit has been shipped, we want the version changed from 000.  000 denotes GMP possesion, !000 denotes out the door.
                {
                    // As Per Jody's Instruction March 23, 2012 (phone call 10:31/Email)
                    rdm.UpdateESNAttribute_Blank(ctx, ReceiveDetailID, "Bin");
                    rdm.UpdateESNAttribute(ctx, ReceiveDetailID, "Location", "None");
                    ////////////////////////////////////////////////////////////////////////////////////////////
                    // As Per Jody's Instruction June 1, 2012 (Add SwapIMEI process to Shipping)
                    ////////////////////////////////////////////////////////////////////////////////////////////
                    //rdm.SM.OrderShipped(ctx, rec.ReceiveDetailID, -1, rec.ClientLocationID, (decimal)rec.ProjectID, CurrentProcessID, CurUserName);
                    //// Turned off 
                    //FileTransferLogManager ftlm = new FileTransferLogManager(CurUserName);
                    //ftlm.AddNewTransferLogShipped_OnlyIfReceived(ctx, rec);
                    rdm.RecordAssignedPartsShipped(ctx, rec.ReceiveDetailID);
                    //// Turned off
                    //IDC_LocationLogManager logman = new IDC_LocationLogManager(CurUserName);
                    //logman.RecordShippedRecord(ctx, rec.ESN, rec.ReceiveDetailID, "", "", "");
                    AdvanceESNVersion01(ctx, ESN, CurUserName, "Work Shipping");
                    /////////////////////////////////////////////////////////////////////
                    // This needs to happen after the AdvanceESN because if the swap is done first
                    //      The Advance does not find it.
                    //try { bool isSwapped = rdm.SwapIMEI(ctx, ReceiveDetailID); }
                    //catch (Exception ex)
                    //{
                    //}
                    /////////////////////////////////////////////////////////////////////////////////////////
                    // Bring Back From MSC if it is a MSC Unit
                    //// Turned off
                    //rdm.CloneFromMSC(ctx, rec.ReceiveDetailID, (decimal)rec.ProjectID, CurrentProcessID);
                    ///////////////////////////////////////////////////////////////////////////////////////////
                }
                #endregion
                #region Parts   --- Turned off here
                #region Request Parts
                //// Turned Off
                //if (CurProcess.Length > 12 && CurProcess.Substring(0, 13).ToUpper() == "REQUEST PARTS")  // This unit has been shipped, we want the version changed from 000.  000 denotes GMP possesion, !000 denotes out the door.
                //{
                //    SendEmail email = new SendEmail();
                //    string rValue = email.SendPartsRequestedEmail(rec.ReceiveDetailID, GetUserIPAddress(), CurUserName);
                //}

                #endregion
                #region Return Parts
                //// Turned Off
                //if (CurProcess.Length > 12 && CurProcess.Substring(0, 13).ToUpper() == "RETURN PARTS")  // This unit has been shipped, we want the version changed from 000.  000 denotes GMP possesion, !000 denotes out the door.
                //{
                //    SendEmail email = new SendEmail();
                //    string rValue = email.SendPartsReturnedEmail(rec.ReceiveDetailID, GetUserIPAddress(), CurUserName);
                //}
                #endregion
                #endregion
                #region GMPRepair
                //if (CurProcess.Length > 9 && CurProcess.Substring(0, 10).ToUpper() == "GMP REPAIR")
                if (CurProcess.Length > 6 && CurProcess.Substring(0, 7).ToUpper() == "_REPAIR")
                {
                    // Look to see if Authorization is required.
                    string IsAuthorizationRequired = rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Authorization");
                    if (IsAuthorizationRequired.ToUpper() == "APPROVAL REQUIRED")
                    {
                        //AuthorizationRequired = "Y";
                        rdm.UpdateESNAttribute_Blank(ctx, ReceiveDetailID, "Authorization");

                        ReceiveDetailAuthohrizationManager rdam = new ReceiveDetailAuthohrizationManager(CurUserName);
                        decimal ef = 0;
                        decimal ff = 0;
                        decimal hst = 0;
                        decimal total = 0;
                        string Note = "";
                        string Note1 = CleanData(rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Claim Action"));
                        string Note2 = CleanData(rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Claim Reason"));
                        string Note3 = CleanData(rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Claim Location"));

                        if (Note1.Length > 0) { Note = Note1 + "\n"; }
                        if (Note2.Length > 0) { Note += Note2 + "\n"; }
                        if (Note3.Length > 0) { Note += Note3; }

                        //Log.LogIt("Middle of Approval Required");

                        //string sef = CleanData(rdm.GetReceiveDetailItem_DataElement(ctx, nReceiveDetailID, "Est"));
                        string sef = CleanData(rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Estimate"));
                        string sff = CleanData(rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Freight"));
                        string shst = CleanData(rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "billing_hst"));
                        string stotal = CleanData(rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "billing_total"));

                        if (decimal.TryParse(sef, out ef) == false) { ef = 0; }
                        if (decimal.TryParse(sff, out ff) == false) { ff = 0; }
                        if (decimal.TryParse(shst, out hst) == false) { hst = 0; }
                        if (decimal.TryParse(stotal, out total) == false) { total = 0; }

                        rdam.AddNewRequest(ctx, ReceiveDetailID, ef, ff, hst, total, Note, "AUT");
                    }
                }

                if (ReceiveDetailAuthorizationLog > 0)         // we are saving on a unit that had an authorization scanned in.
                {
                    ReceiveDetailAuthohrizationManager rdam = new ReceiveDetailAuthohrizationManager(CurUserName);
                    rdam.Complete(ctx, ReceiveDetailAuthorizationLog, CurUserName);
                }
                #endregion
                #region Process IFS Updates
                rdm.ProcessIFSUpdates(ReceiveDetailID, CurProcess);
                #endregion
                #region RMA RECEIVE
                if (CurProcess.Length > 10 && CurProcess.Substring(0, 11).ToUpper() == "RMA RECEIVE") { rdm.ProcessRMAReturn(ReceiveDetailID); }
                #endregion
            }
            #endregion
            rString = new JsonString("Result", "Saved");
            rString.AddValuePair("ReceiveHeaderID", ReceiveHeaderID.ToString());
            rString.AddValuePair("ReceiveDetailBulkID", "-1");
            rString.AddValuePair("ReceiveDetailID", ReceiveDetailID.ToString());
            return rString.ToString();
        }
        private void RecordDetailItemData_02RestrictedProcesses(clsLinqDataContext ctx, ReceiveDetailManager bm, ReceiveDetail rd, JsonString SoucreData, string CurUserName, bool DoCalc, decimal CurrentProcessID, ref bool UpdateSKU, ref bool UpdateCondition)
        {
            try
            {
                //List<TechAssignedKey> TechAssignedKeys = new List<TechAssignedKey>();
                var DetailItems = rd.ReceiveDetailItems;
                List<ReceiveDetailItem> DeleteList = new List<ReceiveDetailItem>();   // Old attributes that will be deleted  (mostly checkboxes, readial buttons and drop downs)
                MasterPartManager mpm = new MasterPartManager(CurUserName);
                List<List<string>> SourceDataAttributeLists = new List<List<string>>();
                SourceDataAttributeLists.Add(SoucreData.SourceDataNonCalcAttributes);
                SourceDataAttributeLists.Add(SoucreData.SourceDataCalcAttributes);

                #region  Reset the Checkboxes, radial buttons and dropdowns to null
                // We are only dealing with checkbox, readial button and dropdown attribute data.
                // The receivedetailitem records carries the optionid as a key pointing to the selected answer.
                // The user may have changed the answer, hence the old one needs to be deleted. Or recycled.
                // How I do this.
                // I get a list of all the questions and answers from the master tables. For the three types we are interested in.
                // I check that list against the source data sent in. If we are sent an answer for the question, we want to reset that question.
                // I place that question/answer record on my master QuestionID keep list. 
                // I now grab all the Question/Answer Records that have the same questionID found in the keep list.
                // It is time to go through all our DetailItems and see if we have any that match the above list. If we do, we want to keep track of it in case we need to delete it. It might get used again
                // 
                var dd1 = SoucreData.dbQuestionAnswerTable_TypeChecks;  // get all the question/Answer(option)s that we may need to reset.
                List<decimal> QuestionIDs = dd1.Where(x => SourceDataAttributeLists[0].Contains(x.jCoded.ToUpper()) == true).Select(y => y.QuestionID).ToList(); // get only those reset options that are in the source data.
                // get all of the dropdowns, chekcbox and radialbutton possible answers that have the same QuestionID as the options we found above. (Those found in the source data attributes.
                var dropdowns = dd1.Where(x => QuestionIDs.Contains(x.QuestionID) == true);
                foreach (var d in dropdowns)
                {
                    // get the found ReceiveDetailItem records and place them on a possible delete list.
                    var ris = DetailItems.Where(x => x.OptionID == d.OptionID);
                    foreach (ReceiveDetailItem ri in ris) { ri.Value = "0"; DeleteList.Add(ri); }
                }
                #endregion
                //JimErrorLogManager logManager = new JimErrorLogManager(CurUserName, "Att List:rd=" + rd.ReceiveDetailID.ToString());
                //logManager.IsActive = (System.Configuration.ConfigurationManager.AppSettings["WriteLog"].ToUpper() == "TRUE");
                //logManager.ReportMessage("Step 0 Attribute list save: ProcessID+" + CurrentProcessID.ToString());
                //bool UpdateSKU = false;
                //bool UpdateCondition = false;
                foreach (List<string> AttributeKeyList in SourceDataAttributeLists)
                {
                    foreach (string s in AttributeKeyList)
                    {
                        var o = SoucreData.dbQuestionAnswerTable.FirstOrDefault(x => x.jCoded.ToUpper() == s.ToUpper());
                        if (o != null)
                        {
                            if ((o.UseOptionValue == "1" && SoucreData.GetValue(s) == "1") || o.UseOptionValue != "1")
                            {
                                ReceiveDetailItem item = DetailItems.FirstOrDefault(x => x.OptionID == o.OptionID);
                                if (o.Name.ToUpper() == "DEALER WAYBILL")
                                {
                                    string wb = SoucreData.GetValue(s);
                                    if (wb.Length > 22)
                                    {
                                        wb = wb.Substring(11);
                                        wb = wb.Substring(0, wb.Length - 10);
                                        SoucreData.PutValue(s, wb);
                                    }
                                }
                                //logManager.ReportMessage("Step 11:");
                                #region Update/Add new Item to database
                                if (item != null)
                                {
                                    if (o.Type.ToUpper() == "CALC") { item.Value = CalcuateValue(rd.ReceiveDetailID, o.OptionValue, CurUserName); }
                                    else { item.Value = o.UseOptionValue == "1" ? "1" : SoucreData.GetValue(s); }
                                    item.LastUpdateDate = DateTime.Now;
                                    item.LastUpdateUser = CurUserName;
                                    //We no longer need to do this transaction because the original one takes on a status = 2 (deleted)
                                    //if (pur != null) { pur.ReceiveDetailID = rd.ReceiveDetailID; item.ReceiveDetailPartsUsages.Add(pur); }
                                    //if (pu != null) { pu.ReceiveDetailID = rd.ReceiveDetailID; item.ReceiveDetailPartsUsages.Add(pu); }
                                    ReceiveDetailItem i = DeleteList.FirstOrDefault(x => x.ReceiveDetailItemID == item.ReceiveDetailItemID);
                                    if (i != null) { DeleteList.Remove(i); }   // Just brought back an old record to life, we no longer want it deleted.
                                }
                                else
                                {
                                    item = new ReceiveDetailItem();
                                    if (o.Type.ToUpper() == "CALC") { item.Value = CalcuateValue(rd.ReceiveDetailID, o.OptionValue, CurUserName); }
                                    else { item.Value = o.UseOptionValue == "1" ? "1" : SoucreData.GetValue(s); }
                                    item.OptionID = o.OptionID;
                                    item.Version = 0;
                                    item.ReceiveDate = rd.ReceiveDate;
                                    item.ReceiveHeaderID = rd.ReceiveHeaderID;
                                    item.ReceiveDetailID = rd.ReceiveDetailID;
                                    item.LastUpdateUser = CurUserName;
                                    item.LastUpdateDate = DateTime.Now;
                                    item.CreateUser = CurUserName;
                                    //We no longer need to do this transaction because the original one takes on a status = 2 (deleted)
                                    //if (pur != null) { pur.ReceiveDetailID = rd.ReceiveDetailID; item.ReceiveDetailPartsUsages.Add(pur); }
                                    //if (pu != null) { pu.ReceiveDetailID = rd.ReceiveDetailID; item.ReceiveDetailPartsUsages.Add(pu); }
                                    DetailItems.Add(item);
                                }
                                #endregion
                                //logManager.ReportMessage("Step 12:");
                            }
                            #region Unit Field Updated
                            if (o.Name.ToUpper() == "CARRIER") { rd.CarrierID = o.OptionID; rd.Carrier = o.OptionValue; UpdateSKU = true; }
                            if (o.Name.ToUpper() == "MANUFACTURER") { rd.ManufacturerID = o.OptionID; rd.Manufacturer = o.OptionValue; UpdateSKU = true; }
                            if (o.Name.ToUpper() == "MODEL") { rd.ModelID = o.OptionID; rd.Model = o.OptionValue; UpdateSKU = true; }
                            if (o.Name.ToUpper() == "COLOUR") { rd.ColourID = o.OptionID; rd.Colour = o.OptionValue; UpdateSKU = true; }
                            if (o.Name.ToUpper() == "GRADE") { rd.GradeID = o.OptionID; rd.Grade = o.OptionValue; }
                            if (o.Name.ToUpper() == "IFS RMA Number") { rd.RMANumber = SoucreData.GetValue(s); }
                            if (o.Name.ToUpper() == "IFS Conditions") { rd.IFSCondition = o.Abbr; }
                            #endregion
                        }
                    }
                    ctx.ReceiveDetailItems.DeleteAllOnSubmit(DeleteList);
                    ctx.SubmitChanges();
                }
                return;
            }
            catch (Exception ex)
            {
                JimErrorLogManager logManagerz = new JimErrorLogManager(CurUserName, "Error");
                logManagerz.ReportMessage("ERROR:" + ex.Message + ":" + ex.StackTrace);
            }
        }



        [OperationContract]
        public string BinBulkProcess(string BinNumber, string Data)
        {
            string IPAddress = GetUserIPAddress();
            decimal HeaderID = -1;
            JsonString ScreenData = new JsonString(Data, true);
            string CurUserName = ScreenData.GetValue("CurUserName");
            string CurProcess = ScreenData.GetValue("CurProcess");
            XBINXLogManager xbinm = new XBINXLogManager(BinNumber, CurProcess, "", Data, CurUserName);
            HeaderID = xbinm.Save();
            bool DoThreadedXBINX = false;
            if (DoThreadedXBINX == true)
            {
                Log.LogIt("Bin Process Started - Threaded:" + BinNumber + ":" + HeaderID.ToString() + ":" + Data);
                ThreadPool.QueueUserWorkItem(Report => BinBulkProcessThreaded(BinNumber, Data, IPAddress, HeaderID));
                JsonString rString = new JsonString("Result", "Saved");
                rString.AddValuePair("BinNumber", BinNumber);
                rString.AddValuePair("UnitCount", "Threaded(Key:" + HeaderID + ")");
                return rString.ToString();
            }
            else
            {
                Log.LogIt("Bin Process Started:" + BinNumber);
                return BinBulkProcessThreaded(BinNumber, Data, IPAddress, HeaderID);
            }
        }
        public string BinBulkProcessThreaded(string BinNumber, string Data, string IPAddress, decimal XbinXHeaderid)
        {
            clsLog Logx = new clsLog(HttpContext.Current.Server.MapPath("~"), "WebServer_xbinx_Log.txt", "JIM", "JIM");
            if (System.Configuration.ConfigurationManager.AppSettings["WriteLog"].ToUpper() == "TRUE")
            {
                WriteLog = true;
            }
            Logx.writeLogData = WriteLog;

            DateTime StartDate = DateTime.Now;
            string Errors = "";
            string EmailMessage = "";
            JsonString rString = new JsonString("Result", "NotSaved");
            rString.AddValuePair("BinNumber", BinNumber);
            if (BinNumber.Trim().Length == 0) { return rString.ToString(); }
            JsonString ScreenData = new JsonString(Data, true);
            string CurUserName = ScreenData.GetValue("CurUserName");
            string CurProcess = ScreenData.GetValue("CurProcess");
            Int16 xCount = 0;
            Int16 xCountError = 0;
            string rMessage = "";
            //Log.LogIt("Bin Process Moving Forward:" + Data);

            // We now have to get all records that have this bin number
            JimErrorLogManager logManagerz = new JimErrorLogManager(CurUserName, "XBINX(" + BinNumber + ") Process:" + CurProcess);
            logManagerz.IsActive = true;
            EmailMessage = "Start time:" + DateTime.Now.ToString();
            logManagerz.ReportMessage(EmailMessage);

            ReceiveDetailManager bm = new ReceiveDetailManager(CurUserName);
            using (clsLinqDataContext ctx = bm.GetDataContext(CurUserName))
            {
                XBINXLogManager xbinm = new XBINXLogManager(ctx, XbinXHeaderid);
                Logx.LogIt("Bin Process Inside CTX");
                List<ReceiveDetailItem> rdlist = bm.GetReceiveDetailItems_ThisDataItem(ctx, "BIN", BinNumber);
                foreach (ReceiveDetailItem ri in rdlist)
                {
                    Logx.LogIt("Bin rdid:" + ri.ReceiveDetailID.ToString());
                    ReceiveDetail rd = ctx.ReceiveDetails.FirstOrDefault(x => x.ReceiveDetailID == ri.ReceiveDetailID);
                    if (rd != null)
                    {
                        ScreenData.SetAttributeValue("ReceiveHeaderID", rd.ReceiveHeaderID.ToString());
                        ScreenData.SetAttributeValue("ReceiveDetailID", rd.ReceiveDetailID.ToString());
                        ScreenData.SetAttributeValue("ClientLocationID", rd.ClientLocationID.ToString());
                        ScreenData.SetAttributeValue("ESN", rd.ESN);
                        logManagerz.ReportMessage("Bin(" + BinNumber + ") ESN(" + rd.ReceiveDetailID.ToString() + "):" + rd.ESN + " Time:" + DateTime.Now.ToString());
                        try
                        {
                            //éérMessage = AddDataDetailThreadedGO(ScreenData.GetNewDataString(), "N", "", IPAddress);
                            StartDate = DateTime.Now;
                            rMessage = RunThreadedXBINXSaveRestrictedProcesses(ctx, bm, rd, ScreenData.GetNewDataString(), IPAddress, logManagerz);
                            xbinm.LogData(rd.ReceiveDetailID, StartDate, rMessage);
                            xCount++;
                        }
                        catch (Exception ex)
                        {
                            xCountError++;
                            Errors = Errors + rd.ReceiveHeaderID.ToString() + ";" + rd.ESN + ";" + ex.ToString() + "/-/";
                            xbinm.LogData(rd.ReceiveDetailID, StartDate, Errors);
                            logManagerz.ReportMessage("Bin Error:" + ex.ToString());
                        }
                    }
                }
                xbinm.Save(ctx);
            }
            rString = new JsonString("Result", "Saved");
            rString.AddValuePair("BinNumber", BinNumber);
            if (xCountError > 0) { rString.AddValuePair("UnitCount", xCount.ToString() + "E" + xCountError.ToString()); }
            else { rString.AddValuePair("UnitCount", "(Key:" + XbinXHeaderid.ToString() + ") " + xCount.ToString()); }
            Logx.LogIt("Bin Return:" + rString.ToString());
            EmailMessage = EmailMessage + Environment.NewLine + "End Out:" + rString.ToString() + Environment.NewLine + "End time:" + DateTime.Now.ToString();
            logManagerz.ReportMessage("End Out:" + rString.ToString());
            logManagerz.ReportMessage("End time:" + DateTime.Now.ToString());
            //EmailManager em = new EmailManager(CurUserName);
            //em.SendEmail("XbinX", "jim.willson@hotmail.com", "xbinx", EmailMessage);
            return rString.ToString();
        }


        private string CalcuateValue(decimal ReceiveDetailID, string Formula, string CurUserName)
        {
            if (Formula.Length == 0) { return ""; }
            string rValue = "";
            GMPCalculator calculator = new GMPCalculator(ReceiveDetailID, CurUserName);
            try { rValue = CleanData(calculator.Calculate(Formula)); }
            catch (Exception ex) { rValue = "CALC ERROR"; }
            return rValue;
        }
        private string GetUserIPAddress()
        {
            string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ip))
            {
                ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return ip;
        }
        private bool IsThisAPartsProcess(string Data)
        {
            JsonString ScreenData = new JsonString(Data, true);
            decimal ReceiveDetailID = ScreenData.GetValueDecimal("ReceiveDetailID", -1);
            string CurUserName = ScreenData.GetValue("CurUserName");

            //ReceiveDetailManager rdm = new ReceiveDetailManager(CurUserName);
            //using (clsLinqDataContext ctx = rdm.GetDataContext(CurUserName))
            //{
            //ReceiveDetail rd = ctx.ReceiveDetails.FirstOrDefault(x => x.ReceiveDetailID == ReceiveDetailID);
            //if (rd == null) { return false; }
            List<List<string>> SourceDataAttributeLists = new List<List<string>>();
            SourceDataAttributeLists.Add(ScreenData.SourceDataNonCalcAttributes);
            foreach (List<string> AttributeKeyList in SourceDataAttributeLists)
            {
                foreach (string s in AttributeKeyList)
                {
                    var o = ScreenData.dbQuestionAnswerTable.FirstOrDefault(x => x.jCoded.ToUpper() == s.ToUpper());
                    if (o != null)
                    {
                        if ((o.UseOptionValue == "1" && ScreenData.GetValue(s) == "1") || o.UseOptionValue != "1")
                        {
                            // If our Attribute is a part... we have a parts process.
                            if (o.Name.Length > 5 && o.Name.Substring(0, 5).ToUpper() == "PART ")
                            {
                                return true;
                            }
                        }
                    }
                }

            }
            //}
            return false;
        }
        private bool AreAllAssignedPartsAccountedFor(string Data)
        {
            JsonString ScreenData = new JsonString(Data, true);
            decimal ReceiveDetailID = ScreenData.GetValueDecimal("ReceiveDetailID", -1);
            string CurUserName = ScreenData.GetValue("CurUserName");
            List<List<string>> SourceDataAttributeLists = new List<List<string>>();
            SourceDataAttributeLists.Add(ScreenData.SourceDataNonCalcAttributes);

            List<decimal> NewIDsBeingAdded = new List<decimal>();
            // We need to go through all the attributes and pull any that have not been saved yet.
            //    Once we have all the MasterPartsLinkTableIDs for those, we can get the ones already saved 
            //    Then compare them to see if we have the correct total.
            //

            foreach (List<string> AttributeKeyList in SourceDataAttributeLists)
            {
                foreach (string s in AttributeKeyList)
                {
                    var o = ScreenData.dbQuestionAnswerTable.FirstOrDefault(x => x.jCoded.ToUpper() == s.ToUpper());
                    if (o != null)
                    {
                        if ((o.UseOptionValue == "1" && ScreenData.GetValue(s) == "1") || o.UseOptionValue != "1")
                        {
                            // If our Attribute is a part... we have a parts process.
                            if (o.Name.Length > 5 && o.Name.Substring(0, 5).ToUpper() == "PART ")
                            {
                                decimal PartNumberID = -1;
                                string value = ScreenData.GetValue(s);
                                if (value.IndexOf(":id=") > -1)
                                {
                                    string x = value.Substring(value.IndexOf(":id=") + 4);
                                    if (decimal.TryParse(x, out PartNumberID) == true)
                                    {
                                        NewIDsBeingAdded.Add(PartNumberID);
                                    }
                                }

                                else if (value.IndexOf("xidX") > -1)   // The part numbers can also be coded as "xidX" Case Sensitive. This is to allow for scanning of the partnumber in.
                                {
                                    string x = value.Substring(value.IndexOf(":xidX") + 4);
                                    if (decimal.TryParse(x, out PartNumberID) == true)
                                    {
                                        NewIDsBeingAdded.Add(PartNumberID);
                                    }
                                }
                            }
                        }
                    }
                }

            }
            // Now we need to get our parts usage summary to see if we balance.
            MasterPartManager mpm = new MasterPartManager(CurUserName);
            List<TechAssignedUsageSummary_New> tus = mpm.GetTechAssignedUsageSummary_New(ReceiveDetailID).Where(x => x.Status == "Tech Assigned").ToList();
            TechAssignedUsageSummary_New t;
            foreach (decimal MasterPartsLinkTableID in NewIDsBeingAdded)
            {
                t = tus.FirstOrDefault(x => x.MasterPartsLinkTableID == MasterPartsLinkTableID);
                if (t != null)
                {
                    t.UnAttached -= 1;
                    t.Attached += 1;
                }
            }
            t = tus.FirstOrDefault(x => x.UnAttached > 0);
            if (t != null)
            {
                return false;
            }
            //}
            return true;
        }
        //private void RecordAssignedPartsShipped(clsLinqDataContext ctx, ReceiveDetailManager rdm, decimal ReceiveDetailID, string UserName)
        //{
        //    JimErrorLogManager logManager = new JimErrorLogManager(UserName, "RecordAssignedPartsShipped");
        //    logManager.ReportMessage("Step 0 Start of attribute list save:" + ReceiveDetailID.ToString());
        //    ReceiveDetail rd = ctx.ReceiveDetails.FirstOrDefault(x => x.ReceiveDetailID == ReceiveDetailID);
        //    if (rd == null) { return; }
        //    MasterPartManager pm = new MasterPartManager(rdm.UserName);
        //    foreach (ReceiveDetailItem item in rd.ReceiveDetailItems)
        //    {
        //        if (item.Option.Question.Name.Length > 4 && item.Option.Question.Name.Substring(0, 5).ToUpper() == "PART ")
        //        {
        //            string rValue = "";
        //            logManager.ReportMessage("MasterPartsTechAssignedLog_Shipped(" + ReceiveDetailID.ToString() + "," + item.ReceiveDetailItemID.ToString() + ")");
        //            rValue = pm.MasterPartsTechAssignedLog_Shipped(ReceiveDetailID, item.ReceiveDetailItemID);
        //            logManager.ReportMessage("Return Value:" + rValue);
        //        }
        //    }
        //}

        #endregion        #region Unknown is stil required




        [OperationContract]
        public string AddDataDetailThreadedAlert(string Data, string Threaded, string AlertString)
        {
            return AddDataDetailThreadedGO(Data, Threaded, AlertString, GetUserIPAddress());
        }




        #region STUBBED
        public string AddDataDetailThreadedGO_Stubbed(string Data, string Threaded, string AlertString, string IPAddress)
        {
            //decimal TimeInMilliSeconds = 0;
            StringBuilder StubMessages = new StringBuilder();

            string IsThreaded = "N";

            TimeLogManager timelog = new TimeLogManager("jmccomb", IPAddress);
            timelog.StartTimer();
            JsonString ScreenData = new JsonString(Data, true);
            decimal ReceiveDetailID = ScreenData.GetValueDecimal("ReceiveDetailID", -1);
            timelog.SaveTimeLogWorkScreen_PreProcess(ReceiveDetailID, "PreWorkScreenSave0", -1, Data);

            decimal ReceiveHeaderID = ScreenData.GetValueDecimal("ReceiveHeaderID", -1);
            decimal ReceiveDetailBulkID = ScreenData.GetValueDecimal("ReceiveDetailBulkID", -1);
            decimal ClientLocationID = ScreenData.GetValueDecimal("ClientLocationID", -1);
            decimal ProjectID = ScreenData.GetValueDecimal("ProjectID", -1);
            decimal CurrentProcessID = ScreenData.GetValueDecimal("CurProcessID", -1);
            string CurUserName = ScreenData.GetValue("CurUserName");
            string ESN = ScreenData.GetValue("ESN");
            string Project = ScreenData.GetValue("Project");
            string CurProcess = ScreenData.GetValue("CurProcess");
            string RMA = ScreenData.GetValue("RMA");
            string ProjectTag = ScreenData.GetValue("PROJTAG");
            string AuthorizationRequired = "N";
            string MakeModelString = "";
            string PCLBString = "";
            string CompList = "";
            //DateTime StartDate = DateTime.Now;
            StubMessages.AppendLine("Data:" + Data.ToString());
            StubMessages.AppendLine("Threaded:" + Threaded.ToString());
            StubMessages.AppendLine("AlertString:" + AlertString.ToString());
            StubMessages.AppendLine("IPAddress:" + IPAddress.ToString());



            StubMessages.AppendLine("ReceiveHeaderID:" + ReceiveHeaderID.ToString());
            StubMessages.AppendLine("ClientLocationID:" + ClientLocationID.ToString());
            StubMessages.AppendLine("ProjectID:" + ProjectID.ToString());
            StubMessages.AppendLine("CurrentProcessID:" + CurrentProcessID.ToString());
            StubMessages.AppendLine("CurUserName:" + CurUserName.ToString());
            StubMessages.AppendLine("ESN:" + ESN.ToString());
            StubMessages.AppendLine("Project:" + Project.ToString());
            StubMessages.AppendLine("CurProcess:" + CurProcess.ToString());
            StubMessages.AppendLine("RMA:" + RMA.ToString());
            StubMessages.AppendLine("ProjectTag:" + ProjectTag.ToString());
            StubMessages.AppendLine("AuthorizationRequired:" + AuthorizationRequired.ToString());
            StubMessages.AppendLine("MakeModelString:" + MakeModelString.ToString());
            StubMessages.AppendLine("PCLBString:" + PCLBString.ToString());
            StubMessages.AppendLine("CompList:" + CompList.ToString());
            StubMessages.AppendLine(":" + ReceiveHeaderID.ToString());
            StubMessages.AppendLine(":" + ReceiveHeaderID.ToString());

            timelog.SaveTimeLogWorkScreen_PreProcess(ReceiveDetailID, "PreWorkScreenSave1stub", CurrentProcessID, Data);



            // We need to get things ready to be received as json when going back to the page.
            JsonString rString = new JsonString("Result", "NotSaved");
            if (ESN.Length == 0)
            {
                //rString = new JsonString("Result", "NotSaved");
                StubMessages.AppendLine("Error:" + "No ESN given".ToString());
                rString.AddValuePair("Error", "No ESN given");
                return rString.ToString();
            }
            if (CurProcess.ToUpper() == "BULKRECEIVE" || CurProcess.ToUpper() == "RECEIVEFROMBULK" || CurProcess.ToUpper() == "BULKMOVE")
            {
                StubMessages.AppendLine("Error:" + "Not Saved! -- Invalid Process:" + CurProcess);
                rString.AddValuePair("Error", "Not Saved! -- Invalid Process:" + CurProcess);
                return rString.ToString();
            }
            //bool addedNew = false;
            ReceiveDetailManager rdm = new ReceiveDetailManager(CurUserName);
            using (clsLinqDataContext ctx = rdm.GetDataContext(CurUserName))
            {
                string IFSRMANumber = ScreenData.GetTextData(ctx, "IFS RMA Number");
                bool ISThereNow = rdm.isDetailThere(ctx, ESN);
                #region New Add edit checks
                //if (rdm.isDetailThere(ctx, ESN) == false && rdm.isOKToAddFromThisProcess(CurProcess) == false)
                if (ISThereNow == false && rdm.isOKToAddFromThisProcess(CurProcess) == false)
                {
                    //rString = new JsonString("Result", "NotSaved");
                    rString.AddValuePair("Error", "Unable to add from this process:" + CurProcess);
                    return rString.ToString();
                }

                // If this is a new one, we need to make sure this is not moving into a "Frozen" area.
                //if (rdm.isDetailThere(ctx, ESN) == false)
                if (ISThereNow == false)
                {
                    string rMessage = "";
                    if (rdm.IsThisFrozen(Data, ref rMessage) == true)
                    {
                        rString = new JsonString("Result", "NotSaved");
                        rString.AddValuePair("Error", rMessage);
                        return rString.ToString();
                    }
                }
                // Check to make sure the SKU is valid.
                if (ISThereNow == false)
                {
                    string rMessage = "";
                    if (ScreenData.IsSKUValid(ctx, ref rMessage) != true)
                    {
                        rString = new JsonString("Result", "NotSaved");
                        rString.AddValuePair("Error", rMessage);
                        return rString.ToString();
                    }
                }
                #endregion
                #region Obtain the proper ReceiveDetail Record.
                ReceiveDetail rec = null;
                rec = rdm.ReceiveDetail_NewOrThisESN(ctx, ESN);
                if (rec == null)
                {
                    //rString = new JsonString("Result", "NotSaved");
                    rString.AddValuePair("Error", "Unable to get a new or existing Unit Record:" + CurProcess);
                    return rString.ToString();
                }   // This may never happen because the function returns a new record if there is no 000 version.

                // If this is a receive screen, and we have a ReceiveDetailID already, then we need to stop/exit
                if (rec.ReceiveDetailID > 0 && rec.ReceiveDetailID != ReceiveDetailID && CurProcess.Length >= 7 && CurProcess.Substring(0, 7).ToUpper() == "RECEIVE")
                {
                    rString.AddValuePair("Error", "Not Saved! -- ESN already on file!");
                    return rString.ToString();
                }
                #endregion
                #region Save ReceiveDetailRecord
                if (rec.ReceiveDetailID < 1)
                {
                    string IFS_POAdjustedData = "";
                    string rMessage = "";
                    decimal IFSPurchaseOrderDetailID = -1;
                    rec.SKU = ScreenData.GetSKU();
                    rec.IFSCondition = "GEN";
                    rec.IFSLocation = rdm.GetIFSLocation(-1, CurProcess, ClientLocationID, "");
                    rec.ClientLocationID = ClientLocationID;
                    rec.ReceiveHeaderID = ReceiveHeaderID;
                    if (RMA.Length == 0) { rec.RMANumber = IFSRMANumber; }
                    else { rec.RMANumber = CleanData(RMA); }
                    rec.ESN = CleanData(ESN);
                    rec.ProjectTag = CleanData(ProjectTag);
                    rec.ICB = "";
                    rec.PIN = "";
                    rec.ProjectName = CleanData(Project);
                    rec.ProjectID = ProjectID;
                    rec.ReceiveDate = DateTime.Now;
                    rec.MiscNote = "";
                    rec.ISFTransactionDirective = rdm.IFSDirective(ctx, "Ignore");
                    rec.IFSPurchaseOrderDetailID = IFSPurchaseOrderDetailID;
                    #region A Couple Quick Edit Checks
                    if (rec.SKU == null || rec.SKU.Length == 0)
                    {
                        rString.AddValuePair("Error", "Error on Save, blank SKU. Please try again");
                        return rString.ToString();
                    }
                    if (rec.IFSLocation == null || rec.IFSLocation.Length == 0)
                    {
                        rString.AddValuePair("Error", "Error on Save, blank Site Location. Please try again");
                        return rString.ToString();
                    }
                    #endregion
                    #region See if it requires a PO
                    if (CurProcess.ToUpper() == "RECEIVE AGAINST PO")      // We only want to deal with the PO  Number if it comes in from this process screen
                    {
                        if (rdm.IsPODataValid02(Data, AlertString, ref IFSPurchaseOrderDetailID, ref IFS_POAdjustedData, ref rMessage) == false)
                        {
                            rString.AddValuePair("Error", rMessage);
                            return rString.ToString();
                        }
                        Data = IFS_POAdjustedData;
                    }
                    #endregion
                    //try {
                    // -------------------         STUBBED OUT THIS STATEMENT   -------->rdm.InsertReceiveDetail(ctx, rec, CurrentProcessID);
                    //}
                    //catch (Exception ex)
                    //{
                    //rMessage = "ERROR TEST";                    // ex.Message;
                    //    rString = new JsonString("Result", "NotSaved");
                    //    rString.AddValuePair("Error", rMessage);
                    //    return rString.ToString();
                    //}
                    // we have got a problem, there should be an ID now that we inserted it.
                    if (rec.ReceiveDetailID < 1)
                    {
                        rString.AddValuePair("Error", "Device data not saved");
                        return rString.ToString();
                    }
                    // we need to ignore the inial save. Now we have things saved, we need to do the next save as a PO_Initiate.
                    Process process = ctx.Processes.FirstOrDefault(x => x.ProcessID == CurrentProcessID);
                    if (process != null && process.IFSDirectiveType != null && process.IFSDirectiveType.Length > 0)
                    {
                        rec.ISFTransactionDirective = rdm.IFSDirective(ctx, process.IFSDirectiveType);
                    }
                    else
                    {
                        rec.ISFTransactionDirective = rdm.IFSDirective(ctx, "PO_Receipt");
                    }

                    //addedNew = true;
                    //// up till now, we may not know what these two ID fields are. 
                    //// Now that we just inserted a new on, we need to update these keys.
                    ReceiveHeaderID = rec.ReceiveHeaderID;
                    ReceiveDetailID = rec.ReceiveDetailID;
                    MakeModelString = rdm.MakeModelColourNickName(ctx, ReceiveDetailID);
                    PCLBString = rdm.GetProjectClientLocationBinString(ctx, ReceiveDetailID);
                    CompList = rdm.GetRequestProcessCompletionList(ctx, ReceiveDetailID);

                    string IsAuthorizationRequired = rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Authorization");
                    if (IsAuthorizationRequired.ToUpper() == "APPROVAL REQUIRED") { AuthorizationRequired = "Y"; }
                }
                #endregion
            }
            #region editchecks to verify Device (receiveDetail) record was saved.
            if (ReceiveDetailID < 1)
            {
                rString.AddValuePair("Error", "Device data not saved");
                return rString.ToString();
            }
            // Jody wants to make sure that if this is a screen with parts, they have picked all parts related to the unit.
            // Otherwise, there are problems and the unit is not saved.
            if (IsThisAPartsProcess(Data) == true && AreAllAssignedPartsAccountedFor(Data) == false)
            {
                rString.AddValuePair("Error", "Unused assigned parts. Data Not Saved");
                return rString.ToString();
            }
            #endregion
            timelog.SaveTimeLogWorkScreen_PreProcess(ReceiveDetailID, "PreWorkScreenSave2Stub", CurrentProcessID, Data);
            string rMessagex = "";
            rMessagex = RunThreadedSave(ReceiveDetailID, Data, IPAddress, timelog);
            #region Finish Up
            if (rMessagex.Length > 0)
            {
                rString.AddValuePair("Error", rMessagex);
                return rString.ToString();
            }

            rString = new JsonString("Result", "Saved");
            rString.AddValuePair("ReceiveHeaderID", ReceiveHeaderID.ToString());
            rString.AddValuePair("ReceiveDetailBulkID", "-1");
            rString.AddValuePair("ReceiveDetailID", ReceiveDetailID.ToString());
            //rString.AddValuePair("SUFD", rdm.GetSetUpFieldDef(ctx, ProjectID));
            //rString.AddValuePair("QTY", QTY);
            rString.AddValuePair("CompProcList", CompList);
            rString.AddValuePair("MMS", MakeModelString);
            rString.AddValuePair("THREADED", IsThreaded);
            rString.AddValuePair("AR", AuthorizationRequired);
            rString.AddValuePair("PCLB", PCLBString);                           //lblProjectClientLocationBinTitle
            timelog.StopTimer();
            timelog.SaveTimeLogWorkScreen(ReceiveDetailID, CurrentProcessID, Data);
            #endregion
            return rString.ToString();
        }
        #endregion
        #endregion
        //  #endregion


        #region ParseScancode
        [OperationContract]
        public string ScanCodeParse(string ClientLocationID, string UnitItemKey, string Process, string ScanCode, string UserName, string StepUpName, string Manufacturer, string Model)
        {
            Log.LogIt("ScanCode Parse - Started (" + ScanCode + ") ******************");
            //return "::Unknown Scancode:353323050480283::::";
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                string ReturnMessage = string.Empty;
                string RTable = string.Empty;
                string RID = string.Empty;
                string RValue = string.Empty;
                string TransferData = string.Empty;

                string MessageQueueStop = "";
                string MessageQueueMessage = "";

                TransferData = ScanCode;
                #region PartNumber Processing
                decimal ManufacturerID = -1;
                decimal ModelID = -1;
                if (decimal.TryParse(Manufacturer, out ManufacturerID) == false) { ManufacturerID = -1; }
                if (decimal.TryParse(Model, out ModelID) == false) { ModelID = -1; }

                // Look to see if this is a partnumber lookup
                if (ScanCode.Length > 6 && ScanCode.Substring(0, 3).ToUpper() == "/PN")         // Search for Partnumber MFG Partnumber
                {

                    TransferData = ScanCode.Substring(3);
                    ScanCode = ScanCode.Substring(1, 2);
                    ReturnMessage = "::Unknown Scancode:" + ScanCode + TransferData + ":::::";
                    //MasterPartsLinkTable r = ctx.MasterPartsLinkTables.FirstOrDefault(x=> x.PartNumber == TransferData);
                    MasterPartsLinkTable r = ctx.MasterPartsLinkTables.FirstOrDefault(x => x.PartNumber == TransferData && x.Manufacturer == ManufacturerID.ToString() && x.MasterPartsLinkTableModelLists.Any(y => y.ModelID == ModelID) == true);
                    if (r == null) { r = ctx.MasterPartsLinkTables.FirstOrDefault(x => x.PartNumber == TransferData && x.Manufacturer == ManufacturerID.ToString() && (x.MasterPartsLinkTableModelLists.Any(y => y.ModelID == -1) == true || x.MasterPartsLinkTableModelLists.Any() == false)); }
                    if (r == null) { r = ctx.MasterPartsLinkTables.FirstOrDefault(x => x.PartNumber == TransferData && x.Manufacturer == "-1" && x.MasterPartsLinkTableModelLists.Any(y => y.ModelID == ModelID) == true); }
                    if (r == null) { r = ctx.MasterPartsLinkTables.FirstOrDefault(x => x.PartNumber == TransferData && x.Manufacturer == "-1" && (x.MasterPartsLinkTableModelLists.Any(y => y.ModelID == -1) == true || x.MasterPartsLinkTableModelLists.Any() == false)); }

                    if (r == null) { return ReturnMessage; }
                    var PartAttribute = ctx.Options.FirstOrDefault(x => x.Question.Name.Substring(0, 5) == "Part ");    //I need a OptionID in order to set the value.
                    if (PartAttribute == null) { return ReturnMessage; }
                    ReturnMessage = "Option:" + PartAttribute.OptionID.ToString() + ":::::" + r.PartNumber + " " + r.MasterPart.Description + "xidX" + r.MasterPartsLinkTableID.ToString();
                    return ReturnMessage;
                    // Option:777:Part #:Part 1:P1:TX
                }
                // Look to see if this is a partnumber lookup
                if (ScanCode.Length > 3 && ScanCode.Substring(0, 3).ToUpper() == "/PG")         // Search for Partnumber GMP Partnumber
                {
                    TransferData = ScanCode.Substring(3);
                    ScanCode = ScanCode.Substring(1, 2);
                    ReturnMessage = "::Unknown Scancode:" + ScanCode + TransferData + ":::::";
                    //MasterPartsLinkTable r = ctx.MasterPartsLinkTables.FirstOrDefault(x => x.GMPPartNumber == TransferData);
                    MasterPartsLinkTable r = ctx.MasterPartsLinkTables.FirstOrDefault(x => x.GMPPartNumber == TransferData && x.Manufacturer == ManufacturerID.ToString() && x.MasterPartsLinkTableModelLists.Any(y => y.ModelID == ModelID) == true);
                    if (r == null) { r = ctx.MasterPartsLinkTables.FirstOrDefault(x => x.GMPPartNumber == TransferData && x.Manufacturer == ManufacturerID.ToString() && (x.MasterPartsLinkTableModelLists.Any(y => y.ModelID == -1) == true || x.MasterPartsLinkTableModelLists.Any() == false)); }
                    if (r == null) { r = ctx.MasterPartsLinkTables.FirstOrDefault(x => x.GMPPartNumber == TransferData && x.Manufacturer == "-1" && x.MasterPartsLinkTableModelLists.Any(y => y.ModelID == ModelID) == true); }
                    if (r == null) { r = ctx.MasterPartsLinkTables.FirstOrDefault(x => x.GMPPartNumber == TransferData && x.Manufacturer == "-1" && (x.MasterPartsLinkTableModelLists.Any(y => y.ModelID == -1) == true || x.MasterPartsLinkTableModelLists.Any() == false)); }
                    if (r == null) { return ReturnMessage; }
                    var PartAttribute = ctx.Options.FirstOrDefault(x => x.Question.Name.Substring(0, 5) == "Part ");    //I need a OptionID in order to set the value.
                    if (PartAttribute == null) { return ReturnMessage; }
                    ReturnMessage = "Option:" + PartAttribute.OptionID.ToString() + ":::::" + r.PartNumber + " " + r.MasterPart.Description + "xidX" + r.MasterPartsLinkTableID.ToString();
                    return ReturnMessage;
                    // Option:777:Part #:Part 1:P1:TX
                }
                if (ScanCode.Length > 3 && ScanCode.Substring(0, 3).ToUpper() == "/PT")         // Search for Master Part and then search for the partnumber that matches.
                {
                    TransferData = ScanCode.Substring(3);
                    ScanCode = ScanCode.Substring(1, 2);
                    ReturnMessage = "::Unknown Scancode:" + ScanCode + TransferData + ":::::";
                    MasterPart mp = ctx.MasterParts.FirstOrDefault(x => x.Name.ToUpper() == TransferData.ToUpper());
                    if (mp == null) { return ReturnMessage; }

                    MasterPartsLinkTable r = mp.MasterPartsLinkTables.FirstOrDefault(x => x.Manufacturer == ManufacturerID.ToString() && x.MasterPartsLinkTableModelLists.Any(y => y.ModelID == ModelID) == true);
                    if (r == null) { r = mp.MasterPartsLinkTables.FirstOrDefault(x => x.Manufacturer == ManufacturerID.ToString() && (x.MasterPartsLinkTableModelLists.Any(y => y.ModelID == -1) == true || x.MasterPartsLinkTableModelLists.Any() == false)); }
                    if (r == null) { r = mp.MasterPartsLinkTables.FirstOrDefault(x => x.Manufacturer == "-1" && x.MasterPartsLinkTableModelLists.Any(y => y.ModelID == ModelID) == true); }
                    if (r == null) { r = mp.MasterPartsLinkTables.FirstOrDefault(x => x.Manufacturer == "-1" && (x.MasterPartsLinkTableModelLists.Any(y => y.ModelID == -1) == true || x.MasterPartsLinkTableModelLists.Any() == false)); }
                    if (r == null) { return ReturnMessage; }

                    var PartAttribute = ctx.Options.FirstOrDefault(x => x.Question.Name.Substring(0, 5) == "Part ");    //I need a OptionID in order to set the value.
                    if (PartAttribute == null) { return ReturnMessage; }
                    ReturnMessage = "Option:" + PartAttribute.OptionID.ToString() + ":::::" + r.PartNumber + " " + r.MasterPart.Description + "xidX" + r.MasterPartsLinkTableID.ToString();
                    return ReturnMessage;
                    // Option:777:Part #:Part 1:P1:TX
                }
                #endregion
                #region UPC Processing - Receive Screens Only.
                if (Process.Substring(0, 7).ToUpper() == "RECEIVE")
                {
                    ctx.Get_ScanComandLookupChain(ScanCode, ref ReturnMessage);
                    if (ReturnMessage.Trim().Length > 0)
                    {
                        Log.LogIt("MasterCarrierManufacturerUPCLookupChain Parse - Done (MacroChain)");
                        return "MacroChain:" + ReturnMessage;
                    }
                }
                #endregion
                if (ScanCode.Length > 2 && ScanCode.Substring(2, 1) == ".")
                {
                    // This is a Macro chaing.
                    string[] mCodes = ScanCode.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                    List<string> rMessages = new List<string>();
                    string rMess = "";
                    foreach (string m in mCodes)
                    {
                        TransferData = m.Substring(2);
                        ScanCode = m.Substring(0, 2);
                        ctx.ProcessMacroKey(UnitItemKey, Process, ScanCode, UserName, StepUpName, ref RTable, ref RID, ref RValue, ref ReturnMessage);
                        rMessages.Add(ReturnMessage + ":" + TransferData);
                        rMess += ReturnMessage + ":" + TransferData + ";";
                    }
                    Log.LogIt("ScanCode Parse - Done (MacroChain)");
                    return "MacroChain:" + rMess;
                }

                if (ScanCode.Substring(0, 1) == "/")
                {
                    TransferData = ScanCode.Substring(3);
                    ScanCode = ScanCode.Substring(1, 2);
                }
                if (ScanCode.Length == 2) { ctx.ProcessMacroKey(UnitItemKey, Process, ScanCode, UserName, StepUpName, ref RTable, ref RID, ref RValue, ref ReturnMessage); }
                else
                {
                    ctx.ProcessScanCode(UnitItemKey, Process, ScanCode, UserName, StepUpName, ref RTable, ref RID, ref RValue, ref ReturnMessage);
                    //////////////////////////
                    //RTable = "";
                    //RID = "";
                    //RValue = "";
                    //ReturnMessage = "::Unknown Scancode:353323050480283::";
                }
                if (RTable == "ClientLocation")
                {
                    //UserAccessControl uac = new UserAccessControl(UserName);
                    //decimal ID = -1;
                    //if (decimal.TryParse(RID, out ID) == false) { ID = -1; }
                    //if (ID > 0 && uac._IsClientLocationValid(ID) == true) { return RTable + ":" + RID; }
                    //ReturnMessage = "::Unknown Scancode:" + ScanCode + "::";
                    Log.LogIt("Load Client Location - Done (ClientLocation)");
                    return RTable + ":" + RID;
                }

                //We may be scanning in a brand new ESN... We want to check to see if the value is an ESN, not just only if we found a valid esn already in the system.
                if (RTable.ToUpper() == "RECEIVEDETAIL" || RTable.ToUpper() == "")
                {
                    ReceiveDetailManager rm = new ReceiveDetailManager(UserName);
                    string[] mx = rm.GetESNQueuedMessage(ScanCode);
                    MessageQueueStop = mx[0];
                    MessageQueueMessage = mx[1];
                }
                Log.LogIt("ScanCode Parse - Finished (" + ScanCode + ")");
                // example ReturnMessage = "Option:226:Serial Number iPhone:Serial Number:SN:TX"

                // example ReturnMessage = "Option:226:Serial Number iPhone:Serial Number:SN:TX" + ":" + TransferData + ":" + MessageQueueStop + ":" + MessageQueueMessage.Replace(':', ' ');

                return ReturnMessage + ":" + TransferData + ":" + MessageQueueStop + ":" + MessageQueueMessage.Replace(':', ' ');
            }
        }
        #endregion


        #region Conversion Examples
        public void WriteDataXML(string FileName)
        {
            string Text = GetThisDataXML();
            System.IO.File.WriteAllText(FileName, Text);
        }

        public string GetThisDataXML()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(string));
            using (StringWriter textWriter = new StringWriter())
            {
                serializer.Serialize(textWriter, "XXXX");
                return textWriter.ToString();
            }
        }

        public void WriteDataJSON(string FileName)
        {
            string Text = GetThisDataJSON();
            System.IO.File.WriteAllText(FileName, Text);
        }

        public string GetThisDataJSON()
        {
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return serializer.Serialize("this");
        }
        #endregion



        [OperationContract]
        private string SalesOrderESNPicked(string sOrderHeaderID
                                            , string IFSLocation
                                            , string CartonNumber
                                            , string IMEI, string UserName)
        {
            decimal OrderHeaderID = -1;
            decimal.TryParse(sOrderHeaderID, out OrderHeaderID);

            OrderManager OM = new OrderManager(UserName);
            return OM.RecordIFSPickIMEI(OrderHeaderID, IFSLocation, CartonNumber, IMEI, UserName);

            //return "99:44:23:This IMEI (" + IMEI + ") was Picked " + OrderHeaderID.ToString();

            //IFS_InventoryPartsManager im = new IFS_InventoryPartsManager(UserName);
            //return im.LogPhysicalInventoryCount(xBatch, xPartNumber, xIFSLocation, xWarehouse, WarehouseID, QTY, UpdateInventory, UserName);


            //if (sUpdateIMEI == "1") { UpdateIMEI = true; }
            //IFS_InventoryManager im = new IFS_InventoryManager(UserName);
            //return im.LogPhysicalInventoryCount(MasterIFSLocationID, MasterIFSConditionID, ESN, Batch, IFSSite, IFSProject, SKU, IFSLocation, IFSCondition, UpdateIMEI, UserName);
        }


        [OperationContract]
        public string GetDefaultLabDestinationBinNumber(string KeyID)
        {
            decimal ID = -1;
            if (decimal.TryParse(KeyID, out ID) == false) { return ""; }
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                var d = ctx.Option_Text_Defaults.FirstOrDefault(x => x.SourceOptionID == ID);
                if (d != null && d.TargetText.Length > 0)
                {
                    return d.TargetText;
                }
            }
            return "";
        }

        private string CleanData(string Data)
        {
            string DoubleQuote = "\"";
            StringBuilder b = new StringBuilder(Data);
            b.Replace("[", string.Empty);
            b.Replace("]", string.Empty);
            b.Replace("'", string.Empty);
            b.Replace(DoubleQuote, string.Empty);
            b.Replace(@"\", string.Empty);
            //b.Replace("/", string.Empty);   // This needs to be gone because it is causing problems with Dates.
            //b.Replace(Environment.NewLine, string.Empty);
            //b.Replace("\\t", string.Empty);
            //b.Replace(" {", "{");
            //b.Replace(" :", ":");
            //b.Replace(": ", ":");
            //b.Replace(", ", ",");
            //b.Replace("; ", ";");
            //b.Replace(";}", "}");
            return b.ToString();
        }

        [OperationContract]
        public void ClearLogFile()
        {
            Log.Clear();
        }
        [OperationContract]
        public void ClearLogFileUtility()
        {
            Log.Clear("UtilityUpload_01_Log.txt");
        }

        [OperationContract]
        private string LogPhysicalPartCount(string xPartNumber
                                            , string xBatch
            , string xQTY
            , string xIFSLocation
            , string xWarehouse
            , string xWarehouseID
            , string sUpdateInventory
            , string UserName)
        {

            decimal QTY = 0;
            decimal WarehouseID = -1;
            decimal.TryParse(xQTY, out QTY);
            decimal.TryParse(xWarehouseID, out WarehouseID);
            bool UpdateInventory = false;
            if (sUpdateInventory == "1") { UpdateInventory = true; }

            IFS_InventoryPartsManager im = new IFS_InventoryPartsManager(UserName);
            return im.LogPhysicalInventoryCount(xBatch, xPartNumber, xIFSLocation, xWarehouse, WarehouseID, QTY, UpdateInventory, UserName);


            //if (sUpdateIMEI == "1") { UpdateIMEI = true; }
            //IFS_InventoryManager im = new IFS_InventoryManager(UserName);
            //return im.LogPhysicalInventoryCount(MasterIFSLocationID, MasterIFSConditionID, ESN, Batch, IFSSite, IFSProject, SKU, IFSLocation, IFSCondition, UpdateIMEI, UserName);
        }




        [OperationContract]
        public string LogCycleCountScan(string DevicePartIMEI
            , string Batch
            , string xQTY
            , string IsThisaDevice
            , string xWarehouseID
            , string UserName)
        {

            decimal QTY = 0;
            decimal WarehouseID = -1;
            decimal.TryParse(xQTY, out QTY);
            decimal.TryParse(xWarehouseID, out WarehouseID);
            bool isDevice = true;
            if (IsThisaDevice != "1") { isDevice = false; }
            CycleCountManager CM = new CycleCountManager(UserName);
            return CM.LogCycleCountInventoryCount(Batch, QTY, DevicePartIMEI, isDevice, WarehouseID, UserName);
            //if (sUpdateIMEI == "1") { UpdateIMEI = true; }
            //IFS_InventoryManager im = new IFS_InventoryManager(UserName);
            //return im.LogPhysicalInventoryCount(MasterIFSLocationID, MasterIFSConditionID, ESN, Batch, IFSSite, IFSProject, SKU, IFSLocation, IFSCondition, UpdateIMEI, UserName);
        }




        [OperationContract]
        private string GetNewPhysicalDeviceBatch(string UserName)
        {
            DeviceInventoryManager im = new DeviceInventoryManager(UserName);
            return im.GetNextPIBatchNumber();
        }


        [OperationContract]
        private string GetNewCycleCountDeviceBatch(string UserName, string CycleInventoryCountHeaderIDString, string CycleCountName, string CountType, string xIFSLocation)
        {
            decimal CycleInventoryCountHeaderID = -1;
            if (decimal.TryParse(CycleInventoryCountHeaderIDString, out CycleInventoryCountHeaderID) == false) { CycleInventoryCountHeaderID = -1; }
            DeviceInventoryManager im = new DeviceInventoryManager(UserName);
            return im.GetNextCCBatchNumber(CycleInventoryCountHeaderID, CycleCountName, CountType, xIFSLocation);
        }

        [OperationContract]
        private string LogCycleDeviceCount(string CycleInventoryCountHeaderIDString
            , string sMasterIFSLocationID
            , string sMasterIFSCondtionID
            , string CountType
            , string ESN
            , string Batch
            , string IFSSite
            , string IFSProject
            , string SKU
            , string IFSLocation
            , string IFSCondition
            , string Grade
            , string sKitted
            , string sUnlocked
            , string sUpdateIMEI
            , string UserName)
        {
            decimal MasterIFSLocationID = -1;
            decimal MasterIFSConditionID = -1;
            decimal CycleInventoryCountHeaderID = -1;
            bool UpdateIMEI = false;
            bool Kitted = false;
            bool Unlocked = false;
            decimal.TryParse(sMasterIFSLocationID, out MasterIFSLocationID);
            decimal.TryParse(sMasterIFSCondtionID, out MasterIFSConditionID);
            decimal.TryParse(CycleInventoryCountHeaderIDString, out CycleInventoryCountHeaderID);
            if (sUpdateIMEI == "1") { UpdateIMEI = true; }
            if (sKitted == "1") { Kitted = true; }
            if (sUnlocked == "1") { Unlocked = true; }

            DeviceInventoryManager im = new DeviceInventoryManager(UserName);
            return im.LogCycleInventoryCount(CycleInventoryCountHeaderID, MasterIFSLocationID, MasterIFSConditionID, CountType, ESN, Batch, IFSSite, IFSProject, SKU, IFSLocation, IFSCondition, Grade, Kitted, Unlocked, UpdateIMEI, UserName);
        }



        [OperationContract]
        private string LogPhysicalDeviceCount(string sMasterIFSLocationID
                                            , string sMasterIFSCondtionID
                                            , string sProjectID
            , string ESN
            , string Batch
            , string IFSSite
            , string IFSProject
            , string SKU
            , string IFSLocation
            , string IFSCondition
            , string Grade
            , string sKitted
            , string sUnlocked
            , string sUpdateIMEI
            , string UserName)
        {
            decimal MasterIFSLocationID = -1;
            decimal MasterIFSConditionID = -1;
            decimal ProjectID = -1;
            bool UpdateIMEI = false;
            bool Kitted = false;
            bool Unlocked = false;
            decimal.TryParse(sMasterIFSLocationID, out MasterIFSLocationID);
            decimal.TryParse(sMasterIFSCondtionID, out MasterIFSConditionID);
            decimal.TryParse(sProjectID, out ProjectID);
            if (sUpdateIMEI == "1") { UpdateIMEI = true; }
            if (sKitted == "1") { Kitted = true; }
            if (sUnlocked == "1") { Unlocked = true; }

            DeviceInventoryManager im = new DeviceInventoryManager(UserName);
            return im.LogPhysicalInventoryCount(MasterIFSLocationID, MasterIFSConditionID, ProjectID, ESN, Batch, IFSSite, IFSProject, SKU, IFSLocation, IFSCondition, Grade, Kitted, Unlocked, UpdateIMEI, UserName);
        }


        [OperationContract]
        private string LogPhysicalDeviceCount_BKUP(string sMasterIFSLocationID
                                            , string sMasterIFSCondtionID
            , string ESN
            , string Batch
            , string IFSSite
            , string IFSProject
            , string SKU
            , string IFSLocation
            , string IFSCondition
            , string Grade
            , string sKitted
            , string sUnlocked
            , string sUpdateIMEI
            , string UserName)
        {
            decimal MasterIFSLocationID = -1;
            decimal MasterIFSConditionID = -1;
            bool UpdateIMEI = false;
            bool Kitted = false;
            bool Unlocked = false;
            decimal.TryParse(sMasterIFSLocationID, out MasterIFSLocationID);
            decimal.TryParse(sMasterIFSCondtionID, out MasterIFSConditionID);
            if (sUpdateIMEI == "1") { UpdateIMEI = true; }
            if (sKitted == "1") { Kitted = true; }
            if (sUnlocked == "1") { Unlocked = true; }

            DeviceInventoryManager im = new DeviceInventoryManager(UserName);
            return im.LogPhysicalInventoryCountBKUP(MasterIFSLocationID, MasterIFSConditionID, ESN, Batch, IFSSite, IFSProject, SKU, IFSLocation, IFSCondition, Grade, Kitted, Unlocked, UpdateIMEI, UserName);
        }

        [OperationContract]
        public string SwitchIMEI(string sReceiveDetailID, string NewIMEI, string UserName)
        {
            string rValue = "Error:" + sReceiveDetailID + ":No Action Taken!";
            decimal ReceiveDetailID = -1;
            decimal.TryParse(sReceiveDetailID, out ReceiveDetailID);
            if (ReceiveDetailID > 0 && NewIMEI.Length > 0)
            {
                ReceiveDetailManager RDM = new ReceiveDetailManager(UserName);
                rValue = RDM.SwitchIMEI(ReceiveDetailID, NewIMEI);
            }
            return rValue;
        }









        [OperationContract]
        public string AdvanceESNVersion(string ESN, string UserName, string Source)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext()) { return AdvanceESNVersion01(ctx, ESN, UserName, Source); }
        }


        public string AdvanceESNVersion01(clsLinqDataContext ctx, string ESN, string UserName, string Source)
        {
            ctx.AdvanceESNVersion_CleanUpAndLog(ESN, Source, 1, UserName);
            //ctx.AdvanceESNVersion_02(ESN, 1, UserName);


            return "DONE";
        }

        [OperationContract]
        public string TransferInToMSC(string ESN, decimal ReceiveDetailID, decimal ProjectID, decimal ProcessID, decimal ClientLocationID, string UserName)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                ReceiveDetailManager RDM = new ReceiveDetailManager(UserName);
                RDM.CloneToMSC(ctx, ReceiveDetailID, ProjectID, ClientLocationID, ProcessID);
                ReceiveDetail rd = RDM.ReceiveDetail(ctx, ESN);
                return rd.ReceiveDetailID.ToString();
            }
        }
        [OperationContract]
        public string DealerSubmissionESN(string ESN, decimal ReceiveDetailID, decimal ProjectID, decimal ProcessID, decimal ClientLocationID, string UserName)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                ReceiveDetailManager RDM = new ReceiveDetailManager(UserName);
                RDM.Clone(ctx, "2", ReceiveDetailID, ProjectID, ClientLocationID, ProcessID);
                ReceiveDetail rd = RDM.ReceiveDetail(ctx, ESN);
                return rd.ReceiveDetailID.ToString();
            }
        }

        [OperationContract]
        public string GetDashboardReceiveDetail(string sReceiveDetailID, string sReceiveDetailAuthorizationLogID, string UserName)
        {
            JsonString rData = new JsonString();
            rData.AddValuePair("ReceiveDetailID", sReceiveDetailID);

            decimal ReceiveDetailID = -1;
            decimal ReceiveDetailAuthorizationLogID = -1;
            if (decimal.TryParse(sReceiveDetailAuthorizationLogID, out ReceiveDetailAuthorizationLogID) == false) { ReceiveDetailAuthorizationLogID = -1; }
            if (decimal.TryParse(sReceiveDetailID, out ReceiveDetailID) == false)
            {
                ReceiveDetailID = -1;

                //rData.AddValuePair("isAuthorize", "");
                rData.AddValuePair("txtClientReference", "");
                rData.AddValuePair("txtServiceRequestNumber", "");
                rData.AddValuePair("txtEQUSAN", "");

                rData.AddValuePair("txtPartsReturned", "");

                rData.AddValuePair("txtCustomerName", "");
                rData.AddValuePair("txtESN", "");
                rData.AddValuePair("txtWarranty", "");
                rData.AddValuePair("txtOriginalIMEI", "");
                rData.AddValuePair("txtActivationDate", "");
                rData.AddValuePair("txtWarrantyType", "");
                rData.AddValuePair("txtFaultCode", "");
                rData.AddValuePair("txtDateSubmitted", "");
                rData.AddValuePair("txtFaultCode", "");
                rData.AddValuePair("txtFaultCode2", "");
                rData.AddValuePair("txtGMPReceivedDate", "");
                rData.AddValuePair("txtRepairDate", "");
                rData.AddValuePair("txtCurrentProcess", "");

                rData.AddValuePair("txtRepairFee", "");
                rData.AddValuePair("txtCustomerNotes", "");
                rData.AddValuePair("txtAccessoriesShipped", "");
                rData.AddValuePair("txtAccessoriesReceived", "");

                rData.AddValuePair("txtStoreComments", "");


                rData.AddValuePair("txtAuthorizationStatus", "");
                rData.AddValuePair("txtAuthorizationName", "");
                rData.AddValuePair("txtAssessment", "");
                rData.AddValuePair("txtRepairNotes", "");

                rData.AddValuePair("txtGMPMSCShippedDate", "");
                rData.AddValuePair("txtOutBoundWayBill_S", "");
                rData.AddValuePair("txtCourier", "");

                rData.AddValuePair("lblEstimate", "");
                rData.AddValuePair("lblFreight", "");
                rData.AddValuePair("lblHST", "");
                rData.AddValuePair("lblTotal", "");



                return rData.ToString();
            }

            ReceiveDetailManager rdm = new ReceiveDetailManager(UserName);
            using (clsLinqDataContext ctx = rdm.GetDataContext(UserName))
            {
                GridDashboardReceiveDetail_Client GDRD = null;
                ReceiveDetail rd = rdm.ReceiveDetail(ctx, ReceiveDetailID);
                if (rd != null)
                {

                    decimal ProjectClientPortalID = ctx.Projects.Where(x => x.Name.ToUpper() == "CLIENT PORTAL").Select(x => x.ProjectID).FirstOrDefault();
                    decimal ProjectClientRepairID = ctx.Projects.Where(x => x.Name.ToUpper() == "CLIENT REPAIR").Select(x => x.ProjectID).FirstOrDefault();

                    GDRD = new GridDashboardReceiveDetail_Client(ctx, rd, ProjectClientPortalID, ProjectClientRepairID);
                    //rData.AddValuePair("RMANumber", rd.RMANumber.ToString());
                    rData.AddValuePair("txtESN", rd.ESN);
                    //if (rd.ProjectName.ToUpper() != "CLIENT PORTAL") { rData.AddValuePair("txtGMPReceivedDate", ""); }
                    //else { rData.AddValuePair("txtGMPReceivedDate", string.Format("{0:MM/dd/yyyy}", rd.ReceiveDate)); }
                }
                if (GDRD == null || GDRD.GMPIReceivedRD == null)
                {
                    rData.AddValuePair("txtGMPReceivedDate", "");
                    //rData.AddValuePair("isAuthorize", rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "isAuthorize"));
                    rData.AddValuePair("txtClientReference", rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Client Reference #"));
                    rData.AddValuePair("txtServiceRequestNumber", rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Service Request Num"));
                    rData.AddValuePair("txtEQUSAN", rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "EQU SAN"));
                    rData.AddValuePair("txtPartsReturned", rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Parts Returning"));

                    rData.AddValuePair("txtCustomerName", rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Customer Name"));
                    rData.AddValuePair("txtActivationDate", rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Activation Date"));
                    rData.AddValuePair("txtWarrantyType", rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Warranty Type"));
                    rData.AddValuePair("txtCustomerNotes", rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Customer Notes"));
                    rData.AddValuePair("txtAccessoriesShipped", rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Accessories Shipping"));
                    rData.AddValuePair("txtDateSubmitted", GDRD.Dealer_ShipDate);   // <---------------

                    rData.AddValuePair("txtWarranty", rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Warranty No."));
                    rData.AddValuePair("txtOriginalIMEI", rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Original IMEI"));
                    rData.AddValuePair("txtRepairFee", rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Repair Fee"));
                    rData.AddValuePair("txtAccessoriesReceived", rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Accessories Received"));
                    rData.AddValuePair("txtStoreComments", rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Store Comments"));

                    //                    rData.AddValuePair("txtAssessment", rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Assessment"));
                    rData.AddValuePair("txtAssessment", rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Unit Assessment"));
                    rData.AddValuePair("txtRepairNotes", rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Repair Notes"));




                    rData.AddValuePair("txtCurrentProcess", rdm.GetReceiveDetailCurrentProcessNameFriendly(ctx, ReceiveDetailID));
                    //rData.AddValuePair("txtCurrentProcess", rdm.GetReceiveDetailCurrentProcessNameAndDate(ctx, ReceiveDetailID));
                    // rData.AddValuePair("txtCurrentProcess", rdm.GetReceiveDetailCurrentProcessName(ctx, ReceiveDetailID));


                    rData.AddValuePair("txtFaultCode", rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Complaint"));
                    rData.AddValuePair("txtFaultCode2", rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Complaint 2"));
                    rData.AddValuePair("txtAuthorizationStatus", rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Authorization Status"));
                    rData.AddValuePair("txtAuthorizationName", "");
                    //rData.AddValuePair("txtAuthorizationName", rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "AuthorizedName"));

                    rData.AddValuePair("txtGMPMSCShippedDate", "");
                    rData.AddValuePair("txtRepairDate", rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Repair Date"));  // <---------------
                    rData.AddValuePair("txtOutBoundWayBill_S", "");
                    rData.AddValuePair("txtCourier", "");
                    rData.AddValuePair("lblEstimate", "");
                    rData.AddValuePair("lblFreight", "");
                    rData.AddValuePair("lblHST", "");
                    rData.AddValuePair("lblTotal", "");

                }
                else
                {
                    //rData.AddValuePair("isAuthorize", rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "isAuthorize"));
                    rData.AddValuePair("txtGMPReceivedDate", GDRD.ReceiveDate);
                    rData.AddValuePair("txtClientReference", rdm.GetReceiveDetailItem_DataElement(ctx, GDRD.ClientSubmittedRD.ReceiveDetailID, "Client Reference #"));
                    rData.AddValuePair("txtServiceRequestNumber", rdm.GetReceiveDetailItem_DataElement(ctx, GDRD.ClientSubmittedRD.ReceiveDetailID, "Service Request Num"));

                    rData.AddValuePair("txtEQUSAN", rdm.GetReceiveDetailItem_DataElement(ctx, GDRD.ClientSubmittedRD.ReceiveDetailID, "EQU SAN"));
                    rData.AddValuePair("txtPartsReturned", rdm.GetReceiveDetailItem_DataElement(ctx, GDRD.ClientSubmittedRD.ReceiveDetailID, "Parts Returning"));


                    rData.AddValuePair("txtCustomerName", rdm.GetReceiveDetailItem_DataElement(ctx, GDRD.ClientSubmittedRD.ReceiveDetailID, "Customer Name"));
                    rData.AddValuePair("txtActivationDate", rdm.GetReceiveDetailItem_DataElement(ctx, GDRD.ClientSubmittedRD.ReceiveDetailID, "Activation Date"));
                    rData.AddValuePair("txtWarrantyType", rdm.GetReceiveDetailItem_DataElement(ctx, GDRD.ClientSubmittedRD.ReceiveDetailID, "Warranty Type"));
                    rData.AddValuePair("txtOriginalIMEI", rdm.GetReceiveDetailItem_DataElement(ctx, GDRD.ClientSubmittedRD.ReceiveDetailID, "Original IMEI"));



                    rData.AddValuePair("txtCustomerNotes", rdm.GetReceiveDetailItem_DataElement(ctx, GDRD.ClientSubmittedRD.ReceiveDetailID, "Customer Notes"));
                    rData.AddValuePair("txtAccessoriesShipped", rdm.GetReceiveDetailItem_DataElement(ctx, GDRD.ClientSubmittedRD.ReceiveDetailID, "Accessories Shipping"));
                    rData.AddValuePair("txtDateSubmitted", GDRD.Dealer_ShipDate);  // <---------------

                    rData.AddValuePair("txtWarranty", rdm.GetReceiveDetailItem_DataElement(ctx, GDRD.GMPIReceivedRD.ReceiveDetailID, "Warranty No."));
                    rData.AddValuePair("txtRepairFee", rdm.GetReceiveDetailItem_DataElement(ctx, GDRD.GMPIReceivedRD.ReceiveDetailID, "Repair Fee"));
                    rData.AddValuePair("txtAccessoriesReceived", rdm.GetReceiveDetailItem_DataElement(ctx, GDRD.GMPIReceivedRD.ReceiveDetailID, "Accessories Received"));
                    rData.AddValuePair("txtStoreComments", rdm.GetReceiveDetailItem_DataElement(ctx, GDRD.GMPIReceivedRD.ReceiveDetailID, "Store Comments"));


                    //rData.AddValuePair("txtAssessment", rdm.GetReceiveDetailItem_DataElement(ctx, GDRD.GMPIReceivedRD.ReceiveDetailID, "Assessment"));
                    rData.AddValuePair("txtAssessment", rdm.GetReceiveDetailItem_DataElement(ctx, GDRD.GMPIReceivedRD.ReceiveDetailID, "Unit Assessment"));
                    rData.AddValuePair("txtRepairNotes", rdm.GetReceiveDetailItem_DataElement(ctx, GDRD.GMPIReceivedRD.ReceiveDetailID, "Repair Notes"));


                    rData.AddValuePair("txtCurrentProcess", rdm.GetReceiveDetailCurrentProcessNameFriendly(ctx, GDRD.GMPIReceivedRD.ReceiveDetailID));
                    //rData.AddValuePair("txtCurrentProcess", rdm.GetReceiveDetailCurrentProcessNameAndDate(ctx, GDRD.GMPIReceivedRD.ReceiveDetailID));

                    rData.AddValuePair("txtFaultCode", rdm.GetReceiveDetailItem_DataElement(ctx, GDRD.GMPIReceivedRD.ReceiveDetailID, "Complaint"));
                    rData.AddValuePair("txtFaultCode2", rdm.GetReceiveDetailItem_DataElement(ctx, GDRD.GMPIReceivedRD.ReceiveDetailID, "Complaint 2"));

                    string AuthorizationStatus = rdm.GetReceiveDetailItem_DataElement(ctx, GDRD.GMPIReceivedRD.ReceiveDetailID, "Authorization Status");

                    rData.AddValuePair("txtAuthorizationStatus", AuthorizationStatus);
                    if (AuthorizationStatus.ToUpper() == "DECLINED") { rData.AddValuePair("txtAuthorizationName", GDRD.DeclinedName); }
                    else { rData.AddValuePair("txtAuthorizationName", GDRD.AuthorizedName); }


                    rData.AddValuePair("txtGMPMSCShippedDate", rdm.GetReceiveDetailItem_DataElement(ctx, GDRD.GMPIReceivedRD.ReceiveDetailID, "Shipping_Created"));
                    rData.AddValuePair("txtRepairDate", rdm.GetReceiveDetailItem_DataElement(ctx, GDRD.GMPIReceivedRD.ReceiveDetailID, "Repair Date"));  // <---------------
                    rData.AddValuePair("txtOutBoundWayBill_S", rdm.GetReceiveDetailItem_DataElement(ctx, GDRD.GMPIReceivedRD.ReceiveDetailID, "Out-Bound Waybill-S"));
                    rData.AddValuePair("txtCourier", rdm.GetReceiveDetailItem_DataElement(ctx, GDRD.GMPIReceivedRD.ReceiveDetailID, "Courier Out"));


                    // If repair fee < = 0 Do Below
                    string RepairFee = rdm.GetReceiveDetailItem_DataElement(ctx, GDRD.GMPIReceivedRD.ReceiveDetailID, "Repair Fee");
                    decimal nRepairFee = 0;
                    if (decimal.TryParse(RepairFee, out nRepairFee) == false) { nRepairFee = 0; }


                    if (nRepairFee <= 0)
                    {
                        rData.AddValuePair("lblEstimate", string.Format("{0:0.00}", GDRD.FeeEstimate));
                        rData.AddValuePair("lblFreight", string.Format("{0:0.00}", GDRD.FeeFreight));
                        rData.AddValuePair("lblHST", string.Format("{0:0.00}", GDRD.FeeHST));
                        rData.AddValuePair("lblTotal", string.Format("{0:0.00}", GDRD.FeeTotal));
                    }
                    else
                    {
                        rData.AddValuePair("lblEstimate", "");
                        rData.AddValuePair("lblFreight", "");
                        rData.AddValuePair("lblHST", "");
                        rData.AddValuePair("lblTotal", "");
                    }
                }

                return rData.ToString();
            }
        }
        [OperationContract]
        public string GetClientRestrictedQuestions(string sClientLocationID, string sProjectID)
        {
            decimal ClientLocationID = -1;
            decimal ProjectID = -1;
            if (decimal.TryParse(sClientLocationID, out ClientLocationID) == false) { ClientLocationID = -1; }
            if (decimal.TryParse(sProjectID, out ProjectID) == false) { ProjectID = -1; }
            ClientManager CM = new ClientManager("JIM");
            return CM.RestrictedQuestionList(ClientLocationID, ProjectID);
        }
        [OperationContract]
        public string GetManufacturerDropDownData(string Carrier, string UserName)
        {
            JsonString rData = new JsonString();
            MasterCarrierManufacturerModelColourManager MM = new MasterCarrierManufacturerModelColourManager(UserName);
            List<MasterCarrierManufacturerLookup> ml = MM.GetMasterManufacturerList(Carrier);
            foreach (MasterCarrierManufacturerLookup mm in ml) { rData.AddValuePair(mm.OptionManufacturerID.ToString(), mm.Manufacturer); }
            return rData.ToString();
        }

        [OperationContract]
        public string GetModelDropDownData(string Carrier, string Manufacturer, string UserName)
        {
            JsonString rData = new JsonString();
            MasterCarrierManufacturerModelColourManager MM = new MasterCarrierManufacturerModelColourManager(UserName);
            List<MasterCarrierManufacturerLookup> ml = MM.GetMasterModelList(Carrier, Manufacturer);
            foreach (MasterCarrierManufacturerLookup mm in ml.OrderBy(x => x.Model)) { rData.AddValuePair(mm.OptionModelID.ToString(), mm.Model); }
            return rData.ToString();
        }

        [OperationContract]
        public string GetMemoryDropDownData(string Model, string UserName)
        {
            JsonString rData = new JsonString();
            MasterCarrierManufacturerModelColourManager MM = new MasterCarrierManufacturerModelColourManager(UserName);
            List<PairIDValue> ml = MM.GetMasterMemoryList(Model);
            foreach (PairIDValue mm in ml.OrderBy(x => x.Desc)) { rData.AddValuePair(mm.ID.ToString(), mm.Desc); }
            return rData.ToString();
        }


        [OperationContract]
        public string GetColourDropDownData(string Carrier, string Manufacturer, string Model, string UserName)
        {
            JsonString rData = new JsonString();
            MasterCarrierManufacturerModelColourManager MM = new MasterCarrierManufacturerModelColourManager(UserName);
            List<MasterCarrierManufacturerLookup> ml = MM.GetMasterColourList(Carrier, Manufacturer, Model);
            foreach (MasterCarrierManufacturerLookup mm in ml) { rData.AddValuePair(mm.OptionColourID.ToString(), mm.Colour); }
            return rData.ToString();
        }

        [OperationContract]
        public string GetManufacturerDropDownData_WithScanCode(string Carrier, string UserName)
        {
            decimal ID = -1;
            JsonString rData = new JsonString();
            AnswerManager am = new AnswerManager(UserName);
            MasterCarrierManufacturerModelColourManager MM = new MasterCarrierManufacturerModelColourManager(UserName);
            List<MasterCarrierManufacturerLookup> ml = MM.GetMasterManufacturerList(Carrier);
            foreach (MasterCarrierManufacturerLookup mm in ml)
            {
                if (mm.OptionManufacturerID != null) { ID = (decimal)mm.OptionManufacturerID; }
                var o = am.GetAnswer(ID);
                if (o != null) { rData.AddValuePair(mm.OptionManufacturerID.ToString(), "(" + o.ScanKey + ") " + mm.Manufacturer); }
                else { rData.AddValuePair(mm.OptionManufacturerID.ToString(), mm.Manufacturer); }
            }
            return rData.ToString();
        }

        [OperationContract]
        public string GetManufacturerDropDownData_WithScanCode_CarrierNon(string Carrier, string UserName)
        {
            decimal ID = -1;
            JsonString rData = new JsonString();
            AnswerManager am = new AnswerManager(UserName);
            MasterCarrierManufacturerModelColourManager MM = new MasterCarrierManufacturerModelColourManager(UserName);
            List<MasterCarrierManufacturerLookup> ml = MM.GetMasterManufacturerList_NoCarrier(Carrier);
            foreach (MasterCarrierManufacturerLookup mm in ml)
            {
                if (mm.OptionManufacturerID != null) { ID = (decimal)mm.OptionManufacturerID; }
                var o = am.GetAnswer(ID);
                if (o != null) { rData.AddValuePair(mm.OptionManufacturerID.ToString(), "(" + o.ScanKey + ") " + mm.Manufacturer); }
                else { rData.AddValuePair(mm.OptionManufacturerID.ToString(), mm.Manufacturer); }
            }
            return rData.ToString();
        }


        [OperationContract]
        public string GetModelDropDownData_WithScanCode(string Carrier, string Manufacturer, string UserName)
        {
            decimal ID = -1;
            JsonString rData = new JsonString();
            AnswerManager am = new AnswerManager(UserName);
            MasterCarrierManufacturerModelColourManager MM = new MasterCarrierManufacturerModelColourManager(UserName);
            List<MasterCarrierManufacturerLookup> ml = MM.GetMasterModelList(Carrier, Manufacturer);
            foreach (MasterCarrierManufacturerLookup mm in ml)
            {
                if (mm.OptionModelID != null) { ID = (decimal)mm.OptionModelID; }
                var o = am.GetAnswer(ID);
                if (o != null) { rData.AddValuePair(mm.OptionModelID.ToString(), "(" + o.ScanKey + ") " + mm.Model); }
                else { rData.AddValuePair(mm.OptionModelID.ToString(), mm.Model); }
            }
            return rData.ToString();
        }

        [OperationContract]
        public string GetModelDropDownData_WithScanCode_CarrierNon(string Carrier, string Manufacturer, string UserName)
        {
            decimal ID = -1;
            JsonString rData = new JsonString();
            AnswerManager am = new AnswerManager(UserName);
            MasterCarrierManufacturerModelColourManager MM = new MasterCarrierManufacturerModelColourManager(UserName);
            List<MasterCarrierManufacturerLookup> ml = MM.GetMasterModelList_NoCarrier(Carrier, Manufacturer);
            foreach (MasterCarrierManufacturerLookup mm in ml)
            {
                if (mm.OptionModelID != null) { ID = (decimal)mm.OptionModelID; }
                var o = am.GetAnswer(ID);
                if (o != null) { rData.AddValuePair(mm.OptionModelID.ToString(), "(" + o.ScanKey + ") " + mm.Model); }
                else { rData.AddValuePair(mm.OptionModelID.ToString(), mm.Model); }
            }
            return rData.ToString();
        }


        [OperationContract]
        public string GetColourDropDownData_WithScanCode(string Carrier, string Manufacturer, string Model, string UserName)
        {
            decimal ID = -1;
            JsonString rData = new JsonString();
            AnswerManager am = new AnswerManager(UserName);
            MasterCarrierManufacturerModelColourManager MM = new MasterCarrierManufacturerModelColourManager(UserName);
            List<MasterCarrierManufacturerLookup> ml = MM.GetMasterColourList(Carrier, Manufacturer, Model);
            foreach (MasterCarrierManufacturerLookup mm in ml)
            {
                if (mm.OptionColourID != null) { ID = (decimal)mm.OptionColourID; }
                var o = am.GetAnswer(ID);
                if (o != null) { rData.AddValuePair(mm.OptionColourID.ToString(), "(" + o.ScanKey + ") " + mm.Colour); }
                else { rData.AddValuePair(mm.OptionColourID.ToString(), mm.Colour); }
            }
            return rData.ToString();
        }

        //[OperationContract]
        //public void MoveToGraveYard(string sReceiveDetailID, string UserName)
        //{
        //    decimal ReceiveDetailID = -1;
        //    if (decimal.TryParse(sReceiveDetailID, out ReceiveDetailID) == false) { ReceiveDetailID = -1; }
        //    if (ReceiveDetailID > 0)
        //    {
        //        ReceiveDetailManager rm = new ReceiveDetailManager(UserName);
        //        rm.MoveToGraveYard(ReceiveDetailID);
        //    }
        //}
        [OperationContract]
        public void MoveBackFromGraveYard(string sReceiveDetailID, string UserName)
        {
            decimal ReceiveDetailID = -1;
            if (decimal.TryParse(sReceiveDetailID, out ReceiveDetailID) == false) { ReceiveDetailID = -1; }
            if (ReceiveDetailID > 0)
            {
                ReceiveDetailManager rm = new ReceiveDetailManager(UserName);
                rm.MoveBackFromGraveYard(ReceiveDetailID);
            }
        }
        // To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
        // To create an operation that returns XML,
        //     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
        //     and include the following line in the operation body:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";



        [OperationContract]
        public string GetThisESNVersionRecordID(string ESNAndVersion)
        {
            JsonString rString = new JsonString();
            string ESN = "";
            string Version = "";
            string[] mCodes = ESNAndVersion.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            if (mCodes.Length > 1)
            {
                ESN = mCodes[0];
                Version = mCodes[1];
            }
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                var ReceiveDetail = ctx.ReceiveDetails.FirstOrDefault(x => x.ESN == ESN && x.Version == Version);
                if (ReceiveDetail != null) { rString.AddValuePair("ReceiveDetailID", ReceiveDetail.ReceiveDetailID.ToString()); }
                else { rString.AddValuePair("ReceiveDetailID", "-1"); }
            }
            rString.AddValuePair("VersionToLoad", ESNAndVersion);

            return rString.ToString();
        }


        [OperationContract]
        public string GetESNEmail01_Message(string sReceiveDetailID, string Process, string UserName)
        {
            CompanyDemographics c = new CompanyDemographics(UserName);
            string message = "";
            decimal ReceiveDetailID = -1;
            if (decimal.TryParse(sReceiveDetailID, out ReceiveDetailID) == false) { ReceiveDetailID = -1; }
            // ClientManager CM = new ClientManager("JIM");
            ReceiveDetailManager rdm = new ReceiveDetailManager(UserName);
            using (clsLinqDataContext ctx = rdm.GetDataContext(UserName))
            {
                //if ((Process.Length > 9 && Process.Substring(0, 10).ToUpper() == "GMP REPAIR") || (Process.Length > 10 && Process.Substring(0, 11).ToUpper() == "LAB BILLING"))
                if ((Process.Length > 6 && Process.Substring(0, 7).ToUpper() == "_REPAIR") || (Process.Length > 10 && Process.Substring(0, 11).ToUpper() == "LAB BILLING"))
                {
                    ReceiveDetail rd = rdm.ReceiveDetail(ReceiveDetailID);
                    ClientLocation cl = rdm.GetClientLocation(rd.ClientLocationID);
                    if (rd != null && cl != null)
                    {
                        string xmessage = "Hello ";
                        xmessage += cl.CompanyName + "%0A" + "%0A";
                        xmessage += rd.ESN + " is in process and requires your further attention on the dashboard." + "%0A" + "%0A";
                        xmessage += "http://209.15.251.162" + "%0A" + "%0A";
                        //xmessage += "http://209.15.251.162:8080" + "%0A" + "%0A";
                        xmessage += "Thank you," + "%0A";
                        xmessage += c.Name;      // The the Company Name and print it here.  
                        return xmessage;
                    }
                }

                if (Process.Substring(0, 13).ToUpper() == "COMMUNICATION")
                {
                    message = rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Dis State 1");
                    message += " " + rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Dis State 2");
                }
            }
            return message;
        }

        [OperationContract]
        public string RecordEmailContact(string sReceiveDetailID, string Message, string Note, string UserName)
        {
            string message = "";
            decimal ReceiveDetailID = -1;
            if (decimal.TryParse(sReceiveDetailID, out ReceiveDetailID) == false) { ReceiveDetailID = -1; }
            ReceiveDetailManager rdm = new ReceiveDetailManager(UserName);
            using (clsLinqDataContext ctx = rdm.GetDataContext(UserName)) { rdm.AddEmailContact(ctx, ReceiveDetailID, Message, Note); }
            return message;
        }


        [OperationContract]
        public string UpdateReceiveDetailLog_Process(string RDLID, string ProcessID, string ProcessText, string UserName)
        {
            JsonString rData = new JsonString();
            rData.AddValuePair("ReceiveDataLogID", RDLID);
            Int32 ID = 0;
            Int32 PID = 0;
            if (Int32.TryParse(ProcessID, out PID) == false) { PID = -1; }
            if (Int32.TryParse(RDLID, out ID) == true && PID > 0)
            {
                using (clsLinqDataContext ctx = new clsLinqDataContext()) { ReceiveDetailProcessLogManager rdplm = new ReceiveDetailProcessLogManager(UserName); rdplm.ChangeLogProcess(ctx, ID, PID, ProcessText); }
            }
            return rData.ToString();
        }

        [OperationContract]
        public string CanUnitJumpProjects(string ProjectID, string ProcessName)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                if (ProcessName.Length == 0) { return "false"; }
                decimal pid = 0;
                if (decimal.TryParse(ProjectID, out pid) == false) { pid = -1; }
                if (pid < 1) { return "false"; }
                var Process = ctx.Processes.FirstOrDefault(x => x.Name.ToUpper() == ProcessName.ToUpper() && x.CanJumpProject == true);
                if (Process == null) { return "false"; }
                var exists = ctx.ProjectProcesses.FirstOrDefault(x => x.ProjectID == pid && x.ProcessID == Process.ProcessID);
                if (exists != null) { return "true"; }
                return "false";
            }
            // return "false";
        }

        [OperationContract]
        public string GetDetailSheetData(string sRDID, string UserName, bool BumpVersionTo900)
        {
            TimeLogManager timelog = new TimeLogManager(UserName, "");
            timelog.StartTimer();
            Log.LogIt("(Get ESN Data) GetDetailSheetData - Started (" + sRDID + ") ******************");
            string sBumpVersionID = sRDID;
            decimal BumpVersionID = -1;
            if (BumpVersionTo900 == true)
            {
                sRDID = "-1";
                decimal.TryParse(sBumpVersionID, out BumpVersionID);
            }
            JsonString rData = new JsonString();
            rData.AddValuePair("ReceiveDataID", sRDID);
            Int32 ID = 0;
            if (Int32.TryParse(sRDID, out ID) == true)
            {
                using (clsLinqDataContext ctx = new clsLinqDataContext())
                {
                    ReceiveDetailManager rm = new ReceiveDetailManager(UserName);
                    string rMessage = "";
                    if (rm.IsThisFrozen(ID, ref rMessage) == true)
                    {
                        rData.AddValuePair("ESN", rMessage);
                        return rData.ToString();
                    }


                    ReceiveDetail dl = rm.ReceiveDetail(ctx, ID);
                    if (dl != null)
                    {
                        rData.AddValuePair("hdnOpenTime", DateTime.Now.ToString());


                        Project proj = ctx.Projects.FirstOrDefault(x => x.ProjectID == dl.ProjectID);
                        rData.AddValuePair("RHID", dl.ReceiveHeaderID.ToString());
                        rData.AddValuePair("RDBID", dl.ReceiveDetailBulkID.ToString());
                        rData.AddValuePair("O_VERSION", rm.HasOtherVersions(ctx, dl.ESN).ToString());
                        rData.AddValuePair("NEEDHCA", rm.RequiresHardCopyAuthorization(ctx, dl.ReceiveDetailID));
                        rData.AddValuePair("RDID", dl.ReceiveDetailID.ToString());
                        Process pl = ctx.Processes.FirstOrDefault(x => x.ProcessID == dl.ProcessID);
                        if (pl == null) { pl = ctx.Processes.FirstOrDefault(x => x.Name == "Save"); }
                        if (pl != null)
                        {
                            rData.AddValuePair("CurP", pl.Name);
                            rData.AddValuePair("CurProcessID", dl.ProcessID.ToString());
                        }
                        else
                        {
                            rData.AddValuePair("CurP", "Save");
                            rData.AddValuePair("CurProcessID", "35");
                        }

                        if (dl.ClientLocationID < 1)
                        {
                            rData.AddValuePair("CLID", "-1");
                            rData.AddValuePair("CLScanKey", "");

                            rData.AddValuePair("CLIENTID", "-1");
                            rData.AddValuePair("CNAME", "NO CLIENT SET");
                            rData.AddValuePair("CNUM", "");
                            rData.AddValuePair("CSUF", "");
                            rData.AddValuePair("CADD", "");
                            rData.AddValuePair("Email", "");
                            rData.AddValuePair("Email2", "");
                            rData.AddValuePair("ProcessDependencies", "");
                            rData.AddValuePair("ProjectDependencies", "");
                            rData.AddValuePair("SetShippingDefaults", "N");

                        }
                        else
                        {
                            ClientLocation cl = ctx.ClientLocations.FirstOrDefault(x => x.ClientLocationID == dl.ClientLocationID);
                            HtmlString hs = new HtmlString(cl.Client.Name + " " + cl.CompanyName);
                            rData.AddValuePair("CLID", dl.ClientLocationID.ToString());
                            rData.AddValuePair("CLScanKey", dl.ClientLocation.ScanKey);
                            rData.AddValuePair("CLIENTID", cl.ClientID.ToString());
                            rData.AddValuePair("CNAME", hs.ToHtmlString());
                            rData.AddValuePair("CNUM", cl.StoreNumber);
                            rData.AddValuePair("CSUF", cl.StoreSuffix);
                            rData.AddValuePair("CADD", cl.AddressLine1 + @"\n" + cl.AddressLine2 + @"\n" + cl.City + @"\n" + cl.StateOrProvince + @"\n" + cl.PostalCode);
                            rData.AddValuePair("Email", cl.EmailAddress);
                            rData.AddValuePair("Email2", cl.EmailAddress2);
                            //Log.LogIt("Email Address Set to:" + cl.EmailAddress);
                            string ProjectDependencies = "";
                            List<ClientProjectDependency> projectDependencies = (from x in ctx.ClientProjectDependencies where x.ClientID == cl.ClientID select x).ToList();
                            foreach (ClientProjectDependency cpd in projectDependencies)
                            {
                                if (ProjectDependencies.Length == 0) { ProjectDependencies = " "; }
                                ProjectDependencies += cpd.ProjectID.ToString() + " ";
                            }
                            string ProcessDependencies = "";
                            List<ClientProcessDependency> processDependencies = (from x in ctx.ClientProcessDependencies where x.ClientID == cl.ClientID select x).ToList();
                            foreach (ClientProcessDependency cpd in processDependencies)
                            {
                                if (ProcessDependencies.Length == 0) { ProcessDependencies = " "; }
                                ProcessDependencies += cpd.ProcessID.ToString() + " ";
                            }
                            rData.AddValuePair("ProjectDependencies", ProjectDependencies);
                            rData.AddValuePair("ProcessDependencies", ProcessDependencies);


                            if (cl.Client.CompanyName.ToUpper() == "xx1")
                            {
                                rData.AddValuePair("SetShippingDefaults", "Y");
                                rData.AddValuePair("DealerID", dl.ClientLocation.ScanKey);
                                rData.AddValuePair("ServiceRequestNum", rm.GetReceiveDetailItem_DataElement(ctx, ID, "Client Reference #"));
                                Option oPO = ctx.Options.FirstOrDefault(x => x.Question.Name.ToUpper() == "PO NO");
                                Option oSH = ctx.Options.FirstOrDefault(x => x.Question.Name.ToUpper() == "ShipTo");
                                if (oSH != null) { rData.AddValuePair("O_ShipToID", "TX_" + oSH.OptionID.ToString()); }
                                else { rData.AddValuePair("O_ShipToID", ""); }
                                if (oPO != null) { rData.AddValuePair("O_PO", "TX_" + oPO.OptionID.ToString()); }
                                else { rData.AddValuePair("O_PO", ""); }
                            }
                            else
                            {
                                rData.AddValuePair("SetShippingDefaults", "N");
                            }
                        }
                        string MakeModelString = rm.MakeModelColourNickName(ctx, dl.ReceiveDetailID);
                        rData.AddValuePair("MMS", MakeModelString);
                        MakeModelString = rm.GetProjectClientLocationBinString(ctx, dl.ReceiveDetailID);
                        rData.AddValuePair("PCLB", MakeModelString);                           //lblProjectClientLocationBinTitle
                        // If they change the project name under project, we still want it to record as the new one.
                        rData.AddValuePair("Project", proj.Name);
                        if (proj.AllowProjectPassThrough == true)
                        {
                            List<ProjectProcess> projp = proj.ProjectProcesses.ToList();
                            string PP = ",";
                            foreach (ProjectProcess x in projp) { PP += x.ProcessID.ToString() + ","; }
                            rData.AddValuePair("ProjPT", "True");    // Project Pass Through
                            rData.AddValuePair("ProjPTVP", PP);     // Project Pass Through Valid Processes
                        }
                        else
                        {
                            rData.AddValuePair("ProjPT", "False");    // Project Pass Through
                            rData.AddValuePair("ProjPTVP", "");    // Project Pass Through Valid Process
                        }
                        //rData.AddValuePair("Project", dl.ProjectName);
                        // -------------------------------------------------------------------------------------------
                        rData.AddValuePair("ProjectID", dl.ProjectID.ToString());
                        // ReceiveDetail dl = ctx.ReceiveDetails.FirstOrDefault(x => x.ReceiveDetailID == ID);
                        //rData.AddValuePair("Status", dl.ReceiveDetailStatus.Status);
                        rData.AddValuePair("Status", rm.StatusName(ctx, dl.StatusID));
                        rData.AddValuePair("StatusID", dl.StatusID.ToString());

                        rData.AddValuePair("ReceiveDate", dl.CreateDate.ToShortDateString() + " " + dl.CreateDate.ToShortTimeString());
                        rData.AddValuePair("ReceiveTime", dl.CreateDate.ToShortTimeString());
                        rData.AddValuePair("RMA", dl.RMANumber);
                        rData.AddValuePair("PROJTAG", dl.ProjectTag);
                        rData.AddValuePair("ESN", dl.ESN);
                        rData.AddValuePair("ESNVERSION", dl.Version);
                        //rData.AddValuePair("ICB", dl.ICB);
                        //rData.AddValuePair("PIN", dl.PIN);
                        rData.AddValuePair("QTY", dl.QTYIntegrated.ToString());
                        rData.AddValuePair("SUFD", rm.GetSetUpFieldDef(ctx, (decimal)dl.ProjectID));

                        rData.AddValuePair("CompProcList", ctx.fn_GetRequestProcessCompletionList(dl.ReceiveDetailID));

                        rData.AddValuePair("CarrierID", dl.CarrierID.ToString());
                        rData.AddValuePair("ManufactuerID", dl.ManufacturerID.ToString());
                        rData.AddValuePair("ModelID", dl.ModelID.ToString());
                        rData.AddValuePair("ColourID", dl.ColourID.ToString());

                        // We now need to loop through the detail and drop that into the "MIX".
                        Log.LogIt("GetDetailSheetData - Step 1 Master Record Data loaded");

                        bool Keep = false;
                        IQueryable<ReceiveDetailItem> ops = from x in ctx.ReceiveDetailItems where x.ReceiveDetailID == dl.ReceiveDetailID && x.Version == 0 select x;
                        if (ops != null)
                        {
                            foreach (ReceiveDetailItem op in ops)
                            {
                                Keep = true;
                                string Key = "";
                                string Value = "";
                                string QuestionType = op.Option.Question.QuestionType.Type;
                                switch (QuestionType.ToUpper())
                                {
                                    case "DROPDOWN":
                                        Key += "DD_" + op.OptionID.ToString();
                                        if (op.Value == "1") { Value = "1"; }
                                        else { Value = "0"; Keep = false; }
                                        break;
                                    case "CHECKBOX":
                                        Key += "CB_" + op.OptionID.ToString();
                                        if (op.Value == "1") { Value = "1"; }
                                        else { Value = "0"; Keep = false; }
                                        break;
                                    case "RADIALBUTTON":
                                        Key += "RD_" + op.OptionID.ToString();
                                        if (op.Value == "1") { Value = "1"; }
                                        else { Value = "0"; Keep = false; }
                                        break;
                                    case "CALC":
                                        Key += "TX_" + op.OptionID.ToString();
                                        Value = rData.CleanData(op.Value);
                                        break;
                                    default:
                                        Key += "TX_" + op.OptionID.ToString();
                                        Value = rData.CleanData(op.Value);
                                        break;
                                }
                                if (Keep == true)
                                {
                                    rData.AddValuePair(Key, Value);
                                }
                            }
                            Log.LogIt("GetDetailSheetData - Step 2 Detail Records loaded");
                        }
                    }
                    else
                    {
                        rData.AddValuePair("ESN", "Access Denied!");
                    }
                }
            }
            Log.LogIt("GetDetailSheetData - Finished (" + sRDID + ")");
            timelog.StopTimer();
            timelog.SaveTimeLogWorkScreenLoad(ID, 1, rData.ToString());
            return rData.ToString();


            //16/4/2018 12:33:59.79: ScanCode Parse - Started (358212070494470) ******************.
            //16/4/2018 12:33:59.80: ScanCode Parse - Finished (358212070494470).
            //16/4/2018 12:33:59.94: (Get ESN Data) GetDetailSheetData - Started (91565) ******************.
            //16/4/2018 12:34:00.11: Email Address Set to:ops_ninja@outlook.com.
            //16/4/2018 12:34:00.45: GetDetailSheetData - Step 1 Master Record Data loaded.
            //16/4/2018 12:34:00.47: GetDetailSheetData - Step 2 Detail Records loaded.
            //16/4/2018 12:34:00.47: GetDetailSheetData - Finished (91565).


        }





        [OperationContract]
        public string GetReceiveDetailCompletedProcessList(string CLID)
        {
            string rValue = "";
            Int32 ID = 0;
            if (Int32.TryParse(CLID, out ID) == true)
            {
                System.Configuration.ConnectionStringSettingsCollection xconnectionString = WebConfigurationManager.ConnectionStrings;
                //if (xconnectionString != null) { ConnectionString = xconnectionString["GMP_DataEntities"].ConnectionString.ToString(); }
                if (xconnectionString != null) { ConnectionString = xconnectionString["DefaultConnectionString"].ConnectionString.ToString(); }
                using (clsLinqDataContext ctx = new clsLinqDataContext()) { rValue = ctx.fn_GetRequestProcessCompletionList(ID); }
            }
            return rValue;
        }





        #region IFS PO Specific

        public string getPODetailPickLineHTML(string LineData)
        {
            string[] rawdta = LineData.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            List<string> linedata = rawdta[2].Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries).ToList();


            //------------------------------------------------------------------------------------------
            string HeaderHTML = "<table style='width: 100%; border-collapse: collapse;'";
            HeaderHTML += "class='mGrid' border='1' rules='all' cellspacing='0'>";
            HeaderHTML += "<tbody>";
            HeaderHTML += "<tr>";
            HeaderHTML += "<th scope='col'>Select</th>";
            HeaderHTML += "<th scope='col' align='left'>PO Line</th>";
            HeaderHTML += "<th scope='col' align='left'>Condition</th>";
            HeaderHTML += "<th scope='col'>QTY</th>";
            HeaderHTML += "<th scope='col'>Left</th>";
            HeaderHTML += "</tr>";

            string FooterHTML = "</tbody>";
            FooterHTML += "</table>";
            //--------------------------------------------------------------------------------------------
            string HTML = "";
            decimal count = 0;
            foreach (string line in linedata)
            {
                string[] dta = line.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                if (count == 1) { HTML += "<tr class='alt'>"; count = 0; }
                else { HTML += "<tr>"; count = 1; }
                HTML += "<td align='center'>";
                if (dta[0].Length > 0)
                {
                    HTML += @"<input id='Button1' type='button' onclick=""PickPOLine('";
                    HTML += dta[0] + ',';
                    HTML += rawdta[3];              // This is the PO Line Element Value. So it can be set from the Pick
                    HTML += @"');"" value='pick'/>";
                }
                HTML += "</td>";
                HTML += "<td align='left'>" + dta[0] + "</td>";
                HTML += "<td align='left'>" + dta[3] + "</td>";
                HTML += "<td align='left'>" + dta[1].ToString() + "</td>";
                HTML += "<td align='left'>" + dta[2].ToString() + "</td>";
                HTML += "</tr>";
            }
            string rvalue = HeaderHTML + HTML + FooterHTML;          // +"|";
            return rvalue;      // +HeaderHTMLPicked + FooterHTMLPicked;
        }

        [OperationContract]
        public string getPODetailPickLines_HTML(string PONumber, string POVendor, string UserName)
        {
            //decimal HeaderID = -1;
            //if (decimal.TryParse(IFSPurchaseOrderHeaderID, out HeaderID) == false) { HeaderID = -1; }

            DeviceInventoryManager IFS_IM = new DeviceInventoryManager(UserName);
            //if (mpm.IsValidReturnTypeForParts(ReceiveDetailID) == false)
            //{
            //    string rType = mpm.GetReturnTypeForParts(ReceiveDetailID);
            //    return "Invalid Receipt Type(" + rType + "), Correct then try again";
            //}

            //List<IFSPurchaseOrderDetail> PoLines = IFS_IM.GetAllPODetailLines(PONumber);
            List<IFSPurchaseOrderDetail> PoLines = IFS_IM.GetOpenPODetailLines(PONumber);

            //string[] rawdta = LineData.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            //List<string> linedata = rawdta[2].Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries).ToList();


            //------------------------------------------------------------------------------------------
            string HeaderHTML = "<table style='width: 100%; border-collapse: collapse;'";
            HeaderHTML += "class='mGrid' border='1' rules='all' cellspacing='0'>";
            HeaderHTML += "<tbody>";
            HeaderHTML += "<tr>";
            HeaderHTML += "<th scope='col'>Select</th>";
            HeaderHTML += "<th scope='col' align='left'>PO Line</th>";
            HeaderHTML += "<th scope='col' align='left'>SKU</th>";
            HeaderHTML += "<th scope='col' align='left'>Condition</th>";
            HeaderHTML += "<th scope='col'>QTY</th>";
            HeaderHTML += "<th scope='col'>Left</th>";
            HeaderHTML += "</tr>";

            string FooterHTML = "</tbody>";
            FooterHTML += "</table>";
            //--------------------------------------------------------------------------------------------
            string HTML = "";
            decimal count = 0;
            foreach (IFSPurchaseOrderDetail line in PoLines.OrderBy(x => x.POLineLineNo.PadLeft(10, '0')))
            {
                //string[] dta = line.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                if (count == 1) { HTML += "<tr class='alt'>"; count = 0; }
                else { HTML += "<tr>"; count = 1; }
                HTML += "<td align='center'>";
                if (line.POLineLineNo.Length > 0)
                {
                    HTML += @"<input id='Button1' type='button' onclick=""PickPOLineSave('";
                    HTML += line.PONumberOrderNo.Replace("'", "") + '|';
                    HTML += POVendor.Replace("'", "") + '|';
                    HTML += line.POLineLineNo.Replace("'", "") + '|';
                    HTML += line.CONDITION_CODE == null ? "" : line.CONDITION_CODE.Replace("'", "") + '|';
                    HTML += line.POCostPrice.ToString().Replace("'", "");
                    HTML += @"');"" value='pick'/>";
                }
                HTML += "</td>";
                HTML += "<td align='left'>" + line.POLineLineNo + "</td>";
                HTML += "<td align='left'>" + line.SKUPartNo + "</td>";
                HTML += "<td align='left'>" + line.CONDITION_CODE + "</td>";
                HTML += "<td align='left'>" + line.QTYOrderQty.ToString() + "</td>";
                HTML += "<td align='left'>" + (line.QTYOrderQty - line.QTYPPicked).ToString() + "</td>";
                HTML += "</tr>";
            }
            string rvalue = HeaderHTML + HTML + FooterHTML;          // +"|";
            return rvalue;      // +HeaderHTMLPicked + FooterHTMLPicked;
        }

        [OperationContract]
        //public string GetIFSPONumberListData_03(string UserName, string sClientID, string sClientLocationID, string sCarrierID, string sManufacturerID, string sModelID, string sReceiveDetailID)
        public string GetIFSPONumberListData_03(string sClientLocationID, string UserName)
        {
            //bool TechAssignedListOnly = Roles.IsUserInRole(UserName, "RemoteTech") == false ? true : false;
            //decimal ClientID = -1;
            //decimal CarrierID = -1;
            //decimal ManufacturerID = -1;
            //decimal ModelID = -1;
            decimal ClientLocationID = -1;
            //decimal ReceiveDetailID = -1;
            //if (decimal.TryParse(sClientID, out ClientID) == false) { ClientID = -1; }
            if (decimal.TryParse(sClientLocationID, out ClientLocationID) == false) { ClientLocationID = -1; }
            //if (decimal.TryParse(sCarrierID, out CarrierID) == false) { CarrierID = -1; }
            //if (decimal.TryParse(sManufacturerID, out ManufacturerID) == false) { ManufacturerID = -1; }
            //if (decimal.TryParse(sModelID, out ModelID) == false) { ModelID = -1; }
            //if (decimal.TryParse(sReceiveDetailID, out ReceiveDetailID) == false) { ReceiveDetailID = -1; }



            DeviceInventoryManager IFS_IM = new DeviceInventoryManager(UserName);
            //if (mpm.IsValidReturnTypeForParts(ReceiveDetailID) == false)
            //{
            //    string rType = mpm.GetReturnTypeForParts(ReceiveDetailID);
            //    return "Invalid Receipt Type(" + rType + "), Correct then try again";
            //}

            List<IFSPurchaseOrderHeader> PoNum = IFS_IM.GetOpenPOListDevice(ClientLocationID);


            if (PoNum == null) { return ""; }
            //string HeaderHTMLPicked = "<table id='PickedList' style='width: 100%; border-collapse: collapse;'";
            //HeaderHTMLPicked += "class='mGrid' border='1' rules='all' cellspacing='0'>";
            //HeaderHTMLPicked += "<tbody>";
            //HeaderHTMLPicked += "<tr>";
            //HeaderHTMLPicked += "<th scope='col' align='left'></th>";
            //HeaderHTMLPicked += "<th scope='col' align='left'>Selected Parts</th>";
            //HeaderHTMLPicked += "</tr>";

            //string FooterHTMLPicked = "</tbody>";
            //FooterHTMLPicked += "</table>";

            //------------------------------------------------------------------------------------------
            string HeaderHTML = "<table style='width: 100%; border-collapse: collapse;'";
            HeaderHTML += "class='mGrid' border='1' rules='all' cellspacing='0'>";
            HeaderHTML += "<tbody>";
            HeaderHTML += "<tr>";
            HeaderHTML += "<th scope='col'>Select</th>";
            HeaderHTML += "<th scope='col' align='left'>PO Number</th>";
            HeaderHTML += "<th scope='col' align='left'>Vendor</th>";
            HeaderHTML += "<th scope='col'>Supplier</th>";
            HeaderHTML += "<th scope='col'>Supplier PO#</th>";
            HeaderHTML += "<th scope='col'>QTY</th>";
            HeaderHTML += "<th scope='col'>Left</th>";
            HeaderHTML += "</tr>";

            string FooterHTML = "</tbody>";
            FooterHTML += "</table>";
            //--------------------------------------------------------------------------------------------
            string HTML = "";
            decimal count = 0;
            foreach (IFSPurchaseOrderHeader pn in PoNum)
            {
                if (pn.PONumberOrderNo == null) { pn.PONumberOrderNo = ""; }
                if (pn.POVendorSupplierID == null) { pn.POVendorSupplierID = ""; }
                if (pn.SUPPLIER_NAME == null) { pn.SUPPLIER_NAME = ""; }
                if (pn.SupplierOrderNo == null) { pn.SupplierOrderNo = ""; }

                if (count == 1) { HTML += "<tr class='alt'>"; count = 0; }
                else { HTML += "<tr>"; count = 1; }
                HTML += "<td align='center'>";
                if (pn.PONumberOrderNo.Length > 0)
                {
                    //if (pn.Stock > 0)
                    //{
                    HTML += @"<input id='Button1' type='button' onclick=""PickPOSave('";
                    HTML += pn.PONumberOrderNo.Replace("'", "") + '|';
                    HTML += pn.POVendorSupplierID.Replace("'", "");
                    HTML += @"');"" value='pick'/>";
                    //}
                    //else
                    //{
                    //    HTML += @"<input id='Button1' type='button' disabled='true' onclick=""displayResult('PickedList','";
                    //    HTML += pn.PartNumber.Replace("'", "") + " " + pn.Description.Replace("'", "") + ":id=" + pn.MasterPartsLinkTableID.ToString() + @"');"" value='----'/>";
                    //}
                }
                HTML += "</td>";


                HTML += "<td align='left'>" + pn.PONumberOrderNo + "</td>";
                HTML += "<td align='left'>" + pn.POVendorSupplierID.Trim() + "</td>";
                HTML += "<td align='left'>" + pn.SUPPLIER_NAME.Trim() + "</td>";
                HTML += "<td align='center'><span >" + pn.SupplierOrderNo + "</span>" + "</td>";
                HTML += "<td align='left'>" + pn.QTY.ToString() + "</td>";
                HTML += "<td align='left'>" + pn.QTYOpen.ToString() + "</td>";


                HTML += "</tr>";
            }
            string rvalue = HeaderHTML + HTML + FooterHTML;          // +"|";
            return rvalue;      // +HeaderHTMLPicked + FooterHTMLPicked;
            //return HeaderHTML + FooterHTML;

        }

        [OperationContract]
        //public string GetIFSPONumberListData_03(string UserName, string sClientID, string sClientLocationID, string sCarrierID, string sManufacturerID, string sModelID, string sReceiveDetailID)
        public string GetIFSPONumberListData_ForLinePick(string sClientLocationID, string UserName)
        {
            //bool TechAssignedListOnly = Roles.IsUserInRole(UserName, "RemoteTech") == false ? true : false;
            //decimal ClientID = -1;
            //decimal CarrierID = -1;
            //decimal ManufacturerID = -1;
            //decimal ModelID = -1;
            decimal ClientLocationID = -1;
            //decimal ReceiveDetailID = -1;
            //if (decimal.TryParse(sClientID, out ClientID) == false) { ClientID = -1; }
            if (decimal.TryParse(sClientLocationID, out ClientLocationID) == false) { ClientLocationID = -1; }
            //if (decimal.TryParse(sCarrierID, out CarrierID) == false) { CarrierID = -1; }
            //if (decimal.TryParse(sManufacturerID, out ManufacturerID) == false) { ManufacturerID = -1; }
            //if (decimal.TryParse(sModelID, out ModelID) == false) { ModelID = -1; }
            //if (decimal.TryParse(sReceiveDetailID, out ReceiveDetailID) == false) { ReceiveDetailID = -1; }



            DeviceInventoryManager IFS_IM = new DeviceInventoryManager(UserName);
            //if (mpm.IsValidReturnTypeForParts(ReceiveDetailID) == false)
            //{
            //    string rType = mpm.GetReturnTypeForParts(ReceiveDetailID);
            //    return "Invalid Receipt Type(" + rType + "), Correct then try again";
            //}

            List<IFSPurchaseOrderHeader> PoNum = IFS_IM.GetOpenPOListDevice(ClientLocationID);


            if (PoNum == null) { return ""; }
            //string HeaderHTMLPicked = "<table id='PickedList' style='width: 100%; border-collapse: collapse;'";
            //HeaderHTMLPicked += "class='mGrid' border='1' rules='all' cellspacing='0'>";
            //HeaderHTMLPicked += "<tbody>";
            //HeaderHTMLPicked += "<tr>";
            //HeaderHTMLPicked += "<th scope='col' align='left'></th>";
            //HeaderHTMLPicked += "<th scope='col' align='left'>Selected Parts</th>";
            //HeaderHTMLPicked += "</tr>";

            //string FooterHTMLPicked = "</tbody>";
            //FooterHTMLPicked += "</table>";

            //------------------------------------------------------------------------------------------
            string HeaderHTML = "<table style='width: 100%; border-collapse: collapse;'";
            HeaderHTML += "class='mGrid' border='1' rules='all' cellspacing='0'>";
            HeaderHTML += "<tbody>";
            HeaderHTML += "<tr>";
            HeaderHTML += "<th scope='col'>Select</th>";
            HeaderHTML += "<th scope='col' align='left'>PO Number</th>";
            HeaderHTML += "<th scope='col' align='left'>Vendor</th>";
            HeaderHTML += "<th scope='col'>Supplier</th>";
            HeaderHTML += "<th scope='col'>Supplier PO#</th>";
            HeaderHTML += "<th scope='col'>QTY</th>";
            HeaderHTML += "<th scope='col'>Left</th>";
            HeaderHTML += "</tr>";

            string FooterHTML = "</tbody>";
            FooterHTML += "</table>";
            //--------------------------------------------------------------------------------------------
            string HTML = "";
            decimal count = 0;
            foreach (IFSPurchaseOrderHeader pn in PoNum)
            {
                if (pn.PONumberOrderNo == null) { pn.PONumberOrderNo = ""; }
                if (pn.POVendorSupplierID == null) { pn.POVendorSupplierID = ""; }
                if (pn.SUPPLIER_NAME == null) { pn.SUPPLIER_NAME = ""; }
                if (pn.SupplierOrderNo == null) { pn.SupplierOrderNo = ""; }

                if (count == 1) { HTML += "<tr class='alt'>"; count = 0; }
                else { HTML += "<tr>"; count = 1; }
                HTML += "<td align='center'>";
                if (pn.PONumberOrderNo.Length > 0)
                {
                    //if (pn.Stock > 0)
                    //{
                    HTML += @"<input id='Button1' type='button' onclick=""PickPOOpenLines('";
                    HTML += pn.PONumberOrderNo.ToString() + '|';
                    HTML += pn.POVendorSupplierID.ToString();
                    HTML += @"');"" value='Select'/>";
                    //}
                    //else
                    //{
                    //    HTML += @"<input id='Button1' type='button' disabled='true' onclick=""displayResult('PickedList','";
                    //    HTML += pn.PartNumber.Replace("'", "") + " " + pn.Description.Replace("'", "") + ":id=" + pn.MasterPartsLinkTableID.ToString() + @"');"" value='----'/>";
                    //}
                }
                HTML += "</td>";


                HTML += "<td align='left'>" + pn.PONumberOrderNo + "</td>";
                HTML += "<td align='left'>" + pn.POVendorSupplierID.Trim() + "</td>";
                HTML += "<td align='left'>" + pn.SUPPLIER_NAME.Trim() + "</td>";
                HTML += "<td align='center'><span >" + pn.SupplierOrderNo + "</span>" + "</td>";
                HTML += "<td align='left'>" + pn.QTY.ToString() + "</td>";
                HTML += "<td align='left'>" + pn.QTYOpen.ToString() + "</td>";


                HTML += "</tr>";
            }
            string rvalue = HeaderHTML + HTML + FooterHTML;          // +"|";
            return rvalue;      // +HeaderHTMLPicked + FooterHTMLPicked;
            //return HeaderHTML + FooterHTML;

        }


        #endregion




        #region Parts
        [OperationContract]
        public string GetPartNumberListData_03(string UserName, string sClientID, string sClientLocationID, string sCarrierID, string sManufacturerID, string sModelID, string sReceiveDetailID)
        {
            bool TechAssignedListOnly = Roles.IsUserInRole(UserName, "RemoteTech") == false ? true : false;
            decimal ClientID = -1;
            decimal CarrierID = -1;
            decimal ManufacturerID = -1;
            decimal ModelID = -1;
            decimal ClientLocationID = -1;
            decimal ReceiveDetailID = -1;
            if (decimal.TryParse(sClientID, out ClientID) == false) { ClientID = -1; }
            if (decimal.TryParse(sClientLocationID, out ClientLocationID) == false) { ClientLocationID = -1; }
            if (decimal.TryParse(sCarrierID, out CarrierID) == false) { CarrierID = -1; }
            if (decimal.TryParse(sManufacturerID, out ManufacturerID) == false) { ManufacturerID = -1; }
            if (decimal.TryParse(sModelID, out ModelID) == false) { ModelID = -1; }
            if (decimal.TryParse(sReceiveDetailID, out ReceiveDetailID) == false) { ReceiveDetailID = -1; }
            MasterPartManager mpm = new MasterPartManager(UserName);
            if (mpm.IsValidReturnTypeForParts(ReceiveDetailID) == false)
            {
                string rType = mpm.GetReturnTypeForParts(ReceiveDetailID);
                return "Invalid Return Type(" + rType + "), Correct then try again";
            }

            List<FullPartNumberList> PN = mpm.GetFullPartNumberList_03(ReceiveDetailID, ClientID, ClientLocationID, CarrierID, ManufacturerID, ModelID, TechAssignedListOnly);
            if (PN == null) { return ""; }
            string HeaderHTMLPicked = "<table id='PickedList' style='width: 100%; border-collapse: collapse;'";
            HeaderHTMLPicked += "class='mGrid' border='1' rules='all' cellspacing='0'>";
            HeaderHTMLPicked += "<tbody>";
            HeaderHTMLPicked += "<tr>";
            HeaderHTMLPicked += "<th scope='col' align='left'></th>";
            HeaderHTMLPicked += "<th scope='col' align='left'>Selected Parts</th>";
            HeaderHTMLPicked += "</tr>";



            string FooterHTMLPicked = "</tbody>";
            FooterHTMLPicked += "</table>";

            //------------------------------------------------------------------------------------------
            string HeaderHTML = "<table style='width: 100%; border-collapse: collapse;'";
            HeaderHTML += "class='mGrid' border='1' rules='all' cellspacing='0'>";
            HeaderHTML += "<tbody>";
            HeaderHTML += "<tr>";
            HeaderHTML += "<th scope='col'>Pull</th>";
            HeaderHTML += "<th scope='col' align='left'>Description</th>";
            HeaderHTML += "<th scope='col'>Part Number</th>";
            HeaderHTML += "<th scope='col'>Stock</th>";
            if (TechAssignedListOnly == false)
            {
                // we need to show the locations in order to pick from it.
                HeaderHTML += "<th scope='col'>Location</th>";
            }


            HeaderHTML += "</tr>";

            string FooterHTML = "</tbody>";
            FooterHTML += "</table>";
            //--------------------------------------------------------------------------------------------
            string HTML = "";
            decimal count = 0;
            foreach (FullPartNumberList pn in PN)
            {

                if (count == 1) { HTML += "<tr class='alt'>"; count = 0; }
                else { HTML += "<tr>"; count = 1; }
                HTML += "<td align='center'>";
                if (pn.PartNumber.Length > 0)
                {
                    if (pn.Stock > 0) { HTML += @"<input id='Button1' type='button' onclick=""displayResult('PickedList','"; }
                    else { HTML += @"<input id='Button1' type='button' disabled='true' onclick=""displayResult('PickedList','"; }
                    if (pn.Stock > 0)
                    {
                        if (TechAssignedListOnly == false) { HTML += pn.PartNumber.Replace("'", "") + " " + pn.Description.Replace("'", "") + ":id=" + pn.MasterPartsLinkTableID.ToString() + ":" + pn.MasterIFSLocationID.ToString() + @"');"" value='pick'/>"; }
                        else { HTML += pn.PartNumber.Replace("'", "") + " " + pn.Description.Replace("'", "") + ":id=" + pn.MasterPartsLinkTableID.ToString() + @"');"" value='pick'/>"; }
                    }
                    else { HTML += pn.PartNumber.Replace("'", "") + " " + pn.Description.Replace("'", "") + ":id=" + pn.MasterPartsLinkTableID.ToString() + @"');"" value='----'/>"; }
                }
                HTML += "</td>";
                HTML += "<td align='left'>(" + pn.Class + ") " + pn.Description.Trim() + "/" + pn.GMPDescription.Trim() + "</td>";
                HTML += "<td align='center'><span >" + pn.PartNumber + "</span></td>";
                HTML += "<td align='center'><span >" + pn.Stock + "</span></td>";
                if (TechAssignedListOnly == false)
                {
                    // we need to show the locations in order to pick from it.
                    HTML += "<td align='center'><span >" + pn.Location.Text + "</span></td>";
                }

                HTML += "</tr>";
            }
            string rValue = "";
            rValue = HeaderHTML + HTML + FooterHTML + "|" + HeaderHTMLPicked + FooterHTMLPicked;
            return rValue;
            //return HeaderHTML + FooterHTML;

        }

        [OperationContract]
        public string GetPartNumberReturnListData_02(string UserName, string sClientID, string sClientLocationID, string sCarrierID, string sManufacturerID, string sModelID, string sReceiveDetailID)
        {
            decimal ClientID = -1;
            decimal CarrierID = -1;
            decimal ManufacturerID = -1;
            decimal ModelID = -1;
            decimal ClientLocationID = -1;
            decimal ReceiveDetailID = -1;
            if (decimal.TryParse(sClientID, out ClientID) == false) { ClientID = -1; }
            if (decimal.TryParse(sClientLocationID, out ClientLocationID) == false) { ClientLocationID = -1; }
            if (decimal.TryParse(sCarrierID, out CarrierID) == false) { CarrierID = -1; }
            if (decimal.TryParse(sManufacturerID, out ManufacturerID) == false) { ManufacturerID = -1; }
            if (decimal.TryParse(sModelID, out ModelID) == false) { ModelID = -1; }
            if (decimal.TryParse(sReceiveDetailID, out ReceiveDetailID) == false) { ReceiveDetailID = -1; }

            MasterPartManager mpm = new MasterPartManager(UserName);

            //if (mpm.IsValidReturnTypeForParts(ReceiveDetailID) == false)
            //{
            //    string rType = mpm.GetReturnTypeForParts(ReceiveDetailID);
            //    return "Invalid Return Type(" + rType + "), Correct then try again";
            //}

            List<PartNumberTechAssignedList> PN = mpm.GetPartNumberReturnList(ReceiveDetailID);
            if (PN == null) { return ""; }

            string HeaderHTMLPicked = "<table id='PickedReturnList' style='width: 100%; border-collapse: collapse;'";
            HeaderHTMLPicked += "class='mGrid' border='1' rules='all' cellspacing='0'>";
            HeaderHTMLPicked += "<tbody>";
            HeaderHTMLPicked += "<tr>";
            HeaderHTMLPicked += "<th scope='col' align='left'></th>";
            HeaderHTMLPicked += "<th scope='col' align='left'>Selected Parts</th>";
            HeaderHTMLPicked += "</tr>";

            string FooterHTMLPicked = "</tbody>";
            FooterHTMLPicked += "</table>";

            //------------------------------------------------------------------------------------------
            string HeaderHTML = "<table style='width: 100%; border-collapse: collapse;'";
            HeaderHTML += "class='mGrid' border='1' rules='all' cellspacing='0'>";
            HeaderHTML += "<tbody>";
            HeaderHTML += "<tr>";
            HeaderHTML += "<th scope='col'>Pull</th>";
            //HeaderHTML += "<th scope='col' align='left'>Description</th>";
            HeaderHTML += "<th scope='col'>Part Number</th>";
            //HeaderHTML += "<th scope='col'>Stock</th>";
            HeaderHTML += "</tr>";

            string FooterHTML = "</tbody>";
            FooterHTML += "</table>";
            //--------------------------------------------------------------------------------------------
            string HTML = "";
            decimal count = 0;
            foreach (PartNumberTechAssignedList pn in PN)
            {

                if (count == 1) { HTML += "<tr class='alt'>"; count = 0; }
                else { HTML += "<tr>"; count = 1; }
                HTML += "<td align='center'>";
                if (pn.GMPPartNumber.Length > 0)
                {
                    HTML += @"<input id='Button1' type='button' onclick=""displayResult('PickedReturnList','";
                    HTML += pn.GMPPartNumber + ":id=" + pn.MasterPartsTechAssignedLogID.ToString() + @"');"" value='Return'/>";
                }
                HTML += "</td>";
                //HTML += "<td align='left'>(" + pn.Class + ") " + pn.Description + "</td>";
                HTML += "<td align='center'><span >" + pn.GMPPartNumber + "</span></td>";
                //HTML += "<td align='center'><span >" + pn.Stock + "</span>";
                //HTML += "</td>";
                HTML += "</tr>";
            }
            string rValue = "";
            rValue = HeaderHTML + HTML + FooterHTML + "|" + HeaderHTMLPicked + FooterHTMLPicked;
            return rValue;
            //return HeaderHTML + FooterHTML;

        }

        [OperationContract]
        public string ProcessReturnedParts(string UserName, string Data)
        {
            // clean the HTML from the data (The data is just an innerHTML from a panel)
            Data = Data.Replace("\"", "").Replace("<tbody><tr><th scope=col align=left></th><th scope=col align=left>Selected Parts</th>", "");
            Data = Data.Replace("</td></tr></tbody>", "");
            Data = Data.Replace("</tr><tr>", "!").Replace("</td><td>", "@");
            Data = Data.Replace("!<td><input id=Button1 onclick=DeleteResult('PickedReturnList','", "@");
            Data = Data.Replace("'); value=< type=button>", "");
            Data = Data.Replace("</td>", "");
            //Data = Data.Replace("</tr><tr>", "!").Replace("</td><td>", "@");
            string ReturnText = "";
            string x = "";
            decimal id = -1;
            MasterPartManager pm = new MasterPartManager(UserName);
            var datalist = Data.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries).Distinct();
            foreach (string s in datalist)
            {
                x = s.Replace(":id=", "@");
                var d = x.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries).Distinct().ToArray();
                if (d.Count() == 2)
                {
                    if (decimal.TryParse(d[1], out id) == false) { id = -1; }


                    string rvalue = pm.MasterPartsTechAssignedLog_Returning(id);
                    if (rvalue == null || (rvalue.Length >= 5 && rvalue.Substring(5).ToUpper() == "ERROR")) { continue; }
                    ReturnText += d[1] + ",";
                    // we now have to return the part.
                    // Now we can take the ID and send it back out to the user.
                }
            }
            return ReturnText;
        }
        #endregion




        //[OperationContract]
        //public string GetPartNumberListData(string UserName, string sClientID, string sCarrierID, string sManufacturerID, string sModelID)
        //{
        //    decimal ClientID = -1;
        //    decimal CarrierID = -1;
        //    decimal ManufacturerID = -1;
        //    decimal ModelID = -1;
        //    decimal ClientLocationID = -1;
        //    if (decimal.TryParse(sClientID, out ClientID) == false) { ClientID = -1; }
        //    if (decimal.TryParse(sCarrierID, out CarrierID) == false) { CarrierID = -1; }
        //    if (decimal.TryParse(sManufacturerID, out ManufacturerID) == false) { ManufacturerID = -1; }
        //    if (decimal.TryParse(sModelID, out ModelID) == false) { ModelID = -1; }

        //    MasterPartManager mpm = new MasterPartManager(UserName);
        //    List<FullPartNumberList> PN = mpm.GetFullPartNumberList(ClientID,ClientLocationID,  CarrierID, ManufacturerID, ModelID);


        //    if (PN == null) { return ""; }


        //    string HeaderHTMLPicked = "<table id='PickedList' style='width: 100%; border-collapse: collapse;'";
        //    HeaderHTMLPicked += "class='mGrid' border='1' rules='all' cellspacing='0'>";
        //    HeaderHTMLPicked += "<tbody>";
        //    HeaderHTMLPicked += "<tr>";
        //    HeaderHTMLPicked += "<th scope='col' align='left'></th>";
        //    HeaderHTMLPicked += "<th scope='col' align='left'>Selected Parts</th>";
        //    HeaderHTMLPicked += "</tr>";



        //    string FooterHTMLPicked = "</tbody>";
        //    FooterHTMLPicked += "</table>";

        //    //------------------------------------------------------------------------------------------
        //    string HeaderHTML = "<table style='width: 100%; border-collapse: collapse;'";
        //    HeaderHTML += "class='mGrid' border='1' rules='all' cellspacing='0'>";
        //    HeaderHTML += "<tbody>";
        //    HeaderHTML += "<tr>";
        //    HeaderHTML += "<th scope='col'>Pull</th>";
        //    HeaderHTML += "<th scope='col' align='left'>Description</th>";
        //    HeaderHTML += "<th scope='col'>Part Number</th>";
        //    HeaderHTML += "<th scope='col'>Stock</th>";
        //    HeaderHTML += "</tr>";

        //    string FooterHTML = "</tbody>";
        //    FooterHTML += "</table>";
        //    //--------------------------------------------------------------------------------------------
        //    string HTML = "";
        //    decimal count = 0;
        //    foreach (FullPartNumberList pn in PN)
        //    {

        //        if (count == 1) { HTML += "<tr class='alt'>"; count = 0; }
        //        else { HTML += "<tr>"; count = 1; }
        //        HTML += "<td align='center'>";
        //        if (pn.PartNumber.Length > 0)
        //        {
        //            HTML += @"<input id='Button1' type='button' onclick=""displayResult('";
        //            HTML += pn.PartNumber + " " + pn.Description + ":id=" + pn.MasterPartsLinkTableID.ToString() + @"');"" value='pick'/>";
        //        }
        //        HTML += "</td>";
        //        HTML += "<td align='left'>(" + pn.Class + ") " + pn.Description + "</td>";
        //        HTML += "<td align='center'><span >" + pn.PartNumber + "</span></td>";
        //        HTML += "<td align='center'><span >" + pn.Stock + "</span>";
        //        HTML += "</td>";
        //        HTML += "</tr>";
        //    }
        //    return HeaderHTML + HTML + FooterHTML + "|" + HeaderHTMLPicked + FooterHTMLPicked;
        //    //return HeaderHTML + FooterHTML;

        //}

        [OperationContract]
        public string GetUnitNote(string UserName, string CurrentProcess, string sReceiveDetailID)
        {
            //decimal ClientID = -1;
            //decimal CarrierID = -1;
            //decimal ManufacturerID = -1;
            decimal ReceiveDetailID = -1;
            //if (decimal.TryParse(sClientID, out ClientID) == false) { ClientID = -1; }
            //if (decimal.TryParse(sCarrierID, out CarrierID) == false) { CarrierID = -1; }
            //if (decimal.TryParse(sManufacturerID, out ManufacturerID) == false) { ManufacturerID = -1; }
            //if (decimal.TryParse(sModelID, out ModelID) == false) { ModelID = -1; }

            //MasterPartManager mpm = new MasterPartManager(UserName);
            //List<FullPartNumberList> PN = mpm.GetFullPartNumberList(ClientID, CarrierID, ManufacturerID, ModelID);


            //if (PN == null) { return ""; }
            ReceiveDetailManager bm = new ReceiveDetailManager(UserName);
            string ReturnValue = bm.GetESNAttribute(ReceiveDetailID, "Customer Notes");
            // ReturnValue = "Here is the value of the notes";
            return ReturnValue;


            //string HeaderHTMLPicked = "<table id='PickedList' style='width: 100%; border-collapse: collapse;'";
            //HeaderHTMLPicked += "class='mGrid' border='1' rules='all' cellspacing='0'>";
            //HeaderHTMLPicked += "<tbody>";
            //HeaderHTMLPicked += "<tr>";
            //HeaderHTMLPicked += "<th scope='col' align='left'></th>";
            //HeaderHTMLPicked += "<th scope='col' align='left'>Selected Parts</th>";
            //HeaderHTMLPicked += "</tr>";



            //string FooterHTMLPicked = "</tbody>";
            //FooterHTMLPicked += "</table>";

            ////------------------------------------------------------------------------------------------
            //string HeaderHTML = "<table style='width: 100%; border-collapse: collapse;'";
            //HeaderHTML += "class='mGrid' border='1' rules='all' cellspacing='0'>";
            //HeaderHTML += "<tbody>";
            //HeaderHTML += "<tr>";
            //HeaderHTML += "<th scope='col'>Pull</th>";
            //HeaderHTML += "<th scope='col' align='left'>Description</th>";
            //HeaderHTML += "<th scope='col'>Part Number</th>";
            //HeaderHTML += "<th scope='col'>Stock</th>";
            //HeaderHTML += "</tr>";

            //string FooterHTML = "</tbody>";
            //FooterHTML += "</table>";
            ////--------------------------------------------------------------------------------------------
            //string HTML = "";
            ////decimal count = 0;
            ////foreach (FullPartNumberList pn in PN)
            ////{

            ////    if (count == 1) { HTML += "<tr class='alt'>"; count = 0; }
            ////    else { HTML += "<tr>"; count = 1; }
            ////    HTML += "<td align='center'>";
            ////    if (pn.PartNumber.Length > 0)
            ////    {
            ////        HTML += @"<input id='Button1' type='button' onclick=""displayResult('";
            ////        HTML += pn.PartNumber + " " + pn.Description + ":id=" + pn.MasterPartsLinkTableID.ToString() + @"');"" value='pick'/>";
            ////    }
            ////    HTML += "</td>";
            ////    HTML += "<td align='left'>" + pn.Description + "</td>";
            ////    HTML += "<td align='center'><span >" + pn.PartNumber + "</span></td>";
            ////    HTML += "<td align='center'><span >" + pn.Stock + "</span>";
            ////    HTML += "</td>";
            ////    HTML += "</tr>";
            ////}
            //return HeaderHTML + HTML + FooterHTML + "|" + HeaderHTMLPicked + FooterHTMLPicked;
            ////return HeaderHTML + FooterHTML;

        }
        [OperationContract]
        public string GetSearchClientLocationData(string UserName, string clientname, string locationname, string Street, string postalcode)
        {
            return GetSearchClientLocationData02(UserName, clientname, locationname, Street, postalcode);
        }
        [OperationContract]
        public string LoadPreReceiveDetail(string ESN, string UserName)
        {
            Log.LogIt("LoadPreReceiveDetail - Started (" + ESN + ") ******************");
            JsonString rData = new JsonString();
            string Detail = "";
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                string d = "";
                ReceiveDetailManager rdm = new ReceiveDetailManager(UserName);
                PreReceiveDetailHeader rh = rdm.GetPreReceiveDetail(ctx, ESN);

                if (rh.ESN.Length == 0) { rData.AddValuePair("Status", "No Action"); }
                else
                {
                    rData.AddValuePair("Status", "Update");
                    rData.AddValuePair("RMA", rh.RMA);
                    rData.AddValuePair("ProjectTag", rh.ProjectTag);
                    rData.AddValuePair("ProjectName", rh.ProjectName);
                    rData.AddValuePair("ProjectID", rh.ProjectID.ToString());
                    // foreach (PreReceiveDetailOptions o in rh.DetailOptions) { Detail = "x|" + o.OptionID.ToString() + "|x|x|x|x|" + o.Value + "|x;"; }
                    foreach (PreReceiveDetailOptions o in rh.DetailOptions)
                    {

                        d = rdm.GetOptionData(ctx, o.OptionID, o.Value);
                        if (d.Length > 0)
                        {
                            Detail += d + ";";
                        }
                        //JsonStringOptionKeyList OptionList = rdm.GetOptionData(o.OptionID);
                        //if (OptionList != null)
                        //{
                        //    Detail = o.OptionID.ToString() + "|" + OptionList.Value + "|" + o.Value + "|" + "|x|x|x|" + o.Value + "|x;";
                        //}


                    }
                    rData.AddValuePair("Detail", Detail);
                }
            }
            Log.LogIt("LoadPreReceiveDetail - Finished (" + ESN + ")");
            return "{" + rData.ToString() + "}";
        }

        [OperationContract]
        public string GetSearchClientLocationData02(string UserName, string clientname, string locationname, string Street, string postalcode)
        {
            string Rows = "";
            JsonString rData = new JsonString();
            string[] ClientLocationID = new string[] { "ClientLocationID", "" };
            string[] ClientName = new string[] { "txtClientName", "" };
            string[] LocationName = new string[] { "txtLocationName", "" };
            string[] StoreNumber = new string[] { "txtStoreNumber", "" };
            string[] StoreSuffix = new string[] { "txtStoreSuffix", "" };
            string[] StoreAddress = new string[] { "txtClientAddress", "" };
            string[] ScanKey = new string[] { "ScanKey", "" };


            try
            {
                using (clsLinqDataContext ctx = new clsLinqDataContext())
                {
                    ClientManager cm = new ClientManager(UserName);
                    List<ClientLocation> cls = cm.SearchLocationsList(ctx, clientname, locationname, Street, postalcode);

                    //rData.AddValuePair(ClientLocationID[0], "-1");
                    //rData.AddValuePair(ClientName[0], "XXX");
                    //rData.AddValuePair(LocationName[0], "Count:" + cls.Count.ToString());
                    //rData.AddValuePair(StoreNumber[0], "");
                    //rData.AddValuePair(StoreSuffix[0], "");
                    //rData.AddValuePair(StoreAddress[0], "");
                    //rData.AddValuePair(ScanKey[0], "xxx");
                    //Rows = "{" + rData.ToString() + "}";
                    //return Rows;

                    foreach (ClientLocation clx in cls.Take(10))
                    {
                        ClientLocationID[1] = clx.ClientLocationID.ToString();
                        Client cl = ctx.Clients.FirstOrDefault(x => x.ClientID == clx.ClientID);
                        if (cl != null) { ClientName[1] = cl.CompanyName; }
                        ScanKey[1] = clx.ScanKey;
                        LocationName[1] = "(" + clx.ScanKey + ") " + clx.CompanyName;
                        StoreNumber[1] = clx.StoreNumber;
                        StoreSuffix[1] = clx.StoreSuffix;
                        StoreAddress[1] = clx.AddressLine1 + " " + clx.AddressLine2 + " " + clx.City + " " + clx.StateOrProvince + " " + clx.PostalCode;

                        rData.AddValuePair(ClientLocationID[0], ClientLocationID[1]);
                        rData.AddValuePair(ClientName[0], ClientName[1]);
                        rData.AddValuePair(LocationName[0], LocationName[1]);
                        rData.AddValuePair(StoreNumber[0], StoreNumber[1]);
                        rData.AddValuePair(StoreSuffix[0], StoreSuffix[1]);
                        rData.AddValuePair(StoreAddress[0], StoreAddress[1]);
                        rData.AddValuePair(ScanKey[0], ScanKey[1]);
                        if (Rows.Length > 0) { Rows += ","; }
                        Rows += "{" + rData.ToString() + "}";
                    }

                }
            }
            catch (Exception ex)
            {
                rData.AddValuePair(ClientLocationID[0], "-1");
                rData.AddValuePair(ClientName[0], "Err");
                rData.AddValuePair(LocationName[0], ex.Message);
                rData.AddValuePair(StoreNumber[0], "");
                rData.AddValuePair(StoreSuffix[0], "");
                rData.AddValuePair(StoreAddress[0], "");
                rData.AddValuePair(ScanKey[0], "ERR");
                Rows = "{" + rData.ToString() + "}";
            }
            //}
            return Rows;

        }


        //[OperationContract]
        //public string AssignPartNumber(string ESN, string PartNumber, string Tech, string Location, string Return, string UserName)
        //{
        //    //if (ESN.Trim().Length == 0 || WayBill.Trim().Length == 0) { return "0"; }
        //    decimal LocationID = -1;
        //    decimal ReceiveDetailID = -1;
        //    bool isReturned = false;
        //    if (Return == "T") {isReturned = true;}
        //    if (decimal.TryParse(Location, out LocationID) == false) { LocationID = -1; }
        //    MasterPartManager MPM = new MasterPartManager(UserName);

        //    using (clsLinqDataContext ctx = MPM.GetDataContext(UserName))
        //    {
        //        // Look to see if the ESN Exists.
        //        ReceiveDetail r = ctx.ReceiveDetails.FirstOrDefault(x => x.ESN == ESN && x.Version == "000");
        //        if (r == null) { return "Error:(" + ESN + ") Unit not found!"; }
        //        ReceiveDetailID = r.ReceiveDetailID;
        //        return MPM.MasterPartsTechAssignedLog_Add(ReceiveDetailID, PartNumber, Tech, LocationID, isReturned);
        //    }
        //}

        [OperationContract]
        public string ShipUnit(string ESN, string WayBill, string Courier, string UserName)
        {
            if (ESN.Trim().Length == 0 || WayBill.Trim().Length == 0) { return "0"; }

            ReceiveDetailManager rdm = new ReceiveDetailManager(UserName);
            using (clsLinqDataContext ctx = rdm.GetDataContext(UserName))
            {

                // Look to see if the ESN Exists.
                ReceiveDetail r = ctx.ReceiveDetails.FirstOrDefault(x => x.ESN == ESN && x.Version == "000");
                if (r == null) { return "Error:(" + ESN + ") Unit not found!"; }
                return shipThisUnit(ctx, rdm, r, WayBill, Courier, UserName);
            }
        }

        string shipThisUnit(clsLinqDataContext ctx, ReceiveDetailManager rdm, ReceiveDetail r, string WayBill, string Courier, string UserName)
        {
            string rValue = rdm.BulkShipUnit(ctx, r.ReceiveDetailID, WayBill, Courier, UserName);
            if (rValue.Length >= 6 && rValue.Substring(0, 5).ToUpper() == "ERROR") { return rValue; }

            //Process shipped = ctx.Processes.FirstOrDefault(x => x.Name.ToUpper() == "SHIPPING");
            //if (shipped == null) { return "Error: process(Shipping) not found"; }




            //rdm.UpdateESNAttribute_Blank(ctx, r.ReceiveDetailID, "Bin");
            //rdm.UpdateESNAttribute(ctx, r.ReceiveDetailID, "Location", "None");

            //rdm.UpdateESNAttribute(ctx, r.ReceiveDetailID, "Outgoing Waybill – S", WayBill);
            //rdm.UpdateESNAttribute(ctx, r.ReceiveDetailID, "Courier Out", Courier);

            //// Generate a shipped record in the processLog.
            //ReceiveDetailProcessLog pl = new ReceiveDetailProcessLog();
            //pl.CreateUser = UserName;
            //pl.MiscText = "Bulk Ship";
            //pl.ProcessID = shipped.ProcessID;
            //pl.ProcessText = shipped.Name;
            //pl.ReceiveDetailID = r.ReceiveDetailID;
            //ctx.ReceiveDetailProcessLogs.InsertOnSubmit(pl);
            //ctx.SubmitChanges();




            //// Find the client. If xx1, then update these attributes...
            //if (r.ClientLocation.Client.CompanyName.ToUpper() == "xx1")
            //{
            //    string DealerID = r.ClientLocation.ScanKey;
            //    string ServiceRequestNum = rdm.GetReceiveDetailItem_DataElement(ctx, r.ReceiveDetailID, "Client Reference #");
            //    rdm.UpdateESNAttribute(ctx, r.ReceiveDetailID, "ShipTo", DealerID);
            //    rdm.UpdateESNAttribute(ctx, r.ReceiveDetailID, "PO NO", ServiceRequestNum);
            //}






            //////////////////////////////////////////////////////////////////////////////////////////////
            ////rdm.SM.OrderShipped(ctx, r.ReceiveDetailID, -1, r.ClientLocationID, (decimal)r.ProjectID, r.ProcessID, UserName);
            //AdvanceESNVersion(ctx, r.ESN, UserName, "Bulk Shipping");
            ///////////////////////////////////////////////////////////////////////
            //// This needs to happen after the AdvanceESN because if the swap is done first
            ////      The Advance does not find it.
            //try
            //{
            //    bool isSwapped = rdm.SwapIMEI(ctx, r.ReceiveDetailID);
            //}
            //catch (Exception ex)
            //{
            //}
            ///////////////////////////////////////////////////////////////////////////////////////////
            //// Bring Back From MSC if it is a MSC Unit
            //rdm.CloneFromMSC(ctx, r.ReceiveDetailID, (decimal)r.ProjectID, rdm.GetCurrentProcessID(ctx,r.ReceiveDetailID));            
            return "Shipped:" + r.ESN + ":" + r.ClientLocation.Client.CompanyName;
        }


        //[OperationContract]
        //public string Ship_Unit(clsLinqDataContext ctx, string ESN, string WayBill, string Courier, string UserName)
        //{
        //    if (ESN.Trim().Length == 0 || WayBill.Trim().Length == 0) { return "0"; }
        //    return "Error: Unit not found";
        //    //return SaveReceiveWayBill(ctx, ESN, WayBill, UserName);
        //}


        [OperationContract]
        public string CycleCountIMEI(string sCycleCountHeaderID, string ESN, string UserName)
        {
            if (ESN.Trim().Length == 0) { return "0"; }

            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                string prMessage = "";
                //prMessage = SaveReceiveWayBill_IDC(ctx, ESN, WayBill, "LOC-PREREC", MasterClientName, PreRecvProductType, UserName);
                prMessage = "Saved:" + ESN;
                //if (prMessage.Substring(0, 6).ToUpper() == "SAVED:")
                //{
                //    IDC_LocationLogManager log = new IDC_LocationLogManager(UserName);
                //    log.Add(ESN, IDC_BIN, WayBill, "LOC-PREREC", MasterClientID, MasterClientName, Courier, -1, UserName, "", "PREREC");
                //}
                //if (prMessage.Substring(0, 8).ToUpper() == "UPDATED:")
                //{
                //    IDC_LocationLogManager log = new IDC_LocationLogManager(UserName);
                //    log.Add(ESN, IDC_BIN, WayBill, "LOC-PREREC", MasterClientID, MasterClientName, Courier, -1, UserName, "", "PREREC");
                //}
                return prMessage;
                //return ESN + ":" + IDC_BIN + ":" + WayBill + ":" + MasterClientID + ":" + MasterClientName;
                //return 
            }
        }



        [OperationContract]
        public string SaveReceiveWayBill_IDC(string ESN, string IDC_BIN, string WayBill, string MasterClientID, string MasterClientName, string Courier, string PreRecvProductType, string UserName)
        {
            if (ESN.Trim().Length == 0 || WayBill.Trim().Length == 0) { return "0"; }

            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                string prMessage = "";
                prMessage = SaveReceiveWayBill_IDC(ctx, ESN, WayBill, "LOC-PREREC", MasterClientName, PreRecvProductType, UserName);

                if (prMessage.Substring(0, 6).ToUpper() == "SAVED:")
                {
                    IDC_LocationLogManager log = new IDC_LocationLogManager(UserName);
                    log.Add(ESN, IDC_BIN, WayBill, "LOC-PREREC", MasterClientID, MasterClientName, Courier, -1, UserName, "", "PREREC");
                }
                if (prMessage.Substring(0, 8).ToUpper() == "UPDATED:")
                {
                    IDC_LocationLogManager log = new IDC_LocationLogManager(UserName);
                    log.Add(ESN, IDC_BIN, WayBill, "LOC-PREREC", MasterClientID, MasterClientName, Courier, -1, UserName, "", "PREREC");
                }
                return prMessage;
                //return ESN + ":" + IDC_BIN + ":" + WayBill + ":" + MasterClientID + ":" + MasterClientName;
                //return 
            }
        }

        public string SaveReceiveWayBill_IDC(clsLinqDataContext ctx, string ESN, string WayBill, string Location, string MasterClient, string PreRecvProductType, string UserName)
        {
            bool isESNThere = (from x in ctx.ReceiveDetails
                               where x.ESN == ESN && x.Version == "000"
                               select true).FirstOrDefault();
            if (isESNThere == true) { return "Already on file:" + ESN; }

            ReceiveDetailPreReceive rd = (from x in ctx.ReceiveDetailPreReceives
                                          where x.ESN == ESN && x.Status == "Open"
                                          select x).FirstOrDefault();
            if (rd != null)
            {
                rd.LastUpdateDate = DateTime.Now;
                rd.LastUpdateUser = UserName;
                rd.OnSite = true;

                SetAttributeLocation(ctx, Location, rd);
                if (WayBill.Length > 0)
                {
                    SetAttributeDealerWayBill(ctx, WayBill, rd);
                }
                SetAttributeMasterClient(ctx, MasterClient, rd);
                SetAttributePreRecvProductType(ctx, PreRecvProductType, rd);

                ctx.SubmitChanges();
                return "Updated:" + ESN + " - Already Queued";
            }
            else
            {

                rd = new ReceiveDetailPreReceive();
                rd.CreateDate = DateTime.Now;
                rd.LastUpdateDate = DateTime.Now;
                rd.CreateUser = UserName;
                rd.LastUpdateUser = UserName;
                rd.ESN = ESN;
                rd.ProjectTag = "";
                rd.RMANumber = "";
                rd.Status = "Open";
                rd.OnSite = true;
                //if (CarrierID > 0) { rd.CarrierID = CarrierID; }
                //if (ManufacturerID > 0) { rd.ManufacturerID = ManufacturerID; }
                //if (ModelID > 0) { rd.ModelID = ModelID; }
                //if (ColourID > 0) { rd.ColourID = ColourID; }

                SetAttributeDealerWayBill(ctx, WayBill, rd);
                SetAttributeLocation(ctx, Location, rd);
                SetAttributeMasterClient(ctx, MasterClient, rd);
                SetAttributePreRecvProductType(ctx, PreRecvProductType, rd);
                ctx.ReceiveDetailPreReceives.InsertOnSubmit(rd);
                ctx.SubmitChanges();
                return "Saved:" + ESN;
            }
        }



        //[OperationContract]
        //public string SaveReceiveWayBill_IDC(string ESN, string IDC_BIN, string WayBill, string MasterClientID, string MasterClientName, string Courier, string UserName)
        //{
        //    if (ESN.Trim().Length == 0 || WayBill.Trim().Length == 0) { return "0"; }

        //    using (clsLinqDataContext ctx = new clsLinqDataContext())
        //    {
        //        string prMessage = "";
        //        prMessage = SaveReceiveWayBill_IDC(ctx, ESN, WayBill, "LOC-PREREC", MasterClientName, UserName);

        //        if (prMessage.Substring(0, 6).ToUpper() == "SAVED:")
        //        {
        //            IDC_LocationLogManager log = new IDC_LocationLogManager(UserName);
        //            log.Add(ESN, IDC_BIN, WayBill, "LOC-PREREC", MasterClientID, MasterClientName, Courier, -1, UserName, "", "PREREC");
        //        }

        //        return prMessage;
        //        //return ESN + ":" + IDC_BIN + ":" + WayBill + ":" + MasterClientID + ":" + MasterClientName;
        //        //return 
        //    }
        //}
        [OperationContract]
        public string SaveReceiveWayBill(string ESN, string WayBill, string MasterClient, string UserName)
        {
            if (ESN.Trim().Length == 0 || WayBill.Trim().Length == 0) { return "0"; }

            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                return SaveReceiveWayBill(ctx, ESN, WayBill, MasterClient, UserName);
            }
        }

        [OperationContract]
        public string WayBillMessageQueue(string WayBill, string UserName)
        {
            ReceiveDetailManager rm = new ReceiveDetailManager(UserName);
            string[] mx = rm.GetWayBillQueuedMessage(WayBill);
            return mx[1];
        }




        public string SaveReceiveWayBill(clsLinqDataContext ctx, string ESN, string WayBill, string MasterClient, string UserName)
        {
            bool isESNThere = (from x in ctx.ReceiveDetails
                               where x.ESN == ESN && x.Version == "000"
                               select true).FirstOrDefault();
            if (isESNThere == true) { return "Already on file:" + ESN; }

            ReceiveDetailPreReceive rd = (from x in ctx.ReceiveDetailPreReceives
                                          where x.ESN == ESN && x.Status == "Open"
                                          select x).FirstOrDefault();
            if (rd != null)
            {
                rd.LastUpdateDate = DateTime.Now;
                rd.LastUpdateUser = UserName;
                rd.OnSite = true;
                SetAttributeDealerWayBill(ctx, WayBill, rd);
                SetAttributeMasterClient(ctx, MasterClient, rd);
                ctx.SubmitChanges();
                return "Already Queued Updated:" + ESN + " - Updated";
            }
            else
            {

                rd = new ReceiveDetailPreReceive();
                rd.CreateDate = DateTime.Now;
                rd.LastUpdateDate = DateTime.Now;
                rd.CreateUser = UserName;
                rd.LastUpdateUser = UserName;
                rd.ESN = ESN;
                rd.ProjectTag = "";
                rd.RMANumber = "";
                rd.Status = "Open";
                rd.OnSite = true;
                //if (CarrierID > 0) { rd.CarrierID = CarrierID; }
                //if (ManufacturerID > 0) { rd.ManufacturerID = ManufacturerID; }
                //if (ModelID > 0) { rd.ModelID = ModelID; }
                //if (ColourID > 0) { rd.ColourID = ColourID; }

                SetAttributeDealerWayBill(ctx, WayBill, rd);
                SetAttributeMasterClient(ctx, MasterClient, rd);
                ctx.ReceiveDetailPreReceives.InsertOnSubmit(rd);
                ctx.SubmitChanges();
                return "Saved:" + ESN;
            }
        }

        private static void SetAttributeDealerWayBill(clsLinqDataContext ctx, string WayBill, ReceiveDetailPreReceive rd)
        {
            Option o = (from x in ctx.Options
                        where x.Question.Name.ToUpper() == "DEALER WAYBILL"
                        select x).FirstOrDefault();
            if (o != null)
            {
                ReceiveDetailPreReceiveAttribute rda = rd.ReceiveDetailPreReceiveAttributes.FirstOrDefault(x => x.OptionID == o.OptionID);
                if (rda != null)
                {
                    rda.LastUpdateDate = rd.LastUpdateDate;
                    rda.LastUpdateUser = rd.LastUpdateUser;
                    rda.Value = WayBill;
                    return;
                }
                rda = new ReceiveDetailPreReceiveAttribute();
                rda.CreateDate = rd.CreateDate;
                rda.CreateUser = rd.CreateUser;
                rda.LastUpdateDate = rd.LastUpdateDate;
                rda.LastUpdateUser = rd.LastUpdateUser;
                rda.OptionID = o.OptionID;
                rda.Value = WayBill;
                rd.ReceiveDetailPreReceiveAttributes.Add(rda);
            }
        }

        private static void SetAttributeMasterClient(clsLinqDataContext ctx, string MasterClient, ReceiveDetailPreReceive rd)
        {
            Option o = (from x in ctx.Options
                        where x.Question.Name.ToUpper() == "MC For"
                        select x).FirstOrDefault();
            if (o != null)
            {
                ReceiveDetailPreReceiveAttribute rda = rd.ReceiveDetailPreReceiveAttributes.FirstOrDefault(x => x.OptionID == o.OptionID);
                if (rda != null)
                {
                    rda.LastUpdateDate = rd.LastUpdateDate;
                    rda.LastUpdateUser = rd.LastUpdateUser;
                    rda.Value = MasterClient;
                    return;
                }
                rda = new ReceiveDetailPreReceiveAttribute();
                rda.CreateDate = rd.CreateDate;
                rda.CreateUser = rd.CreateUser;
                rda.LastUpdateDate = rd.LastUpdateDate;
                rda.LastUpdateUser = rd.LastUpdateUser;
                rda.OptionID = o.OptionID;
                rda.Value = MasterClient;
                rd.ReceiveDetailPreReceiveAttributes.Add(rda);
            }
        }

        private static void SetAttributeLocation(clsLinqDataContext ctx, string Location, ReceiveDetailPreReceive rd)
        {
            Option o = (from x in ctx.Options
                        where x.Question.Name.ToUpper() == "Location"
                        select x).FirstOrDefault();
            if (o != null)
            {
                ReceiveDetailPreReceiveAttribute rda = rd.ReceiveDetailPreReceiveAttributes.FirstOrDefault(x => x.OptionID == o.OptionID);
                if (rda != null)
                {
                    rda.LastUpdateDate = rd.LastUpdateDate;
                    rda.LastUpdateUser = rd.LastUpdateUser;
                    rda.Value = Location;
                    return;
                }
                rda = new ReceiveDetailPreReceiveAttribute();
                rda.CreateDate = rd.CreateDate;
                rda.CreateUser = rd.CreateUser;
                rda.LastUpdateDate = rd.LastUpdateDate;
                rda.LastUpdateUser = rd.LastUpdateUser;
                rda.OptionID = o.OptionID;
                rda.Value = Location;
                rd.ReceiveDetailPreReceiveAttributes.Add(rda);
            }
        }









        private static void SetAttributePreRecvProductType(clsLinqDataContext ctx, string PreRecvProductType, ReceiveDetailPreReceive rd)
        {


            Option o = (from x in ctx.Options
                        where x.Question.Name.ToUpper() == "PreRecv Product Type" && x.OptionText == PreRecvProductType
                        select x).FirstOrDefault();
            if (o != null)
            {
                //ReceiveDetailPreReceiveAttribute rda = rd.ReceiveDetailPreReceiveAttributes.FirstOrDefault(x => x.OptionID == o.OptionID);                
                ReceiveDetailPreReceiveAttribute rda = rd.ReceiveDetailPreReceiveAttributes.FirstOrDefault(x => x.Option != null && x.Option.QuestionID == o.QuestionID);
                if (rda != null)
                {
                    //rda.LastUpdateDate = rd.LastUpdateDate;
                    //rda.LastUpdateUser = rd.LastUpdateUser;
                    //rda.OptionID = o.OptionID;
                    //rda.Value = "1";
                    //rd.ReceiveDetailPreReceiveAttributes.Remove(rda);
                    //return;
                    ctx.ReceiveDetailPreReceiveAttributes.DeleteOnSubmit(rda);
                }
                ReceiveDetailPreReceiveAttribute rda2 = new ReceiveDetailPreReceiveAttribute();
                rda2.CreateDate = rd.CreateDate;
                rda2.CreateUser = rd.CreateUser;
                rda2.LastUpdateDate = rd.LastUpdateDate;
                rda2.LastUpdateUser = rd.LastUpdateUser;
                rda2.OptionID = o.OptionID;
                rda2.Value = "1";
                rd.ReceiveDetailPreReceiveAttributes.Add(rda2);
            }
        }





        [OperationContract]
        public string CountAttribute(string Attribute, string Value, string UserCount, string UserName, string DocLog)
        {
            if (Value.Trim().Length == 0) { return "0"; }

            ReceiveDetailManager rm = new ReceiveDetailManager(UserName);
            decimal Actual = rm.CountAttribute(Attribute, Value);
            if (DocLog == "1" && Value.Trim().Length > 0)
            {
                decimal userCount = 0;
                if (decimal.TryParse(UserCount, out userCount) == false) { userCount = 0; }
                rm.RecordDocCount(Value, userCount, Actual);
            }
            return Actual.ToString();
        }

        [OperationContract]
        public string IsScanKeyOK(string CLID, string ScanKey)
        {
            decimal ID = -1;
            if (decimal.TryParse(CLID, out ID) == false) { ID = -1; }
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                string rValue = "";
                ClientLocation cl = ctx.ClientLocations.FirstOrDefault(x => x.ClientLocationID != ID && x.ScanKey.ToUpper() == ScanKey.ToUpper());
                if (cl != null)
                {
                    rValue = "Scankey (" + ScanKey + ") already used! " + cl.CompanyName;
                }
                return rValue;
            }
            //if (ScanKey.ToUpper() == "YES")
            //{ return ""; }
            //return "Scankey (" + ScanKey + ") already on file";
        }


        [OperationContract]
        public string GetClientLocationData(string CLID, string UserName)
        {
            string[] ClientLocationID = new string[] { "ClientLocationID", "" };
            string[] ClientName = new string[] { "txtClientName", "" };
            string[] StoreNumber = new string[] { "txtStoreNumber", "" };
            string[] StoreSuffix = new string[] { "txtStoreSuffix", "" };
            string[] StoreAddress = new string[] { "txtClientAddress", "" };
            string[] ProjectDependencies = new string[] { "ProjectDependencies", "" };
            string[] ProcessDependencies = new string[] { "ProcessDependencies", "" };
            string[] Email = new string[] { "Email", "" };
            string[] Email2 = new string[] { "Email2", "" };
            JsonString rData = new JsonString();
            Int32 ID = 0;
            if (Int32.TryParse(CLID, out ID) == true)
            {
                using (clsLinqDataContext ctx = new clsLinqDataContext())
                {
                    ClientLocationID[1] = ID.ToString();
                    ClientLocationManager clm = new ClientLocationManager(UserName);
                    ClientLocation cl = clm.GetClientLocation(ctx, ID);
                    if (cl != null)
                    {
                        if (cl.IFSSite.ToUpper() == "C1NA" && (cl.IFSPOVendor == null || cl.IFSPOVendor.Length == 0))
                        {
                            ClientName[1] = "Missing Supplier Number!";
                        }
                        else
                        {
                            ClientName[1] = cl.CompanyName;
                            StoreNumber[1] = cl.StoreNumber;
                            StoreSuffix[1] = cl.StoreSuffix;
                            Email[1] = cl.EmailAddress;
                            if (cl.EmailAddress2 != null && cl.EmailAddress2.Length > 0)
                            {
                                Email2[1] += cl.EmailAddress2;
                            }
                            StoreAddress[1] = cl.AddressLine1 + @"\n" + cl.AddressLine2 + @"\n" + cl.City + @"\n" + cl.StateOrProvince + @"\n" + cl.PostalCode;
                            ProjectDependencies[1] = "";
                            ProcessDependencies[1] = "";
                            List<ClientProjectDependency> projectDependencies = (from x in ctx.ClientProjectDependencies where x.ClientID == cl.ClientID select x).ToList();
                            foreach (ClientProjectDependency cpd in projectDependencies)
                            {
                                if (ProjectDependencies[1].Length == 0) { ProjectDependencies[1] = " "; }
                                ProjectDependencies[1] += cpd.ProjectID.ToString() + " ";
                            }
                            List<ClientProcessDependency> processDependencies = (from x in ctx.ClientProcessDependencies where x.ClientID == cl.ClientID select x).ToList();
                            foreach (ClientProcessDependency cpd in processDependencies)
                            {
                                if (ProcessDependencies[1].Length == 0) { ProcessDependencies[1] = " "; }
                                ProcessDependencies[1] += cpd.ProcessID.ToString() + " ";
                            }
                        }
                    }
                    else
                    {
                        ClientName[1] = "Access Denied!";
                        //throw new UserAccessControlException("Access denied!");
                    }
                    rData.AddValuePair("ClientLocationID", CLID);
                    rData.AddValuePair(ClientName[0], ClientName[1]);
                    rData.AddValuePair(StoreNumber[0], StoreNumber[1]);
                    rData.AddValuePair(StoreSuffix[0], StoreSuffix[1]);
                    rData.AddValuePair(StoreAddress[0], StoreAddress[1]);
                    rData.AddValuePair(ProjectDependencies[0], ProjectDependencies[1]);
                    rData.AddValuePair(ProcessDependencies[0], ProcessDependencies[1]);
                    rData.AddValuePair(Email[0], Email[1]);
                    rData.AddValuePair(Email2[0], Email2[1]);
                }
            }
            return rData.ToString();
        }

        [OperationContract]
        public string UpdateDataDetailHeader(string Data)
        {
            // We need to get things ready to be received as json when going back to the page.
            JsonString rString = new JsonString("Result", "NotSaved");
            rString.AddValuePair("ReceiveHeaderID", "-1");
            rString.AddValuePair("ReceiveDetailBulkID", "-1");
            rString.AddValuePair("ReceiveDetailID", "-1");
            rString.AddValuePair("SUFD", "");
            rString.AddValuePair("QTY", "0");
            rString.AddValuePair("CompProcList", "");
            rString.AddValuePair("MMS", "");
            rString.AddValuePair("PCLB", "");                           //lblProjectClientLocationBinTitle


            //int qty = 0;
            decimal id = -1;
            DateTime dt = DateTime.Now;

            decimal?[] rValue = { -1, -1 };
            JsonString dl = new JsonString(Data);
            Hashtable dList = dl.ListData();      // ParseData(Data);
            string ClientLocationID = dList["ClientLocationID"].ToString();
            string ReceiveHeaderID = dList["ReceiveHeaderID"].ToString();
            string ReceiveDetailBulkID = dList["ReceiveDetailBulkID"].ToString();
            string ReceiveDetailID = dList["ReceiveDetailID"].ToString();
            string StatusID = dList["StatusID"].ToString();
            string CurUserName = dList["CurUserName"].ToString();
            string CurStepUp = dList["CurStepUp"].ToString();
            string sCurProcessID = dList["CurProcessID"].ToString();
            decimal CurProcessID = -1;
            if (decimal.TryParse(sCurProcessID, out CurProcessID) == false) { CurProcessID = -1; }

            string RMA = dList["RMA"].ToString();
            string ESN = dList["ESN"].ToString();
            string Project = dList["Project"].ToString();
            string sProjectID = dList["ProjectID"].ToString();
            string ProjectTag = dList["PROJTAG"].ToString();
            decimal ProjectID = -1;
            decimal.TryParse(sProjectID, out ProjectID);

            if (decimal.TryParse(ReceiveHeaderID, out id) == false) { id = -1; }
            decimal nReceiveHeaderID = id;
            if (decimal.TryParse(ReceiveDetailBulkID, out id) == false) { id = -1; }
            decimal nReceiveDetailBulkID = id;
            if (decimal.TryParse(ReceiveDetailID, out id) == false) { id = -1; }
            decimal nReceiveDetailID = id;
            if (decimal.TryParse(StatusID, out id) == false) { id = -1; }
            decimal nStatusID = id;
            ReceiveDetailManager bm = new ReceiveDetailManager(CurUserName);
            ReceiveDetail rec = bm.ReceiveDetail(nReceiveDetailID);
            if (rec != null)
            {
                bm.RecordHeaderChange(rec);
                rec.RMANumber = RMA;
                rec.ESN = ESN;
                rec.ProjectTag = ProjectTag;
                rec.ICB = "";
                rec.PIN = "";
                rec.StatusID = nStatusID;
                if (rec.ProjectID != ProjectID)
                {
                    // If the new project is different than the old project.
                    // We need to Delete any ReceiveDetailItem values that are not questions on that project.
                }
                rec.ProjectName = Project;
                rec.ProjectID = ProjectID;
                if (decimal.TryParse(ClientLocationID, out id) == false) { id = -1; }
                rec.ClientLocationID = id;
                rValue = bm.UpdateReceiveDetail(rec, CurProcessID);

                rString = new JsonString("Result", "Saved");
                rString.AddValuePair("ReceiveHeaderID", nReceiveHeaderID.ToString());
                rString.AddValuePair("ReceiveDetailBulkID", nReceiveDetailBulkID.ToString());
                rString.AddValuePair("ReceiveDetailID", nReceiveDetailID.ToString());
                rString.AddValuePair("QTY", "1");
                rString.AddValuePair("SUFD", bm.GetSetUpFieldDef(ProjectID));
                rString.AddValuePair("CompProcList", bm.GetRequestProcessCompletionList(nReceiveDetailID));



                string MakeModelString = bm.MakeModelColourNickName(nReceiveDetailID);
                rString.AddValuePair("MMS", MakeModelString);
                MakeModelString = bm.GetProjectClientLocationBinString(nReceiveDetailID);
                rString.AddValuePair("PCLB", MakeModelString);                           //lblProjectClientLocationBinTitle




            }
            return rString.ToString();
        }




        [OperationContract]
        public string AddDataBulk(string Data)
        {
            // We need to get things ready to be received as json when going back to the page.
            JsonString rString = new JsonString("Result", "NotSaved");
            rString.AddValuePair("ReceiveHeaderID", "-1");
            rString.AddValuePair("ReceiveDetailBulkID", "-1");
            rString.AddValuePair("ReceiveDetailID", "-1");
            rString.AddValuePair("QTY", "0");
            int qty = 0;
            decimal id = -1;
            DateTime dt = DateTime.Now;

            decimal?[] rValue = { -1, -1 };
            JsonString dl = new JsonString(Data);
            Hashtable dList = dl.ListData();      // ParseData(Data);
            string ClientLocationID = dList["ClientLocationID"].ToString();
            string CurProcessID = dList["CurProcessID"].ToString();
            string NextProcessID = dList["NextProcessID"].ToString();
            string NextStepID = dList["NextStepID"].ToString();
            string ReceiveHeaderID = dList["ReceiveHeaderID"].ToString();
            string ReceiveDetailBulkID = dList["ReceiveDetailBulkID"].ToString();
            //string CurUnitItemID = dList["CurUnitItemID"].ToString();
            string CurProcess = dList["CurProcess"].ToString();
            string NextProcess = dList["NextProcess"].ToString();
            string NextStep = dList["NextStep"].ToString();
            string CurUserName = dList["CurUserName"].ToString();
            string CurStepUp = dList["CurStepUp"].ToString();
            string QTY = dList["QTY"].ToString();
            string RMA = dList["RMA"].ToString();
            string ESN = dList["ESN"].ToString();
            string sReceiveDate = dList["ReceiveDate"].ToString();

            string Project = dList["Project"].ToString();
            string sProjectID = dList["ProjectID"].ToString();
            string ProjectTag = dList["PROJTAG"].ToString();
            decimal ProjectID = -1;
            decimal.TryParse(sProjectID, out ProjectID);
            decimal nNextStepProcessID = -1;
            decimal.TryParse(NextStepID, out nNextStepProcessID);
            if (CurProcess.ToUpper() == "BULKRECEIVE")
            {

                if (decimal.TryParse(ReceiveHeaderID, out id) == false) { id = -1; }
                decimal nReceiveHeaderID = id;
                if (decimal.TryParse(ReceiveDetailBulkID, out id) == false) { id = -1; }
                decimal nReceiveDetailBulkID = id;

                ReceiveBulkManager bm = new ReceiveBulkManager(CurUserName);
                ReceiveDetailBulk rec = bm.ReceiveBulkDetail(ProjectID);
                rec.ReceiveHeaderID = nReceiveHeaderID;
                rec.RMANumber = RMA;
                rec.ESN = ESN;
                rec.ICB = "";
                rec.PIN = "";
                rec.ProjectTag = ProjectTag;
                rec.ProjectName = Project;
                rec.ProjectID = ProjectID;
                if (decimal.TryParse(ClientLocationID, out id) == false) { id = -1; }
                rec.ClientLocationID = id;
                if (decimal.TryParse(NextStepID, out id) == false) { id = -1; }
                rec.ProcessID = id;

                // This is the receive date... It needs to become more dynamic.
                if (DateTime.TryParse(sReceiveDate, out dt) == false) { dt = DateTime.Now; }
                rec.ReceiveDate = dt;
                ////////////////////////////////////////////////////////////////////
                if (int.TryParse(QTY, out qty) == false) { qty = 0; }
                rec.QTYRecorded = qty;
                //rec.QTYIntegrated = qty;
                rec.MiscNote = "";
                rValue = bm.InsertReceiveDetailBulk(rec);
                // up till now, we may not know what these two ID fields are. 
                if (decimal.TryParse(rValue[0].ToString(), out id) == false) { id = -1; }
                nReceiveHeaderID = id;
                if (decimal.TryParse(rValue[1].ToString(), out id) == false) { id = -1; }
                nReceiveDetailBulkID = id;
                // we now need to go through and save the Dataitems.
                if (decimal.TryParse(CurProcessID, out id) == false) { id = -1; }
                decimal CurrentProcessID = id;
                bm.AddDetailBulkProcessLog(nReceiveDetailBulkID, CurrentProcessID);

                //bm.Update_ReceiveDetailItemBulk_Version(nReceiveHeaderID, nReceiveDetailBulkID);
                foreach (DictionaryEntry item in dList)
                {
                    string key = item.Key.ToString();
                    key = key.Substring(3);
                    string value = item.Value.ToString();
                    List<decimal> quid = new List<decimal> { };
                    ReceiveDetailItemBulk bi = bm.ReceiveBulkDetailItem();
                    if (decimal.TryParse(key, out id) == false) { id = -1; }
                    if (id > 0)
                    {
                        Option op = bm.GetOptionRecord(id);
                        if (op != null)
                        {
                            if (quid.IndexOf(op.QuestionID) < 0)
                            {
                                quid.Add(op.QuestionID);
                                bm.Update_ReceiveDetailBulkItemOption_Version(nReceiveDetailBulkID, id);
                            }
                        }
                        bi.OptionID = id;
                        bi.Value = value;
                        bi.ReceiveDate = dt;
                        bi.ReceiveHeaderID = nReceiveHeaderID;
                        bi.ReceiveDetailBulkID = nReceiveDetailBulkID;
                        bm.InsertReceiveDetailBulkItem(bi);
                    }
                }
                rString = new JsonString("Result", "Saved");
                rString.AddValuePair("ReceiveHeaderID", rValue[0].ToString());
                rString.AddValuePair("ReceiveDetailBulkID", rValue[1].ToString());
                rString.AddValuePair("ReceiveDetailID", "-1");
                rString.AddValuePair("QTY", QTY);
            }
            return rString.ToString();
        }

        [OperationContract]
        public string LocBulkProcess(string BinNumber, string Data)
        {
            JsonString rString = new JsonString("Result", "NotSaved");
            rString.AddValuePair("BinNumber", BinNumber);

            if (BinNumber.Trim().Length == 0) { return rString.ToString(); }


            JsonString dl = new JsonString(Data, true);
            Hashtable dList = dl.ListData();      // ParseData(Data);
            Int16 xCount = 0;
            decimal id = -1;
            string CurUserName = dList["CurUserName"].ToString();
            string CurProcess = dList["CurProcess"].ToString();
            string CurProcessID = dList["CurProcessID"].ToString();
            if (decimal.TryParse(CurProcessID, out id) == false) { id = -1; }
            decimal CurrentProcessID = id;

            // We now have to get all records that have this bin number
            ReceiveDetailManager bm = new ReceiveDetailManager(CurUserName);
            using (clsLinqDataContext ctx = bm.GetDataContext(CurUserName))
            {
                List<ReceiveDetailItem> rdlist = bm.GetReceiveDetailItems_ThisDataItem(ctx, "LOCATION", BinNumber);
                foreach (ReceiveDetailItem rd in rdlist)
                {
                    xCount++;
                    bm.AddDetailProcessLog(ctx, rd.ReceiveDetailID, CurrentProcessID);
                    RecordDetailItemData(ctx, dList, CurUserName, rd.ReceiveHeaderID, rd.ReceiveDetailID, CurrentProcessID, true, false, -1, -1);
                    //
                }
            }
            rString = new JsonString("Result", "Saved");
            rString.AddValuePair("BinNumber", BinNumber);
            rString.AddValuePair("UnitCount", xCount.ToString());

            //string rstring;
            //rstring = AddDataDetail(Data);
            return rString.ToString();
        }






        [OperationContract]
        public string RMANumberUpdate(string ReceiveDetailID, string NewRMANumber, string UserName)
        {
            JsonString rString = new JsonString("Result", "Saved");
            rString.AddValuePair("NewRMANumber", NewRMANumber);
            decimal RDI = -1;
            if (decimal.TryParse(ReceiveDetailID, out RDI) == false) { RDI = -1; }
            ReceiveDetailManager dm = new ReceiveDetailManager(UserName);
            dm.UpdateRMA(RDI, NewRMANumber);
            return rString.ToString();
        }

        [OperationContract]
        public string ProjectTagUpdate(string ReceiveDetailID, string NewProjectTag, string UserName)
        {
            JsonString rString = new JsonString("Result", "Saved");
            rString.AddValuePair("NewProjectTag", NewProjectTag);
            decimal RDI = -1;
            if (decimal.TryParse(ReceiveDetailID, out RDI) == false) { RDI = -1; }
            ReceiveDetailManager dm = new ReceiveDetailManager(UserName);
            dm.UpdateProjectTag(RDI, NewProjectTag);
            return rString.ToString();
        }


        [OperationContract]
        public string XCLXProcess(string ClientLocationScanCode, string sReceiveDetailID, string UserName)
        {
            JsonString rString = new JsonString("Result", "NotSaved");
            //rString.AddValuePair("ClientLocation", ClientLocationScanCode);
            //rString.AddValuePair("ReceiveDetailID", sReceiveDetailID);

            if (ClientLocationScanCode.Trim().Length == 0 || sReceiveDetailID.Length == 0 || sReceiveDetailID == "-1") { return rString.ToString(); }


            ReceiveDetailManager bm = new ReceiveDetailManager(UserName);
            using (clsLinqDataContext ctx = bm.GetDataContext(UserName))
            {

                decimal ReceiveDetailID = -1;
                if (decimal.TryParse(sReceiveDetailID, out ReceiveDetailID) == false) { ReceiveDetailID = -1; }

                ClientLocation cl = ctx.ClientLocations.FirstOrDefault(x => x.ScanKey.ToUpper() == ClientLocationScanCode.ToUpper());
                if (cl == null)
                {
                    rString = new JsonString("Result", "Client Location Scankey not found!");
                    return rString.ToString();
                }

                ReceiveDetail rd = ctx.ReceiveDetails.FirstOrDefault(x => x.ReceiveDetailID == ReceiveDetailID);
                if (rd == null)
                {
                    rString = new JsonString("Result", "ESN/IMEI not found!");
                    return rString.ToString();
                }
                // Removed restriction on Dec 2, 2014 after Deann needed one moved. This edit check was stopping it.
                //         The reason the restriction was here was to keep the Header Location equal with the Unit Location.
                //         The header links to many units. But it is an old thing because way back a the beginning, they wanted to group
                //         all units coming in at the same time to be identifyable. Other tools have been put in place to deal with that.
                //         Removing this requrement will cause a discrepency between the header record (No longer referenced on reports, etc)
                //         to be out of sync with the Unit that it is linked to.
                if (cl.Client != rd.ClientLocation.Client)
                {
                    rString = new JsonString("Result", "Master Client does not match!");
                    return rString.ToString();
                }

                if (cl.IFSSite != rd.ClientLocation.IFSSite)
                {
                    rString = new JsonString("Result", "Sites (" + rd.ClientLocation.IFSSite + "/" + cl.IFSSite + ") Do not match!");
                    return rString.ToString();
                }

                rd.ClientLocation = cl;
                //rd.ClientLocationID = cl.ClientLocationID;
                ctx.SubmitChanges();

                //rString = new JsonString("Result", "We got this far...!");
                //return rString.ToString(); 

                rString = new JsonString("Result", "Saved");
                rString.AddValuePair("ClientLocation", ClientLocationScanCode);
                rString.AddValuePair("ReceiveDetailID", sReceiveDetailID);
                rString.AddValuePair("UnitCount", "1");
            }
            return rString.ToString();
        }


        [OperationContract]
        public string DONEXBulkProcess(string BinNumber, string Data)
        {
            JsonString rString = new JsonString("Result", "NotSaved");
            rString.AddValuePair("BinNumber", BinNumber);

            if (BinNumber.Trim().Length == 0) { return rString.ToString(); }

            JsonString dl = new JsonString(Data, true);
            Hashtable dList = dl.ListData();      // ParseData(Data);
            Int16 xCount = 0;
            decimal id = -1;
            string CurUserName = dList["CurUserName"].ToString();
            string CurProcess = dList["CurProcess"].ToString();
            string CurProcessID = dList["CurProcessID"].ToString();
            if (decimal.TryParse(CurProcessID, out id) == false) { id = -1; }
            decimal CurrentProcessID = id;

            // We now have to get all records that have this bin number
            ReceiveDetailManager rdm = new ReceiveDetailManager(CurUserName);
            using (clsLinqDataContext ctx = rdm.GetDataContext(CurUserName))
            {
                //List<ReceiveDetailItem> rdlist = bm.GetReceiveDetailItems_ThisDataItem(ctx, "BIN", BinNumber);
                List<ReceiveDetail> rdlist = rdm.GetReceiveDetail_XBINX(BinNumber);
                foreach (ReceiveDetail rd in rdlist)
                {
                    xCount++;
                    rdm.AddDetailProcessLog(ctx, rd.ReceiveDetailID, CurrentProcessID);
                    rdm.UpdateESNAttribute(rd.ReceiveDetailID, "XDONEX", CurUserName + ":" + DateTime.Now.ToShortDateString());
                    //RecordDetailItemData(ctx, dList, CurUserName, rd.ReceiveHeaderID, rd.ReceiveDetailID, CurrentProcessID, true, false, -1, -1);
                }
            }
            rString = new JsonString("Result", "Saved");
            rString.AddValuePair("BinNumber", BinNumber);
            rString.AddValuePair("UnitCount", xCount.ToString());
            return rString.ToString();
        }


        [OperationContract]
        public string GetOrderEntryESNList(string OrderNumber, string UserName)
        {
            if (OrderNumber.Length == 0) { return ""; }
            OrderManager om = new OrderManager(UserName);
            return om.GetOrderESNList(OrderNumber);
        }

        [OperationContract]
        public string IMEIBulkAdd(string ESNList, string Data, string UserName, string CloneType, bool AdvanceESN)
        {
            //clsLog log;
            //log = new clsLog(HttpContext.Current.Server.MapPath("~"), "IMEIBULKRunLog", UserName);
            //log.writeLogData = true;
            ////log.writeLogData = true;
            //log.LogIt("IMEI RUN STARTED");
            ReceiveDetailManager bm = new ReceiveDetailManager(UserName);
            ReceiveDetail Source = null;
            ReceiveDetail Target = null;
            string NewData = "";
            //string XLS = "";
            string x = ESNList;
            var y = x.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Distinct();
            //log.LogIt(" Number of ESNs to load:" + y.Count().ToString());

            foreach (string ESN in y)
            {
                if (CloneType.Length != 0)
                {
                    Source = bm.ReceiveDetail_LastVersion(ESN);
                }
                if (AdvanceESN == true)
                {
                    AdvanceESNVersion(ESN, UserName, "IMEIBULKADD");
                }
                NewData = Data.Replace("ESNGOESHERE", ESN);
                //AddDataDetail(NewData);
                AddDataDetailThreaded(NewData, "N");
                if (CloneType.Length != 0)
                {
                    if (Source != null && Source.ReceiveDetailID > 0)
                    {
                        Target = bm.ReceiveDetail_LastVersion(ESN);
                        if (Target.ReceiveDetailID > 0 && Source.ReceiveDetailID != Target.ReceiveDetailID)
                        {
                            List<string> Attributes = new List<string>();
                            Attributes.Add("Carrier");
                            Attributes.Add("Manufacturer");
                            Attributes.Add("Model");
                            Attributes.Add("Colour");
                            Attributes.Add("Disposition");          // may need to remove this one.

                            Attributes.Add("QC");                   // may need to change this to another name when Sandbox becomes live.
                            //Attributes.Add("QC Assessment");                   // may need to change this to another name when Sandbox becomes live.

                            Attributes.Add("Grade");                   // This is for when Sandbox becomes live.
                            bm.CloneAttribute(Source.ReceiveDetailID, Target.ReceiveDetailID, Attributes, true);
                        }
                    }
                }
            }
            //log.LogIt("IMEI RUN Finished");
            return "";
        }

        [OperationContract]
        public string AddDataDetailTSave(string Data)
        {
            Log.LogIt("(TSAVE BUTTON) - AddDataDetailTSave - Started(" + "" + ") ******************");
            // We need to get things ready to be received as json when going back to the page.
            JsonString rString = new JsonString("Result", "NotSaved");
            rString.AddValuePair("ReceiveHeaderID", "-1");
            rString.AddValuePair("ReceiveDetailBulkID", "-1");
            rString.AddValuePair("ReceiveDetailID", "-1");
            rString.AddValuePair("SUFD", "");
            rString.AddValuePair("QTY", "0");
            rString.AddValuePair("CompProcList", "");
            rString.AddValuePair("MMS", "");
            rString.AddValuePair("PCLB", "");
            //int qty = 0;
            decimal id = -1;

            decimal?[] rValue = { -1, -1 };
            JsonString dl = new JsonString(Data, true);
            Hashtable dList = dl.ListData();      // ParseData(Data);

            //string AuthorizationRequired = "N";

            string ClientLocationID = dList["ClientLocationID"].ToString();
            decimal dClientLocationID = -1;
            string CurProcessID = dList["CurProcessID"].ToString();
            string NextProcessID = dList["NextProcessID"].ToString();
            string NextStepID = dList["NextStepID"].ToString();
            string ReceiveHeaderID = dList["ReceiveHeaderID"].ToString();
            string ReceiveDetailBulkID = dList["ReceiveDetailBulkID"].ToString();
            string ReceiveDetailID = dList["ReceiveDetailID"].ToString();
            string sReceiveDate = dList["ReceiveDate"].ToString();
            string ProjectSetup = dList["PROJSet"].ToString();
            //string CurUnitItemID = dList["CurUnitItemID"].ToString();
            string CurProcess = dList["CurProcess"].ToString();
            string NextProcess = dList["NextProcess"].ToString();
            string NextStep = dList["NextStep"].ToString();
            string CurUserName = dList["CurUserName"].ToString();
            string CurStepUp = dList["CurStepUp"].ToString();
            string QTY = dList["QTY"].ToString();
            string RMA = dList["RMA"].ToString();
            string ESN = dList["ESN"].ToString();
            string Project = dList["Project"].ToString();
            string sProjectID = dList["ProjectID"].ToString();
            string ProjectTag = dList["PROJTAG"].ToString();
            string AllowDupAdd = dList["hdnAllowDupAdd"].ToString();
            string sReceiveDetailAuthorizationLog = dList["DoAuthorize"].ToString();

            decimal ProjectID = -1;
            decimal.TryParse(sProjectID, out ProjectID);

            Log.LogIt("Transfer Date parsed(" + ProjectSetup + ":" + CurProcess + ":" + ESN + ")");


            if (ESN.Length == 0)
            {
                rString = new JsonString("Result", "NotSaved");
                rString.AddValuePair("Error", "No ESN given");
                return rString.ToString();
            }


            #region SaveRecordTransaction
            if (CurProcess.ToUpper() != "BULKRECEIVE" && CurProcess.ToUpper() != "RECEIVEFROMBULK" && CurProcess.ToUpper() != "BULKMOVE")
            {
                ReceiveDetailManager rdm = new ReceiveDetailManager(CurUserName);
                using (clsLinqDataContext ctx = rdm.GetDataContext(CurUserName))
                {
                    if (rdm.isDetailThere(ctx, ESN) == false && rdm.isOKToAddFromThisProcess(CurProcess) == false)
                    {
                        rString = new JsonString("Result", "NotSaved");
                        rString.AddValuePair("Error", "Unable to add from this process:" + CurProcess);
                        return rString.ToString();
                    }

                    if (decimal.TryParse(ReceiveDetailID, out id) == false) { id = -1; }
                    decimal nReceiveDetailID = id;
                    if (decimal.TryParse(CurProcessID, out id) == false) { id = -1; }
                    decimal CurrentProcessID = id;
                    if (decimal.TryParse(ClientLocationID, out id) == false) { id = -1; }
                    dClientLocationID = id;

                    if (RMA.Length == 0 && ProjectSetup.ToUpper().Contains("ZRMAZZAUTORZ")) { RMA = rdm.NextRMANumber(ctx, dClientLocationID, CurrentProcessID); }
                    if (RMA.Length == 0 && ProjectSetup.ToUpper().Contains("ZRMAZZAUTOWZ")) { RMA = rdm.NextWorkOrderNumber(ctx); }

                    ReceiveDetailSaveTransaction st = new ReceiveDetailSaveTransaction();
                    st.CreateDate = DateTime.Now;
                    st.CreateUser = CurUserName;
                    st.ESN = ESN;
                    st.ProjectTag = "";
                    st.RMANumber = RMA;
                    st.ReceiveDetailID = nReceiveDetailID;
                    st.RecordDetailString = Data;
                    st.Status = "New";
                    st.StatusMessage = "Batched";
                    rdm.ReceiveDetailSaveTransaction(ctx, st);

                    Log.LogIt("Return Message Build Started");
                    rString = new JsonString("Result", "Saved");
                    rString.AddValuePair("ReceiveHeaderID", "-1");
                    rString.AddValuePair("ReceiveDetailBulkID", "-1");
                    rString.AddValuePair("ReceiveDetailID", nReceiveDetailID.ToString());
                    rString.AddValuePair("SUFD", "");
                    rString.AddValuePair("QTY", "");
                    rString.AddValuePair("CompProcList", "");
                    rString.AddValuePair("MMS", "");
                    rString.AddValuePair("AR", "");
                    rString.AddValuePair("PCLB", "");
                    Log.LogIt("Return Message Build Done");

                }
            }
            #endregion

            #region SaveRecordDataLive
            //if (CurProcess.ToUpper() != "BULKRECEIVE" && CurProcess.ToUpper() != "RECEIVEFROMBULK" && CurProcess.ToUpper() != "BULKMOVE")
            //{
            //    ReceiveDetailManager rdm = new ReceiveDetailManager(CurUserName);
            //    using (clsLinqDataContext ctx = rdm.GetDataContext(CurUserName))
            //    {
            //        ReceiveDetail rec = null;
            //        if (decimal.TryParse(ReceiveHeaderID, out id) == false) { id = -1; }
            //        decimal nReceiveHeaderID = id;
            //        if (decimal.TryParse(ReceiveDetailBulkID, out id) == false) { id = -1; }
            //        decimal nReceiveDetailBulkID = id;
            //        if (decimal.TryParse(ReceiveDetailID, out id) == false) { id = -1; }
            //        decimal nReceiveDetailID = id;
            //        if (decimal.TryParse(CurProcessID, out id) == false) { id = -1; }
            //        decimal CurrentProcessID = id;
            //        if (decimal.TryParse(ClientLocationID, out id) == false) { id = -1; }
            //        dClientLocationID = id;
            //        if (decimal.TryParse(sReceiveDetailAuthorizationLog, out id) == false) { id = -1; }
            //        decimal ReceiveDetailAuthorizationLog = id;
            //        ////if (CurProcess.ToUpper() == "RECEIVEDOA")
            //        if (RMA.Length == 0 && ProjectSetup.ToUpper().Contains("ZRMAZZAUTORZ"))
            //        {
            //            RMA = rdm.NextRMANumber(ctx, dClientLocationID, CurrentProcessID);
            //        }
            //        //if (CurProcess.ToUpper() == "RECEIVEINWARRANTY" || CurProcess.ToUpper() == "RECEIVEEXWARRANTY" || CurProcess.ToUpper() == "RECEIVEOOWARRANTY")
            //        if (RMA.Length == 0 && ProjectSetup.ToUpper().Contains("ZRMAZZAUTOWZ"))
            //        {
            //            RMA = rdm.NextWorkOrderNumber(ctx);
            //        }
            //        Log.LogIt("RMA auto set if needed Done");
            //        if (rdm.isDetailThere(ctx, ESN) == false && rdm.isOKToAddFromThisProcess(CurProcess) == false)
            //        {
            //            rString = new JsonString("Result", "NotSaved");
            //            rString.AddValuePair("Error", "Unable to add from this process:" + CurProcess);
            //            return rString.ToString();
            //        }
            //        rec = rdm.ReceiveDetail_NewOrThisESN(ctx, ESN);
            //        if (rec == null) { return rString.ToString(); }
            //        if (decimal.TryParse(NextStepID, out id) == false) { id = -1; }
            //        rec.ProcessID = id;
            //        Log.LogIt("Loaded Existing ESN(" + ESN + ") or created a new one Done");
            //        //dt = DateTime.ParseExact(sReceiveDate, "MM/dd/yyyy hh:mm tt",System.Globalization.DateTimeFormatInfo.CurrentInfo);
            //        //if (DateTime.TryParse(sReceiveDate, out dt) == false) { dt = DateTime.Now; }
            //        ////////////////////////////////////////////////////////////////////
            //        if (int.TryParse(QTY, out qty) == false) { qty = 0; }
            //        rec.QTYIntegrated = qty;
            //        rec.MiscNote = "";
            //        if (rec.ReceiveDetailID > 0)
            //        {
            //            if (decimal.TryParse(ClientLocationID, out id) == false) { id = -1; }
            //            if (rec.ClientLocationID < 1 && id > 0)
            //            {
            //                rec.ClientLocationID = id;
            //                rdm.UpdateReceiveDetailClientLocation(ctx, rec.ReceiveDetailID, rec.ReceiveHeaderID, id);
            //            }
            //            rValue = rdm.UpdateReceiveDetail(ctx, rec, CurrentProcessID);
            //            Log.LogIt("Update Receive Detail");
            //        }
            //        if (rec.ReceiveDetailID < 1)
            //        {
            //            rec.ClientLocationID = dClientLocationID;
            //            rec.ReceiveHeaderID = nReceiveHeaderID;
            //            rec.RMANumber = RMA;
            //            rec.ESN = ESN;
            //            rec.ProjectTag = ProjectTag;
            //            rec.ICB = "";
            //            rec.PIN = "";
            //            rec.ProjectName = Project;
            //            rec.ProjectID = ProjectID;
            //            rec.ReceiveDate = DateTime.Now;
            //            rValue = rdm.InsertReceiveDetail(ctx, rec, CurrentProcessID);
            //            Log.LogIt("Insert ReceiveDetail");
            //        }
            //        // up till now, we may not know what these two ID fields are. 
            //        if (decimal.TryParse(rValue[0].ToString(), out id) == false) { id = -1; }
            //        nReceiveHeaderID = id;
            //        if (decimal.TryParse(rValue[1].ToString(), out id) == false) { id = -1; }
            //        nReceiveDetailID = id;
            //        rdm.AddDetailProcessLog(ctx, nReceiveDetailID, CurrentProcessID);
            //        Log.LogIt("Add Detail Process Log");
            //        // Send out Billing Record
            //        rdm.AddBillingPoint(ctx, nReceiveDetailID, dClientLocationID, ProjectID, CurrentProcessID);
            //        Log.LogIt("Add Billing Point");
            //        RecordDetailItemData(ctx, dList, CurUserName, nReceiveHeaderID, nReceiveDetailID, CurrentProcessID, false, false, ProjectID, dClientLocationID);
            //        Log.LogIt("Add/Save Detail Item Data");
            //        // if process = "receive" then we need to look to our prereceive inventory.
            //        if (CurProcess.Substring(0, 7).ToUpper() == "RECEIVE")
            //        {
            //            rdm.OffestPreReceive(ctx, nReceiveDetailID, ESN);
            //            Log.LogIt("Pre Receive Offset Done");
            //        }
            //        #region KITTING
            //        if (CurProcess.ToUpper() == "KITTING")  // 
            //        {
            //            Log.LogIt("Kitting Process Started");
            //            rdm.SM.Kitted(ctx, rec.ReceiveDetailID, -1, rec.ClientLocationID, (decimal)rec.ProjectID, CurrentProcessID, CurUserName);
            //            rdm.UpdateESNAttribute_Blank(ctx, nReceiveDetailID, "Bin");
            //            rdm.UpdateESNAttribute_Blank(ctx, nReceiveDetailID, "Location");
            //            bool isSwapped = rdm.SwapIMEI(ctx, nReceiveDetailID);
            //            Log.LogIt("Kitting Process Done");
            //        }
            //        #endregion
            //        #region SHIPPING
            //        if (CurProcess.ToUpper() == "SHIPPING")  // This unit has been shipped, we want the version changed from 000.  000 denotes GMP possesion, !000 denotes out the door.
            //        {
            //            Log.LogIt("Shipping Process Started");
            //            rdm.UpdateESNAttribute_Blank(ctx, rec.ReceiveDetailID, "Bin");
            //            rdm.SM.OrderShipped(ctx, rec.ReceiveDetailID, -1, rec.ClientLocationID, (decimal)rec.ProjectID, CurrentProcessID, CurUserName);
            //            AdvanceESNVersion(ctx, ESN, CurUserName);
            //            Log.LogIt("Shipping process Done");
            //        }
            //        #endregion
            //        #region GMPRepair
            //        if (CurProcess.ToUpper() == "GMP REPAIR")
            //        {
            //            Log.LogIt("GMP Repair Started");
            //            // Look to see if Authorization is required.
            //            string IsAuthorizationRequired = rdm.GetReceiveDetailItem_DataElement(ctx, nReceiveDetailID, "Authorization");
            //            if (IsAuthorizationRequired.ToUpper() == "APPORVAL REQUIRED")
            //            {
            //                AuthorizationRequired = "Y";
            //                rdm.UpdateESNAttribute_Blank(ctx, nReceiveDetailID, "Authorization");
            //                ReceiveDetailAuthohrizationManager rdam = new ReceiveDetailAuthohrizationManager(CurUserName);
            //                decimal ef = 0;
            //                decimal ff = 0;
            //                decimal hst = 0;
            //                decimal total = 0;
            //                string Note = "";
            //                string Note1 = rdm.GetReceiveDetailItem_DataElement(ctx, nReceiveDetailID, "Claim Action");
            //                string Note2 = rdm.GetReceiveDetailItem_DataElement(ctx, nReceiveDetailID, "Claim Reason");
            //                string Note3 = rdm.GetReceiveDetailItem_DataElement(ctx, nReceiveDetailID, "Claim Location");
            //                if (Note1.Length > 0) { Note = Note1 + "\n"; }
            //                if (Note2.Length > 0) { Note += Note2 + "\n"; }
            //                if (Note3.Length > 0) { Note += Note3; }
            //                string sef = rdm.GetReceiveDetailItem_DataElement(ctx, nReceiveDetailID, "Est"); ;
            //                string sff = rdm.GetReceiveDetailItem_DataElement(ctx, nReceiveDetailID, "Fee"); ;
            //                string shst = "0";                // rdm.GetReceiveDetailItem_DataElement(nReceiveDetailID, "Authorization"); ;
            //                string stotal = "0";              //rdm.GetReceiveDetailItem_DataElement(nReceiveDetailID, "Authorization"); ;
            //                if (decimal.TryParse(sef, out ef) == false) { ef = 0; }
            //                if (decimal.TryParse(sff, out ff) == false) { ff = 0; }
            //                if (decimal.TryParse(shst, out hst) == false) { hst = 0; }
            //                if (decimal.TryParse(stotal, out total) == false) { total = 0; }
            //                rdam.AddNewRequest(ctx, nReceiveDetailID, ef, ff, hst, total, Note, "AUT");
            //            }
            //            Log.LogIt("GMP Repair Done");
            //        }
            //        if (ReceiveDetailAuthorizationLog > 0)         // we are saving on a unit that had an authorization scanned in.
            //        {
            //            ReceiveDetailAuthohrizationManager rdam = new ReceiveDetailAuthohrizationManager(CurUserName);
            //            rdam.Complete(ctx, ReceiveDetailAuthorizationLog, CurUserName);
            //            Log.LogIt("Authorization Scan set to Complete");
            //        }
            //        #endregion
            //        Log.LogIt("Return Message Build Started");
            //        rString = new JsonString("Result", "Saved");
            //        rString.AddValuePair("ReceiveHeaderID", nReceiveHeaderID.ToString());
            //        rString.AddValuePair("ReceiveDetailBulkID", nReceiveDetailBulkID.ToString());
            //        rString.AddValuePair("ReceiveDetailID", nReceiveDetailID.ToString());
            //        rString.AddValuePair("SUFD", rdm.GetSetUpFieldDef(ctx, ProjectID));
            //        rString.AddValuePair("QTY", QTY);
            //        rString.AddValuePair("CompProcList", rdm.GetRequestProcessCompletionList(ctx, nReceiveDetailID));
            //        string MakeModelString = rdm.MakeModelColourNickName(ctx, nReceiveDetailID);
            //        rString.AddValuePair("MMS", MakeModelString);
            //        rString.AddValuePair("AR", AuthorizationRequired);
            //        MakeModelString = rdm.GetProjectClientLocationBinString(ctx, nReceiveDetailID);
            //        rString.AddValuePair("PCLB", MakeModelString);                           //lblProjectClientLocationBinTitle
            //        Log.LogIt("Return Message Build Done");
            //    }
            //}
            #endregion

            Log.LogIt("TSave Finished");
            //Log.writeLogData = false;
            return rString.ToString();
        }





        //private void RecordDetailItemData(Hashtable dList, string CurUserName, decimal nReceiveHeaderID, decimal nReceiveDetailID, decimal CurrentProcessID, bool isXBinXUpdate, bool DoCalc)
        //{
        //    RecordDetailItemData(dList, CurUserName, nReceiveHeaderID, nReceiveDetailID, CurrentProcessID, isXBinXUpdate, DoCalc, -1, -1);
        //}
        //private void RecordDetailItemData(Hashtable dList, string CurUserName, decimal nReceiveHeaderID, decimal nReceiveDetailID, decimal CurrentProcessID, bool isXBinXUpdate, bool DoCalc, decimal ProjectID, decimal ClientLocationID)
        //{
        //    //    RecordDetailItemData(dList, CurUserName, nReceiveHeaderID, nReceiveDetailID, CurrentProcessID, isXBinXUpdate, DoCalc, -1, -1);
        //}        
        //[OperationContract]
        //public string AuthorizeRequired(string sReceiveDetailAuthorizationLogID, string sReceiveDetailID, string UserName)
        //{
        //    if (sReceiveDetailAuthorizationLogID.Trim().Length == 0 || sReceiveDetailAuthorizationLogID == "-1") { return ""; }
        //    if (sReceiveDetailID.Trim().Length == 0 || sReceiveDetailID == "-1") { return ""; }

        //    decimal id = -1;
        //    if (decimal.TryParse(sReceiveDetailAuthorizationLogID, out id) == false) { id = -1; }
        //    decimal ReceiveDetailAuthorizationLogID = id;

        //    if (decimal.TryParse(sReceiveDetailID, out id) == false) { id = -1; }
        //    decimal ReceiveDetailID = id;

        //    ReceiveDetailAuthohrizationManager rdam = new ReceiveDetailAuthohrizationManager(UserName);
        //    rdam.r(ReceiveDetailAuthorizationLogID, UserName);

        //    ReceiveDetailManager rdm = new ReceiveDetailManager(UserName);
        //    ReceiveDetail rd = rdm.ReceiveDetail(ReceiveDetailID);
        //    if (rd != null) { return "ESN:" + rd.ESN + " - Authorized Compete"; }
        //    else { return " Authorized Complete"; }
        //}


        [OperationContract]
        public string DeclineAuthorization(string sReceiveDetailAuthorizationLogID, string AuthorizationName, string UserName)
        {
            decimal ReceiveDetailAuthorizationLogID = -1;
            if (decimal.TryParse(sReceiveDetailAuthorizationLogID, out ReceiveDetailAuthorizationLogID) == false) { ReceiveDetailAuthorizationLogID = -1; }
            ReceiveDetailAuthohrizationManager rdm = new ReceiveDetailAuthohrizationManager(UserName);
            rdm.Decline(ReceiveDetailAuthorizationLogID, AuthorizationName);
            return "";
        }

        [OperationContract]
        public string AuthorizeAuthorization(string sReceiveDetailAuthorizationLogID, string sReceiveDetailID, string UserName)
        {
            // The Physical Paper has been received
            decimal ReceiveDetailAuthorizationLogID = -1;
            if (decimal.TryParse(sReceiveDetailAuthorizationLogID, out ReceiveDetailAuthorizationLogID) == false) { ReceiveDetailAuthorizationLogID = -1; }
            ReceiveDetailAuthohrizationManager rdm = new ReceiveDetailAuthohrizationManager(UserName);
            rdm.Received(ReceiveDetailAuthorizationLogID, UserName);

            if (sReceiveDetailID.Trim().Length == 0 || sReceiveDetailID == "-1") { return ""; }
            decimal id = -1;
            if (decimal.TryParse(sReceiveDetailID, out id) == false) { id = -1; }
            decimal ReceiveDetailID = id;

            ReceiveDetailManager rdma = new ReceiveDetailManager(UserName);
            ReceiveDetail rd = rdma.ReceiveDetail(ReceiveDetailID);
            if (rd != null) { return "ESN:" + rd.ESN + " - Authorize Acquired"; }
            else { return " Authorize Acquired"; }
        }

        [OperationContract]
        public string DealerAuthorized(string sReceiveDetailAuthorizationLogID, string AuthorizationName, string UserName, string IPAddress)
        {
            // The Physical Paper has been received
            decimal ReceiveDetailAuthorizationLogID = -1;
            if (decimal.TryParse(sReceiveDetailAuthorizationLogID, out ReceiveDetailAuthorizationLogID) == false) { ReceiveDetailAuthorizationLogID = -1; }
            try
            {
                ReceiveDetailAuthohrizationManager rdm = new ReceiveDetailAuthohrizationManager(UserName);
                rdm.Authorize(ReceiveDetailAuthorizationLogID, AuthorizationName, IPAddress);
            }
            catch (Exception ex)
            {
                Log.LogIt(ex.ToString());
            }
            return "";

        }
        [OperationContract]
        public string AuthorizeRepair(string sReceiveDetailAuthorizationLogID, string sReceiveDetailID, string UserName)
        {
            if (sReceiveDetailAuthorizationLogID.Trim().Length == 0 || sReceiveDetailAuthorizationLogID == "-1") { return ""; }
            if (sReceiveDetailID.Trim().Length == 0 || sReceiveDetailID == "-1") { return ""; }

            decimal id = -1;
            if (decimal.TryParse(sReceiveDetailAuthorizationLogID, out id) == false) { id = -1; }
            decimal ReceiveDetailAuthorizationLogID = id;

            if (decimal.TryParse(sReceiveDetailID, out id) == false) { id = -1; }
            decimal ReceiveDetailID = id;

            ReceiveDetailAuthohrizationManager rdam = new ReceiveDetailAuthohrizationManager(UserName);
            rdam.AddNewRequest(ReceiveDetailID, 0, 0, 0, 0, "", "");

            ReceiveDetailManager rdm = new ReceiveDetailManager(UserName);
            ReceiveDetail rd = rdm.ReceiveDetail(ReceiveDetailID);
            if (rd != null) { return "ESN:" + rd.ESN + " - Authorize Required"; }
            else { return " Authorized Required"; }
        }

        [OperationContract]
        public string AuthorizeRepairOnly(string sReceiveDetailAuthorizationLogID, string sReceiveDetailID, string UserName, string IPAddress)
        {
            if (sReceiveDetailAuthorizationLogID.Trim().Length == 0 || sReceiveDetailAuthorizationLogID == "-1") { return ""; }
            if (sReceiveDetailID.Trim().Length == 0 || sReceiveDetailID == "-1") { return ""; }

            decimal id = -1;
            if (decimal.TryParse(sReceiveDetailAuthorizationLogID, out id) == false) { id = -1; }
            decimal ReceiveDetailAuthorizationLogID = id;

            if (decimal.TryParse(sReceiveDetailID, out id) == false) { id = -1; }
            decimal ReceiveDetailID = id;

            ReceiveDetailAuthohrizationManager rdam = new ReceiveDetailAuthohrizationManager(UserName);
            rdam.Authorize(ReceiveDetailAuthorizationLogID, UserName, IPAddress);

            ReceiveDetailManager rdm = new ReceiveDetailManager(UserName);
            ReceiveDetail rd = rdm.ReceiveDetail(ReceiveDetailID);
            if (rd != null) { return "ESN:" + rd.ESN + " - Authorized"; }
            else { return " Authorized"; }
        }
        [OperationContract]
        public string AuthorizeDecline(string sReceiveDetailAuthorizationLogID, string sReceiveDetailID, string UserName)
        {
            if (sReceiveDetailAuthorizationLogID.Trim().Length == 0 || sReceiveDetailAuthorizationLogID == "-1") { return ""; }
            if (sReceiveDetailID.Trim().Length == 0 || sReceiveDetailID == "-1") { return ""; }

            decimal id = -1;
            if (decimal.TryParse(sReceiveDetailAuthorizationLogID, out id) == false) { id = -1; }
            decimal ReceiveDetailAuthorizationLogID = id;

            if (decimal.TryParse(sReceiveDetailID, out id) == false) { id = -1; }
            decimal ReceiveDetailID = id;

            ReceiveDetailAuthohrizationManager rdam = new ReceiveDetailAuthohrizationManager(UserName);
            rdam.Decline(ReceiveDetailAuthorizationLogID, UserName);

            ReceiveDetailManager rdm = new ReceiveDetailManager(UserName);
            ReceiveDetail rd = rdm.ReceiveDetail(ReceiveDetailID);
            if (rd != null) { return "ESN:" + rd.ESN + " - Authorized Declined"; }
            else { return " Authorized Declined"; }
        }

        [OperationContract]
        public string DeleteDataDetail(string Data)
        {
            // We need to get things ready to be received as json when going back to the page.
            JsonString rString = new JsonString("Result", "NotSaved");
            rString.AddValuePair("ReceiveHeaderID", "-1");
            rString.AddValuePair("ReceiveDetailBulkID", "-1");
            rString.AddValuePair("ReceiveDetailID", "-1");
            rString.AddValuePair("QTY", "0");
            //int qty = 0;
            decimal id = -1;
            DateTime dt = DateTime.Now;

            decimal?[] rValue = { -1, -1 };
            JsonString dl = new JsonString(Data);
            Hashtable dList = dl.ListData();      // ParseData(Data);
            string ReceiveDetailID = dList["ReceiveDetailID"].ToString();
            //string CurUnitItemID = dList["CurUnitItemID"].ToString();
            string CurProcess = dList["CurProcess"].ToString();
            string CurUserName = dList["CurUserName"].ToString();

            if (CurProcess.ToUpper() != "BULKRECEIVE" && CurProcess.ToUpper() != "RECEIVEFROMBULK" && CurProcess.ToUpper() != "BULKMOVE")
            {
                if (decimal.TryParse(ReceiveDetailID, out id) == false) { id = -1; }
                decimal nReceiveDetailID = id;


                ReceiveDetailManager bm = new ReceiveDetailManager(CurUserName);
                //ReceiveDetail rec = null;
                // if not "EDIT", then we need a new RECEVE RECORD
                bm.DeleteReceiveDetailThisID(nReceiveDetailID);

                rString = new JsonString("Result", "Deleted");
                rString.AddValuePair("ReceiveDetailID", nReceiveDetailID.ToString());
            }
            return rString.ToString();
        }

        [OperationContract]
        public string MoveDataBulk(string Data, string TargetData)
        {
            // We need to get things ready to be received as json when going back to the page.
            JsonString rString = new JsonString("Result", "NotFound");
            int qty = 0;
            decimal nReceiveHeaderID = -1;
            decimal ReceiveDetailBulkID = -1;
            decimal id = -1;
            DateTime dt = DateTime.Now;

            decimal?[] rValue = { -1, -1 };
            JsonString dlt = new JsonString(TargetData);
            Hashtable dtList = dlt.ListData();      // ParseData(Data);

            JsonString dl = new JsonString(Data);
            Hashtable dList = dl.ListData();      // ParseData(Data);
            string ClientLocationID = dList["ClientLocationID"].ToString();
            string CurProcessID = dList["CurProcessID"].ToString();
            decimal CurrentProcessID = -1;
            if (decimal.TryParse(CurProcessID, out CurrentProcessID) == false) { CurrentProcessID = -1; }

            string NextProcessID = dList["NextProcessID"].ToString();
            string NextStepID = dList["NextStepID"].ToString();
            string ReceiveHeaderID = dList["ReceiveHeaderID"].ToString();
            string ReceiveDetailID = dList["ReceiveDetailID"].ToString();
            //string CurUnitItemID = dList["CurUnitItemID"].ToString();
            string CurProcess = dList["CurProcess"].ToString();
            string NextProcess = dList["NextProcess"].ToString();
            string NextStep = dList["NextStep"].ToString();
            string CurUserName = dList["CurUserName"].ToString();
            string CurStepUp = dList["CurStepUp"].ToString();
            string QTY = dList["QTY"].ToString();

            string RMA = dList["RMA"].ToString();
            string ESN = dList["ESN"].ToString();
            string ProjectTag = dList["PROJTAG"].ToString();
            //string sReceiveDate = dList["ReceiveDate"].ToString();
            string sReceiveDate = dList["ReceiveDate"].ToString();
            //string ICB = dList["ICB"].ToString();
            //string PIN = dList["PIN"].ToString();
            string Project = dList["Project"].ToString();
            string sProjectID = dList["ProjectID"].ToString();
            decimal ProjectID = -1;
            decimal.TryParse(sProjectID, out ProjectID);
            if (int.TryParse(QTY, out qty) == false) { qty = 1; }
            if (qty == 0) { qty = 0; }

            if (CurProcess.ToUpper() == "BULKMOVE")
            {
                if (decimal.TryParse(ClientLocationID, out id) == false) { id = -1; }
                ReceiveBulkManager rbm = new ReceiveBulkManager(CurUserName);
                ReceiveDetailManager rdm = new ReceiveDetailManager(CurUserName);
                List<ReceiveDetailBulk> dbl = rdm.IsBulkDetailThere_All(id, RMA, ProjectTag, ProjectID, dl);

                if (dbl == null || dbl.Count() == 0)   // Proper detail line not found. see what to do
                {
                    rString.AddValuePair("Error", "No Data Found");
                    return rString.ToString();
                }
                // see if we have enough empty units to equal the total
                decimal qtotal = (decimal)dbl.Sum(x => x.QTYRecorded) - (decimal)dbl.Sum(x => x.QTYIntegrated);
                if (qtotal < 1 || qty < 1 || qtotal < qty)
                {
                    rString.AddValuePair("Error", "Data Not saved: Only (" + qtotal.ToString() + ") product found");
                    return rString.ToString();
                }

                decimal ReceiveDetailBulkSourceID = -1;
                decimal ReceiveDetailBulkTargetID = -1;

                var BulkData = from xx in dbl select new { xx.ReceiveDetailBulkID, xx.ReceiveHeaderID, xx.QTYRecorded, xx.QTYIntegrated, xqty = xx.QTYRecorded - xx.QTYIntegrated };
                foreach (var rd in BulkData.OrderBy(x => x.xqty).Where(x => x.xqty > 0))
                {
                    //foreach (ReceiveDetailBulk rd in dbl.OrderBy(x=>x.QTYRecorded).ThenBy(x=>x.QTYIntegrated))
                    //{
                    ReceiveDetailBulkSourceID = rd.ReceiveDetailBulkID;
                    ReceiveDetailBulkID = rd.ReceiveDetailBulkID;
                    nReceiveHeaderID = rd.ReceiveHeaderID;

                    if (qty < 1)
                    {
                        break;
                    }
                    if (rd.xqty <= qty)  // Easiest one, just update with the new data.
                    {
                        // we just want to update what we have
                        ReceiveDetailBulkTargetID = ReceiveDetailBulkSourceID;
                        ReceiveDetailBulk rec = rbm.ReceiveBulkDetail_ThisOne(ReceiveDetailBulkTargetID);
                        if (rec == null)
                        {
                            rString.AddValuePair("Error", "No product found -- dberror!");
                            return rString.ToString();
                        }
                        //rbm.UpdateReceiveDetailBulk(rec);
                        rbm.AddDetailBulkProcessLog(ReceiveDetailBulkTargetID, CurrentProcessID);
                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        SetBulkItemDetail(nReceiveHeaderID, id, dt, dtList, rbm, rdm, ReceiveDetailBulkTargetID);
                        MoveSourceDetailToTargetDetail(qty, nReceiveHeaderID, ref id, dt, ref rValue, dtList, CurrentProcessID, rdm, ReceiveDetailBulkSourceID, ReceiveDetailBulkTargetID);
                        if (rd.xqty == qty)
                        {
                            qty = 0;
                            rString = new JsonString("Result", "Saved");
                            break;
                        }
                        else { qty -= (int)rd.xqty; }
                    }
                    else if (rd.xqty > qty)
                    {
                        ReceiveDetailBulk recSource = rbm.ReceiveBulkDetail_ThisOne(ReceiveDetailBulkSourceID);
                        ReceiveDetailBulk recTarget = rbm.ReceiveBulkDetail();
                        // we Need to create a new Record for the difference
                        if (recTarget == null || recSource == null)
                        {
                            rString.AddValuePair("Error", "No product found -- dberror!");
                            return rString.ToString();
                        }
                        recTarget.ReceiveHeaderID = recSource.ReceiveHeaderID;
                        recTarget.ReceiveDate = recSource.ReceiveDate;
                        recTarget.QTYRecorded = qty;
                        recSource.QTYRecorded -= qty;    // <<<< Remove the qty from the source recorded
                        recTarget.QTYIntegrated = 0;
                        recTarget.ProjectTag = recSource.ProjectTag;
                        recTarget.ProjectName = recSource.ProjectName;
                        recTarget.ProjectID = recSource.ProjectID;
                        recTarget.ProcessID = recSource.ProcessID;
                        recTarget.RMANumber = recSource.RMANumber;
                        recTarget.PIN = recSource.PIN;
                        recTarget.MiscNote = recSource.MiscNote;
                        recTarget.LastUpdateUser = CurUserName;
                        recTarget.LastUpdateDate = DateTime.Now;
                        recTarget.ICB = recSource.ICB;
                        recTarget.ESN = recSource.ESN;
                        recTarget.CreateUser = CurUserName;
                        recTarget.CreateDate = DateTime.Now;
                        recTarget.ClientLocationID = recSource.ClientLocationID;
                        recTarget.BinLocationID = recSource.BinLocationID;
                        decimal?[] xid = rbm.InsertReceiveDetailBulk(recTarget, false);   // Insert the new target
                        if (xid[1] < 1)
                        {
                            rString.AddValuePair("Error", "Unable to save Record -- dberror!");
                            return rString.ToString();
                        }
                        rbm.UpdateReceiveDetailBulk(recSource);          // update the source and remove the QTY
                        ReceiveDetailBulkTargetID = (decimal)xid[1];
                        rbm.AddDetailBulkProcessLog(ReceiveDetailBulkTargetID, CurrentProcessID);
                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        SetBulkItemDetail(nReceiveHeaderID, id, dt, dtList, rbm, rdm, ReceiveDetailBulkTargetID);
                        MoveSourceDetailToTargetDetail(qty, nReceiveHeaderID, ref id, dt, ref rValue, dtList, CurrentProcessID, rdm, ReceiveDetailBulkSourceID, ReceiveDetailBulkTargetID);
                        qty = 0;
                        rString = new JsonString("Result", "Saved");
                        break;
                    }
                }

            }
            return rString.ToString();
        }

        [OperationContract]
        public string ReceiveDataFromBulk(string Data)
        {
            // We need to get things ready to be received as json when going back to the page.
            bool add = false;
            JsonString rString = new JsonString("Result", "NotFound");
            int qty = 0;
            decimal nReceiveHeaderID = -1;
            decimal ReceiveDetailBulkID = -1;
            decimal id = -1;
            DateTime dt = DateTime.Now;

            decimal?[] rValue = { -1, -1 };

            JsonString dl = new JsonString(Data);
            Hashtable dList = dl.ListData();      // ParseData(Data);
            string ClientLocationID = dList["ClientLocationID"].ToString();
            string CurProcessID = dList["CurProcessID"].ToString();
            string NextProcessID = dList["NextProcessID"].ToString();
            string NextStepID = dList["NextStepID"].ToString();
            string ReceiveHeaderID = dList["ReceiveHeaderID"].ToString();
            string ReceiveDetailID = dList["ReceiveDetailID"].ToString();
            //string CurUnitItemID = dList["CurUnitItemID"].ToString();
            string CurProcess = dList["CurProcess"].ToString();
            string NextProcess = dList["NextProcess"].ToString();
            string NextStep = dList["NextStep"].ToString();
            string CurUserName = dList["CurUserName"].ToString();
            string CurStepUp = dList["CurStepUp"].ToString();
            string QTY = dList["QTY"].ToString();
            string RMA = dList["RMA"].ToString();
            string ESN = dList["ESN"].ToString();
            string ProjectTag = dList["PROJTAG"].ToString();
            //string sReceiveDate = dList["ReceiveDate"].ToString();
            string sReceiveDate = dList["ReceiveDate"].ToString();
            //string ICB = dList["ICB"].ToString();
            //string PIN = dList["PIN"].ToString();
            string Project = dList["Project"].ToString();
            string sProjectID = dList["ProjectID"].ToString();
            decimal ProjectID = -1;
            decimal.TryParse(sProjectID, out ProjectID);



            if (CurProcess.ToUpper() == "RECEIVEFROMBULK")
            {
                if (decimal.TryParse(ClientLocationID, out id) == false) { id = -1; }
                ReceiveDetailManager bm = new ReceiveDetailManager(CurUserName);
                ReceiveDetailBulk db = bm.IsBulkDetailThere(id, RMA, ProjectTag, ProjectID, dl);
                clsLinqDataContext ctx = bm.GetDataContext(CurUserName);
                if (db == null)   // Proper detail line not found. see what to do
                {
                    return rString.ToString();
                }
                rString = new JsonString("Result", "NotSaved");

                if (db != null)
                {
                    bm.TakeOneAwayFromBulk(db.ReceiveDetailBulkID);
                    ReceiveDetailBulkID = db.ReceiveDetailBulkID;
                    nReceiveHeaderID = db.ReceiveHeaderID;
                }
                if (decimal.TryParse(ReceiveHeaderID, out id) == false) { id = -1; }
                nReceiveHeaderID = id;
                decimal nReceiveDetailBulkID = ReceiveDetailBulkID;
                if (decimal.TryParse(ReceiveDetailID, out id) == false) { id = -1; }
                decimal nReceiveDetailID = id;


                bool isNew = false;
                ReceiveDetail rec = bm.GetBulkReceiveDetail(nReceiveDetailBulkID);
                if (rec == null)
                {
                    rec = bm.ReceiveDetail();
                    isNew = true;
                }
                rec.RMANumber = RMA;
                rec.ESN = ESN;
                rec.ICB = "";
                rec.PIN = "";
                rec.ProjectTag = ProjectTag;
                rec.ProjectName = Project;
                rec.ProjectID = ProjectID;
                if (decimal.TryParse(ClientLocationID, out id) == false) { id = -1; }
                rec.ClientLocationID = id;
                if (decimal.TryParse(NextStepID, out id) == false) { id = -1; }
                rec.ProcessID = id;
                if (int.TryParse(QTY, out qty) == false) { qty = 1; }
                if (qty == 0) { qty = 1; }
                rec.QTYIntegrated = qty;
                rec.MiscNote = "";
                if (isNew == true)
                {
                    if (DateTime.TryParse(sReceiveDate, out dt) == false) { dt = DateTime.Now; }
                    rec.ReceiveDate = dt;
                    rec.ReceiveHeaderID = nReceiveHeaderID;
                    rec.ReceiveDetailBulkID = ReceiveDetailBulkID;
                    rValue = bm.InsertReceiveDetailFromBulk(rec);
                }
                if (isNew == false)
                {
                    if (decimal.TryParse(CurProcessID, out id) == false) { id = -1; }
                    bm.UpdateReceiveDetail(rec, id);
                    rValue = new decimal?[] { (decimal?)rec.ReceiveHeaderID, (decimal?)rec.ReceiveDetailID };
                }

                //rValue = bm.InsertReceiveDetail(rec);

                // up till now, we may not know what these two ID fields are. 
                if (decimal.TryParse(rValue[0].ToString(), out id) == false) { id = -1; }
                nReceiveHeaderID = id;
                if (decimal.TryParse(rValue[1].ToString(), out id) == false) { id = -1; }
                nReceiveDetailID = id;
                if (decimal.TryParse(CurProcessID, out id) == false) { id = -1; }
                bm.AddDetailProcessLog(nReceiveDetailID, id);


                // we now need to go through and save the Dataitems.
                bm.Update_ReceiveDetailItem_Version(nReceiveHeaderID, nReceiveDetailID);
                foreach (DictionaryEntry item in dList)
                {
                    string key = item.Key.ToString();
                    key = key.Substring(3);
                    string value = item.Value.ToString();
                    if (decimal.TryParse(key, out id) == false) { id = -1; }
                    if (id > 0)
                    {
                        decimal OptionID = id;
                        add = false;
                        ReceiveDetailItem bi = ctx.ReceiveDetailItems.FirstOrDefault(x => x.ReceiveDetailID == nReceiveDetailID && x.OptionID == OptionID);
                        if (bi == null)
                        {
                            add = true;
                            bi = bm.ReceiveDetailItem();
                            bi.OptionID = OptionID;
                            bi.ReceiveDate = DateTime.Now;
                            bi.ReceiveHeaderID = nReceiveHeaderID;
                            bi.ReceiveDetailID = nReceiveDetailID;
                            bi.CreateDate = DateTime.Now;
                            bi.CreateUser = CurUserName;
                            bi.Version = 0;
                        }
                        bi.LastUpdateDate = DateTime.Now;
                        bi.LastUpdateUser = CurUserName;

                        bi.Value = value;
                        bm.InsertReceiveDetailItem(ctx, bi, add);
                    }
                }

                rString = new JsonString("Result", "Saved");
                rString.AddValuePair("ReceiveHeaderID", nReceiveHeaderID.ToString());
                rString.AddValuePair("ReceiveDetailBulkID", nReceiveDetailBulkID.ToString());
                rString.AddValuePair("ReceiveDetailID", nReceiveDetailID.ToString());
                rString.AddValuePair("QTY", QTY);
            }
            //foreach (DictionaryEntry item in dList)
            //{
            //    string key = item.Key.ToString();
            //    string value = item.Value.ToString();
            //}
            return rString.ToString();
        }

        [OperationContract]
        public string GetProcessQuestionIDValue(string CurUserName, string sProcessID)
        {
            string xString = "";
            decimal id = -1;
            decimal.TryParse(sProcessID, out id);
            ProcessManager pm = new ProcessManager(CurUserName);
            List<PairIDValue> ProcessList = pm.GetProcessQuestionPairIDValue(id);   // get the target list of processes
            foreach (PairIDValue pid in ProcessList)
            {
                if (xString.Length > 0) { xString += ","; }
                xString += pid.ID.ToString();
            }
            return xString;
        }

        private static void SetBulkItemDetail(decimal nReceiveHeaderID, decimal id, DateTime dt, Hashtable dtList, ReceiveBulkManager rbm, ReceiveDetailManager rdm, decimal ReceiveDetailBulkTargetID)
        {
            // Update the bulk Detail record with any Data
            foreach (DictionaryEntry item in dtList)
            {
                string key = item.Key.ToString();
                key = key.Substring(3);
                string value = item.Value.ToString();
                List<decimal> quid = new List<decimal> { };
                ReceiveDetailItemBulk bi = rbm.ReceiveBulkDetailItem();
                if (decimal.TryParse(key, out id) == false) { id = -1; }
                if (id > 0)
                {
                    Option op = rdm.GetOptionRecord(id);
                    if (op != null)
                    {
                        if (quid.IndexOf(op.QuestionID) < 0)
                        {
                            quid.Add(op.QuestionID);
                            rbm.Update_ReceiveDetailBulkItemOption_Version(ReceiveDetailBulkTargetID, id);
                        }
                    }
                    bi.OptionID = id;
                    bi.Value = value;
                    bi.ReceiveDate = dt;
                    bi.ReceiveHeaderID = nReceiveHeaderID;
                    bi.ReceiveDetailBulkID = ReceiveDetailBulkTargetID;
                    rbm.InsertReceiveDetailBulkItem(bi);
                }
            }
            return;
        }

        private static void MoveSourceDetailToTargetDetail(int qty, decimal nReceiveHeaderID, ref decimal id, DateTime dt, ref decimal?[] rValue, Hashtable dtList, decimal CurrentProcessID, ReceiveDetailManager rdm, decimal ReceiveDetailBulkSourceID, decimal ReceiveDetailBulkTargetID)
        {
            bool add = false;
            List<ReceiveDetail> detailList = rdm.GetBulkReceiveDetail_All_BlankESN(ReceiveDetailBulkSourceID);
            clsLinqDataContext ctx = rdm.GetDataContext();
            foreach (ReceiveDetail rdetail in detailList.Take(qty))
            {
                rdetail.ReceiveDetailBulkID = ReceiveDetailBulkTargetID;
                rValue = rdm.UpdateReceiveDetail(rdetail, CurrentProcessID);
                rdm.AddDetailProcessLog(ctx, rdetail.ReceiveDetailID, CurrentProcessID);
                List<decimal> quid = new List<decimal> { };
                foreach (DictionaryEntry item in dtList)
                {
                    string key = item.Key.ToString();
                    key = key.Substring(3);
                    string value = item.Value.ToString();
                    //ReceiveDetailItem bi = rdm.ReceiveDetailItem();

                    if (decimal.TryParse(key, out id) == false) { id = -1; }
                    if (id > 0)
                    {
                        Option op = rdm.GetOptionRecord(id);
                        if (op != null)
                        {
                            if (quid.IndexOf(op.QuestionID) < 0)
                            {
                                quid.Add(op.QuestionID);
                                // lastQuestionID = op.QuestionID;
                                //rdm.Update_ReceiveDetailItemOption_Version(ctx, rdetail.ReceiveDetailID, id);
                            }
                        }
                        add = false;
                        decimal OptionID = id;
                        ReceiveDetailItem bi = ctx.ReceiveDetailItems.FirstOrDefault(x => x.ReceiveDetailID == rdetail.ReceiveDetailID && x.OptionID == OptionID);
                        if (bi == null)
                        {
                            add = true;
                            bi = rdm.ReceiveDetailItem();
                            bi.OptionID = id;
                            bi.ReceiveDate = dt;
                            bi.ReceiveHeaderID = nReceiveHeaderID;
                            bi.ReceiveDetailID = rdetail.ReceiveDetailID;
                            bi.CreateDate = DateTime.Now;
                            bi.CreateUser = rdm.UserName;
                            bi.Version = 0;
                        }
                        bi.LastUpdateDate = DateTime.Now;
                        bi.LastUpdateUser = rdm.UserName;

                        //bi.OptionID = id;
                        bi.Value = value;
                        //bi.ReceiveDate = dt;
                        //bi.ReceiveHeaderID = nReceiveHeaderID;
                        //bi.ReceiveDetailID = rdetail.ReceiveDetailID;
                        rdm.InsertReceiveDetailItem(ctx, bi, add);
                    }
                }
            }
        }


        #region Old Save
        //[OperationContract]
        //public string AddDataDetail(string Data)
        //{
        //    Log.LogIt("(SAVE BUTTON) - AddDataDetail - Started(" + "" + ") ******************");
        //    DateTime starttime = DateTime.Now;
        //    DateTime endtime = DateTime.Now;
        //    TimeSpan diffResult = endtime.Subtract(starttime);


        //    // We need to get things ready to be received as json when going back to the page.
        //    JsonString rString = new JsonString("Result", "NotSaved");
        //    rString.AddValuePair("ReceiveHeaderID", "-1");
        //    rString.AddValuePair("ReceiveDetailBulkID", "-1");
        //    rString.AddValuePair("ReceiveDetailID", "-1");
        //    rString.AddValuePair("SUFD", "");
        //    rString.AddValuePair("QTY", "0");
        //    rString.AddValuePair("CompProcList", "");
        //    rString.AddValuePair("MMS", "");
        //    rString.AddValuePair("PCLB", "");
        //    int qty = 0;
        //    decimal id = -1;

        //    decimal?[] rValue = { -1, -1 };
        //    JsonString dl = new JsonString(Data, true);
        //    Hashtable dList = dl.ListData();      // ParseData(Data);

        //    string AuthorizationRequired = "N";

        //    string ClientLocationID = dList["ClientLocationID"].ToString();
        //    decimal dClientLocationID = -1;
        //    string CurProcessID = dList["CurProcessID"].ToString();
        //    string NextProcessID = dList["NextProcessID"].ToString();
        //    string NextStepID = dList["NextStepID"].ToString();
        //    string ReceiveHeaderID = dList["ReceiveHeaderID"].ToString();
        //    string ReceiveDetailBulkID = dList["ReceiveDetailBulkID"].ToString();
        //    string ReceiveDetailID = dList["ReceiveDetailID"].ToString();
        //    string sReceiveDate = dList["ReceiveDate"].ToString();
        //    string ProjectSetup = dList["PROJSet"].ToString();
        //    //string CurUnitItemID = dList["CurUnitItemID"].ToString();
        //    string CurProcess = dList["CurProcess"].ToString();
        //    string NextProcess = dList["NextProcess"].ToString();
        //    string NextStep = dList["NextStep"].ToString();
        //    string CurUserName = dList["CurUserName"].ToString();
        //    string CurStepUp = dList["CurStepUp"].ToString();
        //    string QTY = dList["QTY"].ToString();
        //    string RMA = dList["RMA"].ToString();
        //    string ESN = dList["ESN"].ToString();
        //    string Project = dList["Project"].ToString();
        //    string sProjectID = dList["ProjectID"].ToString();
        //    string ProjectTag = dList["PROJTAG"].ToString();
        //    string AllowDupAdd = dList["hdnAllowDupAdd"].ToString();

        //    string sReceiveDetailAuthorizationLog = dList["DoAuthorize"].ToString();

        //    decimal ProjectID = -1;
        //    decimal.TryParse(sProjectID, out ProjectID);

        //    //if (dList["hdnCalledFrom"].ToString().Length > 0)
        //    //{
        //    //    //clsLog log;
        //    //    //log = new clsLog(HttpContext.Current.Server.MapPath("~"),"AddDataDetailLog",  CurUserName);
        //    //    ////log.writeLogData = true;
        //    //    //log.LogIt(dList["hdnCalledFrom"].ToString());
        //    //}



        //    //SystemLogManager slm = new SystemLogManager(1, "Save", CurUserName);
        //    Log.LogIt(Data);
        //    Log.LogIt("Transfer Date parsed(" + ProjectSetup + ":" + CurProcess + ":" + ESN + ")");


        //    if (ESN.Length == 0)
        //    {
        //        rString = new JsonString("Result", "NotSaved");
        //        rString.AddValuePair("Error", "No ESN given");
        //        return rString.ToString();
        //    }

        //    if (CurProcess.ToUpper() != "BULKRECEIVE" && CurProcess.ToUpper() != "RECEIVEFROMBULK" && CurProcess.ToUpper() != "BULKMOVE")
        //    {
        //        try
        //        {


        //            ReceiveDetailManager rdm = new ReceiveDetailManager(CurUserName);
        //            using (clsLinqDataContext ctx = rdm.GetDataContext(CurUserName))
        //            {
        //                ReceiveDetail rec = null;


        //                if (decimal.TryParse(ReceiveHeaderID, out id) == false) { id = -1; }
        //                decimal nReceiveHeaderID = id;
        //                if (decimal.TryParse(ReceiveDetailBulkID, out id) == false) { id = -1; }
        //                decimal nReceiveDetailBulkID = id;
        //                if (decimal.TryParse(ReceiveDetailID, out id) == false) { id = -1; }
        //                decimal nReceiveDetailID = id;
        //                if (decimal.TryParse(CurProcessID, out id) == false) { id = -1; }
        //                decimal CurrentProcessID = id;
        //                if (decimal.TryParse(ClientLocationID, out id) == false) { id = -1; }
        //                dClientLocationID = id;


        //                if (decimal.TryParse(sReceiveDetailAuthorizationLog, out id) == false) { id = -1; }
        //                decimal ReceiveDetailAuthorizationLog = id;



        //                ////if (CurProcess.ToUpper() == "RECEIVEDOA")
        //                if (RMA.Length == 0 && ProjectSetup.ToUpper().Contains("ZRMAZZAUTORZ"))
        //                {
        //                    RMA = rdm.NextRMANumber(ctx, dClientLocationID, CurrentProcessID);
        //                }
        //                //if (CurProcess.ToUpper() == "RECEIVEINWARRANTY" || CurProcess.ToUpper() == "RECEIVEEXWARRANTY" || CurProcess.ToUpper() == "RECEIVEOOWARRANTY")
        //                if (RMA.Length == 0 && ProjectSetup.ToUpper().Contains("ZRMAZZAUTOWZ"))
        //                {
        //                    RMA = rdm.NextWorkOrderNumber(ctx);
        //                }

        //                Log.LogIt("RMA auto set if needed Done");


        //                if (rdm.isDetailThere(ctx, ESN) == false && rdm.isOKToAddFromThisProcess(CurProcess) == false)
        //                {
        //                    rString = new JsonString("Result", "NotSaved");
        //                    rString.AddValuePair("Error", "Unable to add from this process:" + CurProcess);
        //                    return rString.ToString();
        //                }
        //                rec = rdm.ReceiveDetail_NewOrThisESN(ctx, ESN);
        //                if (rec == null) { return rString.ToString(); }
        //                if (decimal.TryParse(NextStepID, out id) == false) { id = -1; }
        //                rec.ProcessID = id;

        //                Log.LogIt("Loaded Existing ESN(" + ESN + ") or created a new one Done");


        //                //dt = DateTime.ParseExact(sReceiveDate, "MM/dd/yyyy hh:mm tt",System.Globalization.DateTimeFormatInfo.CurrentInfo);
        //                //if (DateTime.TryParse(sReceiveDate, out dt) == false) { dt = DateTime.Now; }
        //                ////////////////////////////////////////////////////////////////////
        //                if (int.TryParse(QTY, out qty) == false) { qty = 0; }
        //                rec.QTYIntegrated = qty;
        //                rec.MiscNote = "";

        //                if (rec.ReceiveDetailID > 0)
        //                {
        //                    if (decimal.TryParse(ClientLocationID, out id) == false) { id = -1; }
        //                    if (rec.ClientLocationID < 1 && id > 0)
        //                    {
        //                        rec.ClientLocationID = id;
        //                        rdm.UpdateReceiveDetailClientLocation(ctx, rec.ReceiveDetailID, rec.ReceiveHeaderID, id);
        //                    }
        //                    rValue = rdm.UpdateReceiveDetail(ctx, rec, CurrentProcessID);
        //                    Log.LogIt("Update Receive Detail");
        //                    //slm.Catagory = "Update";
        //                }
        //                if (rec.ReceiveDetailID < 1)
        //                {
        //                    //slm.Catagory = "Insert";
        //                    rec.ClientLocationID = dClientLocationID;
        //                    rec.ReceiveHeaderID = nReceiveHeaderID;
        //                    rec.RMANumber = CleanData(RMA);
        //                    rec.ESN = CleanData(ESN);
        //                    rec.ProjectTag = CleanData(ProjectTag);
        //                    rec.ICB = "";
        //                    rec.PIN = "";
        //                    rec.ProjectName = CleanData(Project);
        //                    rec.ProjectID = ProjectID;

        //                    rec.ReceiveDate = DateTime.Now;
        //                    rValue = rdm.InsertReceiveDetail(ctx, rec, CurrentProcessID);
        //                    Log.LogIt("Insert ReceiveDetail");
        //                }
        //                // up till now, we may not know what these two ID fields are. 
        //                if (decimal.TryParse(rValue[0].ToString(), out id) == false) { id = -1; }
        //                nReceiveHeaderID = id;
        //                if (decimal.TryParse(rValue[1].ToString(), out id) == false) { id = -1; }
        //                nReceiveDetailID = id;

        //                rdm.AddDetailProcessLog(ctx, nReceiveDetailID, CurrentProcessID);
        //                Log.LogIt("Add Detail Process Log");
        //                // Send out Billing Record
        //                rdm.AddBillingPoint(ctx, nReceiveDetailID, dClientLocationID, ProjectID, CurrentProcessID);
        //                Log.LogIt("Add Billing Point");

        //                //slm.Log(ctx, "Starting to Save Detail Items", 3);
        //                RecordDetailItemData(ctx, dList, CurUserName, nReceiveHeaderID, nReceiveDetailID, CurrentProcessID, false, false, ProjectID, dClientLocationID);
        //                //slm.Log(ctx, "Detail Items Saved", 2);
        //                Log.LogIt("Add/Save Detail Item Data");
        //                // if process = "receive" then we need to look to our prereceive inventory.
        //                if (CurProcess.Length > 6 && CurProcess.Substring(0, 7).ToUpper() == "RECEIVE")
        //                {
        //                    rdm.OffestPreReceive(ctx, nReceiveDetailID, ESN);
        //                    Log.LogIt("Pre Receive Offset Done");
        //                }
        //                #region KITTING
        //                if (CurProcess.Substring(0, 7).ToUpper() == "KITTING")  // 
        //                {
        //                    Log.LogIt("Kitting Process Started");
        //                    rdm.SM.Kitted(ctx, rec.ReceiveDetailID, -1, rec.ClientLocationID, (decimal)rec.ProjectID, CurrentProcessID, CurUserName);
        //                    // Do Not Update in accordance to Jody's Instruction March 23, 2012 (phone call 10:31/Email)
        //                    //rdm.UpdateESNAttribute_Blank(ctx, nReceiveDetailID, "Bin");
        //                    //rdm.UpdateESNAttribute_Blank(ctx, nReceiveDetailID, "Location");
        //                    ////////////////////////////////////////////////////////////////////////////////////////////
        //                    bool isSwapped = rdm.SwapIMEI(ctx, nReceiveDetailID);
        //                    Log.LogIt("Kitting Process Done");
        //                }
        //                #endregion
        //                #region SHIPPING
        //                if (CurProcess.Length > 7 && CurProcess.Substring(0, 8).ToUpper() == "SHIPPING")  // This unit has been shipped, we want the version changed from 000.  000 denotes GMP possesion, !000 denotes out the door.
        //                {
        //                    Log.LogIt("Shipping Process Started" + ESN + " " + CurUserName);

        //                    // As Per Jody's Instruction March 23, 2012 (phone call 10:31/Email)
        //                    rdm.UpdateESNAttribute_Blank(ctx, nReceiveDetailID, "Bin");
        //                    rdm.UpdateESNAttribute(ctx, nReceiveDetailID, "Location", "None");
        //                    ////////////////////////////////////////////////////////////////////////////////////////////
        //                    // As Per Jody's Instruction June 1, 2012 (Add SwapIMEI process to Shipping)
        //                    Log.LogIt("Bin and Location Set." + ESN + " " + CurUserName);
        //                    ////////////////////////////////////////////////////////////////////////////////////////////
        //                    Log.LogIt("rdm.SM.OrderShipped" + ESN + " " + CurUserName);
        //                    rdm.SM.OrderShipped(ctx, rec.ReceiveDetailID, -1, rec.ClientLocationID, (decimal)rec.ProjectID, CurrentProcessID, CurUserName);
        //                    Log.LogIt("Starting AdvanceESNVersion:" + ESN + " " + CurUserName);
        //                    AdvanceESNVersion(ctx, ESN, CurUserName);
        //                    Log.LogIt("AdvanceESNVersion Done:" + ESN + " " + CurUserName);
        //                    /////////////////////////////////////////////////////////////////////
        //                    // This needs to happen after the AdvanceESN because if the swap is done first
        //                    //      The Advance does not find it.
        //                    try
        //                    {
        //                        bool isSwapped = rdm.SwapIMEI(ctx, nReceiveDetailID);
        //                        Log.LogIt("SwapIMEI done:" + ESN + " " + CurUserName);
        //                        if (isSwapped == true) { Log.LogIt("IMEI Swapped" + ESN + " " + CurUserName); }
        //                        else { Log.LogIt("IMEI NOT Swapped" + ESN + " " + CurUserName); }
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        Log.LogIt("IMEI Swap Error:" + ex.Message);
        //                    }



        //                    /////////////////////////////////////////////////////////////////////////////////////////
        //                    // Bring Back From MSC if it is a MSC Unit
        //                    rdm.CloneFromMSC(ctx, rec.ReceiveDetailID, (decimal)rec.ProjectID, CurrentProcessID);
        //                    /////////////////////////////////////////////////////////////////////////////////////////
        //                    Log.LogIt("Shipping process Done" + ESN + " " + CurUserName);
        //                }
        //                #endregion
        //                #region GMPRepair
        //                if (CurProcess.Length > 9 && CurProcess.Substring(0, 10).ToUpper() == "GMP REPAIR")
        //                {
        //                    Log.LogIt("GMP Repair Started");
        //                    // Look to see if Authorization is required.
        //                    string IsAuthorizationRequired = rdm.GetReceiveDetailItem_DataElement(ctx, nReceiveDetailID, "Authorization");

        //                    ////Log.LogIt("IsAuthorizationRequired = |" + IsAuthorizationRequired + "|");
        //                    //Log.LogIt("IsAuthorizationRequired = |" + (IsAuthorizationRequired.Trim().ToUpper() == "APPROVAL REQUIRED").ToString() + "|");
        //                    //string LookFor = "APPROVAL REQUIRED";
        //                    //Log.LogIt(IsAuthorizationRequired.Length.ToString());
        //                    //Log.LogIt(LookFor.Length.ToString());
        //                    // LookFor = IsAuthorizationRequired;
        //                    if (IsAuthorizationRequired.ToUpper() == "APPROVAL REQUIRED")
        //                    {

        //                        Log.LogIt("Inside Approval Required");
        //                        AuthorizationRequired = "Y";
        //                        Log.LogIt("AuthorizationRequired:" + AuthorizationRequired);
        //                        rdm.UpdateESNAttribute_Blank(ctx, nReceiveDetailID, "Authorization");

        //                        ReceiveDetailAuthohrizationManager rdam = new ReceiveDetailAuthohrizationManager(CurUserName);
        //                        decimal ef = 0;
        //                        decimal ff = 0;
        //                        decimal hst = 0;
        //                        decimal total = 0;
        //                        string Note = "";
        //                        string Note1 = CleanData(rdm.GetReceiveDetailItem_DataElement(ctx, nReceiveDetailID, "Claim Action"));
        //                        string Note2 = CleanData(rdm.GetReceiveDetailItem_DataElement(ctx, nReceiveDetailID, "Claim Reason"));
        //                        string Note3 = CleanData(rdm.GetReceiveDetailItem_DataElement(ctx, nReceiveDetailID, "Claim Location"));

        //                        if (Note1.Length > 0) { Note = Note1 + "\n"; }
        //                        if (Note2.Length > 0) { Note += Note2 + "\n"; }
        //                        if (Note3.Length > 0) { Note += Note3; }

        //                        Log.LogIt("Middle of Approval Required");

        //                        //string sef = CleanData(rdm.GetReceiveDetailItem_DataElement(ctx, nReceiveDetailID, "Est"));
        //                        string sef = CleanData(rdm.GetReceiveDetailItem_DataElement(ctx, nReceiveDetailID, "Estimate"));
        //                        string sff = CleanData(rdm.GetReceiveDetailItem_DataElement(ctx, nReceiveDetailID, "Freight"));
        //                        string shst = CleanData(rdm.GetReceiveDetailItem_DataElement(ctx, nReceiveDetailID, "billing_hst"));
        //                        string stotal = CleanData(rdm.GetReceiveDetailItem_DataElement(ctx, nReceiveDetailID, "billing_total"));

        //                        if (decimal.TryParse(sef, out ef) == false) { ef = 0; }
        //                        if (decimal.TryParse(sff, out ff) == false) { ff = 0; }
        //                        if (decimal.TryParse(shst, out hst) == false) { hst = 0; }
        //                        if (decimal.TryParse(stotal, out total) == false) { total = 0; }

        //                        rdam.AddNewRequest(ctx, nReceiveDetailID, ef, ff, hst, total, Note, "AUT");

        //                        Log.LogIt("Leaving Approval Required");

        //                    }
        //                    Log.LogIt("GMP Repair Done");
        //                }

        //                if (ReceiveDetailAuthorizationLog > 0)         // we are saving on a unit that had an authorization scanned in.
        //                {
        //                    ReceiveDetailAuthohrizationManager rdam = new ReceiveDetailAuthohrizationManager(CurUserName);
        //                    rdam.Complete(ctx, ReceiveDetailAuthorizationLog, CurUserName);
        //                    Log.LogIt("Authorization Scan set to Complete");
        //                }

        //                #endregion

        //                Log.LogIt("Return Message Build Started");

        //                rString = new JsonString("Result", "Saved");
        //                rString.AddValuePair("ReceiveHeaderID", nReceiveHeaderID.ToString());
        //                rString.AddValuePair("ReceiveDetailBulkID", nReceiveDetailBulkID.ToString());
        //                rString.AddValuePair("ReceiveDetailID", nReceiveDetailID.ToString());
        //                rString.AddValuePair("SUFD", rdm.GetSetUpFieldDef(ctx, ProjectID));
        //                rString.AddValuePair("QTY", QTY);
        //                rString.AddValuePair("CompProcList", rdm.GetRequestProcessCompletionList(ctx, nReceiveDetailID));

        //                string MakeModelString = rdm.MakeModelColourNickName(ctx, nReceiveDetailID);
        //                rString.AddValuePair("MMS", MakeModelString);
        //                rString.AddValuePair("THREADED", "N");
        //                rString.AddValuePair("AR", AuthorizationRequired);
        //                Log.LogIt("b - AuthorizationRequired:" + AuthorizationRequired);

        //                MakeModelString = rdm.GetProjectClientLocationBinString(ctx, nReceiveDetailID);
        //                rString.AddValuePair("PCLB", MakeModelString);                           //lblProjectClientLocationBinTitle
        //                Log.LogIt("Return Message Build Done");
        //                //slm.Log(ctx, CleanData(Project) + ":" + CurProcess + ":" + CleanData(ESN), 1);

        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Log.LogIt("Add Data Detail Error:" + ex.Message);
        //            rString = new JsonString("Result", "NotSaved");
        //            rString.AddValuePair("Error", CleanData(ex.Message));
        //            return rString.ToString();
        //        }

        //    }

        //    endtime = DateTime.Now;
        //    diffResult = endtime.Subtract(starttime);
        //    Log.LogIt("Save Finished - Elapsed Time:(HH:" + diffResult.Hours.ToString() + " MM:" + diffResult.Minutes.ToString() + " SS:" + diffResult.Seconds.ToString() + " MS:" + diffResult.Milliseconds.ToString() + ") ******************");

        //    //Log.writeLogData = false;
        //    return rString.ToString();
        //}



        private void RecordDetailItemData(clsLinqDataContext ctx, Hashtable dList, string CurUserName, decimal nReceiveHeaderID, decimal nReceiveDetailID, decimal CurrentProcessID, bool isXBinXUpdate, bool DoCalc, decimal ProjectID, decimal ClientLocationID)
        {
            Hashtable CalcList = new Hashtable();
            Hashtable MasterCarrierList = new Hashtable();
            decimal OptionID = 0;
            decimal PartNumberID = -1;
            ReceiveDetailManager bm = new ReceiveDetailManager(CurUserName);
            MasterPartManager mpm = new MasterPartManager(CurUserName);

            List<decimal> quid = new List<decimal> { };
            var Keys = dList.Keys.Cast<String>().OrderBy(x => x).Where(x => decimal.TryParse(x.Substring(3), out OptionID) == false);
            foreach (string key in Keys) { dList.Remove(key); }
            DateTime Start = DateTime.Now;
            //string AttributeString = "";
            Log.LogIt("Starting Loop to save each Attribute Item");
            foreach (DictionaryEntry item in dList)
            {
                bool add = false;
                string type = item.Key.ToString().Substring(0, 2);
                string key = item.Key.ToString();
                key = key.Substring(3);
                string value = item.Value.ToString();
                if (isXBinXUpdate == true && value.Length == 0) { continue; }
                if (decimal.TryParse(key, out OptionID) == false) { OptionID = -1; }
                if (OptionID > 0)
                {
                    Option op = bm.GetOptionRecord(ctx, OptionID);
                    string QuestionType = bm.QuestionType(ctx, OptionID);
                    string QuestionName = bm.QuestionName(ctx, OptionID);
                    decimal QuestionID = -1;
                    if (op != null)
                    {
                        QuestionID = op.QuestionID;
                        if (QuestionName == "Carrier") { MasterCarrierList.Add("Carrier", op.OptionID); }
                        if (QuestionName == "Manufacturer") { MasterCarrierList.Add("Manufacturer", op.OptionID); }
                        if (QuestionName == "Model") { MasterCarrierList.Add("Model", op.OptionID); }
                        if (QuestionName == "Colour") { MasterCarrierList.Add("Colour", op.OptionID); }



                        if (QuestionName.Length > 5 && QuestionName.Substring(0, 5).ToUpper() == "PART ")
                        {
                            Log.LogIt("Starting Partnumber removal from inventory");
                            decimal ReceiveDetailItemID = -1;
                            ReceiveDetail rd = ctx.ReceiveDetails.FirstOrDefault(x => x.ReceiveDetailID == nReceiveDetailID);
                            if (rd != null)
                            {
                                ReceiveDetailItem bi = ctx.ReceiveDetailItems.FirstOrDefault(x => x.ReceiveDetailID == nReceiveDetailID && x.OptionID == OptionID);
                                if (bi == null) { ReceiveDetailItemID = bi.ReceiveDetailItemID; }

                                // The ID can be identified in two ways. If it comes in via the "wrench", it is coded ":id="
                                if (value.IndexOf(":id=") > -1)
                                {
                                    string x = value.Substring(value.IndexOf(":id=") + 4);
                                    if (decimal.TryParse(x, out PartNumberID) == true)
                                    {
                                        value = value.Substring(0, value.IndexOf(":id="));
                                        mpm.RemoveFromInventory(ctx, PartNumberID, rd.IFSLocation, -1, mpm.GetType("Used by tech"), "", nReceiveDetailID, ReceiveDetailItemID);
                                    }
                                }
                                // The ID can be identified in two ways. If it comes in via the "wrench", it is coded "xidX"    (It is case sensitive)
                                if (value.IndexOf("xidX") > -1)
                                {
                                    string x = value.Substring(value.IndexOf("xidX") + 4);
                                    if (decimal.TryParse(x, out PartNumberID) == true)
                                    {
                                        value = value.Substring(0, value.IndexOf("xidX"));
                                        mpm.RemoveFromInventory(ctx, PartNumberID, rd.IFSLocation, -1, mpm.GetType("Used by tech"), "", nReceiveDetailID, ReceiveDetailItemID);
                                    }
                                }
                            }

                            Log.LogIt("Done Partnumber removal from inventory ");
                        }



                        if (QuestionType.ToUpper() == "CALC" && DoCalc == false) { CalcList.Add(item.Key, item.Value); Log.LogIt("Calc Field found" + item.Value); }
                        else
                        {
                            if (quid.IndexOf(op.QuestionID) < 0) { quid.Add(op.QuestionID); bm.Update_ReceiveDetailItemOption_Version(ctx, nReceiveDetailID, OptionID); }

                            add = false;
                            ReceiveDetailItem bi = ctx.ReceiveDetailItems.FirstOrDefault(x => x.ReceiveDetailID == nReceiveDetailID && x.OptionID == OptionID);
                            if (bi == null)
                            {
                                add = true;
                                bi = bm.ReceiveDetailItem();
                                bi.OptionID = OptionID;
                                bi.ReceiveDate = DateTime.Now;
                                bi.ReceiveHeaderID = nReceiveHeaderID;
                                bi.ReceiveDetailID = nReceiveDetailID;
                                bi.CreateDate = DateTime.Now;
                                bi.CreateUser = CurUserName;
                                bi.Version = 0;
                            }
                            bi.LastUpdateDate = DateTime.Now;
                            bi.LastUpdateUser = CurUserName;

                            if (DoCalc == true)
                            {
                                GMPCalculator calculator = new GMPCalculator(nReceiveDetailID, CurUserName);
                                bi.Value = "";
                                if (op.OptionText.Length > 0)
                                {
                                    try { bi.Value = CleanData(calculator.Calculate(op.OptionText).ToString()); }
                                    catch (Exception ex) { bi.Value = "CALC ERROR"; }
                                }
                            }
                            else { bi.Value = CleanData(value); }
                            if (type == "CB" && value == "0") { }
                            else
                            {
                                // TODO Item Version Stop version bumping.
                                Log.LogIt("Beginning to save Detail Item:" + item.Key);
                                bm.InsertReceiveDetailItem(ctx, bi, add);
                                Log.LogIt("Saved Inventory Detail Line Item");
                                //bm.AddBucketCount(nReceiveDetailID, ClientLocationID, ProjectID, CurrentProcessID, QuestionID, OptionID);
                            }

                        }
                    }
                }
            }
            Log.LogIt("Done Loop to save each Attribute Item");

            if (DoCalc == false && MasterCarrierList.Count == 4)
            {
                Log.LogIt("Update ReceiveDetail Carrier Keys: Started");
                bm.UpdaeReceiveDetailCarrierKeys(ctx, nReceiveDetailID, MasterCarrierList);
                Log.LogIt("Update ReceiveDetail Carrier Keys: Done");
            }

            // We now want to calculate any "Calc attributes."
            if (DoCalc == false && CalcList.Count > 0)
            {
                Log.LogIt("RecordDetailItemData: Started");
                RecordDetailItemData(ctx, CalcList, CurUserName, nReceiveHeaderID, nReceiveDetailID, CurrentProcessID, isXBinXUpdate, true, ProjectID, ClientLocationID);
                Log.LogIt("RecordDetailItemData: Done");
            }
            DateTime EndDate = DateTime.Now;
            return;
        }
        #endregion


    }
}


