using System;
using Sbw.DbClinet;
namespace Esunnet.Model.Proc
{
    public class UP_AddUser : Sbw.DbClinet.Proc
    {
        public int? Id { get; set; }
		public string UserId { get; set; }
		public string Passowrd { get; set; }
		public string UserName { get; set; }
		public string AgentId { get; set; }
		public int? PermId { get; set; }
		public string Permission { get; set; }
		protected override System.Data.Common.DbParameter[] InitParameters(GetDbParameter get)
        {
            Esunnet.Model.Table.User u = new Table.User();
            Input(get, System.Data.DbType.Int32, "@id", Id, 4);
			Input(get, System.Data.DbType.String, "@userId", UserId, 16);
			Input(get, System.Data.DbType.String, "@password", Passowrd, 32);
			Input(get, System.Data.DbType.String, "@userName", UserName, 20);
			Input(get, System.Data.DbType.String, "@agentId", AgentId, 10);
			Input(get, System.Data.DbType.Int32, "@permId", PermId, 4);
			Input(get, System.Data.DbType.String, "@permission", Permission, 2000);
			return base.InitParameters(get);
		}
		public override string ProcName
		{
			get { return "UP_AddUser"; }
		}
	}
}

