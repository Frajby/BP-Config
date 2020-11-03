using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABBConfigMaker
{
    class CfgEIO_SIGNAL : CfgRecord
    {
        public string Name { get; set; }
        public string SignalType { get; set; }
        public string Device { get; set; }
        public int DeviceMap { get; set; }
        public CfgEIO_SIGNAL()
        {

        }

        public override string ToCfgString()
        {
            return null;
        }
    }
}
