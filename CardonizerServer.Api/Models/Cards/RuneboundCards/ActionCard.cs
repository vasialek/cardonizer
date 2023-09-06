using CardonizerServer.Api.Entities;

namespace CardonizerServer.Api.Models.Cards.RuneboundCards;

public class ActionCard : CardEntityBase
{
    public const string CardType = "b31d0712777345cbb779f4e6fece492e";

    public string ChoiceOne { get; set; }

    public string ChoiceTwo { get; set; }
    
    public ActionCard()
    {
        CardTypeId = CardType;
    }
}
