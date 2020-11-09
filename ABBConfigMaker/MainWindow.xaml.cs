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

using Excel = Microsoft.Office.Interop.Excel; 

namespace ABBConfigMaker
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string path = string.Empty;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_loadFile_Click(object sender, RoutedEventArgs e)
        {
            Loader loader = new Loader();
            if (loader.fileReady)
            {
                path = loader.Path;
                txt_loadedFile.Text = loader.Path;
            }
            


        }

        private void btn_MakeFile_Click(object sender, RoutedEventArgs e)
        {
            //XReader xreader = new XReader(path);
            //xreader.Read();

            //CfgReader cfgReader = new CfgReader();
        }

        private void btn_loadFile_Copy_Click(object sender, RoutedEventArgs e)
        {
            CfgLoader cfgloader = new CfgLoader();

        }
    }
}
