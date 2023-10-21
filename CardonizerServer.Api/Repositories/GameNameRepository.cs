using CardonizerServer.Api.Entities;
using CardonizerServer.Api.Interfaces;

namespace CardonizerServer.Api.Repositories;

public class GameNameRepository : IGameNameRepository
{
    public const string RuneboundId = "f4a48dd9f4604033a2a7f76d04e79db4";
    public const string EldritchHorrorId = "1d0d83f2a3324768b0282fb64836f91a";
    public const string AndorId = "4f75e2952ae543a7bdec9454d19b4f10";
    public const string FpgId = "15d7368915914f0ca11176d83a37a4f2";
    
    
    public Task<GameNameEntity[]> LoadAvailableGames()
    {
        var gamesEntities = new[]
        {
            new GameNameEntity("Eldritch Horror", EldritchHorrorId),
            new GameNameEntity("Runebound", RuneboundId),
            new GameNameEntity("Andor", AndorId),
            new GameNameEntity("FPG", FpgId)
        };
        
        return Task.FromResult(gamesEntities);
    }

}