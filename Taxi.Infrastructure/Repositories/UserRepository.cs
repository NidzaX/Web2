using Microsoft.EntityFrameworkCore;
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

    }
}
