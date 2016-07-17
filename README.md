# AppBeat.CLI (AppBeat Command Line Interface)

This is Command Line Interface for AppBeat, website monitoring tool (https://appbeat.io/).
This tool can currently:
* Open URL on remote web browser location (Europe, NorthAmerica, Asia) and return page speed statistics.

Application is free to use and you can modify it as you wish. You will need AppBeat Secret Access Key before you can use it.
You can get new key for free if you create account on https://appbeat.io.
Once you are registered login to https://my.appbeat.io/manage, click Account / API Access and generate new key
(important: free plans have keys bound to IP address - you must set it to IP address from which API is called).

Once you have secret key you will have to save it to text file called **secret.key** located in:
* same directory as AppBeat.CLI.dll or,
* %HOME%/AppBeat on Linux / Unix or %LOCALAPPDATA%\AppBeat on Windows

You can run application on Windows, Linux and Mac, but you have to install .NET Core runtime first. You can get it for free from https://www.microsoft.com/net/core

Usage example:
* run website speed test for https://appbeat.io from North America
   * `dotnet AppBeat.CLI.dll page-speed NorthAmerica https://appbeat.io`

* run website speed test for https://appbeat.io from Europe
  * `dotnet AppBeat.CLI.dll page-speed Europe https://appbeat.io`

* run website speed test for https://appbeat.io from Asia
  * `dotnet AppBeat.CLI.dll page-speed Asia https://appbeat.io`

Please tell us which new test locations should we add in future and which new features would you like to see.
We would also appreciate bug reports or any other problems. You can contact us on https://appbeat.io/contact

