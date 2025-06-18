using Domain.Interfaces;
using Domain.Enums;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Data.Common;

namespace Domain.Services{
    public class NotificationEvaluator
    {
        private readonly UserConfiguration _userConfiguration;

        public NotificationEvaluator(UserConfiguration userConfiguration)
        {
            _userConfiguration = userConfiguration;
        }
        public void EvaluateNotifications()
        {
            // Example logic to evaluate notifications based on enabled parameters
            foreach (var enabledParameter in _userConfiguration.EnabledParameters)
            {
                // Here you would typically check the current weather data
                var currentWeather = GetCurrentWeather(enabledParameter.ParameterType);

                // Evaluate based on the current weather and the enabled parameter
                if (ShouldNotify(currentWeather, enabledParameter))
                {
                    SendNotification(enabledParameter);
                }
            }
        }

        private WeatherParameter GetCurrentWeather(WeatherParameterType parameterType)
        {
            // Placeholder for actual weather data retrieval logic
            // This should return the current weather data based on the parameter type
            // For example, let's assume we have a method that fetches the current weather
            return new WeatherParameter
            {
                // Populate with actual weather data
                Id = Guid.Parse("0"),
                Type = WeatherParameterType.Temperature,
                Value = 33f
            };
        }

        private bool ShouldNotify(WeatherParameter currentWeather, EnabledParameter enabledParameter)
        {
            // Implement your notification logic based on current weather and enabled parameter
            switch (enabledParameter.ParameterType)
            {
                case WeatherParameterType.Temperature:
                    return currentWeather.Value > 30; // Example condition

                // Add cases for other weather parameters as needed

                default:
                    return false;
            }
        }

        private void SendNotification(EnabledParameter enabledParameter)
        {
            // Implement your notification sending logic here
            Console.WriteLine($"Notification: {enabledParameter.ParameterType} is triggered.");
        }
    }

}