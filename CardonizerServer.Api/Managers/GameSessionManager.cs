using CardonizerServer.Api.Exceptions;
using CardonizerServer.Api.Interfaces;
using CardonizerServer.Api.Models;
using Microsoft.Extensions.Caching.Memory;

namespace CardonizerServer.Api.Managers;

public class GameSessionManager : IGameSessionManager
{
    private static readonly MemoryCache MemoryCache = new(new MemoryCacheOptions {SizeLimit = 64});

    private readonly IUniqueIdService _uniqueIdService;

    public GameSessionManager(IUniqueIdService uniqueIdService)
    {
        _uniqueIdService = uniqueIdService;
    }

    public GameSession GetGameSession(string gameSessionId)
    {
        var exists = MemoryCache.TryGetValue(gameSessionId, out GameSession gameSession);

        return exists ? gameSession : throw new InternalFlowException("");
    }

    public GameSession Create()
    {
        var gameSession = new GameSession
        {
            GameSessionId = _uniqueIdService.GetUniqueId(),
            UsedCardIds = Array.Empty<string>()
        };

        MemoryCache.Set(gameSession.GameSessionId, gameSession, new MemoryCacheEntryOptions {Size = 1});

        return gameSession;
    }
}
