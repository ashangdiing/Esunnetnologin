using Esunnet.Model.Frame.Dhtmlx.Grid;
using Esunnet.Record.Bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Esunnet.Record.Biz
{
    public abstract class AToJson : IBll
    {
        public string ToJson(object o)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            jss.RegisterConverters(new JavaScriptConverter[] { new GridConverter() });
            return jss.Serialize(o);
        }

        public abstract void SetConfig(Bll.Setting.Config C);
    }
}
