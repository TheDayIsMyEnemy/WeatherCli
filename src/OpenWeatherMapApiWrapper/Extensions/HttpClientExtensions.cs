using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace OpenWeatherMapApiWrapper.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<(T, HttpStatusCode)> GetAsync<T>(
            this HttpClient httpClient,
            string requestUri)
            where T : class, new()
        {
            T obj = new T();
            var response = await httpClient.GetAsync(requestUri);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                obj = JsonConvert.DeserializeObject<T>(responseBody);
            }
            return (obj, response.StatusCode);
        }
    }
}
