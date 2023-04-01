using CardonizerServer.Api.Interfaces;
using Newtonsoft.Json;

namespace CardonizerServer.Api.Services;

public class JsonService : IJsonService
{
    public T DeserializeObject<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }
}