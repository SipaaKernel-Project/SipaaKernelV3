namespace SipaaKernelV3.Shard
{
    public struct CommandLine
    {
        private string commandName;
        private List<string> args;

        public string CommandName { get => commandName; set => commandName = value; }
        public List<string> Arguments { get => args; set => args = value; }

        public CommandLine()
        {
            commandName = "";
            args = null;
        }
        public CommandLine(string cmdname)
        {
            commandName = cmdname;
            args = null;
        }
        public CommandLine(string cmdname, List<string> args)
        {
            commandName = cmdname;
            this.args = args;
        }
    }
}