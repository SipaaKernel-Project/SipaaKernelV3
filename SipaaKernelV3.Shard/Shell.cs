
namespace SipaaKernelV3.Shard
{
    public class Shell
    {
        private CommandRegister commandRegister;
        private string startLine = "user@shard:> ";

        public CommandRegister CommandRegister { get => commandRegister; set => commandRegister = value; }
        public string StartLine { get => startLine; set => startLine = value; }

        public Shell(CommandRegister cr)
        {
            commandRegister = cr;
        }
        
        public void GetAndRunCommand()
        {
            Console.Write(startLine);
            string input = Console.ReadLine();
            string[] cmdsplit = input.Split(' ');

            List<string> args = new List<string>();

            CommandLine cmdLine = new();

            if (commandRegister == null) { throw new Exception("Shard : CommandRegister is null."); }

            bool finded = false;
            Command findedCommand = null;

            foreach (Command c in CommandRegister.Commands)
            {
                if (c.Name == cmdsplit[0])
                {
                    finded = true;
                    findedCommand = c;
                    break;
                }
            }

            if (finded)
            {
                cmdLine.CommandName = cmdsplit[0];

                foreach (string arg in cmdsplit)
                {
                    args.Add(arg);
                }
                args.Remove(cmdsplit[0]);

                cmdLine.Arguments = args;

                if (RunCommand(cmdLine, findedCommand) == CommandResult.UnloadShard) { return; }
            }
            else
            {
                Console.WriteLine("Command not finded!");
            }
        }

        private CommandResult RunCommand(CommandLine cmdLine, Command c)
        {
            var result = c.Execute(cmdLine.Arguments);

            switch (result)
            {
                case CommandResult.Success:
                    break;
                case CommandResult.Error:
                    Console.WriteLine("Error happened during command execution.", ConsoleColor.Red);
                    break;
                case CommandResult.InvalidArgs:
                    Console.WriteLine("Invalid arguments.", ConsoleColor.Red);
                    break;
            }

            return result;
        }
    }
}