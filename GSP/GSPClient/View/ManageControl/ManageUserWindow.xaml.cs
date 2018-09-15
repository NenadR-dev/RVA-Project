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
using System.Windows.Shapes;

namespace GSPClient.View.ManageControl
{
    /// <summary>
    /// Interaction logic for ManageUserWindow.xaml
    /// </summary>
    public partial class ManageUserWindow : Window
    {
        GSPPackage.IGSPService proxy = new ChannelFactory<GSPPackage.IGSPService>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:8001/GSP")).CreateChannel();

        public ManageUserWindow(string key)
        {
            InitializeComponent();
            MyUser user = proxy.GetEntity(key);
            usernameBox.Text = user.Username;
            passwordBox.Text = user.Password;
            ImeBox.Text = user.FirstName;
            PrezimeBox.Text = user.LastName;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CompleteBtn_Click(object sender, RoutedEventArgs e)
        {

            string[] data = new string[4];
            data[0] = usernameBox.Text;
            data[1] = passwordBox.Text;
            data[2] = ImeBox.Text;
            data[3] = PrezimeBox.Text;
            if(proxy.modifyEntity(data))
            {
                MessageBox.Show("Changes saved.", "Success!", MessageBoxButton.OK,MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Error occured", "Error!", MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
    }
}
