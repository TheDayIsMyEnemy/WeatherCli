using McMaster.Extensions.CommandLineUtils;
using WeatherCli.Commands;

namespace WeatherCli
{
    [Command(Name = "weather", Description = "Weather CLI Tool")]
    [HelpOption("-h|--help")]
    [Subcommand(typeof(WeatherApiCurrentWeatherCommand))]
    [Subcommand(typeof(VersionCommand))]
    public class App
    {
        
    }
}
