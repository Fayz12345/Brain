using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

using BW_WebApp.DataManagers;
using NCalc;

namespace BW_WebApp.Classes
{
    public class BusinessRules
    {
        decimal _ReceiveDetailID = -1;
        EvaluateManager CondManager = null;
        clsLinqDataContext ctx = new clsLinqDataContext();
        ConditionString condstring = null;
        List<PairStringValue> DataDevice = new List<PairStringValue>();
        List<PairStringValue> DataDeviceTokens = new List<PairStringValue>();
        List<PairStringValue> DataDeviceTokensCheckBoxs = new List<PairStringValue>();

        List<JsonStringOptionKeyListByProjectProcess> _QuestionData = new List<JsonStringOptionKeyListByProjectProcess>();
        List<JsonStringOptionKeyListByProjectProcess> _DeviceDataRecordMerged = new List<JsonStringOptionKeyListByProjectProcess>();
        List<JsonStringOptionKeyListByProjectProcess> _DeviceDataRecordLive = new List<JsonStringOptionKeyListByProjectProcess>();
        List<JsonStringReceiveDetailOptionKeyList> _DeviceDataRecord = new List<JsonStringReceiveDetailOptionKeyList>();

        #region Supporting Data
        public List<JsonStringOptionKeyListByProjectProcess> QuestionData
        {
            get { return _QuestionData; }
        }
        public List<JsonStringOptionKeyListByProjectProcess> DeviceDataRecordMerged
        {
            get { return _DeviceDataRecordMerged; }
        }
        public List<JsonStringReceiveDetailOptionKeyList> DeviceDataRecord
        {
            get { return _DeviceDataRecord; }
        }
        public decimal ReceiveDetailID
        {
            get { return _ReceiveDetailID; }
            set { _ReceiveDetailID = value;
            JsonString util = new JsonString();
            _DeviceDataRecord = util.dbQuestionAnswerTable_TypeChecksByDevice(_ReceiveDetailID);
            }
        }


        //public Dictionary<string, object> Parameters { get { return CondManager.Parameters; } }
        //public Dictionary<string, object> ParametersMaster { get { return CondManager.ParametersMaster; } }

        //public Dictionary<string, object> ParametersDeviceData { get { return CondManager.ParametersDeviceData; } }
        //public Dictionary<string, object> ParametersESNRelated { get { return CondManager.ParametersESNRelated; } }

        //public Dictionary<string, object> MasterQuestionData { get { return CondManager.MasterQuestionData; } }
        //public Dictionary<string, object> MasterOptionData { get { return CondManager.MasterOptionData; } }
        #endregion

        #region Answer Evaluate Required
        JsonString jDeviceData = null;
        string _DeviceData = "";
        public string DeviceData
        {
            get { return _DeviceData; }
            set
            {
                _DeviceData = value; jDeviceData = new JsonString(value, true);
                ReceiveDetailID = jDeviceData.GetValueDecimal("ReceiveDetailID", -1);
                LoadParameters();
            }
        }
        #endregion

        #region QueryBuilder
        decimal _DisplayProjectID = -1;
        decimal _DisplayProcessID = -1;
        #endregion


