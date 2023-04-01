using CardonizerServer.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CardonizerServer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CardController : ControllerBase
{
    private readonly ICardService _cardService;

    public CardController(ICardService cardService)
    {
        _cardService = cardService;
    }

    [HttpGet("GetNextCard")]
    public async Task<IActionResult> GetNextCardAsync(string gameSessionId, string cardTypeId)
    {
        var card = await _cardService.GetNextCardAsync(gameSessionId, cardTypeId);

        return Ok(card);
    }
}
