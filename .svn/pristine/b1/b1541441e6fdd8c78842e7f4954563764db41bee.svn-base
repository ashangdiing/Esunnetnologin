using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sbw.DbClinet;

namespace Esunnet.Model.Proc
{
    public class UP_Simple : Sbw.DbClinet.Proc
	{
		private string procName;
		private Dictionary<string, object> inputParams = new Dictionary<string, object>();
		public Dictionary<string, object> InputParams { get { return inputParams; } }
		public UP_Simple(string procName)
		{
			this.procName = procName;
		}
		protected override System.Data.Common.DbParameter[] InitParameters(GetDbParameter get)
		{
			foreach (string key in inputParams.Keys)
			{
				Input(get, "@" + key, inputParams[key]);
			}
			return base.InitParameters(get);
		}
		public override string ProcName
		{
			get { return procName; }
		}
	}
}
