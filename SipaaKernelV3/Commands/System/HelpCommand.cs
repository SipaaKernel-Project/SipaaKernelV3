using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernelV3.Commands.System
{
    public class HelpCommand : Shard.Command
    {
        public HelpCommand() : base("help", "Show all commands and his descriptions.", new string[] { "help" }) { }

        public override Shard.CommandResult Execute(List<string> args)
        {
            foreach (Shard.Command c in Kernel.cr.Commands)
            {
                Console.WriteLine(c.Name + " : " + c.Description);
            }
            return Shard.CommandResult.Success;

        }
    }
}
