using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Threading;

using System.Text.RegularExpressions;

//using Factory_DataModel;
using BW_WebApp.DataManagers;

namespace BW_WebApp.Classes
{
    public class IMEIUploadAttributeProcessor
    {
        clsLog log;
        public string BatchNumber { get; set; }
        public string username { get; set; }
        public API_UpdateDeviceAttribute_Out api_batchdata { get; set; }
        public IMEIUploadAttributeProcessor(API_UpdateDeviceAttribute_Out Batch, string LogPath)
        {
            BatchNumber = Batch.batch;
            username = Batch.username;
            api_batchdata = Batch; 
            log = new clsLog(LogPath, "Util_APIATTUpload_01_Log.txt", username, System.Configuration.ConfigurationManager.AppSettings["WriteLogUserNameToLog"]);
            if (System.Configuration.ConfigurationManager.AppSettings["WriteLog"].ToUpper() == "TRUE")
            {
                log.writeLogData = true;
            }
            //log.LogIt("**** Bulk API Upload Started");
        }
        public IMEIUploadAttributeProcessor(string LogPath)
        {
            BatchNumber = "";
            username = "";
            api_batchdata = null;
            log = new clsLog(LogPath, "Util_APIATTUpload_01_Log.txt", username, System.Configuration.ConfigurationManager.AppSettings["WriteLogUserNameToLog"]);
            if (System.Configuration.ConfigurationManager.AppSettings["WriteLog"].ToUpper() == "TRUE")
            {
                log.writeLogData = true;
            }
        }

        public API_Status ProcessData()
        {
            API_Status rValue = new API_Status();
            if (api_batchdata == null) {
                rValue.message = "No Batch to process";
                rValue.status = "Error";
                return rValue; 
            }
            log.LogIt("**** Bulk API Upload Started");


            return rValue;
        }
        public API_Status ProcessData(API_UpdateDeviceAttribute_Out Batch)
        {
            api_batchdata = Batch;
            API_Status rValue = new API_Status();
            rValue.SetSuccess();
            BatchNumber = api_batchdata.batch;
            username = api_batchdata.username;
            api_batchdata = api_batchdata;
            log.LogIt("**** Bulk API Upload Started");
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                string Message = "";
                foreach (API_Device_Out d in api_batchdata.devices)
                {
                    ReceiveDetail rd = ctx.ReceiveDetails.FirstOrDefault(x => x.ESN == d.esn && x.Version == d.version);
                    if (rd != null)
                    {
                        d.receivedetailid = rd.ReceiveDetailID;
                        foreach (API_DeviceAttribute_Out at in d.attributes)
                        {
                            Message = "";
                            ctx.UpdateESNAttribute_NoProjectRestriction_BYID_rValue(d.receivedetailid, at.attribute, at.value, api_batchdata.username, ref Message);
                            at.SetThisStatus(Message);
                        }
                        d.status = d.SummaryErrorsOnly();
                    }
                    else
                    {
                        d.SetError("Device not found");
                    }
                }
                //ReceiveDetail_APIInsertLog log = ctx.ReceiveDetail_APIInsertLogs.FirstOrDefault(x => x.ReceiveDetail_APIInsertLogID == logID);
                //if (log == null) { return; }
                //log.DateTimeout = DateTime.Now;
                //log.JSONout = JSON();
                ////ctx.ReceiveDetail_APIInsertLogs.InsertOnSubmit(log);
                //ctx.SubmitChanges();
                ////logID = log.ReceiveDetail_APIInsertLogID;
            }
            Batch.WriteProcessLogEnd();
            Batch.Save();
            return Batch.status;
        }


    }
}