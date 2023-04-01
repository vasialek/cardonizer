namespace CardonizerServer.Api.Models;

public class MythCardDto
{
    public string Id { get; set; }

    public MythCategories MythCategory { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string Task { get; set; }

    public string Reckoning { get; set; }

    public IReadOnlyCollection<MythActions> MythActions { get; set; }
}
