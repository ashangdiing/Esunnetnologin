using Esunnet.Record.Bll.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esunnet.Record.Bll
{
    public interface IBll
    {
        void SetConfig(Config C);
        string ToJson(object o);
    }
}
