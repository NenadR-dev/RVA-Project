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
using GSPPackage.Models;
using System.ServiceModel;
using GSPClient.ViewModel;
using System.Collections.ObjectModel;

namespace GSPClient.View.ManageControl
{
    /// <summary>
    /// Interaction logic for ManageLineWindow.xaml
    /// </summary>
    public partial class ManageLineWindow : Window
    {
        public static ObservableCollection<SoferViewModel> Drivers { get; set; }
        public static ObservableCollection<BusViewModel> Buses { get; set; }
        private static IGSPService proxy;
        private static IGSPDataService dataProxy;
        private static Linija currentLine = null;

        public ManageLineWindow(Linija data)
        {
            currentLine = data;
            Drivers = new ObservableCollection<SoferViewModel>();
            Buses = new ObservableCollection<BusViewModel>();
            InitializeComponent();
            InitializeCData(data);
            OznakaBox.Text = data.Oznaka;
            this.DataContext = this;
        }

        private void InitializeCData(Linija data)
        {
            data.M_Autobus.ToList().ForEach(x =>
            {
                Buses.Add(new BusViewModel(x.Oznaka,x.ID));
            });
            data.Vozaci.ToList().ForEach(x =>
            {
                Drivers.Add(new SoferViewModel(x.Oznaka, x.ID));
            });
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            dataProxy = new ChannelFactory<IGSPDataService>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:8002/GSPData")).CreateChannel();
            if (dataProxy.ComputeCommandLine(currentLine, "UPDATE"))
            {
                MessageBox.Show("Line updated");
            }
            else
            {
                MessageBox.Show("Error: cannot update line");
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            dataProxy = new ChannelFactory<IGSPDataService>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:8002/GSPData")).CreateChannel();
            if (dataProxy.ComputeCommandLine(currentLine, "DELETE"))
            {
                MessageBox.Show("Line deleted");
            }
            else
            {
                MessageBox.Show("Error: cannot delete line");
            }
        }
    }
}
