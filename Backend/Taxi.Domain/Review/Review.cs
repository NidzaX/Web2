using Taxi.Domain.Abstractions;

namespace Taxi.Domain.Review
{
    public sealed class Review : Entity
    {
        private Review(
            Guid id,
            Guid userId,
            Guid? driverId,
            Rating rating,
            Comment comment
        )
            : base(id)
        {
            Id = id;
            UserId = userId;
            DriverId = driverId;
            Rating = rating;
            Comment = comment;


        }

        private Review()
        {

        }

        public Guid? DriverId {get; private set; }

        public Guid UserId { get; private set;}

        public Rating Rating { get; private set; }

        public Comment Comment { get; private set; }


        public static Result<Review> Create(
            Guid userId,
            Guid? driverId,
            Rating rating,
            Comment comment
           )
        {
            Review review = new Review(Guid.NewGuid(), userId, driverId, rating, comment);

            return review;
        }
    }
}