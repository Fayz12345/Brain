using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using GMPDemo;
using System.Web.Security;
using System.Web.UI;
using BW_WebApp.DataManagers;

namespace BW_WebApp.DataManagers
{
    public class BL_UserDataRestrictor
    {
        string _UserName { get; set; }
        //public clsLinqDataContext ctx { get; set; }
        //Page P = (Page)HttpContext.Current.Handler;           // We have problems with this when we use BL_UserDataRestrictor called from web services (WebServer_01.svc)

        public BL_UserDataRestrictor(string UserName)
        {
            //this._UserName = "";
            this._UserName = UserName;
            //if (P.User.Identity.IsAuthenticated == true) { this._UserName = UserName; }
        }

        #region IOrderedQueryable<T>
        // Process
        public IQueryable<Process> GetRestricted(IQueryable<Process> process, clsLinqDataContext ctx)
        {
            BL_UserAccessControl uac = new BL_UserAccessControl(ctx, _UserName);
            if (uac.AllowGlobalSelect("Process") == true) { return process; }
            else
            {
                List<decimal> ValidClientList = uac.GetValidSelectIDList("Process");
                var c = from x in process where ValidClientList.Contains(x.ProcessID) select x;
                return c as IQueryable<Process>;
                //return c as IOrderedQueryable<Process>;
            }
        }
        public Process GetRestricted(Process process, clsLinqDataContext ctx)
        {
            if (process == null) { return process; }
            BL_UserAccessControl uac = new BL_UserAccessControl(ctx, _UserName);
            if (uac.AllowGlobalSelect("Process") != true)
            {
                List<decimal> ValidClientList = uac.GetValidSelectIDList("Process");
                if (ValidClientList.Contains(process.ProcessID) == false) { return null; }
            }
            return process;
        }

        // Project
        public IQueryable<Project> GetRestricted(IQueryable<Project> project, clsLinqDataContext ctx)
        {
            BL_UserAccessControl uac = new BL_UserAccessControl(ctx, _UserName);
            if (uac.AllowGlobalSelect("Project") == true) { return project; }
            else
            {
                List<decimal> ValidClientList = uac.GetValidSelectIDList("Project");
                var c = from x in project where ValidClientList.Contains(x.ProjectID) select x;
                return c as IQueryable<Project>;
            }
        }
        public IQueryable<ReceiveDetail> GetRestricted(IQueryable<ReceiveDetail> Receivedetails, clsLinqDataContext ctx)
        {


            BL_UserAccessControl uac = new BL_UserAccessControl(ctx, _UserName);
            if (uac.AllowGlobalSelect("Project") == true && uac.AllowGlobalSelect("Client") == true) { return Receivedetails; }
            else
            {
                List<decimal> ValidProjectList = uac.GetValidSelectIDList("Project");
                List<decimal> ValidClientList = uac.GetValidSelectIDList("Client");
                var c = from x in Receivedetails
                        where ValidProjectList.Contains((decimal)x.ProjectID) && ValidClientList.Contains((decimal)x.ClientLocationID)
                        select x;
                return c as IQueryable<ReceiveDetail>;
            }
        }




        public Project GetRestricted(Project project, clsLinqDataContext ctx)
        {
            if (project == null) { return project; }
            BL_UserAccessControl uac = new BL_UserAccessControl(ctx, _UserName);
            if (uac.AllowGlobalSelect("Project") != true)
            {
                List<decimal> ValidClientList = uac.GetValidSelectIDList("Project");
                if (ValidClientList.Contains(project.ProjectID) == false) { return null; }
            }
            return project;
        }
        // ReceiveDetail
        //public IQueryable<ReceiveDetail> GetRestricted(IQueryable<ReceiveDetail> Receivedetail, clsLinqDataContext ctx)
        //{
        //    BL_UserAccessControl uac = new BL_UserAccessControl(ctx, _UserName); 
        //    bool isClientOK = uac.AllowGlobalSelect("Client");
        //    bool isProjectOK = uac.AllowGlobalSelect("Project");
        //    if (isClientOK == true && isProjectOK) { return Receivedetail; }

        //    List<decimal> ValidClientList = new List<decimal>();
        //    List<decimal> ValidProjectList = new List<decimal>();

        //    //ReceiveDetail c = null;
        //    if (isClientOK != true) { ValidClientList = uac.GetValidSelectIDList("Client"); }
        //    if (isProjectOK != true) { ValidProjectList = uac.GetValidSelectIDList("Project"); }
        //    if (isClientOK != true && isProjectOK != true) { return (from x in Receivedetail where ValidClientList.Contains(x.ClientLocationID) && ValidProjectList.Contains((decimal)x.ProjectID) select x) as IOrderedQueryable<ReceiveDetail>; }
        //    if (isClientOK != true) { return (from x in Receivedetail where ValidClientList.Contains(x.ClientLocationID) select x) as IQueryable<ReceiveDetail>; }
        //    if (isProjectOK != true) { return (from x in Receivedetail where ValidProjectList.Contains((decimal)x.ProjectID) select x) as IQueryable<ReceiveDetail>; }
        //    return Receivedetail;

