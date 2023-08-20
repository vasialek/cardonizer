using CardonizerServer.Api.Entities;
using CardonizerServer.Api.Interfaces;
using CardonizerServer.Api.Models;

namespace CardonizerServer.Api.Services;

public class CardAdminService : ICardAdminService
{
    private readonly ICardValidationServiceFactory _cardValidationServiceFactory;
    private readonly ICardMapper _cardMapper;
    private readonly ICardRepository _cardRepository;

    public CardAdminService(ICardValidationServiceFactory cardValidationServiceFactory, ICardMapper cardMapper, ICardRepository cardRepository)
    {
        _cardValidationServiceFactory = cardValidationServiceFactory;
        _cardMapper = cardMapper;
        _cardRepository = cardRepository;
    }

    public async Task<CardEntityBase> AddCardAsync(CardDto card)
    {
        var validationService = _cardValidationServiceFactory.Create(card.CardTypeId);
        validationService.Validate(card);
        var cardEntityBase = _cardMapper.Map(card);
        
        var cardId = await _cardRepository.AddCardAsync(cardEntityBase);
        cardEntityBase.CardId = cardId;
        
        return cardEntityBase;
    }
}
