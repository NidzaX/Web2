using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.Domain.Review
{
    public interface IReviewRepository
    {
        void Add(Review review);
    }
}