        public string Condtion
        {
            get { if (condstring == null) { condstring = new ConditionString(""); } return condstring.Expression; }
            set { if (condstring == null) { condstring = new ConditionString(value); } condstring.Expression = value; }
        }
        public string CondtionEnglish
        {
            get
            {
                if (condstring == null) { return ""; }
                List<PairStringValue> Translate = new List<PairStringValue>();
                foreach (JsonStringOptionKeyListByProjectProcess d in QuestionData)
                {
                    //JsonStringOptionKeyListByProjectProcess d = (JsonStringOptionKeyListByProjectProcess)x.Value;
                    PairStringValue dta = new PairStringValue(d.ScanKey, d.OptionValue);
                    //dta.Key = d.ScanKey;
                    //dta.Value = d.OptionValue;
                    Translate.Add(dta);
                }
                return condstring.Transform(Translate, false);
            }
        }
        public string CondtionTokens
        {
            get
            {
                if (condstring == null) { return ""; }
                List<PairStringValue> Translate = new List<PairStringValue>();
                foreach (JsonStringOptionKeyListByProjectProcess d in QuestionData)
                {
                    //JsonStringOptionKeyListByProjectProcess d = (JsonStringOptionKeyListByProjectProcess)x.Value;
                    PairStringValue dta = new PairStringValue(d.ScanKey, d.OptionID.ToString());
                    //dta.Key = d.ScanKey;
                    //dta.Value = d.OptionID.ToString();
                    Translate.Add(dta);
                }
                return condstring.Transform(Translate, false);
            }
        }
        public string ConditionWithDataTokens
        {
            get
            {
                if (condstring == null) { return ""; }

                DataDeviceTokens.Clear();
                DataDeviceTokensCheckBoxs.Clear();
                List<string> MoreThanOneAnswer = new List<string>();

                // List<PairStringValue> Translate = new List<PairStringValue>();
                foreach (JsonStringOptionKeyListByProjectProcess d in QuestionData)
                {
                    //JsonStringOptionKeyListByProjectProcess d = (JsonStringOptionKeyListByProjectProcess)y.Value;
                    PairStringValue dData = DataDevice.FirstOrDefault(x => x.Key == d.jCoded);
                    if (dData != null)
                    {
                        PairStringValue dta = new PairStringValue(d.Name, d.OptionID.ToString());
                        //dta.Key = d.Name;
                        //dta.Value = d.OptionID.ToString();

                        if (MoreThanOneAnswer.Contains(d.Name))
                        {
                            DataDeviceTokens.Add(dta);
                        }
                        else
                        {
                            DataDeviceTokens.Add(dta);
                            MoreThanOneAnswer.Add(d.Name);
                        }
                    }
                }
                string TokenizedFormula = CondtionTokens;
                return condstring.TransformData(TokenizedFormula, DataDeviceTokens, false);
            }
        }


        public bool HasErrors
        {
            get { return CondManager.HasErrors; }
        }
        public string EvaluateCondition()
        {
            CondManager.Condtion = condstring.Expression;
            string rValue = "";
            try
            {
                rValue = CondManager.Evaluate();
            }
            catch (Exception er)
            {
                rValue = er.Message;
            }
            return rValue;
        }
        public string EvaluateCondition(string ConditionString)
        {
            CondManager.Condtion = ConditionString;
            string rValue = "";
            try
            {
                rValue = CondManager.Evaluate();
            }
            catch (Exception er)
            {
                rValue = er.Message;
            }
            return rValue;
        }
        public void AddParameter(string Name, object Value)
        {
            CondManager.AddParameter(Name, Value);
        }
        public void AddFunction(string Name, FunctionDeligate Value)
        {
            CondManager.AddFunction(Name, Value);
        }


