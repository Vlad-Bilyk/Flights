namespace Flights.Server.Domain.Entities
{
    public record Booking(
        string PassengerEmail,
        byte NumberOfSeats);
}