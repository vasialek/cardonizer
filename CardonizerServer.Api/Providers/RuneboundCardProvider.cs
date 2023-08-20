using CardonizerServer.Api.Entities;
using CardonizerServer.Api.Interfaces;

namespace CardonizerServer.Api.Providers;

public class RuneboundCardProvider : ICardProvider
{
    private readonly ICardRepository _cardRepository;

    public RuneboundCardProvider(ICardRepository cardRepository)
    {
        _cardRepository = cardRepository;
    }

    public Task<IEnumerable<CardEntityBase>> LoadCardsAsync(string cardTypeId)
    {
        return _cardRepository.LoadCardsByCardType(cardTypeId);
    }
}