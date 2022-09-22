using SipaaKernelV3.Shard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernelV3.Commands.System
{
    public class ClearCommand : Command
    {
        public ClearCommand() : base("cls", "Clear console.", new string[] { "cls" }) { }

        public override CommandResult Execute(List<string> args)
        {
            Console.Clear();
            return CommandResult.Success;
        }
    }
}
