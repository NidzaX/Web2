using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taxi.Application.Abstractions.Messaging;
using Taxi.Domain.Abstractions;
using Taxi.Domain.Review;
using Taxi.Domain.Rides;
using Taxi.Domain.Users;

namespace Taxi.Application.Reviews.AddReview
{
    internal sealed class AddReviewCommandHandler : ICommandHandler<AddReviewCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRideRepository _rideRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddReviewCommandHandler(
            IUserRepository userRepository,
            IRideRepository rideRepository,
            IReviewRepository reviewRepository,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _rideRepository = rideRepository;
            _reviewRepository = reviewRepository;
            _unitOfWork = unitOfWork;
        }



        public async Task<Result> Handle(AddReviewCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetUserByEmailAsync(request.UserEmail);
            Ride? ride = await _rideRepository.GetByIdAsync(request.DriverId, cancellationToken);

            if (ride == null || user == null)
            {
                return Result.Failure(RideErrors.NotFound);
            }

            Result<Rating> ratingResult = Rating.Create(request.Rating);

            if (ratingResult.IsFailure)
            {
                return Result.Failure(ratingResult.Error);
            }

            if(user.Id == ride.UserId)
            {
                Result<Review> reviewResult = Review.Create(
                    user.Id,
                    ride.Id,
                    ratingResult.Value,
                    new Comment(request.Comment));


                if (reviewResult.IsFailure)
                {
                    return Result.Failure(reviewResult.Error);
                }

                _reviewRepository.Add(reviewResult.Value);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result.Success();

            }


            return Result.Failure(new Error("Error", "Error"));
        }
    }
}
