using Esunnet.Model.Frame.Dhtmlx.Menu;
using Sbw.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Script.Serialization;

namespace Esunnet.Model.Frame.Dhtmlx.Grid
{
    public class GridConverter : JavaScriptConverter
    {
        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            return null;
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            PropertyInfo[] propertyInfo = obj.GetType().GetProperties();
            for (int i = 0; i < propertyInfo.Length; i++)
            {
                JSONAttribute json = (Attribute.GetCustomAttribute(propertyInfo[i], typeof(JSONAttribute)) as JSONAttribute) ?? new JSONAttribute();
                object objectValue = propertyInfo[i].GetGetMethod().Invoke(obj, null);
                if (!json.Show || !json.ShowNull && (objectValue == null || objectValue is DBNull || (objectValue is ICollection && (objectValue as ICollection).Count == 0)))
                {
                    continue;
                }
                else if (objectValue is DateTime)
                {
                    dic.Add(propertyInfo[i].Name, string.Format("new Date({0:yyyy},{0:MM},{0:dd},{0:HH},{0:mm},{0:ss})", objectValue));
                }
                else if (objectValue is List<object>)
                {
                    List<object> t = objectValue as List<object>;
                    for (int j = 0; j < t.Count; j++)
                    {
                        if (t[j] is DateTime)
                        {
                            t[j] = string.Format("{0:yyyy-MM-dd HH:mm:ss}", t[j]);
                        }
                    }
                    dic.Add(propertyInfo[i].Name, t);
                }
                else
                {
                    dic.Add(propertyInfo[i].Name, objectValue);
                }
            }
            return dic;
        }

        public override IEnumerable<Type> SupportedTypes
        {
            get { return new Type[] { typeof(Grid), typeof(Row), typeof(Node), typeof(Menu.Menu) }; }
        }
    }
}
