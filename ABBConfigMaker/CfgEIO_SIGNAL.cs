﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABBConfigMaker
{
    class CfgEIO_SIGNAL : CfgRecord
    {
        public override string TypeOfRecord { get; } = "EIO_SIGNAL";
        public string[] parametersNeeded { get; set; } = { "Name", "SignalType", "Device", "DeviceMap" };

        public Dictionary<string, string> parametersInCfg { get; }
        public CfgEIO_SIGNAL(string line)
        {
            parametersInCfg = mapFromCfg(line, parametersNeeded);
        }

        public override string ToCfgString()
        {
            return null;
        }
    }
}
