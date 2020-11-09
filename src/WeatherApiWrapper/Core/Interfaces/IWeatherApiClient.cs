using System.Threading.Tasks;
using WeatherApiWrapper.Core.Models;

namespace WeatherApiWrapper.Core.Interfaces
{
    public interface IWeatherApiClient
    {
        Task<RealtimeWeatherResponse> GetRealtimeWeatherAsync(string q);
    }
}
