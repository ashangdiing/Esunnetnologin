using System;
using Sbw.DbClinet;
namespace Esunnet.Model.Proc
{
    public class UP_FindUserList : Sbw.DbClinet.Proc
	{
		public int? Begin { get; set; }
		public int? End { get; set; }
		public string UserId { get; set; }
		public string UserName { get; set; }
		public string AgentId { get; set; }
		public int? PermId { get; set; }
		public bool? IsDelete { get; set; }
		protected override System.Data.Common.DbParameter[] InitParameters(GetDbParameter get)
		{
			Input(get, System.Data.DbType.Int32, "@begin", Begin, 4);
			Input(get, System.Data.DbType.Int32, "@end", End, 4);
			Input(get, System.Data.DbType.String, "@userId", UserId, 16);
			Input(get, System.Data.DbType.String, "@userName", UserName, 20);
			Input(get, System.Data.DbType.String, "@agentId", AgentId, 10);
			Input(get, System.Data.DbType.Int32, "@permId", PermId, 4);
			Input(get, System.Data.DbType.Boolean, "@isDelete", IsDelete, 1);
			return base.InitParameters(get);
		}
		public override string ProcName
		{
			get { return "UP_FindUserList"; }
		}
	}
}

