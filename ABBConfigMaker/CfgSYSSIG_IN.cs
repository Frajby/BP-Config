using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABBConfigMaker
{
    class CfgSYSSIG_IN : CfgRecord
    {
        public string TypeOfRecord { get; } = "SYSSIG_IN";
        public string[] parametersNeeded { get; set; } = { "Signal", "Action", "Arg1", "Arg2" };
        public Dictionary<string, string> parametersInCfg { get; }

        public CfgSYSSIG_IN(string[] lines)
        {
            parametersInCfg = mapFromCfg(lines, parametersNeeded);
        }

        public override string ToCfgString()
        {
            return null;
        }
    }
}
