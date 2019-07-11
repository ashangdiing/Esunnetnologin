using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Esunnet.Record.Action
{
    /// <summary>
    /// Config 的摘要说明
    /// </summary>
    public class Config : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Write("var Config=");
            context.Response.Write(Sbw.Json.ToJson.Serialize(Esunnet.Tool.Setting.Biz.GetConfig()));
            context.Response.Write(";");


        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}