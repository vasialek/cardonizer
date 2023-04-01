using CardonizerServer.Api.Interfaces;

namespace CardonizerServer.Api.Services;

public class UniqueIdService : IUniqueIdService
{
    public string GetUniqueId()
    {
        return Guid.NewGuid().ToString("N");
    }
}
