using SipaaKernelV3.Shard;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernelV3.Commands.FileSystem
{
    public class CDCommand : Command
    {
        public CDCommand() : base("cd", "Change current dir", new string[] { "cd [dir]" }) { }

        public override CommandResult Execute(List<string> args)
        {
            var dir = args[0];

            if (dir == "..")
            {
                if (Kernel.CurrentDir == @"0:\")
                {
                    Console.WriteLine("Sorry, we can't go to parent directory...");
                    return CommandResult.Error;
                }
                DirectoryInfo dinfo = new DirectoryInfo(Kernel.CurrentDir);
                if (dinfo.Parent.FullName == @"0:\")
                {
                    Kernel.CurrentDir = dinfo.Parent.FullName;
                }
                else
                {
                    Kernel.CurrentDir = dinfo.Parent.FullName + @"0:\";
                }
                return CommandResult.Success;
            }
            else if (dir.StartsWith(@"0:\"))
            {
                if (!Directory.Exists(dir)) { Console.WriteLine("Directory don't exists!"); return CommandResult.Error; }
                Kernel.CurrentDir = dir + @"\";
                return CommandResult.Success;
            }
            else
            {
                if (!Directory.Exists(Kernel.CurrentDir + dir)) { Console.WriteLine("Directory don't exists!"); return CommandResult.Error; }
                Kernel.CurrentDir = Kernel.CurrentDir + dir + @"\";
                return CommandResult.Success;
            }
        }
    }
}
