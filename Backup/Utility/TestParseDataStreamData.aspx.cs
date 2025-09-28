using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BW_WebApp.DataManagers;

namespace BW_WebApp.Utility
{
    public partial class TestParseDataStreamData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //btnParseToLines.Click += new EventHandler(btnParseToLines_Click);
            //btnSubmitToProcess.Click += new EventHandler(btnSubmitToProcess_Click);
            btnTestAPI.Click += new EventHandler(btnTestAPI_Click);
            btnTestAttributeAPI.Click += new EventHandler(btnTestAttributeAPI_Click);
            btnBack.Click += new EventHandler(btnBack_Click);
            btnProcessResultBatch.Click += new EventHandler(btnProcessResultBatch_Click);
            btnProcessResultLogID.Click += new EventHandler(btnProcessResultLogID_Click);
        }

        void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(@"~/Default.aspx");
        }

        void btnTestAPI_Click(object sender, EventArgs e)
        {
            WebServer_01 WS = new WebServer_01();
            //string JSONDataString = "{\"batch\":\"XX\",\"client\":\"BW1\",\"project\":\"Bridge Product\",\"username\":\"jmccomb\",\"devices\":[            {\"esn\":\"358510070275338\",\"attributes\":[{\"attribute\":\"Colour\",\"value\":\"BLK\"},{\"attribute\":\"Provider\",\"value\":\"TEL\"},{\"attribute\":\"Dispostion\",\"value\":\"NotAssessed\"},{\"attribute\":\"Bin\",\"value\":\"\"}]},            {\"esn\":\"358510070275338\",\"attributes\":[{\"attribute\":\"Colour\",\"value\":\"BLK\"},{\"attribute\":\"Provider\",\"value\":\"TEL\"},{\"attribute\":\"Dispostion\",\"value\":\"NotAssessed\"},{\"attribute\":\"Bin\",\"value\":\"\"}]} ]}";


            //string JSONDataString = "{\"batch\":\"4658888\",\"client\":\"BW1\",\"process\":\"Receive\",\"project\":\"Bridge Product\",\"username\":\"admin\",\"devices\":[{\"esn\":\"458510099995398\",\"version\":\"\",\"attributes\":[{\"attribute\":\"Carrier\",\"value\":\"X\"},{\"attribute\":\"Manufacturer\",\"value\":\"ACT\"},{\"attribute\":\"Model\",\"value\":\"V1000H\"},{\"attribute\":\"Colour\",\"value\":\"BLK\"},{\"attribute\":\"Disposition\",\"value\":\"NotAssessed\"},{\"attribute\":\"Provider\",\"value\":\"FSL\"},{\"attribute\":\"Grade\",\"value\":\"A\"},{\"attribute\":\"Product Place\",\"value\":\"AID\"}]}]}";
            string JSONDataStringHome = "{\"batch\":\"BATCHGOESHERE\",\"client\":\"BW1\",\"process\":\"Receive\",\"project\":\"Bridge Product\",\"username\":\"jmccomb\",\"devices\""
                + ":[{\"esn\":\"ESNGOESHERE\",\"version\":\"\",\"attributes\":[{\"attribute\":\"Carrier\",\"value\":\"BEL\"},{\"attribute\":\"Manufacturer\",\"value\":\"aaa\"},{\"attribute\":\"projecttag\",\"value\":\"1223456789\"}," 
                + "{\"attribute\":\"Model\",\"value\":\"A12418Gjj\"},{\"attribute\":\"Colour\",\"value\":\"BLK\"},{\"attribute\":\"Disposition\",\"value\": \"NotAssessed\"}"
                + ",{\"attribute\":\"Provider\",\"value\":\"FSL\"},{\"attribute\":\"Grade\",\"value\":\"A\"},{\"attribute\":\"Product Place\",\"value\":\"ASSURANT\"}]}]}";


            string JSONDataStringBridge = "{\"batch\":\"BATCHGOESHERE\",\"client\":\"BW1\",\"process\":\"Receive\",\"project\":\" Bridge Product\",\"username\":\"jmccomb\",\"devices\""
                + ":[{\"esn\":\"ESNGOESHERE\",\"version\":\"\",\"attributes\":[{\"attribute\":\"Carrier\",\"value\":\"X\"},{\"attribute\":\"Manufacturer\",\"value\":\"ACT\"},{\"attribute\":\"projecttag\",\"value\":\"1223456789\"},"
                + "{\"attribute\":\"Model\",\"value\":\"V1000H\"},{\"attribute\":\"Colour\",\"value\":\"BLK\"},{\"attribute\":\"Disposition\",\"value\": \"NotAssessed\"}"
                + ",{\"attribute\":\"Provider\",\"value\":\"FSL\"},{\"attribute\":\"Grade\",\"value\":\"A\"},{\"attribute\":\"Product Place\",\"value\":\"AID\"}]}]}";

            string JSONDataString = "";
            JSONDataString = JSONDataStringHome;



            JSONDataString = JSONDataString.Replace("BATCHGOESHERE", TestBatch.Text);
            JSONDataString = JSONDataString.Replace("ESNGOESHERE", TestESN.Text);
            txtBatch.Text = TestBatch.Text;
            txtSource.Text = JSONDataString;
            txtProcessMessageList.Text = WS.BulkDeviceUpload(JSONDataString);
            //WebServer_01 ws = new WebServer_01();

        }
        void btnTestAttributeAPI_Click(object sender, EventArgs e)
        {
            {
                WebServer_01 WS = new WebServer_01();
                string JSONDataString = "{\"batch\": \"000100\","
                  + "  \"devices\": ["
                  + "    {"
                  + "      \"attributes\": ["
                  + "        {"
                  + "          \"attribute\": \"Fault Code 1\","
                  + "          \"value\": \"Software Failure\""
                  + "        },"
                  + "        {"
                  + "          \"attribute\": \"Disposition\","
                  + "          \"value\": \"Defective Mainboard\""
                  + "        }"
                  + "      ],"
                  + "      \"esn\": \"869426040051317\","
                  + "      \"version\": \"000\""
                  + "    },"
                  + "    {"
                  + "      \"attributes\": ["
                  + "        {"
                  + "          \"attribute\": \"Fault Code 1\","
                  + "          \"value\": \"SIM Failure\""
                  + "        },"
                  + "        {"
                  + "          \"attribute\": \"Disposition\","
                  + "          \"value\": \"Customer Abuse\""
                  + "        }"
                  + "      ],"
                  + "      \"esn\": \"014476001579438\","
                  + "      \"version\": \"001\""
                  + "    },"
                  + "    {"
                  + "      \"attributes\": ["
                  + "        {"
                  + "          \"attribute\": \"Fault Code 1\","
                  + "          \"value\": \"FunctionFailure\""
                  + "        },"
                  + "        {"
                  + "          \"attribute\": \"Disposition\","
                  + "          \"value\": \"ReturnAsIs\""
                  + "        }"
                  + "      ],"
                  + "      \"esn\": \"869426040055011\","
                  + "      \"version\": \"\""
                  + "    }"
                  + "  ],"
                  + "  \"username\": \"jmccomb\""
                  + "}";


                /*
                ESN = 869426040055011
                Version = 000
                Fault Code 1/Software Failure/SIM Failure/FunctionFailure
                Disposition/DOA/Customer Abuse/ReturnAsIs/Defective Mainboard 
                Select * from ReceiveDetail where ReceiveDetailID = 3162


                ESN = 869426040051317
                Version = 000
                Fault Code 1/Software Failure/SIM Failure/FunctionFailure
                Disposition/DOA/Customer Abuse/ReturnAsIs/Defective Mainboard 
                Select * from ReceiveDetail where ReceiveDetailID = 3130


                ESN = 014476001579438
                Version = 001
                Fault Code 1/Software Failure/SIM Failure/FunctionFailure
                Disposition/DOA/Customer Abuse/ReturnAsIs/Defective Mainboard 
                Select * from ReceiveDetail where ReceiveDetailID = 2674
                  */
                txtSource.Text = JSONDataString;
                txtProcessMessageList.Text = WS.BulkDeviceAttributeUpdate(JSONDataString);
            }
        }
        void btnProcessResultBatch_Click(object sender, EventArgs e)
        {
            WebServer_01 WS = new WebServer_01();
            //string JSONDataString = "{\"batch\":\"XX\",\"client\":\"BW1\",\"project\":\"Bridge Product\",\"username\":\"jmccomb\",\"devices\":[            {\"esn\":\"358510070275338\",\"attributes\":[{\"attribute\":\"Colour\",\"value\":\"BLK\"},{\"attribute\":\"Provider\",\"value\":\"TEL\"},{\"attribute\":\"Dispostion\",\"value\":\"NotAssessed\"},{\"attribute\":\"Bin\",\"value\":\"\"}]},            {\"esn\":\"358510070275338\",\"attributes\":[{\"attribute\":\"Colour\",\"value\":\"BLK\"},{\"attribute\":\"Provider\",\"value\":\"TEL\"},{\"attribute\":\"Dispostion\",\"value\":\"NotAssessed\"},{\"attribute\":\"Bin\",\"value\":\"\"}]} ]}";
            string Batch = txtBatch.Text;
            txtSource.Text = Batch;
            txtProcessMessageList.Text = WS.ProcessResultsBatch(Batch);
        }
        void btnProcessResultLogID_Click(object sender, EventArgs e)
        {
            WebServer_01 WS = new WebServer_01();
            //string JSONDataString = "{\"batch\":\"XX\",\"client\":\"BW1\",\"project\":\"Bridge Product\",\"username\":\"jmccomb\",\"devices\":[            {\"esn\":\"358510070275338\",\"attributes\":[{\"attribute\":\"Colour\",\"value\":\"BLK\"},{\"attribute\":\"Provider\",\"value\":\"TEL\"},{\"attribute\":\"Dispostion\",\"value\":\"NotAssessed\"},{\"attribute\":\"Bin\",\"value\":\"\"}]},            {\"esn\":\"358510070275338\",\"attributes\":[{\"attribute\":\"Colour\",\"value\":\"BLK\"},{\"attribute\":\"Provider\",\"value\":\"TEL\"},{\"attribute\":\"Dispostion\",\"value\":\"NotAssessed\"},{\"attribute\":\"Bin\",\"value\":\"\"}]} ]}";
            string LogID = txtBatchID.Text;
            txtSource.Text = LogID;
            txtProcessMessageList.Text = WS.ProcessResultsLogID(LogID);
        }

        #region Old
        //void btnSubmitToProcess_Click(object sender, EventArgs e)
        //{
        //    txtProcessMessageList.Text = "";
        //    if (txtDataStream.Text.Length == 0) { txtProcessMessageList.Text = "No Data Stream!"; return; }
        //    WebServer_01 WS = new WebServer_01();
        //    string value = WS.AddDataDetailThreaded(txtDataStream.Text, "N");
        //    JsonString ScreenData = new JsonString(value, true);
        //    //txtProcessMessageList.Text = ScreenData.ParsevaluePairsToLine();
        //}
        //void btnParseToLines_Click(object sender, EventArgs e)
        //{
        //    txtParseList.Text = "";
        //    if (txtDataStream.Text.Length == 0) { txtParseList.Text = "No Data Stream!"; return; }
        //    JsonString ScreenData = new JsonString(txtDataStream.Text, true);
        //    //txtParseList.Text = ScreenData.ParsevaluePairsToLine();
        //}
        #endregion

    }
}