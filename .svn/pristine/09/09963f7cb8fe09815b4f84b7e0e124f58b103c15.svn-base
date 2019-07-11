using Esunnet.Model.Frame;
using Esunnet.Model.Frame.Dhtmlx;
using Esunnet.Model.Frame.Dhtmlx.Grid;
using Esunnet.Model.Proc;
using Esunnet.Record.Bll;
using Sbw.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;

namespace Esunnet.Record.Biz
{
    internal class RecordBiz : AToJson, IRecord
    {
        public string GetRecordUrlByTsId(string tsid)
        {
            throw new NotImplementedException();
        }

        public List<string> GetRecordUrlByCallId(string callid)
        {
            throw new NotImplementedException();
        }

        public override void SetConfig(Bll.Setting.Config C)
        {
        }
        private class RecordSwitchData : ISwitchData
        {
            private RecordBiz r;
            public RecordSwitchData(RecordBiz r)
            {
                this.r = r;
            }
            public string Id { get { return "TS_ID"; } }
            public string[] Data { get { return new string[] { "TS_NUMBER", "userName", "TS_CHANNEL[通道:]", "calltype", "TS_CALLER", "TS_CALLED", "TS_START_TIME[date]", "duration[sw]", "agentid", "[null]", "[check]", "[img]" }; } }
            public object SwitchData(string type, object obj)
            {
                if ("sw".Equals(type))
                {
                    int i = (int)obj;
                    return string.Format("{2}:{1,-2:D2}:{0,-2:D2}", i % 60, i / 60 % 60, i / 60 / 60 % 60);
                }
                else if ("date".Equals(type))
                {
                    return string.Format("{0:yyyy-MM-dd HH:mm:ss}", obj);
                }
                else if ("check".Equals(type))
                {
                    return @checked ? "1" : "0";
                }
                else if ("null".Equals(type))
                {
                    return "";
                }
                else if ("img".Equals(type))
                {
                    string s = r.ToJson(obj);
                    return "<img src='imgs/button/record.gif' onclick='PlayAudio(" + s + ",1)' alt='播放' /><img src='imgs/button/line2.gif' /><img src='imgs/button/movedown.gif' onclick='PlayAudio(" + s + ",2)' alt='下载' title='下载' />";
                }
                else if (obj != null)
                {
                    return type + obj.ToString();
                }
                return type;
            }
            public bool @checked { get; set; }


            public string Header
            {
                get { return "分机号,用户名,通道,状态,主叫号码,被叫号码,通话开始时间,时长,坐席编号,客户编号,选择,操作"; }
            }

            public string ColAlign
            {
                get { return "left,left,left,left,left,left,left,right,left,left,center,center"; }
            }

            public string ColTypes
            {
                get { return "ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ch,ro"; }
            }

            public string ColSorting
            {
                get { return "str,str,str,str,str,str,str,int,str,str,int,str"; }
            }
            public string Width
            {
                get { return "55,115,115,115,115,125,115,115,115,115,115,55"; }
            }
        }
        public IGrid FindRecord(Model.Table.User u, IParameter<Model.Table.Record> param)
        {
            IGrid g = param.GetGrid();
            UP_Simple up = new UP_Simple("UP_SelectRecord");
            up.InputParams.Add("begin", param.begin + 1);
            up.InputParams.Add("end", param.begin + param.count);
            up.InputParams.Add("ext", param.GetNullString("reclis_ext"));
            up.InputParams.Add("caller", param.GetNullString("reclis_calling"));
            up.InputParams.Add("called", param.GetNullString("reclis_called"));
            up.InputParams.Add("max", param.GetInt("reclis_starttime"));
            up.InputParams.Add("min", param.GetInt("reclis_starttime"));
            switch (param.GetInt("reclis_calltype"))
            {
                case 1:
                    up.InputParams.Add("type", 0); break;
                case 2:
                    up.InputParams.Add("type", 3); break;
            }
            up.InputParams.Add("beginDate", param.GetNullString("reclis_starttime"));
            up.InputParams.Add("endDate", param.GetNullString("reclis_endtime"));
            up.InputParams.Add("agentid", param.GetNullString("reclis_agentid"));
            // up.InputParams.Add("serverfalg", param.begin + 1);
            //up.InputParams.Add("checked", param.GetInt("checkState"));
            g.SwitchData = new RecordSwitchData(this);
            g.SwitchData.@checked = param.GetBool("checkState") ?? false;
            g.ExecProc(up);
            return g;
        }
    }
}
