using System;
using Domain.Enums;
namespace Domain.Entities
{
    public class EnabledParameter
    {
        public Guid Id { get; set; }
        public WeatherParameterType ParameterType { get; set; }
        public Guid UserConfigurationId { get; set; } // Foreign key to UserConfiguration
    }
}