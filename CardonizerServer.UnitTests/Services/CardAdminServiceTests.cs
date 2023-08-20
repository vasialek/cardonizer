using CardonizerServer.Api.Entities;
using CardonizerServer.Api.Exceptions;
using CardonizerServer.Api.Interfaces;
using CardonizerServer.Api.Models;
using CardonizerServer.Api.Services;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace CardonizerServer.UnitTests.Services;

public class CardAdminServiceTests
{
    private const string CardTypeId = "CardTypeId";
    
    private readonly ICardValidationService _cardValidationService = Substitute.For<ICardValidationService>();
    private readonly ICardMapper _cardMapper = Substitute.For<ICardMapper>();
    private readonly ICardRepository _cardRepository = Substitute.For<ICardRepository>();
    private readonly ICardValidationServiceFactory _cardValidationServiceFactory = Substitute.For<ICardValidationServiceFactory>();

    private readonly CardDto _card = new() {CardTypeId = CardTypeId};

    private readonly CardAdminService _service;

    public CardAdminServiceTests()
    {
        _cardValidationServiceFactory.Create(CardTypeId).Returns(_cardValidationService);
        
        _service = new CardAdminService(_cardValidationServiceFactory, _cardMapper, _cardRepository);
    }

    [Fact]
    public async Task CanAddCardAsync()
    {
        var expected = new CardEntityBase();
        _cardMapper.Map(_card).Returns(expected);
        _cardRepository.AddCardAsync(expected).Returns("CardId");
        
        var actual = await _service.AddCardAsync(_card);

        actual.CardId.Should().Be("CardId");
    }

    [Fact]
    public async Task AddCardAsync_Error_WhenInvalidCard()
    {
        var expected = new InternalFlowException(ErrorCodes.None, "Fake exception");
        _cardValidationService.Validate(_card).Throws(expected);

        await _service.Invoking(s => s.AddCardAsync(_card))
            .Should()
            .ThrowExactlyAsync<InternalFlowException>();
    }

}