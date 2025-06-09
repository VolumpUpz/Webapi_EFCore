using Webapi_EFCore.Models;

namespace Webapi_EFCore.Services.Interfaces
{
    public interface IUserService
    {
        Task<User?> AuthenticateAsync(string username, string password);
    }
}
