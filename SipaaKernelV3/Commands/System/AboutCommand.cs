using SipaaKernelV3.Shard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernelV3.Commands.System
{
    public class AboutCommand : Command
    {
        public AboutCommand() : base("about", "Displays info about SipaaKernel", new string[] {"about"})
        {
        }

        public override CommandResult Execute(List<string> args)
        {
            Console.WriteLine(OSInfo.OSName);
            Console.WriteLine("Version " + OSInfo.OSVersion + " (build " + OSInfo.OSBuild + ")");
            Console.WriteLine("Thanks to :");
            Console.WriteLine("");
            foreach (string contributor in OSInfo.thanks)
            {
                Console.WriteLine(contributor);
            }
            return CommandResult.Success;
        }
    }
}
