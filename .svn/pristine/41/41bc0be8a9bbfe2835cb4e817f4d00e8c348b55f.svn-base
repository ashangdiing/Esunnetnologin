using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Esunnet.Phone.Phone.Struct
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ApplicationLogPdu
    {
        public UInt32 eventID;
        public string agentID;
        public UInt32 logLevel;	// 0: eroror, 1 warning, 2 trace, 3 always 
        public string info;
    }
}
