using Flights.Server.Domain.Entities;

namespace Flights.Server.Data
{
    public class Entities
    {
        public readonly IList<Passenger> Passengers = [];
        public readonly List<Flight> Flights = [];
    }
}
