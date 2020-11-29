using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using OpenWeatherMapApiWrapper;
using System.Threading.Tasks;
using WeatherCli.Options;

namespace WeatherCli
{
    class Program
    {
        static async Task<int> Main(string[] args)
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

            return await app.ExecuteAsync(args);
        }
    }
}
