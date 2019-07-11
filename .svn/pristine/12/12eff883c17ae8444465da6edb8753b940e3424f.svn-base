using Esunnet.Model.Frame;
using Esunnet.Model.Table;
using Esunnet.Record.Bll;
using Esunnet.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Esunnet.Record.Action
{
    /// <summary>
    /// Menu 的摘要说明
    /// </summary>
    public class MenuAction : Esunnet.Record.Biz.AjaxBiz<ISystemMenu, NULL>
    {
        public override void Default()
        {
            User u = new User();
            u.id = 1;
            Response.Write(Biz.ToJson(Biz.GetMenu(u)));
        }

        public override void Init()
        {
        }
    }
}