using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esunnet.Model.Frame
{
    public interface INode
    {
        object id { get; set; }
        object pid { get; set; }
        bool Add(INode node);
    }
}
