using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using WeatherApiWrapper.Core;
using WeatherApiWrapper.Core.Interfaces;
using WeatherCli.Options;

namespace WeatherCli
{
    class Program
    {
        static void Main(string[] args)
        {
            var apiKey = "4da082a12d964a8cb0f03932202110";
            var weatherApiClient = new WeatherApiClient(apiKey);

            var services = new ServiceCollection();

            services
                .AddSingleton<IWeatherApiClient, WeatherApiClient>(sp => weatherApiClient)
                .AddSingleton<IConsole>(PhysicalConsole.Singleton)
                .AddSingleton<IWritableOptions<CurrentWeatherCommandOptions>,
                    WritableOptions<CurrentWeatherCommandOptions>>();

            var serviceProvider = services.BuildServiceProvider();

            var app = new CommandLineApplication<App>();
            app.Conventions
                .UseDefaultConventions()
                .UseConstructorInjection(serviceProvider);

            app.Execute(args);
        }

        private static void ConfigureServices(IServiceCollection services)
        {

        }
    }
}
