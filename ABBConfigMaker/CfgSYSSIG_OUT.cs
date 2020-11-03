using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABBConfigMaker
{
    class CfgSYSSIG_OUT : CfgRecord
    {
        public string Status { get; set; }
        public string Signal { get; set; }
        public CfgSYSSIG_OUT()
        {

        }

        public override string ToCfgString()
        {
            return null;
        }
    }
}
