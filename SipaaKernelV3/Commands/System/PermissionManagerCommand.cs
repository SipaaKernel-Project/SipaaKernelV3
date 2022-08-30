using SipaaKernelV3.Shard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernelV3.Commands.System
{
    public class PermissionManagerCommand : Command
    {
        public PermissionManagerCommand() : base("permmanager", "Permission manager command", new string[] { "permmanager --token", "permmanager --set [permission : user | " }) { }

        public override CommandResult Execute(List<string> args)
        {
            if (args[0].StartsWith("--token"))
            {
                Console.WriteLine("Permission Manager Token : " + Kernel.permToken);
            }
            else if (args[0].StartsWith("--set"))
            {
                string perm = args[1];
                uint token = 0;
                Permission givenPerm = PermissionManager.GetPermissionFromString(perm);

                // VERIFY IF PERMISSION IS UNKNOWN / ZERO
                if (givenPerm == Permission.PermZero || givenPerm == Permission.PermUnknown)
                {
                    Console.WriteLine("Error happened : The permission is unknown or a system-only permission.");
                    return CommandResult.Error;
                }

                // REQUIRE TOKEN
                Console.Write("Enter token : ");
                token = uint.Parse(Console.ReadLine());

                // GIVE PERMISSION
                var sucess = Kernel.permManager.AddPermission(givenPerm, token);

                if (sucess == 0) { Console.WriteLine("Permission " + perm + " given sucessfully!"); }
                else { Console.WriteLine("Error happened : The permission manager cannot give this permission."); return CommandResult.Error; }
            }
            return CommandResult.Success;
        }
    }
}
