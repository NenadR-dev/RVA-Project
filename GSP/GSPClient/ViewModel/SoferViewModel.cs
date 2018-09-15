using GSPPackage.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSPClient.ViewModel
{
    public class SoferViewModel : INotifyPropertyChanged
    {
        private bool _isSelected;
        private string _driverName;
        private int _ID;
        
        public int ID
        {
            get { return _ID; }
        }

        public SoferViewModel(string driverName,int ID)
        {
            _driverName = driverName;
            _isSelected = false;
            _ID = ID;
        }

        public bool IsSoferSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IsSoferSelected"));
            }
        }

        public string DriverName
        {
            get
            {
                return _driverName;
            }
            set
            {
                _driverName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("DriverName"));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
