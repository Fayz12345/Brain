//using System.Web.Script.Serialization;            // Used to generate json for back and forth
using System.Linq;
using System.Text;
//using Factory_DataModel;
using BW_WebApp.DataManagers;



namespace BW_IntegrationManager_PublicMobile
{
    public class IntegrationManager_PublicMobile
    {

        string _UserName = "";

        public IntegrationManager_PublicMobile(string UserName)
        {
            _UserName = UserName;
        }


        #region File01
        public void ReadFile01(string FileName)
        {
            int counter = 0;
            string line;

            // Read the file and display it line by line.
            System.IO.StreamReader file =
                new System.IO.StreamReader(FileName);
            while ((line = file.ReadLine()) != null)
            {
                System.Console.WriteLine(line);
                counter++;
            }

            file.Close();
            System.Console.WriteLine("There were {0} lines.", counter);
            // Suspend the screen.
            System.Console.ReadLine();
        }

        public string GenerateFile01(string fName)
        {
            StringBuilder file = new StringBuilder();
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                ctx.UserName = _UserName;

                var data = from r in ctx.ReceiveDetails
                           where r.ClientLocation.Client.CompanyName.ToUpper() == "xxxx" && r.Version == "000"
                           select r;
                foreach (ReceiveDetail d in data.OrderBy(x => x.ReceiveDate))
                {
                    file.AppendLine(CreateFile01File(d));
                }

            }
            // Example #2: Write one string to a text file.
            System.IO.File.WriteAllText(fName, file.ToString());
            return file.ToString();
        }

        private string CreateFile01File(ReceiveDetail d)
        {
            StringBuilder line = new StringBuilder();
            line.Append(d.ReceiveDate.ToString("yyyy-MM-dd HH:mm:ss"));
            line.Append(";");
            line.Append(d.ClientLocation.ScanKey);
            line.Append(";");
            line.Append("WO1002");
            line.Append(";");
            line.Append(d.RMANumber);
            line.Append(";");
            line.Append("SKU");
            line.Append(";");
            line.Append("1");
            line.Append(";");
            line.Append("WAC");
            line.Append(";");
            line.Append(d.ESN);

            return line.ToString();
        }
        #endregion
    }
}