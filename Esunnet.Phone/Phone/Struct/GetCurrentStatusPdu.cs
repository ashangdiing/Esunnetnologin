using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Esunnet.Phone.Phone.Struct
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct GetCurrentStatusPdu
    {
        public UInt32 eventID;
        public int totalActiveOutboundAgents;
        public int freeOutboundAgents;
        public int freeBothAgents;
        public int totalIvrPorts;
        public int freeIvrPorts;
        public int inQueueNumber;
        public int threshold;
    }
}
