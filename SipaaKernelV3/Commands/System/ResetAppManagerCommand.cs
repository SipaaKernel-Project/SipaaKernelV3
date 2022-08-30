using SipaaKernelV3.Shard;
using SipaaKernelV3.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SipaaKernelV3.Kernel;

namespace SipaaKernelV3.Commands.System
{
    public class ResetAppManagerCommand : Command
    {
        public ResetAppManagerCommand() : base("resetappmanager", "Reset app manager.", new string[] {"resetappmanager"})
        {
        }

        public override CommandResult Execute(List<string> args)
        {
            //SKShowWindow(0);
            appManager = new Core.AppManager();
            Console.WriteLine("Sucessfully resetted app manager");
            return CommandResult.Success;
        }
    }
}
