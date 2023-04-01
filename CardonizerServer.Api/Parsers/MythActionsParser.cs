using CardonizerServer.Api.Interfaces;
using CardonizerServer.Api.Models;

namespace CardonizerServer.Api.Parsers;

public class MythActionsParser : IMythActionsParser
{
    public IEnumerable<MythActions> Parse(string mythActionsCsv)
    {
        return mythActionsCsv.Split(";")
            .Select(c => (MythActions)int.Parse(c))
            .ToList();
    }
}
