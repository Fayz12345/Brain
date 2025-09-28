using System;
using System.Collections.Generic;
using System.Collections;
//using System.Web.Script.Serialization;            // Used to generate json for back and forth
using System.IO;
using System.Web;

using System.Xml;
using System.Xml.Serialization;

using System.Web.UI;
using System.Threading;

using System.Data.Linq;
using System.Linq;
using System.Runtime.Serialization;
using System.Web.Services;
using System.Web.Script.Services;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Security;
using System.Web.Configuration;
//using Factory_DataModel;
using BW_WebApp.DataManagers;
//using System.Web.Mvc;
// using DAL;

using BW_WebApp.Classes;

namespace BW_WebApp
{
    /// <summary>
    /// These are endpoints that are being used by Fayzeen.
    /// </summary>
    public partial class WebServer_01
    {
        #region Fayzeen_RequestedAccessPoints
        #region Device Upload

        //[WebInvoke(Method = "POST",
        //  BodyStyle = WebMessageBodyStyle.WrappedRequest,
        //  RequestFormat = WebMessageFormat.Json,
        //  ResponseFormat = WebMessageFormat.Json,
        //  UriTemplate = "BulkDeviceUploadTest01")]

        //[WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //[OperationContract]
        //public string BulkDeviceUploadTest01(string JSONDataString)
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
        //        ThreadPool.QueueUserWorkItem(Report => RunDeviceUploadBatch(re.logID, LogPath));
        //        // Spawn out a new 
        //        return rValue;
        //    }
        //}


        [OperationContract]
        public string BulkDeviceUpload(string JSONDataString)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                string LogPath = HttpContext.Current.Server.MapPath("~");
                API_UploadDeviceBatch_In x1 = API_UploadDeviceBatch_In.FromJSON(JSONDataString);
                API_UploadDeviceBatch_OutB x = new API_UploadDeviceBatch_OutB(x1, LogPath);
                x.WriteLogStart(JSONDataString);
                x.WriteInsert();
                string rValue = x.ProcessResultFirstSend(x.logID);
                //////// x.WriteProcessLogFirstReply(rValue);
                ////////return LogPath + x.logID.ToString();
                ////////LogPath = @"C:\inetpub\dev\bridge";
                //////RunDeviceUploadBatch(x.logID, LogPath);
                //////rValue = ProcessResultsLogID(x.logID.ToString());
                ThreadPool.QueueUserWorkItem(Report => RunDeviceUploadBatch(x.logID, LogPath));
                // Spawn out a new 
                return rValue;
                //return x.JSON();
            }
        }
        //public string BulkDeviceUploadB(string JSONDataString)
        //{
        //    using (clsLinqDataContext ctx = new clsLinqDataContext())
        //    {
        //        string LogPath = HttpContext.Current.Server.MapPath("~");
        //        API_UploadDeviceBatch_In x1 = API_UploadDeviceBatch_In.FromJSON(JSONDataString);
        //        API_UploadDeviceBatch_OutB x = new API_UploadDeviceBatch_OutB(x1, LogPath);
        //        x.WriteLogStart(JSONDataString);
        //        x.WriteInsert();
        //        string rValue = x.ProcessResultFirstSend(x.logID);
        //        // x.WriteProcessLogFirstReply(rValue);
        //        ThreadPool.QueueUserWorkItem(Report => RunDeviceUploadBatch(x.logID, LogPath));
        //        // Spawn out a new 
        //        return rValue;
        //        //return x.JSON();
        //    }
        //}
        private void RunDeviceUploadBatch(decimal ID, string LogPath)
        {
            API_UploadDeviceBatch_OutB re = new API_UploadDeviceBatch_OutB(LogPath);
            re.ProcessBatch(ID, LogPath);
        }
        #endregion

        #region Device Attribute Update
        [OperationContract]
        public string BulkDeviceAttributeUpdate(string JSONDataString)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                API_UpdateDeviceAttribute_In x1 = API_UpdateDeviceAttribute_In.FromJSON(JSONDataString);
                API_UpdateDeviceAttribute_Out x = new API_UpdateDeviceAttribute_Out(x1);
                x.WriteLogStart(JSONDataString);
                x.WriteInsert();
                string rValue = x.ProcessResult(x.logID);
                string LogPath = HttpContext.Current.Server.MapPath("~");
                ThreadPool.QueueUserWorkItem(Report => RunDeviceAttributeUpdateBatch(x.logID, LogPath));
                // Spawn out a new 
                return rValue;
            }
        }
        private void RunDeviceAttributeUpdateBatch(decimal ID, string LogPath)
        {
            API_UpdateDeviceAttribute_Out re = new API_UpdateDeviceAttribute_Out();
            re.ProcessBatch(ID, LogPath);
        }
        #endregion

        #region Get Batch Results.
        [OperationContract]
        public string ProcessResultsBatch(string Batch)
        {
            string LogPath = HttpContext.Current.Server.MapPath("~");
            API_UploadDeviceBatch_OutB re = new API_UploadDeviceBatch_OutB(LogPath);
            return re.ProcessResult(Batch);
        }
        [OperationContract]
        public string ProcessResultsLogID(string LogID)
        {
            string LogPath = HttpContext.Current.Server.MapPath("~");
            decimal ID = -1;
            decimal.TryParse(LogID, out ID);
            API_UploadDeviceBatch_OutB re = new API_UploadDeviceBatch_OutB(LogPath);
            return re.ProcessResult(ID);
        }
        #endregion

        [OperationContract]
        public string GETUnitData(string ESN, string Version, string UserName, string DataRequested)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                return ctx.GETUnitData(ESN, Version, UserName, DataRequested);
            }
        }
        //[OperationContract]
        //public string RecordBrowserTimetoLog(string sLogID, string Time_ms, string UserName)
        //{
        //    string rValue = "Error:(" + sLogID + ") No action taken!";
        //    decimal LogID = -1;
        //    decimal.TryParse(sLogID, out LogID);
        //    decimal MS = -1;
        //    decimal.TryParse(Time_ms, out MS);
        //    if (MS > 0 && LogID > 0)
        //    {
        //        TimeLogManager timelog = new TimeLogManager("jmccomb", "");
        //        timelog.RecordBrowserTimetoLog(LogID, MS);
        //        rValue = "Success:(" + sLogID + ") Saved!";
        //    }
        //    return rValue;
        //}
        #endregion
    }
}