using AppBeat.CLI.Command;
using AppBeat.CLI.Types;
using System;

namespace AppBeat.CLI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                if (args == null || args.Length == 0)
                {
                    RunHelp();
                    ReturnExitCode(ExitCode.InvalidArgs);
                    return;
                }

                var cmd = Factory.GetCommandByName(args[0]);
                if (cmd == null)
                {
                    RunHelp();
                    ReturnExitCode(ExitCode.InvalidCommand);
                    return;
                }

                ReturnExitCode(cmd.Run(args));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Environment.Exit((int)ExitCode.Exception);
            }
        }

        private static void RunHelp()
        {
            Factory.GetCommandByName(Constants.Command.HELP)?.Run(null);
        }

        private static void ReturnExitCode(ExitCode exitCode)
        {
#if DEBUG
            Console.WriteLine("Press ENTER to exit...");
            Console.ReadLine();
#endif

            Environment.Exit((int)exitCode);
        }
    }
}
