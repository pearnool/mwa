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
            //logic to evaluate notifications based on enabled parameters
            foreach (var enabledParameter in _userConfiguration.EnabledParameters)
            {
                // check the current weather data
                var currentWeather = GetCurrentWeather(enabledParameter.ParameterType);

                //  evaluate based on the current weather and the enabled parameter
                if (ShouldNotify(currentWeather, enabledParameter))
                {
                    SendNotification(enabledParameter);
                }
            }
        }

        private WeatherParameter GetCurrentWeather(WeatherParameterType parameterType)
        {

            return new WeatherParameter
            {
                // ppopulate with actual weather data
                Id = Guid.Parse("0"),
                Type = WeatherParameterType.Temperature,
                Value = 33f
            };
        }

        private bool ShouldNotify(WeatherParameter currentWeather, EnabledParameter enabledParameter)
        {
            switch (enabledParameter.ParameterType)
            {
                case WeatherParameterType.Temperature:
                    return currentWeather.Value > 30; // Example condition

                // Add cases for other cases (empty for a while)

                default:
                    return false;
            }
        }

        private void SendNotification(EnabledParameter enabledParameter)
        {
            // implementation is gonna be here
            Console.WriteLine($"Notification: {enabledParameter.ParameterType} is triggered.");
        }
    }

}