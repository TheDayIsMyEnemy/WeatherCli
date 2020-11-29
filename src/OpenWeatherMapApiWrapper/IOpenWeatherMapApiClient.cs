using System.Threading.Tasks;

namespace OpenWeatherMapApiWrapper
{
    public interface IOpenWeatherMapApiClient
    {
        Task<CurrentWeatherData> GetCurrentWeatherByCityAsync(string cityName);
    }
}
