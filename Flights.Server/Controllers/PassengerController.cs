using Flights.Server.Data;
using Flights.Server.Domain.Entities;
using Flights.Server.Dtos;
using Flights.Server.ReadModels;
using Microsoft.AspNetCore.Mvc;

namespace Flights.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        private readonly Entities _entities;

        public PassengerController(Entities entities)
        {
            _entities = entities;
        }

        [HttpPost]
        [ProducesResponseType(typeof(NewPassengerDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Register(NewPassengerDto dto)
        {
            _entities.Passengers.Add(new Passenger(
                dto.Email,
                dto.FirstName,
                dto.LastName,
                dto.Gender));

            System.Diagnostics.Debug.WriteLine(_entities.Passengers.Count);
            return CreatedAtAction(nameof(Find), new { email = dto.Email }, dto);
        }

        [HttpGet("{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PassengerRm> Find(string email)
        {
            var passenger = _entities.Passengers.FirstOrDefault(p => p.Email == email);

            if (passenger == null)
            {
                return NotFound();
            }

            var rm = new PassengerRm(
                passenger.Email,
                passenger.FirstName,
                passenger.LastName,
                passenger.Gender
                );

            return Ok(rm);
        }
    }
}
