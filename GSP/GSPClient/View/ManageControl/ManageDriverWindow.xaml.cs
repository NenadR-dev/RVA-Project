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
    /// Interaction logic for ManageDriverWindow.xaml
    /// </summary>
    public partial class ManageDriverWindow : Window
    {
        private static IGSPDataService proxy;
        private static DriverDataViewModel editVozac { get; set; }

        public ManageDriverWindow(DriverDataViewModel data)
        {
            InitializeComponent();
            editVozac = data;
            IDBOX.Text = data.ID.ToString();
            LINEIDBOX.Text = data.DriversLineID.ToString();
            FirstNameBox.Text = data.DriversName;
            LastNameBox.Text = data.DriversLastName;
            DriverNumberBox.Text = data.DriversNumber;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            proxy = new ChannelFactory<IGSPDataService>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:8002/GSPData")).CreateChannel();
            if (proxy.ComputeCommandDriver(new Vozac()
            {
                ID = editVozac.ID,
                Ime = FirstNameBox.Text,
                Prezime = LastNameBox.Text,
                Oznaka = DriverNumberBox.Text
            }, "UPDATE"))
            {
                MessageBox.Show("Driver updated");
            }
            else
            {
                MessageBox.Show("Error: Failed to update driver");
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            proxy = new ChannelFactory<IGSPDataService>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:8002/GSPData")).CreateChannel();
            if (proxy.ComputeCommandDriver(new Vozac()
            {
                ID = editVozac.ID,
                Ime = FirstNameBox.Text,
                Prezime = LastNameBox.Text,
                Oznaka = DriverNumberBox.Text
            }, "DELETE"))
            {
                MessageBox.Show("Driver deleted");
            }
            else
            {
                MessageBox.Show("Error: Failed to delete driver");
            }
        }
    }
}
