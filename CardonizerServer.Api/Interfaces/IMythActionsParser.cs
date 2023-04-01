using CardonizerServer.Api.Models;

namespace CardonizerServer.Api.Interfaces;

public interface IMythActionsParser
{
    IEnumerable<MythActions> Parse(string mythActionsCsv);
}