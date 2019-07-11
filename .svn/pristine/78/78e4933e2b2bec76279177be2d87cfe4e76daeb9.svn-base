using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sbw.DbClinet
{
    /// <summary>
    /// 单利模式使用类
    /// </summary>
    /// <typeparam name="T">需要单利的对象 必须有默认的构成方法</typeparam>
    public class Singleton<T> where T : new()
    {
        [ThreadStatic]
        private static T threadObj;
        private static T obj;
        /// <summary>
        /// 获取单利对象
        /// </summary>
        /// <returns></returns>
        [STAThread]
        public static T Instance()
        {
            if (obj == null)
            {
                //obj = (T)typeof(T).GetConstructor(new Type[]{}).Invoke(null);
                obj = new T();
            }
            return (T)obj;
        }

        [STAThread]
        public static T Instance(params object[] param)
        {
            if (obj == null)
            {
                Type[] ts = new Type[param.Length];
                for (int i = 0; i < ts.Length; i++)
                {
                    ts[i] = param[i].GetType();
                }
                obj = (T)typeof(T).GetConstructor(ts).Invoke(param);
            }
            return obj;
        }
        /// <summary>
        /// 相同进程获取单利对象
        /// </summary>
        /// <returns></returns>
        [STAThread]
        public static T ThreadInstance()
        {
            if (threadObj == null)
            {
                //threadObj = (T)typeof(T).GetConstructor(null).Invoke(null);
                threadObj = new T();
            }
            return threadObj;
        }

        [STAThread]
        public static T ThreadInstance(params object[] param)
        {
            if (threadObj == null)
            {
                Type[] ts = new Type[param.Length];
                for (int i = 0; i < ts.Length; i++)
                {
                    ts[i] = param[i].GetType();
                }
                threadObj = (T)typeof(T).GetConstructor(ts).Invoke(param);
            }
            return threadObj;
        }
        /// <summary>
        /// 用于Web 单例模式
        /// </summary>
        /// <returns></returns>
        [STAThread]
        public static T WebInstance()
        {
            if (HttpContext.Current.Items[typeof(T)] == null)
            {
                //HttpContext.Current.Items[typeof(T)] = (T)typeof(T).GetConstructor(null).Invoke(null);
                HttpContext.Current.Items[typeof(T)] = new T();
            }
            return (T)HttpContext.Current.Items[typeof(T)];
        }
        [STAThread]
        public static T WebInstance(params object[] param)
        {
            if (HttpContext.Current.Items[typeof(T)] == null)
            {
                Type[] ts = new Type[param.Length];
                for (int i = 0; i < ts.Length; i++)
                {
                    ts[i] = param[i].GetType();
                }
                HttpContext.Current.Items[typeof(T)] = typeof(T).GetConstructor(ts).Invoke(param);
            }
            return (T)HttpContext.Current.Items[typeof(T)];
        }
    }
}
