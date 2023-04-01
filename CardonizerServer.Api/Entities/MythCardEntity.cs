using CardonizerServer.Api.Models;
using CardonizerServer.Api.Repositories;

namespace CardonizerServer.Api.Entities;

public class MythCardEntity : CardEntityBase
{
    public MythCategories MythCategory { get; set; }

    public string Task { get; set; }

    public string Reckoning { get; set; }

    public string MythActionCsv { get; set; }
}
