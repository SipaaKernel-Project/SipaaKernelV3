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
    public class EchoCommand : Command
    {
        public EchoCommand() : base("echo", "Rewrite the arguments into the console.", new string[] {"gui"})
        {
        }

        public override CommandResult Execute(List<string> args)
        {
            string s = "";
            foreach (var arg in args)
            {
                s += arg + " ";
            }
            Console.WriteLine(s);
            return CommandResult.Success;
        }
    }
}
