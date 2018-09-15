using GSPPackage.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSPServer.App_Service.Commands.CommandTypes
{
    public class AddDataCommand : GSPDataCommand
    {
        public AddDataCommand(CommandExecutor executor, object data, DataType type) : base(executor, type, data)
        {

        }

        public override void Execute()
        {
            Executor.AddData(data, type);
        }

        public override void UnExecute()
        {
            Executor.DeleteData(data, type);
        }
    }
}
