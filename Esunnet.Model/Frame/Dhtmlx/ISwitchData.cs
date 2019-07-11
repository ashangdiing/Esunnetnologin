using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esunnet.Model.Frame.Dhtmlx
{
    public interface ISwitchData
    {
        string Id { get; }
        string[] Data { get; }
        bool @checked { get; set; }
        object SwitchData(string type, object obj);
        string Header { get; }
        string ColAlign { get; }
        string ColTypes { get; }
        string ColSorting { get; }
        string Width { get; }
    }
}
