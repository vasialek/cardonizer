using CardonizerServer.Api.Entities;

namespace CardonizerServer.Api.Interfaces;

public interface IGameOptionsRepository
{
    Task<GameNameEntity> GetGameByGameId(string gameId);
    
    IEnumerable<CardType> GetGameCardTypes();

    Task<CardType> GetCardTypeByIdAsync(string cardTypeId);
}

