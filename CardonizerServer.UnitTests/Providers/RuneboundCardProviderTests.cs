using CardonizerServer.Api.Interfaces;
using CardonizerServer.Api.Models.Cards.RuneboundCards;
using CardonizerServer.Api.Providers;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace CardonizerServer.UnitTests.Providers;

public class RuneboundCardProviderTests
{
    private const string CardTypeId = "CardTypeId";
    
    private readonly ICardRepository _cardRepository = Substitute.For<ICardRepository>();

    [Fact]
    public async Task CanLoadCardsAsync()
    {
        var expected = new[] { new QuestCard() };
        _cardRepository.LoadCardsByCardType(CardTypeId).Returns(expected);
        var provider = new RuneboundCardProvider(_cardRepository);

        var actual = await provider.LoadCardsAsync(CardTypeId);

        actual.Should().BeEquivalentTo(expected);
    }

}