using Taxi.Domain.Rides;

namespace Taxi.Application.Abstractions.Api;

public interface IApiService
{
    public double CalculatePrice(string startAddress, string endAddress);
    public double PredictWaitingTime(string startAddress, string endAddress);
    public double EstimatePickupTime();
}
