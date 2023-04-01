namespace CardonizerServer.Api.Models;

public class GameSession
{
    public string GameSessionId { get; set; }

    public string[] UsedCardIds { get; set; } = Array.Empty<string>();
}