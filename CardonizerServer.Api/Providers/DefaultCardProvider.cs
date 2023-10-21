using CardonizerServer.Api.Entities;
using CardonizerServer.Api.Interfaces;

namespace CardonizerServer.Api.Providers;

public class DefaultCardProvider : ICardProvider
{
    private readonly ICardRepository _cardRepository;

    public DefaultCardProvider(ICardRepository cardRepository)
    {
        _cardRepository = cardRepository;
    }

    public Task<IEnumerable<CardEntityBase>> LoadCardsAsync(string cardTypeId)
    {
        return _cardRepository.LoadCardsByCardType(cardTypeId);
    }
}
