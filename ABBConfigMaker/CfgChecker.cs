using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ABBConfigMaker
{
    class CfgChecker : Checker
    {
        public string path { get; set; }

        public CfgChecker(string path)
        {
            this.path = path;
        }
        public override ErrorDataModel check()
        {
            ErrorDataModel errorData = new ErrorDataModel();
            string format = Path.GetExtension(path);
            Console.WriteLine(format);
            if (format == ".cfg")
            {
                errorData.isError = false;
                errorData.errorMessage = string.Empty;
            }
            else
            {
                errorData.isError = true;
                errorData.errorMessage = "Soubor musí být ve formátu .cfg";
            }
            return errorData;
        }

    }
}
