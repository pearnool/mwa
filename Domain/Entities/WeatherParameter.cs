using Domain.Enums;
namespace Domain.Entities
{
    public class WeatherParameter
    {
        public Guid Id { get; set; }
        public WeatherParameterType Type { get; set; }
        public double Value { get; set; }
    }
    
}