using Flights.Server.ReadModels;
using Microsoft.AspNetCore.Mvc;

namespace Flights.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlightController : ControllerBase
    {
        private static readonly Random random = new();
        private readonly ILogger<FlightController> _logger;
        private static readonly FlightRm[] flights =
        [
            new(Guid.NewGuid(),
                "American Airlines",
                random.Next(90, 5000).ToString(),
                new TimePlaceRm("Los Angeles", DateTime.Now.AddHours(random.Next(1, 3))),
                new TimePlaceRm("Istanbul", DateTime.Now.AddHours(random.Next(4, 10))),
                random.Next(1, 853)),
            new(Guid.NewGuid(),
                "Deutsche BA",
                random.Next(90, 5000).ToString(),
                new TimePlaceRm("Munchen", DateTime.Now.AddHours(random.Next(1, 10))),
                new TimePlaceRm("Schiphol", DateTime.Now.AddHours(random.Next(4, 15))),
                random.Next(1, 853)),
            new(Guid.NewGuid(),
                "British Airways",
                random.Next(90, 5000).ToString(),
                new TimePlaceRm("London, England", DateTime.Now.AddHours(random.Next(1, 15))),
                new TimePlaceRm("Vizzola-Ticino", DateTime.Now.AddHours(random.Next(4, 18))),
                random.Next(1, 853)),
            new(Guid.NewGuid(),
                "BB Heliag",
                random.Next(90, 5000).ToString(),
                new TimePlaceRm("Zurich", DateTime.Now.AddHours(random.Next(1, 15))),
                new TimePlaceRm("Baku", DateTime.Now.AddHours(random.Next(4, 19))),
                random.Next(1, 853)),
            new(Guid.NewGuid(),
                "Adria Airways",
                random.Next(90, 5000).ToString(),
                new TimePlaceRm("Ljubljana", DateTime.Now.AddHours(random.Next(1, 15))),
                new TimePlaceRm("Warshaw", DateTime.Now.AddHours(random.Next(4, 19))),
                random.Next(1, 853)),
            new(Guid.NewGuid(),
                "ABA Air",
                random.Next(90, 5000).ToString(),
                new TimePlaceRm("Praha Ruzyne", DateTime.Now.AddHours(random.Next(1, 55))),
                new TimePlaceRm("Paris", DateTime.Now.AddHours(random.Next(4, 58))),
                random.Next(1, 853)),

        ];

        public FlightController(ILogger<FlightController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<FlightRm> Search()
            => flights;

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

            return Ok(flight);
        }
    }
}
