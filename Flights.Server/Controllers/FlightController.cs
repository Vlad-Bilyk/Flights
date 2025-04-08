using Flights.Server.Domain.Entities;
using Flights.Server.Domain.Errors;
using Flights.Server.Dtos;
using Flights.Server.ReadModels;
using Microsoft.AspNetCore.Mvc;

namespace Flights.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlightController : ControllerBase
    {
        private static readonly Random random = new();
        private static readonly Flight[] flights =
        [
            new(Guid.NewGuid(),
                "American Airlines",
                random.Next(90, 5000).ToString(),
                new TimePlace("Los Angeles", DateTime.Now.AddHours(random.Next(1, 3))),
                new TimePlace("Istanbul", DateTime.Now.AddHours(random.Next(4, 10))),
                random.Next(1, 853)),
            new(Guid.NewGuid(),
                "Deutsche BA",
                random.Next(90, 5000).ToString(),
                new TimePlace("Munchen", DateTime.Now.AddHours(random.Next(1, 10))),
                new TimePlace("Schiphol", DateTime.Now.AddHours(random.Next(4, 15))),
                random.Next(1, 853)),
            new(Guid.NewGuid(),
                "British Airways",
                random.Next(90, 5000).ToString(),
                new TimePlace("London, England", DateTime.Now.AddHours(random.Next(1, 15))),
                new TimePlace("Vizzola-Ticino", DateTime.Now.AddHours(random.Next(4, 18))),
                random.Next(1, 853)),
            new(Guid.NewGuid(),
                "BB Heliag",
                random.Next(90, 5000).ToString(),
                new TimePlace("Zurich", DateTime.Now.AddHours(random.Next(1, 15))),
                new TimePlace("Baku", DateTime.Now.AddHours(random.Next(4, 19))),
                random.Next(1, 853)),
            new(Guid.NewGuid(),
                "Adria Airways",
                random.Next(90, 5000).ToString(),
                new TimePlace("Ljubljana", DateTime.Now.AddHours(random.Next(1, 15))),
                new TimePlace("Warshaw", DateTime.Now.AddHours(random.Next(4, 19))),
                random.Next(1, 853)),
            new(Guid.NewGuid(),
                "ABA Air",
                random.Next(90, 5000).ToString(),
                new TimePlace("Praha Ruzyne", DateTime.Now.AddHours(random.Next(1, 55))),
                new TimePlace("Paris", DateTime.Now.AddHours(random.Next(4, 58))),
                random.Next(1, 853)),

        ];

        public FlightController()
        {

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<FlightRm> Search()
        {
            var flightRmList = flights.Select(flight => new FlightRm(
                flight.Id,
                flight.Airline,
                flight.Price,
                new TimePlaceRm(flight.Departure.Place.ToString(), flight.Departure.Time),
                new TimePlaceRm(flight.Arrival.Place.ToString(), flight.Arrival.Time),
                flight.RemainingNumberOfSeats
            )).ToArray();

            return flightRmList;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<FlightRm> Find(Guid id)
        {
            var flight = flights.SingleOrDefault(f => f.Id == id);

            if (flight == null)
            {
                return NotFound();
            }

            var readModel = new FlightRm(
                flight.Id,
                flight.Airline,
                flight.Price,
                new TimePlaceRm(flight.Departure.Place.ToString(), flight.Departure.Time),
                new TimePlaceRm(flight.Arrival.Place.ToString(), flight.Arrival.Time),
                flight.RemainingNumberOfSeats
            );

            return Ok(readModel);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Book(BookDto dto)
        {
            System.Diagnostics.Debug.WriteLine($"Booking a new flight {dto.FlightId}");

            var flight = flights.SingleOrDefault(f => f.Id == dto.FlightId);

            if (flight is null)
            {
                return NotFound();
            }

            var error = flight.MakeBooking(dto.PassengerEmail, dto.NumberOfSeats);

            if (error is OverbookError)
            {
                return Conflict(new { message = "The number of requested seats exceeds the number of remaining seats" });
            }

            return CreatedAtAction(nameof(Find), new { id = dto.FlightId });
        }
    }
}
