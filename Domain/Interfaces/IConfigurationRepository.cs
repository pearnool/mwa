using Domain.Entities;
namespace Domain.Interfaces
{
    public interface IConfigurationRepository
    {
        void SaveConfiguration(UserConfiguration config);
        UserConfiguration LoadConfiguration(Guid configId);
    }
}