        //}
        public ReceiveDetail GetRestricted(ReceiveDetail Receivedetail, clsLinqDataContext ctx)
        {
            if (Receivedetail == null) { return Receivedetail; }

            BasicUserUtilities buu = new BasicUserUtilities(_UserName, _UserName);
            List<decimal> ValidClientList = buu.GetUserDefaultClientIDList(ctx, _UserName);
            if (ValidClientList.Count > 0 && ValidClientList.Contains(Receivedetail.ClientLocationID) == false) { return null; }

            //BL_UserAccessControl uac = new BL_UserAccessControl(ctx, _UserName);
            //if (uac.AllowGlobalSelect("Client") != true)
            //{
            //    List<decimal> ValidClientList = uac.GetValidSelectIDList("Client");
            //    if (ValidClientList.Contains(Receivedetail.ClientLocationID) == false) { return null; }
            //}

            BL_UserAccessControl uac = new BL_UserAccessControl(ctx, _UserName);
            if (uac.AllowGlobalSelect("Project") != true)
            {
                ValidClientList = uac.GetValidSelectIDList("Project");
                if (ValidClientList.Contains((decimal)Receivedetail.ProjectID) == false) { return null; }
            }
            return Receivedetail;
        }

        // Clients
        public IQueryable<Client> GetRestricted(IQueryable<Client> clients, clsLinqDataContext ctx)
        {
            BL_UserAccessControl uac = new BL_UserAccessControl(ctx, _UserName);
            if (uac.AllowGlobalSelect("Clientx") == true) { return clients; }
            else
            {
                List<decimal> ValidClientList = uac.GetValidSelectIDList("Clientx");
                var c = from x in clients where ValidClientList.Contains(x.ClientID) select x;
                return c as IQueryable<Client>;
            }
        }
        public Client GetRestricted(Client client, clsLinqDataContext ctx)
        {
            if (client == null) { return client; }
            BL_UserAccessControl uac = new BL_UserAccessControl(ctx, _UserName);
            if (uac.AllowGlobalSelect("Clientx") != true)
            {
                List<decimal> ValidClientList = uac.GetValidSelectIDList("Clientx");
                if (ValidClientList.Contains(client.ClientID) == false) { return null; }
            }
            return client;
        }

        // Client Locations
        public IQueryable<ClientLocation> GetRestricted(IQueryable<ClientLocation> clientLocations, clsLinqDataContext ctx)
        {
            //BL_UserAccessControl uac = new BL_UserAccessControl(ctx, _UserName);
            //if (uac.AllowGlobalSelect("Client") == true) { return clientLocations; }
            //else 
            //{
            BasicUserUtilities buu = new BasicUserUtilities(_UserName, _UserName);
            List<decimal> ValidClientLocationList = buu.GetUserDefaultClientIDList(ctx, _UserName);
            if (ValidClientLocationList.Count == 0) { return clientLocations; }

            var c = from x in clientLocations where ValidClientLocationList.Contains(x.ClientLocationID) select x;
            return c as IQueryable<ClientLocation>;
            //}
        }
        public ClientLocation GetRestricted(ClientLocation clientlocation, clsLinqDataContext ctx)
        {
            if (clientlocation == null) { return clientlocation; }

            BasicUserUtilities buu = new BasicUserUtilities(_UserName, _UserName);
            List<decimal> ValidClientList = buu.GetUserDefaultClientIDList(ctx, _UserName);
            // if (ValidClientList.Count == 0) { return clientlocation; }
            if (ValidClientList.Count > 0 && ValidClientList.Contains(clientlocation.ClientLocationID) == false) { return null; }
            return clientlocation;
        }

        // Answer
        public IQueryable<Option> GetRestricted(IQueryable<Option> option, clsLinqDataContext ctx)
        {
            BL_UserAccessControl uac = new BL_UserAccessControl(ctx, _UserName);
            if (uac.AllowGlobalSelect("Answer") == true) { return option; }
            else
            {
                List<decimal> ValidClientList = uac.GetValidSelectIDList("Answer");
                var c = from x in option where ValidClientList.Contains(x.OptionID) select x;
                return c as IQueryable<Option>;
            }
        }
        public Option GetRestricted(Option option, clsLinqDataContext ctx)
        {
            if (option == null) { return option; }
            BL_UserAccessControl uac = new BL_UserAccessControl(ctx, _UserName);
            if (uac.AllowGlobalSelect("Answer") != true)
            {
                List<decimal> ValidClientList = uac.GetValidSelectIDList("Answer");
                if (ValidClientList.Contains(option.OptionID) == false) { return null; }
            }
            return option;
        }

        // Question
        public IQueryable<Question> GetRestricted(IQueryable<Question> question, clsLinqDataContext ctx)
        {
            BL_UserAccessControl uac = new BL_UserAccessControl(ctx, _UserName);
            if (uac.AllowGlobalSelect("Question") == true) { return question; }
            else
            {
                List<decimal> ValidClientList = uac.GetValidSelectIDList("Question");
                var c = from x in question where ValidClientList.Contains(x.QuestionID) select x;
                return c as IQueryable<Question>;
            }
        }
        public Question GetRestricted(Question question, clsLinqDataContext ctx)
        {
            if (question == null) { return question; }
            BL_UserAccessControl uac = new BL_UserAccessControl(ctx, _UserName);
            if (uac.AllowGlobalSelect("Question") != true)
            {
                List<decimal> ValidClientList = uac.GetValidSelectIDList("Question");
                if (ValidClientList.Contains(question.QuestionID) == false) { return null; }
            }
            return question;
        }
        #endregion

