﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABBConfigMaker
{
    public class XRecord
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string DataType { get; set; }
        public string LogicalAddres { get; set; }
        public string Comment { get; set; }

        public bool isSelected { get; set; }

        public XRecord()
        {

        }

        public string toCfgString(bool newLine)
        {
            string cfgString = "      ";
            cfgString += "-Name \"" + Name + "\"";
            cfgString += " -SignalType";

            if (isDI(Name))
                cfgString += " \"DI\"";
            else
                cfgString += " \"DO\"";

            if((cfgString.Length + 29) >= 81)  //v cfg souboru se "zalamuje" text pomocí \ pravděpoobně když delka řádku přesáhne 81 => 29 znaků je Device položka
            {
                cfgString += "\\";
                cfgString += "\r\n";
                cfgString += "     ";
            }

            cfgString += " -Device";
            cfgString += " \"PN_Internal_Device\"";
            cfgString += " -DeviceMap";
            cfgString += " \"" + Comment.Replace(" - PN v robotu","") + "\"";

            if (newLine)
            {
                cfgString += "\r\n";
            }
        
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
