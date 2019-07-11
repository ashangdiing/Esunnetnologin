using Esunnet.Model.Clinet;
using Sbw.DbClinet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esunnet.Model.Table
{
    public class User : Esunnet.Model.DataBase.SYS_User,IView
    {   public int State { get; set; }
        public UserInfo UserInfo { get; set; }
        public User()
        {

        }

        public object this[string name]
        {
            set
            {
                switch (name)
                {
                    case "userId": this.userId = value as string; break;
                    case "userPassword": this.userPassword = value as string; break;
                }
            }
        }

        public string ViewName
        {
            get { return "SYS_User"; }
        }
    }
}
