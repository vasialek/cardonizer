namespace CardonizerServer.Api.Models;

public class UserDto
{
    public string UserId { get; set; }

    public string Email { get; set; }

    public string PasswordHash { get; set; }
}