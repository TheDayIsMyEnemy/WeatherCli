using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using WeatherApiWrapper.Core;
using WeatherCli.Commands;
using ICommand = WeatherCli.Commands.ICommand;

namespace WeatherCli
{
    public class CommandRunner
    {
        private readonly Dictionary<string, Func<string[], ICommand>> _commands;
        private const string apiKey = "4da082a12d964a8cb0f03932202110"; 
        private static readonly WeatherApiClient WeatherApiClient = new WeatherApiClient(apiKey);

        public CommandRunner()
        {
            _commands = new Dictionary<string, Func<string[], ICommand>>();
            _commands.Add("version", (args) => new VersionCommand());
            _commands.Add("current", (args) => new CurrentWeatherCommand(WeatherApiClient));
        }

        public void RunCommand(string[] args)
        {
            var commandName = args[0].ToLower();
            var commandArgs = args.Skip(1).ToArray();

            if (_commands.ContainsKey(commandName))
            {
                _commands[commandName]
                    .Invoke(commandArgs)
                    .Execute(commandArgs);
            }
        }
    }
}
