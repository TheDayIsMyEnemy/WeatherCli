namespace WeatherCli
{
    public class Constants
    {
        public const string ApiKeyEnvironmentVariable = "OPENWEATHERMAP_APIKEY";

        public const string ExtendedHelpText =
            "Get your API key at ( https://openweathermap.org/appid ) to use this tool. " +
            "The API key must be provided either as an option to a command, or by setting the OPENWEATHERMAP_APIKEY environment variable.";

        public const string DegreeSign = "\u00B0C";
    }
}
