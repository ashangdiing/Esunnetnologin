using System;
using Sbw.DbClinet;
namespace Esunnet.Model.Proc
{
    public class UP_GetMenu : Sbw.DbClinet.Proc
	{
		public int? Id { get; set; }
		public string Permission { get; set; }
		public UP_GetMenu(int id,string permission)
		{
			this.Id = id;
			Permission = permission;
		}

		protected override System.Data.Common.DbParameter[] InitParameters(GetDbParameter get)
		{
			Input(get, System.Data.DbType.Int32, "@id", Id, 4);
			Input(get, System.Data.DbType.String, "@permission", Permission, 2000);
			return base.InitParameters(get);
		}
		public override string ProcName
		{
			get { return "UP_GetMenu"; }
		}
	}
}

