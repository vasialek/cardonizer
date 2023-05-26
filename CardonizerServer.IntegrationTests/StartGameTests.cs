using System.Linq;
using System.Threading.Tasks;
using CardonizerServer.Api.Factories;
using CardonizerServer.Api.Managers;
using CardonizerServer.Api.Repositories;
using CardonizerServer.Api.Services;
using Xunit;
using Xunit.Abstractions;

namespace CardonizerServer.IntegrationTests;

public class StartGameTests
{
    private readonly ITestOutputHelper _outputHelper;

    public StartGameTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }

    [Fact]
    public async Task CanStartAndorId()
    {
        var uniqueIdService = new UniqueIdService();
        var gameSessionManager = new GameSessionManager(uniqueIdService);
        var cardRepository = new CardRepository(uniqueIdService);
        var gameOptionsRepository = new GameOptionsRepository();
        var cardService = new CardService(new CardProviderFactory(cardRepository), gameSessionManager, gameOptionsRepository);

        var gameSession = gameSessionManager.Create();
        var andorCardTypes = gameOptionsRepository.GetGameCardTypes()
            .Where(t => t.GameNameId == GameNameRepository.AndorId)
            .ToList();

        for (var i = 0; i < 10; i++)
        {
            var card = await cardService.GetNextCardAsync(gameSession.GameSessionId, andorCardTypes[0].CardTypeId);
            _outputHelper.WriteLine("{0} \\ {1}\n{2}", card.Title, andorCardTypes[0].Name, card.Description);
        }
    }
}