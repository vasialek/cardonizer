using CardonizerServer.Api.Controllers;
using CardonizerServer.Api.Entities;
using CardonizerServer.Api.Interfaces;
using CardonizerServer.Api.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace CardonizerServer.UnitTests.Controllers;

public class CardAdminControllerTests
{
    private const string MythCardId = "MythCardId";
    private readonly CardAdminController _controller;
    private readonly IMythRepository _mythRepository = Substitute.For<IMythRepository>();

    public CardAdminControllerTests()
    {
        _controller = new CardAdminController(_mythRepository);
    }

    [Fact]
    public async Task CanAddMythCardAsync()
    {
        MythCardEntity actualMythCard = null;
        _mythRepository.AddMythCardAsync(Arg.Do<MythCardEntity>(c => actualMythCard = c)).Returns(MythCardId);
        
        var actualResponse = await _controller.AddMythCardAsync("Title", MythCategories.Process, "Task", "Description", "1:2:3", "Reckoning");

        var actual = actualResponse as OkObjectResult;
        actual.Should().NotBeNull();
        ((MythCardEntity)actual.Value).CardId.Should().Be(MythCardId);
        actualMythCard.Should().BeEquivalentTo(new MythCardEntity
        {
            Title = "Title",
            Task = "Task",
            Description = "Description",
            MythActionCsv = "1:2:3",
            Reckoning = "Reckoning",
            MythCategory = MythCategories.Process
        }, option => option.Excluding(c => c.CardId));
    }
}
