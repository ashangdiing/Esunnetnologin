using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Esunnet.Phone.Phone.Struct
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct RouteRequestPdu
    {
        public UInt32 eventID;
        public UInt32 callID;
        public long routeRegisterReqID;
        public long routingCrossRefID;
        public string routepoint;
        public string calling;
    }
}
