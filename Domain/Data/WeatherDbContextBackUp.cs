using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Services;
namespace Domain.Data
{
    public class WeatherDbContextBackUp : DbContext
    {
        public DbSet<UserConfiguration> UserConfigurations { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<WeatherForecast> WeatherForecasts { get; set; }
        public DbSet<WeatherParameter> WeatherParameters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure City
            modelBuilder.Entity<City>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<City>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<UserConfiguration>()
                    .HasKey(uc => uc.Id);

            modelBuilder.Entity<UserConfiguration>()
                .HasMany(uc => uc.EnabledParameters)
                .WithOne() // Assuming EnabledParameter doesn't have a navigation property back to UserConfiguration
                .HasForeignKey(ep => ep.UserConfigurationId)
                .OnDelete(DeleteBehavior.Cascade); // Optional: Define delete behavior

            // Configure WeatherForecast
            modelBuilder.Entity<WeatherForecast>()
                .HasKey(wf => wf.Id);

            modelBuilder.Entity<WeatherForecast>()
                .HasOne(wf => wf.City)
                .WithMany()
                .HasForeignKey("CityId") // Use the foreign key property name
                .OnDelete(DeleteBehavior.Cascade); // Optional: Define delete behavior

            modelBuilder.Entity<WeatherForecast>()
                .HasMany<WeatherParameter>()
                .WithOne()
                .HasForeignKey("WeatherForecastId") // Foreign key in WeatherParameter
                .OnDelete(DeleteBehavior.Cascade); // Optional: Define delete behavior

            // Configure WeatherParameter
            modelBuilder.Entity<WeatherParameter>()
                .HasKey(wp => wp.Id);

            modelBuilder.Entity<WeatherParameter>()
                .Property(wp => wp.Value)
                .IsRequired();

            // Configure EnabledParameter (if using join table approach)
            modelBuilder.Entity<EnabledParameter>()
                .HasKey(ep => ep.Id);

            modelBuilder.Entity<EnabledParameter>()
                .HasOne<UserConfiguration>()
                .WithMany(uc => uc.EnabledParameters)
                .HasForeignKey(ep => ep.UserConfigurationId);
        }
    }
}
