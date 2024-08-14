using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taxi.Domain.Abstractions;

namespace Taxi.Domain.Review
{
    public static class ReviewErrors
    {
        public static readonly Error NotEligible = new(
        "Review.NotEligible",
        "The review is not eligible because the booking is not yet completed");

        public static readonly Error NotFound = new(
       "Review.NotFound",
       "The review doesn not exist");
    }
}
