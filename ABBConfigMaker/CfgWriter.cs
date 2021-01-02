using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABBConfigMaker
{
    class CfgWriter
    {
        public List<XRecord> Xrecords { get; set; }
        public List<CfgRecord> Cfgrecords { get; set; }

        public CfgWriter(List<XRecord> Xrecords, List<CfgRecord> Cfgrecords)
        {
            this.Xrecords = Xrecords;
            this.Cfgrecords = Cfgrecords;


        }

        public void writeToCfg()
        {
            //neejprve se zapíšou ty x záznmy, které jsou nové z fce getNewXrecordToWrite, poté se vytahnou ty záznamy, které mají stejný jméno, a budou se aktualizovat
            //asi nejlepší aktualizace bude vytáhnou všechny stringy, pak porovnávat a mazat nebo přepisovat (pomocí regexu?) a poté vymzat celý soubor a nahrát do něj nebo nějak tak

            List<XRecord> updateXrec = getXrecordsToUpdate();
            List<XRecord> newXrec = getNewXrecords();


        }

        private void updateCfgRecords(List<XRecord> xrecords)
        {

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
