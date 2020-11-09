using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Text;
using WeatherCli.Commands;

namespace WeatherCli
{
    [Command(Name = "weather", Description = "Weather CLI Tool")]
    [HelpOption("-h|--help")]
    [Subcommand(typeof(CurrentWeatherCommand))]
    [Subcommand(typeof(VersionCommand))]
    public class App
    {
        
    }
}
