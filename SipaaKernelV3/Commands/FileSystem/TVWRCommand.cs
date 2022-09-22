using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SipaaKernelV3.Shard;

namespace SipaaKernelV3.Commands.FileSystem
{
    public class TVWRCommand : Command
    {
        public TVWRCommand() : base("tvwr", "View file contents", new string[] {"tvwr [filename]" }) { }

        public override CommandResult Execute(List<string> args)
        {
            try
            {
                string file = args[0];

                if (file.StartsWith(@"0:\"))
                {
                    if (File.Exists(file))
                    {
                        foreach (string line in File.ReadAllLines(Kernel.CurrentDir + file))
                        {
                            Console.WriteLine(line);
                        }
                    }
                    else
                    {
                        Console.WriteLine("File doens't exists.");
                        return CommandResult.Error;
                    }
                }
                else
                {
                    if (File.Exists(Kernel.CurrentDir + file))
                    {
                        foreach (string line in File.ReadAllLines(Kernel.CurrentDir + file))
                        {
                            Console.WriteLine(line);
                        }
                    }
                    else
                    {
                        Console.WriteLine("File doens't exists.");
                        return CommandResult.Error;
                    }
                }

                return CommandResult.Success;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return CommandResult.Error;
            }
        }
    }
}
