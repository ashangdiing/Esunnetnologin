using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Esunnet.Record.Bll;
using Esunnet.Model.Proc;
using Sbw.DbClinet;
namespace Esunnet.Record.Biz
{
    public class User :IUser
    {
        public Model.Table.User Login(string userId, string password, string ip)
        {

            using (Sbw.DbClinet.DbClinet Client = new Sbw.DbClinet.DbClinet())
            {
                UP_UserLogin up = new UP_UserLogin(userId, password, ip);
                Esunnet.Model.Table.User user = Client.Select<Esunnet.Model.Table.User>(up).FirstOrDefault();
                if (user == null)
                {
                    user = new Esunnet.Model.Table.User();
                    user.State = up.Return;
                }
                else
                {
                    user.State = 1;
                }
                return user;
            }
        }

     public int UpdatePassword(string userId, string oldPassword, string newPassword)
		{
			using (Sbw.DbClinet.DbClinet Client = new Sbw.DbClinet.DbClinet())
			{
				return Client.ExecuteNonQuery(new UP_UpdateUserPassword(userId, oldPassword, newPassword));
			}
		}


		public string FindUserList(int begin, int end, string userId, string userName, string agentId, int? permId, bool? isDelete)
		{
			UP_FindUserList up = new UP_FindUserList();
			up.Begin = begin;
			up.End = end;
			up.AgentId = agentId;
			up.PermId = permId;
			up.IsDelete = isDelete;
			/*Data data = new Data();
			using (Sbw.DbClinet.DbClinet Client = new Sbw.DbClinet.DbClinet())
			{
				data.topics = Client.Select(up);
				data.totalCount = up.Return;
			}
			return Sbw.DbClient.ToJson.Serialize(data);
             */
            return null;
		}

		public int AddUser(int? id,string userId, string passowrd, string userName, string agentId, int permId, string permission)
		{
			UP_AddUser up = new UP_AddUser();
            up.Id = id;
			up.AgentId = agentId;
			up.Passowrd = passowrd;
			up.UserName = userName;
			up.UserId = userId;
			up.PermId = permId;
			up.Permission = permission;
			using (Sbw.DbClinet.DbClinet Client = new Sbw.DbClinet.DbClinet())
			{
				return Client.ExecuteNonQuery(up);
			}
		}
        public int EditUserRecordPermission(string userId,string exts,string agents)
        {
            UP_Simple up = new UP_Simple("UP_RecordPermission");
            up.InputParams.Add("userId", userId);
            up.InputParams.Add("exts", exts);
            up.InputParams.Add("agents", agents);
            using (Sbw.DbClinet.DbClinet Client = new Sbw.DbClinet.DbClinet())
            {
                return Client.ExecuteNonQuery(up);
            }
        }
          #region  
        public bool Delete(int id)
        {
            UP_Simple up = new UP_Simple("UP_DeleteUser");
            up.InputParams.Add("id", id);
            using (Sbw.DbClinet.DbClinet Client = new Sbw.DbClinet.DbClinet())
            {
                return Client.ExecuteNonQuery(up)>0;
            }
        }
        #endregion


        public bool UpdateUser(Model.Table.User u)
        {
            throw new NotImplementedException();
        }

        public bool UpdatePassword(Model.Table.User u)
        {
            throw new NotImplementedException();
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

        public void SetConfig(Bll.Setting.Config C)
        {
            throw new NotImplementedException();
        }

        public string ToJson(object o)
        {
            throw new NotImplementedException();
        }
    }
    
}
