using CardonizerServer.Api.Entities;
using CardonizerServer.Api.Interfaces;
using CardonizerServer.Api.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CardonizerServer.Api.Repositories;

public class MythRepository : IMythRepository
{
    private readonly IUniqueIdService _uniqueIdService;
    private readonly IJsonService _jsonSerializer;
    private static IList<MythCardEntity> _cards;

    public MythRepository(IUniqueIdService uniqueIdService, IJsonService jsonSerializer)
    {
        _uniqueIdService = uniqueIdService;
        _jsonSerializer = jsonSerializer;
    }
    
    public async Task<IEnumerable<MythCardEntity>> LoadFromJsonStreamAsync(StreamReader reader)
    {
        var json = await reader.ReadToEndAsync();

        _cards = string.IsNullOrEmpty(json)
                ? new List<MythCardEntity>()
                : _jsonSerializer.DeserializeObject<IList<MythCardEntity>>(json);

        return _cards;
    }

    public async Task<IEnumerable<MythCardEntity>> GetAllMythAsync()
    {
        if (_cards == null)
        {
            throw new FileLoadException($"Method {nameof(MythRepository.LoadFromJsonStreamAsync)} must be called before this.");
        }

        return _cards;
        // return new List<MythCardEntity>
        // {
        //     new()
        //     {
        //         Id = "c8c115b39678472b80ba25343c25e071",
        //         MythCategory = MythCategories.None,
        //         Title = "Myth 1",
        //         Description = "Myth description",
        //         Task = "Myth task",
        //         Reckoning = "Reckoning",
        //         MythActionCsv = GetMythActionCsv(MythActions.SpawnClues, MythActions.AdvanceOmen)
        //     }
        // };
    }

    public async Task<string> AddMythCardAsync(MythCardEntity mythCard)
    {
        if (_cards == null)
        {
            _cards = new List<MythCardEntity>();
            // using var stream = File.Open("/home/al/temp/cardonizer/_mythos_db.json", FileMode.OpenOrCreate);
            // await LoadFromJsonStreamAsync(new StreamReader(stream));
        }

        mythCard.CardId = _uniqueIdService.GetUniqueId();
        _cards.Add(mythCard);
        return mythCard.CardId;
    }

    private static string GetMythActionCsv(params MythActions[] mythActions)
    {
        return string.Join(";", mythActions.Select(a => (int) a));
    }
}
