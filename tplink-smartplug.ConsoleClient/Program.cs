using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommandLine;
using Nito.AsyncEx;

namespace tplink_smartplug.ConsoleClient
{
    class Program
    {
        private static readonly Dictionary<string, string> PredefinedCommands = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { nameof(Commands.On), Commands.On },
            { nameof(Commands.Off), Commands.Off },
            { nameof(Commands.Info), Commands.Info },
            { nameof(Commands.CloudInfo), Commands.CloudInfo },
            { nameof(Commands.WlanScan), Commands.WlanScan },
            { nameof(Commands.Time), Commands.Time },
            { nameof(Commands.Timezone), Commands.Timezone },
            { nameof(Commands.Schedules), Commands.Schedules },
            { nameof(Commands.Countdown), Commands.Countdown },
            { nameof(Commands.Antitheft), Commands.Antitheft },
            { nameof(Commands.Reboot), Commands.Reboot },
        };

        static void Main(string[] args)
        {
            AsyncContext.Run(() => MainAsync(args));
        }

        private static async Task MainAsync(string[] args)
        {
            var options = new Options();
            Parser.Default.ParseArgumentsStrict(args, options);

            var client = new TpLinkClient();
            var cmd = ResolveCommand(options);
            var result = await client.IssueCommand(cmd, options.IpAddress, 9999);

            Console.WriteLine("Sent    : " + cmd);
            Console.WriteLine("Received: " + result);
        }

        private static string ResolveCommand(Options options)
        {
            if (options.CommandJson != null)
            {
                return options.CommandJson;
            }

            string cmd;
            if (PredefinedCommands.TryGetValue(options.Command, out cmd))
            {
                return cmd;
            }
            return null;
        }
    }

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