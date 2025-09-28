using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using BW_WebApp.DataManagers;






namespace BW_WebApp.Classes
{
    public class Cellbie
    {
        public String Username = "";
        public Cellbie(string username)
        {
            Username = username;
        }

        public BrainDeviceTransaction SendReceiveTransaction(decimal ReceiveDetailID, string IMEI, bool OK, string Reason)
        {
            return SendReceiveTransaction(ReceiveDetailID, IMEI, OK, Reason, false);
        }
        public BrainDeviceTransaction SendReceiveTransaction(decimal ReceiveDetailID, string IMEI, bool OK, string Reason, bool UseStub)
        {
            string rMessage = "";
            ReceiveDetailManager rm = new ReceiveDetailManager(Username);
            if (IMEI.Length == 0)
            {
                IMEI = rm.getIMEI(ReceiveDetailID);
            }
            if (ReceiveDetailID < 1)
            {
                ReceiveDetailID = rm.getReceiveDetailID(IMEI);
            }
            BrainDeviceTransaction infosendReceived = null;
            //BrainDeviceTransaction infosendforinfo = null;

            if (UseStub == true)
            {
                Cellbie_Rest_Client_Stub api = new Cellbie_Rest_Client_Stub(Username);
                infosendReceived = api.ReceiveDevice(ReceiveDetailID, IMEI, OK, Reason);
            }
            else
            {
                Cellbie_Rest_Client api = new Cellbie_Rest_Client(Username);
                infosendReceived = api.ReceiveDevice(ReceiveDetailID, IMEI, OK, Reason);
            }
            using (clsLinqDataContext ctx = rm.GetDataContext(Username))
            {
                if (infosendReceived.Success == true)
                {
                    ctx.Record_CellbieTransaction(ReceiveDetailID, "Success", infosendReceived.SuccessText, infosendReceived.API, infosendReceived.CellbieParameterJSON, infosendReceived.CellbieDataJSON, infosendReceived.ToJSON(), infosendReceived.CellbieStatus.ExceptionMessage, infosendReceived.CellbieStatus.InnerExceptionMessage, infosendReceived.Message, Username, ref rMessage);
                    //ctx.Record_CellbieTransaction(ReceiveDetailID, "Success", infosendReceived.SuccessText, infosendReceived.API, infosendReceived.CellbieParameterJSON.ToSingleQuote(), infosendReceived.CellbieDataJSON.ToSingleQuote(), infosendReceived.ToJSON().ToSingleQuote(), infosendReceived.CellbieStatus.ExceptionMessage, infosendReceived.CellbieStatus.InnerExceptionMessage, infosendReceived.Message, Username, ref rMessage);
                    infosendReceived.RecordTransactionStatus.AssumeStatus(rMessage);
                    //return infosend;
                }
                else
                {
                    ctx.Record_CellbieTransaction(ReceiveDetailID, "Error", infosendReceived.SuccessText, infosendReceived.API, infosendReceived.CellbieParameterJSON, infosendReceived.CellbieDataJSON, infosendReceived.ToJSON(), infosendReceived.CellbieStatus.ExceptionMessage, infosendReceived.CellbieStatus.InnerExceptionMessage, infosendReceived.Message, Username, ref rMessage);
                    //ctx.Record_CellbieTransaction(ReceiveDetailID, "Error", infosendReceived.SuccessText, infosendReceived.API, infosendReceived.CellbieParameterJSON.ToSingleQuote(), infosendReceived.CellbieDataJSON.ToSingleQuote(), infosendReceived.ToJSON().ToSingleQuote(), infosendReceived.CellbieStatus.ExceptionMessage, infosendReceived.CellbieStatus.InnerExceptionMessage, infosendReceived.Message, Username, ref rMessage);
                    infosendReceived.RecordTransactionStatus.AssumeStatus(rMessage);
                }
            }
            return infosendReceived;
        }
    }

    public class Cellbie_Rest_Client_Stub
    {
        bool Debug = true;
        String Username = "";
        string AccessToken = "";
        string AccessTokenHidden = "";
        string AccessDomain = "";
        public Cellbie_Rest_Client_Stub(string username)
        {
            Username = username;
            AccessToken = SysUtil.CellbieAPIToken();          //"zi9zk2rvv6ag5qj64mk6";
            AccessDomain = SysUtil.CellbieAPIDomain();         //"https://bridgetest.wbapp.ca";
            AccessTokenHidden = AccessToken;
            if (Debug == false)
            {
                AccessTokenHidden = SysUtil.CellbieAPITokenHidden();
            }
        }
        public BrainDeviceTransaction Getinventoryinfo(decimal ReceiveDetailID, string IMEI)
        {
            string API = "apiv1/inventoryinfo";
            string requestURL = AccessDomain + API;
            GetDeviceInfoParameters Params = new GetDeviceInfoParameters(ReceiveDetailID, AccessTokenHidden, IMEI);
            var postData = "token=" + AccessToken;
            postData += "&IMEI=" + IMEI;
            var data = Encoding.ASCII.GetBytes(postData);

            BrainDeviceTransaction bdi = new BrainDeviceTransaction(API);
            bdi.ReceiveDetailID = ReceiveDetailID;
            #region Simulate Connection
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestURL);
            //request.Method = "POST";
            //request.ContentType = "application/x-www-form-urlencoded";
            //request.ContentLength = data.Length;
            //using (var stream = request.GetRequestStream()) { stream.Write(data, 0, data.Length); }
            //HttpWebResponse response = null;
            //bdi.CellbieParameterJSON = Params.ToJSON();
            //try
            //{
            //    response = (HttpWebResponse)request.GetResponse();
            //}
            //catch (Exception ex)
            //{
            //    bdi.Message = ex.ToString();
            //    bdi.ReceiveDetailID = ReceiveDetailID;
            //    bdi.CellbieDataJSON = "";
            //    bdi.CellbieStatus = new SendStatus(false, ex.ToString(), "", "");
            //    if (ex.InnerException != null)
            //    {
            //        bdi.CellbieStatus.InnerExceptionMessage = ex.InnerException.ToString();
            //    }
            //    return bdi;
            //}
            //var x = new StreamReader(response.GetResponseStream()).ReadToEnd();
            //bdi.CellbieDataJSON = x.ToString();
            #endregion
            bdi.CellbieDataJSON = "Need to simulate a valid reply";
            bdi.CellbieStatus = new SendStatus(true, "", "", "");
            return bdi;
        }
        public BrainDeviceTransaction ReceiveDevice(decimal ReceiveDetailID, string IMEI, bool OK, string Reason)
        {
            //  https://bridgetest.wbapp.ca/apiv1/receivedevice?token=zi9zk2rvv6ag5qj64mk6&IMEI=887&testmode=true
            //
            // token and IMEI are the only required parameters.
            string API = "apiv1/receivedevice";
            string fbagree = "true";               // API Default if not sent
            string fbtext = "";                    // API Default if not sent
            string testmode = "false";             // API Default if not sent
            bool TestMODE = true;
            if (OK != true) { fbagree = "false"; }
            if (TestMODE == true) { testmode = "true"; }
            fbtext = Reason;
            string requestURL = AccessDomain + API;
            BrainDeviceTransaction bdi = new BrainDeviceTransaction(API);
            bdi.ReceiveDetailID = ReceiveDetailID;
            GetDeviceReceiveParameters Params = new GetDeviceReceiveParameters(ReceiveDetailID, AccessTokenHidden, IMEI, OK, fbagree, fbtext, testmode, TestMODE, Reason);
            bdi.CellbieParameterJSON = Params.ToJSON();
            if (ReceiveDetailID < 1 || IMEI.Length == 0)
            {
                bdi.Message = "Error, missing IMEI and or ID";
                bdi.CellbieDataJSON = "";
                bdi.CellbieStatus = new SendStatus(false, "Missing IMEI and or ID", "", "");
                return bdi;
            }
            var postData = "token=" + AccessToken;
            postData += "&IMEI=" + Params.IMEI;
            postData += "&fbagree=" + Params.fbagree;
            postData += "&fbtext=" + Params.fbtext;
            postData += "&testmode=" + Params.testmode;
            #region Call to be replaced/Simmulated
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestURL);
            //var data = Encoding.ASCII.GetBytes(postData);
            //request.Method = "POST";
            //request.ContentType = "application/x-www-form-urlencoded";
            //request.ContentLength = data.Length;
            //using (var stream = request.GetRequestStream()) { stream.Write(data, 0, data.Length); }
            //HttpWebResponse response = null;
            //try
            //{
            //    response = (HttpWebResponse)request.GetResponse();
            //}

