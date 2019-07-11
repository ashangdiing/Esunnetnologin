using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esunnet.Model.Frame.Dhtmlx.Menu
{
    public class Node : INode
    {
        public object id { get; set; }
        public object pid { get; set; }
        public string text { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public string hotKey { get; set; }
        public object userdata { get; set; }
        public bool disabled { get; set; }
        public string img0 { get; set; }
        public string img1 { get; set; }
        public List<INode> child { get; set; }
        public bool Add(INode node)
        {
            if (child == null) { child = new List<INode>(); }
            if (id.Equals(node.pid))
            {
                child.Add(node);
                return true;
            }
            else
            {
                for (int i = 0; i < child.Count; i++)
                {
                    if (child[i].Add(node))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
