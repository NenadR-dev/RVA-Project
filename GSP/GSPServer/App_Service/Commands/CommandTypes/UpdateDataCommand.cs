using GSPPackage.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSPServer.App_Service.Commands.CommandTypes
{
    public class UpdateDataCommand : GSPDataCommand
    {
        public UpdateDataCommand(CommandExecutor Executor,object Data, DataType type) : base(Executor, type, Data) { }
        public override void Execute()
        {
            Executor.UpdateData(data, type);
        }

        public override void UnExecute()
        {
            Executor.UpdateData(data, type);
        }
    }
}
