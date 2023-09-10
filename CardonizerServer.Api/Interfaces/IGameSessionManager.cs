using CardonizerServer.Api.Models;

namespace CardonizerServer.Api.Interfaces;

public interface IGameSessionManager
{
    GameSession GetGameSession(string gameSessionId);

    Task<GameSession> CreateAsync(string gameId);

    void Update(GameSession gameSession);

    Task<IEnumerable<GameSession>> GetAllAsync();
    
    void ResetGameSession(string gameSessionId);
}