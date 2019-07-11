using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esunnet.Record.Bll.WebService
{
    public interface IRecord
    {
        string GetDevice(string ip);
        string[] GetRecordByCallId(string callid);
        string GetRecordByTsId(string tsid);
    }
}
