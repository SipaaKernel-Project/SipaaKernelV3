using SipaaKernelV3.Shard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernelV3.Commands.System
{
    public class ShutdownCommand : Command
    {
        public ShutdownCommand() : base("shutdown", "Unload everything and shutdown SipaaKernel V3.", new string[] { "shutdown" }) { }

        public override CommandResult Execute(List<string> args)
        {
            Kernel.permManager.AddPermission(Permission.PermZero, Kernel.permToken);
            Console.Clear();
            Console.WriteLine("Shutting down...");
            Kernel.appManager = null;
            Kernel.cr = null;
           // Kernel.d = null;
            Kernel.mb = null;
            Kernel.sh = null;
            Cosmos.HAL.Global.PIT.Wait(5000);
            Console.WriteLine("Thanks for using SipaaKernel V3!");
            Cosmos.HAL.Global.PIT.Wait(2000);
            Console.Clear();
            Console.WriteLine("It is now safe to turn off your computer.");
            while (true)
            {
                Cosmos.HAL.Global.PIT.Wait(2000);
            }
            return CommandResult.Success;
        }
    }
}
