using CardonizerServer.Api.Services;
using FluentAssertions;
using Xunit;

namespace CardonizerServer.UnitTests.Services;

public class UniqueIdServiceTests
{
    private readonly UniqueIdService _uniqueIdService = new();

    [Fact]
    public void CanGetUniqueId()
    {
        var actual = _uniqueIdService.GetUniqueId();

        actual.Should().HaveLength(32);
    }
}
