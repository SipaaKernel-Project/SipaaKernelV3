using Cosmos.HAL;
using SipaaKernelV3.Shard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernelV3.Commands.System
{
    public class DateCommand : Command
    {
        public DateCommand() : base("date", "Show current date.", new string[] { "date" }) { }

        public override CommandResult Execute(List<string> args)
        {
            string[] days = new string[]
            {
                "Monday",
                "Tuesday",
                "Wednesday",
                "Thursday",
                "Friday",
                "Saturday",
                "Sunday"
            };

            string[] months = new string[]
            {
                "January",
                "February",
                "March",
                "April",
                "May",
                "June",
                "July",
                "August",
                "September",
                "October",
                "November",
                "December"
            };

            uint dayOfWeek = RTC.DayOfTheWeek;
            uint dayOfMonth = RTC.DayOfTheMonth;
            uint monthOfYear = RTC.Month;
            uint year = RTC.Year;

            Console.Write(days[dayOfWeek - 1]);
            Console.Write(",");
            Console.Write(dayOfMonth);
            Console.Write(" of ");
            Console.Write(months[monthOfYear - 1] + " " + year);
            Console.WriteLine();
            Console.WriteLine(dayOfMonth + "/" + monthOfYear + "/" + year);

            return CommandResult.Success;
        }
    }
}
