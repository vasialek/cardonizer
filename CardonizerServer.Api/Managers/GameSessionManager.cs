using CardonizerServer.Api.Exceptions;
using CardonizerServer.Api.Interfaces;
using CardonizerServer.Api.Models;
using Microsoft.Extensions.Caching.Memory;

namespace CardonizerServer.Api.Managers;

public class GameSessionManager : IGameSessionManager
{
    private static readonly MemoryCache MemoryCache = new(new MemoryCacheOptions {SizeLimit = 64});
    private static readonly IList<string> MemoryCacheKeys = new List<string>();

    private readonly IUniqueIdService _uniqueIdService;

    public GameSessionManager(IUniqueIdService uniqueIdService)
    {
        _uniqueIdService = uniqueIdService;
    }

    public GameSession GetGameSession(string gameSessionId)
    {
        var exists = MemoryCache.TryGetValue(gameSessionId, out GameSession gameSession);

        return exists ? gameSession : throw new InternalFlowException(ErrorCodes.ObjectNotFound, $"Game session does not exist: {gameSessionId}");
    }

    public GameSession Create()
    {
        var gameSession = new GameSession
        {
            GameSessionId = _uniqueIdService.GetUniqueId(),
            UsedCardIds = new List<string>()
        };

        MemoryCache.Set(gameSession.GameSessionId, gameSession, new MemoryCacheEntryOptions {Size = 1});
        MemoryCacheKeys.Add(gameSession.GameSessionId);

        return gameSession;
    }

    public void Update(GameSession gameSession)
    {
        MemoryCache.Set(gameSession.GameSessionId, gameSession, new MemoryCacheEntryOptions {Size = 1});
    }

    public async Task<IEnumerable<GameSession>> GetAllAsync()
    {
        return MemoryCacheKeys.Select(k => MemoryCache.Get<GameSession>(k))
            .ToList();
    }

    public void ResetGameSession(string gameSessionId)
    {
        var session = GetGameSession(gameSessionId);
        session.UsedCardIds.Clear();
    }
}
