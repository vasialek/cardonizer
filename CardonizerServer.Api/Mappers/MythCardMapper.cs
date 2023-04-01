using CardonizerServer.Api.Entities;
using CardonizerServer.Api.Interfaces;
using CardonizerServer.Api.Models;
using CardonizerServer.Api.Repositories;

namespace CardonizerServer.Api.Mappers;

public class MythCardMapper : IMythCardMapper
{
    private readonly IMythActionsParser _mythActionsParser;

    public MythCardMapper(IMythActionsParser mythActionsParser)
    {
        _mythActionsParser = mythActionsParser;
    }

    public MythCardDto Map(MythCardEntity entity)
    {
        var mythActions = _mythActionsParser.Parse(entity.MythActionCsv).ToList();
        return new MythCardDto
        {
            Id = entity.CardId,
            Title = entity.Title,
            Description = entity.Description,
            Task = entity.Task,
            Reckoning = entity.Reckoning,
            MythCategory = entity.MythCategory,
            MythActions = mythActions
        };
    }
}
