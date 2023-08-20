namespace CardonizerServer.Api.Models.Cards.RuneboundCards;

public class QuestCardReward
{
    public RuneboundDices[] Dices { get; set; }
    
    public string Description { get; set; }

    public QuestCardReward()
    {
    }
    
    public QuestCardReward(string description, params RuneboundDices[] dices)
    {
        Description = description;
        Dices = dices;
    }
}
