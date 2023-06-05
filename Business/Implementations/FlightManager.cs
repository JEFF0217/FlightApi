using Business.Interfaces;
using Business.Mappers;
using Business.Models;
using DataAccess.DTOs;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Implementations
{
    public class FlightManager : IFlightManager
    {
        private readonly IFlightApiService _flightApiService;

        public FlightManager(IFlightApiService flightApiService)
        {
            _flightApiService = flightApiService;
        }
        public async Task<List<Journey>> GetJournies(string origin, string destination)
        {
            try
            {
                List<FlightApiDTO> data = await _flightApiService.GetFlightData();
                List<Flight> flightData = FlightDataMapper.MapToFlights(data);
                Graph graph = new Graph();
                flightData.ForEach((item =>
                {
                    graph.AddFlight(item);
                }));
                List<Journey> journeys = graph.GetJourneysForDestination(origin, destination);

                return journeys;
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
