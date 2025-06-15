//using Domain.Entities;
using Domain.Enums;
namespace Domain.Entities
{
    public class WeatherForecast
    {
        public Guid Id { get; set; }
        public City? City { get; set; }
        public DateTime Date { get; set; }
        public Dictionary<WeatherParameterType, double> Parameters { get; set; } = new();
    }
}