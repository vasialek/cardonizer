using CardonizerServer.Api.Entities;

namespace CardonizerServer.Api.Interfaces;

public interface IMythRepository
{
    Task<IEnumerable<MythCardEntity>> LoadFromJsonStreamAsync(StreamReader reader);

    Task<IEnumerable<MythCardEntity>> GetAllMythAsync();

    Task<string> AddMythCardAsync(MythCardEntity mythCard);
}
