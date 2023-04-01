using CardonizerServer.Api.Controllers;
using CardonizerServer.Api.Entities;
using CardonizerServer.Api.Interfaces;
using CardonizerServer.Api.Models;
using CardonizerServer.Api.Models.Responses;
using CardonizerServer.Api.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace CardonizerServer.UnitTests;

public class MythControllerTests
{
    private readonly MythController _controller;
    private readonly IMythRepository _mythRepository = Substitute.For<IMythRepository>();
    private readonly IMythCardMapper _mythCardMapper = Substitute.For<IMythCardMapper>();

    public MythControllerTests()
    {
        _controller = new MythController(_mythRepository, _mythCardMapper);
    }

    [Fact]
    public async Task CanLoadMythCardsAsync()
    {
        var mythCardEntity = new MythCardEntity();
        var mythCards = new List<MythCardEntity> {mythCardEntity};
        var mythCard = new MythCardDto();
        var expected = new LoadMythCardsResponse
        {
            Cards = new [] {mythCard}
        };
        _mythCardMapper.Map(mythCardEntity).Returns(mythCard);
        _mythRepository.GetAllMythAsync().Returns(mythCards);

        var actualResponse = await _controller.LoadMythCardsAsync("GameId");

        var actual = actualResponse as OkObjectResult;
        actual.Should().NotBeNull();
        actual.Value.Should().BeEquivalentTo(expected);
    }
}
