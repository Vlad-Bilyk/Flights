﻿namespace Flights.Server.Dtos
{
    public record NewPassengerDto(
        string Email,
        string FirstName,
        string LastName,
        bool Gender);
}
