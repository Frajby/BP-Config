using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABBConfigMaker
{
    class XToCfg
    {
        XRecord xrecord { get; set; }

        public XToCfg(XRecord xrecord)
        {
            this.xrecord = xrecord;
        }

        //public CfgRecord ToCfg()
        //{

        //}

        //private CfgRecord ToEioSignal()
        //{
        //    CfgEIO_SIGNAL eio = new CfgEIO_SIGNAL()
        //}

    }
}
