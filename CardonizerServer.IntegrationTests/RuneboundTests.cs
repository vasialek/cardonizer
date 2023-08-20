using System.Threading.Tasks;
using CardonizerServer.Api.Entities;
using CardonizerServer.Api.Factories;
using CardonizerServer.Api.Interfaces;
using CardonizerServer.Api.Managers;
using CardonizerServer.Api.Models.Cards.RuneboundCards;
using CardonizerServer.Api.Repositories;
using CardonizerServer.Api.Services;
using FluentAssertions;
using Newtonsoft.Json;
using NSubstitute;
using Xunit;
using Xunit.Abstractions;

namespace CardonizerServer.IntegrationTests;

public class RuneboundTests
{
    private readonly ITestOutputHelper _outputHelper;
    private readonly IRandomProvider _randomProvider = Substitute.For<IRandomProvider>();

    public RuneboundTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }

    [Fact]
    public async Task CanQuestCards()
    {
        var uniqueIdService = new UniqueIdService();
        var cardRepository = Substitute.For<ICardRepository>();
        var gameOptionsRepository = new GameOptionsRepository();
        var cardProviderFactory = new CardProviderFactory(cardRepository);
        var cardRandomizerService = new CardRandomizerService(_randomProvider);
        var gameService = new GameService(gameOptionsRepository);
        var gameSessionManager = new GameSessionManager(gameService, uniqueIdService);
        var cardService = new CardService(cardProviderFactory, gameSessionManager, cardRandomizerService, gameOptionsRepository);
        _randomProvider.Next(Arg.Any<int>()).Returns(20, 30, 10);
        cardRepository.LoadCardsByCardType(QuestCard.CardType)
            .Returns(new[]
            {
                new QuestCard
                {
                    CardId = "1", QuestTask = "Исследуйте долину...", Title = "Погребенное зло", Description = "",
                    PossibleRewards = new []
                    {
                        new QuestCardReward("Разграбьте и получите 1 золото за каждый символ холма", RuneboundDices.Hill),
                        new QuestCardReward("Осверните...", RuneboundDices.Hill, RuneboundDices.Hill),
                    } 
                },
                new QuestCard
                {
                    CardId = "2", QuestTask = "Пройдите проверку силы в Низинах горечи", Title = "Охота на зверя", 
                    Description = "В Низинах горечи видели редчайшего зверя невероятно ценещегося среди охотников.",
                    PossibleRewards = new []
                    {
                        new QuestCardReward("Осверните...", RuneboundDices.Hill, RuneboundDices.Hill),
                    } 
                },
                new QuestCard
                {
                    CardId = "3", QuestTask = "Исследуйте", Title = "Просьба графа", 
                    Description = "Граф замка Сандергард попросил...",
                    PossibleRewards = new []
                    {
                        new QuestCardReward("Займитесь мелкими ппоручиниям. Получите 1 золотой", RuneboundDices.Lane),
                        new QuestCardReward("Выполните задание графа. Возьмите одну карту квеста.", RuneboundDices.Lane, RuneboundDices.Lane),
                        new QuestCardReward("Отбейте атаку... Получите 2 раны и 3 золотых.", RuneboundDices.Everything),
                    } 
                }
            });

        var gameSession = await gameSessionManager.CreateAsync(GameNameRepository.RuneboundId);
        CardEntityBase actual;
        
        actual = await cardService.GetNextCardAsync(gameSession.GameSessionId, QuestCard.CardType);
        _outputHelper.WriteLine(JsonConvert.SerializeObject(actual));
        actual.CardId.Should().Be("3");

        actual = await cardService.GetNextCardAsync(gameSession.GameSessionId, QuestCard.CardType);
        _outputHelper.WriteLine(JsonConvert.SerializeObject(actual));
        actual.CardId.Should().Be("1");
        
        actual = await cardService.GetNextCardAsync(gameSession.GameSessionId, QuestCard.CardType);
        _outputHelper.WriteLine(JsonConvert.SerializeObject(actual));
        actual.CardId.Should().Be("2");
    }

}