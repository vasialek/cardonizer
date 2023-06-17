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
            new() { CardId = "Card01" }, new() { CardId = "Card02" }, new() { CardId = "Card03" }, new() { CardId = "Card04" }
        };
        var expected = new CardEntityBase[]
        {
            new() { CardId = "Card04" }, new() { CardId = "Card03" }, new() { CardId = "Card02" }, new() { CardId = "Card01" }
        };
        _randomProvider.Next(10000).Returns(40, 30, 20, 10);
        
        var actual = _service.RandomizeOrder(cards).ToArray();

        actual.Should().BeEquivalentTo(expected, options => options.WithStrictOrderingFor(c => c));
    }
}