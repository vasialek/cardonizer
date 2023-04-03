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
    private readonly ICardProvider _cardProvider = Substitute.For<ICardProvider>();
    private readonly ICardProviderFactory _cardProviderFactory = Substitute.For<ICardProviderFactory>();
    private readonly IGameOptionsRepository _gameOptionsRepository = Substitute.For<IGameOptionsRepository>();
    private readonly IGameSessionManager _gameSessionManager = Substitute.For<IGameSessionManager>();

    private readonly CardService _service;

    public CardServiceTests()
    {
        _service = new CardService(_cardProviderFactory, _gameSessionManager, _gameOptionsRepository);
    }

    [Fact]
    public async Task CanGetNextCardAsync()
    {
        var expected = new CardEntityBase();
        var expectedUsedCardIds = new List<string> {"UsedCardId"};
        var cardType = new CardType {GameNameId = GameId};
        IEnumerable<string> actualUsedCardIds = null;
        _gameOptionsRepository.GetCardTypeByIdAsync(CardTypeId).Returns(cardType);
        _gameSessionManager.GetGameSession(GameSessionId)
            .Returns(new GameSession {GameSessionId = GameSessionId, UsedCardIds = expectedUsedCardIds});
        _cardProviderFactory.CreateProvider(GameId).Returns(_cardProvider);
        _cardProvider.GetNextCardAsync(CardTypeId, Arg.Do<IEnumerable<string>>(l => actualUsedCardIds = l))
            .Returns(expected);

        var actual = await _service.GetNextCardAsync(GameSessionId, CardTypeId);

        actual.Should().Be(expected);
        actualUsedCardIds.Should().BeEquivalentTo(expectedUsedCardIds);
        _gameSessionManager.Received().Update(Arg.Is<GameSession>(g => g.GameSessionId == GameSessionId));
    }

    [Fact]
    public async Task GetNextCardAsync_Error_WhenIncorrectGameSessionId()
    {
        var expected = new CardEntityBase();
        var cardType = new CardType {GameNameId = GameId};
        _gameSessionManager.GetGameSession(GameSessionId).ReturnsNull();

        await _service.Invoking(s => s.GetNextCardAsync(GameSessionId, CardTypeId))
            .Should()
            .ThrowExactlyAsync<InternalFlowException>()
            .WithMessage($"Failed to get next card, game session was not found: {GameSessionId}");
    }
}
