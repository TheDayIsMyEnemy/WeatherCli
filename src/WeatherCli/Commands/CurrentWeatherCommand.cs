using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeatherApiWrapper.Core;
using WeatherApiWrapper.Core.Models;

namespace WeatherCli.Commands
{
    public class CurrentWeatherCommand : Command, ICommand
    {
        public CurrentWeatherCommand(WeatherApiClient weatherApiClient)
            : base(weatherApiClient)
        {

        }

        public override void Execute(string[] args)
        {
            RealtimeWeatherResponse weather = WeatherApiClient
                .GetRealtimeWeatherAsync(args[0])
                .Result;

            var locationString = GetPropertyInfoAsString(
                weather.Location.GetType(),
                weather.Location);
            var conditionString = GetPropertyInfoAsString(
                weather.Current.Condition.GetType(),
                weather.Current.Condition);
            var currentWeatherString = GetPropertyInfoAsString(
                weather.Current.GetType(),
                weather.Current);

            Console.WriteLine(locationString);
            Console.WriteLine(conditionString);
            Console.WriteLine(currentWeatherString);
        }

        private string GetPropertyInfoAsString(Type type, object obj)
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
               .Where(i => i != string.Empty)
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
            if (Console.WindowWidth > text.Length)
            {
                string emptySpaces = new string(' ', (Console.WindowWidth - text.Length) / 2);
                return emptySpaces + text;
            }

            return text;
        }
    }
}
