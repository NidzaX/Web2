namespace Taxi.Api.Controllers.Reviews
{
    public record AddReviewRequest(string UserEmail, Guid DriverId, int Rating, string Comment);
  
}
