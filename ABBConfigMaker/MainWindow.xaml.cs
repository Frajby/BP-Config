using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace ABBConfigMaker
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string Xpath = string.Empty;
        string CfgPath = string.Empty;

        public List<XRecord> xlsRecords;
        public List<CfgRecord> cfgRecords;

        public enum WritingOptions {WRITE_UPDATE_ALL, DELETE_OLD_ADD_NEW, SELECT_INV}
        public WritingOptions currentOption = WritingOptions.WRITE_UPDATE_ALL;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_loadFile_Click(object sender, RoutedEventArgs e)
        {

            Loader loader = new Loader(true);
            if (loader.fileReady)
            {
                Xpath = loader.Path;
                txt_loadedFile.Text = loader.Path;
            }
            loadXlsRecords();
        }

        private void btn_MakeFile_Click(object sender, RoutedEventArgs e)
        {

            CfgWriter writer = new CfgWriter(xlsRecords, cfgRecords,CfgPath);
            
            writer.writeToCfg();

            if (writer.hasError())
            {
                string errMsg = string.Empty;
                foreach(ErrorDataModel err in writer.Errors)
                {
                    errMsg += err.errorMessage;
                    errMsg += "\n";
                }
                MessageBox.Show(errMsg);
            }
            else
            {
                MessageBox.Show("Config file has been succesfully updated");
            }

        }

        private void btn_loadFile_Copy_Click(object sender, RoutedEventArgs e)
        {
          
            Loader cfgLoader = new Loader(false);
            if (cfgLoader.fileReady)
            {
                CfgPath = cfgLoader.Path;
                txt_cfgFile.Text = CfgPath;
            }
            loadCfgRecords();
        }

        private void radWriteUpdateAll_Checked(object sender, RoutedEventArgs e)
        {
            currentOption = WritingOptions.WRITE_UPDATE_ALL;
            EnableSelectInvCotrols(false);
        }

        private void radDeleteAndAdd_Checked(object sender, RoutedEventArgs e)
        {
            currentOption = WritingOptions.DELETE_OLD_ADD_NEW;
            EnableSelectInvCotrols(false);
        }

        private void radSelect_Checked(object sender, RoutedEventArgs e)
        {
            currentOption = WritingOptions.SELECT_INV;
            EnableSelectInvCotrols(true);
        }

        private void EnableSelectInvCotrols(bool value)
        {
            btnAddRecord.IsEnabled = value;
            btnRemoveRecord.IsEnabled = value;
        }

        private void btnAddRecord_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            radWriteUpdateAll.IsChecked = true;
            EnableSelectInvCotrols(false);
            xlsRecords = new List<XRecord>();
            cfgRecords = new List<CfgRecord>();
        }

        private void loadRecordsIntoListViews()
        {
            foreach(XRecord xrec in xlsRecords)
            {
               
                lsvXls.Items.Add(xrec);
            }
        }

        private void loadXlsRecords()
        {
            XReader xreader = new XReader(Xpath);
            xlsRecords = xreader.Read();

            GridView xgrid = new GridView();

            string[] xColumns = { "Name", "Path", "DataType", "LogicalAddres", "Comment" };
            foreach(string s in xColumns)
            {
                GridViewColumn gvc = new GridViewColumn();
                gvc.Header = s;
                gvc.DisplayMemberBinding = new Binding(s);
                xgrid.Columns.Add(gvc);
            }
            lsvXls.View = xgrid;
            

            foreach (XRecord xrec in xlsRecords)
            {
                
                lsvXls.Items.Add(xrec);
            }
        }
        private void loadCfgRecords()
        {
            CfgReader cfgReader = new CfgReader(CfgPath);
            cfgRecords = cfgReader.mapCfgFile();
        }
    }
}
