using OpenWeatherMapApiWrapper.Endpoints;
using System;
using System.Net.Http;

namespace OpenWeatherMapApiWrapper
{
    public class OpenWeatherMapClient
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private const string UriString = "api.openweathermap.org/data/2.5/";
        private readonly string _apiKey;

        public OpenWeatherMapClient(string apiKey)
        {
            _apiKey = apiKey;
            _httpClient.BaseAddress = new Uri(UriString);
            InitializeEndpoints();
        }

        public CurrentWeather CurrentWeather { get; private set; }

        private void InitializeEndpoints()
        {
            CurrentWeather = new CurrentWeather(_httpClient, _apiKey);
        }
    }
}
