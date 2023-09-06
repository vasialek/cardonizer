using CardonizerServer.Api.Entities;
using CardonizerServer.Api.Interfaces;
using CardonizerServer.Api.Services;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace CardonizerServer.UnitTests.Services;

public class CardRandomizerServiceTests
{
    private readonly CardRandomizerService _service;
    private readonly IRandomProvider _randomProvider = Substitute.For<IRandomProvider>();

    public CardRandomizerServiceTests()
    {
        _service = new CardRandomizerService(_randomProvider);
    }

    [Fact]
    public void CanRandomizeOrder()
    {
        var cards = new CardEntityBase[]
        {
            new() { CardId = "1" }, new() { CardId = "2" }, new() { CardId = "3" }, new() { CardId = "4" }
        };
        var expected = new CardEntityBase[]
        {
            new() { CardId = "4" }, new() { CardId = "3" }, new() { CardId = "2" }, new() { CardId = "1" }
        };
        _randomProvider.Next(10000).Returns(40, 30, 20, 10);
        
        var actual = _service.RandomizeOrder(cards).ToArray();

        actual.Should().BeEquivalentTo(expected, options => options.WithStrictOrderingFor(c => c));
    }
}