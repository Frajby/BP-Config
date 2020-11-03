using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using Excel = Microsoft.Office.Interop.Excel;

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
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(path);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            List<XRecord> recordList = new List<XRecord>();

            for(int i = 2; i <= rowCount; i++)
            {
                if(xlRange.Cells[i] != null && xlRange.Cells[i].Value2 != null)
                {
                    XRecord record = new XRecord();
                    record.Name = xlRange.Cells[i, 1].Value2;
                    record.Path = xlRange.Cells[i, 2].Value2;
                    record.DataType = xlRange.Cells[i, 3].Value2;
                    record.LogicalAddres = xlRange.Cells[i, 4].Value2;
                    record.Comment = xlRange.Cells[i, 5].Value2;
                    recordList.Add(record);
                }
            }
            xlWorkbook.Close();
            xlApp.Quit();
            return recordList;
        }
        
    }
}
