using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

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
#define	MAX_APPDATA			1024
     */
    [StructLayout(LayoutKind.Sequential)]
    public struct SetAgentStatusPdu
    {
        public UInt32 eventID;//0
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
        public string agentID;//4
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
        public string agentGroupID;//37
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 52)]
        public string deviceAddress;//44
        public int state;//96
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 60)]
        public string password;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 60)]
        public string description;
    }
}
