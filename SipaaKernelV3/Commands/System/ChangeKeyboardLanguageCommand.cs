using Cosmos.System.ScanMaps;
using SipaaKernelV3.Shard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sys = Cosmos.System;

namespace SipaaKernelV3.Commands.System
{
    public class ChangeKeyboardLanguageCommand : Command
    {
        public ChangeKeyboardLanguageCommand() : base("chkbl", "Change keyboard language.", new string[] { "chkbl [language : french | english]" }) { }

        public override CommandResult Execute(List<string> args)
        {
            if (args.Count == 0)
            {
                Console.WriteLine("All keyboard languages");
                Console.WriteLine("french : French keyboard");
                Console.WriteLine("english : English (USA) keyboard");
                return CommandResult.Success;
            }
            else if (args.Count == 1)
            {
                if (args[0] == "french")
                {
                    Sys.KeyboardManager.SetKeyLayout(new FR_Standard());
                    return CommandResult.Success;
                }
                else if (args[0] == "english")
                {
                    Sys.KeyboardManager.SetKeyLayout(new US_Standard());
                    return CommandResult.Success;
                }
                else
                {
                    return CommandResult.InvalidArgs;
                }
            }
            else
            {
                return CommandResult.InvalidArgs;
            }
        }
    }
}
