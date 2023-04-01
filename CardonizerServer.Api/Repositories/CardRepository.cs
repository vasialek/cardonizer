using CardonizerServer.Api.Entities;
using CardonizerServer.Api.Interfaces;
using CardonizerServer.Api.Models.Cards.AndorCards;

namespace CardonizerServer.Api.Repositories;

public class CardRepository : ICardRepository
{
    private readonly IUniqueIdService _uniqueIdService;

    public CardRepository(IUniqueIdService uniqueIdService)
    {
        _uniqueIdService = uniqueIdService;
    }

    public async Task<IEnumerable<CardEntityBase>> GetNextCardAsync(string cardTypeId)
    {
        var cardsList = new List<CardEntityBase>
        {
            new GoldenCard{ CardId = _uniqueIdService.GetUniqueId(), Title = "AndorGolden1", Description = "About..."},
            new GoldenCard{ CardId = _uniqueIdService.GetUniqueId(), Title = "AndorGolden2", Description = "About..."},
            new GoldenCard{ CardId = _uniqueIdService.GetUniqueId(), Title = "AndorGolden3", Description = "About..."},
        };

        return cardsList.Where(c => c.CardTypeId == cardTypeId)
            .ToList();
    }
}
