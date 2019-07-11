using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using System.Web.SessionState;

namespace Sbw.Ajax
{
    public delegate void DoFunction();
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    public abstract class Ajax : IHttpHandler
    {
        protected Dictionary<string, DoFunction> fun = new Dictionary<string, DoFunction>();
        public abstract void Init();
        public virtual void Default() { }
        public HttpContext Context { get; set; }
        public HttpRequest Request { get; set; }
        public HttpResponse Response { get; set; }
        public HttpSessionState Session { get; set; }
        public void ProcessRequest(HttpContext context)
        {
            Init();
            Context = context;
            Request = context.Request;
            Response = context.Response;
            Session = context.Session;
            if (string.IsNullOrEmpty(context.Request["fun"]))
            {
                Default();
            }
            else
            {
                if (fun.ContainsKey(context.Request["fun"]))
                {
                    fun[context.Request["fun"]]();
                }
                else
                {
                    Response.Write(context.Request["fun"]+" -> 所对应的方法不存在,请注意大小写!");
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        /// <summary>
        /// 获取参数列表中的参数返回int
        /// </summary>
        /// <param name="name">参数名字</param>
        /// <returns>返回值</returns>
        public int? GetInt(string name)
        {
            try
			{
				if (string.IsNullOrEmpty(Request[name])) return null;
                return Convert.ToInt32(Request[name]);
            }
            catch { return null; }
        }
        public DateTime? GetDate(string name)
        {
            try
            {
                if (string.IsNullOrEmpty(Request[name])) return null;
                return Convert.ToDateTime(Request[name]);
            }
            catch { return null; }
        }
		public bool? GetBool(string name)
		{
			try
			{
				if (string.IsNullOrEmpty(Request[name])) return null;
                if ("on" == Request[name]) return true;
				return Convert.ToBoolean(Request[name]);
			}
			catch { return null; }
		}
		public string NotEmpty(string name)
		{
			if (!string.IsNullOrEmpty(Request[name])) return Request[name];
			return null;
		}
    }
}
