using CardonizerServer.Api.Entities;
using CardonizerServer.Api.Interfaces;
using CardonizerServer.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace CardonizerServer.Api.Controllers;

[Route("api/[controller]")]
public class CardAdminController : ControllerBase
{
    private readonly ICardAdminService _cardAdminService;
    private readonly IMythRepository _mythRepository;

    public CardAdminController(ICardAdminService cardAdminService, IMythRepository mythRepository)
    {
        _cardAdminService = cardAdminService;
        _mythRepository = mythRepository;
    }

    [HttpPost("AddCard")]
    public async Task<IActionResult> AddCardAsync(CardDto card)
    {
        var cardEntityBase = await _cardAdminService.AddCardAsync(card);
        return Ok(cardEntityBase);
    }

    [HttpPost("AddMythCard")]
    public async Task<IActionResult> AddMythCardAsync(string title,
        MythCategories mythCategory,
        string task,
        string description,
        string mythActionsCsv,
        string reckoning = null)
    {
        var card = new MythCardEntity
        {
            Title = title,
            MythCategory = mythCategory,
            Task = task,
            Description = description,
            MythActionCsv = mythActionsCsv,
            Reckoning = reckoning
        };

        var cardId = await _mythRepository.AddMythCardAsync(card);
        card.CardId = cardId;

        return new OkObjectResult(card);
    }
}