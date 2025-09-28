using System;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.IO;
//using System.Diagnostics;


//using GMPDemo;
//using BusinessLayer;
//using Factory_DataModel;
using BW_WebApp.Classes;

namespace BW_WebApp.DataManagers
{
    public class ReportUtility
    {
        public ReportUtility()
        {

        }
        #region Numeric Helpers
        public List<string> ListALLNumericQuestionNames()
        {
            return GetListALLNumericQuestionNames().Concat(ListManualNumericNames()).Distinct().ToList();
        }

        public List<string> ListCALCQuestionNames()
        {
            return GetListCALCQuestionNames();
        }
        public List<string> ListNUMERICQuestionNames()
        {
            return GetListNUMERICQuestionNames();
        }
        public List<string> ListNUM3DIGITQuestionNames()
        {
            return GetListNUM3DIGITQuestionNames();
        }
        public List<string> ListCURRENCYQuestionNames()
        {
            return GetListCURRENCYQuestionNames();
        }





        private List<string> GetListALLNumericQuestionNames()
        {
            List<string> qTypes = new List<string>();
            qTypes.Add("CALC");
            qTypes.Add("NUMERIC");
            qTypes.Add("NUM3DIGIT");
            qTypes.Add("CURRENCY");
            return GetQuestionNamesForThisType(qTypes);
        }
        private List<string> GetListCALCQuestionNames()
        {
            List<string> qTypes = new List<string>();
            qTypes.Add("CALC");
            return GetQuestionNamesForThisType(qTypes);
        }
        private List<string> GetListNUMERICQuestionNames()
        {
            List<string> qTypes = new List<string>();
            qTypes.Add("NUMERIC");
            return GetQuestionNamesForThisType(qTypes);
        }
        private List<string> GetListNUM3DIGITQuestionNames()
        {
            List<string> qTypes = new List<string>();
            qTypes.Add("NUM3DIGIT");
            return GetQuestionNamesForThisType(qTypes);
        }
        private List<string> GetListCURRENCYQuestionNames()
        {
            List<string> qTypes = new List<string>();
            qTypes.Add("CURRENCY");
            return GetQuestionNamesForThisType(qTypes);
        }


        // Want to phase this out... should not have to specifically name them
        private List<string> ListManualNumericNames()
        {
            List<string> qTypes = new List<string>
                                 {"TRANSACTIONQTY","TRANSACTIONUNITPRICE","QUANTITY","UNITPRICE",
                                  "MONTHENDQTY", "MONTHENDUNITPRICE", 
                                  "ORIGINAL COST","SELLING PRICE","INTERNAL COST", "DEVICECOST",
                                  "MOVEDOUT","MOVEDIN","STARTING","ENDING","FREQIN","FREQOUT",
                                  "REPAIR FEE", "REPAIR_FEE", "UNITPRICE01", "UNITPRICE02", "UNITPRICE03", "UNITPRICE04", 
                                  "UNITPRICE05", "UNITPRICE06", "UNITPRICE07",
                                  "UNITPRICE08", "UNITPRICE09", "UNITPRICE10", "TOTALUNITPRICE", "HOLDQUANTITY",
                                  "REPAIR CAP", "SAVETIMEMS", 
                                  "INPROCESSSECONDS","INPROCESSMINUTES","INPROCESSHOURS","MINUTESTOYELLOW","MINUTESTORED"};
            return qTypes;
        }
        #endregion
        #region Date Helpers
        public List<string> ListDateQuestionNames()
        {
            return GetListDateQuestionNames().Concat(ListManualDateNames()).Distinct().ToList();
        }
        private List<string> GetListDateQuestionNames()
        {
            List<string> qTypes = new List<string>();
            qTypes.Add("CALENDAR");
            return GetQuestionNamesForThisType(qTypes);
        }
        private List<string> ListManualDateNames()
        {
            List<string> qTypes = new List<string>
                                 {"LASTUPDATEDATE", "MONTHENDDATE", "CREATEDATE", "RECEIVEDATE", "MOVEDDATE", "REPORTBEGINDATE", "REPORTENDDATE", 
                                  "DATEMOVED", "DATEMOVEDOUT", "BININDATE", "BINOUTDATE", "ATTEMPTDATE", "ATTEMPTDATE2", "ATTEMPTDATE3", "ATTEMPTDATED", 
                                  "ATTEMPTDATE2D", "ATTEMPTDATE3D", "LASTUPDATEDATED", "MONTHENDDATED", "STARTTIMEDATE", "ENDTIMEDATE", "CREATEDATED" };
            return qTypes;
        }
        #endregion
        #region Helpers
        private static List<string> GetQuestionNamesForThisType(List<string> qTypes)
        {
            List<string> Fields = new List<string>();
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {

                var qs = ctx.Questions.Where(x => qTypes.Contains(x.QuestionType.Type.ToUpper())).ToList();
                foreach (Question q in qs)
                {
                    Fields.Add(q.Name.ToUpper());
                }
            }
            return Fields;
        }
        #endregion

    }
}