﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esunnet.Phone.Phone.Util
{
    public class AgentList
    {
        public List<IAgent> agent = new List<IAgent>();
        public IAgent Add(IAgent a)
        {
            foreach (IAgent item in agent)
            {
                if (item.AgentId.Equals(a.AgentId))
                {
                    agent.Remove(item);
                    this.agent.Add(a);
                    return item;
                }
            }
            this.agent.Add(a);
            return null;
        }
        public bool Remove(string AgentId)
        {
            foreach (IAgent item in agent)
            {
                if (item.AgentId.Equals(AgentId))
                {
                    agent.Remove(item);
                    return true;
                }
            }
            return false;
        }
        public string this[string AgentId] {
            get {
                foreach (IAgent item in agent)
                {
                    if (item.AgentId.Equals(AgentId))
                    {
                        return item.ConnectionId;
                    }
                }
                return null;
            }
        }
    }
}
