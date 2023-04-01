using CardonizerServer.Api.Entities;

namespace CardonizerServer.Api.Interfaces;

public interface ICardService
{
    Task<CardEntityBase> GetNextCardAsync(string gameNameId, string cardTypeId);
}