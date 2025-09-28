using System;
using System.Collections.Generic;
//using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;



using BW_WebApp.Classes;
using BW_WebApp.DataManagers;
using NCalc;

namespace BW_WebApp.Utility
{
    public partial class TestParseEquation : System.Web.UI.Page
    {
        BusinessRules br = new BusinessRules();

        TimeLogManager timelog = null;


        void LoadBusinessRules()
        {
            br = new BusinessRules();
            br.Condtion = txtEquation.Text;
            br.DeviceData = txtData.Text;
            br.ReceiveDetailID = 69567;
            //br.AddParameter("Pi", 3.14);
            ////br.AddParameter("Age", 3.14);
            //br.AddParameter("X", "CB_177");
            //br.AddParameter("Y", "X");
            //br.AddParameter("Z", "Y");
            //FunctionDeligate xx = new FunctionDeligate(SecretFunction);
            //br.AddFunction("SecretAdd", new FunctionDeligate(SecretFunction));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            timelog = new TimeLogManager(User.Identity.Name, "");
            btnEvaluate.Click += new EventHandler(btnEvaluate_Click);
            //btnDisplayParameterList.Click += new EventHandler(btnDisplayParameterList_Click);
            //btnDisplayDeviceDataParameterList.Click += new EventHandler(btnDisplayDeviceDataParameterList_Click);
            btnDisplayShowAttributeNamesList.Click += new EventHandler(btnDisplayShowAttributeNamesList_Click);
            //btnDisplayMasterOptionList.Click +=new EventHandler(btnDisplayMasterOptionList_Click);
            dtaLoad.Click += new EventHandler(dtaLoad_Click);
            //Button1.Click += new EventHandler(Button1_Click);
            btnEnglishVersion.Click += new EventHandler(btnTranslate_Click);
            btnTokenVersion.Click += new EventHandler(btnTranslate_Click);
            //btnLoadDeviceData.Click += new EventHandler(btnLoadDeviceData_Click);
            //ParameterList.Attributes.Add("ondblclick", "Javascript:InsertText(this);");
            lstDeviceData.Attributes.Add("ondblclick", "Javascript:InsertText(this);");
            AttributeNamesParameterList.Attributes.Add("ondblclick", "Javascript:InsertText(this);");
            MasterOptionList.Attributes.Add("ondblclick", "Javascript:InsertText(this);");
            btnTokenVersionWithData.Click += new EventHandler(btnTranslate_Click);

            if (!IsPostBack)
            {
                LoadProjectDropDown();
                LoadProcessDropDown();
            }
        }







        //void Button1_Click(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}


        void LoadProjectDropDown()
        {
            drpProject.Items.Clear();
            ProjectManager pm = new ProjectManager(User.Identity.Name);
            drpProject.DataValueField = "ProjectID";
            drpProject.DataTextField = "Name";
            //drpProject.DataTextField = "ProjectID";
            drpProject.DataSource = pm.GetMasterActiveProjectList();
            drpProject.DataBind();
            drpProject.SelectedIndex = 0;
        }
        void LoadProcessDropDown()
        {
            drpProcessList.Items.Clear();
            ProcessManager pm = new ProcessManager(User.Identity.Name);
            drpProcessList.DataValueField = "ProcessID";
            drpProcessList.DataTextField = "Name";
            //drpProcessList.DataTextField = "ProcessID";
            drpProcessList.DataSource = pm.GetMasterProcessList_Raw();
            drpProcessList.DataBind();
            drpProcessList.SelectedIndex = 0;
        }
        void dtaLoad_Click(object sender, EventArgs e)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                txtData.Text = "No data found";
                decimal ProcessID = -1;
                decimal.TryParse(drpProcessList.SelectedItem.Value, out ProcessID);
                var qry = from row in ctx.SystemTimeLogs
                          where row.RecordType == "WorkScreenSave" && row.ProcessID == ProcessID
                          select row;

