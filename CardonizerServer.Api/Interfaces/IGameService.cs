using CardonizerServer.Api.Models;

namespace CardonizerServer.Api.Interfaces;

public interface IGameService
{
    Task<GameDto> GetGameAsync(string gameId);
}