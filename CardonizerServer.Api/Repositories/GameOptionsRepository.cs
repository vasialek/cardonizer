using CardonizerServer.Api.Entities;
using CardonizerServer.Api.Exceptions;
using CardonizerServer.Api.Interfaces;
using CardonizerServer.Api.Models;
using CardonizerServer.Api.Models.Cards.AndorCards;
using CardonizerServer.Api.Models.Cards.EldritchHorrorCards;

namespace CardonizerServer.Api.Repositories;

public class GameOptionsRepository : IGameOptionsRepository
{
    public IEnumerable<GameNameEntity> LoadAvailableGames()
    {
        return new[]
        {
            new GameNameEntity("Eldritch Horror", GameNameRepository.EldritchHorrorId),
            new GameNameEntity("Runebound", GameNameRepository.RuneboundId),
            new GameNameEntity("Andor", GameNameRepository.AndorId),
        };
    }

    public IEnumerable<CardType> GetGameCardTypes()
    {
        var cardTypes = new CardType[]
        {
            new ()
            {
                GameNameId = GameNameRepository.AndorId,
                CardTypeId = GoldenCard.CardType,
                Name = "Golden card"
            },
            new ()
            {
                GameNameId = GameNameRepository.AndorId,
                CardTypeId = "80e4e80a2f4842b79c748b7e1bb015ba",
                Name = "Silver card"
            },
            new ()
            {
                GameNameId = GameNameRepository.EldritchHorrorId,
                CardTypeId = MythCard.CardType,
                Name = "Mythos card"
            },
            new ()
            {
                GameNameId = GameNameRepository.EldritchHorrorId,
                CardTypeId = "e798872bd17c44feadfafc915cc68570",
                Name = "Green Travel card"
            },
            new ()
            {
                GameNameId = GameNameRepository.EldritchHorrorId,
                CardTypeId = "3fc77322657c4d59b986c91815254c02",
                Name = "Brown Travel card"
            },
            new ()
            {
                GameNameId = GameNameRepository.EldritchHorrorId,
                CardTypeId = "9acdc36fe9374d40a14e6c919c792db0",
                Name = "Violet Travel card"
            },
            new ()
            {
                GameNameId = GameNameRepository.EldritchHorrorId,
                CardTypeId = "daa6f9b4e0fa49cc905a1bb593ac7505",
                Name = "Common Travel card"
            },
            new ()
            {
                GameNameId = GameNameRepository.EldritchHorrorId,
                CardTypeId = "3fb3866f86a14f40831ce69e6aa7dc6e",
                Name = "Expedition card"
            },
            new ()
            {
                GameNameId = GameNameRepository.EldritchHorrorId,
                CardTypeId = "0990ed2c14324f4392fa6950bc046bfe",
                Name = "Ruin card"
            },
            new ()
            {
                GameNameId = GameNameRepository.EldritchHorrorId,
                CardTypeId = "31b312161edf4f44ae12ba5637edaa83",
                Name = "Monster mystery card"
            },
            new ()
            {
                GameNameId = GameNameRepository.EldritchHorrorId,
                CardTypeId = "6b65f44429de4ff683c97be90d842197",
                Name = "Monster contact card"
            },
            new ()
            {
                GameNameId = GameNameRepository.EldritchHorrorId,
                CardTypeId = "b2b6d8720c7f489aaf989284387bff41",
                Name = "Clue investigation card"
            },
            new ()
            {
                GameNameId = GameNameRepository.RuneboundId,
                CardTypeId = "7cc5942d38c14c57820226f7014279a1",
                Name = "Quest card (green)"
            },
            new ()
            {
                GameNameId = GameNameRepository.RuneboundId,
                CardTypeId = "0194c57298e94819948951a48badd0f3",
                Name = "Fight card (orange)"
            },
            new ()
            {
                GameNameId = GameNameRepository.RuneboundId,
                CardTypeId = "b31d0712777345cbb779f4e6fece492e",
                Name = "Action card (purple)"
            }
        };

        return cardTypes.ToList();
    }

    public async Task<CardType> GetCardTypeByIdAsync(string cardTypeId)
    {
        return GetGameCardTypes().FirstOrDefault(t => t.CardTypeId == cardTypeId) ??
               throw new InternalFlowException(ErrorCodes.ObjectNotFound, $"Unknown card type `{cardTypeId}`");
    }
}
