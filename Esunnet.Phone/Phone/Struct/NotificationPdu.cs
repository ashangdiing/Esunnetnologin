using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Esunnet.Phone.Phone.Enum;

namespace Esunnet.Phone.Phone.Struct
{
    [StructLayout(LayoutKind.Sequential,Pack=1)]
    public unsafe struct NotificationPdu
    {
        public long eventID;
        public long callID;
        public long oldcallID;
        public long secondcallID;
        public long device;//-->40
        public uint callState;
        public uint callType;//--->48----128-131

        public uint originalCallingPartyType;//
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]//152
        public string originalCallingParty;

        public uint originalCalledPartyType;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]//156-159
        public string originalCalledParty;

        public uint currentCallingPartyType;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
        public string currentCallingParty;

        public uint currentCalledPartyType;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
        public string currentCalledParty;//-128->

        public uint monitorPartyType;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string monitorParty;///----------->105+48/153-5 148

        public uint agentStatus;//--begin 152
        public uint isMonitor;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]//156-160
        public string agentID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1025)]
        public string agentData;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]//1195-1254
        public string agentGroupID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1025)]//2228-2231
        public string applicationData;//1195-1254
        public long function;//2220
        public byte asyncHandle;//2228-2231
        public ErrorCodeEnum errorCode;
    }
}