        private void LoadParameters()
        {
            List<decimal> LiveDataQuestionID = new List<decimal>();
             //Add all the Works screen Values.
            if (CondManager == null)
            {
                CondManager = new EvaluateManager("");
            }

            DataDevice.Clear();
            ////List<string> FieldNames = new List<string>();
            //foreach (string key in jDeviceData.ListData().Keys)
            //{
            //    if (jDeviceData.ListData()[key].ToString() == "1")
            //    {
            //        PairStringValue dta = new PairStringValue();
            //        dta.Key = key;
            //        dta.Value = jDeviceData.ListData()[key].ToString();
            //        DataDevice.Add(dta);
            //    }
            //}
            _QuestionData = jDeviceData.dbQuestionAnswerTable_TypeChecksByProject(_DisplayProjectID);
            //List<JsonStringOptionKeyListByProjectProcess> dbQuestionAnswerTable_TypeChecks = jDeviceData.dbQuestionAnswerTable_TypeChecksByProject(_DisplayProjectID);
            foreach (JsonStringOptionKeyListByProjectProcess i in QuestionData)
            {
                //CondManager.AddParametersMaster(i.jCoded, i);
                //CondManager.AddMasterOptionData(i.UniqueKey, i);
                //CondManager.AddMasterQuestionData(i.Name, i);

                PairStringValue dData = DataDevice.FirstOrDefault(x => x.Key == i.jCoded);
                if (dData != null)
                {
                    //if (ParametersDeviceData.Keys.Contains(i.Name) == false)
                    //{
                    //    ParametersDeviceData.Add(i.Name, i);
                    //}
                    DeviceDataRecordMerged.Add(i);
                    LiveDataQuestionID.Add(i.QuestionID);
                }
            }
            //// we only want to add those questions that are not inside the live data.
            //foreach (JsonStringReceiveDetailOptionKeyList record in _DeviceDataRecord.Where(x=> LiveDataQuestionID.Contains(x.QuestionID) == false).OrderBy(x=> x.Name).ThenBy(x=> x.OptionValue))
            //{
            //    JsonStringOptionKeyListByProjectProcess d = new JsonStringOptionKeyListByProjectProcess();
            //    d.Abbr = record.Abbr;
            //    d.IFS_Condition = record.IFS_Condition;
            //    d.IFS_Condition_Sequence = record.IFS_Condition_Sequence;
            //    d.jCoded = record.jCoded;
            //    d.MacroKey = record.MacroKey;
            //    d.MicroKey = record.MicroKey;
            //    d.Name = record.Name;
            //    d.OptionID = record.OptionID;
            //    d.OptionValue = record.OptionValue;
            //    d.ProcessID = record.ProcessID;

            //    if (record.ProjectID == null) { d.ProjectID = -1; }
            //    else { d.ProjectID = (decimal)record.ProjectID; }

            //    d.QuestionID = record.QuestionID;
            //    d.ScanKey = record.ScanKey;
            //    d.Type = record.Type;
            //    d.UniqueKey = record.OptionID.ToString();
            //    d.UseOptionValue = record.UseOptionValue;
            //    DeviceDataRecordMerged.Add(d);
            //}




        }

        //private bool IsUserData(JsonStringOptionKeyListByProjectProcess i)
        //{
        //    var d = DeviceDataRecord.FirstOrDefault(x => x.OptionID == i.OptionID && x.ProjectID == i.ProjectID && x.ProcessID == i.ProcessID);
        //    if (d == null) { return false; }
        //    return true;
        //}


    }


    public class EvaluateManager
    {
        string _Message = "";
        string _Condition = "";
        Expression expression = null;
        private Dictionary<string, object> _parameters;
        private Dictionary<string, object> _parametersMaster;
        private Dictionary<string, object> _parametersESNRelated;

        
        private Dictionary<string, object> _parametersDeviceData;
        private Dictionary<string, object> _MasterOptionData;
        private Dictionary<string, object> _MasterQuestionData;

        private Dictionary<string, object> _functions;

        public Dictionary<string, object> Parameters
        {
            get { return _parameters ?? (_parameters = new Dictionary<string, object>()); }
            set { _parameters = value; }
        }
        public Dictionary<string, object> ParametersMaster
        {
            get { return _parametersMaster ?? (_parametersMaster = new Dictionary<string, object>()); }
            set { _parametersMaster = value; }
        }
        public Dictionary<string, object> ParametersDeviceData
        {
            get { return _parametersDeviceData ?? (_parametersDeviceData = new Dictionary<string, object>()); }
            set { _parametersDeviceData = value; }
        }
        public Dictionary<string, object> MasterOptionData
        {
            get { return _MasterOptionData ?? (_MasterOptionData = new Dictionary<string, object>()); }
            set { _MasterOptionData = value; }
        }
        public Dictionary<string, object> MasterQuestionData
        {
            get { return _MasterQuestionData ?? (_MasterQuestionData = new Dictionary<string, object>()); }
            set { _MasterQuestionData = value; }
        }
        public Dictionary<string, object> ParametersESNRelated
        {
            get { return _parametersESNRelated ?? (_parametersESNRelated = new Dictionary<string, object>()); }
            set { _parametersESNRelated = value; }
        }


