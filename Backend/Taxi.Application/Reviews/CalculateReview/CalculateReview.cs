using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Taxi.Domain.Abstractions;
using Taxi.Domain.Review;

namespace Taxi.Application.Reviews.CalculateReview
{
    public sealed class CalculateReview
    {
        private readonly IReviewRepository _reviewRepository;

        public CalculateReview(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<double> GetReviewScoresByDriverIdAsync(Guid driverId)
        {
          List<int> scores = await _reviewRepository.GetReviewScoresByDriverIdAsync(driverId);

            if (scores.Count == 0)
            {
                throw new InvalidOperationException("No reviews found for this driver.");
            }


            int sum = 0;
            foreach(int score in scores)
            {
                sum += score;
            }

            double average = sum /(double)scores.Count;

            return average;
        }
    }
}
