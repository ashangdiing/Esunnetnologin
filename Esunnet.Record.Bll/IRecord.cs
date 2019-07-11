using Esunnet.Model.Frame;
using Esunnet.Model.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esunnet.Record.Bll
{
    public interface IRecord : IBll
    {
        IGrid FindRecord(User u, IParameter<Esunnet.Model.Table.Record> param);
        string GetRecordUrlByTsId(string tsid);
        List<string> GetRecordUrlByCallId(string callid);
    }
}
