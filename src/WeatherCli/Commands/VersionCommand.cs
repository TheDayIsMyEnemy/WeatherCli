using System;
using System.Reflection;

namespace WeatherCli.Commands
{
    public class VersionCommand : ICommand
    {
        public string Name => "Version";

        public void Execute(string[] args)
        {
            string version = Assembly.GetEntryAssembly()
                                    .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                                    .InformationalVersion
                                    .ToString();

            Console.WriteLine(version);
        }
    }
}
