using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
//using Factory_DataModel;
using BW_WebApp.DataManagers;
using Syncfusion.Web.UI.WebControls.Shared;
using Syncfusion.XlsIO;

namespace BM_WebApp.Account
{
    public partial class BulkLoadUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnUpload.Click += new EventHandler(btnUpload_Click);
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            UploadFile("~/IDAutomation", FileUploadXLS, lblMsgDetail);
        }
        private void UploadFile(string PathName, FileUpload UploadTool, Label Message)
        {
            //decimal ClientID = -1;
            //if (MainGrid.SelectedValue == null || decimal.TryParse(MainGrid.SelectedValue.ToString(), out ClientID) == false) { ClientID = -1; };
            //if (ClientID < 1)
            //{
            //    Message.Text = "Please select a Client first";
            //    Message.ForeColor = System.Drawing.Color.Red;
            //    Message.Visible = true;
            //    return;
            //}

            if (UploadTool.HasFile)
            {
                string strFileName = UploadTool.FileName + "_" + DateTime.Now.ToString("ddMMyyyy_HHmmss");
                string strFileType = System.IO.Path.GetExtension(UploadTool.FileName).ToString().ToLower();
                //Check file type
                if (strFileType == ".xls" || strFileType == ".xlsx")
                {
                    ////decimal ClientID = decimal.Parse(MainGrid.SelectedValue.ToString());
                    UploadTool.SaveAs(Server.MapPath(PathName + "/" + strFileName + strFileType));
                    ImportNewUserData(Server.MapPath(PathName + "/" + strFileName + strFileType));

                    Message.Text = "Data File Uploaded!";
                    Message.ForeColor = System.Drawing.Color.Green;
                    Message.Visible = true;
                    //UpdateChildGrid(ClientID);
                    ////UpdateMainGrid();
                }
                else
                {
                    Message.Text = "Only excel files allowed";
                    Message.ForeColor = System.Drawing.Color.Red;
                    Message.Visible = true;
                }
            }
            else
            {
                Message.Text = "Please select an excel file first";
                Message.ForeColor = System.Drawing.Color.Red;
                Message.Visible = true;
            }
        }
        private void ImportNewUserData(string FileName)
        {
            //MasterCarrierManufacturerModelColourManager mlm = new MasterCarrierManufacturerModelColourManager(User.Identity.Name);
            ClientLocationManager clm = new ClientLocationManager(User.Identity.Name);

            ExcelEngine excelEngine = new ExcelEngine();
            IApplication application = excelEngine.Excel;
            IWorkbook workbook = application.Workbooks.Open(FileName, ExcelOpenType.Automatic);
            IWorksheet sheet = workbook.Worksheets[0];

            string UserName = "";
            string Password = "";
            string EmailAddress = "";
            string LocationScanCodes = "";
            string Roles = "";
            string isLocked = "";

            int columnStatus = 7;
            int Row = 2;
            //int ID = -1;

            sheet.Range[1, columnStatus].Value = "Status";
            while (sheet.Range[Row, 1].Text != null && sheet.Range[Row, 1].Text.Length > 0)          // Scankey 
            {
                UserName = (sheet.Range[Row, 1].Value == null ? "" : sheet.Range[Row, 1].Value);
                Password = (sheet.Range[Row, 2].Value == null ? "" : sheet.Range[Row, 2].Value);
                EmailAddress = (sheet.Range[Row, 3].Value == null ? "" : sheet.Range[Row, 3].Value);
                LocationScanCodes = (sheet.Range[Row, 4].Value == null ? "" : sheet.Range[Row, 4].Value);
                Roles = (sheet.Range[Row, 5].Value == null ? "" : sheet.Range[Row, 5].Value);
                isLocked = (sheet.Range[Row, 6].Value == null ? "" : sheet.Range[Row, 6].Value);

                if (Password.Length == 0) { Password = Membership.GeneratePassword(8, 2); sheet.Range[Row, 2].Value = Password; }

                sheet.Range[Row, columnStatus].Value = "";
                sheet.Range[Row, columnStatus].Value = ProcessUser(UserName, Password, EmailAddress, LocationScanCodes, Roles, isLocked);
                Row++;
            }
            //workbook.SaveAs("MasterCMMC_Uploaded.xls", Page.Response, ExcelDownloadType.Open);
            workbook.SaveAs("Client_Uploaded.xls", Page.Response, ExcelDownloadType.Open);
            workbook.Close();
            excelEngine.Dispose();
        }

        string ProcessUser(string UserName, string Password, string EmailAddress, string LocationScanCodes, string roles, string islocked)
        {
            if (Password.Length == 0) { return "Invalid Password"; }
            if (EmailAddress.Length == 0) { return "Invalid Email Address"; }
            // See if the user exists and if not, create the user.
            if (Membership.FindUsersByName(UserName).Count > 0)
            {
                return UpdateUser(UserName, Password, EmailAddress, LocationScanCodes, roles, islocked);
            }
            else
            {
                return CreateUser(UserName, Password, EmailAddress, LocationScanCodes, roles, islocked);
            }
        }

        string UpdateUser(string UserName, string Password, string EmailAddress, string LocationScanCodes, string roles, string islocked)
        {
            return "Error: User already exists!";
            //BasicUserUtilities bu = new BasicUserUtilities(User.Identity.Name, UserName, LocationScanCodes);
            //return bu.UpdateUser(Password, EmailAddress, roles, islocked);

        }


        string CreateUser(string UserName, string Password, string EmailAddress, string LocationScanCodes, string roles, string islocked)
        {
            BasicUserUtilities bu = new BasicUserUtilities(User.Identity.Name, UserName, LocationScanCodes,"");
            return bu.CreateUser(Password, EmailAddress, roles, islocked);
        }


        string CreateUser(string UserName, string Password, string EmailAddress, string LocationScanCodes, string roles, string islocked, string FriendlyName)
        {
            BasicUserUtilities bu = new BasicUserUtilities(User.Identity.Name, UserName, LocationScanCodes, FriendlyName);
            return bu.CreateUser(Password, EmailAddress, roles, islocked);
        }



    }
}