        #region List<T>
        public List<Question> GetRestricted(List<Question> question, clsLinqDataContext ctx)
        {
            BL_UserAccessControl uac = new BL_UserAccessControl(ctx, _UserName);
            if (uac.AllowGlobalSelect("Question") == true) { return question; }
            else
            {
                List<decimal> ValidClientList = uac.GetValidSelectIDList("Question");
                var c = from x in question where ValidClientList.Contains(x.QuestionID) select x;
                return c.ToList();
            }
        }

        #endregion

        // So far, I don't see any need for AllowGlobalSelectFunctionExecute. 
        public bool AllowGlobalSelect(string Table, clsLinqDataContext ctx)
        {
            if (_UserName.Length == 0 || Table.Length == 0) { return false; }
            BL_UserAccessControl uac = new BL_UserAccessControl(ctx, _UserName);
            return uac.AllowGlobalSelect(Table);
        }
        public bool AllowGlobalAdd(string Table, clsLinqDataContext ctx)
        {
            if (_UserName.Length == 0 || Table.Length == 0) { return false; }
            BL_UserAccessControl uac = new BL_UserAccessControl(ctx, _UserName);
            return uac.AllowGlobalAdd(Table);
        }
        public bool AllowGlobalUpdate(string Table, clsLinqDataContext ctx)
        {
            if (_UserName.Length == 0 || Table.Length == 0) { return false; }
            BL_UserAccessControl uac = new BL_UserAccessControl(ctx, _UserName);
            return uac.AllowGlobalUpdate(Table);
        }
        public bool AllowGlobalDelete(string Table, clsLinqDataContext ctx)
        {
            if (_UserName.Length == 0 || Table.Length == 0) { return false; }
            BL_UserAccessControl uac = new BL_UserAccessControl(ctx, _UserName);
            return uac.AllowGlobalDelete(Table);
        }

        public bool AllowSelect(string Table, decimal ClientID, clsLinqDataContext ctx)
        {
            BL_UserAccessControl uac = new BL_UserAccessControl(ctx, _UserName);
            return uac.AllowSelect(Table, ClientID);
        }
        public bool AllowAdd(string Table, decimal ClientID, clsLinqDataContext ctx)
        {
            BL_UserAccessControl uac = new BL_UserAccessControl(ctx, _UserName);
            return uac.AllowAdd(Table, ClientID);
        }
        public bool AllowUpdate(string Table, decimal ClientID, clsLinqDataContext ctx)
        {
            BL_UserAccessControl uac = new BL_UserAccessControl(ctx, _UserName);
            return uac.AllowUpdate(Table, ClientID);
        }
        public bool AllowDelete(string Table, decimal ClientID, clsLinqDataContext ctx)
        {
            BL_UserAccessControl uac = new BL_UserAccessControl(ctx, _UserName);
            return uac.AllowDelete(Table, ClientID);
        }

        public bool AllowSelectFunctionExecute(string FunctionName, clsLinqDataContext ctx)
        {
            BL_UserAccessControl uac = new BL_UserAccessControl(ctx, _UserName);
            decimal ID = -1;
            // get the ID for the name of the function Name
            MasterFunctionTableManager mfm = new MasterFunctionTableManager(_UserName);
            ID = mfm.GetFunctionIDFromName(FunctionName);
            if (ID < 1) { return false; }
            return uac.AllowSelectFunctionExecute(ID);
        }

        public AccessControlIDList AccessControlValidIDList(string UserName, string Table, clsLinqDataContext ctx)
        {
            return new AccessControlIDList(UserName, Table, ctx);
        }

        public string RoleString()
        {
            string sRoles = "";
            string[] _Role = Roles.GetRolesForUser(_UserName);
            foreach (string x in _Role) { sRoles += x; }
            return sRoles;
        }

    }

    public class AccessControlIDList
    {
        string _UserName = "";
        string _TableName = "";
        bool _GlobalSelect = false;
        List<decimal> _IDs = new List<decimal>();

        public string UserName { get { return _UserName; } }
        public string TableName { get { return _TableName; } }
        public bool GlobalSelect { get { return _GlobalSelect; } }
        public List<decimal> IDs { get { return _IDs; } }

        public AccessControlIDList(string UserName, string Table, clsLinqDataContext ctx)
        {
            this._UserName = UserName;
            _TableName = Table;
            if (UserName.Length == 0 || Table.Length == 0) { return; }
            BL_UserAccessControl uac = new BL_UserAccessControl(ctx, _UserName);
            if (uac.AllowGlobalSelect(Table) == true) { _GlobalSelect = true; return; }
            _IDs = uac.GetValidSelectIDList(Table);
        }
    }

    public class BL_UserAccessControl
    {
        clsLinqDataContext ctx = null;
        RoleAccessControl rac = null;
        UserTable User = null;
        string _username = "";

        public BL_UserAccessControl(clsLinqDataContext ctx, string UserName)
        {
            this.ctx = ctx;
            _username = UserName;
            User = (from q in ctx.UserTables where q.UserName == _username select q).FirstOrDefault();
            rac = new RoleAccessControl(ctx, UserName);
        }

