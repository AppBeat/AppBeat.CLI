using AppBeat.CLI.Types;
using System;
using System.Threading;

namespace AppBeat.CLI.Command
{
    public class PageSpeed : BaseCommand, ICommand
    {
        public string Description
        {
            get
            {
                return "Open URL on remote web browser location and return page speed statistics.";
            }
        }

        public ExitCode Run(string[] args)
        {
            if (!EnsureSecretIsAvailable())
            {
                return ExitCode.SecretNotAvailable;
            }

            string strLocation = GetArg(args, 0);
            string url = GetArg(args, 1);

            if (string.IsNullOrWhiteSpace(strLocation) || string.IsNullOrWhiteSpace(url))
            {
                ShowUsage();
                return ExitCode.InvalidArgs;
            }

            //do some basic checks to not even call remote API
            url = url.Trim();
            if (!url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) &&
                !url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                url = $"http://{url}";
            }

            //is this valid URL?
            try
            {
                new Uri(url);
            }
            catch (Exception ex)
            {
                WriteLine($"URL '{url}' seems to be invalid: {ex.Message}");
                return ExitCode.InvalidArgs;
            }

            //is this valid location?
            Location location;
            if (!Enum.TryParse<Location>(strLocation, out location))
            {
                WriteLine($"Location '{strLocation}' seems to be invalid. Currently supported locations are:");
                var availableLocations = Enum.GetNames(typeof(Location));
                foreach (var availableLocation in availableLocations)
                {
                    WriteLine($"  {availableLocation}");
                }
                return ExitCode.InvalidArgs;
            }

            //we can now call AppBeat API
            //first submit new test
            var api = new API.PageSpeed();
            WriteLine($"Submitting remote web browser test in {location} for '{url}'...");
            var task = api.SubmitTestAsync(url, location);
            task.Wait(Constants.API_TIMEOUT);

            if (task?.Result?.Status != TestStatus.Submitted)
            {
                WriteLine($"ApiError: {task?.Result?.Error}");
                return ExitCode.ApiError;
            }

            var testId = task?.Result?.TestId;
            if (string.IsNullOrWhiteSpace(testId))
            {
                return ExitCode.ApiError;
            }

            WriteLine("Waiting for results...");

            //once we receive test id, periodically check until test is finished
            for (var i = 0; i < 25; i++)
            {
                Thread.Sleep(5000);

                try
                {
                    task = api.GetTestAsync(testId);
                    task.Wait(Constants.API_TIMEOUT);
                }
                catch
                {
                    //try other endpoints
                    continue;
                }

                if (task?.Result?.Status == TestStatus.Submitted || (task?.Result?.Status == TestStatus.Pending))
                {
                    //results not yet ready
                    continue;
                }

                if (task?.Result?.Status == TestStatus.Success)
                {
                    WriteLine($"LoadTime: {task?.Result?.LoadTime} milliseconds");
                    WriteLine($"TransferredSize: {task?.Result?.TransferredSize} B");
                    WriteLine($"DecompressedSize: {task?.Result?.DecompressedSize} B");
                    WriteLine($"Requests: {task?.Result?.Requests}");

                    if (task?.Result?.Resources != null)
                    {
                        foreach (var resource in task.Result.Resources)
                        {
                            WriteLine($"   -> status={resource.Status}, start={resource.Start}, end={resource.End}, content type={resource.ContentType}, url={resource.Url}");
                        }
                    }

                    return ExitCode.OK;
                }

                WriteLine($"Test failed with status {task?.Result?.Status}, error: {task?.Result?.Error}");
                return ExitCode.ApiError;
            }

            WriteLine("Test did not complete for too long, exiting...");
            return ExitCode.ApiError;
        }

        private void ShowUsage()
        {

        }
    }
}
