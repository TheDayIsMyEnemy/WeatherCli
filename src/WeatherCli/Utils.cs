namespace WeatherCli
{
    public static class Utils
    {
        public static string ConvertDegreesToWindDirection(double degrees)
        {
            string[] directions = new string[]
            {
                "N","NNE", "NE", "ENE", "E", "ESE", "SE", "SSE",
                "S","SSW", "SW", "WSW", "W", "WNW", "NW","NNW"
            };

            int index = (int)((degrees + 11.25) / 22.5);
            return directions[index % 16];
        }

        public static string CenterText(string text)
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
