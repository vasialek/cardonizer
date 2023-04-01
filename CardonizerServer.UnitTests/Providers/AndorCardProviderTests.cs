using CardonizerServer.Api.Interfaces;
using CardonizerServer.Api.Models.Cards.AndorCards;
using CardonizerServer.Api.Providers;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace CardonizerServer.UnitTests.Providers;

public class AndorCardProviderTests
{
    private const string CardTypeId = "CardTypeId";
    
    private readonly ICardRepository _cardRepository = Substitute.For<ICardRepository>();

    private readonly AndorCardProvider _cardProvider;
    private readonly GoldenCard _card = new GoldenCard { CardId = "CardId", Title = "Golden Card"};

    public AndorCardProviderTests()
    {
        _cardProvider = new AndorCardProvider(_cardRepository);
    }

    [Fact]
    public async Task CanGetNextCardAsync()
    {
        _cardRepository.GetNextCardAsync(CardTypeId).Returns(new[] {_card});

        var actual = await _cardProvider.GetNextCardAsync(CardTypeId, Array.Empty<string>());

        actual.Should().Be(_card);
    }

    [Fact]
    public async Task GetNextCardAsync_SkipUsedCard()
    {
        var expected = new GoldenCard() {CardId = "UnusedCardId"};
        _cardRepository.GetNextCardAsync(CardTypeId)
            .Returns(new[] {_card, expected});

        var actual = await _cardProvider.GetNextCardAsync(CardTypeId, new[] {"CardId"});

        actual.Should().Be(expected);
    }
}
