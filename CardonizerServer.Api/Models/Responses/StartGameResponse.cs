namespace CardonizerServer.Api.Models.Responses;

public class StartGameResponse
{
    public GameSession GameSession { get; set; } = new GameSession();
}