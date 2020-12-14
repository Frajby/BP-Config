using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using ExcelDataReader;
using System.IO;


namespace ABBConfigMaker
{
    class XReader
    {
        public string path { get; set; }
        public XReader(string path)
        {
            this.path = path;
        }

        public List<XRecord> Read()
        {
            FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

            bool firstRow = true;

            List<XRecord> records = new List<XRecord>();

            while (excelReader.Read())
            {
                if(firstRow == true)
                {
                    firstRow = !firstRow;
                }

                if (!firstRow)
                {
                    XRecord xrec = new XRecord();
                    xrec.Name = excelReader.GetString(0);
                    xrec.Path = excelReader.GetString(1);
                    xrec.DataType = excelReader.GetString(2);
                    xrec.LogicalAddres = excelReader.GetString(3);
                    xrec.Comment = excelReader.GetString(4);
                    records.Add(xrec);
                }

            }

            excelReader.Close();
            stream.Close();

            return records;
        }
        
    }
}
