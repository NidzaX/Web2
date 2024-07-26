namespace Taxi.Api.Controllers.Reviews
{
    public sealed record AddReviewRequest(string UserEmail, Guid RiderId, int Rating, string Comment);
  
}
