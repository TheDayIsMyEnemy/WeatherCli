using McMaster.Extensions.CommandLineUtils;
using WeatherApiWrapper.Core.Interfaces;

namespace WeatherCli.Commands
{
    public abstract class Command
    {
        public Command(
            IWeatherApiClient weatherApiClient,
            IConsole console)
        {
            WeatherApiClient = weatherApiClient;
            Console = console;
        }

        protected IWeatherApiClient WeatherApiClient { get; }

        protected IConsole Console { get; }

        public abstract void OnExecute();
    }
}
