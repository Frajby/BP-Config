using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

using System.Data.OleDb;

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
            OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=Excel 12.0;");
            conn.Open();
            conn.Close();
            return new List<XRecord>();
        }
        
    }
}
