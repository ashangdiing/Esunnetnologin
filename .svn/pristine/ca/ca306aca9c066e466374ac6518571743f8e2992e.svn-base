using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esunnet.Model.Frame.Dhtmlx.Menu
{
    public class Menu : IMenu
    {
        private List<INode> _Root = new List<INode>();
        public List<INode> Root { get { return _Root; } }
        public void Add(INode node)
        {
            for (int i = 0; i < Root.Count; i++)
            {
                if (Root[i].Add(node))
                {
                    return;
                }
            }
            Root.Add(node);
        }
    }
}
