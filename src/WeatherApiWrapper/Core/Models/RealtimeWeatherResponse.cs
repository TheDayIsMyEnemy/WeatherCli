using Newtonsoft.Json;

namespace WeatherApiWrapper.Core.Models
{
    public class RealtimeWeatherResponse
    {
        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("current")]
        public Current Current { get; set; }
    }
}
