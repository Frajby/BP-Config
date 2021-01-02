using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace ABBConfigMaker
{
    class CfgWriter
    {
        public string Path { get; set; }
        public List<XRecord> Xrecords { get; set; }
        public List<CfgRecord> Cfgrecords { get; set; }

        public CfgWriter(List<XRecord> Xrecords, List<CfgRecord> Cfgrecords, string path)
        {
            this.Path = path;
            this.Xrecords = Xrecords;
            this.Cfgrecords = Cfgrecords;
        }

        public void writeToCfg()
        {
            //neejprve se zapíšou ty x záznmy, které jsou nové z fce getNewXrecordToWrite, poté se vytahnou ty záznamy, které mají stejný jméno, a budou se aktualizovat
            //asi nejlepší aktualizace bude vytáhnou všechny stringy, pak porovnávat a mazat nebo přepisovat (pomocí regexu?) a poté vymzat celý soubor a nahrát do něj nebo nějak tak

            List<XRecord> updateXrec = getXrecordsToUpdate();
            List<XRecord> newXrec = getNewXrecords();

            string[] linesArr = File.ReadAllLines(Path);

            string linesFull = string.Empty;
            foreach (string str in linesArr)
            {
                linesFull += str;
                linesFull += "\r\n";
            }

            string updateCfgStr = string.Empty;
            foreach (XRecord update in updateXrec)
            {
                updateCfgStr += update.toCfgString();
            }

            string toUpdate = returnStringByTopic(linesArr, "EIO_SIGNAL");

            linesFull.Replace(toUpdate, updateCfgStr);



        }

        private void updateCfgRecords(List<XRecord> xrecords)
        {

        }

        private void writeNewRecords(List<XRecord> xrec)
        {

        }
        
        private string returnStringByTopic(string[] lines,string topic)
        {
            bool topicFound = false;
            bool topicEnd = false;
            string retStr = string.Empty;

            for (int i = 0; i < lines.Length; i++)
            {
                if (topicFound && topicEnd == false)
                {
                    retStr += lines[i];
                }

                if (lines[i].Contains(topic))
                {
                    topicFound = true;
                }

                if (topicFound) 
                { 
                    if (lines[i].Contains("#") || i == lines.Length)
                    {
                        topicEnd = true;
                    }
                }
            }
            
            return retStr;
        }
    
        private List<XRecord> getXrecordsToUpdate()
        {
            List<CfgRecord> CfgRecToCompare = getCfgRecordsByDevice("PN_Internal_Device");
            List<XRecord> XrecToUpdate = new List<XRecord>();
            foreach (XRecord xrec in Xrecords)
            {
                if (!isEIONew(xrec))
                {
                    XrecToUpdate.Add(xrec);
                }
            }
            return XrecToUpdate;
        }

        private List<XRecord> getNewXrecords()
        {
            List<CfgRecord> CfgRecToCompare = getCfgRecordsByDevice("PN_Internal_Device");
            List<XRecord> XrecNew = new List<XRecord>();
            foreach (XRecord xrec in Xrecords)
            {
                if (isEIONew(xrec))
                {
                    XrecNew.Add(xrec);
                }
            }
            return XrecNew;
        }

        private bool isEIONew(XRecord record)
        {
            bool isnew = true;
            foreach(CfgRecord cfg in Cfgrecords)
            {
                if(cfg.TypeOfRecord == "EIO_SIGNAL")
                {
                    if(record.Name == cfg.parametersInCfg["Name"])
                    {
                        isnew = false;
                        break;
                    }
                }
            }
            return isnew;
        }

        private List<CfgRecord> getCfgRecordsByDevice(string device)
        {
            List<CfgRecord> retCfg = new List<CfgRecord>();
            foreach(CfgRecord record in Cfgrecords)
            {
                if(record.TypeOfRecord == "EIO_SIGNAL")
                {
                    if(record.parametersInCfg["Device"] == device)
                    {
                        retCfg.Add(record);
                    }
                }
            }
            return retCfg;
        }
    }
}
