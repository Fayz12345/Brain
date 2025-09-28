using System.IO;







//using System.Text;
//using System.Collections.Specialized;
//using System.Net;
//using System.IO;
//using System.Net.Http;
//using System.Collections.Generic;

namespace BW_WebApp
{
    public static class StringUtilities
    {
        public static Stream ToStream(this string str)
        {
            //new MemoryStream(Encoding.UTF8.GetBytes(x ?? ""))
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(str);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static string ToSingleQuote(this string str)
        {
            return str.Replace("\"", "'");
        }
        public static string ToDoubleQuote(this string str)
        {
            return str.Replace("'", "\"");
        }
    }


    public class SysUtil
    {



        public static int ReportTimeOutInSeconds()
        {
            string Temp = "";
            int Min = 0;
            int Sec = 0;
            Temp = System.Configuration.ConfigurationManager.AppSettings["ReportTimeOutInMinutes"];
            if (int.TryParse(Temp, out Min) == false) { Min = 6; }
            Sec = Min * 60;
            return Sec;
        }
        public static bool CellbieAPISimulate()
        {
            return (System.Configuration.ConfigurationManager.AppSettings["CellbieAPISimulate"] == "true");
        }
        public static string CellbieAPIToken()
        {
            return System.Configuration.ConfigurationManager.AppSettings["CellbieAPIToken"];
        }
        public static string CellbieAPITokenHidden()
        {
            return "HiddenForSecurity";
            //return System.Configuration.ConfigurationManager.AppSettings["CellbieAPIToken"];
        }
        public static string CellbieAPIDomain()
        {
            return System.Configuration.ConfigurationManager.AppSettings["CellbieAPIDomain"];
        }

        public static string TempDirectory()
        {
            return System.Configuration.ConfigurationManager.AppSettings["TempDirectory"];
        }
        public static string AssignedClientCode()
        {
            return System.Configuration.ConfigurationManager.AppSettings["AssignedClientCode"];
        }
        public static string ClientName()
        {
            return System.Configuration.ConfigurationManager.AppSettings["ClientName"];
        }
        public static string WriteLog()
        {
            return System.Configuration.ConfigurationManager.AppSettings["WriteLog"];
        }
        public static string WriteWorkScreenSaveTimeLog()
        {
            return System.Configuration.ConfigurationManager.AppSettings["WriteWorkScreenSaveTimeLog"];
        }
        public static string WritePreWorkScreenSaveTimeLog()
        {
            return System.Configuration.ConfigurationManager.AppSettings["WritePreWorkScreenSaveTimeLog"];
        }
        public static string APILogPath()
        {
            return System.Configuration.ConfigurationManager.AppSettings["APILogPath"];
        }
        public static string WriteAssignPartTimeLog()
        {
            return System.Configuration.ConfigurationManager.AppSettings["WriteAssignPartTimeLog"];
        }
        public static string WriteIMEIExcelUploadLog()
        {
            return System.Configuration.ConfigurationManager.AppSettings["WriteIMEIExcelUploadLog"];
        }
        public static string TestParseSave()
        {
            return System.Configuration.ConfigurationManager.AppSettings["TestParseSave"];
        }
        public static string WriteLogUserNameToLog()
        {
            return System.Configuration.ConfigurationManager.AppSettings["WriteLogUserNameToLog"];
        }
        public static string SMTPHost()
        {
            return System.Configuration.ConfigurationManager.AppSettings["SMTPHost"];
        }
        public static string ClientAuthorizedESNEmailAddress()
        {
            return System.Configuration.ConfigurationManager.AppSettings["ClientAuthorizedESNEmailAddress"];
        }
        public static string PMI_EDI_Directory()
        {
            return System.Configuration.ConfigurationManager.AppSettings["PMI_EDI_Directory"];
        }
        public static string PartNumberGridUpdateMax()
        {
            return System.Configuration.ConfigurationManager.AppSettings["PartNumberGridUpdateMax"];
        }
        public static string ProjectStockForSaleFrom()
        {
            return System.Configuration.ConfigurationManager.AppSettings["ProjectStockForSaleFrom"];
        }
        public static string LogoPath()
        {
            return System.Configuration.ConfigurationManager.AppSettings["LogoPath"];
        }
    }
}