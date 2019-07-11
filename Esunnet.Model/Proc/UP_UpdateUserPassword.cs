using System;
using Sbw.DbClinet;
namespace Esunnet.Model.Proc
{
    public class UP_UpdateUserPassword : Sbw.DbClinet.Proc
	{
		public string UserId { get; set; }
		public string Oldpassword { get; set; }
		public string Newpassword { get; set; }
		public UP_UpdateUserPassword(string userId, string oldPassword, string newPassword)
		{
			this.UserId = userId;
			this.Oldpassword = oldPassword;
			this.Newpassword = newPassword;
		}
		protected override System.Data.Common.DbParameter[] InitParameters(GetDbParameter get)
		{
			Input(get, System.Data.DbType.String, "@userId", UserId, 16);
			Input(get, System.Data.DbType.String, "@oldpassword", Oldpassword, 32);
			Input(get, System.Data.DbType.String, "@newpassword", Newpassword, 32);
			return base.InitParameters(get);
		}
		public override string ProcName
		{
			get { return "UP_UpdateUserPassword"; }
		}
	}
}

