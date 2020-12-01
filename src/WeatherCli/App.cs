using McMaster.Extensions.CommandLineUtils;
using WeatherCli.Commands;

namespace WeatherCli
{
    [Command(
        Name = "weather",
        Description = "Weather CLI Tool",
        ExtendedHelpText = Constants.ExtendedHelpText)]
    [HelpOption("-h|--help")]
    [Subcommand(typeof(CurrentWeatherCommand))]
    [Subcommand(typeof(VersionCommand))]
    public class App
    {
        public int OnExecute(CommandLineApplication app)
        {
            app.ShowHelp();
            return (int)CommandOutcome.Success;
        }
    }
}
