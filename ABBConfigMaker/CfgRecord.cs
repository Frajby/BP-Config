﻿using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ABBConfigMaker
{
    abstract class CfgRecord
    {
        public abstract string ToCfgString();
        public abstract string TypeOfRecord { get; }

        protected Dictionary<string,string> mapFromCfg(string line, string[] parametersNeeded)
        {
            Dictionary<string, string> mapped = new Dictionary<string, string>();
                foreach (string parameter in parametersNeeded)
                {
                    string regex = "(?<= -" + parameter + " \\\")(.*?)(?=\\\")";
                    mapped.Add(parameter, regexMatch(line, regex));
                }
            return mapped;

        }
        private string regexMatch(string text, string exp)
        {
            Match m = Regex.Match(text, exp);
            return m.Value;
        }
    }


}
