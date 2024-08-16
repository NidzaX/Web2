using MediatR;
using Microsoft.AspNetCore.Mvc;
using Taxi.Domain.Abstractions;
using Taxi.Application.Reviews.Commands.CalculateReview;
using Taxi.Application.Reviews.Commands.AddReview;

namespace Taxi.Api.Controllers.Reviews
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly CalculateReview _calculateReview;
        public ReviewsController(ISender sender, CalculateReview calculateReview)
        {
            _sender = sender;
            _calculateReview = calculateReview;

        }

        [HttpPost]
        public async Task<IActionResult> AddReview(AddReviewRequest request, CancellationToken cancellationToken)
        {
            var command = new AddReviewCommand(request.UserEmail, request.DriverId, request.Rating, request.Comment);

            Result result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }

        [HttpGet("median/{driverId}")]
        public async Task<IActionResult> GetMedianReviewScore(Guid driverId)
        {
            try
            {
                double medianScore = await _calculateReview.GetReviewScoresByDriverIdAsync(driverId);
                return Ok(medianScore);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
