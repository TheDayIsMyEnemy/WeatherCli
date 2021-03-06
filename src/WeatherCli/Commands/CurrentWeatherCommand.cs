﻿using McMaster.Extensions.CommandLineUtils;
using OpenWeatherMapApiWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherCli.Options;
using static WeatherCli.Constants;

namespace WeatherCli.Commands
{
    [Command(
        Name = "current",
        Description = "Get current weather data by city name")]
    public class CurrentWeatherCommand : Command
    {
        private readonly IWritableOptions<CurrentWeatherCommandOptions> _options;

        public CurrentWeatherCommand(
            IOpenWeatherMapApiClient openWeatherMapApiClient,
            IConsole console,
            IWritableOptions<CurrentWeatherCommandOptions> options)
            : base(openWeatherMapApiClient, console)
        {
            _options = options;
        }

        public string CityName { get; set; }

        [Argument(0,
            "CityName",
            "You can call by city name or city name, state code and country code")]
        public IEnumerable<string> CityNameArgs { get; set; }

        [Option("-d|--default", "Sets a default city name", CommandOptionType.NoValue)]
        public bool Default { get; set; }

        [Option("-k|--apikey", "Sets an OpenWeatherMap API key", CommandOptionType.SingleValue)]
        public string ApiKey { get; set; }

        public override async Task<int> OnExecuteAsync()
        {
            if (!SetApiKey())
            {
                Console.WriteLine("API key is required");
                return (int)CommandOutcome.Error;
            }

            if (!SetCityName())
            {
                Console.WriteLine("City name is required");
                return (int)CommandOutcome.Error;
            }

            if (Default)
            {
                _options.Update(o => o.DefaultCityName = CityName);
                Console.WriteLine($"The default city name has been set to '{CityName}'");
            }

            var (weatherData, statusCode) = await OpenWeatherMapApiClient
                .GetCurrentWeatherByCityNameAsync(CityName);
            if (weatherData == null)
            {
                Console.WriteLine($"Unable to fetch weather data. Status code: {(int)statusCode} {statusCode}");
                return (int)CommandOutcome.Error;
            }

            var weatherStr = BuildWeatherResult(weatherData);
            Console.WriteLine(weatherStr);

            return (int)CommandOutcome.Success;
        }

        private bool SetApiKey()
        {
            if (string.IsNullOrWhiteSpace(ApiKey))
                ApiKey = Environment.GetEnvironmentVariable(ApiKeyEnvironmentVariable);
            if (string.IsNullOrWhiteSpace(ApiKey) && Environment.OSVersion.Platform == PlatformID.Win32NT)
                ApiKey = Environment.GetEnvironmentVariable(ApiKeyEnvironmentVariable, EnvironmentVariableTarget.User);

            try
            {
                OpenWeatherMapApiClient.ApiKey = ApiKey;
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private bool SetCityName()
        {
            CityName = string.Join(" ", CityNameArgs ?? Enumerable.Empty<string>());
            if (string.IsNullOrWhiteSpace(CityName))
            {
                CityName = _options.Value?.DefaultCityName;
            }
            return !string.IsNullOrWhiteSpace(CityName);
        }

        private string BuildWeatherResult(CurrentWeatherData weatherData)
        {
            var sb = new StringBuilder();

            var weather = weatherData
                .Weather
                .FirstOrDefault();

            sb.AppendLine(weatherData.Name);
            sb.AppendLine($"{weather?.Main} {Math.Round(weatherData.Main.Temp)}{DegreeSign}");
            sb.AppendLine($"Feels like {Math.Round(weatherData.Main.FeelsLike, 0)}{DegreeSign}." +
                $" {weather?.Description}");
            sb.AppendLine($"Wind {weatherData.Wind.Speed:f1}m/s" +
                $" {Utils.ConvertDegreesToWindDirection(weatherData.Wind.Deg)}," +
                $" {weatherData.Main.Pressure}hPa, Humidity: {weatherData.Main.Humidity}%");
            sb.AppendLine($"Visibility: {(weatherData.Visibility / 1000.0):f1}km");

            return sb.ToString();
        }
    }
}
