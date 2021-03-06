﻿using Sbw.DbClinet;
using Sbw.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;

namespace Esunnet.Model.Frame.Dhtmlx.Grid
{
    public class Grid : IGrid
    {
        public int total_count { get; set; }
        public int pos { get; set; }
        private List<Row> _rows = new List<Row>();
        public List<Row> rows { get { return _rows; } }
        public Grid() { }
        public Grid(int pos) { this.pos = pos; }
        public string Header { get; set; }
        public string ColAlign { get; set; }
        public string ColTypes { get; set; }
        public string ColSorting { get; set; }
        public string Width { get; set; }
        [JSON(Show = false)]
        public ISwitchData SwitchData { get; set; }
        public void ExecProc(Sbw.DbClinet.Proc proc)
        {
            using (DbClinet Clinet = new DbClinet())
            {
                DbDataReader dr = Clinet.ExecuteReader(proc);
                Row r;
                List<object> data;
                Dictionary<string, object> ud = null;
                while (dr.Read())
                {
                    r = new Row();
                    data = new List<object>();
                    if (SwitchData != null)
                    {
                        ud = new Dictionary<string, object>();
                        r.id = dr[SwitchData.Id];
                        for (int i = 0; i < dr.FieldCount; i++)
                        {
                            ud.Add(dr.GetName(i), dr.GetValue(i));
                        }
                        for (int i = 0; i < SwitchData.Data.Length; i++)
                        {
                            Match m = Regex.Match(SwitchData.Data[i], "(.{0,})\\[(.{0,})\\]");
                            if (m.Success)
                            {
                                if ("".Equals(m.Groups[1].Value))
                                {
                                    data.Add(SwitchData.SwitchData(m.Groups[2].Value, ud));
                                }
                                else
                                {
                                    data.Add(SwitchData.SwitchData(m.Groups[2].Value, dr[m.Groups[1].Value]));
                                }
                            }
                            else
                            {
                               data.Add(dr[SwitchData.Data[i]]);
                            }
                        }
                    }
                    else
                    {
                        r.id = dr.GetValue(0);
                        for (int i = 1 - proc.type; i < dr.FieldCount; i++)
                        {
                            data.Add(dr.GetValue(i));
                        }
                    }
                    r.data = data;
                    r.userdata = ud;
                    rows.Add(r);
                }
                if (proc.type == 0 && dr.NextResult() && dr.Read())
                {
                    this.total_count = dr.GetInt32(0);
                }
                if (dr.NextResult() && dr.Read())
                {
                    List<string> l = new List<string>();
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        l.Add(dr.GetName(i));
                    }
                    if (l.Contains("Header")) this.Header = dr["Header"] as string;
                    if (l.Contains("ColAlign")) this.ColAlign = dr["ColAlign"] as string;
                    if (l.Contains("ColTypes")) this.ColTypes = dr["ColTypes"] as string;
                    if (l.Contains("ColSorting")) this.ColSorting = dr["ColSorting"] as string;
                    if (l.Contains("Width")) this.Width = dr["Width"] as string;
                }
                else if (SwitchData != null)
                {
                    this.Header = SwitchData.Header;
                    this.ColAlign = SwitchData.ColAlign;
                    this.ColTypes = SwitchData.ColTypes;
                    this.ColSorting = SwitchData.ColSorting;
                    this.Width = SwitchData.Width;
                }
                dr.Close();
            }
        }


        public string toJson(object o)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            jss.RegisterConverters(new JavaScriptConverter[] { new GridConverter() });
            return jss.Serialize(o);
        }
    }
}
