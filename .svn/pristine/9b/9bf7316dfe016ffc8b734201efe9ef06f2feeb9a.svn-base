using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sbw.DbClinet;

namespace Esunnet.Model.Proc
{
    public class UP_AddOrUpdateExt : Sbw.DbClinet.Proc
    {
		public string Ext { get; set; }
        public string UserIp { get; set; }
        public string Ip { get; set; }
        public string Mac { get; set; }
        public string UserName { get; set; }
        public UP_AddOrUpdateExt(string Ext, string UserIp, string ip, string Mac, string UserName)
		{
            this.Ext = Ext;
            this.UserIp = UserIp;
            this.Ip = ip;
            this.Mac = Mac;
            this.UserName = UserName;
		}
		protected override System.Data.Common.DbParameter[] InitParameters(GetDbParameter get)
		{
            /*
             *  @ext varchar(10),
             *  @userIp varchar(15)=null,
             *  @ip varchar(15)=null,
             *  @mac char(17)=null,
             *  @userName varchar(20)
             */
            Input(get, System.Data.DbType.String, "@ext", Ext, 10);
            Input(get, System.Data.DbType.String, "@userIp", UserIp, 15);
            Input(get, System.Data.DbType.String, "@ip", Ip, 15);
            Input(get, System.Data.DbType.String, "@mac", Mac, 17);
            Input(get, System.Data.DbType.String, "@userName", UserName, 20);
			return base.InitParameters(get);
		}
		public override string ProcName
		{
            get { return "UP_AddOrUpdateExt"; }
		}
    }
}
