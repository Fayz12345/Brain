using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using BW_WebApp.DataManagers;

namespace BW_WebApp.Classes
{

    public class GMPCalculator
    {
        string _UserName { get; set; }
        decimal _ReceiveDetialID { get; set; }
        ReceiveDetailManager rdm = null;

        public GMPCalculator(decimal ReceiveDetailID, string UserName)
        {
            this._UserName = UserName;
            this._ReceiveDetialID = ReceiveDetailID;
            rdm = new ReceiveDetailManager(UserName);
        }


        public string Calculate(string Formula)
        {
            FormulaCalculator fc = new FormulaCalculator();
            fc.RequestParameterValue += new FormulaCalculator.TranslateParameter(TranslateParameter);
            decimal ans = fc.Calculate(Formula);
            //decimal ans = fc.Calculate("%Bob% + %Bill% * %George%");
            return ans.ToString();
        }


        //public decimal TranslateParameter(string field)
        //{
        //    string Value = rdm.GetReceiveDetailItem_DataElement(_ReceiveDetialID, field);
        //    decimal dValue = 0;
        //    if (decimal.TryParse(Value, out dValue) == false) { dValue = 0; }
        //    return dValue;
        //}
        public string TranslateParameter(string field)
        {
            string Value = rdm.GetReceiveDetailItem_DataElement(_ReceiveDetialID, field);
            if (Value.Length == 0) { Value = "0"; }

            // We need to see if this is a date field. If it is we need to reverse the forward slash so it is not assume divide
            DateTime DateTest = DateTime.Now;
            string[] formats = { "MM/dd/yyyy" };
            if (DateTime.TryParseExact(Value, formats, new CultureInfo("en-US"), DateTimeStyles.None,
                                       out DateTest) == true)
            {
                Value = Value.Replace('/', '\\');
            }





            //if (DateTime.TryParse(Value, out DateTest) == true) { Value = Value.Replace('/', '\\'); }


            return Value;
            //decimal dValue = 0;
            //if (decimal.TryParse(Value, out dValue) == false) { dValue = 0; }
            //return dValue;
        }
    }


    public class FormulaCalculator
    {

        public delegate string TranslateParameter(string Parameter);

        public event TranslateParameter RequestParameterValue;

        private Dictionary<string, decimal> _Parameters = new Dictionary<string, decimal>();
        private List<String> OperationOrder = new List<string>();

        public Dictionary<string, decimal> Parameters
        {
            get { return _Parameters; }
            set { _Parameters = value; }
        }

