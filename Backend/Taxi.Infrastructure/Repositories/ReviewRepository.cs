using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taxi.Domain.Review;

namespace Taxi.Infrastructure.Repositories
{
    public sealed class ReviewRepository : Repository<Review>, IReviewRepository
    {
        public ReviewRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<int>> GetReviewScoresByDriverIdAsync(Guid driverId)
        {
            return await DbContext.Reviews
                .Where(review => review.DriverId == driverId)
                .Select(review => review.Rating.Value)
                .ToListAsync();
        }
    }
}
