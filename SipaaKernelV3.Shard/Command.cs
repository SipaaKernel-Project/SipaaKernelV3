namespace SipaaKernelV3.Shard
{
    public abstract class Command
    {
        private string name = "", description = "";
        private readonly string[] usages = null;

        public string Name { get => name; }
        public string Description { get => description; }
        public string[] Usages { get => usages; }

        public Command(string name, string description, string[] usages)
        {
            this.name = name;
            this.description = description;
            this.usages = usages;
        }

        public abstract CommandResult Execute(List<string> args);
    }
    public enum CommandResult
    {
        Error,
        Success,
        InvalidArgs,
    }
}