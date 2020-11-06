using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherApiWrapper.Core.Interfaces;
using WeatherApiWrapper.Core.Models;

namespace WeatherApiWrapper.Core
{
    public class WeatherApiClient : IWeatherApiClient
    {
        private static readonly HttpClient HttpClient = new HttpClient();
        private const string  Protocol = "https";
        private const string Hostname = "api.weatherapi.com/v1";
        private const string Format = "json";

        private readonly string _apiKey;
        private readonly string _weatherApiUrl;

        public WeatherApiClient(string apiKey)
        {
            _apiKey = apiKey;
            _weatherApiUrl = $"{Protocol}://{Hostname}";
        }

        public async Task<RealtimeWeatherResponse> GetRealtimeWeatherAsync(string q)
        {
            string requestUrl = $"{_weatherApiUrl}/current.{Format}?key={_apiKey}&q={q}";

            try
            {
                var response = await HttpClient.GetAsync(requestUrl);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var currentWeather = JsonConvert
                    .DeserializeObject<RealtimeWeatherResponse>(responseBody);

                return currentWeather;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return null;
        }
    }
}
