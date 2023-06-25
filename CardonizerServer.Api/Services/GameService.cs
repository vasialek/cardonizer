using CardonizerServer.Api.Interfaces;
using CardonizerServer.Api.Models;

namespace CardonizerServer.Api.Services;

public class GameService : IGameService
{
    private readonly IGameOptionsRepository _gameRepository;

    public GameService(IGameOptionsRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<GameDto> GetGameAsync(string gameId)
    {
        var game = _gameRepository.GetGameByGameId(gameId);
        var cardTypes = _gameRepository.GetGameCardTypes()
            .Where(t => t.GameNameId == gameId)
            .ToArray();

        return new GameDto
        {
            GameNameId = gameId,
            Title = game.Name,
            AvailableCardTypes = cardTypes
        };
    }
}
