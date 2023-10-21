using CardonizerServer.Api.Entities;
using CardonizerServer.Api.Exceptions;
using CardonizerServer.Api.Factories;
using CardonizerServer.Api.Interfaces;
using CardonizerServer.Api.Models;
using CardonizerServer.Api.Providers;
using CardonizerServer.Api.Repositories;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace CardonizerServer.UnitTests.Factories;

public class CardProviderFactoryTests
{
    private readonly ICardRepository _cardRepository = Substitute.For<ICardRepository>();
    private readonly IGameOptionsRepository _gameOptionsRepository = Substitute.For<IGameOptionsRepository>();
    
    private readonly CardProviderFactory _factory;

    public CardProviderFactoryTests()
    {
        _factory = new CardProviderFactory(_cardRepository, _gameOptionsRepository);
    }

    [Theory]
    [InlineData(GameNameRepository.AndorId, typeof(AndorCardProvider))]
    [InlineData(GameNameRepository.RuneboundId, typeof(RuneboundCardProvider))]
    [InlineData(GameNameRepository.EldritchHorrorId, typeof(EldritchHorrorCardProvider))]
    public void CanCreateProvider(string gameNameId, Type expected)
    {
        var actual = _factory.CreateProvider(gameNameId);

        actual.Should().NotBeNull();
        actual.Should().BeOfType(expected);
    }

    [Fact]
    public void CreateProvider_UseDefaulCardProvider_WhenGameExists()
    {
        _gameOptionsRepository.GetGameByGameId("GameWithDefaultCardProvider").Returns(new GameNameEntity("Name", "Id"));
        
        var actual = _factory.CreateProvider("GameWithDefaultCardProvider");

        actual.Should().BeOfType<DefaultCardProvider>();
    }


    [Fact]
    public void CreateProvider_Error_WhenUnknownProvider()
    {
        _gameOptionsRepository.GetGameByGameId(default).ThrowsForAnyArgs(new InternalFlowException(ErrorCodes.None, "Fake"));
        
        _factory.Invoking(f => f.CreateProvider("SomeNonExistingGameNameId"))
            .Should()
            .ThrowExactly<InternalFlowException>()
            .WithMessage("Can't create card provider for unknown game `SomeNonExistingGameNameId`.")
            .Where(e => e.ErrorCode == ErrorCodes.ObjectNotFound);
    }
}
