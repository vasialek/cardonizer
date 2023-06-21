using CardonizerServer.Api.Entities;

namespace CardonizerServer.Api.Interfaces;

public interface ICardProvider
{
    Task<IEnumerable<CardEntityBase>> GetCardsAsync(string cardTypeId);
}
