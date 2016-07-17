using AppBeat.CLI.Types;
using System.Reflection;
using System.IO;
using System.Linq;

namespace AppBeat.CLI.Command
{
    public class Help : BaseCommand, ICommand
    {
        public string Description
        {
            get
            {
                return "Display this help.";
            }
        }

        public ExitCode Run(string[] args)
        {
            WriteLine($"Usage: dotnet {Path.GetFileName(Assembly.GetEntryAssembly().Location)} [command] [options]");
            WriteLine();
            WriteLine("options:");

            var supportedCommands = Factory.SupportedCommands;
            var maxCommandNameLength = supportedCommands.Max(e => e.Length);
            var startDescriptionAt = maxCommandNameLength + 10; //make some space between command name and description

            foreach (var command in supportedCommands)
            {
                WriteLine($"  {command}{new string(' ', startDescriptionAt - command.Length)}{Factory.GetCommandByName(command)?.Description}");
                WriteLine();
            }

            return ExitCode.OK;
        }
    }
}
