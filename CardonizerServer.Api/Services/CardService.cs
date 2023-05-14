using CardonizerServer.Api.Entities;
using CardonizerServer.Api.Exceptions;
using CardonizerServer.Api.Interfaces;
using CardonizerServer.Api.Models;
using Newtonsoft.Json;

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

        var cardType = await _gameOptionsRepository.GetCardTypeByIdAsync(cardTypeId);
        var cardProvider = _cardProviderFactory.CreateProvider(cardType.GameNameId);

        var card = await cardProvider.GetNextCardAsync(cardTypeId, gameSession.UsedCardIds);
        Console.WriteLine("Got next card: {0}", card.CardId);
        gameSession.UsedCardIds.Add(card.CardId);
        _gameSessionManager.Update(gameSession);
        Console.WriteLine("Updating game session: {0}", JsonConvert.SerializeObject(gameSession));

        return card;
    }
}
