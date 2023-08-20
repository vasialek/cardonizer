using CardonizerServer.Api.Exceptions;
using CardonizerServer.Api.Interfaces;
using CardonizerServer.Api.Models;

namespace CardonizerServer.Api.Services;

public class CardValidationService : ICardValidationService
{
    public bool Validate(CardDto card)
    {
        if (string.IsNullOrEmpty(card.CardTypeId))
        {
            throw new InternalFlowException(ErrorCodes.IncorrectValue, nameof(card.CardTypeId));
        }
        if (string.IsNullOrEmpty(card.Title))
        {
            throw new InternalFlowException(ErrorCodes.IncorrectValue, nameof(card.Title));
        }
        if (string.IsNullOrEmpty(card.Description))
        {
            throw new InternalFlowException(ErrorCodes.IncorrectValue, nameof(card.Description));
        }
        return true;
    }
}