        public bool AllowGlobalFunctionExecute()
        {
            return AllowGlobalSelect("Function");
        }
        public bool AllowGlobalSelect(string TableName)
        {
            bool ReturnValue = false;
            if (User == null) { return ReturnValue; }

            UserAccessTable uat = (from x in ctx.UserAccessTables where (x.AllowTableRecordID == -1 && x.TableName == TableName && x.UserTable.UserName.ToUpper() == _username.ToUpper()) select x).FirstOrDefault();
            if (uat != null) { if (uat.AllowSelect) { return true; } }
            if (TableName.ToUpper() == "CLIENT") { return AllowGlobalSelect("Clientx"); }
            return rac.AllowGlobalSelect(TableName);
        }
        public bool AllowGlobalAdd(string TableName)
        {
            bool ReturnValue = false;
            if (User == null) { return ReturnValue; }

            UserAccessTable uat = (from x in ctx.UserAccessTables where (x.AllowTableRecordID == -1 && x.TableName == TableName && x.UserTable.UserName.ToUpper() == _username.ToUpper()) select x).FirstOrDefault();
            if (uat != null) { if (uat.AllowAdd) { return true; } }
            if (TableName.ToUpper() == "CLIENT") { return AllowGlobalAdd("Clientx"); }
            return rac.AllowGlobalAdd(TableName);
        }
        public bool AllowGlobalUpdate(string TableName)
        {
            bool ReturnValue = false;
            if (User == null) { return ReturnValue; }
            UserAccessTable uat = (from x in ctx.UserAccessTables where (x.AllowTableRecordID == -1 && x.TableName == TableName && x.UserTable.UserName.ToUpper() == _username.ToUpper()) select x).FirstOrDefault();
            if (uat != null) { if (uat.AllowUpdate) { return true; } }
            if (TableName.ToUpper() == "CLIENT") { return AllowGlobalUpdate("Clientx"); }
            return rac.AllowGlobalUpdate(TableName);
        }
        public bool AllowGlobalDelete(string TableName)
        {
            bool ReturnValue = false;
            if (User == null) { return ReturnValue; }

            UserAccessTable uat = (from x in ctx.UserAccessTables where (x.AllowTableRecordID == -1 && x.TableName == TableName && x.UserTable.UserName.ToUpper() == _username.ToUpper()) select x).FirstOrDefault();
            if (uat != null) { if (uat.AllowDelete) { return true; } }
            if (TableName.ToUpper() == "CLIENT") { return AllowGlobalDelete("Clientx"); }
            return rac.AllowGlobalDelete(TableName);
        }


        public bool AllowSelectFunctionExecute(decimal ID)
        {
            if (User == null) { return false; }
            if (AllowGlobalFunctionExecute() == true) { return true; }
            var uat = (from x in User.UserAccessTables
                       where x.AllowTableRecordID == ID
                          && x.TableName.ToUpper() == "FUNCTION"
                          && x.UserTable.UserName.ToUpper() == _username.ToUpper()
                          && x.AllowSelect == true
                       select x).FirstOrDefault();
            if (uat != null) { return true; }
            return rac.AllowSelectFunctionExecute(ID);
        }
        public bool AllowSelect(string TableName, decimal ID)
        {
            if (User == null) { return false; }
            if (AllowGlobalSelect(TableName) == true) { return true; }
            var uat = (from x in User.UserAccessTables
                       where x.AllowTableRecordID == ID
                          && x.TableName.ToUpper() == TableName.ToUpper()
                          && x.UserTable.UserName.ToUpper() == _username.ToUpper()
                          && x.AllowSelect == true
                       select x).FirstOrDefault();
            if (uat != null) { return true; }

            if (TableName.ToUpper() == "CLIENT")
            {
                // We now need to get the ClientID's and then convert those to the list of Client Locations to go out
                if (AllowGlobalSelect("CLIENTX") == true) { return true; }
                uat = (from x in User.UserAccessTables
                       where x.AllowTableRecordID == ID
                          && x.TableName.ToUpper() == "CLIENTX"
                          && x.UserTable.UserName.ToUpper() == _username.ToUpper()
                          && x.AllowSelect == true
                       select x).FirstOrDefault();
                if (uat != null) { return true; }

                BasicUserUtilities buu = new BasicUserUtilities(_username, _username);
                List<decimal> ValidClientList = buu.GetUserDefaultClientIDList(ctx, _username);
                if (ValidClientList.Contains(ID) == true) { return true; }

            }
            return rac.AllowSelect(TableName, ID);
        }
        public bool AllowAdd(string TableName, decimal ID)
        {
            if (User == null) { return false; }
            if (AllowGlobalAdd(TableName) == true) { return true; }
            var uat = (from x in User.UserAccessTables
                       where x.AllowTableRecordID == ID
                          && x.TableName.ToUpper() == TableName.ToUpper()
                          && x.UserTable.UserName.ToUpper() == _username.ToUpper()
                          && x.AllowAdd == true
                       select x).FirstOrDefault();
            if (uat != null) { return true; }

            if (TableName.ToUpper() == "CLIENT")
            {
                if (AllowGlobalAdd("CLIENTX") == true) { return true; }
                // We now need to get the ClientID's and then convert those to the list of Client Locations to go out
                uat = (from x in User.UserAccessTables
                       where x.AllowTableRecordID == ID
                          && x.TableName.ToUpper() == "CLIENTX"
                          && x.UserTable.UserName.ToUpper() == _username.ToUpper()
                          && x.AllowAdd == true
                       select x).FirstOrDefault();
                if (uat != null) { return true; }
            }
            return rac.AllowAdd(TableName, ID);
        }
        public bool AllowUpdate(string TableName, decimal ID)
        {
            if (User == null) { return false; }
            if (AllowGlobalUpdate(TableName) == true) { return true; }
            var uat = (from x in User.UserAccessTables
                       where x.AllowTableRecordID == ID
                          && x.TableName.ToUpper() == TableName.ToUpper()
                          && x.UserTable.UserName.ToUpper() == _username.ToUpper()
                          && x.AllowUpdate == true
                       select x).FirstOrDefault();
            if (uat != null) { return true; }

            if (TableName.ToUpper() == "CLIENT")
            {
                if (AllowGlobalUpdate("CLIENTX") == true) { return true; }
                // We now need to get the ClientID's and then convert those to the list of Client Locations to go out
                uat = (from x in User.UserAccessTables
                       where x.AllowTableRecordID == ID
                          && x.TableName.ToUpper() == "CLIENTX"
                          && x.UserTable.UserName.ToUpper() == _username.ToUpper()
                          && x.AllowUpdate == true
                       select x).FirstOrDefault();
                if (uat != null) { return true; }
            }
            return rac.AllowUpdate(TableName, ID);
        }
        public bool AllowDelete(string TableName, decimal ID)
        {
            if (User == null) { return false; }
            if (AllowGlobalDelete(TableName) == true) { return true; }
            var uat = (from x in User.UserAccessTables
                       where x.AllowTableRecordID == ID
                          && x.TableName.ToUpper() == TableName.ToUpper()
                          && x.UserTable.UserName.ToUpper() == _username.ToUpper()
                          && x.AllowDelete == true
                       select x).FirstOrDefault();
            if (uat != null) { return true; }

            if (TableName.ToUpper() == "CLIENT")
            {
                if (AllowGlobalDelete("CLIENTX") == true) { return true; }
                // We now need to get the ClientID's and then convert those to the list of Client Locations to go out
                uat = (from x in User.UserAccessTables
                       where x.AllowTableRecordID == ID
                          && x.TableName.ToUpper() == "CLIENTX"
                          && x.UserTable.UserName.ToUpper() == _username.ToUpper()
                          && x.AllowDelete == true
                       select x).FirstOrDefault();
                if (uat != null) { return true; }
            }
            return rac.AllowDelete(TableName, ID);
        }

