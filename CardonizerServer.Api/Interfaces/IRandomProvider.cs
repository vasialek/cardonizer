namespace CardonizerServer.Api.Interfaces;

public interface IRandomProvider
{
    int Next(int max);
}