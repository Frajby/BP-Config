using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace ABBConfigMaker
{
    class XFileChecker : Checker
    {
        public string path { get; set; }
        public XFileChecker(string path)
        {
            this.path = path;
        }
        public override ErrorDataModel check()
        {
            ErrorDataModel errorData = new ErrorDataModel();
            try
            {
                Excel.Application xlapp = new Excel.Application();
                Excel.Workbook xlworkbook = xlapp.Workbooks.Open(path);
            }
            catch (Exception e)
            {
                if (e != null)
                {
                    errorData.isError = true;
                    errorData.errorMessage = e.ToString();
                }
                else
                {
                    errorData.isError = false;
                    errorData.errorMessage = string.Empty;
                }
            }
            return errorData;
        }
    }
}
