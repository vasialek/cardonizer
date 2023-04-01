namespace CardonizerServer.Api.Interfaces;

public interface IJsonService
{
    T DeserializeObject<T>(string json);
}
