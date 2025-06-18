using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Npgsql;
using Domain.Entities;
public class Program
{
    static void Main(string[] args)
    {
        // Path to your JSON file
        string jsonFilePath = "new.json"; // Update the path accordingly

        // Read and deserialize JSON data
        var jsonString = File.ReadAllText(jsonFilePath);
        var userConfigurations = JsonConvert.DeserializeObject<UserConfigurationList>(jsonString);

        // Connect to PostgreSQL
        var connectionString = "Host=localhost;Database=YourDatabase;Username=YourUsername;Password=YourPassword"; // Update with your credentials

        using (var connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();

            foreach (var config in userConfigurations.UserConfigurations)
            {
                // Insert or update the city
                if (config.SelectedCity != null)
                {
                    using (var command = new NpgsqlCommand("INSERT INTO Cities (Id, Name) VALUES (@Id, @Name) ON CONFLICT (Id) DO UPDATE SET Name = EXCLUDED.Name", connection))
                    {
                        command.Parameters.AddWithValue("Id", config.SelectedCity.Id);
                        command.Parameters.AddWithValue("Name", config.SelectedCity.Name);
                        command.ExecuteNonQuery();
                    }
                }

                // Insert or update the user configuration
                using (var command = new NpgsqlCommand("INSERT INTO UserConfigurations (Id, TemperatureUnit, ForecastDaysAhead, EnableNotifications, NotifyHighPressure, NotifyHighRainProbability) VALUES (@Id, @TemperatureUnit, @ForecastDaysAhead, @EnableNotifications, @NotifyHighPressure, @NotifyHighRainProbability) ON CONFLICT (Id) DO UPDATE SET TemperatureUnit = EXCLUDED.TemperatureUnit, ForecastDaysAhead = EXCLUDED.ForecastDaysAhead, EnableNotifications = EXCLUDED.EnableNotifications, NotifyHighPressure = EXCLUDED.NotifyHighPressure, NotifyHighRainProbability = EXCLUDED.NotifyHighRainProbability", connection))
                {
                    command.Parameters.AddWithValue("Id", config.Id);
                    command.Parameters.AddWithValue("TemperatureUnit", config.TemperatureUnit);
                    command.Parameters.AddWithValue("ForecastDaysAhead", config.ForecastDaysAhead);
                    command.Parameters.AddWithValue("EnableNotifications", config.NotificationSetting.EnableNotifications);
                    command.Parameters.AddWithValue("NotifyHighPressure", config.NotificationSetting.NotifyHighPressure);
                    command.Parameters.AddWithValue("NotifyHighRainProbability", config.NotificationSetting.NotifyHighRainProbability);
                    command.ExecuteNonQuery();
                }

                // Insert or update enabled parameters
                if (config.EnabledParameters != null)
                {
                    foreach (var param in config.EnabledParameters)
                    {
                        using (var command = new NpgsqlCommand("INSERT INTO EnabledParameters (Id, ParameterType, UserConfigurationId) VALUES (@Id, @ParameterType, @User ConfigurationId) ON CONFLICT (Id) DO UPDATE SET ParameterType = EXCLUDED.ParameterType, UserConfigurationId = EXCLUDED.UserConfigurationId", connection))
                        {
                            command.Parameters.AddWithValue("Id", param.Id);
                            command.Parameters.AddWithValue("ParameterType", param.ParameterType);
                            command.Parameters.AddWithValue("User ConfigurationId", param.UserConfigurationId);
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }

            Console.WriteLine("Migration completed successfully.");
        }
    }
}
