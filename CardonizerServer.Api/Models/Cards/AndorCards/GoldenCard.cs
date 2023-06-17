using CardonizerServer.Api.Entities;

namespace CardonizerServer.Api.Models.Cards.AndorCards;

public class GoldenCard : CardEntityBase
{
    public const string CardType = "efcb46987cca4f8cb3bd753c610eee53";

    public GoldenCard()
    {
        CardTypeId = CardType;
    }
}
