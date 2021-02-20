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
    class ListViewManager
    {
        public ListViewManager()
        {

        }
        public GridView makeAndBindGridView(string[] parameters)
        {
            GridView gv = new GridView();
            foreach (string s in parameters)
            {
                GridViewColumn gvc = new GridViewColumn();
                gvc.Header = s;
                gvc.DisplayMemberBinding = new Binding(s);
                gv.Columns.Add(gvc);
            }
            return gv;
        }

        public EIO_Signal_ViewDataModel createEioSignalView(CfgRecord EIO_Signal)
        {
            EIO_Signal_ViewDataModel eio = new EIO_Signal_ViewDataModel();
            eio.Name = EIO_Signal.parametersInCfg["Name"];
            eio.SignalType = EIO_Signal.parametersInCfg["SignalType"];
            eio.Device = EIO_Signal.parametersInCfg["Device"];
            eio.DeviceMap = EIO_Signal.parametersInCfg["DeviceMap"];
            eio.ReferenceCfgRecord = EIO_Signal;

            return eio;
        }

       







    }
}
