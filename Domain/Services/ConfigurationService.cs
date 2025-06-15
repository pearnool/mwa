using Domain.Interfaces;
using Domain.Entities;
namespace Domain.Services;

public class ConfigurationService
{
    private readonly IConfigurationRepository _repository;

    public ConfigurationService(IConfigurationRepository repository)
    {
        _repository = repository;
    }

    public void Save(UserConfiguration config)
    {
        _repository.SaveConfiguration(config);
    }

    public UserConfiguration Load(Guid configId)
    {
        return _repository.LoadConfiguration(configId);
    }
}