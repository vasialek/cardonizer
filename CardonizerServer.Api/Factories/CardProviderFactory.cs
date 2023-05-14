using CardonizerServer.Api.Exceptions;
using CardonizerServer.Api.Interfaces;
using CardonizerServer.Api.Models;
using CardonizerServer.Api.Providers;
using CardonizerServer.Api.Repositories;

namespace CardonizerServer.Api.Factories;

public class CardProviderFactory : ICardProviderFactory
{
    private readonly ICardRepository _cardRepository;

    public CardProviderFactory(ICardRepository cardRepository)
    {
        _cardRepository = cardRepository;
    }

    public ICardProvider CreateProvider(string gameNameId)
    {
        return gameNameId switch
        {
            GameOptionsRepository.AndorId => new AndorCardProvider(_cardRepository),
            _ => throw new InternalFlowException(ErrorCodes.ObjectNotFound, $"Can't create card provider for unknown game `{gameNameId}`.")
        };
    }
}
