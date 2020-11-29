using System.Net;
using System.Threading.Tasks;

namespace OpenWeatherMapApiWrapper
{
    public interface IOpenWeatherMapApiClient
    {
        string ApiKey { get; set; }

        Task<(CurrentWeatherData, HttpStatusCode)> GetCurrentWeatherByCityNameAsync(string cityName);
    }
}
