using Newtonsoft.Json;

namespace WeatherApiWrapper.Core.Models
{
    public class Condition
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("code")]
        public int Code { get; set; }
    }
}
