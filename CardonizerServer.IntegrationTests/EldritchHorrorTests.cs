using System.Linq;
using System.Threading.Tasks;
using CardonizerServer.Api.Entities;
using CardonizerServer.Api.Factories;
using CardonizerServer.Api.Interfaces;
using CardonizerServer.Api.Managers;
using CardonizerServer.Api.Models;
using CardonizerServer.Api.Models.Cards.AndorCards;
using CardonizerServer.Api.Models.Cards.EldritchHorrorCards;
using CardonizerServer.Api.Repositories;
using CardonizerServer.Api.Services;
using FluentAssertions;
using Newtonsoft.Json;
using NSubstitute;
using Xunit;
using Xunit.Abstractions;

namespace CardonizerServer.IntegrationTests;

public class EldritchHorrorTests
{
    private readonly ITestOutputHelper _outputHelper;
    private readonly IRandomProvider _randomProvider = Substitute.For<IRandomProvider>();

    public EldritchHorrorTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }

    [Fact]
    public async Task CanStartEldritchHorror()
    {
        var uniqueIdService = new UniqueIdService();
        var gameSessionManager = new GameSessionManager(uniqueIdService);
        var cardRepository = Substitute.For<ICardRepository>();
        var gameOptionsRepository = new GameOptionsRepository();
        var cardProviderFactory = new CardProviderFactory(cardRepository);
        var cardRandomizerService = new CardRandomizerService(_randomProvider);
        var cardService = new CardService(cardProviderFactory, gameSessionManager, cardRandomizerService, gameOptionsRepository);
        var gameSession = gameSessionManager.Create();
        _randomProvider.Next(Arg.Any<int>()).Returns(30, 10, 20, 5);
        cardRepository.LoadCardsByCardType(MythCard.CardType)
            .Returns(new[]
            {
                new MythCard
                {
                    MythActions = new [] { MythActions.AdvanceOmen, MythActions.MonsterSurge, MythActions.SpawnClues },
                    CardId = "1", Title = "Tainted Rations", Effect = "Each investigator loses Health 3 Health unless he gains a Poisoned Condition.",
                    Description = "You have been surrounded by corruption for so long that it has permeated everything you touch. Somehow, all of your food supplies have grown rancid overnight."
                },
                new MythCard
                {
                    MythActions = new [] { MythActions.AdvanceOmen, MythActions.ResolveReckoning, MythActions.SpawnGate },
                    CardId = "2", Title = "A Dark Power", Effect = "Each Monster recovers all Health Health. Then each investigator immediately encounters each Monster on his space in the order of his choice.",
                    Description = "Lightning cracks across the sky. You have a sinking dread that all the good you've accomplished is about to be undone."
                },
                new MythCard
                {
                    MythActions = new [] { MythActions.AdvanceOmen, MythActions.MonsterSurge, MythActions.SpawnClues },
                    CardId = "3", Title = "A Proposition", Effect = "The Lead Investigator may gain a Dark Pact Condition to immediately solve 1 Rumor Mythos card in play.",
                    Description = "You had told everyone the parchment was indecipherable, but you were lying. You know the ritual necessary to summon and bind the dark power. You could strike a bargain with it. The situation is desperate, but you know that a solution is within its power. But what will it ask of you in return?"
                },
                new MythCard
                {
                    MythActions = new [] { MythActions.SpawnClues, MythActions.RumorToken, MythActions.EldritchToken },
                    CardId = "4", Title = "Dimensions Collide ", Effect = "As an encounter, an investigator on space 11 may attempt to infiltrate a hidden sect of Tcho-Tchos that is destabilizing the fabric of reality by invoking Chaugnar Faugn (Observation). If he passes, he puts an end to their rituals; he may spend Clue Clues equal to half NoInvestigators to solve this Rumor.When there are no Eldritch token Eldritch tokens on this card, investigators lose the game.",
                    Description = "Everywhere that these portals have been reported, earthquakes shake the streets into rubble, and sinkholes swallow buildings whole.",
                    Reckoning = "Discard Eldritch token Eldritch tokens from this card equal to half the number of Gates on the game board."
                },
            });

        var card = await cardService.GetNextCardAsync(gameSession.GameSessionId, MythCard.CardType);
        _outputHelper.WriteLine(JsonConvert.SerializeObject(card));
        card.CardId.Should().Be("4");
        
        card = await cardService.GetNextCardAsync(gameSession.GameSessionId, MythCard.CardType);
        _outputHelper.WriteLine(JsonConvert.SerializeObject(card));
        card.CardId.Should().Be("2");

        card = await cardService.GetNextCardAsync(gameSession.GameSessionId, MythCard.CardType);
        _outputHelper.WriteLine(JsonConvert.SerializeObject(card));
        card.CardId.Should().Be("3");
        
        card = await cardService.GetNextCardAsync(gameSession.GameSessionId, MythCard.CardType);
        _outputHelper.WriteLine(JsonConvert.SerializeObject(card));
        card.CardId.Should().Be("1");
    }
}