        public List<decimal> GetValidSelectIDList(string TableName)
        {
            List<decimal> ReturnValue = new List<decimal>();
            if (User == null) { return ReturnValue; }

            var uat = from x in User.UserAccessTables
                      where x.AllowTableRecordID > 0
                         && x.TableName.ToUpper() == TableName.ToUpper()
                         && x.UserTable.UserName.ToUpper() == _username.ToUpper()
                         && x.AllowSelect == true
                      select x;

            foreach (var at in uat) { ReturnValue.Add(at.AllowTableRecordID); }

            // if Table name = Client (client Location) we need to also get any Master Clients (ClientX) as well
            if (TableName.ToUpper() == "CLIENT")
            {
                // We now need to get the ClientID's and then convert those to the list of Client Locations to go out
                uat = from x in User.UserAccessTables
                      where x.AllowTableRecordID > 0
                         && x.TableName.ToUpper() == "CLIENTX"
                         && x.UserTable.UserName.ToUpper() == _username.ToUpper()
                         && x.AllowSelect == true
                      select x;
                List<decimal> uatList = new List<decimal>();
                foreach (var at in uat) { uatList.Add(at.AllowTableRecordID); }
                var cl_loc = from x in ctx.ClientLocations where uatList.Contains(x.ClientID) select x;
                foreach (var cl in cl_loc) { ReturnValue.Add(cl.ClientLocationID); }

                BasicUserUtilities buu = new BasicUserUtilities(_username, _username);
                List<decimal> ValidClientList = buu.GetUserDefaultClientIDList(ctx, _username);
                foreach (var cl in ValidClientList) { ReturnValue.Add(cl); }
            }

            // Get any of the Roles ID fields and add them to the list.
            foreach (var ID in rac.GetValidSelectIDList(TableName)) { ReturnValue.Add(ID); }

            // Return the list.
            return ReturnValue.Distinct().ToList();
        }

        //public List<decimal> GetValidClientSelectIDList()
        //{
        //    List<decimal> ReturnValue = new List<decimal>();
        //    if (User == null) { return ReturnValue; }


        //    string TableName = "Client";

        //    var uat = from x in User.UserAccessTables
        //              where x.AllowTableRecordID > 0
        //                 && x.TableName.ToUpper() == TableName.ToUpper()
        //                 && x.UserTable.UserName.ToUpper() == _username.ToUpper()
        //                 && x.AllowSelect == true
        //              select x;
        //    foreach (var at in uat)
        //    {
        //        ReturnValue.Add(at.AllowTableRecordID);
        //    }
        //    #region Client
        //    if (TableName.ToUpper() == "CLIENTX")
        //    {
        //        uat = from x in User.UserAccessTables
        //              where x.AllowTableRecordID > 0
        //                 && x.TableName.ToUpper() == "CLIENTX"
        //                 && x.UserTable.UserName.ToUpper() == _username.ToUpper()
        //                 && x.AllowSelect == true
        //              select x;
        //        List<decimal> uatList = new List<decimal>();
        //        foreach (var at in uat)
        //        {
        //            ReturnValue.Add(at.AllowTableRecordID);
        //        }
        //    }
        //    #endregion
        //    #region Client Locations
        //    if (TableName.ToUpper() == "CLIENT")
        //    {
        //        uat = from x in User.UserAccessTables
        //              where x.AllowTableRecordID > 0
        //                 && x.TableName.ToUpper() == "CLIENTX"
        //                 && x.UserTable.UserName.ToUpper() == _username.ToUpper()
        //                 && x.AllowSelect == true
        //              select x;
        //        List<decimal> uatList = new List<decimal>();
        //        foreach (var at in uat)
        //        {
        //            uatList.Add(at.AllowTableRecordID);
        //        }

