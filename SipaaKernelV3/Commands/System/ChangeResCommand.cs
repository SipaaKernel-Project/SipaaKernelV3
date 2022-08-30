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
    public class ChangeResCommand : Command
    {
        public ChangeResCommand() : base("changeres", "Change GUI mode resolution.", new string[] {"changeres"})
        {
        }

        public override CommandResult Execute(List<string> args)
        {
            //SKShowWindow(0);
            SKChangeRes(uint.Parse(args[0]), uint.Parse(args[1]));
            Console.WriteLine("Restart GUI to see change appear.");
            return CommandResult.Success;
        }
    }
}
