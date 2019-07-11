using Esunnet.Model.Frame.Dhtmlx.Grid;
using Esunnet.Model.Proc;
using Esunnet.Record.Bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Esunnet.Record.Biz
{
    public class ReportBiz : AToJson, IReport
    {

        public Model.Frame.IGrid GetReport(string procName, DateTime? date, int? type)
        {
            Model.Frame.IGrid g = new Grid();
            UP_Simple up = new UP_Simple(procName);
            up.type = 1;
            up.InputParams.Add("datetime", date);
            up.InputParams.Add("type", type);
            g.ExecProc(up);
            return g;
        }

        public override void SetConfig(Bll.Setting.Config C)
        {
        }

        public Model.Frame.IGrid GetReport(string procName, DateTime? begin, DateTime? end, int? type)
        {
            Model.Frame.IGrid g = new Grid();
            UP_Simple up = new UP_Simple(procName);
            up.type = 1;
            up.InputParams.Add("begin", begin);
            up.InputParams.Add("end", end);
            up.InputParams.Add("type", type);
            g.ExecProc(up);
            return g;
        }
    }
}
