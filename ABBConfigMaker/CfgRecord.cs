
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ABBConfigMaker
{
    public abstract class CfgRecord
    {
        public abstract string TypeOfRecord { get; }
        public abstract string rawLine { get; set; }
        public abstract Dictionary<string, string> parametersInCfg { get; }
        public abstract string[] parametersNeeded { get; set; }

        public abstract XRecord XlsRecordTwin { get; set; }
        
        protected Dictionary<string,string> mapFromCfg(string line, string[] parametersNeeded)
        {
            Dictionary<string, string> mapped = new Dictionary<string, string>();
                foreach (string parameter in parametersNeeded)
                {
                    string regex = "(?<= -" + parameter + " \\\")(.*?)(?=\\\")";
                    mapped.Add(parameter, regexMatch(line, regex));
                }
            return mapped;

        }
        private string regexMatch(string text, string exp)
        {
            Match m = Regex.Match(text, exp);
            return m.Value;
        }
    }


}
