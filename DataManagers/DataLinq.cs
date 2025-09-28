using System;
using System.Linq;
//using BW_WebApp.DataManagers;

namespace BW_WebApp.DataManagers
{
    partial class clsLinqDataContext
    {
        private string _AuthenticationName = string.Empty;
        public string UserName
        {
            get { return _AuthenticationName; }
            set { _AuthenticationName = value; }
        }



        public string HelperAddAttributeValue(string QuestionName, string AttributeScanKey, string Abbr, string Value, string Seq, string UserName)
        {
            string message = "";
            Utility_LoadAttributeValue_04(QuestionName, AttributeScanKey, Abbr, Value, Seq, UserName, ref message);
            return message;
        }


        public string GETUnitData(string ESN, string Version, string UserName, string DataRequested)
        {
            string rString = "";
            string[] rData = DataRequested.ToUpper().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            var data = Querry_UnitData_ESN(ESN, Version.Trim());
            foreach (UnitData d in data)
            {
                if (rData.Count() == 0)
                {
                    rString += "/" + d.Name + ":" + d.Desc;
                }
                else
                {
                    if (rData.Contains(d.Name.ToUpper()) == true) { rString += "/" + d.Name + ":" + d.Desc; }
                }
            }
            return rString;
        }






        public void Util_RebuildReceiveDetailHeaderAttributes()
        {
            Utility_RebuildReceiveDetailHeaderAttributes();
        }


        public void AdvanceESNVersion_List(string ESNList, decimal increment, string username)
        {
            string[] ESNs = ESNList.Split(',');
            string EList = "";
            foreach (string ESN in ESNs)
            {
                if (EList.Length > 0) { EList += ","; }
                EList += ESN;
                if (EList.Length > 7500)
                {
                    AdvanceESNVersion_03(EList, increment, username);
                    EList = "";
                }
            }
            if (EList.Length > 0) { AdvanceESNVersion_03(EList, increment, username); }
        }


        public string GetReceiveDetailItem_DataElement(string ESN, string FieldName)
        {

            ReceiveDetail rdl = (from x in ReceiveDetails
                                 where x.ESN == ESN && x.ReceiveDetailStatus.Status.ToUpper() != "GRAVEYARD"
                                 orderby x.Version
                                 select x).FirstOrDefault();

            if (rdl != null)
            {
                return GetReceiveDetailItem_DataElement(rdl.ReceiveDetailID, FieldName);
            }
            return "";
        }
        public string GetReceiveDetailItem_DataElement(decimal ReceiveDetailID, string FieldName)
        {

            ReceiveDetailItem rd = ReceiveDetailItems.FirstOrDefault(x => x.ReceiveDetailID == ReceiveDetailID
                                                                       && x.Option.Question.Name.ToUpper() == FieldName.ToUpper()
                                                                       && x.Version == 0
                                                                       &&
                                                                    (
                                                                      ((x.Option.Question.QuestionType.Type.ToUpper() == "CHECKBOX" || x.Option.Question.QuestionType.Type.ToUpper() == "RADIALBUTTON" || x.Option.Question.QuestionType.Type.ToUpper() == "DROPDOWN") && x.Value == "1")
                                                                      ||
                                                                       (x.Option.Question.QuestionType.Type.ToUpper() != "CHECKBOX" && x.Option.Question.QuestionType.Type.ToUpper() != "RADIALBUTTON" && x.Option.Question.QuestionType.Type.ToUpper() != "DROPDOWN")
                                                                    ));
            if (rd != null)
            {
                if (rd.Option.Question.QuestionType.Type.ToUpper() == "DROPDOWN")
                {
                    return rd.Option.OptionText;
                }
                else if (rd.Option.Question.QuestionType.Type.ToUpper() == "CHECKBOX" ||
                    rd.Option.Question.QuestionType.Type.ToUpper() == "RADIALBUTTON")
                {
                    return GetReceiveDetailItem_DataElement_CheckDrop(ReceiveDetailID, FieldName);
                }
                else      // All others, Keyboard, Caledar, Cacl
                {
                    return rd.Value;
                }
            }
            return "";
        }
        public string GetReceiveDetailItem_DataElement_CheckDrop(decimal ReceiveDetailID, string FieldName)
        {
            string rString = "";
            var data = from x in ReceiveDetailItems
                       where x.ReceiveDetailID == ReceiveDetailID && x.Option.Question.Name.ToUpper() == FieldName.ToUpper() && x.Version == 0
                                                                       && (x.Option.Question.QuestionType.Type.ToUpper() == "CHECKBOX" || x.Option.Question.QuestionType.Type.ToUpper() == "RADIALBUTTON")
                       select x;
            foreach (var x in data)
            {
                rString += rString.Length > 0 ? "/" : "" + x.Option.OptionText;
            }
            return rString;
        }

