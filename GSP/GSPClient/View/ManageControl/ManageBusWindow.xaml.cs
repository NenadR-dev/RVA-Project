using GSPPackage.Models;
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
using System.Windows.Shapes;
using GSPPackage;
using System.ServiceModel;
using GSPClient.ViewModel;

namespace GSPClient.View.ManageControl
{
    /// <summary>
    /// Interaction logic for BusManageWindow.xaml
    /// </summary>
    public partial class ManageBusWindow : Window
    {
        private static Autobus newBus = new Autobus();
        private static IGSPDataService proxy;

        public ManageBusWindow(BusDataViewModel data)
        {
            InitializeComponent();
            BusNameBox.Text = data.Bus;
            IDBOX.Text = data.ID.ToString();
            LINEIDBOX.Text = data.BusLineID.ToString();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            newBus.ID = Int32.Parse(IDBOX.Text);
            newBus.Oznaka = BusNameBox.Text;
            proxy = new ChannelFactory<IGSPDataService>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:8002/GSPData")).CreateChannel();
            if(proxy.ComputeCommandBus(newBus, "UPDATE"))
            {
                MessageBox.Show("Bus updated");
            }
            else
            {
                MessageBox.Show("Error occured");
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            newBus.ID = Int32.Parse(IDBOX.Text);
            newBus.Oznaka = BusNameBox.Text;
            proxy = new ChannelFactory<IGSPDataService>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:8002/GSPData")).CreateChannel();
            if(proxy.ComputeCommandBus(newBus, "DELETE"))
            {
                MessageBox.Show("Bus deleted");
            }
            else
            {
                MessageBox.Show("Error occured");
            }
        }
    }
}
