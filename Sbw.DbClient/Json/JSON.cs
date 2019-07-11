using System;
using System.Reflection;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Text.RegularExpressions;
namespace Sbw.Json
{
    public class JSON
    {
        public static string ToJSON(object obj)
        {
            if (obj is IDictionary)
            {
                return _ObjectToJSON(obj);
            }
            else if (obj is IEnumerable)
            {
                return ArrayToJSON(obj as IEnumerable);
            }
            else if (obj is DataTable)
            {
                return DataTableToJSON(obj as DataTable);
            }
            else if (obj is DbDataReader)
            {
                return DataReaderToJSON(obj as DbDataReader);
            }
            else if (obj is DataSet)
            {
                return DataSetToJSON(obj as DataSet);
            }
            else
            {
                return _ObjectToJSON(obj);
            }
        }
        private static string _ObjectToJSON(object item)
        {
            StringBuilder sb = new StringBuilder();
            if (item is DBNull || item == null)
            {
                sb.Append("null");
            }
            if (item is string || item is Guid || item is TimeSpan)
            {
                sb.AppendFormat("\"{0}\"",item);//Regex.Replace(item.ToString(), "\\\\", "\\\\")
            }
            else if (item is IDictionary)
            {
                IDictionary id = (IDictionary)item;
                sb.Append("{ ");
                foreach (object o in id.Keys)
                {
                    sb.AppendFormat("{0}:{1}", _ObjectToJSON(o), _ObjectToJSON(id[o]));
                    sb.Append(",");
                }
                sb.Length--;
                sb.Append("}");
            }
            else if (item is bool)
            {
                sb.AppendFormat("{0}", ((bool)item) ? "true" : "false");
            }
            else if (item is IEnumerable)
            {
                sb.AppendFormat("{0}", ArrayToJSON(item as IEnumerable));
            }
            else if (item is DateTime)
            {
                sb.AppendFormat("new Date({0:yyyy},{0:MM},{0:dd},{0:HH},{0:mm},{0:ss})", item);
            }
            else if (item is Function || item is IConvertible)
            {
                sb.AppendFormat("{0}", item);
            }
            else
            {
                sb.Append(ObjectToJSON(item));
            }
            return sb.ToString();
        }
        public static string ArrayToJSON(IEnumerable array)
        {
            StringBuilder sb = new StringBuilder("[ ");
            foreach (object item in array)
            {
                sb.Append(_ObjectToJSON(item));
                sb.Append(",");
            }
            sb.Length--;
            sb.Append("]");
            return sb.ToString();
        }
        public static string DataTableToJSON(DataTable table)
        {
            StringBuilder sb = new StringBuilder("[ ");
            DataRowCollection drc = table.Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                sb.Append("{ ");
                foreach (DataColumn column in table.Columns)
                {
                    if (column.DataType == typeof(string))
                    {
                        sb.AppendFormat("\"{0}\":\"{1}\"", column.ColumnName, drc[i][column.ColumnName]);
                    }
                    else if (column.DataType == typeof(DateTime))
                    {
                        sb.AppendFormat("\"{0}\":new Date({1:yyyy},{1:MM},{1:dd},{1:HH},{1:mm},{1:ss})", column.ColumnName, drc[i][column.ColumnName]);
                    }
                    else
                    {
                        sb.AppendFormat("\"{0}\":{1}", column.ColumnName, drc[i][column.ColumnName]);
                    }
                    sb.Append(",");
                }
                sb.Length--;
                sb.Append("}");
            }
            sb.Append("]");
            return sb.ToString();
        }
        public static string DataReaderToJSON(DbDataReader dataReader)
        {
            StringBuilder sb = new StringBuilder("[ ");
            while (dataReader.Read())
            {
                sb.Append("{ ");
                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    if (dataReader.GetFieldType(i) == typeof(string))
                    {
                        sb.AppendFormat("\"{0}\":\"{1}\"", dataReader.GetName(i), dataReader[i]);
                    }
                    else if (dataReader.GetFieldType(i) == typeof(DateTime))
                    {
                        sb.AppendFormat("\"{0}\":new Date({1:yyyy},{1:MM},{1:dd},{1:HH},{1:mm},{1:ss})", dataReader.GetName(i), dataReader[i]);
                    }
                    else
                    {
                        sb.AppendFormat("\"{0}\":{1}", dataReader.GetName(i), dataReader[i]);
                    }
                    sb.Append(",");
                }
                sb.Length--;
                sb.Append("}");
            }
            dataReader.Close();
            return sb.ToString();
        }
        /// <summary>
        /// DataSet转换为Json
        /// </summary>
        /// <param name="dataSet">DataSet对象</param>
        /// <returns>Json字符串</returns>
        public static string DataSetToJSON(DataSet dataSet)
        {

            StringBuilder sb = new StringBuilder("{ ");
            foreach (DataTable table in dataSet.Tables)
            {
                sb.AppendFormat("\"{0}\":{1}", table.TableName, DataTableToJSON(table));
                sb.Append(",");
            }
            sb.Length--;
            sb.Append("}");
            return sb.ToString();
        }
        public static string ObjectToJSON(object obj)
        {
            if (obj == null) { return "null"; }
            StringBuilder sb = new StringBuilder("{ ");
            PropertyInfo[] propertyInfo = obj.GetType().GetProperties();
            for (int i = 0; i < propertyInfo.Length; i++)
            {
                JSONAttribute json = (Attribute.GetCustomAttribute(propertyInfo[i], typeof(JSONAttribute)) as JSONAttribute) ?? new JSONAttribute();
                object objectValue = propertyInfo[i].GetGetMethod().Invoke(obj, null);
                if (!json.Show || !json.ShowNull && (objectValue == null || objectValue is DBNull || (objectValue is IList && (objectValue as IList).Count == 0)))
                {
                    continue;
                }
                sb.AppendFormat("\"{0}\":{1}", propertyInfo[i].Name, _ObjectToJSON(objectValue));
                sb.Append(",");
            }
            sb.Length--;
            sb.Append("}");
            return sb.ToString();
        }
    }
}

