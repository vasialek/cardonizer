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

    private readonly IUniqueIdService _uniqueIdService = Substitute.For<IUniqueIdService>();

    private readonly GameSessionManager _manager;

    public GameSessionManagerTests()
    {
        _uniqueIdService.GetUniqueId().Returns(GameSessionId);

        _manager = new GameSessionManager(_uniqueIdService);
    }

    [Fact]
    public void CanCreate()
    {
        var actual = _manager.Create();

        actual.Should().BeEquivalentTo(new GameSession
        {
            GameSessionId = GameSessionId,
            CurrentCardIndex = 0,
            AvailableCards = null
        });
    }


    [Fact]
    public void CanGetGameSession()
    {
        _manager.Create();

        var actual = _manager.GetGameSession(GameSessionId);

        actual.Should().BeEquivalentTo(new GameSession()
        {
            GameSessionId = GameSessionId
        });
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
    public void CanResetGameSession()
    {
        _manager.Create();
        var gameSession = _manager.GetGameSession(GameSessionId);
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
            AvailableCards = Array.Empty<CardEntityBase>()
        });
    }
}
