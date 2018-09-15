using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSPClient.ViewModel
{
    public class DataViewModel
    {
        public LineDataViewModel LineVM { get; set; }
        public DriverDataViewModel DriverVM { get; set; }
        public BusDataViewModel BusVM { get; set; }
    }
}