        public FormulaCalculator()
        {
            OperationOrder.Add("/");
            OperationOrder.Add("*");
            OperationOrder.Add("-");
            OperationOrder.Add("+");
        }
        public decimal Calculate(string Formula)
        {
            try
            {
                string Operators = "";
                foreach (string x in OperationOrder)
                {
                    Operators += x;
                }
                Operators += "(";
                Operators += ")";
                string[] arr = Formula.Split(Operators.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                //string[] arr = Formula.Split("/+-*()".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                foreach (string s in arr)
                {
                    decimal value = 0;
                    string x1 = s.Replace('%', ' ').Trim();
                    if (decimal.TryParse(x1, out value) == false)
                    {
                        Formula = Formula.Replace(s, RequestParameterValue(x1).ToString());
                    }
                }
                while (Formula.LastIndexOf("(") > -1)
                {
                    int lastOpenPhrantesisIndex = Formula.LastIndexOf("(");
                    int firstClosePhrantesisIndexAfterLastOpened = Formula.IndexOf(")", lastOpenPhrantesisIndex);
                    decimal result = ProcessOperation(Formula.Substring(lastOpenPhrantesisIndex + 1, firstClosePhrantesisIndexAfterLastOpened - lastOpenPhrantesisIndex - 1));
                    bool AppendAsterix = false;
                    if (lastOpenPhrantesisIndex > 0)
                    {
                        if (Formula.Substring(lastOpenPhrantesisIndex - 1, 1) != "(" && !OperationOrder.Contains(Formula.Substring(lastOpenPhrantesisIndex - 1, 1)))
                        {
                            AppendAsterix = true;
                        }
                    }

                    Formula = Formula.Substring(0, lastOpenPhrantesisIndex) + (AppendAsterix ? "*" : "") + result.ToString() + Formula.Substring(firstClosePhrantesisIndexAfterLastOpened + 1);

                }
                return ProcessOperation(Formula);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Occured While Calculating. Check Syntax", ex);
            }
        }
        private decimal ProcessOperation(string operation)
        {
            ArrayList arr = new ArrayList();
            string s = "";
            for (int i = 0; i < operation.Length; i++)
            {
                string currentCharacter = operation.Substring(i, 1);
                if (OperationOrder.IndexOf(currentCharacter) > -1)
                {
                    if (s != "")
                    {
                        arr.Add(s);
                    }
                    arr.Add(currentCharacter);
                    s = "";
                }
                else
                {
                    s += currentCharacter;
                }
            }
            arr.Add(s);
            s = "";
            foreach (string op in OperationOrder)
            {
                while (arr.IndexOf(op) > -1)
                {
                    bool WasProcessed = false;
                    int operatorIndex = arr.IndexOf(op);
                    // Determin if we are working with Dates.

                    DateTime dateBeforeOperator = DateTime.Now;
                    DateTime dateAfterOperator = DateTime.Now;
                    string[] formats = { "MM/dd/yyyy" };
                    if (DateTime.TryParseExact(arr[operatorIndex - 1].ToString().Replace('\\', '/'), formats, new CultureInfo("en-US"), DateTimeStyles.None, out dateBeforeOperator) == true
                     && DateTime.TryParseExact(arr[operatorIndex + 1].ToString().Replace('\\', '/'), formats, new CultureInfo("en-US"), DateTimeStyles.None, out dateAfterOperator) == true)
                    {
                        arr[operatorIndex] = CalculateByOperator(dateBeforeOperator, dateAfterOperator, op);
                        WasProcessed = true;
                    }

                    if (WasProcessed == false)
                    {
                        // Determin if we are working with numbers.
                        decimal digitBeforeOperator = Convert.ToDecimal(arr[operatorIndex - 1]);
                        decimal digitAfterOperator = 0;
                        if (arr[operatorIndex + 1].ToString() == "-")
                        {
                            arr.RemoveAt(operatorIndex + 1);
                            digitAfterOperator = Convert.ToDecimal(arr[operatorIndex + 1]) * -1;
                        }
                        else
                        {
                            digitAfterOperator = Convert.ToDecimal(arr[operatorIndex + 1]);
                        }
                        arr[operatorIndex] = CalculateByOperator(digitBeforeOperator, digitAfterOperator, op);
                        WasProcessed = true;
                    }
                    if (WasProcessed == false)
                    {
                        arr[operatorIndex] = 0;
                    }
                    arr.RemoveAt(operatorIndex - 1);
                    arr.RemoveAt(operatorIndex);
                }
            }
            return Convert.ToDecimal(arr[0]);
        }



        private decimal CalculateByOperator(DateTime Date1, DateTime Date2, string op)
        {
            if (op == "/")
            {
                return 0;
                //return number1 / number2;
            }
            else if (op == "*")
            {
                return 0;
                //return number1 * number2;
            }
            else if (op == "-")
            {
                double rvalue = 0;
                TimeSpan span = Date1 - Date2;
                rvalue = span.TotalDays;
                return Convert.ToDecimal(rvalue);
                //return number1 - number2;
            }
            else if (op == "+")
            {
                return 0;
                //return number1 + number2;
            }
            else
            {
                return 0;
            }
        }
        private decimal CalculateByOperator(decimal number1, decimal number2, string op)
        {
            if (op == "/")
            {
                return number1 / number2;
            }
            else if (op == "*")
            {
                return number1 * number2;
            }
            else if (op == "-")
            {
                return number1 - number2;
            }
            else if (op == "+")
            {
                return number1 + number2;
            }
            else
            {
                return 0;
            }
        }
    }
}