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
    public class GUICommand : Command
    {
        public GUICommand() : base("gui", "Run GUI mode of SipaaKernel.", new string[] {"gui"})
        {
        }

        public override CommandResult Execute(List<string> args)
        {
            //SKShowWindow(0);
            SKLoadGUI();
            return CommandResult.Success;
        }
    }
}
