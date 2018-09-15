using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSPClient.ViewModel
{
    public class BusDataViewModel : INotifyPropertyChanged
    {

        private string _bus;
        private int _busLineID;
        public int ID { get; set; }


        public int BusLineID
        {
            get
            {
                return _busLineID;
            }
            set
            {
                _busLineID = value;
                //PropertyChanged(this, new PropertyChangedEventArgs("BusLineID"));
            }
        }
        public string Bus
        {
            get { return _bus; }
            set
            {
                _bus = value;
                //PropertyChanged(this, new PropertyChangedEventArgs("Bus"));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
