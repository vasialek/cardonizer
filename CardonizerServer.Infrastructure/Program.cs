using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using CardonizerServer.Api.Models;
using CardonizerServer.Infrastructure.Repositories;
using Newtonsoft.Json;

namespace CardonizerServer.Infrastructure;

public static class Program
{
    public static async Task Main()
    {
        Console.WriteLine("Main...");
        var bytes = Encoding.UTF8.GetBytes("Test1234");
        var hash = HashAlgorithm.Create("SHA1").ComputeHash(bytes);
        var passwordHash = Convert.ToBase64String(hash);

        var userRepository = new UserRepository();
        var user = await userRepository.GetByEmailAsync("p.roglamer@gmail.com");
        Console.WriteLine(JsonConvert.SerializeObject(user));
        // await new UserRepository().CreateAsync(new UserDto
        // {
        //     Email = "p.roglamer@gmail.com",
        //     UserId = "6cbb80fd662e47d6b22dbb5271b98c35",
        //     PasswordHash = passwordHash
        // });

    }
}