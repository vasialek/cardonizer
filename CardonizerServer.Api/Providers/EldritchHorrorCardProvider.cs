using CardonizerServer.Api.Entities;
using CardonizerServer.Api.Interfaces;

namespace CardonizerServer.Api.Providers;

public class EldritchHorrorCardProvider : ICardProvider
{
    private readonly ICardRepository _cardRepository;

    public EldritchHorrorCardProvider(ICardRepository cardRepository)
    {
        _cardRepository = cardRepository;
    }

    public async Task<IEnumerable<CardEntityBase>> GetCardsAsync(string cardTypeId)
    {
        return await _cardRepository.LoadCardsByCardType(cardTypeId);
    }
}