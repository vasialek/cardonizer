using CardonizerServer.Api.Entities;

namespace CardonizerServer.Api.Models;

public class GameDto
{
    public string GameNameId { get; set; }

    public string Title { get; set; }

    public CardType[] AvailableCardTypes { get; set; }
}