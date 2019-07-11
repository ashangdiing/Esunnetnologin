using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web.Script.Serialization;

namespace Esunnet.Phone.OCX.Util
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

        public WebOperate Exec(OcxPhone Phone)
        {
            try
            {
                switch (fun)
                {
                    case "Connect": Phone.Ocx.Connect(p[0], int.Parse(p[1])); break;
                    case "Disconnect": Phone.Ocx.Disconnect(); break;
                    case "Login": Phone.Ocx.Login(p[0], p[1], p[2], p[3]); break;
                    case "Logout": Phone.Ocx.Logout(); break;
                    case "AnswerCall": Phone.Ocx.AnswerCall(); break;
                    case "Cancel": Phone.Ocx.Cancel(); break;
                    case "ChangeAgentPassword": Phone.Ocx.ChangeAgentPassword(p[0], p[1]); break;
                    case "SetAgentStatus": Phone.Ocx.ChangeAgentStatus(short.Parse(p[0]), p[1]); break;
                    case "CompleteTransfer": Phone.Ocx.CompleteTransfer(); break;
                    case "ConferenceCall": Phone.Ocx.ConferenceCall(p[0], p[1]); break;
                    case "ConsultCall": Phone.Ocx.ConsultCall(p[0], p[1]); break;
                    case "HangUp": Phone.Ocx.HangUp(); break;
                    case "HoldCall": Phone.Ocx.HoldCall(); break;
                    case "ReteiveCall": Phone.Ocx.RetrieveCall(); break;
                    case "MakeCall": Phone.Ocx.MakeCall(p[0], p[1]); break;
                    case "TransferCall": Phone.Ocx.TransferCall(p[0], p[1]); break;
                    case "SendTXTMessage": Phone.Ocx.SendTXTMessage(p[0], p[1]); break;
                }
                this.p = new string[] { "True" };
            }
            catch
            {
                this.p = new string[] { "False" };
            }
            this.fun = "callback";
            return this;
        }
    }
}