        //        var cl_loc = from x in ctx.ClientLocations where uatList.Contains(x.ClientID) select x;

        //        foreach (var cl in cl_loc)
        //        {
        //            ReturnValue.Add(cl.ClientID);
        //        }
        //    }
        //    #endregion

        //    var listDistinct = ReturnValue.GroupBy(i => i, (key, group) => group.First());
        //    return listDistinct.ToList();
        //}



        //public bool AllowSelect(string TableName, decimal ID)
        //{
        //    bool ReturnValue = false;
        //    if (_SelectListTableName != TableName)
        //    {
        //        UpdateSelectList(TableName);
        //    }
        //    if (_SelectListGlobal) { ReturnValue = true; }
        //    else if (_SelectListKeys.Contains(ID)) { ReturnValue = true; }
        //    return ReturnValue;
        //}
        //private void UpdateSelectList(string TableName)
        //{
        //    _SelectListTableName = TableName;
        //    _SelectListGlobal = false;
        //    _SelectListKeys.Clear();
        //    if (User != null)
        //    {
        //        foreach (UserAccessTable uat in User.UserAccessTables)
        //        {
        //            if (uat.TableName == TableName)
        //            {
        //                _SelectListKeys.Add(uat.AllowTableRecordID);
        //            }
        //        }
        //    }
        //}
        //public bool _IsClientLocationValid(decimal ClientLocationID)
        //{
        //    if (AllowGlobalSelect("Client") == true || AllowGlobalSelect("Clientx") == true)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        List<decimal> ValidList = GetValidSelectIDList("Client");
        //        if (ValidList.Contains(ClientLocationID) == true) { return true; }
        //        return false;
        //    }
        //}



        //public bool AllowAdd(string TableName)   // Add only has "Global"
        //{
        //    bool ReturnValue = false;
        //    if (User == null) { return ReturnValue; }

        //    UserAccessTable uat = (from x in User.UserAccessTables where (x.AllowTableRecordID == -1 && x.TableName == TableName && x.UserTable.UserName.ToUpper() == _username.ToUpper()) select x).FirstOrDefault();
        //    if (uat != null)
        //    {
        //        if (uat.AllowAdd)
        //        {
        //            ReturnValue = true;
        //        }
        //    }
        //    return ReturnValue;
        //}
        //public bool AllowUpdate(string TableName, decimal ID)
        //{
        //    bool ReturnValue = false;
        //    if (User == null) { return ReturnValue; }

        //    //UserTable User = (from q in ctx.UserTables where q.UserName == _username select q).FirstOrDefault();
        //    UserAccessTable uat = (from x in ctx.UserAccessTables where (x.AllowTableRecordID == ID && x.TableName == TableName && x.UserTable.UserName.ToUpper() == _username.ToUpper()) select x).FirstOrDefault();
        //    if (uat != null)
        //    {
        //        if (uat.AllowUpdate)
        //        {
        //            ReturnValue = true;
        //        }
        //    }
        //    else
        //    {
        //        // no specific designation, now look to see if there is a "global one"
        //        uat = (from x in ctx.UserAccessTables where (x.AllowTableRecordID == -1 && x.TableName == TableName && x.UserTable.UserName.ToUpper() == _username.ToUpper()) select x).FirstOrDefault();
        //        if (uat != null)
        //        {
        //            if (uat.AllowUpdate)
        //            {
        //                ReturnValue = true;
        //            }
        //        }
        //    }
        //    return ReturnValue;
        //}
        //public bool AllowDelete(string TableName, decimal ID)
        //{
        //    bool ReturnValue = false;
        //    if (User == null) { return ReturnValue; }

        //    // look first to see if there is a specific designation for the record.
        //    UserAccessTable uat = (from x in User.UserAccessTables where (x.AllowTableRecordID == ID && x.TableName == TableName && x.UserTable.UserName.ToUpper() == _username.ToUpper()) select x).FirstOrDefault();
        //    if (uat != null)
        //    {
        //        if (uat.AllowDelete)
        //        {
        //            ReturnValue = true;
        //        }
        //    }
        //    else
        //    {
        //        // no specific designation, now look to see if there is a "global one"
        //        uat = (from x in User.UserAccessTables where (x.AllowTableRecordID == -1 && x.TableName == TableName && x.UserTable.UserName.ToUpper() == _username.ToUpper()) select x).FirstOrDefault();
        //        if (uat != null)
        //        {
        //            if (uat.AllowDelete)
        //            {
        //                ReturnValue = true;
        //            }
        //        }
        //    }
        //    return ReturnValue;
        //}
    }
    public class RoleAccessControl
    {
        clsLinqDataContext ctx = null;
        List<string> _Roles = new List<string>();
        string _username = "";

        public RoleAccessControl(clsLinqDataContext ctx, string UserName)
        {
            this.ctx = ctx;
            _username = UserName;
            string[] _Role = Roles.GetRolesForUser(UserName);
            foreach (string x in _Role) { _Roles.Add(x); }
        }


