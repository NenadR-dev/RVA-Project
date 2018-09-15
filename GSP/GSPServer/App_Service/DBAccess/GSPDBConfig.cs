using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSPServer.App_Service
{
    public class GSPDBConfig : DbMigrationsConfiguration<GSPDataAccess>
    {
        public GSPDBConfig()
        {
            AutomaticMigrationDataLossAllowed = true;
            AutomaticMigrationsEnabled = true;
            ContextKey = "GSPDB";
        }
    }
}
