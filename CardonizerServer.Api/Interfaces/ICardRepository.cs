using CardonizerServer.Api.Entities;

namespace CardonizerServer.Api.Interfaces;

public interface ICardRepository
{
    Task<IEnumerable<CardEntityBase>> LoadCardsByCardType(string cardTypeId);
}
