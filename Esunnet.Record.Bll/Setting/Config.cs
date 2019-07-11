using Esunnet.Model.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Esunnet.Record.Bll.Setting
{
    public class Config
    {
        public bool HasPermission { get; set; }
        public bool HasAgent { get; set; }
        public string FUN { get { return "fun"; } }
        public string PATH { get { return "url"; } }
        public string PARAM { get { return "param"; } }
        public User user { get { try { return HttpContext.Current.Session["User"] as User; } catch { return null; } } }
    }
}
