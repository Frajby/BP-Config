using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Windows;

namespace ABBConfigMaker
{
    class CfgLoader
    {
        public string Path { get; set; }
        public bool fileReady { get; }

        public CfgLoader()
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "cfg files (*.cfg *.txt)|*.cfg;*.txt|All files (*.*)|*.*";
                if (ofd.ShowDialog() == true)
                {
                    Path = ofd.FileName;
                }
            CfgReader cfgreader = new CfgReader(Path);
            List<CfgRecord> Records = cfgreader.mapCfgFile();

        }


    }
}
