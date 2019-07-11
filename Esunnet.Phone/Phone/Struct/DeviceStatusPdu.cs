using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Esunnet.Phone.Phone.Struct
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct DeviceStatusPdu
    {
        public uint eventID;
       // [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string deviceAddress;
        //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string type;
        //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string role;
       // [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string status;
    }
}
