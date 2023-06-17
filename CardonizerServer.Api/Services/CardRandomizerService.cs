using CardonizerServer.Api.Entities;
using CardonizerServer.Api.Interfaces;

namespace CardonizerServer.Api.Services;

public class CardRandomizerService : ICardRandomizerService
{
    private readonly IRandomProvider _randomProvider;

    public CardRandomizerService(IRandomProvider randomProvider)
    {
        _randomProvider = randomProvider;
    }

    public IEnumerable<CardEntityBase> RandomizeOrder(IEnumerable<CardEntityBase> cards)
    {
        var orderedCards = cards.ToDictionary(c => _randomProvider.Next(10000));

        return orderedCards
            .OrderBy(c => c.Key)
            .Select(c => c.Value);
    }
}
