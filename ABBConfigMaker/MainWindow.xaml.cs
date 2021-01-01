﻿using System;
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
            


        }

        private void btn_MakeFile_Click(object sender, RoutedEventArgs e)
        {
            XReader xreader = new XReader(Xpath);
            List<XRecord> readedRecords = xreader.Read();

            CfgReader cfgReader = new CfgReader(CfgPath);
            List<CfgRecord> cfgRecords = cfgReader.mapCfgFile();

            //Zkontroovat duplicity jen u těch záznamů, které jsou jk v xlsx souboru tak v cfg souboru, což budou jen ty, které mají Device "PN_Internal_Device"

            string s = string.Empty;

            foreach(XRecord rec in readedRecords)
            {
                s += rec.toCfgString();
               
            }

            //CfgReader cfgReader = new CfgReader();
        }

        private void btn_loadFile_Copy_Click(object sender, RoutedEventArgs e)
        {
            //zde ještě cfg checker
            Loader cfgLoader = new Loader(false);
            if (cfgLoader.fileReady)
            {
                CfgPath = cfgLoader.Path;
                txt_cfgFile.Text = CfgPath;
            }

        }
    }
}
