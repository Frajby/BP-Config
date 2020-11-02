using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ABBConfigMaker
{
    class XFormatChecker : Checker
    {
        public string path { get; set; }
       
        public XFormatChecker(string path)
        {
            this.path = path;
        }
        public override ErrorDataModel check()
        {
            ErrorDataModel errorData = new ErrorDataModel();
            string format = Path.GetExtension(path);
            Console.WriteLine(format);
            if (format == ".xls" || format == ".xlsx")
            {
                errorData.isError = false;
                errorData.errorMessage = string.Empty;
            }
            else
            {
                errorData.isError = true;
                errorData.errorMessage = "Soubor musí být ve formátu .xls nebo .xlsx";
            }
            return errorData;
        }
    }
}