        public Dictionary<string, object> Functions
        {
            get { return _functions ?? (_functions = new Dictionary<string, object>()); }
            set { _functions = value; }
        }
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        public string Condtion
        {
            get { return _Condition; }
            set
            {
                _Condition = value;
                if (value.Length > 0)
                {
                    //expression = new Expression(Condtion, EvaluateOptions.IterateParameters);
                    expression = new Expression(Condtion);
                    //expression.Parameters["Bob"] = new string[] {"9876","33444","66666"};
                    //expression.Parameters["Bill"] = new string[] { "19876", "133444", "166666" };
                    expression.EvaluateParameter += new EvaluateParameterHandler(EvaluateParameter);
                    expression.EvaluateFunction += new EvaluateFunctionHandler(EvaluateFunction);
                    //expression.Options = EvaluateOptions.IgnoreCase;
                }
            }
        }
        public bool HasErrors
        {
            get { return expression.HasErrors(); } 
        }

        public EvaluateManager(string condtion)
        {
            Condtion = condtion;
        }

        public string Evaluate()
        {
            //object eval =  expression.Evaluate();
            //expression.Parameters.Add(

            StringBuilder rValue = new StringBuilder();
            //try
            //{

            //foreach (var result in (IList<object>)expression.Evaluate())
            //{
            //    rValue.AppendLine(string.Format("{0}", result));
            //}
            rValue.Append(expression.Evaluate());


            
            //}
            //catch (Exception er)
            ////catch (EvaluationException er)
            //{
            //    rValue = er.Message;
            //}
            Message = rValue.ToString();
            return Message;
        }

        void EvaluateParameter(string name, ParameterArgs args)
        {
            //args.Result = TraverseParametersTillEnd(args.Result);
            if (Parameters.ContainsKey(name) == true)
            {
                JsonStringOptionKeyListByProjectProcess dta = (JsonStringOptionKeyListByProjectProcess)TraverseParametersTillEnd(Parameters[name]);
                args.Result = dta.OptionID;
            }
        }

        private object TraverseParametersTillEnd(object result)
        {
            object rValue = result;
            if (Parameters.ContainsKey(result.ToString()) == true)
            {
                rValue = TraverseParametersTillEnd(Parameters[result.ToString()]);
            }

            return rValue;
        }
        public void AddParameter(string Name, object Value)
        {
            if (Parameters.ContainsKey(Name) == false)
            {
                Parameters.Add(Name, Value);
            }
            else
            {
                Parameters[Name] = Value;
            }
        }
        public void AddParametersMaster(string Name, object Value)
        {
            if (ParametersMaster.ContainsKey(Name) == false)
            {
                ParametersMaster.Add(Name, Value);
            }
            else
            {
                ParametersMaster[Name] = Value;
            }
        }
        public void AddParametersDeviceData(string Name, object Value)
        {
            if (ParametersDeviceData.ContainsKey(Name) == false)
            {
                ParametersDeviceData.Add(Name, Value);
            }
            else
            {
                ParametersDeviceData[Name] = Value;
            }
        }


        public void AddMasterOptionData(string Name, object Value)
        {
            if (MasterOptionData.ContainsKey(Name) == false)
            {
                MasterOptionData.Add(Name, Value);
            }
            else
            {
                MasterOptionData[Name] = Value;
            }
        }
        public void AddMasterQuestionData(string Name, object Value)
        {
            if (MasterQuestionData.ContainsKey(Name) == false)
            {
                MasterQuestionData.Add(Name, Value);
            }
            else
            {
                MasterQuestionData[Name] = Value;
            }
        }





        public void AddParametersESNRelated(string Name, object Value)
        {
            if (ParametersESNRelated.ContainsKey(Name) == false)
            {
                ParametersESNRelated.Add(Name, Value);
            }
            else
            {
                ParametersESNRelated[Name] = Value;
            }
        }

