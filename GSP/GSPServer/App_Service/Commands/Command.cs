using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSPServer.App_Service.Commands
{
    public abstract class Command
    {
        public Command()
        {

        }
        public abstract void Execute();
        public abstract void UnExecute();
    }
}
