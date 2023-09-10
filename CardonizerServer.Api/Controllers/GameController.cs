using CardonizerServer.Api.Interfaces;
using CardonizerServer.Api.Models;
using CardonizerServer.Api.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CardonizerServer.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GameController : ControllerBase
{
    private readonly IGameSessionManager _gameSessionManager;
    private readonly IGameNameRepository _gameNameRepository;

    public GameController(IGameSessionManager gameSessionManager, IGameNameRepository gameNameRepository)
    {
        _gameSessionManager = gameSessionManager;
        _gameNameRepository = gameNameRepository;
    }

    [HttpPost("StartGame")]
    public async Task<ActionResult> StartGameAsync(string gameNameId)
    {
        var gameSession = await _gameSessionManager.CreateAsync(gameNameId);

        return Ok(new StartGameResponse{GameSession = gameSession});
    }

    [HttpPost("ResetGame")]
    public async Task<IActionResult> ResetGameAsync(string gameSessionId)
    {
        _gameSessionManager.ResetGameSession(gameSessionId);
        return Ok();
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> LoadAvailableGames()
    {
        var games = await _gameNameRepository.LoadAvailableGames();
        return Ok(games);
    }

    [HttpGet("GetGameSessionsList")]
    public async Task<IReadOnlyCollection<GameSession>> GetGameSessionsListAsync()
    {
        return (await _gameSessionManager.GetAllAsync()).ToList();

    }
}