using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esunnet.Phone.Phone.Enum
{
    public enum AgentStateEnum
    {
        AGENT_LOGIN = 1,
        AGENT_READY = 2,
        AGENT_WORKREADY = 3,
        AGENT_NOTREADY = 4,
        AGENT_WORKNOTREADY = 5,
        AGENT_LOGOUT = 6,
        AGENT_CAUSECHANGED = 100,
    }
}
