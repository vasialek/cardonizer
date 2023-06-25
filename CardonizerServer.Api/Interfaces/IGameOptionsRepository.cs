using CardonizerServer.Api.Entities;

namespace CardonizerServer.Api.Interfaces;

public interface IGameOptionsRepository
{
    GameNameEntity GetGameByGameId(string gameId);
    
    IEnumerable<GameNameEntity> LoadAvailableGames();

    IEnumerable<CardType> GetGameCardTypes();

    Task<CardType> GetCardTypeByIdAsync(string cardTypeId);
}

