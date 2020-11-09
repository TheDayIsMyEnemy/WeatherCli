using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using WeatherApiWrapper.Core;
using WeatherApiWrapper.Core.Interfaces;

namespace WeatherCli
{
    class Program
    {
        static void Main(string[] args)
        {
            var apiKey = "";
            var weatherApiClient = new WeatherApiClient(apiKey);

            var services = new ServiceCollection()
                .AddSingleton<IWeatherApiClient, WeatherApiClient>(sp => weatherApiClient)
                .AddSingleton<IConsole>(PhysicalConsole.Singleton)
                .BuildServiceProvider();

            var app = new CommandLineApplication<App>();
            app.Conventions
                .UseDefaultConventions()
                .UseConstructorInjection(services);

            app.Execute(args);
        }
    }
}
