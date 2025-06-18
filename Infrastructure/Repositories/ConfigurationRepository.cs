using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Data;
using Domain.Interfaces;
namespace Infrastracture.Repositories
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        private readonly WeatherDbContext _context;

        public ConfigurationRepository(WeatherDbContext context)
        {
            _context = context;
        }

        public void SaveConfiguration(UserConfiguration config)
        {
            _context.UserConfigurations.Update(config);
            _context.SaveChanges();
        }

        public UserConfiguration LoadConfiguration(Guid configId)
        {
            return _context.UserConfigurations
                .Include(c => c.SelectedCity)
                .FirstOrDefault(c => c.Id == configId);
        }
    }
}