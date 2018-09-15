using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSPPackage;
using GSPPackage.Models;

namespace GSPServer.App_Service
{
    public class GSPDataAccess : DbContext
    {
        private static GSPDataAccess DB = null;
        
        //Creating Singleton
        public static GSPDataAccess Connect()
        {
            if(DB == null)
            {
                DB = new GSPDataAccess();
            }
            return DB;
        }

        private GSPDataAccess() : base("GSPDB")
        {

        }

        public virtual DbSet<Linija> dbSetLinija { get; set; }
        public virtual DbSet<Vozac> dbSetVozac { get; set; }
        public virtual DbSet<Autobus> dbSetAutobus { get; set; }
        public virtual DbSet<MyUser> dbSetUser { get; set; }
    }
}
