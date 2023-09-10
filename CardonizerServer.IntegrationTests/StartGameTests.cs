using System.Linq;
using System.Threading.Tasks;
using CardonizerServer.Api.Factories;
using CardonizerServer.Api.Interfaces;
using CardonizerServer.Api.Managers;
using CardonizerServer.Api.Models.Cards.AndorCards;
using CardonizerServer.Api.Repositories;
using CardonizerServer.Api.Services;
using FluentAssertions;
using NSubstitute;
using Xunit;
using Xunit.Abstractions;

namespace CardonizerServer.IntegrationTests;

public class StartGameTests
{
    private readonly ITestOutputHelper _outputHelper;
    private readonly IRandomProvider _randomProvider = Substitute.For<IRandomProvider>();
    private readonly IGameNameRepository _gameNameRepository = Substitute.For<IGameNameRepository>();

    public StartGameTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }

    [Fact]
    public async Task CanStartAndorId()
    {
        var uniqueIdService = new UniqueIdService();
        var cardRepository = Substitute.For<ICardRepository>();
        var gameOptionsRepository = new GameOptionsRepository(_gameNameRepository);
        var cardProviderFactory = new CardProviderFactory(cardRepository);
        var cardRandomizerService = new CardRandomizerService(_randomProvider);
        var gameService = new GameService(gameOptionsRepository);
        var gameSessionManager = new GameSessionManager(gameService, uniqueIdService);
        var cardService = new CardService(cardProviderFactory, gameSessionManager, cardRandomizerService, gameOptionsRepository);

        var gameSession = await gameSessionManager.CreateAsync(GameNameRepository.AndorId);
        var andorCardTypes = gameOptionsRepository.GetGameCardTypes()
            .Where(t => t.GameNameId == GameNameRepository.AndorId)
            .ToList();

        _randomProvider.Next(Arg.Any<int>()).Returns(30, 10, 20);
        cardRepository.LoadCardsByCardType(GoldenCard.CardType)
            .Returns(new[]
            {
                new GoldenCard { CardId = "Card01", Title = "First", Description = "Description 1" },
                new GoldenCard { CardId = "Card02", Title = "Second", Description = "Description 2" },
                new GoldenCard { CardId = "Card03", Title = "Third", Description = "Description 3" }
            });
        
        var card = await cardService.GetNextCardAsync(gameSession.GameSessionId, GoldenCard.CardType);
        _outputHelper.WriteLine("{0} \\ {1}\n{2}", card.Title, card.GetType().Name, card.Description);
        card.CardId.Should().Be("Card02");
        
        card = await cardService.GetNextCardAsync(gameSession.GameSessionId, GoldenCard.CardType);
        _outputHelper.WriteLine("{0} \\ {1}\n{2}", card.Title, card.GetType().Name, card.Description);
        card.CardId.Should().Be("Card03");

        card = await cardService.GetNextCardAsync(gameSession.GameSessionId, GoldenCard.CardType);
        _outputHelper.WriteLine("{0} \\ {1}\n{2}", card.Title, card.GetType().Name, card.Description);
        card.CardId.Should().Be("Card01");
    }
}