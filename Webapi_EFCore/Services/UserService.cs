using Microsoft.EntityFrameworkCore;
using Webapi_EFCore.Data;
using Webapi_EFCore.Models;
using Webapi_EFCore.Services.Interfaces;

namespace Webapi_EFCore.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<User?> AuthenticateAsync(string username, string password)
        {
            //not need await if no code run after this , can safe a little resource
            return _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Username == username && u.Password == password && u.IsDeleted == 0);
        }
    }
}
