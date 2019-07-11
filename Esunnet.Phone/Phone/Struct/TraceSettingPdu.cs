using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Esunnet.Phone.Phone.Struct
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TraceSettingPdu
    {
        public UInt32 eventID;
        public bool traceOn;
        public UInt32 traceSetting;
    }
}
