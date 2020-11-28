using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace OpenWeatherMapApiWrapper.Endpoints
{
    public class CurrentWeather : EndpointBase
    {
        public CurrentWeather(HttpClient httpClient, string apiKey)
            : base(httpClient, apiKey)
        {
        }

        public async Task<CurrentWeatherData> GetCurrentWeatherByCity(string cityName)
        {
            CurrentWeatherData currentWeatherData = null;
            string requestUrl = $"weather?q={cityName}&appid={ApiKey}";

            var response = await HttpClient.GetAsync(requestUrl);
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                currentWeatherData = JsonConvert.DeserializeObject<CurrentWeatherData>(responseBody);
            }

            return currentWeatherData;
        }
    }
}
