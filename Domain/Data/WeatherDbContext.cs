using Microsoft.EntityFrameworkCore;
using Domain.Entities;
namespace Domain.Data
{
    public class WeatherDbContext : DbContext
    {
        public WeatherDbContext(DbContextOptions<WeatherDbContext> options)
            : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<UserConfiguration> UserConfigurations { get; set; }
        public DbSet<EnabledParameter> EnabledParameters { get; set; }
        public DbSet<NotificationSetting> NotificationSettings { get; set; }
        public DbSet<WeatherForecast> WeatherForecasts { get; set; }
        public DbSet<WeatherParameter> WeatherParameters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //emplty for a whilr
        }
    }
}