namespace AppBeat.CLI.Types
{
    public enum ExitCode : int
    {
        OK = 0,
        InvalidArgs = 1,
        InvalidCommand = 2,
        SecretNotAvailable = 3,
        Exception = 100,
        ApiError = 200
    }
}
