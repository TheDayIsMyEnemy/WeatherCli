using McMaster.Extensions.CommandLineUtils;
using OpenWeatherMapApiWrapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherCli.Options;

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

        // This is in case the city name has more than one word
        [Argument(0,
            "CityName",
            "You can call by city name or city name, state code and country code")]
        public IEnumerable<string> CityNameArgs { get; set; }      

        [Option("-d|--default", "Sets a default city name", CommandOptionType.NoValue)]
        public bool Default { get; set; }

        [Option("-k|--apikey", "Sets an OpenWeatherMap API key", CommandOptionType.SingleValue)]
        public string ApiKey { get; set; }

        public override async Task<int> OnExecute()
        {
            if (!SetApiKey())
            {
                Console.WriteLine("API key is required");
                return (int)CommandOutcome.Error;
            }

            CityName = string.Join(" ", CityNameArgs);
            if (string.IsNullOrWhiteSpace(CityName))
            {
                CityName = _options.Value?.DefaultCityName;
                if (string.IsNullOrWhiteSpace(CityName))
                {
                    Console.WriteLine("City name is required");
                    return (int)CommandOutcome.Error;
                }
            }

            if (Default)
            {
                _options.Update(o => o.DefaultCityName = CityName);
                Console.WriteLine($"The default search city name has been set to '{CityName}'");
            }

            var (weatherData, statusCode) = await OpenWeatherMapApiClient.GetCurrentWeatherByCityNameAsync(CityName);
            if (weatherData == null)
            {
                Console.WriteLine($"Status code: {statusCode}");
                return (int)CommandOutcome.Error;
            }

            Console.WriteLine($"{weatherData.Weather[0].Main} {weatherData.Main.Temp}\u00B0C");
            Console.WriteLine($"Feels like {weatherData.Main.FeelsLike}\u00B0C. {weatherData.Weather[0].Description}");
            Console.WriteLine($"Wind {weatherData.Wind.Speed}m/s {Utils.ConvertDegreesToWindDirection(weatherData.Wind.Deg)}, " +
                              $"{weatherData.Main.Pressure}hPa, Humidity: {weatherData.Main.Humidity}%");
            Console.WriteLine($"Visibility: {weatherData.Visibility / 1000}km");

            return (int)CommandOutcome.Success;
        }

        private bool SetApiKey()
        {
            if (string.IsNullOrWhiteSpace(ApiKey))
                ApiKey = Environment.GetEnvironmentVariable(Constants.ApiKeyEnvironmentVariable);
            if (string.IsNullOrWhiteSpace(ApiKey) && Environment.OSVersion.Platform == PlatformID.Win32NT)
                ApiKey = Environment.GetEnvironmentVariable(Constants.ApiKeyEnvironmentVariable, EnvironmentVariableTarget.User);

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
    }
}
