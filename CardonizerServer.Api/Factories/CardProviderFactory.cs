using CardonizerServer.Api.Exceptions;
using CardonizerServer.Api.Interfaces;
using CardonizerServer.Api.Models;
using CardonizerServer.Api.Providers;
using CardonizerServer.Api.Repositories;

namespace CardonizerServer.Api.Factories;

public class CardProviderFactory : ICardProviderFactory
{
    private readonly ICardRepository _cardRepository;
    private readonly IGameOptionsRepository _gameOptionsRepository;

    public CardProviderFactory(ICardRepository cardRepository, IGameOptionsRepository gameOptionsRepository)
    {
        _cardRepository = cardRepository;
        _gameOptionsRepository = gameOptionsRepository;
    }

    public ICardProvider CreateProvider(string gameNameId)
    {
        return gameNameId switch
        {
            GameNameRepository.AndorId => new AndorCardProvider(_cardRepository),
            GameNameRepository.EldritchHorrorId => new EldritchHorrorCardProvider(_cardRepository),
            GameNameRepository.RuneboundId => new RuneboundCardProvider(_cardRepository),
            _ => CreateDefaultIfValidGame(gameNameId)
        };
    }

    private ICardProvider CreateDefaultIfValidGame(string gameNameId)
    {
        try
        {
            _gameOptionsRepository.GetGameByGameId(gameNameId);
            return new DefaultCardProvider(_cardRepository);
        }
        catch (InternalFlowException exception)
        {
            throw new InternalFlowException(ErrorCodes.ObjectNotFound, $"Can't create card provider for unknown game `{gameNameId}`.");
        }
    }
}
