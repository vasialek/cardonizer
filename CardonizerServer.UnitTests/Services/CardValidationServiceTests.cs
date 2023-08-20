using CardonizerServer.Api.Exceptions;
using CardonizerServer.Api.Models;
using CardonizerServer.Api.Services;
using FluentAssertions;
using Xunit;

namespace CardonizerServer.UnitTests.Services;

public class CardValidationServiceTests
{
    private readonly CardValidationService _service = new CardValidationService();
    private readonly CardDto _card;

    public CardValidationServiceTests()
    {
        _card = new CardDto
        {
            CardTypeId = "CardTypeId",
            Title = "Title",
            Description = "Description",
            Payload = "Payload" 
        };
    }

    [Fact]
    public void CanValidate()
    {
        var actual = _service.Validate(_card);

        actual.Should().BeTrue();
    }

    [Fact]
    public void Validate_Error_WhenNoCardTypeId()
    {
        _card.CardTypeId = "";
        
        _service.Invoking(s => s.Validate(_card))
            .Should()
            .ThrowExactly<InternalFlowException>()
            .Where(e => e.ErrorCode == ErrorCodes.IncorrectValue)
            .Where(e => e.Message == "CardTypeId");
    }

    [Fact]
    public void Validate_Error_WhenNoTitle()
    {
        _card.Title = "";
        
        _service.Invoking(s => s.Validate(_card))
            .Should()
            .ThrowExactly<InternalFlowException>()
            .Where(e => e.ErrorCode == ErrorCodes.IncorrectValue)
            .Where(e => e.Message == "Title");
    }

    [Fact]
    public void Validate_Error_WhenNoDescription()
    {
        _card.Description = "";
        
        _service.Invoking(s => s.Validate(_card))
            .Should()
            .ThrowExactly<InternalFlowException>()
            .Where(e => e.ErrorCode == ErrorCodes.IncorrectValue)
            .Where(e => e.Message == "Description");
    }

}