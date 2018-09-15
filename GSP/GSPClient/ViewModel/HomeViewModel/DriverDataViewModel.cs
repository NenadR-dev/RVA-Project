using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSPClient.ViewModel
{
    public class DriverDataViewModel : INotifyPropertyChanged
    {

        private string _driversName;
        private string _driversLastname;
        private string _driversNumber;
        private int _driverLineID;
        public int ID { get; set; }

        public string DriversName
        {
            get
            {
                return _driversName;
            }
            set
            {
                _driversName = value;
                //PropertyChanged(this, new PropertyChangedEventArgs("DriversName"));
            }
        }

        public string DriversLastName
        {
            get
            {
                return _driversLastname;
            }
            set
            {
                _driversLastname = value;
                //PropertyChanged(this, new PropertyChangedEventArgs("DriversLastName"));
            }
        }

        public string DriversNumber
        {
            get
            {
                return _driversNumber;
            }
            set
            {
                _driversNumber = value;
                //PropertyChanged(this, new PropertyChangedEventArgs("DriversNumber"));
            }
        }

        public int DriversLineID
        {
            get { return _driverLineID; }
            set
            {
                _driverLineID = value;
                //PropertyChanged(this, new PropertyChangedEventArgs("DriversLineID"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
