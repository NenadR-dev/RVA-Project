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
namespace GSPClient.View.Forms
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        public Login()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            IGSPService proxy = new ChannelFactory<IGSPService>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:8001/GSP")).CreateChannel();
            try
            {
                string result = proxy.Login(UsernameBox.Text, PasswordBox.Text);
                if(result.Contains("Success"))
                {
                    Application application = Application.Current;
                    application.MainWindow.Width = 1080;
                    application.MainWindow.Height = 600;
                    string[] parts = result.Split(';');
                    application.MainWindow.DataContext = new AdminForm(parts[1]);
                }
                else if(result.Contains("Success"))
                {
                    string[] parts = result.Split(';');
                    Application application = Application.Current;
                    application.MainWindow.Width = 1080;
                    application.MainWindow.Height = 600;
                    application.MainWindow.DataContext = new UserForm(parts[1]);
                }
                else
                {
                    ErrorMSG.Content = "LoginError: Failed to login";
                    ErrorMSG.Foreground = Brushes.Red;
                }
            }
            catch(Exception ep)
            {
                ErrorMSG.Content = "Error occured";
                ErrorMSG.Foreground = Brushes.Red;
            }
        }
    }
}
