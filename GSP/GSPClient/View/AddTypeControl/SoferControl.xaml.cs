using GSPPackage;
using GSPPackage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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

namespace GSPClient.View.AddTypeControl
{
    /// <summary>
    /// Interaction logic for SoferControl.xaml
    /// </summary>
    public partial class SoferControl : UserControl
    {
        public SoferControl()
        {
            InitializeComponent();
        }

        private void AddSoferBtn_Click(object sender, RoutedEventArgs e)
        {
            IGSPDataService proxy = new ChannelFactory<IGSPDataService>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:8002/GSPData")).CreateChannel();

            try
            {
                Vozac driver = new Vozac()
                {
                    Ime = ImeBox.Text,
                    Prezime = PrezimeBox.Text,
                    Oznaka = OznakaBox.Text
                };

                bool info = proxy.ComputeCommandDriver(driver, "ADD");
                if (info)
                {
                    MessageBox.Show("Sofer added!", "Success", MessageBoxButton.OK);
                }
                else
                {
                    ErrorMSG.Foreground = Brushes.Red;
                    ErrorMSG.Text = "Error. Sofer already exists";
                }
            }
            catch
            {
                ErrorMSG.Foreground = Brushes.Red;
                ErrorMSG.Text = "Unknown error occured";
            }
        }
    }
}
