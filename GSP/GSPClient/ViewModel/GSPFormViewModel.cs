using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace GSPClient.ViewModel
{
    public class GSPFormViewMode : INotifyPropertyChanged
    {
        private string errorMSG;

        public string ErrorMSG
        {
            get
            {
                return errorMSG;
            }
            set
            {
                errorMSG = value;
                OnPropertyChanged("ErrorMSG");
            }
        }

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
