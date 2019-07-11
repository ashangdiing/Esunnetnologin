
using Sbw.DbClinet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Esunnet.Model.Frame.Dhtmlx
{
    public class Parameter<T> : Esunnet.Model.Frame.IParameter<T> where T :IView, new()
    {
        public string GetString(string name)
        {
            return HttpContext.Current.Request[name];
        }
        public int? GetInt(string name)
        {
            string s = GetString(name);
            if (string.IsNullOrEmpty(s)) { return null; }
            try { return int.Parse(s); }
            catch { return null; }
        }
        public bool? GetBool(string name)
        {
            string s = GetString(name);
            if (string.IsNullOrEmpty(s)) { return null; }
            try { return bool.Parse(s); }
            catch { return null; }
        }
        public U? Get<U>(string name) where U : struct
        {
            string s = GetString(name);
            if (string.IsNullOrEmpty(s)) { return null; }
            try { return (U)Convert.ChangeType(s, typeof(U)); }
            catch { return null; }
        }
        public string GetNullString(string name)
        {
            string s = GetString(name);
            if (string.IsNullOrEmpty(s)) { return null; }
            return s;
        }
        public Parameter()
        {
            begin = GetInt("posStart") ?? 0;
            count = GetInt("count") ?? 20;
            this.t = new T();
            foreach (string item in HttpContext.Current.Request.Params.AllKeys)
            {
                t[item] = HttpContext.Current.Request.Params[item];
            }
        }
        public int begin { get; set; }
        public int count { get; set; }
        public T t { get; private set; }


        public IGrid GetGrid()
        {
            return new Grid.Grid(begin);
        }
    }
}
