using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABBConfigMaker
{
    class EIO_Signal_ViewDataModel
    {
        public string Name { get; set; }
        public string SignalType { get; set; }
        public string Device { get; set; }
        public string DeviceMap { get; set; }

        public CfgRecord ReferencedRecord { get; set; }
    }
}
