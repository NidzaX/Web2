using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taxi.Domain.Abstractions;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Error = Taxi.Domain.Abstractions.Error;

namespace Taxi.Domain.Review
{
    public sealed record Rating
    {
        public static readonly Error Invalid = new("Rating.Invalid", "The rating is invalid");

        private Rating(int value) => Value = value;

        public int Value { get; init; }

        public static Result<Rating> Create(int value)
        {
            if(value < 1 || value > 5 )
            {
                return Result.Failure<Rating>(Invalid);
            }

            return new Rating(value);
        }



    }
}
