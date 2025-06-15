using Domain.Interfaces;
using Domain.Enums;
using Domain.Entities;

namespace Domain.Services
{
    public class NotificationEvaluator
    {
        private readonly INotificationService _notificationService;

        public NotificationEvaluator(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public void EvaluateAndNotify(UserConfiguration config, List<WeatherForecast> forecasts)
        {
            if (!config.NotificationSetting.EnableNotifications)
                return;

            foreach (var forecast in forecasts)
            {
                if (config.NotificationSetting.NotifyHighPressure &&
                    forecast.Parameters.TryGetValue(WeatherParameterType.Pressure, out var pressure) &&
                    pressure > 1020)
                {
                    _notificationService.SendNotification($"High pressure alert for {forecast.City.Name} on {forecast.Date.ToShortDateString()}");
                }

                if (config.NotificationSetting.NotifyHighRainProbability &&
                    forecast.Parameters.TryGetValue(WeatherParameterType.RainProbability, out var rainProb) &&
                    rainProb > 80)
                {
                    _notificationService.SendNotification($"High chance of rain in {forecast.City.Name} on {forecast.Date.ToShortDateString()}");
                }
            }
        }
    }
}