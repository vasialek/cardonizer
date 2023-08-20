using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using CardonizerServer.Api.Interfaces;
using CardonizerServer.Api.Models;

namespace CardonizerServer.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    public async Task<UserDto> GetByEmailAsync(string email)
    {
        var client = new AmazonDynamoDBClient();
        var request = new GetItemRequest
        {
            TableName = "cardonizer-users",
            Key = new Dictionary<string, AttributeValue>
            {
                ["email"] = new AttributeValue{S = email}
            }
        };
        
        var user = await client.GetItemAsync(request);
        var userId = user.Item["userid"];
        var passwordhash = user.Item["password_hash"];

        return new UserDto
        {
            UserId = userId.S,
            Email = email,
            PasswordHash = passwordhash.S
        };
    }

    public async Task CreateAsync(UserDto user)
    {
        var client = new AmazonDynamoDBClient();
        var request = new PutItemRequest
        {
            TableName = "cardonizer-users",
            Item = new Dictionary<string, AttributeValue>
            {
                ["userid"] = new AttributeValue{S = user.UserId},
                ["email"] = new AttributeValue{S = user.Email},
                ["password_hash"] = new AttributeValue{S = user.PasswordHash}
            }
        };

        await client.PutItemAsync(request);
    }
}