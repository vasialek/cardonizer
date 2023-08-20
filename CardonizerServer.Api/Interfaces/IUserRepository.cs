using CardonizerServer.Api.Models;

namespace CardonizerServer.Api.Interfaces;

public interface IUserRepository
{
    Task<UserDto> GetByEmailAsync(string email);
    Task CreateAsync(UserDto user);
}