using SipaaKernelV3.Shard;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernelV3.Commands.FileSystem
{
    public class DelCommand : Command
    {
        public DelCommand() : base("del", "Delete dirs/files.", new string[] { "del [type : file | dir] [name]" }) { }

        public override CommandResult Execute(List<string> args)
        {
            string type = args[0];
            string name = args[1];

            switch (type)
            {
                case "file":
                    File.Delete(Kernel.CurrentDir + name);
                    return CommandResult.Success;
                case "dir":
                    Directory.Delete(Kernel.CurrentDir + name);
                    return CommandResult.Success;
                default:
                    Console.WriteLine("Unknown type. The valid types are [file | dir].");
                    return CommandResult.Error;
            }
        }
    }
}
