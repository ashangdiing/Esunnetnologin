using Esunnet.Model.Frame;
using Esunnet.Model.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esunnet.Record.Bll
{
    public interface ISystemMenu : IBll
    {
        IMenu GetMenu(User u);
    }
}
