using Esunnet.Phone.Phone;
using Esunnet.Phone.Phone.Enum;
using Esunnet.Phone.Phone.Util;
using SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Esunnet.Phone.Phone.Web
{
    public class WebPhone : PersistentConnection, IPhone
    {
        public static Dictionary<string, IPhone> user = new Dictionary<string, IPhone>();
        public static AgentList al = new AgentList();

        public AgentList List { get { return al; } }
        public Dictionary<string, IPhone> User { get { return user; } }

        public string ConnectionId { get; set; }
        public WebPhone()
        {
        }
        //private void S(object o) { }
        protected override Task OnReceivedAsync(IRequest request, string connectionId, string data)
        {
            this.ConnectionId = connectionId;
            return Connection.Send(connectionId, WebOperate.Get(data).Exec(this));
            //return Connection.Broadcast();
        }
        protected override Task OnConnectedAsync(IRequest request, string connectionId)
        {
            if (!user.ContainsKey(connectionId))
            {
                //AddEvent();
                user.Add(connectionId, this);
            }
            else
            {
                user[connectionId] = this;
            }
            return base.OnConnectedAsync(request, connectionId);
        }
        protected override Task OnDisconnectAsync(string connectionId)
        {
            if (user.ContainsKey(connectionId))
            {
                //user[connectionId].RemoveEvent();
                user.Remove(connectionId);
            }
            return base.OnDisconnectAsync(connectionId);
        }
        public override void Initialize(IDependencyResolver resolver)
        {
            base.Initialize(resolver);
        }

        public void AgentPhone_InboundCall(string arg1, string arg2, string arg3, string arg4, string arg5, int arg6, string arg7)
        {
            if (Connection != null)
            {
                Connection.Broadcast("AgentPhone_InboundCall");
            }
        }

        public void AgentPhone_DestInvalid()
        {
            if (Connection != null)
            {
                Connection.Broadcast("AgentPhone_DestInvalid");
            }
        }

        public void AgentPhone_DestBusy()
        {
            if (Connection != null)
            {
                Connection.Broadcast("AgentPhone_DestBusy");
            }
        }

        public void AgentPhone_DeviceStatus(string arg1, string arg2)
        {
            if (Connection != null)
            {
                Connection.Broadcast("AgentPhone_DeviceStatus");
            }
        }

        public void AgentPhone_Transfered()
        {
            if (Connection != null)
            {
                Connection.Broadcast("AgentPhone_Transfered");
            }
        }

        public void AgentPhone_TextMessage(string arg1, string arg2, string arg3)
        {
            if (Connection != null)
            {
                Connection.Broadcast("AgentPhone_TextMessage");
            }
        }

        public void AgentPhone_Conferenced()
        {
            if (Connection != null)
            {
                Connection.Broadcast("AgentPhone_Conferenced");
            }
        }

        public void AgentPhone_DialTone()
        {
            if (Connection != null)
            {
                Connection.Broadcast("AgentPhone_DialTone");
            }
        }

        public void AgentPhone_CallIdle()
        {
            if (Connection != null)
            {
                Connection.Broadcast("AgentPhone_CallIdle");
            }
        }

        public void AgentPhone_Answered(uint obj)
        {
            if (Connection != null)
            {
                Connection.Broadcast("AgentPhone_Answered");
            }
        }

        public void AgentPhone_CallInformation(string arg1, string arg2, string arg3, string arg4, string arg5, int arg6)
        {
            if (Connection != null)
            {
                Connection.Broadcast("AgentPhone_CallInformation");
            }
        }

        public void AgentPhone_UnknownEvent()
        {
            if (Connection != null)
            {
                Connection.Broadcast("AgentPhone_UnknownEvent");
            }
        }

        public void AgentPhone_AgentStatistics(string arg1, string arg2)
        {
            if (Connection != null)
            {
                Connection.Broadcast("AgentPhone_AgentStatistics");
            }
        }

        public void AgentPhone_AgentStatusCNGed(uint obj)
        {
            if (Connection != null)
            {
                Connection.Broadcast("AgentPhone_AgentStatusCNGed");
            }
        }

        public void AgentPhone_AgentLogout(string arg1, long arg2, long arg3)
        {
            if (Connection != null)
            {
                Connection.Broadcast("AgentPhone_AgentLogout");
            }
        }

        public void AgentPhone_AgentLogin(string arg1, long arg2, long arg3)
        {
            if (Connection != null)
            {
                Connection.Send(List[arg1], new WebOperate("AgentLogin", arg1, arg2, arg3));
                
                //Connection.Broadcast(new WebOperate("AgentLogin", arg1, arg2, arg3));
            }
        }

        public void AgentPhone_AgentStatus(string arg1, string arg2, string arg3, string arg4, AgentStateEnum arg5, string arg6)
        {
            if (Connection != null)
            {
                Connection.Broadcast(new WebOperate("AgentStatus", arg1, arg2, arg3, arg4, arg5, arg6));
                //Connection.Broadcast("AgentPhone_AgentStatus");
            }
        }

        public void AgentPhone_ConnectionReady()
        {
            if (Connection != null)
            {
                //Connection.Send(ConnectionId, new WebOperate("ConnectionReady", "OK").ToString());
                Connection.Broadcast(new WebOperate("ConnectionReady", "OK"));
            }
        }

        public void AgentPhone_ConnectionBroken()
        {
            if (Connection != null)
            {
                //Connection.Send(ConnectionId, );
                Connection.Broadcast(new WebOperate("ConnectionBroken", "OK"));
            }
        }

        public void Error(string arg1)
        {
            if (Connection != null)
            {
                Connection.Broadcast(new WebOperate("Error", arg1));
            }
        }



        public void AddEvent()
        {
            AgentPhone.ConnectionBroken += AgentPhone_ConnectionBroken;
            AgentPhone.ConnectionReady += AgentPhone_ConnectionReady;
            AgentPhone.AgentStatus += AgentPhone_AgentStatus;
            AgentPhone.AgentLogin += AgentPhone_AgentLogin;
            AgentPhone.AgentLogout += AgentPhone_AgentLogout;
            AgentPhone.AgentStatusCNGed += AgentPhone_AgentStatusCNGed;
            AgentPhone.AgentStatistics += AgentPhone_AgentStatistics;
            AgentPhone.UnknownEvent += AgentPhone_UnknownEvent;
            AgentPhone.CallInformation += AgentPhone_CallInformation;
            AgentPhone.Answered += AgentPhone_Answered;
            AgentPhone.CallIdle += AgentPhone_CallIdle;
            AgentPhone.DialTone += AgentPhone_DialTone;
            AgentPhone.Conferenced += AgentPhone_Conferenced;
            AgentPhone.TextMessage += AgentPhone_TextMessage;
            AgentPhone.Transfered += AgentPhone_Transfered;
            AgentPhone.DeviceStatus += AgentPhone_DeviceStatus;
            AgentPhone.DestBusy += AgentPhone_DestBusy;
            AgentPhone.DestInvalid += AgentPhone_DestInvalid;
            AgentPhone.InboundCall += AgentPhone_InboundCall;
            AgentPhone.Error += Error;
        }

        public void RemoveEvent()
        {
            AgentPhone.ConnectionBroken -= AgentPhone_ConnectionBroken;
            AgentPhone.ConnectionReady -= AgentPhone_ConnectionReady;
            AgentPhone.AgentStatus -= AgentPhone_AgentStatus;
            AgentPhone.AgentLogin -= AgentPhone_AgentLogin;
            AgentPhone.AgentLogout -= AgentPhone_AgentLogout;
            AgentPhone.AgentStatusCNGed -= AgentPhone_AgentStatusCNGed;
            AgentPhone.AgentStatistics -= AgentPhone_AgentStatistics;
            AgentPhone.UnknownEvent -= AgentPhone_UnknownEvent;
            AgentPhone.CallInformation -= AgentPhone_CallInformation;
            AgentPhone.Answered -= AgentPhone_Answered;
            AgentPhone.CallIdle -= AgentPhone_CallIdle;
            AgentPhone.DialTone -= AgentPhone_DialTone;
            AgentPhone.Conferenced -= AgentPhone_Conferenced;
            AgentPhone.TextMessage -= AgentPhone_TextMessage;
            AgentPhone.Transfered -= AgentPhone_Transfered;
            AgentPhone.DeviceStatus -= AgentPhone_DeviceStatus;
            AgentPhone.DestBusy -= AgentPhone_DestBusy;
            AgentPhone.DestInvalid -= AgentPhone_DestInvalid;
            AgentPhone.InboundCall -= AgentPhone_InboundCall;
            AgentPhone.Error -= Error;
        }
    }
}
