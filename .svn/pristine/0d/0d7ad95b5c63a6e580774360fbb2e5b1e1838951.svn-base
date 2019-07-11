using Esunnet.Record.Bll;
using Sbw.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Esunnet.Record.Action
{
    /// <summary>
    /// RecordAction 的摘要说明
    /// </summary>
    public class RecordAction : Esunnet.Record.Biz.AjaxBiz<IRecord, Esunnet.Model.Table.Record>
    {
        public override void Init()
        {
            fun.Add("find", Find);
        }

        private void Find()
        {
            Response.Write(Biz.ToJson(Biz.FindRecord(null, Parameter)));
        }
    }
}