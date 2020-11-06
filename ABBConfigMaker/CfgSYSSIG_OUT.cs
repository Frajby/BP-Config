using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Security.AccessControl;
using System.Dynamic;

namespace ABBConfigMaker
{
    class CfgSYSSIG_OUT : CfgRecord
    {
        public string TypeOfRecord { get; } = "SYSSIG_OUT";

        public string[] parametersNeeded { get; set; } = { "Status", "Signal" };
        public Dictionary<string, string> parametersInCfg { get; }

        public CfgSYSSIG_OUT(string[] lines)
        {
            parametersInCfg = mapFromCfg(lines, parametersNeeded);
        }

        

        public override string ToCfgString()
        {
            return null;
        }
    }
}
