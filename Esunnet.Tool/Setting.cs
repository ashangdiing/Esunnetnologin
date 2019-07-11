using Esunnet.Record.Bll;
using Esunnet.Record.Bll.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Esunnet.Tool
{
    public class Setting
    {
        public static ISetting Biz {  get; private set; }
        //public string HasP
        static Setting()
        {
            string[] s = System.Configuration.ConfigurationManager.AppSettings["BizSetting"].Split(',');
            Assembly ass = Assembly.Load(s[0]);
            Biz = (ISetting)ass.CreateInstance(s[1]);
        }
    }
}
