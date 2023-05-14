using CardonizerServer.Api.Exceptions;
using CardonizerServer.Api.Factories;
using CardonizerServer.Api.Models;
using CardonizerServer.Api.Providers;
using CardonizerServer.Api.Repositories;
using FluentAssertions;
using Xunit;

namespace CardonizerServer.UnitTests.Factories;

public class CardProviderFactoryTests
{
    private readonly CardProviderFactory _factory;

    public CardProviderFactoryTests()
    {
        _factory = new CardProviderFactory(null);
    }

    [Fact]
    public void CanCreateProvider()
    {
        var actual = _factory.CreateProvider(GameOptionsRepository.AndorId);

        actual.Should().NotBeNull();
        actual.Should().BeOfType<AndorCardProvider>();
    }

    [Fact]
    public void CreateProvider_Error_WhenUnknownProvider()
    {
        _factory.Invoking(f => f.CreateProvider("SomeNonExistingGameNameId"))
            .Should()
            .ThrowExactly<InternalFlowException>()
            .WithMessage("Can't create card provider for unknown game `SomeNonExistingGameNameId`.")
            .Where(e => e.ErrorCode == ErrorCodes.ObjectNotFound);
    }
}
