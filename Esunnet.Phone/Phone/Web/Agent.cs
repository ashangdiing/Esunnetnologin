using Esunnet.Phone.Phone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esunnet.Phone.Phone.Web
{
    public class Agent : IAgent
    {
        public string AgentId { get; set; }
        public string ConnectionId { get; set; }
        public string CallId { get; set; }
    }
}
