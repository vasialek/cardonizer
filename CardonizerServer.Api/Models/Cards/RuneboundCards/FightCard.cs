using CardonizerServer.Api.Entities;

namespace CardonizerServer.Api.Models.Cards.RuneboundCards;

public class FightCard : CardEntityBase
{
    public const string CardType = "0194c57298e94819948951a48badd0f3";

    public Fighters Fighter { get; set; }

    public int Health { get; set; }

    public string Ability { get; set; }

    public SpecialAttack[] SpecialAttacks { get; set; }

    public string Reward { get; set; }

    public FightCard()
    {
        CardTypeId = CardType;
    }
}

public class MainEnemyCard : FightCard
{
    public const string CardType = "c92953f1867e4f36b486367d8dd43781";
}