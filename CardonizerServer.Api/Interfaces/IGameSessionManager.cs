using CardonizerServer.Api.Models;

namespace CardonizerServer.Api.Interfaces;

public interface IGameSessionManager
{
    GameSession GetGameSession(string gameSessionId);

    GameSession Create();
}
