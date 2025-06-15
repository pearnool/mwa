using Domain.Interfaces;
using Domain.Entities;
namespace Domain.Services
{
    public class WeatherService
    {
        private readonly IWeatherGenerator _weatherGenerator;

        public WeatherService(IWeatherGenerator weatherGenerator)
        {
            _weatherGenerator = weatherGenerator;
        }

        public List<WeatherForecast> GetForecast(UserConfiguration config)
        {
            return _weatherGenerator.GenerateForecast(config.SelectedCity, config.ForecastDaysAhead);
        }
    }
}