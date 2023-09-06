namespace CardonizerServer.Api.Models.Cards.RuneboundCards;

public class SpecialAttack
{
    public int Lightnings { get; set; }

    public string Effect { get; set; }

    public SpecialAttack() : this(0, "")
    {
    }

    public SpecialAttack(int lightnings, string effect)
    {
        Lightnings = lightnings;
        Effect = effect;
    }
}
