using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Esunnet.Phone.Phone.Enum;

namespace Esunnet.Phone.Phone.Struct
{
    /*
     * 
#define	MAX_AGENTID			6
#define	MAX_GROUPID			6
#define	MAX_AGENTNAME		32
#define	MAX_PASSWORD		8
#define	MAX_DESCRIPTION		20
#define	MAX_ADDRESS			16
#define	MAX_APPDATA			1024/*/
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct AgentStatusPdu
    {
        public UInt32 eventID;//0
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
        public string agentName;//4
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
        public string agentID;//37
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 17)]
        public string deviceAddress;//44
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 35)]
        public string loginTime;//61
        public AgentStateEnum state;//96
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string cause;
    }
}
