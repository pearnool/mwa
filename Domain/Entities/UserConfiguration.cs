using Domain.Enums;
using Domain.Entities;
namespace Domain.Entities
{
    public class UserConfiguration
    {
        public Guid Id { get; set; }
        public City? SelectedCity { get; set; }
        public List<EnabledParameter> EnabledParameters { get; set; } = new(); // Change here
        public TemperatureUnit TemperatureUnit { get; set; }
        public int ForecastDaysAhead { get; set; } // 1, 3, or 7
        public NotificationSetting? NotificationSetting { get; set; }
    }

}
