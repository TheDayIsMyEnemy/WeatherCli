using System.Net.Http;

namespace OpenWeatherMapApiWrapper.Endpoints
{
    public abstract class EndpointBase
    {
        public EndpointBase(HttpClient httpClient, string apiKey)
        {
            HttpClient = httpClient;
            ApiKey = apiKey;
        }

        protected string ApiKey { get; private set; }

        protected HttpClient HttpClient { get; private set; }
    }
}
