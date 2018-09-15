using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSPClient.ViewModel
{
    public class LineDataViewModel : INotifyPropertyChanged
    {

        private string _name;
        private int _lineID;

        public string LineName
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                //PropertyChanged(this, new PropertyChangedEventArgs("LineName"));
            }
        }

        public int ID
        {
            get
            {
                return _lineID;
            }
            set
            {
                _lineID = value;
                //PropertyChanged(this, new PropertyChangedEventArgs("ID"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
