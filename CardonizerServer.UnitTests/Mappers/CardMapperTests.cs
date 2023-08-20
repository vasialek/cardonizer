using CardonizerServer.Api.Entities;
using CardonizerServer.Api.Mappers;
using CardonizerServer.Api.Models;
using FluentAssertions;
using Xunit;

namespace CardonizerServer.UnitTests.Mappers;

public class CardMapperTests
{
    private const string CardTypeId = "CardTypeId";
    private const string Title = "Title";
    private const string Description = "Description";
    private const string Payload = "Payload";
    
    private readonly CardMapper _mapper = new();

    [Fact]
    public void CanMap()
    {
        var card = new CardDto
        {
            CardTypeId = CardTypeId,
            Title = Title,
            Description = Description,
            Payload = Payload
        };
        var expected = new CardEntityBase
        {
            CardTypeId = CardTypeId,
            Title = Title,
            Description = Description,
            Payload = Payload
        };

        var actual = _mapper.Map(card);

        actual.Should().BeEquivalentTo(expected);
    }

}