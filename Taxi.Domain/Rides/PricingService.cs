using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Taxi.Domain.Rides
{
    public class PricingService
    {
        private const string ApiKey = "pk.efbcae8c5d9dcd6bc03125912d8a6d4b";

        public double CalculatePrice(string startAddress, string endAddress)
        {
            double distance = GetDistance(startAddress, endAddress);
            double price = GeneratePrice(distance);
            return price;
        }

        private double GetDistance(string startAddress, string endAddress)
        {
            string startCoordinates = GetCoordinates(startAddress);
            string endCoordinates = GetCoordinates(endAddress);

            if (startCoordinates == null || endCoordinates == null)
            {
                throw new Exception("Could not retrieve coordinates for the provided addresses.");
            }

            var client = new RestClient($"https://us1.locationiq.com/v1/directions/driving/{startCoordinates};{endCoordinates}?key={ApiKey}");
            var request = new RestRequest();
            request.Method = Method.Get;

            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                var jsonResponse = JObject.Parse(response.Content!);
                double distanceInMeters = jsonResponse["routes"]![0]!["distance"]!.Value<double>();
                return distanceInMeters / 1000.0; // Convert to kilometers
            }
            else
            {
                throw new Exception("Error fetching distance from LocationIQ API.");
            }
        }



        private string GetCoordinates(string address)
        {
            var client = new RestClient($"https://us1.locationiq.com/v1/search.php?key={ApiKey}&q={Uri.EscapeDataString(address)}&format=json");
            var request = new RestRequest();
            request.Method = Method.Get;

            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                var jsonResponse = JArray.Parse(response.Content!);
                var latitude = jsonResponse[0]["lat"]!.Value<string>();
                var longitude = jsonResponse[0]["lon"]!.Value<string>();

                return $"{longitude},{latitude}";
            }
            else
            {
                return null;
            }
        }

        private double GeneratePrice(double distance)
        {
            // Example price calculation: Base fare + distance rate
            double baseFare = 5.00; // Base fare in currency units
            double ratePerKm = 1.50; // Rate per kilometer

            return baseFare + (ratePerKm * distance);
        }

        public double PredictWaitingTime(string startAddress, string endAddress)
        {
            double distance = GetDistance(startAddress, endAddress);

            // Assuming waiting time is 2 minutes per kilometer as a basic estimation.
            double waitingTime = distance * 2.0; // in minutes

            return waitingTime;
        }
    }
}