                int count = qry.Count(); // 1st round-trip
                if (count > 0)
                {
                    int index = new Random().Next(count);
                    var dta = qry.Skip(index).FirstOrDefault(); // 2nd round-trip
                    if (dta != null)
                    {
                        txtData.Text = dta.RecordDetailString;
                    }
                }
            }
        }

        void btnDisplayShowAttributeNamesList_Click(object sender, EventArgs e)
        {
            List<string> Names = new List<string>();
            List<string> ScanKey = new List<string>();
            decimal projID = -1;
            decimal ProcID = -1;
            if (chkProjectOnly.Checked == true) { decimal.TryParse(drpProject.SelectedItem.Value, out projID); }
            if (chkProcessOnly.Checked == true) { decimal.TryParse(drpProcessList.SelectedItem.Value, out ProcID); }

            LoadBusinessRules();
            AttributeNamesParameterList.Items.Clear();
            MasterOptionList.Items.Clear();
            lstDeviceData.Items.Clear();


            foreach (JsonStringOptionKeyListByProjectProcess dta in br.QuestionData.OrderBy(x => x.Name).ThenBy(x => x.OptionValue))
            {
                //JsonStringOptionKeyListByProjectProcess dta = (JsonStringOptionKeyListByProjectProcess)br.MasterOptionData[key];
                ListItem l = new ListItem(dta.Name, dta.Name);
                ListItem lo = new ListItem(dta.Name + ":" + dta.OptionValue, dta.ScanKey);
                if (projID < 0 && ProcID < 0)
                {
                    if (ScanKey.Contains(dta.ScanKey) == false) { MasterOptionList.Items.Add(lo); ScanKey.Add(dta.ScanKey); }
                    if (Names.Contains(dta.Name) == false)
                    {
                        AttributeNamesParameterList.Items.Add(l); Names.Add(dta.Name);
                    }
                }
                if (projID < 0 && (ProcID > 0 && dta.ProcessID == ProcID))
                {
                    if (ScanKey.Contains(dta.ScanKey) == false) { MasterOptionList.Items.Add(lo); ScanKey.Add(dta.ScanKey); }
                    if (Names.Contains(dta.Name) == false)
                    {
                        AttributeNamesParameterList.Items.Add(l); Names.Add(dta.Name);
                    }
                }

                if (ProcID < 0 && (projID > 0 && dta.ProjectID == projID))
                {
                    if (ScanKey.Contains(dta.ScanKey) == false) { MasterOptionList.Items.Add(lo); ScanKey.Add(dta.ScanKey); }
                    if (Names.Contains(dta.Name) == false)
                    {
                        AttributeNamesParameterList.Items.Add(l); Names.Add(dta.Name);
                    }
                }
                if (ProcID > 0 && projID > 0 && ProcID == dta.ProcessID && dta.ProjectID == projID)
                {
                    if (ScanKey.Contains(dta.ScanKey) == false) { MasterOptionList.Items.Add(lo); ScanKey.Add(dta.ScanKey); }
                    if (Names.Contains(dta.Name) == false)
                    {
                        AttributeNamesParameterList.Items.Add(l); Names.Add(dta.Name);
                    }
                }
            }
            //SortListBox(AttributeNamesParameterList);
            //SortListBox(MasterOptionList);
            foreach (JsonStringOptionKeyListByProjectProcess dta in br.DeviceDataRecordMerged.OrderBy(x => x.Name))
            {
                ListItem l = new ListItem(dta.Name + " = " + dta.OptionValue + " (" + dta.jCoded + "/" + dta.ScanKey + ")", dta.Name + "] = [" + dta.ScanKey);
                if (projID < 0 && ProcID < 0) { lstDeviceData.Items.Add(l); }
                if (projID < 0 && (ProcID > 0 && dta.ProcessID == ProcID)) { lstDeviceData.Items.Add(l); }
                if (ProcID < 0 && (projID > 0 && dta.ProjectID == projID)) { lstDeviceData.Items.Add(l); }
                if (ProcID > 0 && projID > 0 && ProcID == dta.ProcessID && dta.ProjectID == projID) { lstDeviceData.Items.Add(l); }
            }


            //foreach (string key in br.ParametersDeviceData.Keys.OrderBy(x => x))
            //{
            //    JsonStringOptionKeyListByProjectProcess dta = (JsonStringOptionKeyListByProjectProcess)br.ParametersDeviceData[key];
            //    ListItem l = new ListItem(dta.Name + " = " + dta.OptionValue + " (" + dta.jCoded + "/" + dta.ScanKey + ")", dta.Name + "] = [" + dta.ScanKey );
            //    if (projID < 0 && ProcID < 0) { lstDeviceData.Items.Add(l); }
            //    if (projID < 0 && (ProcID > 0 && dta.ProcessID == ProcID)) { lstDeviceData.Items.Add(l); }
            //    if (ProcID < 0 && (projID > 0 && dta.ProjectID == projID)) { lstDeviceData.Items.Add(l); }
            //}
            ////SortListBox(lstDeviceData);



        }

        void btnTranslate_Click(object sender, EventArgs e)
        {
            LoadBusinessRules();
            txtEnglish.Text = br.CondtionEnglish;
            txtTokenWithData.Text = br.ConditionWithDataTokens;
            txtToken.Text = br.CondtionTokens;
        }

        object SecretFunction(FunctionArgs args)
        {
            return (int)args.Parameters[0].Evaluate() + (int)args.Parameters[1].Evaluate();
        }
        void btnEvaluate_Click(object sender, EventArgs e)
        {
            LoadBusinessRules();
            string Conditionformula = br.ConditionWithDataTokens;
            timelog.StartTimer();
            if (txtEquation.Text.Length == 0)
            {
                lblResult.Text = "No Equation Given";
            }
            lblResult.Text = string.Format("{1}:    {0}", Conditionformula, br.EvaluateCondition(Conditionformula));
            btnEvaluate.Text = string.Format("Evaluate {0}ms:", Math.Round(timelog.TimeInMilliSeconds(), MidpointRounding.AwayFromZero));
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



        private void SortListBox(ListBox lb1)
        {
            List<ListItem> t = new List<ListItem>();
            Comparison<ListItem> compare = new Comparison<ListItem>(CompareListItems);
            foreach (ListItem lbItem in lb1.Items)
                t.Add(lbItem);
            t.Sort(compare);
            lb1.Items.Clear();
            lb1.Items.AddRange(t.ToArray());
        }


        int CompareListItems(ListItem li1, ListItem li2)
        {
            return String.Compare(li1.Text, li2.Text);
        }





    }
}