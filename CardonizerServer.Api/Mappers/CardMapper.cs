using CardonizerServer.Api.Entities;
using CardonizerServer.Api.Interfaces;
using CardonizerServer.Api.Models;

namespace CardonizerServer.Api.Mappers;

public class CardMapper : ICardMapper
{
    public CardEntityBase Map(CardDto card)
    {
        return new CardEntityBase
        {
            CardTypeId = card.CardTypeId,
            Title = card.Title,
            Description = card.Description,
            Payload = card.Payload
        };
    }
}