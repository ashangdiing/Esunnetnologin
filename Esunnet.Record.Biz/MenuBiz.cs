using Esunnet.Model.Frame;
using Esunnet.Model.Frame.Dhtmlx.Grid;
using Esunnet.Model.Frame.Dhtmlx.Menu;
using Esunnet.Model.Proc;
using Esunnet.Record.Bll;
using Sbw.DbClinet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Esunnet.Record.Biz
{
    internal class MenuBiz : AToJson, ISystemMenu
    {
        private Bll.Setting.Config C;
        public IMenu GetMenu(Model.Table.User u)
        {
            Esunnet.Model.Frame.IMenu m = new Esunnet.Model.Frame.Dhtmlx.Menu.Menu();
            UP_GetMenu us = new UP_GetMenu(u.id, u.permission);
            using (DbClinet db = new DbClinet())
            {
                List<Dictionary<string, object>> mv = db.Select(us);
                for (int i = 0; i < mv.Count; i++)
                {
                    Node n = new Node();
                    n.id = mv[i]["id"];
                    n.pid = mv[i]["pid"];
                    n.text = mv[i]["text"] as string;
                    n.title = mv[i]["title"] as string;
                    n.type = "node";
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    dic.Add(C.FUN, mv[i]["function"] as string);
                    dic.Add(C.PATH, mv[i]["path"] as string);
                    dic.Add(C.PARAM, mv[i]["remark"] as string);
                    n.userdata = dic;
                    m.Add(n);
                }
            }
            return m;
        }

        public override void SetConfig(Bll.Setting.Config C)
        {
            this.C = C;
        }
    }
}
