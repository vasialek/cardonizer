using CardonizerServer.Api.Entities;
using CardonizerServer.Api.Interfaces;
using CardonizerServer.Api.Models;
using CardonizerServer.Api.Services;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace CardonizerServer.UnitTests.Services;

public class GameServiceTests
{
    private const string GameId = "GameId";
    private readonly GameService _service;
    private readonly IGameOptionsRepository _gameRepository = Substitute.For<IGameOptionsRepository>();

    public GameServiceTests()
    {
        _service = new GameService(_gameRepository);
    }

    [Fact]
    public async Task CanGetGameAsync()
    {
        _gameRepository.GetGameByGameId(GameId).Returns(new GameNameEntity("GameName", GameId));
        _gameRepository.GetGameCardTypes().Returns(new[]
        {
            new CardType { CardTypeId = "CardType1", GameNameId = GameId, Name = "Card Type 1" },
            new CardType { CardTypeId = "CardType2", GameNameId = "IncorrectGameId", Name = "Card Type 2" },
        });
        
        var actual = await _service.GetGameAsync(GameId);

        actual.Should().BeEquivalentTo(new GameDto
        {
            GameNameId = GameId,
            Title = "GameName",
        }, option => option.Excluding(p => p.AvailableCardTypes));
        actual.AvailableCardTypes.Should().HaveCount(1);
        actual.AvailableCardTypes.Should().ContainSingle(t => t.CardTypeId == "CardType1");
    }

}