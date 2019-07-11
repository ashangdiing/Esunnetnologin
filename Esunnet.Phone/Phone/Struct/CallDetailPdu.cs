using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Esunnet.Phone.Phone.Struct
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CallDetailPdu
    {
        public UInt32 eventID;
        public string callingNumber;
        public string agentID;
        public string fromRoutePoint;
        public string toRoutePoint;
        public string beginTime;
        public string pickupTime;
        public string endTime;
        public bool isInbound;
    }
}
