using WeatherApiWrapper.Core;

namespace WeatherCli.Commands
{
    public abstract class Command : ICommand
    {
        public Command(WeatherApiClient weatherApiClient)
        {
            WeatherApiClient = weatherApiClient;
        }

        protected WeatherApiClient WeatherApiClient { get; }

        public string Name { get; }

        public abstract void Execute(string[] args);
    }
}
