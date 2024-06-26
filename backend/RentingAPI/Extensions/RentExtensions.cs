﻿using RentingAPI.Models;

namespace RentingAPI.Extensions
{
    public static class RentExtensions
    {
        public static RentResponse ToRentResponse(this Models.Data.Rent rent)
        {
            return new RentResponse
            {
                Id = rent.Id,
                ClientId = rent.ClientId,
                RegistrationDate = (DateTime)rent.RegistrationDate,
                ResourceId = rent.ResourceId,
                ReturnDate = (DateTime)rent.ReturnDate,
                CopyId = rent.CopyId,
                ResourceName=rent.Name
                
            };
        }
    }
}
