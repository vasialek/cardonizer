using CardonizerServer.Api.Entities;

namespace CardonizerServer.Api.Models;

public class GameSession
{
    public string GameSessionId { get; set; }

    public string GameNameId { get; set; }
    
    public int CurrentCardIndex { get; set; }

    public CardType[] AvailableCardTypes { get; set; }

    public CardEntityBase[] AvailableCards { get; set; }

    public bool IsLoaded => AvailableCards?.Any() == true;
}
