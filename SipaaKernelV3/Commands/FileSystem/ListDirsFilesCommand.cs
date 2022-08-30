using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SipaaKernelV3.Shard;

namespace SipaaKernelV3.Commands.FileSystem
{
    public class ListDirsFilesCommand : Command
    {
        public ListDirsFilesCommand() : base("ldf", "List dirs and files of the current directory.", new string[] { "ldf" })
        {
        }

        public override CommandResult Execute(List<string> args)
        {
            Console.WriteLine("Showing contents of " + Kernel.CurrentDir + ".");
            Console.WriteLine();
            foreach (string dir in Directory.GetDirectories(Kernel.CurrentDir))
            {
                Console.WriteLine(dir + " : Directory");
            }
            foreach (string file in Directory.GetFiles(Kernel.CurrentDir))
            {
                Console.WriteLine(file + " : File");
            }
            return CommandResult.Success;
        }
    }
}
