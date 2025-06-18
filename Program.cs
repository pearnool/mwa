using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Npgsql;
using Domain.Entities;
public class Program
{
    public static void Main()
    {
        //raad the JSON file
        string jsonFilePath = "new.json";
        string jsonData = File.ReadAllText(jsonFilePath);
        var userConfigurations = JsonConvert.DeserializeObject<Dictionary<string, List<UserConfiguration>>>(jsonData, new JsonSerializerSettings
        {
            Converters = new List<JsonConverter> { new GuidConverter() }
        });

        // postgres connection;
        var connectionString = "Host=localhost;Username=postgres;Password=balls;Database=balls";
        using (var connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();

            //data insertion
            foreach (var userConfig in userConfigurations["User  Configurations"])
            {
                var insertCommand = new NpgsqlCommand("INSERT INTO user_configurations (id, selected_city_id, temperature_unit, forecast_days_ahead, enable_notifications, notify_high_pressure, notify_high_rain_probability) VALUES (@id, @selectedCityId, @temperatureUnit, @forecastDaysAhead, @enableNotifications, @notifyHighPressure, @notifyHighRainProbability)", connection);
                insertCommand.Parameters.AddWithValue("id", userConfig.Id);
                insertCommand.Parameters.AddWithValue("selectedCityId", userConfig.SelectedCity?.Id);
                insertCommand.Parameters.AddWithValue("temperatureUnit", userConfig.TemperatureUnit.ToString());
                insertCommand.Parameters.AddWithValue("forecastDaysAhead", userConfig.ForecastDaysAhead);
                insertCommand.Parameters.AddWithValue("enableNotifications", userConfig.NotificationSetting?.EnableNotifications);
                insertCommand.Parameters.AddWithValue("notifyHighPressure", userConfig.NotificationSetting?.NotifyHighPressure);
                insertCommand.Parameters.AddWithValue("notifyHighRainProbability", userConfig.NotificationSetting?.NotifyHighRainProbability);

                insertCommand.ExecuteNonQuery();

                foreach (var param in userConfig.EnabledParameters)
                {
                    var paramInsertCommand = new NpgsqlCommand("INSERT INTO enabled_parameters (id, parameter_type, user_configuration_id) VALUES (@id, @parameterType, @userConfigurationId)", connection);
                    paramInsertCommand.Parameters.AddWithValue("id", param.Id);
                    paramInsertCommand.Parameters.AddWithValue("parameterType", param.ParameterType.ToString());
                    paramInsertCommand.Parameters.AddWithValue("userConfigurationId", param.UserConfigurationId);

                    paramInsertCommand.ExecuteNonQuery();
                }
            }
        }
    }
}

public class GuidConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(Guid);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        string value = (string)reader.Value;
        return Guid.TryParse(value, out Guid guid) ? guid : Guid.Empty; // Handle invalid GUIDs
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        writer.WriteValue(value.ToString());
    }
}