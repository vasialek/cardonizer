using CardonizerServer.Api.Controllers;
using CardonizerServer.Api.Entities;
using CardonizerServer.Api.Interfaces;
using CardonizerServer.Api.Models;
using CardonizerServer.Api.Models.Responses;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace CardonizerServer.UnitTests.Controllers;

public class GameControllerTests
{
    private const string GameSessionId = "GameSessionId";
    private const string GameNameId = "GameNameId";
    
    private readonly IGameSessionManager _gameSessionManager = Substitute.For<IGameSessionManager>();
    private readonly IGameNameRepository _gameNameRepository = Substitute.For<IGameNameRepository>();

    private readonly GameController _controller;

    public GameControllerTests()
    {
        _controller = new GameController(_gameSessionManager, _gameNameRepository);
    }

    [Fact]
    public async Task CanStartGameAsync()
    {
        var expected = new GameSession{GameSessionId = GameSessionId};
        _gameSessionManager.CreateAsync(GameNameId).Returns(expected);

        var actualResponse = await _controller.StartGameAsync(GameNameId);

        var actual = actualResponse as OkObjectResult;
        actual.Should().NotBeNull();
        actual.Value.Should().BeEquivalentTo(new StartGameResponse{GameSession = expected});
    }

    [Fact]
    public async Task CanResetGameAsync()
    {
        var actualResponse = await _controller.ResetGameAsync(GameSessionId);

        var actual = actualResponse as OkResult;
        actual.Should().NotBeNull();
        _gameSessionManager.Received(1).ResetGameSession(GameSessionId);
    }

    [Fact]
    public async Task CanLoadAvailableGames()
    {
        var expected = new[] { new GameNameEntity("Name 1", "GameNameId 1") };
        _gameNameRepository.LoadAvailableGames().Returns(expected);

        var actualResponse = await _controller.LoadAvailableGames();

        var actual = actualResponse as OkObjectResult;
        actual.Should().NotBeNull();
        actual.Value.Should().BeEquivalentTo(expected);
    }

}
