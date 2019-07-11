using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sbw.DbClinet
{
    public class Paging
    {
        public List<Dictionary<string, object>> Data { get; set; }
        public int Count { get; set; }
    }
}
