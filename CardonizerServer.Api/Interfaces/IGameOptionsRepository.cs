using CardonizerServer.Api.Entities;

namespace CardonizerServer.Api.Interfaces;

public interface IGameOptionsRepository
{
    IEnumerable<GameNameEntity> LoadAvailableGames();

    IEnumerable<CardType> GetGameCardTypes();

    Task<CardType> GetCardTypeByIdAsync(string cardTypeId);
}

