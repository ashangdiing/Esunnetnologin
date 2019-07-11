using Esunnet.Model.Frame;
using Sbw.DbClinet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esunnet.Record.Bll
{
    public interface IAjax<T, M>
        where T : IBll
        where M : IView, new()
    {
        T Biz { get; }
        IParameter<M> Parameter { get; }
    }
}
