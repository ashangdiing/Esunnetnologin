using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;

namespace Sbw.DbClinet
{
    public delegate DbParameter GetDbParameter();
    public abstract class Proc
    {
        public abstract string ProcName { get; }
        public int type { get; set; }
        public Int32 Return { get; set; }
        protected Dictionary<string, DbParameter> param = new Dictionary<string, DbParameter>();
        /// <summary>
        /// 添加输入参数
        /// </summary>
        /// <param name="get">获得参数类型</param>
        /// <param name="type">数据库类型</param>
        /// <param name="name">参数名称</param>
        /// <param name="value">值</param>
        protected void Input(GetDbParameter get, DbType type, string name, object value, int size)
        {
            if (value != null)
            {
                DbParameter p = get();
                p.Direction = ParameterDirection.Input;
                p.DbType = type;
                p.ParameterName = name;
                p.Value = value;
                p.Size = size;
                param.Add(name, p);
            }
        }
		protected void Input(GetDbParameter get, string name, object value)
		{
			if (value != null)
			{
				DbParameter p = get();
				p.Direction = ParameterDirection.Input;
				p.ParameterName = name;
				p.Value = value;
				param.Add(name, p);
			}
		}
        protected void Output(GetDbParameter get, DbType type, string name)
        {
            DbParameter p = get();
            p.Direction = ParameterDirection.Output;
            p.DbType = type;
            p.ParameterName = name;
            param.Add(name, p);
        }
        protected void InputOutput(GetDbParameter get, DbType type, string name, object value, int size)
        {
            DbParameter p = get();
            p.Direction = ParameterDirection.InputOutput;
            p.DbType = type;
            p.ParameterName = name;
            p.Value = value;
            p.Size = size;
            param.Add(name, p);
        }
        private DbParameter[] GetParam()
        {
            DbParameter[] ps = new DbParameter[param.Values.Count];
            int i = 0;
            foreach (DbParameter item in param.Values)
            {
                ps[i++] = item;
            }
            return ps;
        }
        internal protected virtual DbParameter[] InitParameters(GetDbParameter get)
        {
            DbParameter p = get();
            p.Direction = ParameterDirection.ReturnValue;
            p.DbType = DbType.Int32;
            p.ParameterName = "@Return";
            param.Add("@Return", p);
            return GetParam();
        }
        internal protected virtual void InitOutput()
        {
            Return = (param["@Return"].Value as int?).GetValueOrDefault();
        }
    }
}
