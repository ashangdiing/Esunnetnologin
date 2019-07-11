using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;

namespace Sbw.DbClinet
{
    public class Log
    {
        private static FileInfo file
        {
            get
            {
                if (HttpContext.Current != null && HttpContext.Current.Server != null)
                {
                    if (!Directory.Exists(HttpContext.Current.Server.MapPath("") + "\\log\\"))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath("") + "\\log\\");
                    }
                    return new FileInfo(HttpContext.Current.Server.MapPath("") + "\\log\\" + DateTime.Now.ToLongDateString() + ".log");
                }
                else
                {
                    return new FileInfo(AppDomain.CurrentDomain.BaseDirectory + DateTime.Now.ToLongDateString() + ".log");
                }
            }
        }
        private static int TYPE
        {
            get
            {
                if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["debug"]))
                    switch (System.Configuration.ConfigurationManager.AppSettings["debug"].ToLower())
                    {
                        case "debug": return 4;
                        case "warn": return 3;
                        case "info": return 2;
                        case "error": return 1;
                    }
                return 2;
            }
        }
        public static bool IsDebug
        {
            get { return TYPE == 4; }
        }
        public static void Debug(string message, params object[] args)
        {
            if (TYPE > 3)
            {
                Print("调试",string.Format(message, args));
            }
        }
        public static void Info(string message, params object[] args)
        {
            if (TYPE > 2)
            {
                Print("信息", string.Format(message, args));
            }
        }
        public static void Warn(string message, params object[] args)
        {
            if (TYPE > 1)
            {
                Print("警告",string.Format( message, args));
            }
        }
        public static void Error(string message, params object[] args)
        {
            if (TYPE > 0)
            {
                Print("错误", string.Format(message, args));
            }
        }
        private static void Print(string type, string message)
        {
            System.IO.StreamWriter sw;
            if (!file.Exists)
            {
                sw = file.CreateText();
            }
            else
            {
                sw = file.AppendText();
            }
            sw.WriteLine("{0:yyyy年MM月dd日 hh:mm:ss} {1}:{2}", DateTime.Now, type, message);
            //sw.WriteLine();
            sw.Flush();
            sw.Close();
        }
        public static void Pint(string message)
        {
            if (string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["debug"]))
            {
                System.IO.StreamWriter sw;
                if (!file.Exists)
                {
                    sw = file.CreateText();
                }
                else
                {
                    sw = file.AppendText();
                }
                sw.WriteLine("时间:{0:yyyy年MM月dd日 hh:mm:ss} 信息{1}", DateTime.Now, message);
                sw.WriteLine();
                sw.Flush();
                sw.Close();
            }
        }
    }
}
