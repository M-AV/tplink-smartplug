using CommandLine;

namespace tplink_smartplug.ConsoleClient
{
    class Options
    {
        [Option('i', "ip", Required = true, HelpText = "IP address of device.")]
        public string IpAddress { get; set; }

        [Option('c', "command", HelpText = "Predefined command to send.", MutuallyExclusiveSet = "cmd")]
        public string Command { get; set; }

        [Option('j', "json", HelpText = "Command to send.", MutuallyExclusiveSet = "cmd")]
        public string CommandJson { get; set; }
    }
}