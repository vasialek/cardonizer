using CardonizerServer.Api.Controllers;
using CardonizerServer.Api.Entities;
using CardonizerServer.Api.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace CardonizerServer.UnitTests.Controllers;

public class CardControllerTests
{
    private const string GameId = "GameId";
    private const string CardTypeId = "CardTypeId";
    private readonly CardController _controller;
    private readonly ICardService _cardService = Substitute.For<ICardService>();

    public CardControllerTests()
    {
        _controller = new CardController(_cardService);
    }

    [Fact]
    public async Task CanGetNextCard()
    {
        var expected = new CardEntityBase();
        _cardService.GetNextCardAsync(GameId, CardTypeId).Returns(expected);

        var actualResponse = await _controller.GetNextCardAsync(GameId, CardTypeId);

        var actual = actualResponse as OkObjectResult;
        actual.Should().NotBeNull();
        actual.Value.Should().Be(expected);
    }
}
