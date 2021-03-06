﻿
namespace Esunnet.Phone.Phone.Enum
{
    public enum EventIDEnum
    {
        ID_SETAGENTSTATUS = 0x1001,
        ID_ANSWERCALL = 0x1002,
        ID_MAKECALL = 0x1004,
        ID_HOLDCALL = 0x1005,
        ID_RETRIEVECALL = 0x1006,
        ID_TRANSFERCALL = 0x1007,
        ID_CONFERENCECALL = 0x1008,
        ID_HANGUPCALL = 0x1009,
        ID_LEAVECONFERENCE = 0x100A,
        ID_CANCEL = 0x100B,
        ID_SETAPPDATA = 0x100C,
        ID_GETAPPDATA = 0x100D,
        ID_MONITOR = 0x100E,
        ID_CONSULTCALL = 0x1011,
        ID_COMPLETETRANSFER = 0x1012,
        ID_AGENTLOGGEDON = 0x2001,
        ID_AGENTLOGGEDOFF = 0x2002,
        ID_AGENTSTATUSCHANGED = 0x2003,
        ID_BACKINSERVICE = 0x2004,
        ID_OUTOFSERVICE = 0x2005,
        ID_CALLINFORMATION = 0x2006,
        ID_DESTBUSY = 0x2007,
        ID_DESTCHANGED = 0x2008,
        ID_DESTINVALID = 0x2009,
        ID_DESTNOTOBTAINABLE = 0x200A,
        ID_DESTSEIZED = 0x200B,
        ID_DIVERTED = 0x200C,
        ID_ERROR = 0x200D,
        ID_INBOUNDCALL = 0x200E,
        ID_OFFHOOK = 0x200F,
        ID_OPANSWERED = 0x2010,
        ID_OPCONFERENCED = 0x2011,
        ID_OPDISCONNECTED = 0x2012,
        ID_OPHELD = 0x2013,
        ID_OPRETRIEVED = 0x2014,
        ID_TPANSWERED = 0x2015,
        ID_TPCONFERENCED = 0x2016,
        ID_TPDISCONNECTED = 0x2017,
        ID_TPSUSPENDED = 0x2018,
        ID_TPRETRIEVED = 0x2019,
        ID_CALLIDLE = 0x201A,
        ID_TRANSFERED = 0x201B,
        ID_LINEREPLY = 0x201C,
        ID_DIALTONE = 0x201D,
        ID_UNAVAILABLE = 0x201E,
        ID_HOLDCONF = 0x201F,
        ID_UNMONITOR = 0x2020,
        ID_APPLOG = 0x2021,
        ID_NEWREQUEST = 0x2022,
        ID_DISPATCHREQUEST = 0x2023,
        ID_CANCELREQUEST = 0x2024,
        ID_DEVICESTATUS = 0x2025,
        ID_CONNECTIONSTATE = 0x2026,
        ID_INSTANTMESSAGE = 0x2027,
        ID_AGENTSTATUS = 0x2028,
        ID_CHANGEPASSWORD = 0x2029,
        ID_ONTIMER = 0x202A,
        ID_SOCKETBROKEN = 0x202B,
        ID_LEAVECAUSE = 0x202C,
        ID_PING = 0x202D,
        ID_TRACESETTING = 0x202E,
        ID_CALLDETAIL = 0x202F,
        ID_TIMEOUTED = 0x2030,
        ID_RINGBACK = 0x2031,
        ID_STOPMUSIC = 0x2032,
        ID_OPTRANSFERED = 0x2033,
        ID_SETTIMER = 0x2034,
        ID_HOLDCONFERENCE = 0x2035,
        ID_HOLDTRANSFER = 0x2036,
        ID_LOCKFREEAGENT = 0x2037,
        ID_TRANSFERTOAGENT = 0x2038,
        ID_ROUTEREQUEST = 0x2039,
        ID_TSAPIROUTE_END = 0x203A,
        ID_PREVIOUSSERVICEAGENT = 0x203B,
        ID_WAITFORSERVICEAGENT = 0x203C,
        ID_SERVICEAGENTRESULT = 0x203D,
        ID_GETCURRENTSTATUS = 0x203E,
        ID_CONTROLLERMONITOR = 0x203F,
    }
}