        void  EvaluateFunction(string name, FunctionArgs args)
        {
            if (Functions.ContainsKey(name) == true)
            {
                FunctionDeligate Func = (FunctionDeligate)Functions[name];
                args.Result = Func.DynamicInvoke(args);
            }


          //if (name == "SecretOperation")
          //    args.Result = (int)args.Parameters[0].Evaluate() + (int)args.Parameters[1].Evaluate();
        }
        public void AddFunction(string Name, FunctionDeligate Value)
        {
            if (Functions.ContainsKey(Name) == false)
            {
                Functions.Add(Name, Value);
            }
            else
            {
                Functions[Name] = Value;
            }
        }

    }


    public class ConditionString
    {
        string _expression = "";
        public string Expression
        {
            get { return _expression; }
            set { _expression = value; }
        }


        public ConditionString(string expression)
        {
            Expression = expression;

        }

        //public string EnglishVersion
        //{
        //    get { return Version(); }
        //}

        public string Transform(List<PairStringValue> Translate, bool Indent)
        {
            StringBuilder s = new StringBuilder(Expression);

            if (Indent == true) { FormatLineSingle(s); }
            else { FormatLineSingle(s); }

            foreach (PairStringValue pair in Translate.Where(x=> x.Key.Length > 0).OrderByDescending(x=> x.Value.Length))
            {
                if (s.ToString().Contains(pair.Key) == true)
                {
                    if (s.ToString().Contains("[" + pair.Key + "]") == true)
                    {
                        s.Replace("[" + pair.Key + "]", pair.Value);
                    }
                    else { s.Replace(pair.Key, pair.Value); }
                }
            }
            return s.ToString();
        }


        public string TransformData(string equation, List<PairStringValue> Translate, bool Indent)
        {
            StringBuilder s = new StringBuilder(equation);

            if (Indent == true) { FormatLineSingle(s); }
            else { FormatLineSingle(s); }

            foreach (PairStringValue pair in Translate.Where(x => x.Key.Length > 0).OrderByDescending(x => x.Value.Length))
            {
                if (s.ToString().Contains(pair.Key) == true)
                {
                    if (s.ToString().Contains("[" + pair.Key + "]") == true)
                    {
                        s.Replace("[" + pair.Key + "]", pair.Value);
                    }
                    else { s.Replace(pair.Key, pair.Value); }
                }
            }
            return s.ToString();
        }




        static string FormatLineSingle(StringBuilder b)
        {
            //StringBuilder b = new StringBuilder(p);
            b.Replace("  ", " ");
            b.Replace(Environment.NewLine, " ");
            b.Replace("\\t", " ");
            b.Replace("\r", " ");
            b.Replace("\n", " ");
            b.Replace("]=[", "] = [");
            b.Replace("]=", "] =");
            b.Replace("=[", "= [");


            b.Replace("][", "] or [");
            b.Replace("](", "] or (");
            b.Replace(")[", ") or [");


            b.Replace("]or[", "] or [");
            b.Replace("]or", "] or");
            b.Replace("or[", "or [");

            b.Replace("]and[", "] and [");
            b.Replace("]and", "] and");
            b.Replace("and[", "and [");

            b.Replace("in (", "in(");
            b.Replace("IN(", "in(");
            b.Replace("In(", "in(");
            //b.Replace(" (", Environment.NewLine + "(");
            //b.Replace(") ", ")" + Environment.NewLine);


            //b.Replace(" {", "{");
            //b.Replace(" :", ":");
            //b.Replace(": ", ":");
            //b.Replace(", ", ",");
            //b.Replace("; ", ";");
            //b.Replace(";}", "}");
            return b.ToString();
        }
        static string FormatLine(StringBuilder b)
        {
            b.Replace("  ", string.Empty);
            b.Replace(Environment.NewLine, string.Empty);
            b.Replace("\\t", string.Empty);
            b.Replace(" {", "{");
            b.Replace(" :", ":");
            b.Replace(": ", ":");
            b.Replace(", ", ",");
            b.Replace("; ", ";");
            b.Replace(";}", "}");
            return b.ToString();
        }











    }

}