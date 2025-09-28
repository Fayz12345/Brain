namespace BW_WebApp
{
    public static class Config
    {

        public static string ProjectStockForSaleFrom
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ProjectStockForSaleFrom"];
            }
        }
        public static string PartNumberGridUpdateMax
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["PartNumberGridUpdateMax"];
            }
        }
        public static string WriteLogUserNameToLog
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["WriteLogUserNameToLog"];
            }
        }
        public static string ClientAuthorizedESNEmailAddress
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ClientAuthorizedESNEmailAddress"];
            }
        }

    }
}