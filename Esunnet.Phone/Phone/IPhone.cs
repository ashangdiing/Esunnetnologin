﻿using Esunnet.Phone.Phone.Enum;
using Esunnet.Phone.Phone.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esunnet.Phone.Phone
{
    public interface IPhone
    {
        string ConnectionId { get; set; }
        AgentList List { get; }
        Dictionary<string, IPhone> User { get ; }
        void Error(string arg1);
        void AgentPhone_InboundCall(string arg1, string arg2, string arg3, string arg4, string arg5, int arg6, string arg7);
        void AgentPhone_DestInvalid();
        void AgentPhone_DestBusy();
        void AgentPhone_DeviceStatus(string arg1, string arg2);
        void AgentPhone_Transfered();
        void AgentPhone_TextMessage(string arg1, string arg2, string arg3);
        void AgentPhone_Conferenced();
        void AgentPhone_DialTone();
        void AgentPhone_CallIdle();
        void AgentPhone_Answered(uint obj);
        void AgentPhone_CallInformation(string arg1, string arg2, string arg3, string arg4, string arg5, int arg6);
        void AgentPhone_UnknownEvent();
        void AgentPhone_AgentStatistics(string arg1, string arg2);
        void AgentPhone_AgentStatusCNGed(uint obj);
        void AgentPhone_AgentLogout(string arg1, long arg2, long arg3);
        void AgentPhone_AgentLogin(string arg1, long arg2, long arg3);
        void AgentPhone_AgentStatus(string arg1, string arg2, string arg3, string arg4, AgentStateEnum arg5, string arg6);
        void AgentPhone_ConnectionReady();
        void AgentPhone_ConnectionBroken();
        void AddEvent();
        void RemoveEvent();
    }
}
