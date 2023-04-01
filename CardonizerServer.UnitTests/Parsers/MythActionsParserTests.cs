using CardonizerServer.Api.Models;
using CardonizerServer.Api.Parsers;
using FluentAssertions;
using Xunit;

namespace CardonizerServer.UnitTests.Parsers;

public class MythActionsParserTests
{
    private readonly MythActionsParser _parser = new MythActionsParser();

    [Fact]
    public void CanParse()
    {
        var actual = _parser.Parse("1;2;3").ToList();

        actual.Should().BeEquivalentTo(new List<MythActions>
        {
            MythActions.AdvanceOmen,
            MythActions.ResolveReckoning,
            MythActions.SpawnGate
        });
    }
}
