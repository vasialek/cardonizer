using CardonizerServer.Api.Entities;

namespace CardonizerServer.Api.Interfaces;

public interface ICardRepository
{
    Task<string> AddCardAsync(CardEntityBase card);
    
    Task<IEnumerable<CardEntityBase>> LoadCardsByCardType(string cardTypeId);
}
