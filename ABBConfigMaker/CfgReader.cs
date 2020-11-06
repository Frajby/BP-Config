using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

namespace ABBConfigMaker
{
    class CfgReader
    {
        public string Path { get; set; }
        public CfgReader(string path)
        {
            this.Path = path;
        }

        public string[] readFileByLines()
        {
           return File.ReadAllLines(Path);
        }

        public List<CfgRecord> mapCfgFile()
        {
            string currentType = string.Empty;
            string[] lines = readFileByLines();
            List<CfgRecord> Records = new List<CfgRecord>();
           
            while
                if(lines[i] == "#" && lines[i+1] != null)
                {
                    switch (currentType)
                    {
                        case "EIO_SIGNAL:":
                            
                        case "SYSSIG_OUT:":

                        case "SYSSIG_IN:":

                    }
                }
            }
            return Records;
        }

    }
}
