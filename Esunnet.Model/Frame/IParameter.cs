using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esunnet.Model.Frame
{
    public interface IParameter<T>
    {
        string GetString(string name);
        int? GetInt(string name);
        bool? GetBool(string name);
        U? Get<U>(string name) where U : struct;
        string GetNullString(string name);
        IGrid GetGrid();
        int begin { get; set; }
        int count { get; set; }
        T t { get; }
    }
}
