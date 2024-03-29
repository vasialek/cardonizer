using CardonizerServer.Api.Entities;
using CardonizerServer.Api.Exceptions;
using CardonizerServer.Api.Interfaces;
using CardonizerServer.Api.Models;
using CardonizerServer.Api.Services;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace CardonizerServer.UnitTests.Services;

public class CardServiceTests
{
    private const string GameId = "GameId1";
    private const string CardTypeId = "CardType1";
    private const string GameSessionId = "GameSessionId";
    private const string IncorrectCardTypeId = "IncorrectCardType";

    private readonly ICardProvider _cardProvider = Substitute.For<ICardProvider>();
    private readonly ICardProviderFactory _cardProviderFactory = Substitute.For<ICardProviderFactory>();
    private readonly ICardRandomizerService _cardRandomizerService = Substitute.For<ICardRandomizerService>();
    private readonly IGameOptionsRepository _gameOptionsRepository = Substitute.For<IGameOptionsRepository>();
    private readonly IGameSessionManager _gameSessionManager = Substitute.For<IGameSessionManager>();

    private readonly CardService _service;
    private readonly CardEntityBase _card1 = new() {CardId = "Card2"};
    private readonly CardEntityBase _card2 = new() {CardId = "Card1"};
    private readonly CardType _cardType = new() { CardTypeId = CardTypeId};

    public CardServiceTests()
    {
        _service = new CardService(_cardProviderFactory, _gameSessionManager, _cardRandomizerService, _gameOptionsRepository);
    }

    [Fact]
    public async Task CanGetNextCardAsync()
    {
        var expectedAvailableCards = new[] { _card2, _card1 };
        _gameSessionManager.GetGameSession(GameSessionId)
            .Returns(new GameSession {GameSessionId = GameSessionId, CurrentCardIndex = 1, AvailableCardTypes = new [] { _cardType }, AvailableCards = expectedAvailableCards});

        var actual = await _service.GetNextCardAsync(GameSessionId, CardTypeId);

        actual.Should().Be(_card1);
        _gameSessionManager.Received().Update(Arg.Is<GameSession>(g => g.GameSessionId == GameSessionId && g.CurrentCardIndex == 2));
    }

    [Fact]
    public async Task GetNextCardAsync_LoadCards_WhenNotLoaded()
    {
        _gameSessionManager.GetGameSession(GameSessionId).Returns(new GameSession {GameSessionId = GameSessionId, AvailableCardTypes = new [] { _cardType }});
        _gameOptionsRepository.GetCardTypeByIdAsync(CardTypeId).Returns(new CardType { GameNameId = GameId });
        _cardProviderFactory.CreateProvider(GameId).Returns(_cardProvider);
        var cards = new[] { _card2, _card1 };
        _cardProvider.LoadCardsAsync(CardTypeId).Returns(cards);
        _cardRandomizerService.RandomizeOrder(cards).Returns(new[] { _card1, _card2 });

        var actual = await _service.GetNextCardAsync(GameSessionId, CardTypeId);

        actual.Should().Be(_card1);
    }

    [Fact]
    public async Task GetNextCardAsync_Error_WhenIncorrectCardType()
    {
        _gameSessionManager.GetGameSession(GameSessionId).Returns(new GameSession {GameNameId = GameId, GameSessionId = GameSessionId, AvailableCardTypes = Array.Empty<CardType>()});
        _gameOptionsRepository.GetCardTypeByIdAsync(IncorrectCardTypeId).Returns(new CardType { CardTypeId = IncorrectCardTypeId });
        
        await _service.Invoking(s => s.GetNextCardAsync(GameSessionId, IncorrectCardTypeId))
            .Should()
            .ThrowExactlyAsync<InternalFlowException>()
            .Where(e => e.ErrorCode == ErrorCodes.ObjectNotFound)
            .Where(e => e.Message == $"Card type {IncorrectCardTypeId} is not available for Game {GameId}");
    }


    [Fact]
    public async Task GetNextCardAsync_Error_WhenIncorrectGameSessionId()
    {
        _gameSessionManager.GetGameSession(GameSessionId).ReturnsNull();

        await _service.Invoking(s => s.GetNextCardAsync(GameSessionId, CardTypeId))
            .Should()
            .ThrowExactlyAsync<InternalFlowException>()
            .WithMessage($"Failed to get next card, game session was not found: {GameSessionId}")
            .Where(e => e.ErrorCode == ErrorCodes.ObjectNotFound);
    }

    [Fact]
    public async Task GetNextCardAsync_Error_WhenNoNextCard()
    {
        _gameSessionManager.GetGameSession(GameSessionId)
            .Returns(new GameSession { GameSessionId = GameSessionId, CurrentCardIndex = 1, AvailableCardTypes = new []{ _cardType }, AvailableCards = new[] { _card1 } });

        await _service.Invoking(s => s.GetNextCardAsync(GameSessionId, CardTypeId))
            .Should()
            .ThrowExactlyAsync<InternalFlowException>()
            .Where(e => e.ErrorCode == ErrorCodes.NoNextCard);
    }
}
