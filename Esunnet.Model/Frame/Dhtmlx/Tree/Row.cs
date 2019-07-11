using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esunnet.Model.Frame.Dhtmlx.Grid
{
    public class Row
    {
        public object id { get; set; }
        public object data { get; set; }
        private Dictionary<string, object> _userdata;
        public Dictionary<string, object> userdata
        {
            get
            {
                if (_userdata == null)
                {
                    _userdata = new Dictionary<string, object>();
                }
                return _userdata;
            }
            set { _userdata = value; }
        }
        public bool? selected { get; set; }
        public string style { get; set; }
    }
}
