using Esunnet.Model.Table;
using Esunnet.Record.Bll;
using Sbw.Ajax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Esunnet.Record.Action
{
    /// <summary>
    /// RepotAction 的摘要说明
    /// </summary>
    public class RepotAction : Esunnet.Record.Biz.AjaxBiz<IReport, NULL>
    {

        public override void Init()
        {
            fun.Add("report", Report);
            fun.Add("queryReport", QueryReport);
        }
        private void Report()
        {
            Response.Write(Biz.ToJson(Biz.GetReport(Request["proc"], GetDate("datetime"), GetInt("type"))));
        }
        private void QueryReport()
        {
            Response.Write(Biz.ToJson(Biz.GetReport(Request["proc"], GetDate("begin"), GetDate("end"), GetInt("type"))));
        }
    }
}