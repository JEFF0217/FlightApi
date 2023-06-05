using Business.Models;
using DataAccess.DTOs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Mappers
{
    internal class FlightDataMapper
    {

        public static List<Flight> MapToFlights(List<FlightApiDTO> data)
        {
            try
            {
                return data.Select(item => new Flight
                {
                    Transport = new Transport
                    {
                        FlightCarrier = item.flightCarrier,
                        FlightNumber = item.flightNumber
                    },
                    Origin = item.departureStation,
                    Destination = item.arrivalStation,
                    Price = item.price
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while mapping flight data to flights. Please contact support.", ex);
            }
        }
    }
}
