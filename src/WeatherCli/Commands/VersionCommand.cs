using System;
using System.Reflection;
using McMaster.Extensions.CommandLineUtils;

namespace WeatherCli.Commands
{
    [Command(Name = "version", Description = "Get current version of Weather CLI.")]
    public class VersionCommand
    {
        public void OnExecute()
        {
            string version = Assembly.GetEntryAssembly()
                                    .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                                    .InformationalVersion
                                    .ToString();

            Console.WriteLine(version);
        }
    }
}
