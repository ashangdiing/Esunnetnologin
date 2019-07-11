using Esunnet.Record.Bll;
using Esunnet.Record.Bll.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esunnet.Record.Biz
{
    public class Setting : ISetting
    {
        private Config c = new Config();
        public Setting()
        {
            c.HasPermission = true;
        }
        public T GetBean<T>() where T : IBll
        {
            IBll e = null;
            if (typeof(T).Equals(typeof(IExt)))
            {
                e = new ExtBiz();
            }
            else if (typeof(T).Equals(typeof(IPermission)))
            {

            }
            else if (typeof(T).Equals(typeof(ISystemMenu)))
            {
                e = new MenuBiz();
            }
            else if (typeof(T).Equals(typeof(IRecord)))
            {
                e = new RecordBiz();
            }
            else if (typeof(T).Equals(typeof(ISystemMenu)))
            {

            }
            else if (typeof(T).Equals(typeof(IUser)))
            {
                e = new UserBiz();
            }
            else if (typeof(T).Equals(typeof(IReport)))
            {
                e = new ReportBiz();
            }
            if (e != null)
            {
                e.SetConfig(c);
            }
            return (T)e;
        }


        public T GetWebServiceBean<T>()
        {
            object o = null;
            if (typeof(T).Equals(typeof(Bll.WebService.IRecord)))
            {
                o = new WebService.Record();
            }
            return (T)o;
        }


        public Config GetConfig()
        {
            return c;
        }
    }
}
