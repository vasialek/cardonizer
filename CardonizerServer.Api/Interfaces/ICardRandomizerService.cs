using CardonizerServer.Api.Entities;

namespace CardonizerServer.Api.Interfaces;

public interface ICardRandomizerService
{
    IEnumerable<CardEntityBase> RandomizeOrder(IEnumerable<CardEntityBase> cards);
}