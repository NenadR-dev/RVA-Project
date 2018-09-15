using GSPClient.View.AddTypeControl;
using GSPClient.View.ManageControl;
using GSPPackage;
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

namespace GSPClient.View.Forms
{
    /// <summary>
    /// Interaction logic for UserForm.xaml
    /// </summary>
    public partial class UserForm : UserControl
    {
        private static string key = string.Empty;
        private static UserControl usc = null;
        public string currentControl = string.Empty;

        public UserForm(string KeyData)
        {
            key = KeyData;
            InitializeComponent();
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GridMain.Children.Clear();

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ItemHome":
                    usc = new ManageHome();
                    GridMain.Children.Add(usc);
                    currentControl = "Home";
                    break;
                case "ItemCreate":
                    usc = new AddControl();
                    GridMain.Children.Add(usc);
                    currentControl = "Create";
                    break;
                case "ItemUndo":
                    CallAndExecuteUndo();
                    RefreshPage();
                    break;
                case "ItemRedo":
                    CallAndExecuteRedo();
                    RefreshPage();
                    break;
                case "ItemRefresh":
                    usc.UpdateDefaultStyle();
                    usc.UpdateLayout();
                    break;
                default:
                    break;
            }
        }

        private void RefreshPage()
        {
            switch (currentControl)
            {
                case "Home":
                    {
                        usc = new ManageHome();
                        GridMain.Children.Add(usc);
                        break;
                    }
                case "Create":
                    {
                        usc = new AddControl();
                        GridMain.Children.Add(usc);
                        break;
                    }
                case "User":
                    {
                        usc = new AddUserControl();
                        GridMain.Children.Add(usc);
                        break;
                    }
            }
        }

        private void CallAndExecuteRedo()
        {
            IGSPDataService proxy = new ChannelFactory<IGSPDataService>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:8002/GSPData")).CreateChannel();
            if (proxy.Redo())
            {
                GridMain.Children.Clear();
                GridMain.Children.Add(usc);
            }
            else
            {

            }
        }

        private void CallAndExecuteUndo()
        {
            IGSPDataService proxy = new ChannelFactory<IGSPDataService>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:8002/GSPData")).CreateChannel();
            if (proxy.Undo())
            {
                GridMain.Children.Clear();
                GridMain.Children.Add(usc);
            }
            else
            {

            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            GSPPackage.IGSPService proxy = new ChannelFactory<GSPPackage.IGSPService>(new NetTcpBinding(),
                new EndpointAddress("net.tcp://localhost:8001/GSP")).CreateChannel();
            proxy.Logout(key);
            App.Current.MainWindow.Close();
            MainWindow main = new MainWindow();
        }

        private void ManageAccount_Click(object sender, RoutedEventArgs e)
        {
            ManageUserWindow window = new ManageUserWindow(key);
            window.ShowDialog();
        }
    }
}
