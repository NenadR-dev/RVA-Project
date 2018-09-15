using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSPPackage;
using System.ServiceModel;
using GSPPackage.Models;

namespace GSPServer.App_Service
{
    public class GSPServerService
    {
        private ServiceHost host;
        private ServiceHost hostModify;
        public static readonly List<MyUser> users = new List<MyUser>();
        public GSPServerService()
        {
            host = new ServiceHost(typeof(GSPService));
            hostModify = new ServiceHost(typeof(GSPDataService));
            GSPDBConfig config = new GSPDBConfig();
            if (!users.Exists(x=> x.Username == "admin"))
             users.Add(new MyUser() {
                 FirstName = "admin",
                 LastName = "admin",
                 Username = "admin",
                 Password = "admin"
             });
            host.AddServiceEndpoint(typeof(IGSPService), new NetTcpBinding(), new Uri("net.tcp://localhost:8001/GSP"));
            hostModify.AddServiceEndpoint(typeof(IGSPDataService), new NetTcpBinding(), new Uri("net.tcp://localhost:8002/GSPData"));
        }

        public void Open()
        {
            try
            {
                host.Open();
                Console.WriteLine("GSP Server is up and running");
                hostModify.Open();
                Console.WriteLine("GSPDataService is up and running");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occured at starting server\n" + e.Message);
            }
        }
        public void Close()
        {
            try
            {
                host.Close();
                hostModify.Close();
                Console.WriteLine("GSP Server closed");
            }
            catch(Exception e)
            {
                Console.WriteLine("Error occured at shutting down server\n" + e.Message);
            }
        }

        public void GetEndPoints()
        {
               
        }
    }
}
