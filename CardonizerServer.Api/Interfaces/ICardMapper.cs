using CardonizerServer.Api.Entities;
using CardonizerServer.Api.Models;

namespace CardonizerServer.Api.Interfaces;

public interface ICardMapper
{
    CardEntityBase Map(CardDto card);
}