using System.Text;
using CardonizerServer.Api.Entities;
using CardonizerServer.Api.Interfaces;
using CardonizerServer.Api.Repositories;
using CardonizerServer.Api.Services;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace CardonizerServer.UnitTests;

public class MythRepositoryTests
{
    private const string CardId = "CardId";
    private const string JsonFileContent = "JsonFileContent";
    private readonly MythRepository _repository;
    private IJsonService _jsonSerializer;
    private readonly IUniqueIdService _uniqueIdService = Substitute.For<IUniqueIdService>();
    private readonly MythCardEntity _mythCard = new() {CardId = "123"};

    public MythRepositoryTests()
    {
        _jsonSerializer = Substitute.For<IJsonService>();

        _repository = new MythRepository(_uniqueIdService, _jsonSerializer);
    }

    [Fact]
    public async Task CanLoadFromJsonStreamAsync()
    {
        var content = Encoding.UTF8.GetBytes(JsonFileContent);
        var expected = new List<MythCardEntity>
        {
            _mythCard
        };
        _jsonSerializer.DeserializeObject<IList<MythCardEntity>>(JsonFileContent).Returns(expected);
        using var streamReader = new StreamReader(new MemoryStream(content));

        var actual = (await _repository.LoadFromJsonStreamAsync(streamReader)).ToList();

        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task CanAddMythCardAsync()
    {
        var mythCard = new MythCardEntity();
        var content = Encoding.UTF8.GetBytes(JsonFileContent);
        var expected = new List<MythCardEntity>
        {
            _mythCard
        };
        _uniqueIdService.GetUniqueId().Returns(CardId);
        _jsonSerializer.DeserializeObject<IList<MythCardEntity>>(JsonFileContent).Returns(expected);
        using var streamReader = new StreamReader(new MemoryStream(content));
        await _repository.LoadFromJsonStreamAsync(streamReader);
        // _jsonSerializer.DeserializeObject<IReadOnlyCollection<MythCardEntity>>()

        var actual = await _repository.AddMythCardAsync(mythCard);

        actual.Should().Be(CardId);
    }
}
