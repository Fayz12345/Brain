using System;
using System.Collections.Generic;
using System.Linq;

namespace BW_WebApp.DataManagers
{

    public class CustomReportManager : DataManagers
    {
        public CustomReportManager(string Username)
            : base(Username)
        {
        }

        public List<Template_DespatchNote> GetDespatchNoteList(string PField, string PSlip, ref string Message)
        {
            Message = "Error:";
            List<Template_DespatchNote> rdata = null;
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                rdata = ctx.Report_D_DespatchNote(PField, PSlip).ToList();
                if (rdata == null)
                {
                    return rdata;
                }
                Message = "Success";
                return rdata;
            }
        }

        public string GetPO_NoList(List<decimal> RDIDList)
        {
            List<string> POValues = new List<string>();
            string rvalue = "";
            string xValue = "";
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                int count = 0;
                foreach (decimal id in RDIDList.Take(25))
                {
                    xValue = ctx.GetReceivedQuestionAnswerString_03(id, "PO No");
                    if (xValue.Length > 0 && POValues.Contains(xValue) == false)
                    {
                        POValues.Add(xValue);
                    }
                    count++;
                    if (count == 1 && POValues.Count == 0)          // assume we have nothing.
                    {
                        break;
                    }
                    if (count > 5 && POValues.Count <= 1)          // assume we have nothing.
                    {
                        break;
                    }
                }
            }
            return string.Join(",", POValues);
        }

        public List<Template_Utility_AnalyzeData> GetAnalyseSystemResultList(ref string Message)
        {
            Message = "Error:";
            List<Template_Utility_AnalyzeData> rdata = null;
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                rdata = ctx.Utility_AnalyzeData(UserName).ToList();
                if (rdata == null)
                {
                    return rdata;
                }
                Message = "Success";
                return rdata;
            }
        }
        public void GetSiteCurrentAddress()
        {
        }


        public List<ViewWorkScreenSaveLog> GetWorkScreenSaveLogList(DateTime Start, DateTime EndDate, ref string Message)
        {
            Start = StartOfDay(Start);  // The final querry will look for anything >= this date.
            EndDate = StartOfDay(EndDate).AddDays(1);    // The final querry will look for anything < than this date
            Message = "Error:";
            List<ViewWorkScreenSaveLog> rdata = null;
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                rdata = ctx.ViewWorkScreenSaveLogs.Where(x => x.CreateDate >= Start && x.CreateDate < EndDate).ToList();
                if (rdata != null)
                {
                    Message = "Success";
                }
                return rdata;
            }
        }
        public List<ViewWorkScreenSaveLog> GetWorkScreenSaveLogList(string IMEI, ref string Message)
        {
            Message = "Error:";
            List<ViewWorkScreenSaveLog> rdata = null;
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                rdata = ctx.ViewWorkScreenSaveLogs.Where(x => x.ESN == IMEI).ToList();
                if (rdata != null)
                {
                    Message = "Success";
                }
                return rdata;
            }
        }
        public DateTime StartOfDay(DateTime Value)
        {
            string BeginDateString = string.Format("{0}", string.Format("{0:MM/dd/yyyy}", Value));
            DateTime.TryParse(BeginDateString, out Value);
            return Value;
        }

    }

}