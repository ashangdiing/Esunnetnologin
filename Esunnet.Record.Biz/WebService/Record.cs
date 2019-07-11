using Esunnet.Record.Bll.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Esunnet.Record.Biz.WebService
{
    internal class Record : IRecord
    {
        public string GetDevice(string ip)
        {
            using (Sbw.DbClinet.DbClinet C = new Sbw.DbClinet.DbClinet())
            {
                C.AddParameter("userip", ip);
                return C.ExecuteScalar("select top 1 TS_DEVICENUMBER from TS_RECDEV where TS_ALIAS like @userip") as string;
            }
        }

        public string[] GetRecordByCallId(string callid)
        {
            using (Sbw.DbClinet.DbClinet C = new Sbw.DbClinet.DbClinet())
            {
                C.AddParameter("@callid", callid);
                List<Dictionary<string, object>> l = C.Select("select TS_FILE_URL from dbo.TS_REC_LOG where TS_CALL_ID like @callid");
                string[] s = new string[l.Count];
                for (int i = 0; i < s.Length; i++)
                {
                    s[i] = "http:" + Regex.Replace(l[i]["TS_FILE_URL"].ToString(), "\\\\", "/");
                }
                return s;
            }
        }

        public string GetRecordByTsId(string tsid)
        {
            using (Sbw.DbClinet.DbClinet C = new Sbw.DbClinet.DbClinet())
            {
                C.AddParameter("@tsid", tsid);
                string url = C.ExecuteScalar("select top 1 TS_FILE_URL from dbo.TS_REC_LOG where TS_ID like @tsid") as string;
                return "http:" + Regex.Replace(url, "\\\\", "/");
            }
        }
    }
}
