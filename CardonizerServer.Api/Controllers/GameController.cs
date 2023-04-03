using CardonizerServer.Api.Interfaces;
using CardonizerServer.Api.Models;
using CardonizerServer.Api.Models.Requests;
using CardonizerServer.Api.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CardonizerServer.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GameController : ControllerBase
{
    private readonly IGameSessionManager _gameSessionManager;

    public GameController(IGameSessionManager gameSessionManager)
    {
        _gameSessionManager = gameSessionManager;
    }

    [HttpPost(Name = "StartGame")]
    public async Task<ActionResult> StartGameAsync(StartGameRequest request)
    {
        var gameSession = _gameSessionManager.Create();

        return Ok(new StartGameResponse{GameSession = gameSession});
    }

    [HttpGet(Name = "GetAll")]
    public async Task<IReadOnlyCollection<GameSession>> GetGameSessionsListAsync()
    {
        return (await _gameSessionManager.GetAllAsync()).ToList();

    }
}
