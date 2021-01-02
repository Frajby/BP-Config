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

            int lineCounter = 0;

            while(lineCounter < lines.Length)
            {
                string line = lines[lineCounter];

                if(line == "#" && lines[lineCounter + 1] != null)
                {
                    currentType = lines[lineCounter + 1];
                }

                if(line != "" && line.Contains('-'))
                {
                    string dataIn = string.Empty;
                    switch (currentType)
                    {
                        case "EIO_SIGNAL:":
                            if (readNextLine(line))
                            {
                                dataIn += line;
                                dataIn += "\r\n";
                                dataIn += lines[lineCounter + 1];
                                lineCounter += 2;
                            }
                            else
                            {
                                dataIn = line;
                                lineCounter += 1;
                            }
                            Records.Add(new CfgEIO_SIGNAL(dataIn));
                            break;

                        case "SYSSIG_OUT:":
                            if (readNextLine(line))
                            {
                                dataIn = line + lines[lineCounter + 1];
                                lineCounter += 2;
                            }
                            else
                            {
                                dataIn = line;
                                lineCounter += 1;
                            }
                            Records.Add(new CfgSYSSIG_OUT(dataIn));
                            break;

                        case "SYSSIG_IN:":
                            if (readNextLine(line))
                            {
                                dataIn = line + lines[lineCounter + 1];
                                lineCounter += 2;
                            }
                            else
                            {
                                dataIn = line;
                                lineCounter += 1;
                            }
                            Records.Add(new CfgSYSSIG_IN(dataIn));
                            break;
                        default:
                            lineCounter += 1;
                            break;
                    }
                }
                else
                {
                    lineCounter += 1;
                }
            }
            
            return Records;
        }

        private bool readNextLine(string line)
        {
            char last = line[line.Length - 1];
            if(last == '\\')    // \ has to be writen as \\
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
