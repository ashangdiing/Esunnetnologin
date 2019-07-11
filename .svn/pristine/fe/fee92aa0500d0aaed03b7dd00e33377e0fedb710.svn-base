using Esunnet.Model.Frame;
using Esunnet.Model.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esunnet.Record.Bll
{
    public interface IUser : IBll
    {
        User Login(User u);
        bool UpdateUser(User u);
        bool UpdatePassword(User u);
        bool AddUser(User u);
        bool DeleteUser(User u);
        IGrid Find(IParameter<User> param);
        bool ResetUserPassword(User u);
    }
}
