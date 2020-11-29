using McMaster.Extensions.CommandLineUtils;
using OpenWeatherMapApiWrapper;
using System;
using System.Linq;
using System.Text;
using WeatherApiWrapper.Core.Models;
using WeatherCli.Options;

namespace WeatherCli.Commands
{
    [Command(Name = "current", Description = "Get current weather information.")]
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

        [Argument(0, "SearchQuery",
            "You can call by city name or city name, state code and country code. Please note that searching by states available only for the USA locations")]
        public string SearchQuery { get; set; }

        [Option("-d|--default", "Add a default search query.", CommandOptionType.NoValue)]
        public bool Default { get; set; }

        public override void OnExecute()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                string defaultSearchQuery = _options.Value?.DefaultSearchQuery;
                if (string.IsNullOrWhiteSpace(defaultSearchQuery))
                {
                    Console.WriteLine("Search query is required.");
                    return;
                }

                SearchQuery = defaultSearchQuery;
            }


            //RealtimeWeatherResponse weather = WeatherApiClient
            //    .GetRealtimeWeatherAsync(SearchQuery)
            //    .Result;

            //var locationString = GetPropertyInformation(
            //    weather.Location.GetType(),
            //    weather.Location);
            //var conditionString = GetPropertyInformation(
            //    weather.Current.Condition.GetType(),
            //    weather.Current.Condition);
            //var currentWeatherString = GetPropertyInformation(
            //    weather.Current.GetType(),
            //    weather.Current);

            if (Default)
            {
                _options.Update(o => o.DefaultSearchQuery = SearchQuery);
                Console.WriteLine($"The default search query has been set to '{SearchQuery}'");
            }

            //Console.WriteLine(locationString);
            //Console.WriteLine(conditionString);
            //Console.WriteLine(currentWeatherString);
        }

        private string GetPropertyInformation(Type type, object obj)
        {
            var propertyInfo = type
               .GetProperties()
               .Select(prop =>
               {
                   var propValue = prop.GetValue(obj);
                   return propValue.GetType() != typeof(Condition)
                    ? $"{prop.Name}: {propValue}"
                    : string.Empty;
               })
               .Where(i => !string.IsNullOrWhiteSpace(i))
               .ToList();

            var sb = new StringBuilder()
                .AppendLine(CenterText(type.Name));

            string currentLine = string.Empty;

            for (int i = 0; i < propertyInfo.Count; i++)
            {
                currentLine += $"{propertyInfo[i]} | ";
                if ((i + 1) % 3 == 0 || i == propertyInfo.Count - 1)
                {
                    sb.AppendLine(CenterText(currentLine.TrimEnd(new char[] { ' ', '|' })));
                    currentLine = string.Empty;
                }
            }

            return sb.ToString();
        }

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
