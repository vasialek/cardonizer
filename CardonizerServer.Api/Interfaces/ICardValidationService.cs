using CardonizerServer.Api.Models;

namespace CardonizerServer.Api.Interfaces;

public interface ICardValidationService
{
    bool Validate(CardDto card);
}
