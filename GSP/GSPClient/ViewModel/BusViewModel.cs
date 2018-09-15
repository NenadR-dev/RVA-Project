using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSPClient.ViewModel
{
    public class BusViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _busName;
        private bool _isSelected;
        private int _ID;

        public int ID
        {
            get { return _ID; }
        }

        public BusViewModel(string busName,int ID)
        {
            _busName = busName;
            _ID = ID;
        }

        public string BusName
        {
            get { return _busName; }
            set
            {
                _busName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("BusName"));
            }
        }
        public bool IsBusSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IsBusSelected"));
            }
        }
    }
}
