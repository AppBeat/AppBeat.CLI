using System;

namespace AppBeat.CLI.Command
{
    public class BaseCommand
    {
        protected static string GetArg(string[] args, int i)
        {
            //first argument is command name, but we will skip it because we already know which command to run
            i++;

            if (args == null || args.Length <= i)
            {
                return null;
            }

            return args[i];
        }

        protected static void WriteLine(string message = null)
        {
            Console.WriteLine(message);
        }

        protected bool EnsureSecretIsAvailable()
        {
            bool isSecretAvailable = !string.IsNullOrWhiteSpace(API.BaseAPI.Secret);

            if (!isSecretAvailable)
            {
                WriteLine("AppBeat API can be used only if Secret Access Key is set.");
                WriteLine("If you don't have your private Secret Access Key, you can get one for free if you create new account on https://appbeat.io. Once you are registered login to https://my.appbeat.io/manage, click Account / API Access and generate key (important: free plans have keys bound to IP address - you must set it to IP address from which API is called).");
                WriteLine("Once you have secret key you will have to save it to text file called secret.key located in:");
                WriteLine(@"  -> same directory as AppBeat.CLI.dll or,");
                WriteLine(@"  -> %HOME%/AppBeat on Linux or %LOCALAPPDATA%\AppBeat on Windows");
            }

            return isSecretAvailable;
        }
    }
}
