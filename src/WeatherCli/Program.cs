using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using OpenWeatherMapApiWrapper;
using WeatherCli.Options;

namespace WeatherCli
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();

            services
                .AddSingleton<IOpenWeatherMapApiClient, OpenWeatherMapApiClient>()
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
    }
}
