using System;
using Sbw.DbClinet;
namespace Esunnet.Model.Proc
{
    public class UP_UserLogin : Sbw.DbClinet.Proc
	{
		public string UserId { get; set; }
		public string Password { get; set; }
		public string Ip { get; set; }
		public UP_UserLogin(string userId, string password, string ip)
		{
			this.UserId = userId;
			this.Password = password;
			this.Ip = ip;
		}
		protected override System.Data.Common.DbParameter[] InitParameters(GetDbParameter get)
		{
			Input(get, System.Data.DbType.String, "@userId", UserId, 16);
			Input(get, System.Data.DbType.String, "@password", Password, 32);
			Input(get, System.Data.DbType.String, "@ip", Ip, 15);
			return base.InitParameters(get);
		}
		public override string ProcName
		{
			get { return "UP_UserLogin"; }
		}
	}
}

