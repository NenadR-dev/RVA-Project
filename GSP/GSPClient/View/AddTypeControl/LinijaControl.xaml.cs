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
using GSPClient.ViewModel;
using System.Collections;
using System.Collections.ObjectModel;
using GSPPackage.Models;

namespace GSPClient.View.AddTypeControl
{
    /// <summary>
    /// Interaction logic for LinijaControl.xaml
    /// </summary>
    public partial class LinijaControl : UserControl
    {
        public static ObservableCollection<SoferViewModel> Drivers { get; set; }
        public static ObservableCollection<BusViewModel> Buses { get; set; }
        private static IGSPService proxy;
        private static IGSPDataService dataProxy;
        public LinijaControl()
        {
            
            InitializeComponent();
            InitializeData();
            this.DataContext = this;
        }

        private void InitializeData()
        {
            proxy = new ChannelFactory<IGSPService>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:8001/GSP")).CreateChannel();

            Drivers = new ObservableCollection<SoferViewModel>();
            List<Linija> lList = proxy.GetLinijaList();
            List<Vozac> dList = proxy.GetSoferList();
            Buses = new ObservableCollection<BusViewModel>();
            List<Autobus> bList = proxy.GetAutobusList();
            if (lList.Count > 0)
            {
                dList.ForEach(driver =>
                {
                    foreach (var node in lList)
                    {
                        if (!node.Vozaci.ToList().Exists(x => x.ID == driver.ID))
                            Drivers.Add(new SoferViewModel(driver.Ime, driver.ID));
                    }
                });


                bList.ForEach(bus =>
                {
                    foreach (var node in lList)
                    {
                        if (!node.M_Autobus.ToList().Exists(x => x.ID == bus.ID))
                            Buses.Add(new BusViewModel(bus.Oznaka, bus.ID));
                    }
                });
            }
            else
            {
                dList.ForEach(driver =>
                {

                   Drivers.Add(new SoferViewModel(driver.Ime, driver.ID));

                });


                bList.ForEach(bus =>
                {

                    Buses.Add(new BusViewModel(bus.Oznaka, bus.ID));

                });
            }
        }

        private void AddSoferBtn_Click(object sender, RoutedEventArgs e)
        {
            dataProxy = new ChannelFactory<IGSPDataService>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:8002/GSPData")).CreateChannel();

            List<Autobus> selectedBuses = new List<Autobus>();
            List<Autobus> bList = proxy.GetAutobusList();
            foreach(var node in Buses)
            {
                if(node.IsBusSelected == true)
                {
                    selectedBuses.Add(bList.Find(y => y.ID == node.ID));
                }
            }

            List<Vozac> selectedDrivers = new List<Vozac>();
            List<Vozac> dList = proxy.GetSoferList();
            foreach(var node in Drivers)
            {
                if(node.IsSoferSelected == true)
                {
                    selectedDrivers.Add(dList.Find(y => y.ID == node.ID));
                }
            }
            Linija line = new Linija()
            {
                Oznaka = OznakaBox.Text,
                M_Autobus = selectedBuses,
                Vozaci = selectedDrivers
            };
            if (dataProxy.ComputeCommandLine(line, "ADD"))
            {
                MessageBox.Show("New linija created!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Could not create linija", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
