namespace MauiReactor.WeatherTwentyOne.Services;

public interface IWeatherService
{
    Task<IEnumerable<Location>> GetLocations(string query);
    Task<WeatherResponse> GetWeather(Coordinate location);
}
