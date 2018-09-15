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
using GSPPackage;
using System.ServiceModel;
using GSPPackage.Models;

namespace GSPClient.View
{
    /// <summary>
    /// Interaction logic for AutobusControl.xaml
    /// </summary>
    public partial class AutobusControl : UserControl
    {

        public AutobusControl()
        {
            InitializeComponent();
        }

        private void AddAutobusBtn_Click(object sender, RoutedEventArgs e)
        {
            IGSPDataService proxy = new ChannelFactory<IGSPDataService>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:8002/GSPData")).CreateChannel();

            try
            {
                Autobus bus = new Autobus()
                {
                    Oznaka = OznakaBox.Text
                };
                bool info = proxy.ComputeCommandBus(bus, "ADD");
                if (info)
                {
                    MessageBox.Show("Autobus added!", "Success", MessageBoxButton.OK);
                }
                else
                {
                    ErrorMSG.Foreground = Brushes.Red;
                    ErrorMSG.Text = "Error. Autobus already exists";
                }
            }
            catch(Exception ex)
            {
                ErrorMSG.Foreground = Brushes.Red;
                ErrorMSG.Text = "Unknown error occured";
            }
        }
    }
}
