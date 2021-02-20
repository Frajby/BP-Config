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

        private List<string> RawCfgFile;

        public List<ErrorDataModel> errors;

        public WritingOptions myOption;


        public CfgWriter(List<XRecord> Xrecords, List<CfgRecord> Cfgrecords, string path, WritingOptions option)
        {
            this.Path = path;
            this.Xrecords = Xrecords;
            this.Cfgrecords = Cfgrecords;
            this.myOption = option;
            RawCfgFile = File.ReadAllLines(Path).ToList();
            RawCfgFile.Add("");
            errors = new List<ErrorDataModel>();
        }

        public void writeToCfg()
        {
            if(myOption == WritingOptions.WRITE_UPDATE_ALL)
            {
                List<XRecord> updateXrec = getXrecordsToUpdate();
                List<XRecord> newXrec = getNewXrecords();

                insertXrecordsIntoTopic("EIO_SIGNAL", newXrec);
                UpdateCfgRecords(updateXrec);

                RewriteCfgFile();
            }

            if (myOption == WritingOptions.DELETE_OLD_ADD_NEW)
            {

            }

            if (myOption == WritingOptions.SELECT_INV)
            {

            }

        }

        public bool hasError()
        {
            bool ret = false;
            foreach(ErrorDataModel err in errors)
            {
                if (err.isError)
                {
                    ret = true;
                }
            }
            return ret;
        }

        public List<ErrorDataModel> Errors
        {
            get => errors;
        }

        private void RewriteCfgFile()
        {
            try
            {
                File.WriteAllText(Path, String.Empty);
                TextWriter writer = new StreamWriter(Path);
                foreach(string s in RawCfgFile)
                {
                    writer.WriteLine(s);
                }
            }
            catch (Exception e)
            {
                ErrorDataModel err = new ErrorDataModel();
                err.isError = true;
                err.errorMessage = e.ToString();
                errors.Add(err);
            }
        }

        private void UpdateCfgRecords(List<XRecord> records)
        {
            try
            {
                string Filelines = string.Empty;
                foreach (string str in RawCfgFile)
                {
                    Filelines += str;
                    Filelines += "\r\n";
                }
                string updateCfgStr = string.Empty;
                foreach (XRecord record in records)
                {
                    updateCfgStr += record.toCfgString(true);
                }
                string toUpdate = returnStringsByTopic(RawCfgFile, "EIO_SIGNAL");
                Filelines.Replace(toUpdate, updateCfgStr);

                string[] spliter = { "\r\n" };
                RawCfgFile = Filelines.Split(spliter,System.StringSplitOptions.None).ToList();
            }
            catch (Exception e)
            {
                ErrorDataModel err = new ErrorDataModel();
                err.isError = true;
                err.errorMessage = e.ToString();
                errors.Add(err);
            }

        }

        private void insertXrecordsIntoTopic(string topic,List<XRecord> xrecords)
        {
            if (isTopicExist(topic, RawCfgFile))
            {
                int lastLineIndexofTopic = getIndexOfLastLineInTopic(RawCfgFile, topic);
                for (int i = 0; i < xrecords.Count; i++)
                {
                    RawCfgFile.Insert(lastLineIndexofTopic + 1 + i, xrecords[i].toCfgString(true));
                }
            }
            else
            {
                RawCfgFile.Add("#");
                RawCfgFile.Add(topic + ":");
                RawCfgFile.Add("");

                for (int i = 0; i < xrecords.Count; i++)
                {
                    RawCfgFile.Add(xrecords[i].toCfgString(true));
                }
            }
        }

        private bool isTopicExist(string topic,List<string> lines)
        {
            bool ret = false;
            foreach(string line in lines)
            {
                if (line.Contains(topic))
                {
                    ret = true;
                }
            }
            return ret;
        }

        private int getIndexOfLastLineInTopic(List<string> lines, string topic)
        {
            bool topicFound = false;
            bool topicEnd = false;
            int lastIndex = -1;

            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].Contains(topic))
                {
                    topicFound = true;
                }

                if (topicFound && !topicEnd)
                {
                   
                    if (lines[i].Contains("#"))
                    {
                        lastIndex = i-1;
                        topicEnd = true;
                    }
                    if (i == lines.Count - 1)
                    {
                        lastIndex = i;
                        topicEnd = true;
                    }
                }
            }
            return lastIndex;
        }

        private string returnStringsByTopic(List<string> lines,string topic)
        {
            bool topicFound = false;
            bool topicEnd = false;
            string retStr = string.Empty;

            for (int i = 0; i < lines.Count; i++)
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
                    if (lines[i].Contains("#") || i == lines.Count)
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
