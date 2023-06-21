using CardonizerServer.Api.Entities;

namespace CardonizerServer.Api.Models.Cards.EldritchHorrorCards;

public class MythCard : CardEntityBase
{
    public const string CardType = "d03643da39e64265be1319312725ed40";

    public MythActions[] MythActions { get; set; }
    
    public string Reckoning { get; set; }
    public string Effect { get; set; }

    public MythCard()
    {
        CardTypeId = CardType;
    }
}