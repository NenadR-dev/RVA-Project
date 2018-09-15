using GSPClient.View.AddTypeControl;
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

namespace GSPClient.View
{
    /// <summary>
    /// Interaction logic for AddControl.xaml
    /// </summary>
    public partial class AddControl : UserControl
    {
        public AddControl()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserControl usc = null;
            OptionGrid.Children.Clear();
            switch (((ComboBoxItem)((ComboBox)sender).SelectedItem).Name)
            {
                case "ItemSofer":
                    {
                        usc = new SoferControl();
                        OptionGrid.Children.Add(usc);
                        break;
                    }
                case "ItemAutobus":
                    {
                        usc = new AutobusControl();
                        OptionGrid.Children.Add(usc);
                        break;
                    }
                case "ItemLinija":
                    {
                        usc = new LinijaControl();
                        OptionGrid.Children.Add(usc);
                        break;
                    }
            }
        }
    }
}
