using CardonizerServer.Api.Entities;

namespace CardonizerServer.Api.Interfaces;

public interface IGameNameRepository
{
    Task<GameNameEntity[]> LoadAvailableGames();
}
