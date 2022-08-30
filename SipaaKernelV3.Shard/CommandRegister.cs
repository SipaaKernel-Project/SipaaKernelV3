namespace SipaaKernelV3.Shard
{
    public class CommandRegister
    {
        private List<Command> commands;

        public List<Command> Commands { get => commands; }

        public CommandRegister()
        {
            this.commands = new List<Command>();
        }

        public void AppendCommand(Command c)
        {
            if (c == null) return;
            Commands.Add(c);
        }

        public void RemoveCommand(Command c)
        {
            if (c == null) return;
            Commands.Remove(c);
        }
    }
}