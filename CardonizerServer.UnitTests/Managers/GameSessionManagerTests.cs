using CardonizerServer.Api.Entities;
using CardonizerServer.Api.Exceptions;
using CardonizerServer.Api.Interfaces;
using CardonizerServer.Api.Managers;
using CardonizerServer.Api.Models;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace CardonizerServer.UnitTests.Managers;

public class GameSessionManagerTests
{
    private const string GameSessionId = "GameSessionId";
    private const string GameNameId = "GameId";

    private readonly IGameService _gameService = Substitute.For<IGameService>();
    private readonly IUniqueIdService _uniqueIdService = Substitute.For<IUniqueIdService>();

    private readonly CardType _cardType = new() { CardTypeId = "CardTypeId" };

    private readonly GameSessionManager _manager;
    private readonly GameSession _gameSession;

    public GameSessionManagerTests()
    {
        var expectedCardTypes = new [] { _cardType };
        _gameService.GetGameAsync(GameNameId).Returns(new GameDto { Title = "Game", AvailableCardTypes = expectedCardTypes });
        _uniqueIdService.GetUniqueId().Returns(GameSessionId);

        _gameSession = new GameSession
        {
            GameSessionId = GameSessionId,
            CurrentCardIndex = 0,
            AvailableCardTypes = new[] { _cardType },
            AvailableCards = null
        };
        
        _manager = new GameSessionManager(_gameService, _uniqueIdService);
    }

    [Fact]
    public async Task CanCreate()
    {
        
        var actual = await _manager.CreateAsync(GameNameId);

        actual.Should().BeEquivalentTo(_gameSession);
    }


    [Fact]
    public async Task CanGetGameSession()
    {
        await _manager.CreateAsync(GameNameId);

        var actual = _manager.GetGameSession(GameSessionId);

        actual.Should().BeEquivalentTo(_gameSession);
    }

    [Fact]
    public void GetGameSession_Error_WhenSessionNotExists()
    {
        _manager.Invoking(m => m.GetGameSession("NonExistingSession"))
            .Should()
            .ThrowExactly<InternalFlowException>()
            .WithMessage("Game session does not exist: NonExistingSession")
            .Where(e => e.ErrorCode == ErrorCodes.ObjectNotFound);
    }

    [Fact]
    public async Task CanResetGameSession()
    {
        await _manager.CreateAsync(GameNameId);
        var gameSession = _manager.GetGameSession(GameSessionId);
        var expectedCardTypes = new[] { _cardType };
        gameSession.AvailableCardTypes = expectedCardTypes;
        gameSession.AvailableCards = new[] { new CardEntityBase() };
        gameSession.CurrentCardIndex = 111;
        _manager.Update(gameSession);

        _manager.ResetGameSession(GameSessionId);
        var actual = _manager.GetGameSession(GameSessionId);

        actual.IsLoaded.Should().BeFalse();
        actual.Should().BeEquivalentTo(new GameSession
        {
            GameSessionId = GameSessionId,
            CurrentCardIndex = 0,
            AvailableCardTypes = expectedCardTypes,
            AvailableCards = Array.Empty<CardEntityBase>()
        });
    }
}
