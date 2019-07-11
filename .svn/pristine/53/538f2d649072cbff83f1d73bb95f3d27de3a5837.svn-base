using Esunnet.Phone.Phone.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web.Script.Serialization;

namespace Esunnet.Phone.Phone.Util
{
    public class WebOperate
    {
        public string fun { get; set; }
        public string[] p { get; set; }
        public WebOperate() { }
        public WebOperate(string fun, params object[] p)
        {
            this.fun = fun;
            this.p = new string[p.Length];
            for (int i = 0; i < p.Length; i++)
            {
                if (p[i] != null)
                {
                    this.p[i] = p[i].ToString();
                }
                else
                {
                    this.p[i] = "";
                }
            }
        }
        public override string ToString()
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            //return jss.Deserialize<WebOperate>(json);
            return jss.Serialize(this);
        }
        public string this[int index]
        {
            get { return p[index]; }
        }
        public static WebOperate Get(string json)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<WebOperate>(json);
        }
        
        static WebOperate()
        {
            Thread t = new Thread(Connect);
            t.Start();
        }

        private static void Connect()
        {
            while (true)
                if (AgentPhone.ping > 590)
                {
                    if (AgentApi.MornsunSvrConnect("192.168.27.124", 8012) == 0)
                    {
                        AgentPhone.ping = 0;
                    }
                    Thread.Sleep(10000);
                }
        }
        public WebOperate Exec(IPhone Phone)
        {
            int i = -1;
            switch (fun)
            {
                case "Connect": i = AgentApi.MornsunSvrConnect(p[0], int.Parse(p[1])); break;
                case "Disconnect": i = AgentApi.MornsunSvrShutdown(); break;
                case "Login": i = AgentApi.MornsunSvrLogin(p[0], p[1], p[2], p[3]);
                    if (i == 0) { 
                        Agent a = new Agent();
                        a.AgentId=p[0];
                        a.ConnectionId=Phone.ConnectionId;
                        IAgent aa = Phone.List.Add(a);
                        if (aa != null && Phone.User.ContainsKey(aa.ConnectionId))
                        {
                            Phone.User[aa.ConnectionId].RemoveEvent();
                        }
                        else
                        {
                            Phone.AddEvent();
                        }
                    }
                    break;
                case "Logout": i = AgentApi.MornsunSvrLogout(p[0]);
                    if (i == 0)
                    {
                        Phone.List.Remove(p[0]);
                    }
                    break;
                case "AnswerCall": i = AgentApi.MornsunSvrAnswerCall(p[0], int.Parse(p[1])); break;
                case "Cancel": i = AgentApi.MornsunSvrCancel(p[0], int.Parse(p[1]), int.Parse(p[2]), int.Parse(p[3])); break;
                case "ChangeAgentPassword": i = AgentApi.MornsunSvrChangePassword(p[0], p[1], p[2]); break;
                case "SetAgentStatus": i = AgentApi.MornsunSvrChangeAgentStatus(p[0], byte.Parse(p[1]), p[2]); break;
                case "CompleteTransfer": i = AgentApi.MornsunSvrCompleteTransfer(p[0], int.Parse(p[2])); break;
                case "ConferenceCall": i = AgentApi.MornsunSvrConferenceCall(p[0], int.Parse(p[1]), p[2], p[3]); break;
                case "ConsultCall": i = AgentApi.MornsunSvrConsultCall(p[0], int.Parse(p[1]), p[2], p[3], p[4]); break;
                case "HangUp": i = AgentApi.MornsunSvrHangupCall(p[0], int.Parse(p[1])); break;
                case "HoldCall": i = AgentApi.MornsunSvrHoldCall(p[0], int.Parse(p[1])); break;
                case "ReteiveCall": i = AgentApi.MornsunSvrRetrieveCall(p[0], int.Parse(p[1])); break;
                case "MakeCall": i = AgentApi.MornsunSvrMakeCall(p[0], int.Parse(p[1]), p[2], p[3]); break;
                case "TransferCall": i = AgentApi.MornsunSvrTransferCall(p[0], int.Parse(p[1]), p[2], p[3], p[4]); break;
                case "SendTXTMessage": i = AgentApi.MornsunSvrSendTXTMessage(p[0], p[1], p[2]); break;
            }
            this.p = new string[] { (i > -1).ToString() };
            this.fun = "callback";
            return this;
        }
    }
}
