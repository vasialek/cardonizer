using CardonizerServer.Api.Entities;

namespace CardonizerServer.Api.Interfaces;

public interface ICardProvider
{
    Task<CardEntityBase> GetNextCardAsync(string cardTypeId, IEnumerable<string> usedCardIds);
}
