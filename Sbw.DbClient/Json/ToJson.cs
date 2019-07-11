
//#define Framework3_5 
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using System.IO;
using System.Text;
using System.Data.Common;
using System.Collections.Generic;

namespace Sbw.Json
{
    public class ToJson
    {
        /// <summary>
        /// 转换 object 为json
        /// </summary>
        /// <param name="obj">object</param>
        /// <returns>json</returns>
        public static string ObjectToJson(object obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, obj);
                StringBuilder sb = new StringBuilder();
                sb.Append(Encoding.UTF8.GetString(ms.ToArray()));
                return sb.ToString();
            }
        }
        
        public static string Serialize(object obj)
        {
			if (obj == null) return null;
            JavaScriptSerializer jss = new JavaScriptSerializer();
            //jss.RegisterConverters();
            return jss.Serialize(obj);
        }
        public static T Deserialize<T>(string json)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<T>(json);
        }
        /// <summary>
        /// 转换 List<T> 为 json
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="list">集合</param>
        /// <returns>json</returns>
        public static string ListToJson<T>(List<T> list)
        {
            return ToJson.Serialize(list);
        }
        /// <summary>
        /// 转换 DbDataReader 为 json
        /// </summary>
        /// <param name="dr">DbDataReader</param>
        /// <returns>json</returns>
        public static string DataReaderToJson(DbDataReader dr)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[ ");
            while (dr.Read())
            {
                sb.Append("{");
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    sb.AppendFormat(" \"{0}\": {1}, ", dr.GetName(i), ToString(dr.GetValue(i)));
                }
                sb.Length -= 2;
                sb.Append("},");
            }
            sb.Length--;
            sb.Append("]");
            return sb.ToString();
        }
        /// <summary>
        /// 转换 DataTable 为 json
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns>json</returns>
        public static string TableToJson(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[ ");
            foreach (DataRow item in dt.Rows)
            {
                sb.Append("{");
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sb.AppendFormat(" \"{0}\": {1}, ", dt.Columns[i].ColumnName, ToString(item[i]));
                }
                sb.Length -= 2;
                sb.Append("},");
            }
            sb.Length--;
            sb.Append("]");
            return sb.ToString();
        }
        /// <summary>
        /// 转译字符
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static string ToString(object obj)
        {
            if (obj is DBNull || obj == null)
            {
                return "null";
            }
            else if (obj is bool)
            {
                bool? b = obj as bool?;
                if (b == null) return "null";
                return b.Value ? "true" : "false";
            }
            else if (obj is string)
            {
                string s = obj as string;
                s = s.Replace("\n", "\\\n");
                s = s.Replace("\"", "\\\"");
                return "\"" + s + "\"";
            }
            else if (obj is DateTime)
            {
                return "\"" + (obj as DateTime?).GetValueOrDefault().ToString("yyyy/MM/dd hh:mm:ss") + "\"";
            }
            else if (obj.GetType().IsValueType && obj is IFormattable)
            {
                return obj.ToString();
            }
            return "\"" + obj.ToString() + "\"";
        }
    }
}
