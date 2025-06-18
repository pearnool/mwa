/*using System.Collections.Generic;
using Domain.Entities;
using System.Collections.Generic;
namespace Domain.Data {

using System.Collections.Generic;

    public class MigrationService
    {
        private readonly WeatherDbContext _context;

        public MigrationService(WeatherDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context)); // Ensure _context is not null
        }

        // Correctly named method without spaces
        public bool MigrateUser Configurations(List<UserConfiguration> userConfigurations)
        {
            try
            {
                foreach (var config in userConfigurations)
                {
                    // Add or update the city
                    if (config.SelectedCity != null)
                    {
                        _context.Cities.Update(config.SelectedCity);
                    }

                    // Add or update the user configuration
                    _context.UserConfigurations.Update(config);
                }

                // Save changes to the database
                _context.SaveChanges();
                return true; // Indicate success
            }
            catch (Exception ex)
            {
                // Log the exception (you can use any logging framework)
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false; // Indicate failure
            }
        }
    }


}*/