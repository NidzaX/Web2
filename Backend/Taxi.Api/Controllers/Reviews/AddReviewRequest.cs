namespace Taxi.Api.Controllers.Reviews
{
    public sealed record AddReviewRequest(string UserEmail, Guid DriverId, int Rating, string Comment);
  
}