        //----------------------------------------
        public string GetReceiveDetailItem_DataElementName(string ESN, string FieldName)
        {

            ReceiveDetail rdl = (from x in ReceiveDetails
                                 where x.ESN == ESN && x.ReceiveDetailStatus.Status.ToUpper() != "GRAVEYARD"
                                 orderby x.Version
                                 select x).FirstOrDefault();

            if (rdl != null)
            {
                return GetReceiveDetailItem_DataElementName(rdl.ReceiveDetailID, FieldName);
            }
            return "";
        }
        public string GetReceiveDetailItem_DataElementName(decimal ReceiveDetailID, string FieldName)
        {

            ReceiveDetailItem rd = ReceiveDetailItems.FirstOrDefault(x => x.ReceiveDetailID == ReceiveDetailID
                                                                       && x.Option.Question.Name.ToUpper() == FieldName.ToUpper()
                                                                       && x.Version == 0
                                                                       &&
                                                                    (
                                                                      ((x.Option.Question.QuestionType.Type.ToUpper() == "CHECKBOX" || x.Option.Question.QuestionType.Type.ToUpper() == "RADIALBUTTON" || x.Option.Question.QuestionType.Type.ToUpper() == "DROPDOWN") && x.Value == "1")
                                                                      ||
                                                                       (x.Option.Question.QuestionType.Type.ToUpper() != "CHECKBOX" && x.Option.Question.QuestionType.Type.ToUpper() != "RADIALBUTTON" && x.Option.Question.QuestionType.Type.ToUpper() != "DROPDOWN")
                                                                    ));
            if (rd != null)
            {
                if (rd.Option.Question.QuestionType.Type.ToUpper() == "DROPDOWN")
                {
                    return rd.Option.Name;
                }
                else if (rd.Option.Question.QuestionType.Type.ToUpper() == "CHECKBOX" ||
                    rd.Option.Question.QuestionType.Type.ToUpper() == "RADIALBUTTON")
                {
                    return GetReceiveDetailItem_DataElement_CheckDropName(ReceiveDetailID, FieldName);
                }
                else      // All others, Keyboard, Caledar, Cacl
                {
                    return rd.Value;
                }
            }
            return "";
        }
        public string GetReceiveDetailItem_DataElement_CheckDropName(decimal ReceiveDetailID, string FieldName)
        {
            string rString = "";
            var data = from x in ReceiveDetailItems
                       where x.ReceiveDetailID == ReceiveDetailID && x.Option.Question.Name.ToUpper() == FieldName.ToUpper() && x.Version == 0
                                                                       && (x.Option.Question.QuestionType.Type.ToUpper() == "CHECKBOX" || x.Option.Question.QuestionType.Type.ToUpper() == "RADIALBUTTON")
                       select x;
            foreach (var x in data)
            {
                rString += rString.Length > 0 ? "/" : "" + x.Option.Name;
            }
            return rString;
        }



        public void AddDetailBulkProcessLog(decimal ReceiveDetailBulkID, decimal ProcessID)
        {
            // This is used only... mainly for New and passthrough stuff
            Process p = this.Processes.FirstOrDefault(x => x.ProcessID == ProcessID);
            if (p == null) { return; }
            ReceiveDetailBulkProcessLog bl = new ReceiveDetailBulkProcessLog();
            bl.ReceiveDetailBulkID = ReceiveDetailBulkID;
            bl.CreateDate = DateTime.Now;
            bl.CreateUser = UserName;
            bl.ProcessID = ProcessID;
            bl.ProcessText = p.Name;
            bl.MiscText = "";
            this.ReceiveDetailBulkProcessLogs.InsertOnSubmit(bl);
        }

