﻿using McMaster.Extensions.CommandLineUtils;
using OpenWeatherMapApiWrapper;

namespace WeatherCli.Commands
{
    public abstract class Command
    {
        public Command(
            IOpenWeatherMapApiClient openWeatherMapApiClient,
            IConsole console)
        {
            OpenWeatherMapApiClient = openWeatherMapApiClient;
            Console = console;
        }

        protected IOpenWeatherMapApiClient OpenWeatherMapApiClient { get; }

        protected IConsole Console { get; }

        public abstract void OnExecute();
    }
}
