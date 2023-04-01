using CardonizerServer.Api.Services;

namespace CardonizerServer.Api.Interfaces;

public interface ICardProviderFactory
{
    ICardProvider CreateProvider(string gameNameId);
}