            //catch (WebException ex)
            //{
            //    using (var stream = ex.Response.GetResponseStream())
            //    using (var reader = new StreamReader(stream))
            //    {
            //        bdi.Message = ex.ToString();
            //        bdi.CellbieDataJSON = "";
            //        bdi.CellbieStatus = new SendStatus(false, ex.ToString(), "", "");
            //        bdi.CellbieStatus.InnerExceptionMessage = reader.ReadToEnd();
            //        bdi.CellbieError = Cellbieerror.fromJSON(bdi.CellbieStatus.InnerExceptionMessage);
            //        return bdi;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    bdi.Message = ex.ToString();
            //    bdi.CellbieDataJSON = "";
            //    bdi.CellbieStatus = new SendStatus(false, ex.ToString(), "", "");
            //    if (ex.InnerException != null)
            //    {
            //        bdi.CellbieStatus.InnerExceptionMessage = ex.InnerException.ToString();
            //    }
            //    //bdi.Message = request.
            //    return bdi;
            //}
            //var x = new StreamReader(response.GetResponseStream()).ReadToEnd();
            ////DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(DeviceReceive));
            //bdi.CellbieDataJSON = x.ToString();
            #endregion
            string Seg1 = @"{'Name': 'Galaxy S7 Edge (SM-G935W8)', 'lock': {'locked': false, 'locktype': '', 'date': '19-03-19 22:45 GMT', 'org': 'accellerant.wbapp.ca'}, 'Make': 'Samsung', ";
            string Seg2 = @"'gsma': {'errorcode': '0', 'blacklist': 'No', 'reason': 'Success', 'greylist': 'No', 'date': '19-03-19 22:45 GMT', 'org': 'accellerant.wbapp.ca', 'callstatus': true}, 'Memory': '32GB', ";
            string Seg3 = @"'device': {'mceModel': 'SM-G935W8', 'mceMake': 'Samsung', 'network': '', 'country': 'ca', 'org': 'accellerant.wbapp.ca', 'mceProductCode': '', 'MCC': '', 'mceDisplayName': ";
            string Seg4 = string.Format(@"'Galaxy S7 Edge (SM-G935W8)', 'IMEI': '{0}', 'carrier': null, 'accDeviceBroken': false, 'hasCTN': false, 'date': '19-03-19 22:45 GMT', 'ICCID': '', 'mceMemSize': '32GB', 'MNC': '', ", Params.IMEI);
            string Seg5 = @"'CTN': ''}, 'questionnaire': {'answers': {}, 'date': '19-03-19 22:45 GMT', 'org': 'accellerant.wbapp.ca', 'questionnaireType': 'KNOWN QUALITY'}, 'quality': {'autoset': false, 'reason': 'Known Quality', ";
            string Seg6 = @"'qualityLetter': 'E', 'date': '19-03-19 22:45 GMT', 'org': 'accellerant.wbapp.ca', 'quality': 'Unacceptable'}, 'Model': 'SM-G935W8', ";
            string Seg6a = string.Format(@"'IMEI': '{0}', 'Price': '62.00', ", Params.IMEI);
            string Seg7 = @"'hwdiags': {'date': '19-03-19 22:45 GMT', 'org': 'accellerant.wbapp.ca', 'hwdiags': {'HWDIAG_KNOWN_QUALITY': {'category': 'SKIP_TEST', 'caption': 'Diagnostics skipped on known quality device', ";
            string Seg8 = @"'result': 'Pass', 'value': 0}}}, 'wiped': {'complete': true, 'date': '19-03-19 22:45 GMT', 'org': 'accellerant.wbapp.ca', 'type': 'Manual'}}";
            string ReturnJSON = Seg1 + Seg2 + Seg3 + Seg4 + Seg5 + Seg6 + Seg6a + Seg7 + Seg8;
            bdi.CellbieDataJSON = ReturnJSON.Replace("'", "\"");
            bdi.CellbieStatus = new SendStatus(true, "", "", "");
            return bdi;
        }
    }
    public class Cellbie_Rest_Client
    {
        #region Desc
        #region Cellbie Domain
        //Hi Guys:
        //
        //Based on our discussion yesterday, I've created a test domain for Jim to use while developing and testing the Brain/Cellbie Receive integration. The details are as follows:
        //•	Login URL: https://bridgetest.wbapp.ca/login
        //•	Username: bridgetestbuyer
        //•	Password: 4jim2change!
        //•	Username: bridgetestreceiver
        //•	Password: 4jim2change!
        //I'll process a few trades to the domain later today so there are some showing in the Buyer Overview tab. Pasting the IMEIs into the field in the Receiver account will bring up the details of the devices and allow you to receive them. We can add more as necessary. I'll send an introduction to Terry, who's the head of our development team and does the API development so you can work with him on the API call when you're ready. I've asked him to set up the token on the domain and we'll send it to you when it's in place (probably later today).
        //
        //Dave.
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------
        //Jim, I'd like to e-introduce you to Terry who runs our Engineering team here at Cellbie. Terry, meet Jim, who develops "The Brain" for our pals at Bridge.
        //
        //Jim, as promised, here's your token for the API (it's specifically for the bridgetest domain that I set up for you).
        //
        //zi9zk2rvv6ag5qj64mk6
        //
        //As you might expect, we'd like you to protect this token as confidential, as it's "the keys to the kingdom" so to speak. Having said that, it's currently authorized to access just the deviceinfo, getpricequote, getmodelquote calls, so you can only drive around parts of the kingdom with these keys!
        //
        //Terry is somewhat familiar with our discussion around integrating the Brain/Cellbie Receive functions and can work with you to build an appropriate call to make it happen. I'm going to be on vacation next week, but if there are questions I need to answer, I'll have access to email in the evenings and should be able to respond most nights.
        //
        //Happy Integrating!
        //
        //Dave.
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------
        #endregion
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------
        //I thought it might help a little if I explained how Cellbie works, especially around the receiving process. From the time Cellbie starts processing a device, 
        //    up until the time it is received, it is stored and tracked in our internal data store (Redis). Once it is received, 
        //      there is no more processing to be done within Cellbie, so we send the device to long term storage (Elastic) and release 
        //          it from our internal store. Within Cellbie users can perform state changing operations while the device is in the internal store, 
        //      but once it goes to external storage, it can really only be accessed for reporting purposes. Within the Cellbie UI, receiving a device 
        //          gives the agent performing the task the ability to agree with the device assessment provided by the intake agent, or disagree and 
        //              send them a text message regarding what was wrong. This information is used to assess and rate the intake technicians.

        //The new call, receivedevice, causes this transition from internal storage to external storage to occur. As such, receivedevice really can only 
        //    "see" devices that are still being processed. The call finishes up the device's processing and marks it to expire immediately, also sending it 
        //      into long term storage. It returns a dump of data about the device for you to use in creating an inventory record. If you were to call it a 
        //          second time on the same IMEI (assuming call #1 worked), you would get "no such device", since it is no longer in internal storage at all. 
        //              The receivedevice call needs only an IMEI to run successfully, but to give the functionality of rating the technician, it can optionally 
        //                  take fbagree and fbtext parameters to express disagreement with the device rating. Note that the call also has an optional parameter 
        //                      "testmode", a boolean that when set to true, causes the call to do everything it normally would (like checking parameters) *except* the 
        //                          actual receive & rating operations, allowing developers to experiment without receiving a bunch of devices inadvertently. 

        //The other (new to you) call, inventoryinfo, assumes a different kind of inventory integration - one where devices are received in Cellbie then 
        //    loaded later into the inventory system. As such, inventoryinfo only works on historical data, does not cause the device state to change within 
        //        Cellbie, and returns a dump of information useful to adding a device to inventory. The reason I gave you both of these calls is that they 
        //            are kind of the complements of each other. My thought was that in some of the BRAIN error cases we discussed earlier in the thread, 
        //                you could call inventoryinfo and if you got a success return code, you would know the device was already received. It might also 
        //                    be useful to you in catch up cases, or if some BRAIN-busting procedural mistakes are made.
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------
        #region Inventory Information
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------
        // *************************************** Inventory inventoryinfo
        // - API entrypoint inventoryinfo
        //    - accessed via a POST to https://yourdomain.wbapp.ca/apiv1/inventoryinfo
        //      - standard HTTP form POST must contain
        //        - token - developers API token, each token is unique to a domain
        //        - IMEI - IMEI of device to get information for
        //      - access via HTTPS only
        //      - on success returns status code 200 and a JSON structure (type application/json) which looks like:
        //        - {"Name":"Apple iPhone 5C", "Memory":"8Gb", "IMEI":"123456789123456", "Price":<FOB price paid>, 
        //           "Make":"Apple", "Model":"iPhone 5C", "device":<device info>, "questionnaire":<questionnaire info>, 
        //           "quality":<quality info>, "hwdiags":<test results>, "gsma":<gsma results>, 
        //           "lock"<lock status>, "wiped":<wipe status>}
        //      - on error returns appropriate error status and JSON structure (type application/json) which looks like:
        //        - {"error":"description of error"}
        //      - authentication tests that
        //        - you are accessing a known endpoint that you are allowed access to (error 405 Method Not Allowed)
        //        - a token has been submitted (error 400 Bad Request)
        //        - token is in our Db (error 401 Unauthorized)
        //        - request is on domain which matches domain token is assigned to (error 403 Forbidden)
        //        - request rate is less than 100 per 10 seconds
        //      - deviceinfo tests that
        //        - IMEI is supplied
        //        - device exists
        //        - device has been received by domain making request (except for demo IMEIs)
        //        - device has been processed in system
        //        - device has been sold
        //        - otherwise error 500 Internal Server Error
        //      - API can be tested with the cURL command line utility
        //        - curl -v -i -X POST -F "token=<yourAPItoken>" -F "IMEI=<theMEItoLookUp>" https://<yourdomain>.wbapp.ca/apiv1/inventoryinfo
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------
        #endregion
        #region ReceiveDevice
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------
        // *************************************** Inventory receivedevice
        //- API call receivedevice
        //    - accessed via a POST to https://yourdomain.wbapp.ca/apiv1/receivedevice
        //      - standard HTTP form POST must contain
        //        - token - developers API token, each token is unique to a domain
        //        - IMEI - IMEI of device to receive
        //      - POST may optionally contain
        //        - fbagree - boolean, defaults to true, use false to specify there was an issue with agent rating
        //        - fbtext - text, defaults to "Accepted", used only when fbagree is false to record receiver feedback to host agent
        //        - testmode - boolean, default false, operates call in test mode for testing, does all checks and returns as normal but does not actually receive device
        //      - access via HTTPS only
        //      - on success returns status code 200 and a JSON structure (type application/json) which looks like:
        //        - {"Name":"Apple iPhone 5C", "Memory":"8Gb", "IMEI":"123456789123456", "Price":<FOB price paid>, "Make":"Apple", "Model":"iPhone 5C", 
        //           "device":<device info>, "questionnaire":<questionnaire info>, "quality":<quality info>, "hwdiags":<test results>, "gsma":<gsma results>, 
        //           "lock"<lock status>, "wiped":<wipe status>}
        //      - on error returns appropriate error status and JSON structure (type application/json) which looks like:
        //        - {"error":"description of error"}
        //      - authentication tests that
        //        - you are accessing a known endpoint that you are allowed access to(error 405 Method Not Allowed)
        //        - a token has been submitted (error 400 Bad Request)
        //        - token is in our Db (error 401 Unauthorized)
        //        - request is on domain which matches domain token is assigned to (error 403 Forbidden)
        //        - request rate is less than 100 per 10 seconds
        //      -  receivedevice tests that
        //        - IMEI is supplied
        //        - device exists
        //        - device has not been received yet
        //        - device has been processed in system
        //        - device has been sold
        //        - calling domain is purchaser of device
        //        - otherwise error 500 Internal Server Error
        //      - API can be tested with the cURL command line utility
        //        - curl -v -i -X POST -F "token=<yourAPItoken>" -F "IMEI=<theMEItoLookUp>" https://<yourdomain>.wbapp.ca/apiv1/receivedevice
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------
        #endregion
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------
        #endregion
        /*
        curl -v -i -X POST -F "token=zi9zk2rvv6ag5qj64mk6" -F "IMEI=990004600575546" https://bridgetest.wbapp.ca/apiv1/inventoryinfo
         * {"IMEI":"356160070900412","Message":"","OK":true,"ReceiveDetailID":2959,"TestMode":true,"fbagree":"true","fbtext":"","testmode":"true","token":"zi9zk2rvv6ag5qj64mk6"}
        curl -v -i -X POST -F "token=zi9zk2rvv6ag5qj64mk6" -F "IMEI=358040080423290" https://bridgetest.wbapp.ca/apiv1/receivedevice
         * 
         * 
        curl -v -i -X POST -F "token=zi9zk2rvv6ag5qj64mk6" -F "IMEI=358040080423290" -F "fbagree=true" -F "fbtext=" -F "testmode=true" https://bridgetest.wbapp.ca/apiv1/receivedevice
        curl -v -i -X POST -F "token=zi9zk2rvv6ag5qj64mk6" -F "IMEI=355458060568814" -F "fbagree=true" -F "fbtext=" -F "testmode=true" https://bridgetest.wbapp.ca/apiv1/receivedevice
        curl -v -i -X POST -F "token=zi9zk2rvv6ag5qj64mk6" -F "IMEI=356160070900412" -F "fbagree=true" -F "fbtext=" -F "testmode=true" https://bridgetest.wbapp.ca/apiv1/receivedevice
        curl -v -i -X POST -F "token=zi9zk2rvv6ag5qj64mk6" -F "IMEI=990004600575546" -F "fbagree=true" -F "fbtext=" -F "testmode=true" https://bridgetest.wbapp.ca/apiv1/receivedevice
         * 
         * 
         * 
         * 
         * 
        */
        bool Debug = true;
        String Username = "";
        string AccessToken = "";
        string AccessTokenHidden = "";
        string AccessDomain = "";
        public Cellbie_Rest_Client(string username)
        {
            Username = username;
            AccessToken = SysUtil.CellbieAPIToken();          //"zi9zk2rvv6ag5qj64mk6";
            AccessDomain = SysUtil.CellbieAPIDomain();         //"https://bridgetest.wbapp.ca";
            AccessTokenHidden = AccessToken;
            if (Debug == false)
            {
                AccessTokenHidden = SysUtil.CellbieAPITokenHidden();
            }
        }
        public BrainDeviceTransaction Getinventoryinfo(decimal ReceiveDetailID, string IMEI)
        {
            string API = "apiv1/inventoryinfo";
            string requestURL = AccessDomain + API;
            GetDeviceInfoParameters Params = new GetDeviceInfoParameters(ReceiveDetailID, AccessTokenHidden, IMEI);
            var postData = "token=" + AccessToken;
            postData += "&IMEI=" + IMEI;
            var data = Encoding.ASCII.GetBytes(postData);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestURL);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            using (var stream = request.GetRequestStream()) { stream.Write(data, 0, data.Length); }
            BrainDeviceTransaction bdi = new BrainDeviceTransaction(API);
            bdi.ReceiveDetailID = ReceiveDetailID;
            HttpWebResponse response = null;
            bdi.CellbieParameterJSON = Params.ToJSON();
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception ex)
            {
                bdi.Message = ex.ToString();
                bdi.CellbieDataJSON = "";
                bdi.CellbieStatus = new SendStatus(false, ex.ToString(), "", "");
                if (ex.InnerException != null)
                {
                    bdi.CellbieStatus.InnerExceptionMessage = ex.InnerException.ToString();
                }
                return bdi;
            }
            var x = new StreamReader(response.GetResponseStream()).ReadToEnd();
            //DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(DeviceInfo));
            //DeviceInfo result = ser.ReadObject(x.ToStream()) as DeviceInfo;
            //bdi.Message = x.ToString();
            //bdi.ReceiveDetailID = ReceiveDetailID;
            //bdi.CellbieData = result;
            bdi.CellbieDataJSON = x.ToString();
            bdi.CellbieStatus = new SendStatus(true, "", "", "");
            return bdi;
        }
        public BrainDeviceTransaction ReceiveDevice(decimal ReceiveDetailID, string IMEI, bool OK, string Reason)
        {
            //  https://bridgetest.wbapp.ca/apiv1/receivedevice?token=zi9zk2rvv6ag5qj64mk6&IMEI=887&testmode=true
            //
            // token and IMEI are the only required parameters.

            string API = "apiv1/receivedevice";
            string fbagree = "true";               // API Default if not sent
            string fbtext = "";                    // API Default if not sent
            string testmode = "false";             // API Default if not sent
            bool TestMODE = true;
            if (OK != true) { fbagree = "false"; }
            if (TestMODE == true) { testmode = "true"; }
            fbtext = Reason;
            string requestURL = AccessDomain + API;
            BrainDeviceTransaction bdi = new BrainDeviceTransaction(API);
            bdi.ReceiveDetailID = ReceiveDetailID;
            GetDeviceReceiveParameters Params = new GetDeviceReceiveParameters(ReceiveDetailID, AccessTokenHidden, IMEI, OK, fbagree, fbtext, testmode, TestMODE, Reason);
            bdi.CellbieParameterJSON = Params.ToJSON();
            if (ReceiveDetailID < 1 || IMEI.Length == 0)
            {
                bdi.Message = "Error, missing IMEI and or ID";
                bdi.CellbieDataJSON = "";
                bdi.CellbieStatus = new SendStatus(false, "Missing IMEI and or ID", "", "");
                return bdi;
            }

            var postData = "token=" + AccessToken;
            postData += "&IMEI=" + Params.IMEI;
            postData += "&fbagree=" + Params.fbagree;
            postData += "&fbtext=" + Params.fbtext;
            postData += "&testmode=" + Params.testmode;
            var data = Encoding.ASCII.GetBytes(postData);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestURL);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            using (var stream = request.GetRequestStream()) { stream.Write(data, 0, data.Length); }
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }

            catch (WebException ex)
            {
                using (var stream = ex.Response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    bdi.Message = ex.ToString();
                    bdi.CellbieDataJSON = "";
                    bdi.CellbieStatus = new SendStatus(false, ex.ToString(), "", "");
                    bdi.CellbieStatus.InnerExceptionMessage = reader.ReadToEnd();
                    bdi.CellbieError = Cellbieerror.fromJSON(bdi.CellbieStatus.InnerExceptionMessage);
                    return bdi;
                }
            }
            catch (Exception ex)
            {
                bdi.Message = ex.ToString();
                bdi.CellbieDataJSON = "";
                bdi.CellbieStatus = new SendStatus(false, ex.ToString(), "", "");
                if (ex.InnerException != null)
                {
                    bdi.CellbieStatus.InnerExceptionMessage = ex.InnerException.ToString();
                }
                //bdi.Message = request.
                return bdi;
            }
            var x = new StreamReader(response.GetResponseStream()).ReadToEnd();
            //DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(DeviceReceive));
            bdi.CellbieDataJSON = x.ToString();
            bdi.CellbieStatus = new SendStatus(true, "", "", "");
            return bdi;
        }
        #region Example Calls
        ///// <summary>
        ///// Get all contacts
        ///// </summary>
        ///// <returns></returns>
        //static List<Contact> GetAll()
        //{
        //    // web client
        //    WebClient client = new WebClient();
        //    client.Headers["Content-type"] = "application/json";

        //    // invoke the REST method
        //    byte[] data = client.DownloadData(
        //           "http://localhost:49193/Contacts.svc/GetAll");

        //    // put the downloaded data in a memory stream
        //    MemoryStream ms = new MemoryStream();
        //    ms = new MemoryStream(data);

        //    // deserialize from json
        //    DataContractJsonSerializer ser =
        //           new DataContractJsonSerializer(typeof(List<Contact>));

        //    List<Contact> result = ser.ReadObject(ms) as List<Contact>;

        //    return result;
        //}

        ///// <summary>
        ///// Get a single contact
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //static Contact GetContact(string id)
        //{
        //    // web client
        //    WebClient client = new WebClient();
        //    client.Headers["Content-type"] = "application/json";

        //    // invoke the REST method
        //    byte[] data = client.DownloadData(
        //           "http://localhost:49193/Contacts.svc/GetContact/" + id);

        //    // put the downloaded data in a memory stream
        //    MemoryStream ms = new MemoryStream();
        //    ms = new MemoryStream(data);

        //    // deserialize from json
        //    DataContractJsonSerializer ser =
        //           new DataContractJsonSerializer(typeof(Contact));
        //    Contact result = ser.ReadObject(ms) as Contact;

        //    return result;
        //}

        ///// <summary>
        ///// Add a contact
        ///// </summary>
        ///// <param name="c"></param>
        ///// <returns></returns>
        //static Contact Add(Contact c)
        //{
        //    // web client
        //    WebClient client = new WebClient();
        //    client.Headers["Content-type"] = "application/json";

        //    // serialize the object data in json format
        //    MemoryStream ms = new MemoryStream();
        //    DataContractJsonSerializer ser =
        //           new DataContractJsonSerializer(typeof(Contact));
        //    ser.WriteObject(ms, c);

        //    // invoke the REST method
        //    byte[] data = client.UploadData(
        //           "http://localhost:49193/Contacts.svc/Add",
        //           "ADD",
        //           ms.ToArray());

        //    // deserialize the data returned by the service
        //    ms = new MemoryStream(data);
        //    Contact result = ser.ReadObject(ms) as Contact;

        //    return result;
        //}

        ///// <summary>
        ///// Update the contact
        ///// </summary>
        ///// <param name="c"></param>
        //static void Update(Contact c)
        //{
        //    // web client
        //    WebClient client = new WebClient();
        //    client.Headers["Content-type"] = "application/json";

        //    // serialize the object data in json format
        //    MemoryStream ms = new MemoryStream();
        //    DataContractJsonSerializer ser =
        //           new DataContractJsonSerializer(typeof(Contact));
        //    ser.WriteObject(ms, c);

        //    // invoke the REST method
        //    client.UploadData(
        //            "http://localhost:49193/Contacts.svc/Update",
        //            "PUT",
        //            ms.ToArray());
        //}

        ///// <summary>
        ///// Delete the contact
        ///// </summary>
        ///// <param name="id"></param>
        //static void Delete(string id)
        //{
        //    // web client
        //    WebClient client = new WebClient();
        //    client.Headers["Content-type"] = "application/json";

        //    // serialize the object data in json format
        //    MemoryStream ms = new MemoryStream();
        //    DataContractJsonSerializer ser =
        //           new DataContractJsonSerializer(typeof(string));
        //    ser.WriteObject(ms, id);

        //    // invoke the REST method
        //    byte[] data = client.UploadData(
        //           "http://localhost:49193/Contacts.svc/Delete",
        //           "DELETE",
        //           ms.ToArray());

        //}
        #endregion
    }


    public class GetDeviceReceiveParameters
    {
        public decimal ReceiveDetailID { get; set; }
        public string token { get; set; }
        public string IMEI { get; set; }
        public bool OK { get; set; }
        public bool TestMode { get; set; }
        public string fbagree { get; set; }
        public string fbtext { get; set; }
        public string testmode { get; set; }
        //var postData = "token=" + AccessToken;
        //postData += "&IMEI=" + Params.IMEI;
        //postData += "&fbagree=" + fbagree;
        //postData += "&fbtext=" + fbtext;
        //postData += "&testmode=" + testmode;


        public string Message { get; set; }
        public GetDeviceReceiveParameters() { }
        public GetDeviceReceiveParameters(decimal receivedetailid, string Token, string Imei, bool ok, string Fbagree, string Fbtext, string Fbtestmode, bool Testmode, string message)
        {
            token = Token;
            IMEI = Imei;
            OK = ok;
            Message = message;
            ReceiveDetailID = receivedetailid;
            TestMode = Testmode;
            fbagree = Fbagree;
            fbtext = Fbtext;
            testmode = Fbtestmode;
        }
        public string ToJSON()
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(GetDeviceReceiveParameters));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, this);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            return sr.ReadToEnd();
        }
        public GetDeviceReceiveParameters fromJSON(string json)
        {
            GetDeviceReceiveParameters deserializedUser = new GetDeviceReceiveParameters();
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(deserializedUser.GetType());
            deserializedUser = ser.ReadObject(ms) as GetDeviceReceiveParameters;
            ms.Close();
            return deserializedUser;
        }
    }
    public class GetDeviceInfoParameters
    {
        public decimal ReceiveDetailID { get; set; }
        public string token { get; set; }
        public string IMEI { get; set; }
        public GetDeviceInfoParameters() { }
        public GetDeviceInfoParameters(decimal receivedetailid, string Token, string Imei) { token = Token; IMEI = Imei; ReceiveDetailID = receivedetailid; }
        public string ToJSON()
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(GetDeviceInfoParameters));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, this);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            return sr.ReadToEnd();
        }
        public GetDeviceInfoParameters fromJSON(string json)
        {
            GetDeviceInfoParameters deserializedUser = new GetDeviceInfoParameters();
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(deserializedUser.GetType());
            deserializedUser = ser.ReadObject(ms) as GetDeviceInfoParameters;
            ms.Close();
            return deserializedUser;
        }
    }
    public class Cellbieerror
    {
        //{"error": "Bad Request - No such device 356160070900412"}
        public string error { get; set; }
        public string Error { get { return error; } set { error = value; } }
        public Cellbieerror()
        {
            error = "";
        }

        public string ToJSON()
        {
            //MemoryStream stream1 = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Cellbieerror));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, this);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            return sr.ReadToEnd();
            //DeviceInfo result = ser.ReadObject(x.ToStream()) as DeviceInfo;
        }
        public static Cellbieerror fromJSON(string json)
        {
            Cellbieerror deserializedUser = new Cellbieerror();
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(deserializedUser.GetType());
            deserializedUser = ser.ReadObject(ms) as Cellbieerror;
            ms.Close();
            return deserializedUser;
        }

    }


    public class BrainDeviceTransaction
    {
        public string API { get; set; }
        public decimal ReceiveDetailID { get; set; }
        public string APIType
        {
            get
            {
                if (API == "apiv1/receivedevice")
                {
                    return "receivedevice";
                }
                else if (API == "apiv1/inventoryinfo")
                {
                    return "inventoryinfo";
                }
                else
                {
                    return "Unknown";
                }
            }
        }
        //public DeviceInfo CellbieData { get; set; }
        public string CellbieParameterJSON { get; set; }
        public string CellbieDataJSON { get; set; }
        public String Message
        {
            get { return CellbieStatus.Message; }
            set { CellbieStatus.Message = value; }
        }
        public String MessageRecordTransaction
        {
            get { return RecordTransactionStatus.Message; }
            set { RecordTransactionStatus.Message = value; }
        }

        public SendStatus CellbieStatus { get; set; }
        public SendStatus RecordTransactionStatus { get; set; }

        public bool Success { get { return CellbieStatus.Success; } }
        public string SuccessText { get { return CellbieStatus.Success ? "Success" : "Error"; } }
        public bool SuccessRecordTransaction { get { return RecordTransactionStatus.Success; } }
        Cellbieerror _error { get; set; }
        public Cellbieerror CellbieError
        {
            get { return _error; }
            set
            {
                _error = value;
                if (value.Error.Length > 0)
                {
                    CellbieStatus.Success = false;
                    Message = value.Error;
                }
            }
        }
        Cellbieerror _errorWriteTransaction { get; set; }
        public Cellbieerror BrainError
        {
            get { return _errorWriteTransaction; }
            set
            {
                _errorWriteTransaction = value;
                if (value.Error.Length > 0)
                {
                    RecordTransactionStatus.Success = false;
                    MessageRecordTransaction = value.Error;
                }
            }
        }
        public BrainDeviceTransaction()
        {
            API = "UNKNOWN";
            CellbieStatus = new SendStatus();
            RecordTransactionStatus = new SendStatus();
            CellbieError = new Cellbieerror();
            BrainError = new Cellbieerror();
        }
        public BrainDeviceTransaction(string api)
        {
            API = api;
            CellbieStatus = new SendStatus();
            RecordTransactionStatus = new SendStatus();
            CellbieError = new Cellbieerror();
            BrainError = new Cellbieerror();
        }
        public string ToJSON()
        {
            //MemoryStream stream1 = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(BrainDeviceTransaction));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, this);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            return sr.ReadToEnd();
            //DeviceInfo result = ser.ReadObject(x.ToStream()) as DeviceInfo;
        }
        public BrainDeviceTransaction fromJSON(string json)
        {
            BrainDeviceTransaction deserializedUser = new BrainDeviceTransaction();
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(deserializedUser.GetType());
            deserializedUser = ser.ReadObject(ms) as BrainDeviceTransaction;
            ms.Close();
            return deserializedUser;
        }
    }
    public class SendStatus
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string ExceptionMessage { get; set; }
        public string InnerExceptionMessage { get; set; }
        public void AssumeStatus(string message)
        {
            Message = message;
            Success = true;
            if (message.Length >= 5 && message.Substring(0, 5) == "Error")
            {
                Success = false;
            }
        }
        public SendStatus()
        {
            Success = false;
            Message = "";
            ExceptionMessage = "";
            InnerExceptionMessage = "";
        }
        public SendStatus(bool success, string exception, string innerexception, string message)
        {
            Success = success;
            Message = message;
            ExceptionMessage = exception;
            InnerExceptionMessage = innerexception;
        }
    }


    #region DeviceInfo
    public class DeviceInfo
    {
        #region Sample Data
        /*
          {
            "Name": "BB10 (Generic BB10)",
            "lock": {
              "locked": false,
              "locktype": "",
              "date": "19-03-19 22:50 GMT",
              "org": "accellerant.wbapp.ca"
            },
            "Make": "BlackBerry",
            "gsma": {
              "errorcode": "0",
              "blacklist": "No",
              "reason": "Success",
              "greylist": "No",
              "date": "19-03-19 22:50 GMT",
              "org": "accellerant.wbapp.ca",
              "callstatus": true
            },
            "Memory": "16GB",
            "device": {
              "mceModel": "Generic BB10",
              "mceMake": "BlackBerry",
              "network": "",
              "country": "ca",
              "mceProductCode": "",
              "MCC": "",
              "mceDisplayName": "BB10 (Generic BB10)",
              "hasCTN": false,
              "IMEI": "990004600575546",
              "carrier": null,
              "accDeviceBroken": false,
              "CTN": "",
              "org": "accellerant.wbapp.ca",
              "date": "19-03-19 22:50 GMT",
              "ICCID": "",
              "mceMemSize": "16GB",
              "MNC": ""
            },
            "questionnaire": {
              "answers": {
             
              },
              "date": "19-03-19 22:50 GMT",
              "org": "accellerant.wbapp.ca",
              "questionnaireType": "KNOWN QUALITY"
            },
            "quality": {
              "qualityLetter": "B",
              "autoset": false,
              "reason": "Known Quality",
              "date": "19-03-19 22:50 GMT",
              "org": "accellerant.wbapp.ca",
              "quality": "Very Good"
            },
            "Model": "Generic BB10",
            "IMEI": "990004600575546",
            "Price": "5.00",
            "hwdiags": {
              "date": "19-03-19 22:50 GMT",
              "org": "accellerant.wbapp.ca",
              "hwdiags": {
                "HWDIAG_KNOWN_QUALITY": {
                  "category": "SKIP_TEST",
                  "caption": "Diagnostics skipped on known quality device",
                  "result": "Pass",
                  "value": 0
                }
              }
            },
            "wiped": {
              "complete": true,
              "date": "19-03-19 22:50 GMT",
              "org": "accellerant.wbapp.ca",
              "type": "Manual"
            }
          }
        */
        #endregion
        //public decimal ReceiveDetailID { get; set; }
        public string Name { get; set; }
        public DeviceInfoLock @lock { get; set; }
        public string Make { get; set; }
        public DeviceInfogsma gsma { get; set; }
        public string Memory { get; set; }
        public DeviceInfodevice device { get; set; }
        public DeviceInfoquestionnair questionnaire { get; set; }
        public DeviceInfoquality quality { get; set; }
        public string Model { get; set; }
        public string IMEI { get; set; }
        public string Price { get; set; }   //
        public DeviceInfohwdiags hwdiags { get; set; }
        public DeviceInfowiped wiped { get; set; }
        public DeviceInfo() { }
        public string ToJSON()
        {
            //MemoryStream stream1 = new MemoryStream();  
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(DeviceInfo));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, this);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            return sr.ReadToEnd();
            //DeviceInfo result = ser.ReadObject(x.ToStream()) as DeviceInfo;
        }
        public DeviceInfo fromJSON(string json)
        {
            DeviceInfo deserializedUser = new DeviceInfo();
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(deserializedUser.GetType());
            deserializedUser = ser.ReadObject(ms) as DeviceInfo;
            ms.Close();
            return deserializedUser;
        }
    }

    public class DeviceReceive
    {
        public string Name { get; set; }
        public DeviceInfoLock @lock { get; set; }
        public string Make { get; set; }
        public DeviceInfogsma gsma { get; set; }
        public string Memory { get; set; }
        public DeviceInfodevice device { get; set; }
        public DeviceInfoquestionnair questionnaire { get; set; }
        public DeviceInfoquality quality { get; set; }
        public string Model { get; set; }
        public string IMIE { get; set; }
        public string Price { get; set; }
        public DeviceInfohwdiags hwdiags { get; set; }
        public DeviceInfowiped wiped { get; set; }
        public DeviceReceive() { }
        public string ToJSON()
        {
            //MemoryStream stream1 = new MemoryStream();  
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(DeviceReceive));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, this);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            return sr.ReadToEnd();
            //DeviceInfo result = ser.ReadObject(x.ToStream()) as DeviceInfo;
        }
        public DeviceReceive fromJSON(string json)
        {
            DeviceReceive deserializedUser = new DeviceReceive();
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(deserializedUser.GetType());
            deserializedUser = ser.ReadObject(ms) as DeviceReceive;
            ms.Close();
            return deserializedUser;
        }
    }

    public class DeviceInfoLock
    {
        public bool locked { get; set; }
        public string locktype { get; set; }
        public string date { get; set; }
        public string org { get; set; }
        public DeviceInfoLock() { }
    }
    public class DeviceInfogsma
    {
        public string errorcode { get; set; }
        public string blacklist { get; set; }
        public string reason { get; set; }
        public string greylist { get; set; }
        public string date { get; set; }
        public string org { get; set; }
        public bool callstatus { get; set; }
        public DeviceInfogsma() { }
    }
    public class DeviceInfodevice
    {
        public string mceModel { get; set; }
        public string mceMake { get; set; }
        public string network { get; set; }
        public string country { get; set; }
        public string mceProductCode { get; set; }
        public string MCC { get; set; }
        public string mceDisplayName { get; set; }
        public bool hasCTN { get; set; }
        public string IMEI { get; set; }
        public string carrier { get; set; }
        public bool accDeviceBroken { get; set; }
        public string org { get; set; }
        public string date { get; set; }
        public string ICCID { get; set; }
        public string mceMemSize { get; set; }
        public string MNC { get; set; }
        public DeviceInfodevice() { }
    }
    public class DeviceInfoquestionnair
    {
        public List<string> answers { get; set; }
        public string org { get; set; }
        public string date { get; set; }
        public string questionnaireType { get; set; }
        public DeviceInfoquestionnair() { }
    }
    public class DeviceInfoquality
    {
        public string qualityLetter { get; set; }
        public bool autoset { get; set; }
        public string reason { get; set; }
        public string org { get; set; }
        public string date { get; set; }
        public string quality { get; set; }
        public DeviceInfoquality() { }
    }
    public class DeviceInfohwdiags
    {
        public string org { get; set; }
        public string date { get; set; }
        public DeviceInfohwdiagsb hwdiags { get; set; }
        public DeviceInfohwdiags() { }
    }
    public class DeviceInfohwdiagsb
    {
        public DeviceInfoHWDIAG_KNOWN_QUALITY HWDIAG_KNOWN_QUALITY { get; set; }
        public DeviceInfohwdiagsb() { }
    }
    public class DeviceInfoHWDIAG_KNOWN_QUALITY
    {
        public string category { get; set; }
        public string caption { get; set; }
        public string result { get; set; }
        public string value { get; set; }
        public DeviceInfoHWDIAG_KNOWN_QUALITY() { }
    }
    public class DeviceInfowiped
    {
        public string complete { get; set; }
        public string org { get; set; }
        public string date { get; set; }
        public string type { get; set; }
        public DeviceInfowiped() { }
    }
    #endregion

    #region DeviceReceive
    #endregion



    //    public class Cellbie_API
    //    {
    //        #region Desc
    //        //----------------------------------------------------------------------------------------------------------------------------------------------------------------
    //        //----------------------------------------------------------------------------------------------------------------------------------------------------------------
    //        //I thought it might help a little if I explained how Cellbie works, especially around the receiving process. From the time Cellbie starts processing a device, 
    //        //    up until the time it is received, it is stored and tracked in our internal data store (Redis). Once it is received, 
    //        //      there is no more processing to be done within Cellbie, so we send the device to long term storage (Elastic) and release 
    //        //          it from our internal store. Within Cellbie users can perform state changing operations while the device is in the internal store, 
    //        //      but once it goes to external storage, it can really only be accessed for reporting purposes. Within the Cellbie UI, receiving a device 
    //        //          gives the agent performing the task the ability to agree with the device assessment provided by the intake agent, or disagree and 
    //        //              send them a text message regarding what was wrong. This information is used to assess and rate the intake technicians.

    //        //The new call, receivedevice, causes this transition from internal storage to external storage to occur. As such, receivedevice really can only 
    //        //    "see" devices that are still being processed. The call finishes up the device's processing and marks it to expire immediately, also sending it 
    //        //      into long term storage. It returns a dump of data about the device for you to use in creating an inventory record. If you were to call it a 
    //        //          second time on the same IMEI (assuming call #1 worked), you would get "no such device", since it is no longer in internal storage at all. 
    //        //              The receivedevice call needs only an IMEI to run successfully, but to give the functionality of rating the technician, it can optionally 
    //        //                  take fbagree and fbtext parameters to express disagreement with the device rating. Note that the call also has an optional parameter 
    //        //                      "testmode", a boolean that when set to true, causes the call to do everything it normally would (like checking parameters) *except* the 
    //        //                          actual receive & rating operations, allowing developers to experiment without receiving a bunch of devices inadvertently. 

    //        //The other (new to you) call, inventoryinfo, assumes a different kind of inventory integration - one where devices are received in Cellbie then 
    //        //    loaded later into the inventory system. As such, inventoryinfo only works on historical data, does not cause the device state to change within 
    //        //        Cellbie, and returns a dump of information useful to adding a device to inventory. The reason I gave you both of these calls is that they 
    //        //            are kind of the complements of each other. My thought was that in some of the BRAIN error cases we discussed earlier in the thread, 
    //        //                you could call inventoryinfo and if you got a success return code, you would know the device was already received. It might also 
    //        //                    be useful to you in catch up cases, or if some BRAIN-busting procedural mistakes are made.

    //        //----------------------------------------------------------------------------------------------------------------------------------------------------------------
    //        //----------------------------------------------------------------------------------------------------------------------------------------------------------------
    //        #endregion
    //        String Username = "";

    //        // - API entrypoint inventoryinfo
    //        //    - accessed via a POST to https://yourdomain.wbapp.ca/apiv1/inventoryinfo
    //        //      - standard HTTP form POST must contain
    //        //        - token - developers API token, each token is unique to a domain
    //        //        - IMEI - IMEI of device to get information for
    //        //      - access via HTTPS only
    //        //      - on success returns status code 200 and a JSON structure (type application/json) which looks like:
    //        //        - {"Name":"Apple iPhone 5C", "Memory":"8Gb", "IMEI":"123456789123456", "Price":<FOB price paid>, 
    //        //           "Make":"Apple", "Model":"iPhone 5C", "device":<device info>, "questionnaire":<questionnaire info>, 
    //        //           "quality":<quality info>, "hwdiags":<test results>, "gsma":<gsma results>, 
    //        //           "lock"<lock status>, "wiped":<wipe status>}
    //        //      - on error returns appropriate error status and JSON structure (type application/json) which looks like:
    //        //        - {"error":"description of error"}
    //        //      - authentication tests that
    //        //        - you are accessing a known endpoint that you are allowed access to (error 405 Method Not Allowed)
    //        //        - a token has been submitted (error 400 Bad Request)
    //        //        - token is in our Db (error 401 Unauthorized)
    //        //        - request is on domain which matches domain token is assigned to (error 403 Forbidden)
    //        //        - request rate is less than 100 per 10 seconds
    //        //      - deviceinfo tests that
    //        //        - IMEI is supplied
    //        //        - device exists
    //        //        - device has been received by domain making request (except for demo IMEIs)
    //        //        - device has been processed in system
    //        //        - device has been sold
    //        //        - otherwise error 500 Internal Server Error
    //        //      - API can be tested with the cURL command line utility
    //        //        - curl -v -i -X POST -F "token=<yourAPItoken>" -F "IMEI=<theMEItoLookUp>" https://<yourdomain>.wbapp.ca/apiv1/inventoryinfo


    //        public string inventoryinfo(string IMEI, bool OK, string Reason, ref string Message)
    //        {
    //            //var client = new HttpClient();
    //            //var pairs = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("pqpUserName", "admin"), new KeyValuePair<string, string>("password", "test@123") };
    //            //var content = new FormUrlEncodedContent(pairs);
    //            //var response = client.PostAsync("youruri", content).Result;
    //            //if (response.IsSuccessStatusCode)
    //            //{
    //            //    Message = "Success:" + response.IsSuccessStatusCode;
    //            //}
    //            //Message = "Error:" + response.IsSuccessStatusCode;
    //            return Message;
    //        }

    //        //- API call receivedevice
    //        //    - accessed via a POST to https://yourdomain.wbapp.ca/apiv1/receivedevice
    //        //      - standard HTTP form POST must contain
    //        //        - token - developers API token, each token is unique to a domain
    //        //        - IMEI - IMEI of device to receive
    //        //      - POST may optionally contain
    //        //        - fbagree - boolean, defaults to true, use false to specify there was an issue with agent rating
    //        //        - fbtext - text, defaults to "Accepted", used only when fbagree is false to record receiver feedback to host agent
    //        //        - testmode - boolean, default false, operates call in test mode for testing, does all checks and returns as normal but does not actually receive device
    //        //      - access via HTTPS only
    //        //      - on success returns status code 200 and a JSON structure (type application/json) which looks like:
    //        //        - {"Name":"Apple iPhone 5C", "Memory":"8Gb", "IMEI":"123456789123456", "Price":<FOB price paid>, "Make":"Apple", "Model":"iPhone 5C", 
    //        //           "device":<device info>, "questionnaire":<questionnaire info>, "quality":<quality info>, "hwdiags":<test results>, "gsma":<gsma results>, 
    //        //           "lock"<lock status>, "wiped":<wipe status>}
    //        //      - on error returns appropriate error status and JSON structure (type application/json) which looks like:
    //        //        - {"error":"description of error"}
    //        //      - authentication tests that
    //        //        - you are accessing a known endpoint that you are allowed access to(error 405 Method Not Allowed)
    //        //        - a token has been submitted (error 400 Bad Request)
    //        //        - token is in our Db (error 401 Unauthorized)
    //        //        - request is on domain which matches domain token is assigned to (error 403 Forbidden)
    //        //        - request rate is less than 100 per 10 seconds
    //        //      -  receivedevice tests that
    //        //        - IMEI is supplied
    //        //        - device exists
    //        //        - device has not been received yet
    //        //        - device has been processed in system
    //        //        - device has been sold
    //        //        - calling domain is purchaser of device
    //        //        - otherwise error 500 Internal Server Error
    //        //      - API can be tested with the cURL command line utility
    //        //        - curl -v -i -X POST -F "token=<yourAPItoken>" -F "IMEI=<theMEItoLookUp>" https://<yourdomain>.wbapp.ca/apiv1/receivedevice
    //        public bool receivedevice(decimal ReceiveDetailID, string IMEI, bool OK, string Reason, ref string Message)
    //        {
    //            ReceiveDetailManager rdm = new ReceiveDetailManager(Username);
    //            rdm.Cellbie_Sent(ReceiveDetailID, IMEI, OK.ToString(), Reason);
    //            //var client = new HttpClient();
    //            //var pairs = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("pqpUserName", "admin"), new KeyValuePair<string, string>("password", "test@123") };
    //            //var content = new FormUrlEncodedContent(pairs);
    //            //var response = client.PostAsync("youruri", content).Result;
    //            //if (response.IsSuccessStatusCode)
    //            //{
    //            //    Message = "Success:" + response.IsSuccessStatusCode;
    //            //    rdm.Cellbie_Success(ReceiveDetailID, "", "", "");
    //            //    return true;
    //            //}
    //            // else
    //            //{
    //            //    Message = "Error:" + response.IsSuccessStatusCode;
    //            //    rdm.Cellbie_Success(ReceiveDetailID, "", "", "");
    //            //    return false;
    //            //}
    //            return true;
    //        }



    //        public Cellbie_API(string UserName)
    //        {
    //            Username = UserName;
    //        }





    //        public void xxxxx(string requestUrl, string referrer)
    //        {
    //            HttpWebRequest http = (HttpWebRequest)HttpWebRequest.Create(requestUrl);
    //            http.Referer = referrer;
    //            HttpWebResponse response = (HttpWebResponse)http.GetResponse();
    //            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
    //            {
    //                string responseJson = sr.ReadToEnd();
    //                // more stuff
    //            }
    //        }


    //        public static string HttpPost(string URI, string Parameters)
    //        {
    //            string ProxyString = "";
    //            System.Net.WebRequest req = System.Net.WebRequest.Create(URI);
    //            req.Proxy = new System.Net.WebProxy(ProxyString, true);
    //            //Add these, as we're doing a POST
    //            req.ContentType = "application/x-www-form-urlencoded";
    //            req.Method = "POST";
    //            //We need to count how many bytes we're sending. 
    //            //Post'ed Faked Forms should be name=value&
    //            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(Parameters);
    //            req.ContentLength = bytes.Length;
    //            System.IO.Stream os = req.GetRequestStream();
    //            os.Write(bytes, 0, bytes.Length); //Push it out there
    //            os.Close();
    //            System.Net.WebResponse resp = req.GetResponse();
    //            if (resp == null) return null;
    //            System.IO.StreamReader sr =
    //                  new System.IO.StreamReader(resp.GetResponseStream());
    //            return sr.ReadToEnd().Trim();
    //        }


    //        private void PostForm()
    //        {
    //            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://dork.com/service");
    //            //request.Method = "POST";
    //            //request.ContentType = "application/x-www-form-urlencoded";
    //            //string postData = "home=Cosby&favorite+flavor=flies";
    //            //byte[] bytes = Encoding.UTF8.GetBytes(postData);
    //            //request.ContentLength = bytes.Length;

    //            //Stream requestStream = request.GetRequestStream();
    //            //requestStream.Write(bytes, 0, bytes.Length);

    //            //WebResponse response = request.GetResponse();
    //            //Stream stream = response.GetResponseStream();
    //            //StreamReader reader = new StreamReader(stream);

    //            //var result = reader.ReadToEnd();
    //            //stream.Dispose();
    //            //reader.Dispose();
    //        }


    //        private string WebRequestGetExample()
    //        {
    //            string rValue = "";
    //            WebRequest request = WebRequest.Create("http://www.contoso.com/default.html");
    //            // If required by the server, set the credentials.
    //            request.Credentials = CredentialCache.DefaultCredentials;
    //            // Get the response.
    //            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
    //            // Display the status.
    //            //Console.WriteLine(response.StatusDescription);
    //            // Get the stream containing content returned by the server.
    //            Stream dataStream = response.GetResponseStream();
    //            // Open the stream using a StreamReader for easy access.
    //            StreamReader reader = new StreamReader(dataStream);
    //            // Read the content.
    //            string responseFromServer = reader.ReadToEnd();
    //            // Display the content.
    //            rValue = responseFromServer;
    //            //Console.WriteLine(responseFromServer);
    //            // Cleanup the streams and the response.
    //            reader.Close();
    //            dataStream.Close();
    //            response.Close();
    //            return rValue;
    //        }


    //        /*



    //- API entrypoint inventoryinfo
    //    - accessed via a POST to https://yourdomain.wbapp.ca/apiv1/inventoryinfo
    //      - standard HTTP form POST must contain
    //        - token - developers API token, each token is unique to a domain
    //        - IMEI - IMEI of device to get information for
    //      - access via HTTPS only
    //      - on success returns status code 200 and a JSON structure (type application/json) which looks like:
    //        - {"Name":"Apple iPhone 5C", "Memory":"8Gb", "IMEI":"123456789123456", "Price":<FOB price paid>, 
    //           "Make":"Apple", "Model":"iPhone 5C", "device":<device info>, "questionnaire":<questionnaire info>, 
    //           "quality":<quality info>, "hwdiags":<test results>, "gsma":<gsma results>, 
    //           "lock"<lock status>, "wiped":<wipe status>}
    //      - on error returns appropriate error status and JSON structure (type application/json) which looks like:
    //        - {"error":"description of error"}
    //      - authentication tests that
    //        - you are accessing a known endpoint that you are allowed access to (error 405 Method Not Allowed)
    //        - a token has been submitted (error 400 Bad Request)
    //        - token is in our Db (error 401 Unauthorized)
    //        - request is on domain which matches domain token is assigned to (error 403 Forbidden)
    //        - request rate is less than 100 per 10 seconds
    //      - deviceinfo tests that
    //        - IMEI is supplied
    //        - device exists
    //        - device has been received by domain making request (except for demo IMEIs)
    //        - device has been processed in system
    //        - device has been sold
    //        - otherwise error 500 Internal Server Error
    //      - API can be tested with the cURL command line utility
    //        - curl -v -i -X POST -F "token=<yourAPItoken>" -F "IMEI=<theMEItoLookUp>" https://<yourdomain>.wbapp.ca/apiv1/inventoryinfo

    //- API call receivedevice
    //    - accessed via a POST to https://yourdomain.wbapp.ca/apiv1/receivedevice
    //      - standard HTTP form POST must contain
    //        - token - developers API token, each token is unique to a domain
    //        - IMEI - IMEI of device to receive
    //      - POST may optionally contain
    //        - fbagree - boolean, defaults to true, use false to specify there was an issue with agent rating
    //        - fbtext - text, defaults to "Accepted", used only when fbagree is false to record receiver feedback to host agent
    //        - testmode - boolean, default false, operates call in test mode for testing, does all checks and returns as normal but does not actually receive device
    //      - access via HTTPS only
    //      - on success returns status code 200 and a JSON structure (type application/json) which looks like:
    //        - {"Name":"Apple iPhone 5C", "Memory":"8Gb", "IMEI":"123456789123456", "Price":<FOB price paid>, "Make":"Apple", "Model":"iPhone 5C", 
    //           "device":<device info>, "questionnaire":<questionnaire info>, "quality":<quality info>, "hwdiags":<test results>, "gsma":<gsma results>, 
    //           "lock"<lock status>, "wiped":<wipe status>}
    //      - on error returns appropriate error status and JSON structure (type application/json) which looks like:
    //        - {"error":"description of error"}
    //      - authentication tests that
    //        - you are accessing a known endpoint that you are allowed access to(error 405 Method Not Allowed)
    //        - a token has been submitted (error 400 Bad Request)
    //        - token is in our Db (error 401 Unauthorized)
    //        - request is on domain which matches domain token is assigned to (error 403 Forbidden)
    //        - request rate is less than 100 per 10 seconds
    //      -  receivedevice tests that
    //        - IMEI is supplied
    //        - device exists
    //        - device has not been received yet
    //        - device has been processed in system
    //        - device has been sold
    //        - calling domain is purchaser of device
    //        - otherwise error 500 Internal Server Error
    //      - API can be tested with the cURL command line utility
    //        - curl -v -i -X POST -F "token=<yourAPItoken>" -F "IMEI=<theMEItoLookUp>" https://<yourdomain>.wbapp.ca/apiv1/receivedevice














    //        */
    //    }

}
