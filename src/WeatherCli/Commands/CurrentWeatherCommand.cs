using McMaster.Extensions.CommandLineUtils;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using WeatherApiWrapper.Core.Interfaces;
using WeatherApiWrapper.Core.Models;

namespace WeatherCli.Commands
{
    [Command(Name = "current", Description = "Get current weather information")]
    [HelpOption()]
    public class CurrentWeatherCommand : Command
    {
        public CurrentWeatherCommand(
            IWeatherApiClient weatherApiClient,
            IConsole console)
            : base(weatherApiClient, console)
        {

        }

        [Argument(0, "SearchQuery",
            "Pass US Zipcode, UK Postcode, Canada Postalcode, IP address, Latitude/Longitude (decimal degree) or city name.")]
        [Required]
        public string CurrentWeatherSearchQuery { get; set; }

        public override void OnExecute()
        {
            RealtimeWeatherResponse weather = WeatherApiClient
                .GetRealtimeWeatherAsync(CurrentWeatherSearchQuery)
                .Result;

            var locationString = GetPropertyInformation(
                weather.Location.GetType(),
                weather.Location);
            var conditionString = GetPropertyInformation(
                weather.Current.Condition.GetType(),
                weather.Current.Condition);
            var currentWeatherString = GetPropertyInformation(
                weather.Current.GetType(),
                weather.Current);

            Console.WriteLine(locationString);
            Console.WriteLine(conditionString);
            Console.WriteLine(currentWeatherString);
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
            var windowWidth = System.Console.WindowWidth;

            if (windowWidth > text.Length)
            {
                string emptySpaces = new string(' ', (windowWidth - text.Length) / 2);
                return emptySpaces + text;
            }

            return text;
        }
    }
}