        public bool AllowGlobalSelect(string TableName)
        {
            foreach (string r in _Roles)
            {
                RoleAccessTable rat = (from x in ctx.RoleAccessTables
                                       where (x.AllowTableRecordID == -1 &&
                                              x.TableName == TableName &&
                                              x.Role.ToUpper() == r.ToUpper() &&
                                              x.AllowSelect == true)
                                       select x).FirstOrDefault();
                if (rat != null) { return true; }
            }
            return false;
        }
        public bool AllowGlobalAdd(string TableName)
        {
            foreach (string r in _Roles)
            {
                RoleAccessTable rat = (from x in ctx.RoleAccessTables
                                       where (x.AllowTableRecordID == -1 &&
                                              x.TableName == TableName &&
                                              x.Role.ToUpper() == r.ToUpper() &&
                                              x.AllowAdd == true)
                                       select x).FirstOrDefault();
                if (rat != null) { return true; }
            }
            return false;
        }
        public bool AllowGlobalUpdate(string TableName)
        {
            foreach (string r in _Roles)
            {
                RoleAccessTable rat = (from x in ctx.RoleAccessTables
                                       where (x.AllowTableRecordID == -1 &&
                                              x.TableName == TableName &&
                                              x.Role.ToUpper() == r.ToUpper() &&
                                              x.AllowUpdate == true)
                                       select x).FirstOrDefault();
                if (rat != null) { return true; }
            }
            return false;
        }
        public bool AllowGlobalDelete(string TableName)
        {
            foreach (string r in _Roles)
            {
                RoleAccessTable rat = (from x in ctx.RoleAccessTables
                                       where (x.AllowTableRecordID == -1 &&
                                              x.TableName == TableName &&
                                              x.Role.ToUpper() == r.ToUpper() &&
                                              x.AllowDelete == true)
                                       select x).FirstOrDefault();
                if (rat != null) { return true; }
            }
            return false;
        }


        public bool AllowSelectFunctionExecute(decimal ID)
        {
            foreach (string r in _Roles)
            {
                RoleAccessTable rat = (from x in ctx.RoleAccessTables
                                       where (x.AllowTableRecordID == ID &&
                                              x.TableName.ToUpper() == "FUNCTION" &&
                                              x.Role.ToUpper() == r.ToUpper() &&
                                              x.AllowSelect == true)
                                       select x).FirstOrDefault();
                if (rat != null) { return true; }
            }
            return false;
        }
        public bool AllowSelect(string TableName, decimal ID)
        {
            if (AllowGlobalSelect(TableName) == true) { return true; }

            foreach (string r in _Roles)
            {
                RoleAccessTable rat = (from x in ctx.RoleAccessTables
                                       where (x.AllowTableRecordID == ID &&
                                              x.TableName == TableName &&
                                              x.Role.ToUpper() == r.ToUpper() &&
                                              x.AllowSelect == true)
                                       select x).FirstOrDefault();
                if (rat != null) { return true; }
            }
            return false;
        }
        public bool AllowAdd(string TableName, decimal ID)
        {
            if (AllowGlobalAdd(TableName) == true) { return true; }

            foreach (string r in _Roles)
            {
                RoleAccessTable rat = (from x in ctx.RoleAccessTables
                                       where (x.AllowTableRecordID == ID &&
                                              x.TableName == TableName &&
                                              x.Role.ToUpper() == r.ToUpper() &&
                                              x.AllowAdd == true)
                                       select x).FirstOrDefault();
                if (rat != null) { return true; }
            }
            return false;
        }
        public bool AllowUpdate(string TableName, decimal ID)
        {
            if (AllowGlobalUpdate(TableName) == true) { return true; }

            foreach (string r in _Roles)
            {
                RoleAccessTable rat = (from x in ctx.RoleAccessTables
                                       where (x.AllowTableRecordID == ID &&
                                              x.TableName == TableName &&
                                              x.Role.ToUpper() == r.ToUpper() &&
                                              x.AllowUpdate == true)
                                       select x).FirstOrDefault();
                if (rat != null) { return true; }
            }
            return false;
        }
        public bool AllowDelete(string TableName, decimal ID)
        {
            if (AllowGlobalDelete(TableName) == true) { return true; }

            foreach (string r in _Roles)
            {
                RoleAccessTable rat = (from x in ctx.RoleAccessTables
                                       where (x.AllowTableRecordID == ID &&
                                              x.TableName == TableName &&
                                              x.Role.ToUpper() == r.ToUpper() &&
                                              x.AllowDelete == true)
                                       select x).FirstOrDefault();
                if (rat != null) { return true; }
            }
            return false;
        }

        public List<decimal> GetValidSelectIDList(string TableName)
        {
            List<decimal> ReturnValue = new List<decimal>();
            if (_Roles.Count == 0) { return ReturnValue; }
            var rat = from x in ctx.RoleAccessTables
                      where (x.AllowTableRecordID > 0 &&
                          x.TableName == TableName &&
                          _Roles.Contains(x.Role) &&
                          x.AllowSelect == true)
                      select x;

            foreach (var r in rat) { ReturnValue.Add(r.AllowTableRecordID); }

            // if Table name = Client (client Location) we need to also get any Master Clients (ClientX) as well
            if (TableName.ToUpper() == "CLIENT")
            {
                // We now need to get the ClientID's and then convert those to the list of Client Locations to go out
                var rat2 = from x in ctx.RoleAccessTables
                           where (x.AllowTableRecordID > 0 &&
                               x.TableName == "Clientx" &&
                               _Roles.Contains(x.Role) &&
                               x.AllowSelect == true)
                           select x;

                List<decimal> uatList = new List<decimal>();
                foreach (var at in rat2) { uatList.Add(at.AllowTableRecordID); }
                var cl_loc = from x in ctx.ClientLocations where uatList.Contains(x.ClientID) select x;
                foreach (var cl in cl_loc) { ReturnValue.Add(cl.ClientLocationID); }

                BasicUserUtilities buu = new BasicUserUtilities(_username, _username);
                List<decimal> ValidClientList = buu.GetUserDefaultClientIDList(ctx, _username);
                foreach (var cl in ValidClientList) { ReturnValue.Add(cl); }
            }
            return ReturnValue.Distinct().ToList();
        }

