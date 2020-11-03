using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABBConfigMaker
{
    class CfgSYSSIG_IN : CfgRecord
    {
        public string Signal { get; set; }
        public string Action { get; set; }
        public string[] Arg { get; set; }

        public CfgSYSSIG_IN()
        {

        }

        public override string ToCfgString()
        {
            return null;
        }
    }
}
