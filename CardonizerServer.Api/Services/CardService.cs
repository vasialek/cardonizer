using CardonizerServer.Api.Entities;
using CardonizerServer.Api.Exceptions;
using CardonizerServer.Api.Interfaces;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace CardonizerServer.Api.Services;

public class CardService : ICardService
{
    private readonly ICardProviderFactory _cardProviderFactory;
    private readonly IGameSessionManager _gameSessionManager;
    private readonly IGameOptionsRepository _gameOptionsRepository;

    public CardService(ICardProviderFactory cardProviderFactory, IGameSessionManager gameSessionManager, IGameOptionsRepository gameOptionsRepository)
    {
        _cardProviderFactory = cardProviderFactory;
        _gameSessionManager = gameSessionManager;
        _gameOptionsRepository = gameOptionsRepository;
    }

    public async Task<CardEntityBase> GetNextCardAsync(string gameSessionId, string cardTypeId)
    {
        var gameSession = _gameSessionManager.GetGameSession(gameSessionId) ??
                          throw new InternalFlowException($"Failed to get next card, game session was not found: {gameSessionId}");

        var cardType = await _gameOptionsRepository.GetCardTypeByIdAsync(cardTypeId);
        var cardProvider = _cardProviderFactory.CreateProvider(cardType.GameNameId);

        return await cardProvider.GetNextCardAsync(cardTypeId, gameSession.UsedCardIds);
    }
}
