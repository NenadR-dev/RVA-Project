using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using GSPPackage.Models;
using GSPPackage.Enums;

namespace GSPPackage
{
    [ServiceContract]
    public interface IGSPDataService
    {
        [OperationContract]
        bool ComputeCommandBus(Autobus Data,string command);
        [OperationContract]
        bool ComputeCommandDriver(Vozac Data,string command);
        [OperationContract]
        bool ComputeCommandLine(Linija Data, string command);
        [OperationContract]
        bool ComputeCommandUser(MyUser Data, string command);
        [OperationContract]
        bool Undo();
        [OperationContract]
        bool Redo();
        //[OperationContract]
        //bool RevertUpdate(object Data, DataType type, string command);
        //[OperationContract]
        //bool AddData(object Data, DataType type, string command);
        //[OperationContract]
        //bool DeleteData(object Data, DataType type, string command);
    }
}
