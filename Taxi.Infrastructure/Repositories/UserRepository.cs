using Microsoft.EntityFrameworkCore;
using System.Data;
using Taxi.Domain.Users;

namespace Taxi.Infrastructure.Repositories
{
    public sealed class UserRepository : Repository<User>, IUserRepository
    {

        public UserRepository(ApplicationDbContext dbContext) : base(dbContext) 
        { 
  
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await DbContext
                .Set<User>()
                .FirstOrDefaultAsync(u => u.Email == new Email(email));
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await DbContext
                .Set<User>()
                .FirstOrDefaultAsync(user => user.Username == new Username(username));

        }
    }
}
