using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABBConfigMaker
{
    class CfgSYSSIG_IN : CfgRecord
    {
        public override string TypeOfRecord { get; } = "SYSSIG_IN";
        public override string[] parametersNeeded { get; set; } = { "Signal", "Action", "Arg1", "Arg2" };
        public override Dictionary<string, string> parametersInCfg { get; }

        public override string rawLine { get; set; }

        public override XRecord XlsRecordTwin { get; set; }
        public CfgSYSSIG_IN(string line)
        {
            rawLine = line;
            parametersInCfg = mapFromCfg(line, parametersNeeded);
        }

    }
}
