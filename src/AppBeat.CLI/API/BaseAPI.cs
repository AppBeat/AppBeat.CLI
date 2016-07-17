using System.IO;

namespace AppBeat.CLI.API
{
    public class BaseAPI
    {
        internal static string Secret
        {
            get
            {
                if (_secret == null)
                {
                    _secret = TryGetSecretFromAssemblyLocation();
                    if (_secret != null) return _secret;

                    _secret = TryGetSecretFromHomeDir();
                    if (_secret != null) return _secret;

                    //could not get Appbeat API secret
                    _secret = string.Empty;
                }

                return _secret;
            }
        }

        /// <summary>
        /// Returns API endpoint. Current implementation tries to use all available endpoints each time this property is accessed.
        /// </summary>
        protected static string Endpoint
        {
            get
            {
                if (_currentEndpoint >= _availableEndpoints.Length)
                {
                    _currentEndpoint = 0;
                }

                return _availableEndpoints[_currentEndpoint++];
            }
        }
        private static int _currentEndpoint = 0;

        private static string[] _availableEndpoints = new string[] {
            "https://web1.appbeat.io/API/v1",
            "https://web2.appbeat.io/API/v1"
        };

        /// <summary>
        /// Checks for secret.key file in same directory as main application.
        /// </summary>
        private static string TryGetSecretFromAssemblyLocation()
        {
            try
            {
                var dir = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
                var path = Path.Combine(dir, "secret.key");
                if (File.Exists(path))
                {
                    return File.ReadAllText(path).Trim();
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Checks for secret.key file in %HOME%\AppBeat directory.
        /// </summary>
        private static string TryGetSecretFromHomeDir()
        {
            try
            {
                string home;
                if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
                {
                    home = System.Environment.GetEnvironmentVariable("LOCALAPPDATA");
                }
                else
                {
                    home = System.Environment.GetEnvironmentVariable("HOME");
                }

                if (home == null)
                {
                    return null;
                }

                var path = Path.Combine(home, "AppBeat", "secret.key");
                if (File.Exists(path))
                {
                    return File.ReadAllText(path).Trim();
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        private static string _secret;
    }
}
