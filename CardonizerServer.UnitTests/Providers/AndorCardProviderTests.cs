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
}
