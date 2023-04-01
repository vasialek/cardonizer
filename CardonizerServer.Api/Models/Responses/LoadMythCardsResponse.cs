using CardonizerServer.Api.Controllers;

namespace CardonizerServer.Api.Models.Responses;

public class LoadMythCardsResponse
{
    public IReadOnlyCollection<MythCardDto> Cards { get; set; }
}