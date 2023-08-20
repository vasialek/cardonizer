using CardonizerServer.Api.Entities;

namespace CardonizerServer.Api.Models.Cards.AndorCards;

public class SilverCard : CardEntityBase
{
    public const string CardType = "3aadce9850eb4baaac9845bcbb5acaa6";
    
    public SilverCard()
    {
        CardTypeId = CardType;
    }
}
