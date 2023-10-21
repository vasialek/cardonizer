using System.Collections;
using CardonizerServer.Api.Entities;
using CardonizerServer.Api.Interfaces;
using CardonizerServer.Api.Models;
using CardonizerServer.Api.Models.Cards.AndorCards;
using CardonizerServer.Api.Models.Cards.EldritchHorrorCards;
using CardonizerServer.Api.Models.Cards.RuneboundCards;
using Newtonsoft.Json;

namespace CardonizerServer.Api.Repositories;

public class CardRepository : ICardRepository
{
    private static List<CardEntityBase>? _cardsList;
    private readonly IUniqueIdService _uniqueIdService;

    public CardRepository(IUniqueIdService uniqueIdService)
    {
        _uniqueIdService = uniqueIdService;
        if (_cardsList == null)
        {
            LoadCards();
        }
    }

    public async Task<string> AddCardAsync(CardEntityBase card)
    {
        card.CardId = _uniqueIdService.GetUniqueId();

        _cardsList.Add(card);

        return card.CardId;
    }

    public async Task<IEnumerable<CardEntityBase>> LoadCardsByCardType(string cardTypeId)
    {
        return _cardsList.Where(c => c.CardTypeId == cardTypeId)
            .ToList();
    }

    private void LoadCards()
    {
        _cardsList = new List<CardEntityBase>
        {
            new SilverCard { CardId = _uniqueIdService.GetUniqueId(), Title = "AndorSilver1", Description = "About..." },
            new GoldenCard { CardId = _uniqueIdService.GetUniqueId(), Title = "AndorGolden1", Description = "About..." },
            new GoldenCard { CardId = _uniqueIdService.GetUniqueId(), Title = "AndorGolden2", Description = "About..." },
            new GoldenCard { CardId = _uniqueIdService.GetUniqueId(), Title = "AndorGolden3", Description = "About..." },
            // new MythCard
            // {
            //     MythActions = new[] { MythActions.AdvanceOmen, MythActions.MonsterSurge, MythActions.SpawnClues },
            //     CardId = _uniqueIdService.GetUniqueId(), Title = "Отравленные припасы",
            //     Description = "Ваша еду отравлена",
            //     Effect = "Каждый сыщик теряет 3 здоровья если он не возьмет состояние Отравление"
            // },
            // new QuestCard
            // {
            //     CardId = _uniqueIdService.GetUniqueId(), QuestTask = "Исследуйте долину...", Title = "Погребенное зло", Description = "",
            //     PossibleRewards = new []
            //     {
            //         new QuestCardReward("Разграбьте и получите 1 золото за каждый символ холма", RuneboundDices.Hill),
            //         new QuestCardReward("Осверните...", RuneboundDices.Hill, RuneboundDices.Hill),
            //     } 
            // },
            // new QuestCard
            // {
            //     CardId = _uniqueIdService.GetUniqueId(), QuestTask = "Пройдите проверку силы в Низинах горечи", Title = "Охота на зверя", 
            //     Description = "В Низинах горечи видели редчайшего зверя невероятно ценещегося среди охотников.",
            //     PossibleRewards = new []
            //     {
            //         new QuestCardReward("Осверните...", RuneboundDices.Hill, RuneboundDices.Hill),
            //     } 
            // },
            // new QuestCard
            // {
            //     CardId = _uniqueIdService.GetUniqueId(), QuestTask = "Исследуйте", Title = "Просьба графа", 
            //     Description = "Граф замка Сандергард попросил...",
            //     PossibleRewards = new []
            //     {
            //         new QuestCardReward("Займитесь мелкими ппоручиниям. Получите 1 золотой", RuneboundDices.Lane),
            //         new QuestCardReward("Выполните задание графа. Возьмите одну карту квеста.", RuneboundDices.Lane, RuneboundDices.Lane),
            //         new QuestCardReward("Отбейте атаку... Получите 2 раны и 3 золотых.", RuneboundDices.Everything),
            //     } 
            // },
            // new MythCard
            // {
            //     MythActions = new[] { MythActions.AdvanceOmen, MythActions.ResolveReckoning, MythActions.SpawnGate },
            //     CardId = _uniqueIdService.GetUniqueId(), Title = "A Dark Power",
            //     Effect = "Each Monster recovers all Health Health. Then each investigator immediately encounters each Monster on his space in the order of his choice.",
            //     Description = "Lightning cracks across the sky. You have a sinking dread that all the good you've accomplished is about to be undone."
            // },
            // new MythCard
            // {
            //     MythActions = new[] { MythActions.AdvanceOmen, MythActions.MonsterSurge, MythActions.SpawnClues },
            //     CardId = _uniqueIdService.GetUniqueId(), Title = "A Proposition",
            //     Effect = "The Lead Investigator may gain a Dark Pact Condition to immediately solve 1 Rumor Mythos card in play.",
            //     Description = "You had told everyone the parchment was indecipherable, but you were lying. You know the ritual necessary to summon and bind the dark power. You could strike a bargain with it. The situation is desperate, but you know that a solution is within its power. But what will it ask of you in return?"
            // },
            // new MythCard
            // {
            //     MythActions = new[] { MythActions.SpawnClues, MythActions.RumorToken, MythActions.EldritchToken },
            //     CardId = _uniqueIdService.GetUniqueId(), Title = "Dimensions Collide ",
            //     Effect = "As an encounter, an investigator on space 11 may attempt to infiltrate a hidden sect of Tcho-Tchos that is destabilizing the fabric of reality by invoking Chaugnar Faugn (Observation). If he passes, he puts an end to their rituals; he may spend Clue Clues equal to half NoInvestigators to solve this Rumor.When there are no Eldritch token Eldritch tokens on this card, investigators lose the game.",
            //     Description = "Everywhere that these portals have been reported, earthquakes shake the streets into rubble, and sinkholes swallow buildings whole.",
            //     Reckoning = "Discard Eldritch token Eldritch tokens from this card equal to half the number of Gates on the game board."
            // }
        };

        var mythCards = LoadCardsFromFile<List<MythCard>>("mythos.json", MythCard.CardType);
        var runboundQuestCards = LoadCardsFromFile<List<QuestCard>>("runebound_questcards.json");
        var runboundActionCards = LoadCardsFromFile<List<ActionCard>>("runebound_actioncards.json");
        var fpgQuestCards = LoadCardsFromFile<List<QuestCard>>("fpg_quests.json");
        _cardsList.AddRange(mythCards);
        _cardsList.AddRange(runboundQuestCards);
        _cardsList.AddRange(runboundActionCards);
        _cardsList.AddRange(fpgQuestCards);
        
        
    }

    private static T LoadCardsFromFile<T>(string cardFileName, string cardTypeId) where T : IEnumerable<CardEntityBase>
    {
        var cards = LoadCardsFromFile<T>(cardFileName);
        foreach (var card in cards)
        {
            card.CardTypeId = cardTypeId;
        }
        
        return cards;
    }
    
    private static T LoadCardsFromFile<T>(string cardFileName) where T : IEnumerable<CardEntityBase>
    {
        var json = File.ReadAllText($"/home/aleksey/_projects/cardonizer/_data/{cardFileName}");

        var cards = JsonConvert.DeserializeObject<T>(json);
        // cards.ForEach(c => c.CardTypeId = cardTypeId);
        Console.WriteLine("Loaded {0} cards", cards.Count());
        return cards;
    }
}
