using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;

namespace Sbw.DbClinet
{
    public class DboException
    {
        private static FileInfo file { get { return new FileInfo(AppDomain.CurrentDomain.BaseDirectory + DateTime.Now.ToLongDateString() + ".log"); } }

        public DboException()
        {
        }
        public void Exception(string message)
        {
            throw new Exception(message);
        }
        public virtual void Log(DbException e, string sql, DbParameterCollection param)
        {
            if (!Sbw.DbClinet.Log.IsDebug)
            {
                if (e != null) throw e;
            }
            else
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder("参数: ");
                if (param != null && param.Count > 0)
                {
                    for (int i = 0; i < param.Count; i++)
                    {
                        if (i < param.Count - 1)
                        {
                            sb.AppendFormat("{0} = '{1}',", param[i].ParameterName, param[i].Value);
                        }
                        else
                        {
                            sb.AppendFormat("{0} = '{1}'", param[i].ParameterName, param[i].Value);
                        }
                    }

                }
                Sbw.DbClinet.Log.Error("异常类:{0},异常信息:{1},{2}", e.GetType().Name, e.Message, sb.ToString());
            }
        }
    }
}
