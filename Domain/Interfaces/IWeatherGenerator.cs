using Domain.Entities;
namespace Domain.Interfaces
{
    public interface IWeatherGenerator
    {
        List<WeatherForecast> GenerateForecast(City city, int daysAhead);
    }
}
