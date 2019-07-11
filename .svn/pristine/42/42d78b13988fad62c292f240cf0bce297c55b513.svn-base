using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sbw.Json
{
    public class Function
    {
        public List<string> funParams = new List<string>();
        public string call { get; set; }
        public List<string> callParams = new List<string>();
        public override string ToString()
        {
            System.Text.StringBuilder sb = new StringBuilder();
            sb.Append("function( ");
            foreach (string item in funParams)
            {
                sb.Append(item);
                sb.Append(",");
            }
            sb.Length--;
            sb.AppendLine("){");
            if (!string.IsNullOrEmpty(call))
            {
                sb.Append("\t");
                sb.Append(call);
                sb.Append("( ");
                foreach (string item in callParams)
                {
                    if (funParams.Contains(item))
                    {
                        sb.Append(item);
                    }
                    else
                    {
                        sb.Append("'");
                        sb.Append(item);
                        sb.Append("'");
                    }
                    sb.Append(",");
                }
                sb.Length--;
                sb.AppendLine(");");
            }
            sb.AppendLine("\n}");
            return sb.ToString() ;
        }
    }
}
