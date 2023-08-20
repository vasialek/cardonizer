namespace CardonizerServer.Api.Interfaces;

public interface ICardValidationServiceFactory
{
    ICardValidationService Create(string cardTypeId);
}