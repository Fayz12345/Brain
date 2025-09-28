using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace BW_WebApp.Classes
{
    
    
    public static class ExtendedMethods
    {
        #region Colour Extensions
        public static String HexConverter(this System.Drawing.Color c)
        {
            return string.Format("#{0:X2}{1:X2}{2:X2}", c.R, c.G, c.B);
        }
        public static String RGBConverter(this System.Drawing.Color c)
        {
            return string.Format("RGB({0},{1},{2})", c.R, c.G, c.B);
        }
        #endregion
        public static string ToBase64String(this byte[] value)
        {
            return Convert.ToBase64String(value);
        }
        public static DateTime ToDateTimeNow(this string value)
        {
            DateTime date = DateTime.Now;
            DateTime.TryParseExact(value, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out date);
            return date;
        }
        public static double ToDoubleZero(this string value)
        {
            double data = 0;
            double.TryParse(value, out data);
            return data;
        }
        public static int ToIntZero(this string value)
        {
            int data = 0;
            int.TryParse(value, out data);
            return data;
        }
        #region JSON
        public static Guid ToGuid(this string value)
        {
            Guid rValue = Guid.Empty;
            Guid.TryParse(value, out rValue);
            return rValue;
        }
        public static string ToJSONFormat(this DateTime value)
        {
            return value.ToString("yyyy-MM-dd");
        }
        public static string ToJSONFormatWithTime(this DateTime value)
        {
            return string.Format("yyyy-MM-dd HH:mm:ss", value);
        }
        public static string ToJSONFormat(this bool value)
        {
            return value.ToString();
        }
        public static string ToJSONFormat(this decimal value)
        {
            return value.ToString();
        }
        public static string ToJSONFormat(this long value)
        {
            return value.ToString();
        }
        public static string ToJSONFormat(this int value)
        {
            return value.ToString();
        }
        public static string ToJSONFormat(this Guid value)
        {
            return value.ToString();
        }
        public static string ToJSONFormatEmail(this string value)
        {
            return value.ToString();
        }
        public static string ToJSONFormatPhone(this string value)
        {
            return value.ToString();
        }

    //public class Helpers
    //{
    //    public static string JsonSerializerNeuton<T>(T t)
    //    {
    //        return JsonConvert.SerializeObject(t, Formatting.Indented);
    //    }
    //    public static T JsonDeserializeNeuton<T>(string jsonString)
    //    {
    //        return JsonConvert.DeserializeObject<T>(jsonString);
    //    }
    //}
        public static string JsonSerializer<T>(T t)
        {
            //MemoryStream stream1 = new MemoryStream();  
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, t);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return jsonString;
            //ms.Position = 0;
            //StreamReader sr = new StreamReader(ms);
            //return sr.ReadToEnd();
            ////DeviceInfo result = ser.ReadObject(x.ToStream()) as DeviceInfo;
        }
        public static T JsonDeserialize<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(ms);
            //ms.Close();
            return obj;
        }


        #endregion

        //#region Terminology Translator
        //public static string DispTerm(this string controller, string Lng)
        //{
        //    //string TheFormText;
        //    //string TheFormName;
        //    //string TheFormtopName;
        //    //string TheFormTag;
        //    //Form TheForm = Form.ActiveForm;
        //    //TheFormText = TheForm.ActiveMdiChild.Text.ToString();
        //    //TheFormName = TheForm.ActiveMdiChild.Name.ToString();
        //    //TheFormtopName = TheForm.Name.ToString();
        //    //try
        //    //{
        //    //    TheFormTag = TheForm.ActiveMdiChild.Tag.ToString();
        //    //}
        //    //catch
        //    //{
        //    //    TheFormTag = "<none>";
        //    //}




        //    //CultureInfo c = controller.Culture();
        //    //string lang = c.TwoLetterISOLanguageName;
        //    return TermConverter.DispTerm(controller, "actionName", "controllerName", Lng);
        //}
        //public static string DispTerm(this string controller)
        //{
        //    return controller.DispTerm("en");
        //    //string TheFormText;
        //    //string TheFormName;
        //    //string TheFormtopName;
        //    //string TheFormTag;
        //    //Form TheForm = Form.ActiveForm;
        //    //TheFormText = TheForm.ActiveMdiChild.Text.ToString();
        //    //TheFormName = TheForm.ActiveMdiChild.Name.ToString();
        //    //TheFormtopName = TheForm.Name.ToString();
        //    //try
        //    //{
        //    //    TheFormTag = TheForm.ActiveMdiChild.Tag.ToString();
        //    //}
        //    //catch
        //    //{
        //    //    TheFormTag = "<none>";
        //    //}
        //    //CultureInfo c = controller.Culture();
        //    //string lang = c.TwoLetterISOLanguageName;
        //    //return TermConverter.DispTerm(controller, "actionName", "controllerName", "en");
        //}
        //#endregion
    }









}