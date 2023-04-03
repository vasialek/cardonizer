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
            GameSessionId = GameSessionId
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
}
