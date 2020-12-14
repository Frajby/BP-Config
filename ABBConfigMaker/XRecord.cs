using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABBConfigMaker
{
    class XRecord
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string DataType { get; set; }
        public string LogicalAddres { get; set; }
        public string Comment { get; set; }

        public XRecord()
        {

        }

        public string toCfgString()
        {
            string cfgString = "      ";
            cfgString += "-Name \"" + Name + "\" ";
            cfgString += "-SignalType ";
            if (isDI(Name))
                cfgString += "\"DI\" ";
            else
                cfgString += "\"DO\" ";

            cfgString += "-Device ";
            cfgString += "\"PN_Internal_Device\" ";
            cfgString += "-DeviceMap ";
            cfgString += "\"" + Comment.Replace(" - PN v robotu","") + "\"";

            cfgString += "\r\n";
            cfgString += "\r\n";
            return cfgString;

        }

        private bool isDI(string str)
        {
            if (str.Contains("PN_I"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

       
    }
}
