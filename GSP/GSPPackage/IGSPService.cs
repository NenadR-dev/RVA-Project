using GSPPackage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace GSPPackage
{
    [ServiceContract]
    public interface IGSPService
    {
        [OperationContract]
        string Login(string user,string pass);
        [OperationContract]
        bool addNewEntity(string data);

        [OperationContract]
        bool modifyEntity(string[] data);
        [OperationContract]
        void Logout(string key);
        [OperationContract]
        bool removeEntity(string user);

        [OperationContract]
        MyUser GetEntity(string key);
        [OperationContract]
        List<Vozac> GetSoferList();
        [OperationContract]
        List<Autobus> GetAutobusList();
        [OperationContract]
        List<Linija> GetLinijaList();


    }
}
