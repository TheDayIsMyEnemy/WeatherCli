using McMaster.Extensions.CommandLineUtils;
using OpenWeatherMapApiWrapper;
using System;
using System.Net;
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

        [Argument(0,
            "CityName",
            "You can call by city name or city name, state code and country code")]
        public string CityName { get; set; }

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

            var (currentWeatherData, statusCode) = await OpenWeatherMapApiClient.GetCurrentWeatherByCityNameAsync(CityName);

            if (currentWeatherData == null)
            {
                Console.WriteLine($"Status code: {statusCode}");
                return (int)CommandOutcome.Error;
            }

            return (int)CommandOutcome.Success;
        }

        private bool SetApiKey()
        {
            ApiKey = string.IsNullOrWhiteSpace(ApiKey) ?
                Environment.GetEnvironmentVariable(Constants.ApiKeyEnvironmentVariable, EnvironmentVariableTarget.User)
                : ApiKey;

            try
            {
                OpenWeatherMapApiClient.ApiKey = ApiKey;
            }
            catch (Exception)
            {
                return false;
            }

            return !String.IsNullOrWhiteSpace(ApiKey);
        }

        //private string GetPropertyInformation(Type type, object obj)
        //{
        //    var propertyInfo = type
        //       .GetProperties()
        //       .Select(prop =>
        //       {
        //           var propValue = prop.GetValue(obj);
        //           return propValue.GetType() != typeof(Condition)
        //            ? $"{prop.Name}: {propValue}"
        //            : string.Empty;
        //       })
        //       .Where(i => !string.IsNullOrWhiteSpace(i))
        //       .ToList();

        //    var sb = new StringBuilder()
        //        .AppendLine(CenterText(type.Name));

        //    string currentLine = string.Empty;

        //    for (int i = 0; i < propertyInfo.Count; i++)
        //    {
        //        currentLine += $"{propertyInfo[i]} | ";
        //        if ((i + 1) % 3 == 0 || i == propertyInfo.Count - 1)
        //        {
        //            sb.AppendLine(CenterText(currentLine.TrimEnd(new char[] { ' ', '|' })));
        //            currentLine = string.Empty;
        //        }
        //    }

        //    return sb.ToString();
        //}

        private static string CenterText(string text)
        {
            int windowWidth = System.Console.WindowWidth;

            if (windowWidth > text.Length)
            {
                string emptySpaces = new string(' ', (windowWidth - text.Length) / 2);
                return emptySpaces + text;
            }

            return text;
        }
    }
}
