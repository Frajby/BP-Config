using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace ABBConfigMaker
{
    class Loader
    {
        public string Path { get; set; }
        public bool fileReady { get; }
        public Loader()
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "xls files (*.xls *.xlsx)|*.xls;*.xlsx|All files (*.*)|*.*";
            if (ofd.ShowDialog() == true)
            {
                Path = ofd.FileName;

                MainChecker mainChecker = new MainChecker();
                XFormatChecker formatChecker = new XFormatChecker(Path);
                XFileChecker fileChecker = new XFileChecker(Path);
                mainChecker.addChecker(formatChecker.check);
                mainChecker.addChecker(fileChecker.check);

                if (mainChecker.checkAll())
                {
                    fileReady = false;
                }
                else
                {
                    fileReady = true;
                }
            }


        }
    }
}
