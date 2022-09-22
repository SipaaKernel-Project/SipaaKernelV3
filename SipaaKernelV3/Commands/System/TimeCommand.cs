using Cosmos.HAL;
using SipaaKernelV3.Shard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernelV3.Commands.System
{
    public class TimeCommand : Command
    {
        public TimeCommand() : base("time", "Show current time.", new string[] { "time" }) { }

        public override CommandResult Execute(List<string> args)
        {
            Console.WriteLine(RTC.Hour + ":" + RTC.Minute + ":" + RTC.Second);
            return CommandResult.Success;
        }
    }
}
