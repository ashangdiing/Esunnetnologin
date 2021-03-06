﻿using Esunnet.Model.Proc;
using Esunnet.Model.Table;
using Esunnet.Record.Bll;
using Sbw.DbClinet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esunnet.Record.Biz
{
    class UserBiz : AToJson,IUser
    {
        private Bll.Setting.Config C;
        public User Login(Model.Table.User u)
        {
            String pw = u.userPassword;
            u.userId = "admin";
          //  u.userPassword = "admin";
            using (DbClinet db = new DbClinet())
            {
                db.AddParameter("@userId", u.userId);
                List<Dictionary<string, object>> mv = db.Select("select * from SYS_User where userId=@userId");

                if (mv.Count > 0)
                {
                    string userPasswordTemp = mv.FirstOrDefault()["userPassword"] as string;
                    if (userPasswordTemp.Equals(u.userPassword, StringComparison.OrdinalIgnoreCase))
                    {
                        //u.lastLoginIp = mv.FirstOrDefault()["lastLoginIp"] as string;
                        u.State = 1;
                        db.AddParameter("@userId", u.userId);
                        db.AddParameter("@ip", u.lastLoginIp);
                        db.ExecuteNonQuery("update SYS_User set lastLoginTime=GETDATE(),lastLoginIp=@ip  where userId=@userId");
                    }
                    else
                    {
                        //密码错误
                        u.State = -1;
                    }

                }
                else u.State = -2;//不存在用户
            }
            return u;
        }

        public bool UpdateUser(Model.Table.User u)
        {
            throw new NotImplementedException();
        }

        public bool UpdatePassword(Model.Table.User u)
        {
            using (DbClinet db = new DbClinet())
            {
                db.AddParameter("@userPassword", u.userPassword);
                db.AddParameter("@userId", u.userId);
                db.ExecuteNonQuery("update SYS_User set userPassword=@userPassword  where userId=@userId");
                return true;
            }
        }

        public bool AddUser(Model.Table.User u)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUser(Model.Table.User u)
        {
            throw new NotImplementedException();
        }

        public Model.Frame.IGrid Find(Model.Frame.IParameter<Model.Table.User> param)
        {
            throw new NotImplementedException();
        }

        public bool ResetUserPassword(Model.Table.User u)
        {
            throw new NotImplementedException();
        }
        public override void SetConfig(Bll.Setting.Config C)
        {
            this.C = C;
        }
    }
}
