using Newtonsoft.Json;

namespace OpenWeatherMapApiWrapper
{
    public class Clouds
    {
        [JsonProperty("all")]
        public int All { get; set; }
    }
}