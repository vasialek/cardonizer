using CardonizerServer.Api.Entities;
using CardonizerServer.Api.Interfaces;
using CardonizerServer.Api.Models.Cards.AndorCards;

namespace CardonizerServer.Api.Repositories;

public class CardRepository : ICardRepository
{
    private readonly IUniqueIdService _uniqueIdService;
    private static List<CardEntityBase>? _cardsList;

    public CardRepository(IUniqueIdService uniqueIdService)
    {
        _uniqueIdService = uniqueIdService;
        if (_cardsList == null)
        {
            LoadCards();
        }
    }

    public async Task<IEnumerable<CardEntityBase>> LoadCardsByCardType(string cardTypeId)
    {
        return _cardsList.Where(c => c.CardTypeId == cardTypeId)
            .ToList();
    }

    private void LoadCards()
    {
        _cardsList = new List<CardEntityBase>
        {
            new GoldenCard {CardId = _uniqueIdService.GetUniqueId(), Title = "AndorGolden1", Description = "About..."},
            new GoldenCard {CardId = _uniqueIdService.GetUniqueId(), Title = "AndorGolden2", Description = "About..."},
            new GoldenCard {CardId = _uniqueIdService.GetUniqueId(), Title = "AndorGolden3", Description = "About..."},
        };
    }
}
