﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Esunnet.Phone.Phone.Struct
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PreviousServiceAgentPdu
    {
        public UInt32 eventID;
        public int callID;
        public string callingNbr;
        public string agentID;
        public int timeout;
    }
}
