using GSPClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Threading;

namespace GSPClient.View.ManageControl
{
    /// <summary>
    /// Interaction logic for ManageHome.xaml
    /// </summary>
    public partial class ManageHome : UserControl
    {
        private static IGSPService proxy;

        public ObservableCollection<DriverDataViewModel> DriverVM { get; set; }
        public ObservableCollection<BusDataViewModel> BusVM { get; set; }
        public ObservableCollection<LineDataViewModel> LineVM { get; set; }
        public ObservableCollection<string> Filters { get; set; }

        public static LineDataViewModel SelectedLine { get; set; }
        public static BusDataViewModel SelectedBus { get; set; }
        public static DriverDataViewModel SelectedDriver { get; set; }
        public static string SelectedFilter { get; set; }
        public ManageHome()
        {

            SelectedDriver = new DriverDataViewModel();
            SelectedBus = new BusDataViewModel();
            SelectedLine = new LineDataViewModel();

            Filters = new ObservableCollection<string>();
            LineVM = new ObservableCollection<LineDataViewModel>();
            BusVM = new ObservableCollection<BusDataViewModel>();
            DriverVM = new ObservableCollection<DriverDataViewModel>();
            InitializeComponent();
            InitializeData();
            this.DataContext = this;
        }


        private void InitializeData()
        {
            proxy = new ChannelFactory<IGSPService>(new NetTcpBinding(),
                new EndpointAddress("net.tcp://localhost:8001/GSP")).CreateChannel();

            List<Vozac> vList = proxy.GetSoferList();
            List<Autobus> aList = proxy.GetAutobusList();
            List<Linija> lList = proxy.GetLinijaList();
                lList.ForEach(node =>
                {
                    node.M_Autobus.ToList().ForEach(bus =>
                    {
                        BusVM.Add(new BusDataViewModel()
                        {
                            Bus = bus.Oznaka,
                            BusLineID = node.ID,
                            ID = bus.ID
                        });
                    });
                    node.Vozaci.ToList().ForEach(driver =>
                    {
                        DriverVM.Add(new DriverDataViewModel()
                        {
                            DriversName = driver.Ime,
                            DriversLastName = driver.Prezime,
                            DriversLineID = node.ID,
                            DriversNumber = driver.Oznaka,
                            ID = driver.ID
                        });
                    });
                });

                vList.ForEach(d =>
                {
                    if (!DriverVM.ToList().Exists(x => x.DriversNumber == d.Oznaka))
                    {
                        DriverVM.Add(new DriverDataViewModel()
                        {
                            DriversLastName = d.Prezime,
                            DriversNumber = d.Oznaka,
                            DriversLineID = 0,
                            DriversName = d.Ime
                        });
                    }
                });

                aList.ForEach(x =>
                {
                    if (!BusVM.ToList().Exists(y => y.Bus == x.Oznaka))
                    {
                        BusVM.Add(new BusDataViewModel
                        {
                            Bus = x.Oznaka,
                            BusLineID = 0
                        });
                    }
                });

                Filters.Add("None");
                lList.ForEach(node =>
                {
                    LineVM.Add(new LineDataViewModel()
                    {
                        LineName = node.Oznaka,
                        ID = node.ID
                    });
                    Filters.Add(node.ID.ToString());
                });

        }
        private void LineDG_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            List<Linija> lList = proxy.GetLinijaList();
            ManageLineWindow lineWindow = new ManageLineWindow(lList.Find(x => x.ID == SelectedLine.ID));
            lineWindow.ShowDialog();
        }

        private void DriverDG_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ManageDriverWindow driverWindow = new ManageDriverWindow(DriverVM.ToList().Find(x=> x.DriversNumber == SelectedDriver.DriversNumber));
            driverWindow.ShowDialog();
        }

        private void BusDG_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ManageBusWindow busWindow = new ManageBusWindow(BusVM.ToList().Find(x=> x.ID== SelectedBus.ID));
            busWindow.ShowDialog();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DriverDG.ItemsSource = null;
            BusDG.ItemsSource = null;
            LineDG.ItemsSource = null;

            proxy = new ChannelFactory<IGSPService>(new NetTcpBinding(),
                new EndpointAddress("net.tcp://localhost:8001/GSP")).CreateChannel();

            List<Vozac> vList = proxy.GetSoferList();
            List<Autobus> aList = proxy.GetAutobusList();
            List<Linija> lList = proxy.GetLinijaList();
            BusVM = new ObservableCollection<BusDataViewModel>();
            DriverVM = new ObservableCollection<DriverDataViewModel>();
            LineVM = new ObservableCollection<LineDataViewModel>();
            DriverDG.ItemsSource = DriverVM;
            LineDG.ItemsSource = LineVM;
            BusDG.ItemsSource = BusVM;

            lList.ForEach(node =>
            {
                node.M_Autobus.ToList().ForEach(bus =>
                {
                    if (SelectedFilter == "None")
                    {
                        BusVM.Add(new BusDataViewModel()
                        {
                            Bus = bus.Oznaka,
                            BusLineID = node.ID,
                            ID = bus.ID
                        });
                    }
                    else
                    {
                        if(node.ID == Int32.Parse(SelectedFilter))
                        BusVM.Add(new BusDataViewModel()
                        {
                            Bus = bus.Oznaka,
                            BusLineID = node.ID,
                            ID = bus.ID
                        });
                    }
                });
                node.Vozaci.ToList().ForEach(driver =>
                {
                    if(SelectedFilter == "None")
                        DriverVM.Add(new DriverDataViewModel()
                        {
                            DriversName = driver.Ime,
                            DriversLastName = driver.Prezime,
                            DriversLineID = node.ID,
                            DriversNumber = driver.Oznaka,
                            ID = driver.ID
                        });
                    else
                    {
                        if(node.ID == Int32.Parse(SelectedFilter))
                        {
                            DriverVM.Add(new DriverDataViewModel()
                            {
                                DriversName = driver.Ime,
                                DriversLastName = driver.Prezime,
                                DriversLineID = node.ID,
                                DriversNumber = driver.Oznaka,
                                ID = driver.ID
                            });
                        }
                    }
                });
            });

            if (SelectedFilter == "None")
            {
                vList.ForEach(d =>
                {
                    if (!DriverVM.ToList().Exists(x => x.DriversNumber == d.Oznaka))
                    {
                        DriverVM.Add(new DriverDataViewModel()
                        {
                            DriversLastName = d.Prezime,
                            DriversNumber = d.Oznaka,
                            DriversLineID = 0,
                            DriversName = d.Ime,
                            ID = d.ID
                        });
                    }
                });

                aList.ForEach(x =>
                {
                    if (!BusVM.ToList().Exists(y => y.Bus == x.Oznaka))
                    {
                        BusVM.Add(new BusDataViewModel
                        {
                            Bus = x.Oznaka,
                            BusLineID = 0,
                            ID = x.ID
                        });
                    }
                });
            }

            lList.ForEach(node =>
            {
                if(SelectedFilter == "None")
                    LineVM.Add(new LineDataViewModel()
                    {
                        LineName = node.Oznaka,
                        ID = node.ID
                    });
                else
                {
                    if(node.ID == Int32.Parse(SelectedFilter))
                    {
                        LineVM.Add(new LineDataViewModel()
                        {
                            LineName = node.Oznaka,
                            ID = node.ID
                        });
                    }
                }
            });
        }
    }
}
