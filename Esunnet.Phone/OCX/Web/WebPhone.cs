using Esunnet.Phone.OCX.Util;
using SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esunnet.Phone.OCX.Web
{
    public class WebPhone : PersistentConnection,TechsungCTIX.IMainFormEvents
    {
        public static Dictionary<string, OcxPhone> User = new Dictionary<string, OcxPhone>();
        public string ConnectionId { get; set; }
        protected override Task OnReceivedAsync(IRequest request, string connectionId, string data)
        {
            if (User.ContainsKey(connectionId))
            {
                return Connection.Send(connectionId, WebOperate.Get(data).Exec(User[connectionId]));
            }
            else
            {
                return Connection.Broadcast(data);
            }
        }
        protected override Task OnConnectedAsync(IRequest request, string connectionId)
        {
            if (!User.ContainsKey(connectionId))
            {
                this.ConnectionId = connectionId;
                User.Add(connectionId, new OcxPhone());
                User[connectionId].Ocx.AgentStatistics += AgentStatistics;
                User[connectionId].Ocx.AgentStatusCNGed += AgentStatusCNGed;
                User[connectionId].Ocx.AgtGroupInfo += AgtGroupInfo;
                User[connectionId].Ocx.Answered += Answered;
                User[connectionId].Ocx.CallIdle += CallIdle;
                User[connectionId].Ocx.CallInformation += CallInformation;
                User[connectionId].Ocx.Conferenced += Conferenced;
                User[connectionId].Ocx.ConnectionBroken += ConnectionBroken;
                User[connectionId].Ocx.ConnectionReady += ConnectionReady;
                User[connectionId].Ocx.DestBusy += DestBusy;
                User[connectionId].Ocx.DestInvalid += DestInvalid;
                User[connectionId].Ocx.DeviceStatus += DeviceStatus;
                User[connectionId].Ocx.DialTone += DialTone;
                User[connectionId].Ocx.InboundCall += InboundCall;
                User[connectionId].Ocx.LoggedIn += LoggedIn;
                User[connectionId].Ocx.LoggedOut += LoggedOut;
                User[connectionId].Ocx.OnActivate += OnActivate;
                User[connectionId].Ocx.OnClick += OnClick;
                User[connectionId].Ocx.OnCreate += OnCreate;
                User[connectionId].Ocx.OnDblClick += OnDblClick;
                User[connectionId].Ocx.OnDeactivate += OnDeactivate;
                User[connectionId].Ocx.OnDestroy += OnDestroy;
                User[connectionId].Ocx.OnKeyPress += OnKeyPress;
                User[connectionId].Ocx.OnPaint += OnPaint;
                User[connectionId].Ocx.TextMessage += TextMessage;
                User[connectionId].Ocx.Transfered += Transfered;
                User[connectionId].Ocx.UnknownEvent += UnknownEvent;
                User[connectionId].Ocx.agentStatus += agentStatus;
            }
            return base.OnConnectedAsync(request, connectionId);
        }
        protected override Task OnDisconnectAsync(string connectionId)
        {
            if (User.ContainsKey(connectionId))
            {
                User[connectionId].Ocx.AgentStatistics -= AgentStatistics;
                User[connectionId].Ocx.AgentStatusCNGed -= AgentStatusCNGed;
                User[connectionId].Ocx.AgtGroupInfo -= AgtGroupInfo;
                User[connectionId].Ocx.Answered -= Answered;
                User[connectionId].Ocx.CallIdle -= CallIdle;
                User[connectionId].Ocx.CallInformation -= CallInformation;
                User[connectionId].Ocx.Conferenced -= Conferenced;
                User[connectionId].Ocx.ConnectionBroken -= ConnectionBroken;
                User[connectionId].Ocx.ConnectionReady -= ConnectionReady;
                User[connectionId].Ocx.DestBusy -= DestBusy;
                User[connectionId].Ocx.DestInvalid -= DestInvalid;
                User[connectionId].Ocx.DeviceStatus -= DeviceStatus;
                User[connectionId].Ocx.DialTone -= DialTone;
                User[connectionId].Ocx.InboundCall -= InboundCall;
                User[connectionId].Ocx.LoggedIn -= LoggedIn;
                User[connectionId].Ocx.LoggedOut -= LoggedOut;
                User[connectionId].Ocx.OnActivate -= OnActivate;
                User[connectionId].Ocx.OnClick -= OnClick;
                User[connectionId].Ocx.OnCreate -= OnCreate;
                User[connectionId].Ocx.OnDblClick -= OnDblClick;
                User[connectionId].Ocx.OnDeactivate -= OnDeactivate;
                User[connectionId].Ocx.OnDestroy -= OnDestroy;
                User[connectionId].Ocx.OnKeyPress -= OnKeyPress;
                User[connectionId].Ocx.OnPaint -= OnPaint;
                User[connectionId].Ocx.TextMessage -= TextMessage;
                User[connectionId].Ocx.Transfered -= Transfered;
                User[connectionId].Ocx.UnknownEvent -= UnknownEvent;
                User[connectionId].Ocx.agentStatus -= agentStatus;
                User[connectionId].Ocx.Disconnect();
                User.Remove(connectionId);
            }
            return base.OnDisconnectAsync(connectionId);
        }
        public override void Initialize(IDependencyResolver resolver)
        {
            base.Initialize(resolver);
        }
        public void AgentStatistics(string SName, string SValue)
        {
            
        }

        public void AgentStatusCNGed(int agentStatus)
        {
        }

        public void AgtGroupInfo(string group, string orgincaller, string currentcaller, int callid, int flag)
        {
        }

        public void Answered(int opNumber)
        {
        }

        public void CallIdle()
        {
        }

        public void CallInformation(string currentCallingParty, string currentCalledParty, string originalCallingParty, string originalCalledParty, string applicationData, int callType)
        {
        }

        public void Conferenced()
        {
        }

        public void ConnectionBroken()
        {
        }

        public void ConnectionReady()
        {
        }

        public void DestBusy()
        {
        }

        public void DestInvalid()
        {
        }

        public void DeviceStatus(string deviceAddress, bool isIdle)
        {
        }

        public void DialTone()
        {
        }

        public void InboundCall(string currentCallingParty, string currentCalledParty, string originalCallingParty, string originalCalledParty, string applicationData, int callType, int operationNumber)
        {
        }

        public void LoggedIn(bool IsMonitor)
        {
        }

        public void LoggedOut()
        {
        }

        public void OnActivate()
        {
        }

        public void OnClick()
        {
        }

        public void OnCreate()
        {
        }

        public void OnDblClick()
        {
        }

        public void OnDeactivate()
        {
        }

        public void OnDestroy()
        {
        }

        public void OnKeyPress(ref short Key)
        {
        }

        public void OnPaint()
        {
        }

        public void TextMessage(string AgentID, string address, string amessage)
        {
        }

        public void Transfered()
        {
        }

        public void UnknownEvent()
        {
        }

        public void agentStatus(string agentName, string AgentID, string deviceAddress, string loginTime, short Status, string cause)
        {
        }
    }
}
