namespace CardonizerServer.Api.Entities;

public class GameNameEntity
{
    public string GameNameId { get; }

    public string Name { get; set; }

    public GameNameEntity(string name, string gameNameId = "")
    {
        Name = name;
        GameNameId = gameNameId;
    }
}