        public void AddDetailProcessLog(decimal ReceiveDetailID, decimal ProcessID)
        {
            // This is used only... mainly for New and passthrough stuff
            Process p = this.Processes.FirstOrDefault(x => x.ProcessID == ProcessID);
            if (p == null) { return; }
            ReceiveDetailProcessLog bl = new ReceiveDetailProcessLog();
            bl.ReceiveDetailID = ReceiveDetailID;
            bl.CreateDate = DateTime.Now;
            bl.CreateUser = UserName;
            bl.ProcessID = ProcessID;
            bl.ProcessText = p.Name;
            bl.MiscText = "";
            this.ReceiveDetailProcessLogs.InsertOnSubmit(bl);

            AddBucketCount(ReceiveDetailID, -1, -1, ProcessID, -1, -1);
        }

        public void AddBucketCount(decimal ReceiveDetailID, string OptionName)
        {
            var o = Options.FirstOrDefault(x => x.Name.ToUpper() == OptionName.ToUpper());
            if (o != null)
            {
                decimal OptionID = o.OptionID;
                if (OptionID > 0)
                {
                    AddBucketCount(ReceiveDetailID, -1, -1, -1, -1, OptionID);
                }
            }
        }


        public void AddBucketCount(decimal ReceiveDetailID, decimal ClientLocationID, decimal ProjectID, decimal ProcessID, decimal QuestionID, decimal OptionID)
        {
            if (ReceiveDetailID < 1) { return; }
            InsertStatisticalRawBucketData(ReceiveDetailID, -1, ClientLocationID, ProjectID, ProcessID, QuestionID, OptionID, 1, UserName);
        }


        public decimal AddBillingPointGMPBuy(decimal ReceiveDetailID, decimal ClientLoationID, decimal ProjectID, decimal ProcessID, string UserName)
        {



            ClientLocation cl = ClientLocations.FirstOrDefault(x => x.ClientLocationID == ClientLoationID);
            if (cl == null) { return -1; }


            ReceiveDetailBillingPoint bp = new ReceiveDetailBillingPoint();
            bp.ReceiveDetailID = ReceiveDetailID;
            bp.CreateDate = DateTime.Now;
            bp.CreateUser = UserName;
            bp.LastUpdateDate = DateTime.Now;
            bp.LastUpdateUser = UserName;
            bp.RateValue = 0;
            bp.ProcessID = ProcessID;
            bp.ClientID = cl.ClientID;
            bp.ProjectID = ProjectID;
            this.ReceiveDetailBillingPoints.InsertOnSubmit(bp);
            return ProcessID;

        }


        public bool AddBillingPoint(decimal ReceiveDetailID, decimal ClientLoationID, decimal ProjectID, decimal ProcessID, string UserName)
        {
            ClientLocation cl = ClientLocations.FirstOrDefault(x => x.ClientLocationID == ClientLoationID);
            if (cl == null) { return false; }
            ReceiveDetailBillingPoint bp = ReceiveDetailBillingPoints.FirstOrDefault(x => x.ReceiveDetailID == ReceiveDetailID
                                                                                       && x.ClientID == cl.ClientID
                                                                                       && x.ProjectID == ProjectID
                                                                                       && x.ProcessID == ProcessID);
            if (bp == null)
            {
                ClientBillingPoint cbp = ClientBillingPoints.FirstOrDefault(x => x.ClientID == cl.ClientID
                                                                              && x.ProcessID == ProcessID
                                                                              && x.ProjectID == ProjectID);
                // if no cbp found and the client does not have any billing points set
                if (cbp == null && ClientBillingPoints.FirstOrDefault(x => x.ClientID == cl.ClientID) == null)
                {
                    // See if there are any "generic billing points"
                    cbp = ClientBillingPoints.FirstOrDefault(x => x.ClientID == -1
                                                               && x.ProcessID == ProcessID
                                                               && x.ProjectID == ProjectID);
                }
                if (cbp == null) { return false; }

                bp = new ReceiveDetailBillingPoint();
                bp.ReceiveDetailID = ReceiveDetailID;
                bp.CreateDate = DateTime.Now;
                bp.CreateUser = UserName;
                bp.LastUpdateDate = DateTime.Now;
                bp.LastUpdateUser = UserName;
                bp.RateValue = cbp.RateValue;
                bp.ProcessID = ProcessID;
                bp.ClientID = cl.ClientID;
                bp.ProjectID = ProjectID;
                this.ReceiveDetailBillingPoints.InsertOnSubmit(bp);
                return true;
            }
            return false;
        }