        public List<MasterTableAcccessList> GetRoleAccessList(string TableName)
        {
            var rat = from x in ctx.RoleAccessTables
                      where (x.TableName == TableName &&
                          _Roles.Contains(x.Role))
                      select new MasterTableAcccessList()
                      {
                          AllowAdd = x.AllowAdd,
                          AllowDelete = x.AllowDelete,
                          AllowScan = x.AllowScan,
                          AllowSelect = x.AllowSelect,
                          AllowUpdate = x.AllowUpdate,
                          ID = (x.AllowTableRecordID == -1 ? -20 : x.AllowTableRecordID),
                          Name = (x.AllowTableRecordID == -1 ? "Global (" + x.Role + ") *" : x.Role),
                          Sequence = (x.AllowTableRecordID == -1 ? -9 : 0),
                          TableName = TableName
                      };

            List<MasterTableAcccessList> b = rat.ToList();

            #region Requestion Locations (Convert Client Data to Location Data)
            // If this is a Client request (Client Locations), we have to get the "ClientX" (Master) report and convert it from Client to Locations.
            if (TableName.ToUpper() == "CLIENT")
            {
                rat = from x in ctx.RoleAccessTables
                      where (x.TableName == "CLIENTX" &&
                             _Roles.Contains(x.Role))
                      select new MasterTableAcccessList()
                      {
                          AllowAdd = x.AllowAdd,
                          AllowDelete = x.AllowDelete,
                          AllowScan = x.AllowScan,
                          AllowSelect = x.AllowSelect,
                          AllowUpdate = x.AllowUpdate,
                          ID = (x.AllowTableRecordID == -1 ? -20 : x.AllowTableRecordID),
                          Name = (x.AllowTableRecordID == -1 ? "Global (" + x.Role + ") *" : x.Role),
                          Sequence = (x.AllowTableRecordID == -1 ? -9 : 0),
                          TableName = "CLIENTX"
                      };


                List<MasterTableAcccessList> cl = null;
                foreach (MasterTableAcccessList m in rat.Where(x => x.ID > 0).ToList())
                {
                    cl = (from x in ctx.ClientLocations
                          where (x.ClientID == m.ID)
                          select new MasterTableAcccessList()
                          {
                              AllowAdd = m.AllowAdd,
                              AllowDelete = m.AllowDelete,
                              AllowScan = m.AllowScan,
                              AllowSelect = m.AllowSelect,
                              AllowUpdate = m.AllowUpdate,
                              ID = x.ClientLocationID,
                              Name = m.Name,
                              Sequence = x.Sequence,
                              TableName = TableName
                          }).ToList();
                    b = b.Union(cl).ToList();
                }
            }
            b = b.Distinct().ToList();
            #endregion
            //----------------------------------------------------------------------------------------------------------
            if (TableName.ToUpper() == "PROJECT")
            {
                ProjectManager manager = new ProjectManager(_username);
                foreach (MasterTableAcccessList x in b.Where(d => d.ID > 0)) { x.Name = manager.Description(ctx, x.ID) + "(" + x.Name + ") *"; x.ID = -20; }
            }
            if (TableName.ToUpper() == "PROCESS")
            {
                ProcessManager manager = new ProcessManager(_username);
                foreach (MasterTableAcccessList x in b.Where(d => d.ID > 0)) { x.Name = manager.Description(ctx, x.ID) + "(" + x.Name + ") *"; x.ID = -20; }
            }

            if (TableName.ToUpper() == "CLIENTX")
            {
                ClientManager manager = new ClientManager(_username);
                foreach (MasterTableAcccessList x in b.Where(d => d.ID > 0)) { x.Name = manager.Description(ctx, x.ID) + "(" + x.Name + ") *"; x.ID = -20; }
            }
            if (TableName.ToUpper() == "CLIENT")
            {
                ClientLocationManager manager = new ClientLocationManager(_username);
                foreach (MasterTableAcccessList x in b.Where(d => d.ID > 0)) { x.Name = manager.Description(ctx, x.ID) + "(" + x.Name + ") *"; x.ID = -20; }
            }
            if (TableName.ToUpper() == "QUESTION")
            {
                QuestionManager manager = new QuestionManager(_username);
                foreach (MasterTableAcccessList x in b.Where(d => d.ID > 0)) { x.Name = manager.Description(ctx, x.ID) + "(" + x.Name + ") *"; x.ID = -20; }
            }

            if (TableName.ToUpper() == "FUNCTION")
            {
                MasterFunctionTableManager manager = new MasterFunctionTableManager(_username);
                foreach (MasterTableAcccessList x in b.Where(d => d.ID > 0)) { x.Name = manager.Description(ctx, x.ID) + "(" + x.Name + ") *"; x.ID = -20; }
            }



            return b;
        }

    }


}