using CardonizerServer.Api.Entities;
using CardonizerServer.Api.Interfaces;

namespace CardonizerServer.Api.Providers;

public class AndorCardProvider : ICardProvider
{
    private readonly ICardRepository _cardRepository;

    public AndorCardProvider(ICardRepository cardRepository)
    {
        _cardRepository = cardRepository;
    }

    // public async Task<CardEntityBase> GetNextCardAsync(string cardTypeId, IEnumerable<string> usedCardIds)
    // {
    //     var cards = await _cardRepository.LoadCardsByCardType(cardTypeId);
    //
    //     return cards.First(c => usedCardIds.Contains(c.CardId) == false);
    // }

    public async Task<IEnumerable<CardEntityBase>> LoadCardsAsync(string cardTypeId)
    {
        return await _cardRepository.LoadCardsByCardType(cardTypeId);
    }
}
