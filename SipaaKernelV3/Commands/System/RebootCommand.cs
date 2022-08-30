using SipaaKernelV3.Shard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernelV3.Commands.System
{
    public class RebootCommand : Command
    {
        public RebootCommand() : base("reboot", "Unload everything and reboot SipaaKernel V3.", new string[] { "shutdown" }) { }

        public override CommandResult Execute(List<string> args)
        {
            Kernel.permManager.AddPermission(Permission.PermZero, Kernel.permToken);
            Console.Clear();
            Console.WriteLine("Rebooting...");
            Kernel.appManager = null;
            Kernel.cr = null;
            Kernel.d = null;
            Kernel.mb = null;
            Kernel.sh = null;
            Cosmos.HAL.Global.PIT.Wait(5000);
            Console.WriteLine("Thanks for using SipaaKernel V3!");
            Cosmos.HAL.Global.PIT.Wait(2000);
            Cosmos.System.Power.Reboot();
            return CommandResult.Success;
        }
    }
}
