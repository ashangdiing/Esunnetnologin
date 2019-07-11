using Esunnet.Model.Table;
using Esunnet.Model.Tool.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esunnet.Record.Bll
{
    public interface IPermission : IBll
    {
        bool SetPermission(User u, List<PermLeaf> pl);
    }
}
