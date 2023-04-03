namespace CardonizerServer.Api.Models;

public class GameSession
{
    public string GameSessionId { get; set; }

    public List<string> UsedCardIds { get; set; } = new List<string>();
}
