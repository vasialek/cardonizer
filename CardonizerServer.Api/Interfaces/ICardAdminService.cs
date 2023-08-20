using CardonizerServer.Api.Entities;
using CardonizerServer.Api.Models;

namespace CardonizerServer.Api.Interfaces;

public interface ICardAdminService
{
    Task<CardEntityBase> AddCardAsync(CardDto card);
}
