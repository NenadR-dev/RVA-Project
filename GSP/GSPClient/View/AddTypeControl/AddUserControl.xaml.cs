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
    /// Interaction logic for AddUserControl.xaml
    /// </summary>
    public partial class AddUserControl : UserControl
    {
        public AddUserControl()
        {
            InitializeComponent();
        }

        private void addUserBtn_Click(object sender, RoutedEventArgs e)
        {

            IGSPDataService proxy = new ChannelFactory<IGSPDataService>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:8002/GSPData")).CreateChannel();

            try
            {
                MyUser user = new MyUser()
                {
                    FirstName = ImeBox.Text,
                    LastName = PrezimeBox.Text,
                    Username = usernameBox.Text,
                    Password = passwordBox.Text
                };
                if (proxy.ComputeCommandUser(user, "ADD"))
                {
                    MessageBox.Show("USer added");
                }
                else
                {
                    MessageBox.Show("Cannot add user");
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
