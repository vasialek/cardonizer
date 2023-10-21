using System.IO;
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
// ReSharper disable IdentifierTypo

namespace CardonizerServer.IntegrationTests;

public class RuneboundTests
{
    private readonly ITestOutputHelper _outputHelper;
    private readonly IRandomProvider _randomProvider = Substitute.For<IRandomProvider>();
    private readonly ICardRepository? _cardRepository = Substitute.For<ICardRepository>();
    private readonly IGameNameRepository _gameNameRepository = new GameNameRepository();
    
    private readonly GameOptionsRepository _gameOptionsRepository;

    public RuneboundTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
        _gameOptionsRepository = new GameOptionsRepository(_gameNameRepository);
    }

    [Fact]
    public async Task CanQuestCards()
    {
        var gameSessionManager = ArrangeGameSessionManager();
        var cardService = ArrangeCardService(gameSessionManager);
        _randomProvider.Next(Arg.Any<int>()).Returns(20, 30, 10);
        _cardRepository.LoadCardsByCardType(QuestCard.CardType)
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

        var actual = await cardService.GetNextCardAsync(gameSession.GameSessionId, QuestCard.CardType);
        _outputHelper.WriteLine(JsonConvert.SerializeObject(actual));
        actual.CardId.Should().Be("3");

        actual = await cardService.GetNextCardAsync(gameSession.GameSessionId, QuestCard.CardType);
        _outputHelper.WriteLine(JsonConvert.SerializeObject(actual));
        actual.CardId.Should().Be("1");
        
        actual = await cardService.GetNextCardAsync(gameSession.GameSessionId, QuestCard.CardType);
        _outputHelper.WriteLine(JsonConvert.SerializeObject(actual));
        actual.CardId.Should().Be("2");
    }

    [Fact]
    public async Task CanActionCards()
    {
        var gameSessionManager = ArrangeGameSessionManager();
        var cardService = ArrangeCardService(gameSessionManager);
        _cardRepository.LoadCardsByCardType(ActionCard.CardType)
            .Returns(new[]
            {
                new ActionCard { CardId = "1" },
                new ActionCard { CardId = "2" },
                new ActionCard { CardId = "3" },
                new ActionCard { CardId = "4" },
            });
        _randomProvider.Next(Arg.Any<int>()).Returns(40, 30, 20, 10);

        var gameSession = await gameSessionManager.CreateAsync(GameNameRepository.RuneboundId);

        var actual = await cardService.GetNextCardAsync(gameSession.GameSessionId, ActionCard.CardType);
        _outputHelper.WriteLine(JsonConvert.SerializeObject(actual));
        actual.CardId.Should().Be("4");

        actual = await cardService.GetNextCardAsync(gameSession.GameSessionId, ActionCard.CardType);
        _outputHelper.WriteLine(JsonConvert.SerializeObject(actual));
        actual.CardId.Should().Be("3");
        
        actual = await cardService.GetNextCardAsync(gameSession.GameSessionId, ActionCard.CardType);
        _outputHelper.WriteLine(JsonConvert.SerializeObject(actual));
        actual.CardId.Should().Be("2");
        
        actual = await cardService.GetNextCardAsync(gameSession.GameSessionId, ActionCard.CardType);
        _outputHelper.WriteLine(JsonConvert.SerializeObject(actual));
        actual.CardId.Should().Be("1");
    }

    [Fact]
    public async Task CanQuestCard()
    {
        var gameSessionManager = ArrangeGameSessionManager();
        var cardService = ArrangeCardService(gameSessionManager);
        _cardRepository.LoadCardsByCardType(QuestCard.CardType)
            .Returns(new[]
            {
                new ActionCard { CardId = "1" },
                new ActionCard { CardId = "2" },
                new ActionCard { CardId = "3" },
            });
        _randomProvider.Next(Arg.Any<int>()).Returns(30, 20, 10);

        var gameSession = await gameSessionManager.CreateAsync(GameNameRepository.RuneboundId);
        var actual = await cardService.GetNextCardAsync(gameSession.GameSessionId, QuestCard.CardType);
        _outputHelper.WriteLine(JsonConvert.SerializeObject(actual));
        actual.CardId.Should().Be("3");
        
        actual = await cardService.GetNextCardAsync(gameSession.GameSessionId, QuestCard.CardType);
        _outputHelper.WriteLine(JsonConvert.SerializeObject(actual));
        actual.CardId.Should().Be("2");

        actual = await cardService.GetNextCardAsync(gameSession.GameSessionId, QuestCard.CardType);
        _outputHelper.WriteLine(JsonConvert.SerializeObject(actual));
        actual.CardId.Should().Be("1");
    }

    [Fact]
    public async Task CanActionCard()
    {
        var gameSessionManager = ArrangeGameSessionManager();
        var cardService = ArrangeCardService(gameSessionManager);
        _cardRepository.LoadCardsByCardType(ActionCard.CardType)
            .Returns(new[]
            {
                new ActionCard
                {
                    CardId = "1", Title = "Старые связи", Description = "Древняя металлургия гномов известна по всем Терринту.",
                    ChoiceOne = "Благодаря старым связям в гильдии горняков, Корбина узнают в каждом городе Терринота. Торговцы и лавочники предлагают ему в дар свои товары в знак уважения к гильдии. Возьмите бесплатно любую карту приобретений с рынка любого города. Затем сбросьте эту карту.",
                    ChoiceTwo = "Окажите услугу своему согильдийцу. Он просит вас доставить к его шатру повозку ручной ковки. Используйте повозку, чтобы переместиться до любой крепости из лоюбой местности и в любое время. Это действие можно использовать только 1 раз за игру. После этого сохраните эту карту как трофей."
                },
                new ActionCard
                {
                    CardId = "2", Title = "Великолепный лук", Description = "Лаурэль путешествует по терриноту, заглядывая из раза в раз в столичные города. Ей приглянулся великолепно выполненный лук на рынке Тамалира. Но из-за неимоверной цены на него, она решается выкрасть его у местного лавочника.",
                    ChoiceOne = "Находясь в Тамалире потратье действие для того чтобы попытаться выкрасть этот лук. Для этого бростье 5 кубиков исследования. На 3-х из 5-ти должны выпасть джокеры. Это действие можно выполнять только 1 раз в свой ход. При успехе получите оружжие 'Ривенбоу' ($02) и заберите эту карту как трофей.",
                    ChoiceTwo = ""
                },
                new ActionCard
                {
                    CardId = "3", Title = "", Description = "",
                    ChoiceOne = "",
                    ChoiceTwo = ""
                },
                new ActionCard
                {
                    CardId = "4", Title = "", Description = "",
                    ChoiceOne = "",
                    ChoiceTwo = ""
                }
            });
        _randomProvider.Next(Arg.Any<int>()).Returns(40, 30, 20, 10);

        var gameSession = await gameSessionManager.CreateAsync(GameNameRepository.RuneboundId);
        CardEntityBase actual;
        actual = await cardService.GetNextCardAsync(gameSession.GameSessionId, ActionCard.CardType);
        _outputHelper.WriteLine(JsonConvert.SerializeObject(actual));
        actual.CardId.Should().Be("4");
        
        actual = await cardService.GetNextCardAsync(gameSession.GameSessionId, ActionCard.CardType);
        _outputHelper.WriteLine(JsonConvert.SerializeObject(actual));
        actual.CardId.Should().Be("3");
        
        actual = await cardService.GetNextCardAsync(gameSession.GameSessionId, ActionCard.CardType);
        _outputHelper.WriteLine(JsonConvert.SerializeObject(actual));
        actual.CardId.Should().Be("2");

        actual = await cardService.GetNextCardAsync(gameSession.GameSessionId, ActionCard.CardType);
        _outputHelper.WriteLine(JsonConvert.SerializeObject(actual));
        actual.CardId.Should().Be("1");
    }


    [Fact]
    public async Task CanFightCard()
    {
        var gameSessionManager = ArrangeGameSessionManager();
        var cardService = ArrangeCardService(gameSessionManager);
        _cardRepository.LoadCardsByCardType(FightCard.CardType)
            .Returns(new[]
            {
                new FightCard
                {
                    CardId = "1", Title = "Морской змей", Fighter = Fighters.Trikster, Health = 6,
                    Reward = "",
                    SpecialAttacks = new[]
                    {
                        new SpecialAttack(1, "Уберите 1 щит."),
                        new SpecialAttack(2, "Вылечить 1 здоровья."),
                        new SpecialAttack(3, "Нанесите 2 урона (неблокируемые).")
                    }
                },
                new FightCard
                {
                    CardId = "2", Title = "Тенеклык", Fighter = Fighters.Trikster, Health = 8,
                    Reward = "Получите 10 золотых",
                    Ability = "Этот враг получает удвоенный урон от каждой магической атаки.",
                    SpecialAttacks = new[]
                    {
                        new SpecialAttack(1, "Уход в тень: уберите 2 топора противника."),
                        new SpecialAttack(2, "Контратака: уберите 1 топор противника и нанесите 1 урон.")
                    }
                },
                new FightCard
                {
                    CardId = "3", Title = "Куархадрон", Fighter = Fighters.Mystic, Health = 8,
                    Reward = "Получите Легендарный Коравим", Ability = "Этот враг получает +1 к инициативе.",
                    SpecialAttacks = new[]
                    {
                        new SpecialAttack(1, "Указ правителя: уберите 1 топор противника."),
                        new SpecialAttack(2,
                            "Проклятье: за каждый потраченный жетон в этом раунде нанесите по 1 урону (нельзя заблокировать).")
                    }
                },
                new FightCard
                {
                    CardId = "4", Title = "Таинственный тролль", Fighter = Fighters.Savage, Health = 6,
                    Reward = "Получите Эльфийские сапоги", Ability = "",
                    SpecialAttacks = new[]
                    {
                        new SpecialAttack(1, "Уклонение: уберите 1 топор противника."),
                        new SpecialAttack(2,
                            "Контратака вырванным деревом: уберите 1 топор противника и нанесите 1 урон.")
                    },
                }
            });
        _randomProvider.Next(Arg.Any<int>()).Returns(10, 20, 30, 25);

        var gameSession = await gameSessionManager.CreateAsync(GameNameRepository.RuneboundId);
        var actual = await cardService.GetNextCardAsync(gameSession.GameSessionId, FightCard.CardType);
        _outputHelper.WriteLine(JsonConvert.SerializeObject(actual));
        actual.CardId.Should().Be("1");

        actual = await cardService.GetNextCardAsync(gameSession.GameSessionId, FightCard.CardType);
        _outputHelper.WriteLine(JsonConvert.SerializeObject(actual));
        actual.CardId.Should().Be("2");
        
        actual = await cardService.GetNextCardAsync(gameSession.GameSessionId, FightCard.CardType);
        _outputHelper.WriteLine(JsonConvert.SerializeObject(actual));
        actual.CardId.Should().Be("4");

        actual = await cardService.GetNextCardAsync(gameSession.GameSessionId, FightCard.CardType);
        _outputHelper.WriteLine(JsonConvert.SerializeObject(actual));
        actual.CardId.Should().Be("3");
    }

    [Fact]
    public async Task CanMainEnemyCard()
    {
        var gameSessionManager = ArrangeGameSessionManager();
        var cardService = ArrangeCardService(gameSessionManager);
        _cardRepository.LoadCardsByCardType(MainEnemyCard.CardType)
            .Returns(new[]
            {
                new MainEnemyCard
                {
                    CardId = "1", Title = "Огненый змей", Fighter = Fighters.Mystic, Health = 15,
                    SpecialAttacks = new[]
                    {
                        new SpecialAttack(1, "Переверните все жетоны противника"),
                        new SpecialAttack(2, "Нанести 2 черепа"),
                        new SpecialAttack(3, "Вылечить 3 здоровья")
                    }
                },
            });
        _randomProvider.Next(Arg.Any<int>()).Returns(10, 20, 30);

        var gameSession = await gameSessionManager.CreateAsync(GameNameRepository.RuneboundId);
        var actual = await cardService.GetNextCardAsync(gameSession.GameSessionId, MainEnemyCard.CardType);
        _outputHelper.WriteLine(JsonConvert.SerializeObject(actual));
        actual.CardId.Should().Be("1");
    }

    private CardService ArrangeCardService(GameSessionManager gameSessionManager)
    {
        var cardProviderFactory = new CardProviderFactory(_cardRepository);
        var cardRandomizerService = new CardRandomizerService(_randomProvider);
        var cardService = new CardService(cardProviderFactory, gameSessionManager, cardRandomizerService, _gameOptionsRepository);
        return cardService;
    }

    private GameSessionManager ArrangeGameSessionManager()
    {
        var gameService = new GameService(_gameOptionsRepository);
        var uniqueIdService = new UniqueIdService();
        var gameSessionManager = new GameSessionManager(gameService, uniqueIdService);
        return gameSessionManager;
    }
}