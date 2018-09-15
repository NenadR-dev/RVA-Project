using GSPPackage.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSPServer.App_Service.Commands
{
    public abstract class GSPDataCommand : Command
    {
        protected CommandExecutor Executor;
        protected DataType type;
        protected object data;

        public GSPDataCommand(CommandExecutor executor, DataType type, object data)
        {
            this.data = data;
            this.Executor = executor;
            this.type = type;
        }
    }
}
