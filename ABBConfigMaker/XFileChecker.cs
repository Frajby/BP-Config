using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using ExcelDataReader;
using System.IO;

using System.Data;

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
                
                FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read);
                IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                excelReader.Close();
                stream.Close();
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
