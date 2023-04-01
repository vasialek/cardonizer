using CardonizerServer.Api.Controllers;
using CardonizerServer.Api.Entities;
using CardonizerServer.Api.Models;
using CardonizerServer.Api.Repositories;

namespace CardonizerServer.Api.Interfaces;

public interface IMythCardMapper
{
    MythCardDto Map(MythCardEntity entity);
}
