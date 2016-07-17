using AppBeat.CLI.Types;

namespace AppBeat.CLI.Command
{
    public interface ICommand
    {
        ExitCode Run(string[] args);
    
        string Description
        {
            get;
        }
    }
}
