﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Esunnet.Phone.Phone.Struct
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct UnMonitorPdu
    {
        public UInt32 eventID;
        public string agentID;
        public string deviceID;
    }
}
