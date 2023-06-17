using CardonizerServer.Api.Interfaces;

namespace CardonizerServer.Api.Providers;

public class RandomProvider : IRandomProvider
{
    private readonly Random _random = new Random(DateTime.Now.Millisecond);

    public int Next(int max)
    {
        return _random.Next(max);
    }
}