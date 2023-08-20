using CardonizerServer.Api.Entities;

namespace CardonizerServer.Api.Models.Cards.RuneboundCards;

public class QuestCard : CardEntityBase
{
    public const string CardType = "7cc5942d38c14c57820226f7014279a1";

    public string QuestTask { get; set; }
    
    public QuestCardReward[] PossibleRewards { get; set; }
}
