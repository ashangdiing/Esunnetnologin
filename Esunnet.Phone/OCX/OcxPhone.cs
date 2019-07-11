using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esunnet.Phone.OCX
{
    public class OcxPhone 
    {
        public string ConnectionId { get; set; }
        public TechsungCTIX.MainFormClass Ocx { get; set; }
        public OcxPhone() {
            TechsungCTIX.MainFormClass  ccx = new TechsungCTIX.MainFormClass();
            ((System.ComponentModel.ISupportInitialize)(this.Ocx)).BeginInit();
            Ocx.Connect("192.168.27.124", 8012);
        }
    }
}
