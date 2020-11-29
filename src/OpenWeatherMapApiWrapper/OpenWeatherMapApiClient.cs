using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace OpenWeatherMapApiWrapper
{
    public class OpenWeatherMapApiClient : IOpenWeatherMapApiClient
    {
        private readonly HttpClient _httpClient;
        private const string BaseAddress = "http://api.openweathermap.org/data/2.5/";
        private string _apiKey;

        public OpenWeatherMapApiClient()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(BaseAddress);
        }

        public OpenWeatherMapApiClient(string apiKey)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(BaseAddress);
            _apiKey = apiKey;
        }

        public string ApiKey 
        {
            get
            {
                return _apiKey;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(nameof(value), $"{nameof(ApiKey)} cannot be null or empty");
                }

                _apiKey = value;
            }
        }

        public async Task<(CurrentWeatherData, HttpStatusCode)> GetCurrentWeatherByCityNameAsync(string cityName)
        {
            CurrentWeatherData currentWeatherData = null;
            string requestUrl = $"weather?q={cityName}&appid={_apiKey}";

            var response = await _httpClient.GetAsync(requestUrl);
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                currentWeatherData = JsonConvert.DeserializeObject<CurrentWeatherData>(responseBody);
            }

            return (currentWeatherData, response.StatusCode);
        }
    }
}
