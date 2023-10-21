using CardonizerServer.Api.Entities;
using CardonizerServer.Api.Interfaces;
using CardonizerServer.Api.Providers;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace CardonizerServer.UnitTests.Providers;

public class DefaultCardProviderTests
{
    private const string CardTypeId = "CardTypeId";
    private readonly ICardRepository _cardRepository = Substitute.For<ICardRepository>();
    private readonly DefaultCardProvider _provider;

    public DefaultCardProviderTests()
    {
        _provider = new DefaultCardProvider(_cardRepository);
    }

    [Fact]
    public async Task CanLoadCardsAsync()
    {
        var expected = new[] { new CardEntityBase { CardId = "1" } };
        _cardRepository.LoadCardsByCardType(CardTypeId).Returns(expected);
        
        var actual = await _provider.LoadCardsAsync(CardTypeId);

        actual.ToList().Should().BeEquivalentTo(expected);
    }

}