        public void CopyBulkProcessLogToDetail(decimal ReceiveDetailBulkID, decimal ReceiveDetailID)
        {
            ReceiveDetailBulkProcessLog bulkLog = this.ReceiveDetailBulkProcessLogs.FirstOrDefault(x => x.ReceiveDetailBulkID == ReceiveDetailBulkID);
            if (bulkLog == null)
            {
                return;
            }
            ReceiveDetailProcessLog bl = new ReceiveDetailProcessLog();
            bl.ReceiveDetailID = ReceiveDetailID;
            bl.ProcessID = bulkLog.ProcessID;
            bl.CreateDate = bulkLog.CreateDate;
            bl.CreateUser = bulkLog.CreateUser;
            bl.ProcessText = bulkLog.ProcessText;
            bl.MiscText = "Received Bulk";
            this.ReceiveDetailProcessLogs.InsertOnSubmit(bl);
        }

        public string SystemDisplayText()
        {
            SystemData sd = this.SystemDatas.FirstOrDefault(x => x.DataKey.ToUpper() == "SYSTEM");
            if (sd != null)
            {
                return sd.Data;
            }
            return "";
        }

        public string NextWorkOrderNumber()
        {
            string Number = "";
            this.GetNextWorkOrderNumber(ref Number);
            return Number;
        }

        public decimal NextBinNumber(decimal NumberRequired)
        {
            if (NumberRequired < 1) { return 0; }
            decimal? Number = 0;
            this.GetNextBinNumber((decimal?)NumberRequired, ref Number);
            return (decimal)Number;
        }

        public string NextPurchaseOrderNumber()
        {
            string Number = "";
            this.GetNextWorkOrderNumber(ref Number);
            return Number;
        }

        public string NextProcessRMANumber(decimal ProcessID)
        {
            string Number = "";
            this.GetNextProcessRMANumber(ProcessID, ref Number);
            return Number.PadLeft(6, '0');
        }

        public string NextRMANumber(decimal ClientLocationID, decimal ProcessID)
        {
            string Number = "Error";
            ClientLocation cl = this.ClientLocations.FirstOrDefault(x => x.ClientLocationID == ClientLocationID);
            if (cl != null)
            {
                Process p = this.Processes.FirstOrDefault(x => x.ProcessID == ProcessID);
                Client c = this.Clients.FirstOrDefault(x => x.ClientID == cl.ClientID);
                if (c != null && p != null)
                {
                    string Suffix_1 = "";
                    string Suffix_2 = "";
                    //string Suffix_3 = "";
                    //string Suffix_4 = "";
                    //string Suffix_5 = "";
                    if (c.RMASuffix != null) { Suffix_1 = c.RMASuffix; }
                    if (p.RMASuffix != null) { Suffix_2 = p.RMASuffix; }

                    Number = Suffix_1.Trim() + Suffix_2.Trim() + DateTime.Now.ToString("yyyyMMdd") + "-" + NextProcessRMANumber(ProcessID);
                }
            }
            return Number;
        }

    }




    [Serializable]
    public partial class GetReceiveDetail_SalesAvailableStock_ManufacturerResult
    {
    }

    [Serializable]
    public partial class GetReceiveDetail_SalesAvailableStock_ModelResult
    {
    }

    [Serializable]
    public partial class GetReceiveDetail_SalesAvailableStock_ColourResult
    {
    }






}
