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
    public enum WritingOptions {WRITE_UPDATE_ALL, DELETE_OLD_ADD_NEW, SELECT_INV}
    public partial class MainWindow : Window
    {
        string Xpath = string.Empty;
        string CfgPath = string.Empty;

        public List<XRecord> xlsRecords;
        public List<CfgRecord> cfgRecords;

        public List<XRecord> xlsRecordsSelected;
        public List<CfgRecord> cfgRecordsSelected;

       
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
                loadXlsRecords();
            }
            
        }

        private void btn_MakeFile_Click(object sender, RoutedEventArgs e)
        {

            List<CfgRecord> cfgToWrite = new List<CfgRecord>();
            if(cfgRecordsSelected.Count > 0)
            {
                cfgToWrite = cfgRecordsSelected;
            }
            else
            {
                cfgToWrite = cfgRecords;
            }

            CfgWriter writer = new CfgWriter(xlsRecords, cfgToWrite,CfgPath,currentOption);
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
                loadCfgRecords();
            }
            
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
            if(lsvCfgLoaded.SelectedItems.Count > 0)
            {
                List<CfgRecord> toRemove = new List<CfgRecord>();
                foreach(var item in lsvCfgLoaded.SelectedItems)
                {
                    int index = lsvCfgLoaded.Items.IndexOf(item);
                    cfgRecordsSelected.Add(cfgRecords[index]);
                    toRemove.Add(cfgRecords[index]);
                }
                foreach (CfgRecord cfg in toRemove)
                {
                    cfgRecords.Remove(cfg);
                }
                lsvCfgLoaded.UnselectAll();

            }
            else if (lsvXls.SelectedItems.Count > 0)
            {
                List<XRecord> toRemove = new List<XRecord>();
                foreach(var item in lsvXls.SelectedItems)
                {
                    int index = lsvXls.Items.IndexOf(item);
                    CfgEIO_SIGNAL cfg = new CfgEIO_SIGNAL(xlsRecords[index].toCfgString(false));
                    cfg.XlsRecordTwin = xlsRecords[index];
                    cfgRecordsSelected.Add(cfg);
                    toRemove.Add(xlsRecords[index]);
                }
                foreach(XRecord xrecRemove in toRemove)
                {
                    xlsRecords.Remove(xrecRemove);
                }
                lsvXls.UnselectAll();
            }
            else
            {
                MessageBox.Show("Please choose at least one record to add");
            }
            refreshListViews();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            radWriteUpdateAll.IsChecked = true;
            EnableSelectInvCotrols(false);
            xlsRecords = new List<XRecord>();
            cfgRecords = new List<CfgRecord>();
            cfgRecordsSelected = new List<CfgRecord>();

            string[] xColumns = { "Name", "Path", "DataType", "LogicalAddres", "Comment" };
            lsvXls.View = new ListViewManager().makeAndBindGridView(xColumns);

            string[] cfgColumns = { "Name", "SignalType", "Device", "DeviceMap" };
            lsvCfgLoaded.View = new ListViewManager().makeAndBindGridView(cfgColumns);
            lsvCfgFinal.View = new ListViewManager().makeAndBindGridView(cfgColumns);

        }

        private void refrehXlsListView()
        {
            lsvXls.Items.Clear();
            foreach(XRecord xrec in xlsRecords)
            {
                lsvXls.Items.Add(xrec);
            }
        }

        private void refreshCfgLoadedListView()
        {
            lsvCfgLoaded.Items.Clear();
            foreach (CfgRecord cfgrec in cfgRecords)
            {
                lsvCfgLoaded.Items.Add(new ListViewManager().createEioSignalView(cfgrec));
            }
        }

        private void refreshCfgFinalListView()
        {
            lsvCfgFinal.Items.Clear();
            foreach (CfgRecord cfgrec in cfgRecordsSelected)
            {
                lsvCfgFinal.Items.Add(new ListViewManager().createEioSignalView(cfgrec));
            }
        }

        private void loadXlsRecords()
        {
            XReader xreader = new XReader(Xpath);
            xlsRecords = xreader.Read();
            refreshListViews();
        }
        private void loadCfgRecords()
        {
            CfgReader cfgreader = new CfgReader(CfgPath);
            List<CfgRecord> cfgRecordsAll = cfgreader.mapCfgFile();

            cfgRecords = new List<CfgRecord>();
            foreach(CfgRecord cfgrec in cfgRecordsAll)
            {
                if(cfgrec.TypeOfRecord == "EIO_SIGNAL" )
                {
                    cfgRecords.Add(cfgrec);
                }
            }
            refreshListViews();
        }

        private void refreshListViews()
        {
            refrehXlsListView();
            refreshCfgLoadedListView();
            refreshCfgFinalListView();
        }

        private void btnRemoveRecord_Click(object sender, RoutedEventArgs e)
        {
            if (lsvCfgFinal.SelectedItems.Count > 0)
            {
                List<CfgRecord> toRemove = new List<CfgRecord>();

                foreach (var item in lsvCfgFinal.SelectedItems)
                {
                    int index = lsvCfgFinal.Items.IndexOf(item);
                    
                    if(cfgRecordsSelected[index].XlsRecordTwin != null)
                    {
                        xlsRecords.Add(cfgRecordsSelected[index].XlsRecordTwin);
                        toRemove.Add(cfgRecordsSelected[index]);
                    }
                    else
                    {
                        cfgRecords.Add(cfgRecordsSelected[index]);
                        toRemove.Add(cfgRecordsSelected[index]);
                    }
                }
                foreach(CfgRecord cfg in toRemove) 
                {
                    cfgRecordsSelected.Remove(cfg);
                }
            }
            else
            {
                MessageBox.Show("Please choose at least one record to remove");
            }
            refreshListViews();
        }

        private void SelectAllXls_Checked(object sender, RoutedEventArgs e)
        {
            lsvXls.SelectAll();
        }

        private void chckboxSelectAllCfg_Checked(object sender, RoutedEventArgs e)
        {
            lsvCfgLoaded.SelectAll();
        }

        private void SelectAllXls_Unchecked(object sender, RoutedEventArgs e)
        {

            lsvXls.SelectedItems.Clear();
            lsvXls.UnselectAll();
         
        }

        private void chckboxSelectAllCfg_Unchecked(object sender, RoutedEventArgs e)
        {
            lsvCfgLoaded.SelectedItems.Clear();
            lsvCfgLoaded.UnselectAll();
        }

        private void lsvXls_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
  
        }

        private void lsvCfgLoaded_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
       
        }
    }
}
