using CardonizerServer.Api.Interfaces;
using CardonizerServer.Api.Models.Responses;
using CardonizerServer.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CardonizerServer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MythController : ControllerBase
{
    private readonly IMythRepository _mythRepository;
    private readonly IMythCardMapper _mythCardMapper;

    public MythController(IMythRepository mythRepository, IMythCardMapper mythCardMapper)
    {
        _mythRepository = mythRepository;
        _mythCardMapper = mythCardMapper;
    }

    [HttpGet(Name = "LoadAllCards")]
    public async Task<IActionResult> LoadMythCardsAsync(string gameId)
    {
        var cards = await _mythRepository.GetAllMythAsync();
        var mythCards = cards.Select(_mythCardMapper.Map)
            .ToList();

        return Ok(new LoadMythCardsResponse{Cards = mythCards});
    }
}
