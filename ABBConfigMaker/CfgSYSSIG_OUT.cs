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
        public override string TypeOfRecord { get; } = "SYSSIG_OUT";

        public override string[] parametersNeeded { get; set; } = { "Status", "Signal" };
        public override Dictionary<string, string> parametersInCfg { get; }

        public CfgSYSSIG_OUT(string line)
        {
            parametersInCfg = mapFromCfg(line, parametersNeeded);
        }

        

        public override string ToCfgString()
        {
            return null;
        }
    }
}
