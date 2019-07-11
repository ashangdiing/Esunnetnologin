using Esunnet.Record.Bll.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Esunnet.Record.WebService
{
    /// <summary>
    /// Record 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class Record : System.Web.Services.WebService
    {
        private IRecord ir = Esunnet.Tool.Setting.Biz.GetWebServiceBean<IRecord>();
        [WebMethod]
        public string GetDevice(string ip)
        {
            return ir.GetDevice(ip);
        }
        [WebMethod]
        public string[] GetRecordByCallId(string callid)
        {
            return ir.GetRecordByCallId(callid);
        }
        [WebMethod]
        public string GetRecordByTsId(string tsid)
        {
            return ir.GetRecordByTsId(tsid);
        }
    }
}
