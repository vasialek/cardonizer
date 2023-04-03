using CardonizerServer.Api.Controllers;
using CardonizerServer.Api.Interfaces;
using CardonizerServer.Api.Models;
using CardonizerServer.Api.Models.Requests;
using CardonizerServer.Api.Models.Responses;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace CardonizerServer.UnitTests.Controllers;

public class GameControllerTests
{
    private readonly IGameSessionManager _gameSessionManager = Substitute.For<IGameSessionManager>();

    private readonly GameController _controller;

    public GameControllerTests()
    {
        _controller = new GameController(_gameSessionManager);
    }

    [Fact]
    public async Task CanStartGameAsync()
    {
        var expected = new GameSession{GameSessionId = "GameSessionId"};
        var request = new StartGameRequest();
        _gameSessionManager.Create().Returns(expected);

        var actualResponse = await _controller.StartGameAsync(request);

        var actual = actualResponse as OkObjectResult;
        actual.Should().NotBeNull();
        actual.Value.Should().BeEquivalentTo(new StartGameResponse{GameSession = expected});
    }
}
