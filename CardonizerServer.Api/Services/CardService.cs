using CardonizerServer.Api.Entities;
using CardonizerServer.Api.Exceptions;
using CardonizerServer.Api.Interfaces;
using CardonizerServer.Api.Models;

namespace CardonizerServer.Api.Services;

public class CardService : ICardService
{
    private readonly ICardProviderFactory _cardProviderFactory;
    private readonly IGameOptionsRepository _gameOptionsRepository;
    private readonly IGameSessionManager _gameSessionManager;

    public CardService(ICardProviderFactory cardProviderFactory,
        IGameSessionManager gameSessionManager,
        IGameOptionsRepository gameOptionsRepository)
    {
        _cardProviderFactory = cardProviderFactory;
        _gameSessionManager = gameSessionManager;
        _gameOptionsRepository = gameOptionsRepository;
    }

    public async Task<CardEntityBase> GetNextCardAsync(string gameSessionId, string cardTypeId)
    {
        var gameSession = _gameSessionManager.GetGameSession(gameSessionId) ??
                          throw new InternalFlowException(ErrorCodes.ObjectNotFound, $"Failed to get next card, game session was not found: {gameSessionId}");

        if (gameSession.IsLoaded == false)
        {
            var cardType = await _gameOptionsRepository.GetCardTypeByIdAsync(cardTypeId);
            var cardProvider = _cardProviderFactory.CreateProvider(cardType.GameNameId);
            var availableCards = await cardProvider.GetCardsAsync(cardTypeId);
            gameSession.AvailableCards = availableCards.ToArray();
            gameSession.CurrentCardIndex = 0;
        }

        if (gameSession.AvailableCards.Length <= gameSession.CurrentCardIndex)
        {
            throw new InternalFlowException(ErrorCodes.NoNextCard, "");
        }

        var card = gameSession.AvailableCards[gameSession.CurrentCardIndex++];
        _gameSessionManager.Update(gameSession);
        return card;
    }
}
