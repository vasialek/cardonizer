using CardonizerServer.Api.Models;
using CardonizerServer.Api.Models.Requests;

namespace CardonizerServer.Api.Interfaces;

public interface IGameSessionManager
{
    GameSession GetGameSession(string gameSessionId);

    GameSession Create();

    void Update(GameSession gameSession);

    Task<IEnumerable<GameSession>> GetAllAsync();
    
    void ResetGameSession(string gameSessionId);
}