using CardonizerServer.Api.Entities;
using CardonizerServer.Api.Interfaces;
using CardonizerServer.Api.Mappers;
using CardonizerServer.Api.Models;
using CardonizerServer.Api.Repositories;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace CardonizerServer.UnitTests.Mappers;

public class MythCardMapperTests
{
    private readonly MythCardMapper _mapper;
    private readonly IMythActionsParser _mythActionsParser = Substitute.For<IMythActionsParser>();

    public MythCardMapperTests()
    {
        _mapper = new MythCardMapper(_mythActionsParser);
    }

    [Fact]
    public void CanMap()
    {
        var entity = new MythCardEntity
        {
            CardId = "ID",
            Title = "Title",
            Description = "Description",
            Reckoning = "Reckoning",
            Task = "Task",
            MythCategory = MythCategories.None,
            MythActionCsv = "1;2"
        };
        _mythActionsParser.Parse("1;2").Returns(new[] {MythActions.AdvanceOmen});

        var actual = _mapper.Map(entity);

        actual.Should().BeEquivalentTo(new MythCardDto
        {
            Id = "ID",
            Title = "Title",
            Description = "Description",
            Reckoning = "Reckoning",
            Task = "Task",
            MythCategory = MythCategories.None,
            MythActions = new[] {MythActions.AdvanceOmen}
        });
    }
}
