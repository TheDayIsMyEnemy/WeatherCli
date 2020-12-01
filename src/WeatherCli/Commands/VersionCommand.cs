using McMaster.Extensions.CommandLineUtils;
using System.Reflection;

namespace WeatherCli.Commands
{
    [Command(Name = "version", Description = "Get current version of Weather CLI")]
    public class VersionCommand
    {
        private readonly IConsole _console;

        public VersionCommand(IConsole console)
        {
            _console = console;
        }

        public void OnExecute()
        {
            string version = Assembly.GetEntryAssembly()
                                    .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                                    .InformationalVersion
                                    .ToString();

            _console.WriteLine(version);
        }
    }
}
